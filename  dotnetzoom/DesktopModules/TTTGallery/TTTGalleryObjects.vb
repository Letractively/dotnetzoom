'=======================================================================================
' TTTGALLERY MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' Original version by:              DAVID BARRETT http://www.davidbarrett.net
' A modified version for DNN by:    KENNYRICE
' This version for TTTCompany       http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' Slideshow component:              FREEK VAN OORT
' Flash Player component:           TYLER JENSEN
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
'========================================================================================
Option Strict On

Imports System.xml
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Threading
Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Drawing

' Base objects for gallery content.

Namespace DotNetZoom

#Region "HelperObjects"

    '
    ' Following classes are to store details about the folder and pager items for databinding.
    '

    Public Class FolderDetail

        Public Name As String
        Public URL As String

    End Class

    Public Class PagerDetail

        Public Text As String
        Public Strip As Integer

    End Class
#End Region

    Public Class GalleryObjectCollection
        Inherits CollectionBase

#Region "Private Variables"
        ' Allows access to items by key AND index
        Private keyIndexLookup As Hashtable = New Hashtable()
#End Region

#Region "Methods and Functions"

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Shadows Sub Clear()
            keyIndexLookup.Clear()
            MyBase.Clear()
        End Sub

        Public Sub Add(ByVal key As String, ByVal value As Object)
            Dim index As Integer

            index = MyBase.List.Add(value)
            keyIndexLookup.Add(key, index)
        End Sub

        Public Function Item(ByVal index As Integer) As Object
            Try
                Dim obj As Object
                obj = MyBase.List.Item(index)
                Return obj
            Catch Exc As System.Exception
            End Try
        End Function

        Public Function Item(ByVal key As String) As Object
            Dim index As Integer
            Dim obj As Object

            ' Do validation first
            If keyIndexLookup.Item(key) Is Nothing Then
                Throw New ArgumentException("Impossible de trouver l'item demandé.")
            End If

            index = CInt(keyIndexLookup.Item(key))
            obj = MyBase.List.Item(index)

            Return obj
        End Function

#End Region

    End Class

    Public Class GalleryFile
        Implements IGalleryObjectInfo

#Region "Private Variables"

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Private _url As String
		Private _Width As String
		Private _Height As String
        Private _name As String
        Private _thumbNail As String
        Private _icon As String
        Private _path As String
        Private _size As Long
        Private _index As Integer
        Private _title As String
        Private _description As String
        Private _categories As String
        Private _type As IGalleryObjectInfo.ItemType
        Private _owner As GalleryUser
        Private _ownerID As Integer = 0
        Private _isEditable As Boolean = False

#End Region

#Region "Properties"

        ' File name
        Public Property Name() As String Implements IGalleryObjectInfo.Name
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        ' Added to match interface
        Public Property Title() As String Implements IGalleryObjectInfo.Title
            Get
                If Len(_title) > 0 Then
                    Return _title
                Else
                    Return _name
                End If
            End Get
            Set(ByVal Value As String)
                _title = Value
            End Set
        End Property

        ' For new feature User Gallery
        Public Property OwnerID() As Integer Implements IGalleryObjectInfo.OwnerID
            Get
                Return _ownerID
            End Get
            Set(ByVal Value As Integer)
                OwnerID = Value
            End Set
        End Property

		
        Public Property Description() As String Implements IGalleryObjectInfo.Description
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
            End Set
        End Property

        Public Property Categories() As String Implements IGalleryObjectInfo.Categories
            Get
                Return _Categories
            End Get
            Set(ByVal Value As String)
                _Categories = Value
            End Set
        End Property

        ' Filesystem path
        Public Property Path() As String Implements IGalleryObjectInfo.Path
            Get
                Return _path
            End Get
            Set(ByVal Value As String)
                _path = Value
            End Set
        End Property

        ' Index of this item in the collection
        Public ReadOnly Property Index() As Integer Implements IGalleryObjectInfo.Index
            Get
                Return _index
            End Get
        End Property

        ' Size on disk
        Public ReadOnly Property Size() As Long Implements IGalleryObjectInfo.Size
            Get
                Return _size
            End Get
        End Property

        ' Thumbnail URL for this file
        Public ReadOnly Property ThumbNail() As String Implements IGalleryObjectInfo.Thumbnail
            Get
                Return _thumbNail
            End Get
        End Property

        ' Icon URL for this file
        Public ReadOnly Property Icon() As String Implements IGalleryObjectInfo.Icon
            Get
                Return _icon
            End Get
        End Property

        ' Is this a folder?
        Public ReadOnly Property IsFolder() As Boolean Implements IGalleryObjectInfo.IsFolder
            Get
                Return False
            End Get
        End Property

        ' URL for this file
        Public ReadOnly Property URL() As String Implements IGalleryObjectInfo.URL
            Get
                Return _url
            End Get
        End Property

        ' Width for this file
        Public ReadOnly Property Width() As String Implements IGalleryObjectInfo.Width
            Get
                Return _Width
            End Get
        End Property

        ' Height for this file
        Public ReadOnly Property Height() As String Implements IGalleryObjectInfo.Height
            Get
                Return _Height
            End Get
        End Property



		
        Public ReadOnly Property Type() As IGalleryObjectInfo.ItemType Implements IGalleryObjectInfo.Type
            Get
                Return _type
            End Get
        End Property

        Public ReadOnly Property Owner() As GalleryUser Implements IGalleryObjectInfo.Owner
            Get
                Return _owner
            End Get
        End Property

