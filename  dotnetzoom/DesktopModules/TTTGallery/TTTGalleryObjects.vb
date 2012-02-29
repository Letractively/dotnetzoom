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
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
'========================================================================================

Imports System.xml
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Threading
Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Drawing
Imports System.Web.Caching

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

    Public Class LatLong
        Public Latitude As String
        Public Longitude As String
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
            Try
                Dim index As Integer

                index = MyBase.List.Add(value)
                keyIndexLookup.Add(key, index)
            Catch ex As Exception

            End Try

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

        Private _url As String
        Private _Width As String
        Private _Height As String
        Private _name As String
        Private _sort As String
        Private _thumbNail As String
        Private _icon As String
        Private _path As String
        Private _size As Long
        Private _index As Integer
        Private _title As String
        Private _description As String
        Private _categories As String
        Private _latitude As String
        Private _Longitude As String
        Private _gpsicon As String
        Private _gpsiconsize As String
        Private _link As String
        Private _type As IGalleryObjectInfo.ItemType
        Private _ownerID As Integer = 0
        Private _IsFolder As Boolean = False

#End Region

#Region "Properties"
        ' Latitude
        Public Property Latitude() As String Implements IGalleryObjectInfo.Latitude
            Get
                Return _latitude
            End Get
            Set(ByVal Value As String)
                _latitude = Value
            End Set
        End Property

        ' Longitude
        Public Property Longitude() As String Implements IGalleryObjectInfo.Longitude
            Get
                Return _Longitude
            End Get
            Set(ByVal Value As String)
                _Longitude = Value
            End Set
        End Property

        ' GPSIcon
        Public Property GPSIcon() As String Implements IGalleryObjectInfo.Gpsicon
            Get
                Return _gpsicon
            End Get
            Set(ByVal Value As String)
                _gpsicon = Value
            End Set
        End Property

        ' GPSIconsize
        Public Property GPSIconsize() As String Implements IGalleryObjectInfo.Gpsiconsize
            Get
                Return _gpsiconsize
            End Get
            Set(ByVal Value As String)
                _gpsiconsize = Value
            End Set
        End Property

        ' Link
        Public Property Link() As String Implements IGalleryObjectInfo.Link
            Get
                Return _link
            End Get
            Set(ByVal Value As String)
                _link = Value
            End Set
        End Property

        ' File name
        Public Property Name() As String Implements IGalleryObjectInfo.Name
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        ' Sort order
        Public Property Sort() As String Implements IGalleryObjectInfo.Sort
            Get
                If Len(_sort) > 0 Then
                    Return _sort
                Else
                    Return _index.ToString("d8")
                End If
            End Get
            Set(ByVal Value As String)
                _sort = Value
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
                _ownerID = Value
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
        Public Property Index() As Integer Implements IGalleryObjectInfo.Index
            Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        ' Size on disk
        Public Property Size() As Long Implements IGalleryObjectInfo.Size
            Get
                Return _size
            End Get
            Set(ByVal value As Long)
                _size = value
            End Set
        End Property

        ' Thumbnail URL for this file
        Public Property ThumbNail() As String Implements IGalleryObjectInfo.Thumbnail
            Get
                Return _thumbNail
            End Get
            Set(ByVal value As String)
                _thumbNail = value
            End Set
        End Property

        ' Icon URL for this file
        Public Property Icon() As String Implements IGalleryObjectInfo.Icon
            Get
                Return _icon
            End Get
            Set(ByVal value As String)
                _icon = value
            End Set
        End Property

        ' Is this a folder?
        Public Property IsFolder() As Boolean Implements IGalleryObjectInfo.IsFolder
            Get
                Return False
            End Get
            Set(ByVal value As Boolean)
                _IsFolder = value
            End Set
        End Property

        ' URL for this file
        Public Property URL() As String Implements IGalleryObjectInfo.URL
            Get
                Return _url
            End Get
            Set(ByVal value As String)
                _url = value
            End Set
        End Property

        ' Width for this file
        Public Property Width() As String Implements IGalleryObjectInfo.Width
            Get
                Return _Width
            End Get
            Set(ByVal value As String)
                _Width = value
            End Set
        End Property

        ' Height for this file
        Public Property Height() As String Implements IGalleryObjectInfo.Height
            Get
                Return _Height
            End Get
            Set(ByVal value As String)
                _Height = value
            End Set
        End Property




        Public Property Type() As IGalleryObjectInfo.ItemType Implements IGalleryObjectInfo.Type
            Get
                Return _type
            End Get
            Set(ByVal value As IGalleryObjectInfo.ItemType)
                _type = value
            End Set
        End Property



#End Region

#Region "Methods and Functions"

        Friend Sub New( _
            ByVal Sort As String, _
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
            ByVal Latitude As String, _
            ByVal Longitude As String, _
            ByVal gpsicon As String, _
            ByVal gpsiconsize As String, _
            ByVal link As String)

            _latitude = Latitude
            _Longitude = Longitude
            _gpsicon = gpsicon
            _gpsiconsize = gpsiconsize
            _link = link
            _sort = Sort
            _url = URL
            _Width = Width
            _Height = Height
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

        End Sub

#End Region

    End Class

    Public Class GalleryFolder
        Implements IGalleryObjectInfo

#Region "Private Variables"
        Private _galleryConfig As GalleryConfig

        Private _list As GalleryObjectCollection
        Private _url As String
        Private _Width As String
        Private _Height As String
        Private _latitude As String
        Private _Longitude As String
        Private _gpsicon As String
        Private _gpsiconsize As String
        Private _link As String
        Private _path As String
        Private _thumbPath As String
        Private _resPath As String
        Private _tempPath As String
        Private _sourcePath As String
        Private _isPopulated As Boolean
        Private _name As String
        Private _sort As String
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
        Private _IsFolder As Boolean = True
        Private _Type As IGalleryObjectInfo.ItemType


#End Region

