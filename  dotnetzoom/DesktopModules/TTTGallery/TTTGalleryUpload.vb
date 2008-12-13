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

Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DotNetZoom
Imports DotNetZoom.TTTUtils
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Drawing


' Base objects for gallery content.

Namespace DotNetZoom


    Public Class GalleryUploadCollection
        Inherits ArrayList

        Private Const FileListCacheKeyPrefix As String = "flckp_"
        Private ZmoduleID As Integer
        Private _album As GalleryFolder
        Private ZgalleryConfig As GalleryConfig
        Private _uploadPath As String
        Private _errMessage As String
		Private _SpaceUsed As Double

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Shadows Sub Clear()
            MyBase.Clear()
        End Sub

        Public Shared Function GetList(ByVal Album As GalleryFolder, ByVal ModuleID As Integer) As GalleryUploadCollection
            ' Grab reference to the sessionstate object
            Dim sess As SessionState.HttpSessionState = HttpContext.Current.Session
            Dim strKey As String = Album.Name + ModuleID.ToString()

            If sess(FileListCacheKeyPrefix & strKey) Is Nothing Then
                ' If this object has not been instantiated yet, we need to grab it
                Dim fileList As GalleryUploadCollection = New GalleryUploadCollection(Album, ModuleID)
                sess.Add(FileListCacheKeyPrefix & strKey, fileList)
            End If

            Return CType(sess.Item(FileListCacheKeyPrefix & strKey), GalleryUploadCollection)

        End Function 'GetThreadInfo

        Public Shared Sub ResetList(ByVal Album As GalleryFolder, ByVal ModuleID As Integer)

            ' Grab reference to the sessionstate object
            Dim sess As SessionState.HttpSessionState = HttpContext.Current.Session
            Dim strKey As String = Album.Name + ModuleID.ToString()

            ' and delete the settings object
            sess.Remove(FileListCacheKeyPrefix & strKey)

        End Sub


        Public Sub New(ByVal Album As GalleryFolder, ByVal ModuleID As Integer)

            _album = Album
            ZmoduleID = ModuleID
            ZgalleryConfig = GalleryConfig.GetGalleryConfig(ModuleID)

        End Sub 'New

        Public Sub New(ByVal c As ICollection)
            MyBase.New(c)
        End Sub 'New

        Public Property Album() As GalleryFolder
            Get
                Return _album
            End Get
            Set(ByVal Value As GalleryFolder)
                _album = Value
            End Set
        End Property

        Public ReadOnly Property ErrMessage() As String
            Get
                Return _errMessage
            End Get
        End Property

        Public Function FileExists(ByVal FileName As String) As Boolean

            Dim filePath As String = Path.Combine(_album.Path, FileName)
            ' search in file system
            If IO.File.Exists(filePath) Then
                Return True
            End If

            ' search in this collection
            Dim i As Integer
            For i = 1 To Me.Count
                If CType(Me.Item(i - 1), GalleryUploadFile).Name = FileName Then
                    Return True
                End If
            Next

        End Function

        Public Function Upload(ByVal StrFolder As String) as Double
            Dim uploadFile As GalleryUploadFile
            Dim uploadPath As String
            Dim UploadFilePath As String = ""
            Dim albumFilePath As String
			Dim ThumbFilePath As String
            Dim IStobeResized As Boolean
            Dim i As Integer
			
			Dim TmpStrFolder As String
			Dim lWidth As Integer = 0
            Dim lHeight As Integer = 0
            Dim mImage As System.Drawing.Image
            Dim MaxWidth As Integer = ZgalleryConfig.MaximumThumbHeight
            Dim MaxHeight As Integer = ZgalleryConfig.MaximumThumbHeight
            _errMessage = ""
            _SpaceUsed = 0

            If ZgalleryConfig.IsFixedSize Then
                IStobeResized = True
            Else
                IStobeResized = False
            End If


            Dim objAdmin As New AdminDB()
            TmpStrFolder = HttpContext.Current.Request.MapPath(ZgalleryConfig.RootURL)

            For i = Count To 1 Step -1 ' Go backward to make sure correct item will be remove after uploading

                uploadFile = CType(Item(i - 1), GalleryUploadFile)
                If File.Exists(uploadFile.uploadFilePath) Then


                    If uploadFile.Type = IGalleryObjectInfo.ItemType.Zip Then
                        uploadPath = BuildPath(New String(1) {_album.Path, ZgalleryConfig.TempFolder}, "\", False, False)
                    Else
                        If Not IStobeResized Or uploadFile.Type = IGalleryObjectInfo.ItemType.Flash Or uploadFile.Type = IGalleryObjectInfo.ItemType.Movie Then
                            uploadPath = _album.Path
                        Else
                            uploadPath = BuildPath(New String(1) {_album.Path, ZgalleryConfig.SourceFolder}, "\", False, False)
                        End If
                    End If


                    albumFilePath = Path.Combine(_album.Path, uploadFile.Name.ToLower())
                    ThumbFilePath = Path.Combine(_album.Path, ZgalleryConfig.ThumbFolder & "\" & uploadFile.Name.ToLower())
                    ' Do upload - create folder if not exists then upload file
                    Try
                        If Not Directory.Exists(uploadPath) Then
                            Directory.CreateDirectory(uploadPath)
                        End If

                    Catch Exc As System.Exception
                        _errMessage += "<br> 1) " + Exc.Message
                    End Try

                    ' Do Resize
                    If IStobeResized AndAlso ZgalleryConfig.IsValidImageType(uploadFile.Extension) Then

                        Try 'Save it to album folder for display
                            ResizeImage(uploadFile.uploadFilePath, albumFilePath, ZgalleryConfig.FixedWidth, ZgalleryConfig.FixedHeight, uploadFile.Extension, ZgalleryConfig.Quality)
                            _SpaceUsed += FileLen(albumFilePath)
                            ' Add File Size
                            'Delete original source file to save disk space
                            If Not ZgalleryConfig.IsKeepSource Then
                                ' Delete File Size
                                _SpaceUsed -= FileLen(uploadFile.uploadFilePath)
                                File.Delete(uploadFile.uploadFilePath)
                            End If

                        Catch Exc As System.Exception
                            _errMessage += "<br> 2)" + Exc.Message
                        End Try

                    End If ' Resize

                    ' Update XMLData - except Zip file
                    ' If uploaded file is not Zip then update XML data - If it's a Zip then do Unzip
                    If Not uploadFile.Type = IGalleryObjectInfo.ItemType.Zip Then


                        If ZgalleryConfig.IsValidImageType(uploadFile.Extension) Then
                            Try
                                mImage = System.Drawing.Image.FromFile(albumFilePath)
                                lWidth = mImage.Width
                                lHeight = mImage.Height
                                ResizeImage(albumFilePath, ThumbFilePath, MaxWidth, MaxHeight, uploadFile.Extension, ZgalleryConfig.Quality)
                                _SpaceUsed += FileLen(ThumbFilePath)
                            Catch Exc As System.Exception
                                _errMessage += "<br> upload 3" + Exc.Message + "<br>" + albumFilePath + "<br>" + ThumbFilePath + "<br>"
                            End Try

                        Else
                            lWidth = 0
                            lHeight = 0
                        End If
                        ' Dim metaData As New GalleryXML(_album.Path)
                        GalleryXML.SaveMetaData(_album.Path, uploadFile.Name, uploadFile.Title, uploadFile.Description, uploadFile.Categories, uploadFile.OwnerID, lWidth.ToString(), lHeight.ToString())

                    Else
                        Dim objZipInputStream As New ZipInputStream(File.OpenRead(uploadFile.uploadFilePath))
                        Dim objZipEntry As ZipEntry
                        objZipEntry = objZipInputStream.GetNextEntry
                        While Not objZipEntry Is Nothing

                            Dim unzipFile As String = Unzip(objZipEntry, objZipInputStream, uploadPath).ToLower()

                            Dim strExtension As String = ""
                            If InStr(1, unzipFile, ".") > 0 Then
                                strExtension = Path.GetExtension(unzipFile)
                                UploadFilePath = Path.Combine(uploadPath, unzipFile)
                                albumFilePath = Path.Combine(_album.Path, unzipFile)
                                ThumbFilePath = Path.Combine(_album.Path, ZgalleryConfig.ThumbFolder & "\" & unzipFile)
                            End If
                            Try 'Save unzip file
                                If IStobeResized And ZgalleryConfig.IsValidImageType(strExtension) Then
                                    ResizeImage(UploadFilePath, albumFilePath, ZgalleryConfig.FixedWidth, ZgalleryConfig.FixedHeight, strExtension, ZgalleryConfig.Quality)
                                    _SpaceUsed += FileLen(albumFilePath)
                                    ' Add File Size  filelen(uploadFilePath)
                                ElseIf ZgalleryConfig.IsValidMovieType(strExtension) _
                                OrElse ZgalleryConfig.IsValidFlashType(strExtension) _
                                OrElse (Not IStobeResized And ZgalleryConfig.IsValidImageType(strExtension)) Then
                                    File.Copy(UploadFilePath, albumFilePath)
                                    _SpaceUsed += FileLen(albumFilePath)


                                End If

                                Dim metaData As New GalleryXML(_album.Path)

                                If ZgalleryConfig.IsValidImageType(strExtension) Then
                                    mImage = System.Drawing.Image.FromFile(albumFilePath)
                                    lWidth = mImage.Width
                                    lHeight = mImage.Height
                                    Try
                                        ResizeImage(albumFilePath, ThumbFilePath, MaxWidth, MaxHeight, strExtension, ZgalleryConfig.Quality)
                                        _SpaceUsed += FileLen(ThumbFilePath)
                                    Catch Exc As System.Exception
                                        _errMessage += "<br> Resize Image " + Exc.Message + "<br>" + albumFilePath + "<br>" + ThumbFilePath + "<br>"
                                    End Try
                                Else
                                    lWidth = 0
                                    lHeight = 0
                                End If


                                GalleryXML.SaveMetaData(_album.Path, unzipFile, "", "", uploadFile.Categories, uploadFile.OwnerID, lWidth.ToString(), lHeight.ToString())

                            Catch Exc As System.Exception
                                _errMessage += "<br> upload 5 " + Exc.Message
                            End Try

                            objZipEntry = objZipInputStream.GetNextEntry

                        End While

                        'Delete temp files & folder
                        _SpaceUsed -= objAdmin.GetFolderSizeRecursive(uploadPath)
                        IO.Directory.Delete(uploadPath, True)

                    End If 'Upzip

                    ' Remove uploaded file off the collection
                    Me.RemoveAt(i - 1)
                End If
            Next

            Return _SpaceUsed
        End Function

        Private Function Unzip(ByVal ZipEntry As ZipEntry, ByVal InputStream As ZipInputStream, ByVal TempDir As String) As String
            Dim strFileName As String
            Dim strFileNamePath As String

            Try
                strFileName = Path.GetFileName(ZipEntry.Name)

                If strFileName <> "" Then
                    strFileNamePath = Path.Combine(TempDir, strFileName)

                    If File.Exists(strFileNamePath) Then
					    _SpaceUsed -= Filelen(strFileNamePath)
                        File.Delete(strFileNamePath)
                    End If

                    Dim objFileStream As FileStream = File.Create(strFileNamePath)
                    Dim intSize As Integer = 2048
                    Dim arrData(2048) As Byte

                    intSize = InputStream.Read(arrData, 0, arrData.Length)
                    While intSize > 0
                        objFileStream.Write(arrData, 0, intSize)
                        intSize = InputStream.Read(arrData, 0, arrData.Length)
                    End While

                    objFileStream.Close()
					_SpaceUsed += Filelen(strFileNamePath)
                    Return strFileName

                End If

            Catch Exc As System.Exception
                _errMessage += "<br> unzip error " + Exc.Message
            End Try

        End Function

    End Class

    Public Class GalleryUploadFile

        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        
        Private ZmoduleID As Integer
        Private _title As String
        Private _description As String
        Private _categories As String
        Private _owner As String = ""
        Private _ownerID As Integer = 0
        Private _type As IGalleryObjectInfo.ItemType
        Private _icon As String
        Private _extension As String
		Private _Name As String
		Private _FileName As String
        Private _ContentType As String
        Private _uploadFilePath As String
		Private _ContentLength As Integer

        Public Property ModuleID() As Integer
            Get
                Return ZmoduleID
            End Get
            Set(ByVal Value As Integer)
                ZmoduleID = Value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
			Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

        Public Property FileName() As String
            Get
                Return _FileName
            End Get
			Set(ByVal Value As String)
                _FileName = Value
            End Set
        End Property

        Public Property uploadFilePath() As String
            Get
                Return _uploadFilePath
            End Get
            Set(ByVal Value As String)
                _uploadFilePath = Value
            End Set
        End Property



        Public Property ContentType() As String
            Get
                Return _ContentType
            End Get
			Set(ByVal Value As String)
                _ContentType = Value
            End Set
        End Property

        Public Property ContentLength() As Integer
            Get
                Return _ContentLength
            End Get
			Set(ByVal Value As Integer)
                _ContentLength = Value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal Value As String)
                _title = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
            End Set
        End Property

        Public Property Categories() As String
            Get
                Return _categories
            End Get
            Set(ByVal Value As String)
                _categories = Value
            End Set
        End Property

        Public Property OwnerID() As Integer
            Get
                Return _ownerID
            End Get
            Set(ByVal Value As Integer)
                _ownerID = Value
            End Set
        End Property

        Public ReadOnly Property Owner() As String
            Get
                Dim dbUser As New UsersDB()
                Dim dr As SqlDataReader = dbUser.GetSingleUser(_portalSettings.PortalId, _ownerID)
                If dr.Read Then
                    _owner = ConvertString(dr("UserName"))
                End If
                dr.Close()

                Return _owner
            End Get
        End Property

        Public ReadOnly Property Icon() As String
            Get
                Return _icon
            End Get
        End Property



        Public ReadOnly Property Type() As IGalleryObjectInfo.ItemType
            Get
                Return _type
            End Get
        End Property

        Public ReadOnly Property Extension() As String
            Get
                Return _extension
            End Get
        End Property

        Public Function ValidationInfo(ByVal ModuleID As Integer) As String
            Dim strReturn As String = ""
            Dim ZgalleryConfig As GalleryConfig = GalleryConfig.GetGalleryConfig(ModuleID)
			
            If _ContentLength = 0 Then
                Return GetLanguage("Gal_Invalid_FileType")
            End If

            Dim size As Double = (_ContentLength / 1024)
            If Not (size < ZgalleryConfig.MaxFileSize) Then
			strReturn = Replace(GetLanguage("Gal_MaxFileKB"), "{MaxFileSize}", ZgalleryConfig.MaxFileSize.ToString("#,##0.0"))
			strReturn = Replace(strReturn, "{FileSize}", (_ContentLength / 1024).tostring("#,##0.0"))
			strReturn = Replace(strReturn, "{FileName}", _FileName)
			Return strReturn
            End If

            _extension = Path.GetExtension(_FileName)

            If Not (ZgalleryConfig.IsValidFileType(_extension)) Then
                Return _extension & " " & GetLanguage("Gal_Invalid_FileType")

            Else 'Generate icon 
                If ZgalleryConfig.IsValidFlashType(_extension) Then
                    _type = IGalleryObjectInfo.ItemType.Flash
                    _icon = "~/images/TTT/TTT_s_flash.gif"

                ElseIf ZgalleryConfig.IsValidImageType(_extension) Then
                    _type = IGalleryObjectInfo.ItemType.Image
                    _icon = "~/images/TTT/TTT_s_jpg.gif"

                ElseIf ZgalleryConfig.IsValidMovieType(_extension) Then
                    _type = IGalleryObjectInfo.ItemType.Movie
                    _icon = "~/images/TTT/TTT_s_MediaPlayer.gif"

                ElseIf _extension = ".zip" Then
                    _type = IGalleryObjectInfo.ItemType.Zip
                    _icon = "~/images/TTT/TTT_s_zip.gif"

                End If
            End If
            Return ""

        End Function


    End Class

End Namespace