#End Region

#Region "Methods and Functions"

        Friend Sub New( _
            ByVal Name As String, _
            ByVal Path As String, _
            ByVal URL As String, _
            ByVal Width As String, _
            ByVal Height As String, _
            ByVal ThumbNail As String, _
            ByVal Icon As String, _
            ByVal Size As Long, _
            ByVal Index As Integer, _
            ByVal Type As IGalleryObjectInfo.ItemType, _
            ByVal Title As String, _
            ByVal Description As String, _
            ByVal Categories As String, _
            ByVal OwnerID As Integer, _
            ByVal Owner As GalleryUser)

            _url = URL
            _Width = Width
            _height = height
            _path = Path
            _name = Name
            _thumbNail = ThumbNail
            _icon = Icon
            _size = Size
            _index = Index
            _type = Type
            _title = Title
            _description = Description
            _categories = Categories
            _ownerID = OwnerID
            _owner = Owner

        End Sub

#End Region

    End Class

    Public Class GalleryFolder
        Implements IGalleryObjectInfo

#Region "Private Variables"
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Private ZgalleryConfig As GalleryConfig

        Private _list As GalleryObjectCollection
        Private _url As String
		Private _Width As String
		Private _Height As String
        Private _path As String
        Private _thumbPath As String
		Private _resPath As String
        Private _tempPath As String
        Private _sourcePath As String
        Private _isPopulated As Boolean
        Private _name As String
        Private _thumbFolder As String
        Private _galleryHierarchy As String
        Private _thumbNail As String
        Private _icon As String
        Private _size As Long
        Private _parent As GalleryFolder
        Private _title As String
        Private _description As String
        Private _index As Integer
        Private _browsableItems As ArrayList = New ArrayList()
        Private _IntegratedForumID As Integer
        Private _IntegratedForumTabID As Integer
        Private _categories As String
        Private _ownerID As Integer = 0
        Private _owner As GalleryUser

#End Region