#Region "Properties"

        ' Latitude
        Public Property Latitude() As String Implements IGalleryObjectInfo.Latitude
            Get
                Return _latitude
            End Get
            Set(ByVal Value As String)
                _latitude = Value
            End Set
        End Property

        ' Longitude
        Public Property Longitude() As String Implements IGalleryObjectInfo.Longitude
            Get
                Return _Longitude
            End Get
            Set(ByVal Value As String)
                _Longitude = Value
            End Set
        End Property

        ' GPSIcon
        Public Property GPSIcon() As String Implements IGalleryObjectInfo.Gpsicon
            Get
                Return _gpsicon
            End Get
            Set(ByVal Value As String)
                _gpsicon = Value
            End Set
        End Property

        ' GPSIconsize
        Public Property GPSIconsize() As String Implements IGalleryObjectInfo.Gpsiconsize
            Get
                Return _gpsiconsize
            End Get
            Set(ByVal Value As String)
                _gpsiconsize = Value
            End Set
        End Property

        ' Link
        Public Property Link() As String Implements IGalleryObjectInfo.Link
            Get
                Return _link
            End Get
            Set(ByVal Value As String)
                _link = Value
            End Set
        End Property

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

        Public Property Sort() As String Implements IGalleryObjectInfo.Sort
            Get
                If Len(_sort) > 0 Then
                    Return _sort
                Else
                    Return _index.ToString("d8")
                End If
            End Get
            Set(ByVal Value As String)
                _sort = Value
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
        Public Property Index() As Integer Implements IGalleryObjectInfo.Index
            Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        ' Path for this folder on the filesystem. 
        Public ReadOnly Property ThumbFolder() As String
            Get
                _thumbFolder = _galleryConfig.ThumbFolder
                Return _thumbFolder
            End Get
        End Property


        Public ReadOnly Property ResFolderPath() As String
            Get
                _resPath = BuildPath(New String(1) {_path, _galleryConfig.ResFolder}, "\", False, False)
                Return _resPath
            End Get
        End Property


        Public ReadOnly Property ThumbFolderPath() As String
            Get
                _thumbPath = BuildPath(New String(1) {_path, _galleryConfig.ThumbFolder}, "\", False, False)
                Return _thumbPath
            End Get
        End Property

        Public ReadOnly Property SourceFolderPath() As String
            Get
                _sourcePath = BuildPath(New String(1) {_path, _galleryConfig.SourceFolder}, "\", False, False)
                Return _sourcePath
            End Get
        End Property

        ' Returns the size of this folder
        Public Property Size() As Long Implements IGalleryObjectInfo.Size
            Get
                If _isPopulated Then
                    Return _list.Count
                Else
                    Return _size
                End If
            End Get
            Set(ByVal value As Long)
                _size = value
            End Set
        End Property

        ' URL for the thumbnail for this folder
        Public Property ThumbNail() As String Implements IGalleryObjectInfo.Thumbnail
            Get
                Return _thumbNail
            End Get
            Set(ByVal value As String)
                _thumbNail = value
            End Set
        End Property

        ' URL for the thumbnail for this folder
        Public Property Icon() As String Implements IGalleryObjectInfo.Icon
            Get
                Return _icon
            End Get
            Set(ByVal value As String)
                _icon = value
            End Set
        End Property

        ' Gets the relative path of this folder in relation to the root
        Public Property GalleryHierarchy() As String Implements IGalleryObjectInfo.URL
            Get
                Return _galleryHierarchy
            End Get
            Set(ByVal value As String)
                _galleryHierarchy = value
            End Set
        End Property

        ' Is this a folder?
        Public Property IsFolder() As Boolean Implements IGalleryObjectInfo.IsFolder
            Get
                Return True
            End Get
            Set(ByVal value As Boolean)
                _IsFolder = value
            End Set
        End Property

        Public Property Type() As IGalleryObjectInfo.ItemType Implements IGalleryObjectInfo.Type
            Get
                Return IGalleryObjectInfo.ItemType.Folder
            End Get
            Set(ByVal value As IGalleryObjectInfo.ItemType)
                _Type = value
            End Set
        End Property

        ' URL for this folder
        Public ReadOnly Property URL() As String
            Get
                Return _url
            End Get
        End Property

        ' Width for this folder
        Public Property Width() As String Implements IGalleryObjectInfo.Width
            Get
                Return _Width
            End Get
            Set(ByVal value As String)
                _Width = value
            End Set
        End Property

        ' Height for this folder
        Public Property Height() As String Implements IGalleryObjectInfo.Height
            Get
                Return _Height
            End Get
            Set(ByVal value As String)
                _Height = value
            End Set
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


#End Region

#Region "Public Methods and Functions"

        Sub New()
            MyBase.New()
        End Sub

        Friend Sub New( _
            ByVal Sort As String, _
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
            ByVal Latitude As String, _
            ByVal Longitude As String, _
            ByVal gpsicon As String, _
            ByVal gpsiconsize As String, _
            ByVal link As String, _
            ByVal Size As Long)

            _latitude = Latitude
            _Longitude = Longitude
            _gpsicon = gpsicon
            _gpsiconsize = gpsiconsize
            _link = link

            _list = New GalleryObjectCollection()
            _size = 0
            _url = URL
            _Width = Width
            _Height = Height
            _path = Path
            _name = Name
            _sort = Sort
            _galleryHierarchy = GalleryHierarchy
            _thumbNail = ThumbNail
            _icon = Icon
            _parent = Parent
            _index = Index
            _galleryConfig = GalleryConfig
            _title = Title
            _description = Description
            _categories = Categories
            _ownerID = OwnerID
            _size = Size

        End Sub

        Public Sub Clear()
            _list.Clear()
            _isPopulated = False
        End Sub

        ' Public Sub Save()
        '    If Not Directory.Exists(Me.Path) Then
        '       Try
        '          IO.Directory.CreateDirectory(Path)
        '         IO.Directory.CreateDirectory(ThumbFolderPath)
        '        IO.Directory.CreateDirectory(SourceFolderPath)
        '       IO.Directory.CreateDirectory(ResFolderPath)
        '  Catch exc As System.Exception
        ' End Try
        '    End If

        ' Dim metaData As New GalleryXML(Parent.Path)

        'Dim What As IGalleryObjectInfo
        '   What.Name = Name
        '  What.Title = Title
        ' What.Description = Description
        'What.Categories = Categories
        '    What.OwnerID = OwnerID
        '   What.Width = "0"
        '    What.Height = "0"
        '    What.Sort = Sort
        '    What.Latitude = Me.Latitude
        '    What.Longitude = Me.Longitude
        '    What.Gpsicon = Me.GPSIcon
        '    What.Gpsiconsize = Me.GPSIconsize
        '    What.Link = Me.Link
        '    What.IsFolder = True
        '    What.Size = Size

        '   GalleryXML.SaveGalleryData(Parent.Path, What)


        'End Sub

        Public Function CreateChild( _
            ByVal Name As String, _
            ByVal Title As String, _
            ByVal Description As String, _
            ByVal Categories As String, _
            ByVal OwnerID As Integer) As String

            Dim newFolderPath As String = BuildPath(New String(1) {_path, Name}, "\", False, True)
            Dim newThumbFolderPath As String = BuildPath(New String(1) {newFolderPath, _galleryConfig.ThumbFolder}, "\", False, True)
            Dim newSourceFolderPath As String = BuildPath(New String(1) {newFolderPath, _galleryConfig.SourceFolder}, "\", False, True)
            Dim newResFolderPath As String = BuildPath(New String(1) {newFolderPath, _galleryConfig.ResFolder}, "\", False, True)
            Dim NumberOfFile As Integer = 0
            Dim strInfo As String
            strInfo = GetLanguage("Gal_AlbumAdd")
            If IsNotAlpha(newFolderPath) Then
                strInfo = GetLanguage("Gal_AlbumNo")
            Else
                If Not Directory.Exists(newFolderPath) Then
                    Try
                        IO.Directory.CreateDirectory(newFolderPath)
                        IO.Directory.CreateDirectory(newThumbFolderPath)
                        IO.Directory.CreateDirectory(newSourceFolderPath)
                        IO.Directory.CreateDirectory(newResFolderPath)
                        ' Put Sort in the XML
                        ' Dim metaData As New GalleryXML(Me.Path)
                        ' Put Sort in the XML



                        For Each folder In Me.List
                            If TypeOf folder Is GalleryFolder AndAlso IsNumeric(folder.Sort) Then
                                NumberOfFile = CInt(folder.sort) + 1
                            End If
                        Next

                        ' Dim What As IGalleryObjectInfo
                        Dim What As New DataInfo
                        What.Name = Name
                        What.Title = Title
                        What.Description = Description
                        What.Categories = Categories
                        What.OwnerID = OwnerID
                        What.Width = "0"
                        What.Height = "0"
                        What.Sort = Name
                        What.Latitude = Me.Latitude
                        What.Longitude = Me.Longitude
                        What.Gpsicon = Me.GPSIcon
                        What.Gpsiconsize = Me.GPSIconsize
                        What.Link = ""
                        What.IsFolder = True
                        What.Size = 0

                        GalleryXML.SaveGalleryData(_path, What)


                        Dim metaData As New GalleryXML(_path)


                        GalleryXML.SaveMapMetaData(newFolderPath, _
                                                   metaData.Map_Width, _
                                                   metaData.Map_Height, _
                                                   metaData.Map_FullScreen, _
                                                   "[" + Me.Latitude + "," + Me.Longitude + "]", _
                                                   metaData.Map_Zoom, _
                                                   metaData.Map_Opacity, _
                                                   metaData.Map_Type, _
                                                   metaData.Map_ZoomClick, _
                                                   metaData.Map_ZoomMouse, _
                                                   metaData.Map_Centering, _
                                                   metaData.Map_ZoomControl, _
                                                   metaData.Map_ScaleControl, _
                                                   metaData.Map_CenterCoord, _
                                                   metaData.Map_Crosshair, _
                                                   metaData.Map_OpacityCtrl, _
                                                   metaData.Map_TypeCtrl, _
                                                   metaData.Map_TypeFltr, _
                                                   metaData.Map_TypeExcl, _
                                                   metaData.Map_LegendOn, _
                                                   metaData.Map_LegendPos, _
                                                   metaData.Map_LegendDrag, _
                                                   metaData.Map_LegendColl, _
                                                   metaData.Map_Tools, _
                                                   metaData.Map_TracklistOn, _
                                                   metaData.Map_TracklistPos, _
                                                   metaData.Map_TracklistMwidth, _
                                                   metaData.Map_TracklistMheight, _
                                                   metaData.Map_TracklistDesc, _
                                                   metaData.Map_TracklistZoom, _
                                                   metaData.Map_TracklistTool, _
                                                   metaData.Map_TracklistDrag, _
                                                   metaData.Map_TracklistColl, _
                                                   metaData.Map_Marker, _
                                                   metaData.Map_Shadows, _
                                                   metaData.Map_LinkTgt, _
                                                   metaData.Map_InfoWidth, _
                                                   metaData.Map_thumbnailwidth, _
                                                   metaData.Map_PhotoSize, _
                                                   metaData.Map_LabelsHide, _
                                                   metaData.Map_LabelOff, _
                                                   metaData.Map_LabelCenter, _
                                                   metaData.Map_DD, _
                                                   metaData.Map_IconSet)

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

                    Dim forumInfo As ForumItemInfo = ForumItemInfo.GetForumInfo(Me.IntegratedForumTabID)
                    With forumInfo
                        .IsIntegrated = False
                        .IntegratedAlbumName = ""
                        .IntegratedGallery = 0
                        .UpdateForumInfo()
                        .ResetForumInfo(Me.IntegratedForumID)
                    End With


                Else
                    SpaceUsed -= New FileInfo(Child.Path).Length
                    Dim SourceFilePath As String = BuildPath(New String(1) {Me.SourceFolderPath, Child.Name}, "\", False, False)
                    If IO.File.Exists(SourceFilePath) Then
                        SpaceUsed -= New FileInfo(SourceFilePath).Length
                        IO.File.Delete(SourceFilePath)
                    End If




                    IO.File.Delete(Child.Path)
                    If (Child.Thumbnail <> glbPath & "images/TTT/TTT_folder.gif") And (Child.Thumbnail <> glbPath & "images/TTT/TTT_MediaPlayer.gif") And (Child.Thumbnail <> glbPath & "images/TTT/TTT_Flash.gif") And _
                       (Child.Thumbnail <> ForumConfig.DefaultImageFolder() & "TTT_folder.gif") And (Child.Thumbnail <> ForumConfig.DefaultImageFolder() & "TTT_MediaPlayer.gif") And (Child.Thumbnail <> ForumConfig.DefaultImageFolder() & "TTT_Flash.gif") Then
                        SpaceUsed -= New FileInfo(HttpContext.Current.Server.MapPath(Child.Thumbnail)).Length
                        IO.File.Delete(HttpContext.Current.Server.MapPath(Child.Thumbnail))
                    End If
                End If

                ' Dim metaData As New GalleryXML(_path)
                GalleryXML.DeleteMetaData(_path, Child.Name)

            Catch exc As System.Exception
                If PortalSettings.GetHostSettings("EnableErrorReporting") <> "N" Then
                    ' Function SendNotification(ByVal strFrom As String, ByVal strTo As String, ByVal strBcc As String, ByVal strSubject As String, ByVal strBody As String, Optional ByVal strAttachment As String = "", Optional ByVal strBodyType As String = "")
                    SendNotification(PortalSettings.GetHostSettings("HostEmail"), PortalSettings.GetHostSettings("HostEmail2"), "", "DeleteChild", Child.Path + ControlChars.CrLf + ControlChars.CrLf + exc.ToString)
                End If
            End Try


            Return SpaceUsed

        End Function

        Public Sub Reset()
            Me.Clear()

            Dim item As FileInfo
            Dim dirInfo As New DirectoryInfo(Me.Path)
            Dim items As FileInfo() = dirInfo.GetFiles("*.dat")
            For Each item In items
                ' LogEvent("Delete : " + Name + vbCrLf)
                item.Delete()
            Next
            If File.Exists(BuildPath(New String(1) {Me.Path, "_sorteddata.xml"}, "\", False, True)) Then
                IO.File.Delete(BuildPath(New String(1) {Me.Path, "_sorteddata.xml"}, "\", False, True))
            End If
        End Sub

        Public Sub LogEvent(ByVal What As String)
            Dim objStream As StreamWriter
            Dim FileName As String
            FileName = "_" + Year(Now()).ToString + "-" + Month(Now).ToString + "-" + Day(Now).ToString + ".log"
            objStream = File.AppendText(BuildPath(New String(1) {_galleryConfig.RootPath, FileName}, "\", False, True))
            objStream.WriteLine(DateTime.Now.ToString() & " : " & What & ControlChars.CrLf)
            objStream.Close()
        End Sub


        Public Sub Populate()


            ' Populates the folder object with the info from XML file

            ' (in case someone decides to call this again without clearing the data first)
            _list.Clear()

            'HttpContext.Current.Response.Write("<!--Populate : " + Me.Path + "-->" + vbCrLf)

            If File.Exists(BuildPath(New String(1) {Me.Path, "_sorteddata.xml"}, "\", False, True)) Then

                Dim StrExtension As String
                Dim xmlDoc As New XmlDocument()
                Dim FileNode As XmlNode
                xmlDoc.Load(BuildPath(New String(1) {Me.Path, "_sorteddata.xml"}, "\", False, True))
                For Each FileNode In xmlDoc.SelectNodes("//files/file")
                    Dim What As New DataInfo
                    What.Name = FileNode.Attributes.GetNamedItem("name").InnerText
                    What.Categories = FileNode.Item("categories").InnerText
                    What.Description = FileNode.Item("description").InnerText
                    What.Height = FileNode.Item("height").InnerText
                    What.Icon = FileNode.Item("icon").InnerText
                    What.Index = CInt(FileNode.Item("index").InnerText)
                    What.IsFolder = CBool(FileNode.Item("isfolder").InnerText)
                    If InStr(FileNode.InnerXml, "<lat>") > 0 Then
                        If FileNode.Item("lat").InnerText <> "" Then
                            What.Latitude = FileNode.Item("lat").InnerText
                        Else
                            What.Latitude = "0"
                        End If
                        If FileNode.Item("long").InnerText <> "" Then
                            What.Longitude = FileNode.Item("long").InnerText
                        Else
                            What.Longitude = "0"
                        End If

                        What.GPSIcon = FileNode.Item("gpsicon").InnerText
                        What.GPSIconsize = FileNode.Item("gpsiconsize").InnerText
                        What.Link = FileNode.Item("link").InnerText
                    Else
                        What.Latitude = "0"
                        What.Longitude = "0"
                        What.GPSIcon = ""
                        What.GPSIconsize = ""
                        What.Link = ""
                    End If


                    Select Case FileNode.Item("type").InnerText
                        Case "Folder"
                            What.Type = IGalleryObjectInfo.ItemType.Folder
                        Case "Image"
                            What.Type = IGalleryObjectInfo.ItemType.Image
                        Case "Movie"
                            What.Type = IGalleryObjectInfo.ItemType.Movie
                        Case "Flash"
                            What.Type = IGalleryObjectInfo.ItemType.Flash
                        Case "Zip"
                            What.Type = IGalleryObjectInfo.ItemType.Zip

                    End Select

                    What.OwnerID = CInt(FileNode.Item("ownerid").InnerText)
                    What.Path = FileNode.Item("path").InnerText
                    What.Size = CType(FileNode.Item("size").InnerText, Long)
                    What.Sort = FileNode.Item("sort").InnerText
                    What.ThumbNail = FileNode.Item("thumbnail").InnerText
                    What.Title = FileNode.Item("title").InnerText
                    What.URL = FileNode.Item("url").InnerText
                    What.Width = FileNode.Item("width").InnerText



                    If What.IsFolder Then
                        'It is a folder
                        Dim newFolder As GalleryFolder = New GalleryFolder(What.Sort, What.Name, IO.Path.Combine(_path, What.Name), BuildPath(New String(1) {_url, What.Name}, "/", False, False), BuildPath(New String(1) {_galleryHierarchy, What.Name}, "/"), _thumbFolder, What.ThumbNail, What.Icon, Me, What.Index, _galleryConfig, What.Title, What.Description, What.Categories, What.OwnerID, What.Latitude, What.Longitude, What.GPSIcon, What.GPSIconsize, What.Link, What.Size)
                        _list.Add(What.Name, newFolder)
                    Else
                        Dim newFile As GalleryFile = New GalleryFile(What.Sort, What.Name, IO.Path.Combine(_path, What.Name), BuildPath(New String(1) {_url, What.Name}, "/", False, False), What.Width, What.Height, What.ThumbNail, What.Icon, What.Size, What.Index, What.Type, What.Title, What.Description, What.Categories, What.OwnerID, What.Latitude, What.Longitude, What.GPSIcon, What.GPSIconsize, What.Link)
                        _list.Add(What.Name, newFile)

                        If InStr(1, What.Name, ".") <> 0 Then
                            StrExtension = Mid(What.Name, InStrRev(What.Name, ".")).ToLower
                            If _galleryConfig.IsValidImageType(StrExtension) Then
                                _browsableItems.Add(What.Index) ' store reference to index
                            End If
                        End If


                    End If

                Next


            Else

                Dim items() As String
                Dim item As String
                Dim sort As String
                Dim name As String
                Dim thumbNail As String
                Dim icon As String
                Dim Counter As Integer = 0
                Dim albumOwnerID As Integer
                Dim fileOwnerID As Integer
                Dim lWidth As Integer
                Dim lHeight As Integer
                Dim TempArrayList As ArrayList = New ArrayList



                ' Get thumbnail specs

                Dim MaxWidth As Integer = _galleryConfig.MaximumThumbHeight
                Dim MaxHeight As Integer = _galleryConfig.MaximumThumbHeight

                ' Check for metadata folder
                Dim metaData As New GalleryXML(Me.Path)

                ' Add folders here.

                items = Directory.GetDirectories(_path)

                Dim myKeys(items.Length - 1) As String

                'Sort directories
                For Each item In items
                    name = IO.Path.GetFileName(item)
                    If metaData.Sort(name) <> "" Then
                        myKeys(Counter) = metaData.Sort(name)
                    Else
                        myKeys(Counter) = Counter.ToString("d8")
                    End If
                    Counter += 1
                Next
                Array.Sort(myKeys, items)
                Counter = 0

                For Each item In items
                    name = IO.Path.GetFileName(item)

                    ' Assign owner
                    albumOwnerID = ConvertInteger(metaData.OwnerID(name))
                    If albumOwnerID = 0 Then

                        albumOwnerID = Me.OwnerID
                    End If


                    If Not (Left(IO.Path.GetFileName(item), 1) = "_" OrElse IO.Path.GetFileName(item) = _thumbFolder) Then
                        ' ajout par rene boulard 2004-04-23 pour image folder
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), _galleryConfig.ResFolder, IO.Path.GetFileName(item) & ".jpg"}, "\", False, False)

                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ResFolder, IO.Path.GetFileName(item) & ".jpg"}, "/", False, False)
                        Else
                            thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), _galleryConfig.ResFolder, IO.Path.GetFileName(item) & ".gif"}, "\", False, False)
                            If File.Exists(thumbNail) Then
                                thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ResFolder, IO.Path.GetFileName(item) & ".gif"}, "/", False, False)
                            Else
                                thumbNail = ForumConfig.DefaultImageFolder() & "TTT_folder.gif"
                            End If
                        End If
                        icon = ForumConfig.DefaultImageFolder() & "TTT_s_folder.gif"
                        Dim newFolder As GalleryFolder = New GalleryFolder(metaData.Sort(name), name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), BuildPath(New String(1) {_galleryHierarchy, name}, "/"), _thumbFolder, thumbNail, icon, Me, Counter, _galleryConfig, metaData.Title(name), metaData.Description(name), metaData.Categories(name), albumOwnerID, metaData.latitude(name), metaData.Longitude(name), metaData.gpsicon(name), metaData.gpsiconsize(name), metaData.Link(name), metaData.Size(name))
                        _list.Add(name, newFolder)
                        TempArrayList.Add(CType(newFolder, IGalleryObjectInfo))
                        Counter += 1
                    End If
                Next



                ' Add files here
                items = System.IO.Directory.GetFiles(_path)

                Dim myFKeys(items.Length - 1) As String

                'Sort Files
                Dim FCounter As Integer = 0

                For Each item In items
                    name = IO.Path.GetFileName(item)
                    If metaData.Sort(name) <> "" Then
                        myFKeys(FCounter) = metaData.Sort(name)
                    Else
                        myFKeys(FCounter) = FCounter.ToString("d8")
                    End If
                    FCounter += 1
                Next

                Array.Sort(myFKeys, items)




                For Each item In items
                    name = IO.Path.GetFileName(item)
                    fileOwnerID = ConvertInteger(metaData.OwnerID(name))
                    If fileOwnerID = 0 Then 'Assign parent owner to be file Owner
                        fileOwnerID = Me.OwnerID
                    End If


                    ' Remember that we should add image first
                    If _galleryConfig.IsValidImageType(LCase(New FileInfo(item).Extension)) Then
                        ' This file is a valid image type Add the new image to the browsable items list

                        _browsableItems.Add(Counter) ' store reference to index

                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), _galleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "\", False, False)
                        icon = ForumConfig.DefaultImageFolder() & "TTT_s_jpg.gif"
                        thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "/", False, False)
                        lWidth = ConvertInteger(metaData.Width(name))
                        lHeight = ConvertInteger(metaData.height(name))
                        Dim newFile As GalleryFile = New GalleryFile(metaData.Sort(name), name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), lWidth.ToString(), lHeight.ToString(), thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Image, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, metaData.latitude(name), metaData.Longitude(name), metaData.gpsicon(name), metaData.gpsiconsize(name), metaData.Link(name))
                        _list.Add(name, newFile)
                        TempArrayList.Add(CType(newFile, IGalleryObjectInfo))
                        Counter += 1
                    End If

                    ' add movie types.
                    If _galleryConfig.IsValidMovieType(LCase(New FileInfo(item).Extension)) Then ' This file is a valid movie type



                        ' ajout par rene boulard 2004-04-23 pour image folder
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), _galleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "\", False, False)

                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "/", False, False)
                        Else
                            thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), _galleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "\", False, False)
                            If File.Exists(thumbNail) Then
                                thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "/", False, False)
                            Else
                                thumbNail = ForumConfig.DefaultImageFolder() & "TTT_MediaPlayer.gif"
                            End If
                        End If
                        icon = ForumConfig.DefaultImageFolder() & "TTT_s_MediaPlayer.gif"
                        ' Add the file and increment the counter
                        Dim newFile As GalleryFile = New GalleryFile(metaData.Sort(name), name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), "0", "0", thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Movie, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, metaData.latitude(name), metaData.Longitude(name), metaData.gpsicon(name), metaData.gpsiconsize(name), metaData.Link(name))
                        _list.Add(name, newFile)
                        TempArrayList.Add(CType(newFile, IGalleryObjectInfo))
                        Counter += 1
                    End If

                    ' add flash types.
                    If _galleryConfig.IsValidFlashType(LCase(New FileInfo(item).Extension)) Then


                        ' ajout par rene boulard 2004-04-23 pour image folder
                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), _galleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "\", False, False)

                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".jpg"}, "/", False, False)
                        Else
                            thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), _galleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "\", False, False)
                            If File.Exists(thumbNail) Then
                                thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ResFolder, IO.Path.GetFileNameWithoutExtension(item) & ".gif"}, "/", False, False)
                            Else
                                thumbNail = ForumConfig.DefaultImageFolder() & "TTT_Flash.gif"
                            End If
                        End If

                        icon = ForumConfig.DefaultImageFolder() & "TTT_s_flash.gif"

                        ' Add the file and increment the counter
                        Dim newFile As GalleryFile = New GalleryFile(metaData.Sort(name), name, IO.Path.Combine(_path, name), BuildPath(New String(1) {_url, name}, "/", False, False), "0", "0", thumbNail, icon, New FileInfo(item).Length, Counter, IGalleryObjectInfo.ItemType.Flash, metaData.Title(name), metaData.Description(name), metaData.Categories(name), fileOwnerID, metaData.latitude(name), metaData.Longitude(name), metaData.gpsicon(name), metaData.gpsiconsize(name), metaData.Link(name))
                        _list.Add(name, newFile)
                        TempArrayList.Add(CType(newFile, IGalleryObjectInfo))
                        Counter += 1

                    End If
                Next
                GalleryXML.SaveSortedData(Me.Path, TempArrayList)
                ' Size of _list.count()
                If Not Me.Parent Is Nothing Then
                    Dim FmetaData As New GalleryXML(Me.Parent.Path)
                    Dim What As IGalleryObjectInfo = FmetaData.CompleteInfo(Me.Name)
                    If What.Size <> _list.Count() Then
                        What.Size = _list.Count()
                        GalleryXML.SaveGalleryData(Me.Parent.Path, What)
                    End If
                End If

            End If


            ' Get integrated Forum Info
            If _galleryConfig.IsIntegrated Then
                Dim dbGallery As New TTT_GalleryDB()
                Dim dr As SqlDataReader = dbGallery.TTTGallery_GetIntegratedForum(_galleryConfig.ModuleID)
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

        Public Sub REPopulate(Optional ByVal ByDate As Boolean = False)

            ' REPopulates make sure the thumbs and files exists
            Dim items() As String
            Dim item As String
            Dim name As String
            Dim mlatitude As String
            Dim mlongitude As String
            Dim LWidth As Integer
            Dim LHeight As Integer
            Dim thumbNail As String

            Dim albumOwnerID As Integer
            Dim fileOwnerID As Integer
            Dim mImage As System.Drawing.Image
            Dim SaveIt As Boolean
            Dim TempSort As String
            Dim o As Integer = 0

            ' Get thumbnail specs

            Dim MaxWidth As Integer = _galleryConfig.MaximumThumbHeight
            Dim MaxHeight As Integer = _galleryConfig.MaximumThumbHeight

            Reset()



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

            ' Check source folder and create new image is not there
            Try

                items = System.IO.Directory.GetFiles(SourceFolderPath)

                For Each item In items
                    name = IO.Path.GetFileName(item)
                    If Not File.Exists(BuildPath(New String(1) {Me.Path, name}, "\", False, True)) Then
                        If _galleryConfig.IsValidImageType(LCase(New FileInfo(item).Extension)) Then
                            ResizeImage(item, BuildPath(New String(1) {Me.Path, name}, "\", False, True), _galleryConfig.FixedWidth, _galleryConfig.FixedHeight, LCase(New FileInfo(item).Extension), _galleryConfig.Quality)
                        ElseIf _galleryConfig.IsValidMovieType(LCase(New FileInfo(item).Extension)) Or _galleryConfig.IsValidFlashType(LCase(New FileInfo(item).Extension)) Then
                            'Copy the file
                            System.IO.File.Copy(item, BuildPath(New String(1) {Me.Path, name}, "\", False, True))
                        End If
                    End If
                Next


            Catch ex As Exception

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
                For i = 0 To elemList.Count - 1
                    Dim TempDoc As New XmlDocument()
                    TempDoc.LoadXml(elemList.Item(i).OuterXml)
                    Dim InnerList As XmlElement = TempDoc.DocumentElement
                    If (InnerList.HasAttribute("name")) Then
                        ' search in file system
                        If (Not IO.File.Exists(IO.Path.Combine(Me.Path, InnerList.GetAttribute("name"))) And Not System.IO.Directory.Exists(IO.Path.Combine(Me.Path, InnerList.GetAttribute("name")))) And Not _galleryConfig.IsValidFileType(LCase(New FileInfo(IO.Path.Combine(Me.Path, InnerList.GetAttribute("name"))).Extension)) Then
                            GalleryXML.DeleteMetaData(Me.Path, InnerList.GetAttribute("name"))
                        End If
                    End If
                Next i
            End If

            ' Add folders here.
            items = Directory.GetDirectories(_path)

            For Each item In items
                SaveIt = False
                name = IO.Path.GetFileName(item)
                If Not Left(name, 1) = "_" Then
                    ' Assign owner
                    o += o
                    albumOwnerID = ConvertInteger(metaData.OwnerID(name))
                    TempSort = metaData.Sort(name)
                    If albumOwnerID = 0 Then
                        albumOwnerID = Me.OwnerID
                        SaveIt = True
                    End If

                    If Not TempSort <> "" Then
                        SaveIt = True
                        TempSort = name
                    End If

                    If SaveIt Then

                        Dim What As IGalleryObjectInfo = metaData.CompleteInfo(name)
                        What.OwnerID = fileOwnerID
                        What.Sort = TempSort
                        'What.Latitude = mlatitude
                        'What.Longitude = mlongitude
                        What.IsFolder = True
                        GalleryXML.SaveGalleryData(_path, What)
                    End If
                End If

            Next

            items = System.IO.Directory.GetFiles(_path)


            For Each item In items
                name = IO.Path.GetFileName(item)
                SaveIt = False

                If Not Left(name, 1) = "_" And _galleryConfig.IsValidFileType(LCase(New FileInfo(item).Extension)) Then
                    o += o
                    LWidth = CInt(metaData.Width(name))
                    LHeight = CInt(metaData.height(name))
                    mlatitude = metaData.latitude(name)
                    mlongitude = metaData.Longitude(name)

                    fileOwnerID = ConvertInteger(metaData.OwnerID(name))


                    If fileOwnerID = 0 Then 'Assign parent owner to be file Owner
                        fileOwnerID = Me.OwnerID
                        SaveIt = True
                    End If

                    TempSort = metaData.Sort(name)
                    If Not metaData.Sort(name) <> "" Then
                        SaveIt = True
                        TempSort = name
                    End If

                    ' Remember that we should add image first
                    If _galleryConfig.IsValidImageType(LCase(New FileInfo(item).Extension)) Then
                        ' This file is a valid image type Add the new image to the browsable items list


                        thumbNail = BuildPath(New String(2) {IO.Path.GetDirectoryName(item), _galleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "\", False, False)

                        If File.Exists(thumbNail) Then
                            thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "/", False, False)
                        Else
                            Try ' Build the thumbs on the fly
                                ResizeImage(item, BuildPath(New String(2) {Me.Path, _galleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "\", False, False), MaxWidth, MaxHeight, IO.Path.GetExtension(item), _galleryConfig.Quality)
                                thumbNail = BuildPath(New String(2) {_url, _galleryConfig.ThumbFolder, IO.Path.GetFileName(item)}, "/", False, False)
                            Catch ex As Exception
                                Throw ex
                            End Try
                        End If


                        ' Add the file 
                        If LWidth = "0" Or LHeight = "0" Then
                            mImage = System.Drawing.Image.FromFile(item)
                            LWidth = mImage.Width
                            LHeight = mImage.Height
                            mImage.Dispose()
                            SaveIt = True
                        End If


                        ' Check Lat And Long
                        If mlatitude = "0" Or ByDate Then
                            Dim Exif As New ExifWorks(item)
                            If mlatitude = "0" Then
                                SaveIt = True
                                mlatitude = Exif.Latitude(CType(3, ExifWorks.LatLongFormat))
                                mlongitude = Exif.Longitude(CType(3, ExifWorks.LatLongFormat))
                            End If

                            If ByDate Then
                                TempSort = Exif.DateTimeOriginal.ToString("yyyy\-MM\-dd HH\:mm\:ss")
                                If TempSort = "0001-01-01 00:00:00" Then
                                    TempSort = Exif.DateTimeLastModified.ToString("yyyy\-MM\-dd HH\:mm\:ss")
                                    If TempSort = "0001-01-01 00:00:00" Then
                                        Dim fi As FileInfo = New FileInfo(IO.Path.Combine(Me.Path, name))
                                        TempSort = fi.CreationTimeUtc.ToString("yyyy\-MM\-dd HH\:mm\:ss")
                                    End If
                                End If
                            End If
                            If TempSort <> metaData.Sort(name) Then
                                SaveIt = True
                            End If
                            Exif.Dispose()
                        End If


                    Else
                        If ByDate Then
                            Dim fi As FileInfo = New FileInfo(IO.Path.Combine(Me.Path, name))
                            TempSort = fi.CreationTimeUtc.ToString("yyyy\-MM\-dd HH\:mm\:ss")
                            SaveIt = True
                        End If

                    End If

                    If SaveIt Then

                        Dim What As IGalleryObjectInfo = metaData.CompleteInfo(name)
                        'What.Name = name
                        'What.Title = Title
                        'What.Description = Description
                        'What.Categories = Categories
                        What.OwnerID = fileOwnerID
                        What.Width = LWidth.ToString()
                        What.Height = LHeight.ToString()
                        What.Sort = TempSort
                        What.Latitude = mlatitude
                        What.Longitude = mlongitude
                        'What.Gpsicon = Me.GPSIcon
                        'What.Gpsiconsize = Me.GPSIconsize
                        'What.Link = ""
                        What.IsFolder = False
                        What.Size = 0

                        GalleryXML.SaveGalleryData(_path, What)


                    End If

                End If


            Next
            ' Size of _list.count()
            If Not Me.Parent Is Nothing Then
                Dim FmetaData As New GalleryXML(Me.Parent.Path)
                Dim What As IGalleryObjectInfo = FmetaData.CompleteInfo(Me.Name)

                If What.Size <> o Then
                    What.Size = CType(o, Long)
                    GalleryXML.SaveGalleryData(Me.Parent.Path, What)
                End If
            End If

            ' Set the flag so we don't call again
            _isPopulated = False

        End Sub



#End Region

    End Class


    Public Class DataInfo
        Implements IGalleryObjectInfo

        Private _url As String
        Private _Width As String = "0"
        Private _Height As String = "0"
        Private _latitude As String = "0"
        Private _Longitude As String = "0"
        Private _gpsicon As String = "/images/gps/24camera.png"
        Private _gpsiconsize As String = "[24,24]"
        Private _link As String = ""
        Private _name As String
        Private _sort As String
        Private _thumbNail As String
        Private _icon As String
        Private _path As String
        Private _size As Long = 0
        Private _index As Integer
        Private _title As String
        Private _description As String
        Private _categories As String
        Private _type As IGalleryObjectInfo.ItemType
        Private _ownerID As Integer
        Private _IsFolder As Boolean

        ' Latitude
        Public Property Latitude() As String Implements IGalleryObjectInfo.Latitude
            Get
                Return _latitude
            End Get
            Set(ByVal Value As String)
                _latitude = Value
            End Set
        End Property

        ' Longitude
        Public Property Longitude() As String Implements IGalleryObjectInfo.Longitude
            Get
                Return _Longitude
            End Get
            Set(ByVal Value As String)
                _Longitude = Value
            End Set
        End Property

        ' GPSIcon
        Public Property GPSIcon() As String Implements IGalleryObjectInfo.Gpsicon
            Get
                Return _gpsicon
            End Get
            Set(ByVal Value As String)
                _gpsicon = Value
            End Set
        End Property

        ' GPSIconsize
        Public Property GPSIconsize() As String Implements IGalleryObjectInfo.Gpsiconsize
            Get
                Return _gpsiconsize
            End Get
            Set(ByVal Value As String)
                _gpsiconsize = Value
            End Set
        End Property

        ' Link
        Public Property Link() As String Implements IGalleryObjectInfo.Link
            Get
                Return _link
            End Get
            Set(ByVal Value As String)
                _link = Value
            End Set
        End Property

        Public Property Name() As String Implements IGalleryObjectInfo.Name
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        ' Sort order
        Public Property Sort() As String Implements IGalleryObjectInfo.Sort
            Get
                Return _sort
             End Get
            Set(ByVal Value As String)
                _sort = Value
            End Set
        End Property

        Public Property Title() As String Implements IGalleryObjectInfo.Title
            Get
                Return _title
            End Get
            Set(ByVal Value As String)
                _title = Value
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

         Public Property Path() As String Implements IGalleryObjectInfo.Path
            Get
                Return _path
            End Get
            Set(ByVal Value As String)
                _path = Value
            End Set
        End Property

        ' Index of this item in the collection
        Public Property Index() As Integer Implements IGalleryObjectInfo.Index
            Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        ' Size on disk
        Public Property Size() As Long Implements IGalleryObjectInfo.Size
            Get
                Return _size
            End Get
            Set(ByVal value As Long)
                _size = value
            End Set
        End Property

        ' Thumbnail URL for this file
        Public Property ThumbNail() As String Implements IGalleryObjectInfo.Thumbnail
            Get
                Return _thumbNail
            End Get
            Set(ByVal value As String)
                _thumbNail = value
            End Set
        End Property

        ' Icon URL for this file
        Public Property Icon() As String Implements IGalleryObjectInfo.Icon
            Get
                Return _icon
            End Get
            Set(ByVal value As String)
                _icon = value
            End Set
        End Property

        ' Is this a folder?
        Public Property IsFolder() As Boolean Implements IGalleryObjectInfo.IsFolder
            Get
                Return _IsFolder
            End Get
            Set(ByVal value As Boolean)
                _IsFolder = value
            End Set
        End Property

        ' URL for this file
        Public Property URL() As String Implements IGalleryObjectInfo.URL
            Get
                Return _url
            End Get
            Set(ByVal value As String)
                _url = value
            End Set
        End Property

        ' Width for this file
        Public Property Width() As String Implements IGalleryObjectInfo.Width
            Get
                Return _Width
            End Get
            Set(ByVal value As String)
                _Width = value
            End Set
        End Property

        ' Height for this file
        Public Property Height() As String Implements IGalleryObjectInfo.Height
            Get
                Return _Height
            End Get
            Set(ByVal value As String)
                _Height = value
            End Set
        End Property




        Public Property Type() As IGalleryObjectInfo.ItemType Implements IGalleryObjectInfo.Type
            Get
                Return _type
            End Get
            Set(ByVal value As IGalleryObjectInfo.ItemType)
                _type = value
            End Set
        End Property


    End Class


    Public Class mySortClass
        Implements IComparer

        ' Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
           Implements IComparer.Compare
            Dim _x As DataInfo = CType(x, DataInfo)
            Dim _y As DataInfo = CType(y, DataInfo)

            Return New CaseInsensitiveComparer().Compare(_x.Sort, _y.Sort)

            ' Dim myComparer = New mySortClass()
            ' myAL.Sort(myComparer)
        End Function 'IComparer.Compare

    End Class 'myReverserClass


    Public Interface IGalleryObjectInfo

        Property Name() As String
        Property Title() As String
        Property Sort() As String
        Property Description() As String
        Property Categories() As String
        Property OwnerID() As Integer
        Property Path() As String
        Property URL() As String
        Property Width() As String
        Property Height() As String
        Property Latitude() As String
        Property Longitude() As String
        Property Gpsicon() As String
        Property Gpsiconsize() As String
        Property Link() As String
        Property IsFolder() As Boolean
        Property Thumbnail() As String
        Property Icon() As String
        Property Size() As Long
        Property Index() As Integer
        Property Type() As ItemType

        Enum ItemType
            Folder
            Image
            Movie
            Flash
            Zip
        End Enum

    End Interface


End Namespace