#Region "Properties"


        ' Name of this folder
        Public Property Name() As String Implements IGalleryObjectInfo.Name
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        Public Property Parent() As GalleryFolder
            Get
                Return _parent
            End Get
            Set(ByVal Value As GalleryFolder)
                _parent = Value
            End Set
        End Property

        ' Path for this folder on the filesystem.
        Public Property Path() As String Implements IGalleryObjectInfo.Path
            Get
                '_path = BuildPath(New String(1) {_parent.FolderPath(), _name}, "\", False, False)
                Return _path
            End Get
            Set(ByVal Value As String)
                _path = Value
            End Set
        End Property

        Public Property Title() As String Implements IGalleryObjectInfo.Title
            Get
                If Len(_title) > 0 Then
                    Return _title
                Else
                    Return _name
                End If
            End Get
            Set(ByVal Value As String)
                _title = Value
            End Set
        End Property

        Public Property Description() As String Implements IGalleryObjectInfo.Description
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
            End Set
        End Property

        Public Property Categories() As String Implements IGalleryObjectInfo.Categories
            Get
                Return _categories
            End Get
            Set(ByVal Value As String)
                _categories = Value
            End Set
        End Property

        Public Property OwnerID() As Integer Implements IGalleryObjectInfo.OwnerID
            Get
                Return _ownerID
            End Get
            Set(ByVal Value As Integer)
                _ownerID = Value
            End Set
        End Property

        ' Collection of items that can be viewed using the viewer
        Public ReadOnly Property BrowsableItems() As ArrayList
            Get
                Return _browsableItems
            End Get
        End Property

        ' Index of this item in the collection
        Public ReadOnly Property Index() As Integer Implements IGalleryObjectInfo.Index
            Get
                Return _index
            End Get
        End Property

        ' Path for this folder on the filesystem. 
        Public ReadOnly Property ThumbFolder() As String
            Get
                _thumbFolder = ZgalleryConfig.ThumbFolder
            End Get
        End Property

		
        Public ReadOnly Property ResFolderPath() As String
            Get
                _ResPath = BuildPath(New String(1) {_path, ZgalleryConfig.ResFolder}, "\", False, False)
                Return _ResPath
            End Get
        End Property

		
        Public ReadOnly Property ThumbFolderPath() As String
            Get
                _thumbPath = BuildPath(New String(1) {_path, ZgalleryConfig.ThumbFolder}, "\", False, False)
                Return _thumbPath
            End Get
        End Property

        Public ReadOnly Property SourceFolderPath() As String
            Get
                _sourcePath = BuildPath(New String(1) {_path, ZgalleryConfig.SourceFolder}, "\", False, False)
                Return _sourcePath
            End Get
        End Property

        ' Returns the size of this folder
        Public ReadOnly Property Size() As Long Implements IGalleryObjectInfo.Size
            Get
                Return _list.Count
            End Get
        End Property

        ' URL for the thumbnail for this folder
        Public ReadOnly Property ThumbNail() As String Implements IGalleryObjectInfo.Thumbnail
            Get
                Return _thumbNail
            End Get
        End Property

        ' URL for the thumbnail for this folder
        Public ReadOnly Property Icon() As String Implements IGalleryObjectInfo.Icon
            Get
                Return _icon
            End Get
        End Property

        ' Gets the relative path of this folder in relation to the root
        Public ReadOnly Property GalleryHierarchy() As String Implements IGalleryObjectInfo.URL
            Get
                Return _galleryHierarchy
            End Get
        End Property

        ' Is this a folder?
        Public ReadOnly Property IsFolder() As Boolean Implements IGalleryObjectInfo.IsFolder
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property Type() As IGalleryObjectInfo.ItemType Implements IGalleryObjectInfo.Type
            Get
                Return IGalleryObjectInfo.ItemType.Folder
            End Get
        End Property

        ' URL for this folder
        Public ReadOnly Property URL() As String
            Get
                Return _url
            End Get
        End Property

        ' Width for this folder
        Public ReadOnly Property Width() As String Implements IGalleryObjectInfo.width
            Get
                Return _Width
            End Get
        End Property

        ' Height for this folder
        Public ReadOnly Property Height() As String Implements IGalleryObjectInfo.Height
            Get
                Return _Height
            End Get
        End Property
		
		
        ' Collection of objects implementing IGalleryObjectInfo
        Public ReadOnly Property List() As GalleryObjectCollection
            Get
                Return _list
            End Get
        End Property

        ' Whether this folder has been populated or not
        Public ReadOnly Property IsPopulated() As Boolean
            Get
                Return _isPopulated
            End Get
        End Property

        ' Whether this folder has at least one browsable item
        Public ReadOnly Property IsBrowsable() As Boolean
            Get
                Return (_browsableItems.Count > 0)
            End Get
        End Property

        Public ReadOnly Property IntegratedForumID() As Integer
            Get
                Return _IntegratedForumID
            End Get
        End Property

        Public ReadOnly Property IntegratedForumTabID() As Integer
            Get
                Return _IntegratedForumTabID
            End Get
        End Property

        Public ReadOnly Property Owner() As GalleryUser Implements IGalleryObjectInfo.Owner
            Get
                Return _owner
            End Get
        End Property


#End Region

#Region "Public Methods and Functions"

        Sub New()
            MyBase.New()
        End Sub

        Friend Sub New( _
            ByVal Name As String, _
            ByVal Path As String, _
            ByVal URL As String, _
            ByVal GalleryHierarchy As String, _
            ByVal ThumbFolder As String, _
            ByVal ThumbNail As String, _
            ByVal Icon As String, _
            ByVal Parent As GalleryFolder, _
            ByVal Index As Integer, _
            ByVal GalleryConfig As GalleryConfig, _
            ByVal Title As String, _
            ByVal Description As String, _
            ByVal Categories As String, _
            ByVal OwnerID As Integer, _
            ByVal Owner As GalleryUser)

            _list = New GalleryObjectCollection()
            _url = URL
			_Width = Width
			_Height = Height
            _path = Path
            _name = Name
            _galleryHierarchy = GalleryHierarchy
            _thumbNail = ThumbNail
            _icon = Icon
            _parent = Parent
            _index = Index
            ZgalleryConfig = GalleryConfig
            _title = Title
            _description = Description
            _categories = Categories
            _ownerID = OwnerID
            _owner = Owner

        End Sub

        Public Sub Clear()
            _list.Clear()
            _isPopulated = False
        End Sub

        Public Sub Save()
            If Not Directory.Exists(Me.Path) Then
                Try
                    IO.Directory.CreateDirectory(Path)
                    IO.Directory.CreateDirectory(ThumbFolderPath)
                    IO.Directory.CreateDirectory(SourceFolderPath)
					IO.Directory.CreateDirectory(ResFolderPath)
                Catch exc As System.Exception
                End Try
            End If

            ' Dim metaData As New GalleryXML(Parent.Path)
            GalleryXML.SaveMetaData(Parent.Path, Name, Title, Description, Categories, OwnerID, "0", "0")

        End Sub

        Public Function CreateChild( _
            ByVal Name As String, _
            ByVal Title As String, _
            ByVal Description As String, _
            ByVal Categories As String, _
            ByVal OwnerID As Integer) As String

            Dim newFolderPath As String = BuildPath(New String(1) {_path, Name}, "\", False, True)
            Dim newThumbFolderPath As String = BuildPath(New String(1) {newFolderPath, ZgalleryConfig.ThumbFolder}, "\", False, True)
            Dim newSourceFolderPath As String = BuildPath(New String(1) {newFolderPath, ZgalleryConfig.SourceFolder}, "\", False, True)
            Dim newResFolderPath As String = BuildPath(New String(1) {newFolderPath, ZgalleryConfig.ResFolder}, "\", False, True)
 
            Dim strInfo As String
			strInfo = GetLanguage("Gal_AlbumAdd")
			If IsNotAlpha(newFolderPath) then
			strInfo = GetLanguage("Gal_AlbumNo") 
			else
            If Not Directory.Exists(newFolderPath) Then
                Try
                    IO.Directory.CreateDirectory(newFolderPath)
                    IO.Directory.CreateDirectory(newThumbFolderPath)
                    IO.Directory.CreateDirectory(newSourceFolderPath)
					IO.Directory.CreateDirectory(newResFolderPath)

                    ' Dim metaData As New GalleryXML(Me.Path)
                    GalleryXML.SaveMetaData(_path, Name, Title, Description, Categories, OwnerID, "0", "0")

                Catch exc As System.Exception
                        strInfo = "CreateChild " + exc.Message
                End Try
            Else
                strInfo = GetLanguage("Gal_AlbumExist") 
            End If
			End If
            
            Return strInfo

        End Function

        Public Function DeleteChild(ByVal Child As IGalleryObjectInfo) As Double
            
            Dim SpaceUsed As Double = 0

            Try
                If Child.IsFolder Then
                    IO.Directory.Delete(Child.Path, True)
					Dim objAdmin As New AdminDB()
					SpaceUsed -= objAdmin.GetFolderSizeRecursive(Child.Path)
                Else
				SpaceUsed -= Filelen(Child.Path)
                IO.File.Delete(Child.Path)
                    If (Child.Thumbnail <> glbPath & "images/TTT/TTT_folder.gif") And (Child.ThumbNail <> "/images/TTT/TTT_MediaPlayer.gif") And (Child.ThumbNail <> "/images/TTT/TTT_Flash.gif") Then
                        SpaceUsed -= FileLen(HttpContext.Current.Server.MapPath(Child.Thumbnail))
                        IO.File.Delete(HttpContext.Current.Server.MapPath(Child.Thumbnail))
                    End If
                End If

                ' Dim metaData As New GalleryXML(_path)
                GalleryXML.DeleteMetaData(_path, Child.Name)

            Catch exc As System.Exception

            End Try


            Return SpaceUsed

        End Function

        Public Sub Populate()

            ' Populates the folder object with the info from XML file
            Dim items() As String
            Dim item As String
            Dim name As String
            Dim thumbNail As String
            Dim icon As String
            Dim Counter As Integer = 0
            Dim albumOwnerID As Integer
            Dim albumOwner As GalleryUser
            Dim fileOwnerID As Integer
            Dim fileOwner As GalleryUser
            Dim lWidth As Integer
            Dim lHeight As Integer


            ' Get thumbnail specs

            Dim MaxWidth As Integer = ZgalleryConfig.MaximumThumbHeight
            Dim MaxHeight As Integer = ZgalleryConfig.MaximumThumbHeight

            ' (in case someone decides to call this again without clearing the data first)
            _list.Clear()



            ' Check for metadata folder
            Dim metaData As New GalleryXML(Me.Path)

            ' Add folders here.
            items = Directory.GetDirectories(_path)

            For Each item In items
                name = IO.Path.GetFileName(item)

                ' Assign owner
                albumOwnerID = ConvertInteger(metaData.OwnerID(name))
                If AlbumOwnerID = 0 Then

                    AlbumOwnerID = Me.OwnerID
                End If
                albumOwner = GalleryUser.GetGalleryUser(albumOwnerID)

                If Not (Left(IO.Path.GetFileName(item), 1) = "_" OrElse IO.Path.GetFileName(item) = _thumbFolder) Then
                    ' ajout par rene boulard 2004-04-23 pour image folder
                    thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileName(item) & ".jpg"}, "\", False, False)

                    If File.Exists(thumbNail) Then
                        thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileName(item) & ".jpg"}, "/", False, False)
                    Else
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileName(item) & ".gif"}, "\", False, False)
                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileName(item) & ".gif"}, "/", False, False)
                        Else
                            thumbNail = glbPath & "images/TTT/TTT_folder.gif"
                        End If
                    End If
                    icon = glbPath & "images/TTT/TTT_s_folder.gif"
                    Dim newFolder As GalleryFolder = New GalleryFolder(name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), BuildPath(New String(1) {_galleryHierarchy, name}, "/"), _thumbFolder, thumbNail, icon, Me, Counter, ZgalleryConfig, metaData.Title(name), metaData.Description(name), metaData.Categories(name), albumOwnerID, albumOwner)
                    _list.Add(name, newFolder)
                    Counter += 1
                End If
            Next

            ' Add files here
            items = System.IO.Directory.GetFiles(_path)

            For Each item In items
                name = IO.Path.GetFileName(item)
                fileOwnerID = ConvertInteger(metaData.OwnerID(name))
                If fileOwnerID = 0 Then 'Assign parent owner to be file Owner
                    fileOwnerID = Me.OwnerID
                End If
                fileOwner = GalleryUser.GetGalleryUser(fileOwnerID)

                ' Remember that we should add image first
                If ZgalleryConfig.IsValidImageType(LCase(New FileInfo(item).Extension)) Then
                    ' This file is a valid image type Add the new image to the browsable items list

                    _browsableItems.Add(Counter) ' store reference to index

                    thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "\", False, False)
                    icon = glbPath & "images/TTT/TTT_s_jpg.gif"
                    thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "/", False, False)
                    lwidth = ConvertInteger(metaData.Width(name))
                    lHeight = ConvertInteger(metaData.height(name))
                    Dim newFile As GalleryFile = New GalleryFile(name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), lWidth.ToString(), lHeight.ToString(), thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Image, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, fileOwner)
                    _list.Add(name, newFile)
                    Counter += 1
                End If

                ' add movie types.
                If ZgalleryConfig.IsValidMovieType(LCase(New FileInfo(item).Extension)) Then ' This file is a valid movie type



                    ' ajout par rene boulard 2004-04-23 pour image folder
                    thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "\", False, False)

                    If File.Exists(thumbNail) Then
                        thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "/", False, False)
                    Else
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "\", False, False)
                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "/", False, False)
                        Else
                            thumbNail = glbPath & "images/TTT/TTT_MediaPlayer.gif"
                        End If
                    End If
                    icon = glbPath & "images/TTT/TTT_s_MediaPlayer.gif"
                    ' Add the file and increment the counter
                    Dim newFile As GalleryFile = New GalleryFile(name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), "0", "0", thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Movie, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, fileOwner)
                    _list.Add(name, newFile)
                    Counter += 1
                End If

                ' add flash types.
                If ZgalleryConfig.IsValidFlashType(LCase(New FileInfo(item).Extension)) Then


                    ' ajout par rene boulard 2004-04-23 pour image folder
                    thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "\", False, False)

                    If File.Exists(thumbNail) Then
                        thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "/", False, False)
                    Else
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "\", False, False)
                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "/", False, False)
                        Else
                            thumbNail = glbPath & "images/TTT/TTT_Flash.gif"
                        End If
                    End If

                    icon = glbPath & "images/TTT/TTT_s_flash.gif"

                    ' Add the file and increment the counter
                    Dim newFile As GalleryFile = New GalleryFile(name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), "0", "0", thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Flash, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, fileOwner)
                    _list.Add(name, newFile)

                    Counter += 1

                End If
            Next

            ' Get integrated Forum Info
            If ZgalleryConfig.IsIntegrated Then
                Dim dbGallery As New TTT_GalleryDB()
                Dim dr As SqlDataReader = dbGallery.TTTGallery_GetIntegratedForum(ZgalleryConfig.ModuleID)
                While dr.Read
                    If ConvertString(dr("AlbumName")) = _name Then
                        _IntegratedForumID = ConvertInteger(dr("ForumID"))
                        _IntegratedForumTabID = ConvertInteger(dr("TabID"))
                        Exit While
                    End If
                End While
                dr.Close()
            End If
            ' Set the flag so we don't call again
            _isPopulated = True
        End Sub

        Public Sub REPopulate()

            ' REPopulates make sure the thumbs and files exists
            Dim items() As String
            Dim item As String
            Dim name As String
            Dim thumbNail As String
            Dim icon As String
            Dim Counter As Integer = 0
            Dim albumOwnerID As Integer
            Dim albumOwner As GalleryUser
            Dim fileOwnerID As Integer
            Dim fileOwner As GalleryUser
            Dim lWidth As Integer
            Dim lHeight As Integer
            Dim mImage As System.Drawing.Image
            Dim SaveIt As Boolean


            ' Get thumbnail specs

            Dim MaxWidth As Integer = ZgalleryConfig.MaximumThumbHeight
            Dim MaxHeight As Integer = ZgalleryConfig.MaximumThumbHeight

            ' (in case someone decides to call this again without clearing the data first)
            _list.Clear()


            ' Check existence of res folder
            Try
                If Not System.IO.Directory.Exists(ResFolderPath) Then
                    ' and create it if required
                    System.IO.Directory.CreateDirectory(Me.ResFolderPath)
                End If
            Catch ex As Exception
                Throw ex
            End Try



            ' Check existence of thumbs folder
            Try
                If Not System.IO.Directory.Exists(ThumbFolderPath) Then
                    ' and create it if required
                    System.IO.Directory.CreateDirectory(Me.ThumbFolderPath)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            ' Check for metadata folder
            Dim metaData As New GalleryXML(Me.Path)

            ' Find out if metadata file exists; if it does, check to make sure it is coordinated with file system
            If File.Exists(BuildPath(New String(1) {Me.Path, "_metadata.xml"}, "\", False, True)) Then
                Dim doc As New XmlDocument()
                doc.Load(BuildPath(New String(1) {Me.Path, "_metadata.xml"}, "\", False, True))

                Dim root As XmlElement = doc.DocumentElement
                Dim elemList As XmlNodeList = root.GetElementsByTagName("file")

                ' Dim elemList As XmlNodeList = doc.GetElementsByTagName("files")
                Dim i As Integer
                Dim objStream As StreamWriter
                objStream = File.AppendText(BuildPath(New String(1) {Me.Path, "_metadata.log"}, "\", False, True))
                For i = 0 To elemList.Count - 1
                    Dim TempDoc As New XmlDocument()
                    TempDoc.LoadXml(elemList.Item(i).OuterXML)
                    Dim InnerList As XmlElement = Tempdoc.DocumentElement
                    If (InnerList.HasAttribute("name")) Then
                        ' search in file system
                        If Not IO.File.Exists(io.Path.Combine(Me.Path, InnerList.GetAttribute("name"))) And Not System.IO.Directory.Exists(io.Path.Combine(Me.Path, InnerList.GetAttribute("name"))) Then
                            GalleryXML.deletemetadata(Me.Path, InnerList.GetAttribute("name"))
                            objStream.WriteLine(DateTime.Now.ToString() & " effacer : " & io.Path.Combine(Me.Path, InnerList.GetAttribute("name")) & VbCrLf)
                        End If
                    End If
                Next i
                objStream.Close()
            End If



            ' Add folders here.
            items = Directory.GetDirectories(_path)

            For Each item In items
                name = IO.Path.GetFileName(item)

                ' Assign owner
                albumOwnerID = ConvertInteger(metaData.OwnerID(name))
                If AlbumOwnerID = 0 Then
                    AlbumOwnerID = Me.OwnerID
                    GalleryXML.SaveMetaData(_path, name, metaData.Title(name), metaData.Description(name), metaData.Categories(name), albumOwnerID, metaData.Width(name), metaData.height(name))
                End If
                albumOwner = GalleryUser.GetGalleryUser(albumOwnerID)

                If Not (Left(IO.Path.GetFileName(item), 1) = "_" OrElse IO.Path.GetFileName(item) = _thumbFolder) Then
                    ' ajout par rene boulard 2004-04-23 pour image folder
                    thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileName(item) & ".jpg"}, "\", False, False)

                    If File.Exists(thumbNail) Then
                        thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileName(item) & ".jpg"}, "/", False, False)
                    Else
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileName(item) & ".gif"}, "\", False, False)
                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileName(item) & ".gif"}, "/", False, False)
                        Else
                            thumbNail = glbPath & "images/TTT/TTT_folder.gif"
                        End If
                    End If
                    icon = glbPath & "images/TTT/TTT_s_folder.gif"
                    Dim newFolder As GalleryFolder = New GalleryFolder(name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), BuildPath(New String(1) {_galleryHierarchy, name}, "/"), _thumbFolder, thumbNail, icon, Me, Counter, ZgalleryConfig, metaData.Title(name), metaData.Description(name), metaData.Categories(name), albumOwnerID, albumOwner)
                    _list.Add(name, newFolder)
                    Counter += 1
                End If
            Next

            ' Add files here
            items = System.IO.Directory.GetFiles(_path)

            For Each item In items
                name = IO.Path.GetFileName(item)
                SaveIt = False
                fileOwnerID = ConvertInteger(metaData.OwnerID(name))
                If fileOwnerID = 0 Then 'Assign parent owner to be file Owner
                    fileOwnerID = Me.OwnerID
                    SaveIt = True
                End If
                fileOwner = GalleryUser.GetGalleryUser(fileOwnerID)

                ' Remember that we should add image first
                If ZgalleryConfig.IsValidImageType(LCase(New FileInfo(item).Extension)) Then
                    ' This file is a valid image type Add the new image to the browsable items list

                    _browsableItems.Add(Counter) ' store reference to index

                    thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "\", False, False)
                    icon = glbPath & "images/TTT/TTT_s_jpg.gif"

                    If File.Exists(thumbNail) Then
                        thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "/", False, False)
                    Else
                        Try ' Build the thumbs on the fly
                            ResizeImage(item, BuildPath(New String(2) {Me.Path, ZgalleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "\", False, False), MaxWidth, MaxHeight, IO.Path.GetExtension(item), ZgalleryConfig.quality)
                            thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "/", False, False)
                        Catch ex As Exception
                            Throw ex
                        End Try
                    End If



                    ' Add the file and increment the counter
                    If metaData.Width(name) = "0" Or metaData.height(name) = "0" Or SaveIt Then
                        mImage = System.Drawing.Image.FromFile(item)
                        lWidth = mImage.Width
                        lHeight = mImage.Height
                        GalleryXML.SaveMetaData(_path, name, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, LWidth.ToString(), Lheight.ToString())
                    Else
                        lwidth = ConvertInteger(metaData.Width(name))
                        lHeight = ConvertInteger(metaData.height(name))
                    End If

                    Dim newFile As GalleryFile = New GalleryFile(name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), lWidth.ToString(), lHeight.ToString(), thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Image, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, fileOwner)
                    _list.Add(name, newFile)

                    Counter += 1

                End If

                ' add movie types.
                If ZgalleryConfig.IsValidMovieType(LCase(New FileInfo(item).Extension)) Then ' This file is a valid movie type



                    ' ajout par rene boulard 2004-04-23 pour image folder
                    thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "\", False, False)

                    If File.Exists(thumbNail) Then
                        thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "/", False, False)
                    Else
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "\", False, False)
                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "/", False, False)
                        Else
                            thumbNail = glbPath & "images/TTT/TTT_MediaPlayer.gif"
                        End If
                    End If

                    icon = glbPath & "images/TTT/TTT_s_MediaPlayer.gif"
                    ' Add the file and increment the counter
                    Dim newFile As GalleryFile = New GalleryFile(name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), "0", "0", thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Movie, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, fileOwner)
                    _list.Add(name, newFile)

                    Counter += 1

                End If

                ' add flash types.
                If ZgalleryConfig.IsValidFlashType(LCase(New FileInfo(item).Extension)) Then


                    ' ajout par rene boulard 2004-04-23 pour image folder
                    thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "\", False, False)

                    If File.Exists(thumbNail) Then
                        thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "/", False, False)
                    Else
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "\", False, False)
                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, ZgalleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "/", False, False)
                        Else
                            thumbNail = glbPath & "images/TTT/TTT_Flash.gif"
                        End If
                    End If

                    icon = glbPath & "images/TTT/TTT_s_flash.gif"

                    ' Add the file and increment the counter
                    Dim newFile As GalleryFile = New GalleryFile(name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), "0", "0", thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Flash, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, fileOwner)
                    _list.Add(name, newFile)

                    Counter += 1

                End If
            Next

            ' Get integrated Forum Info
            If ZgalleryConfig.IsIntegrated Then
                Dim dbGallery As New TTT_GalleryDB()
                Dim dr As SqlDataReader = dbGallery.TTTGallery_GetIntegratedForum(ZgalleryConfig.ModuleID)
                While dr.Read
                    If ConvertString(dr("AlbumName")) = _name Then
                        _IntegratedForumID = ConvertInteger(dr("ForumID"))
                        _IntegratedForumTabID = ConvertInteger(dr("TabID"))
                        Exit While
                    End If
                End While
                dr.Close()
            End If

            ' Set the flag so we don't call again
            _isPopulated = True

        End Sub
		
		
		
#End Region

    End Class

    Public Interface IGalleryObjectInfo

        Property Name() As String
        Property Title() As String
        Property Description() As String
        Property Categories() As String
        Property OwnerID() As Integer
        Property Path() As String
        ReadOnly Property URL() As String
        ReadOnly Property Width() As String
        ReadOnly Property Height() As String
        ReadOnly Property IsFolder() As Boolean
        ReadOnly Property Thumbnail() As String
        ReadOnly Property Icon() As String
        ReadOnly Property Size() As Long
        ReadOnly Property Index() As Integer
        ReadOnly Property Owner() As GalleryUser
        ReadOnly Property Type() As ItemType

        Enum ItemType
            Folder
            Image
            Movie
            Flash
            Zip
        End Enum

    End Interface


End Namespace