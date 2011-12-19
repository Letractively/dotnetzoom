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
Option Strict On

Imports System.Web
Imports System.Text

Namespace DotNetZoom

    Public Class GalleryRequest
        ' Class for traditional gallery viewing (i.e., 'strip' viewing)
        Inherits BaseGalleryRequest

#Region "Private Variables"

        Private _req As HttpRequest

        Private _currentStrip As Integer
        Private _SelectedIndex As Integer
        Private _stripCount As Integer
        Private _startItem As Integer
        Private _endItem As Integer
        Private _firstImageIndex As Integer = -1
        Private ZpagerItems As New ArrayList()

#End Region

#Region "Public Properties"


        Public ReadOnly Property StartItem() As Integer
            ' Beginning item of this page
            Get
                Return _startItem
            End Get
        End Property

        Public ReadOnly Property EndItem() As Integer
            ' Ending item of this page
            Get
                Return _endItem
            End Get
        End Property

        Public ReadOnly Property CurrentStrip() As Integer
            ' Current page being viewed
            Get
                Return _currentStrip
            End Get
        End Property


        Public ReadOnly Property SelectedIndex() As Integer
            ' Current page being viewed
            Get
                Return _SelectedIndex
            End Get
        End Property

        Public ReadOnly Property StripCount() As Integer
            ' Count of pages existing in this folder
            Get
                Return _stripCount
            End Get
        End Property

        Public ReadOnly Property FirstImageIndex() As Integer
            Get
                Return _firstImageIndex
            End Get
        End Property

        Public Function PagerItems() As ArrayList
            ' Collection of pages to be clicked on for navigation

            Return ZpagerItems

        End Function


        Public Function CurrentItems() As ArrayList
            ' Collection of items for current page only

            Dim result As New ArrayList()
            Dim intCounter As Integer

            If _startItem > 0 Then
                For intCounter = _startItem To _endItem
                    result.Add(MyBase.Folder.List.Item(intCounter - 1))

                    ' find the first image in these current items
                    If CType(MyBase.Folder.List.Item(intCounter - 1), IGalleryObjectInfo).Type = IGalleryObjectInfo.ItemType.Image _
                    AndAlso _firstImageIndex = -1 Then
                        _firstImageIndex = (intCounter - 1)
                    End If
                Next
            End If

            Return result

        End Function

        Public Function SubAlbumItems() As ArrayList
            ' Collection of items for current page only

            Dim result As New ArrayList()
            Dim intCounter As Integer

            For intCounter = 1 To MyBase.Folder.List.Count
                If CType(MyBase.Folder.List.Item(intCounter - 1), IGalleryObjectInfo).IsFolder Then
                    result.Add(MyBase.Folder.List.Item(intCounter - 1))
                End If
            Next

            Return result

        End Function

        Public Function FileItems() As ArrayList
            ' Collection of items for current page only

            Dim result As New ArrayList()
            Dim intCounter As Integer

            For intCounter = 1 To MyBase.Folder.List.Count
                If Not CType(MyBase.Folder.List.Item(intCounter - 1), IGalleryObjectInfo).IsFolder Then
                    result.Add(MyBase.Folder.List.Item(intCounter - 1))
                End If
            Next

            Return result

        End Function


#End Region

#Region "Public Methods"

        Public Sub New(ByVal ModuleID As Integer)
            ' Constructor method for the traditional gallery request
            MyBase.New(ModuleID)

            ' Don't want to continue processing if this is an invalid path
            If Not GalleryConfig.IsValidPath Then
                Exit Sub
            End If

            Dim newPagerDetail As PagerDetail
            Dim pagerCounter As Integer

            ' Grab current request context
            _req = HttpContext.Current.Request

            Try
                ' Logic to determine paging
                _currentStrip = CInt(_req("CurrentStrip"))
            Catch ex As Exception
                _currentStrip = 1
            End Try

            Try

                ' Get the count of pages for this folder
                _stripCount = CInt(System.Math.Ceiling(MyBase.Folder.List.Count / MyBase.GalleryConfig.ItemsPerStrip))

            Catch ex As Exception
                _stripCount = 1
            End Try

            ' Do a little validation
            If _currentStrip = 0 OrElse (_currentStrip > _stripCount) Then
                _currentStrip = 1
            End If

            ' Calculate the starting item
            If MyBase.Folder.List.Count = 0 Then
                _startItem = 0
            Else
                _startItem = (_currentStrip - 1) * MyBase.GalleryConfig.ItemsPerStrip + 1
            End If
            ' and calculate the ending item
            _endItem = _startItem + MyBase.GalleryConfig.ItemsPerStrip - 1
            If _endItem > MyBase.Folder.List.Count Then
                _endItem = MyBase.Folder.List.Count
            End If

            ' Creates the pager items
            ' Create the previous item
            Dim TempSelectedIndex As Integer = 0
            _SelectedIndex = TempSelectedIndex

            If _stripCount > 1 AndAlso _currentStrip > 1 Then
                newPagerDetail = New PagerDetail()
                newPagerDetail.Strip = _currentStrip - 1
                newPagerDetail.Text = GetLanguage("Gal_Prev0")
                ZpagerItems.Add(newPagerDetail)
                TempSelectedIndex += +1
            End If

            For pagerCounter = 1 To _stripCount
                If (pagerCounter \ 10 = _currentStrip \ 10) Or (pagerCounter = (pagerCounter \ 10) * 10) Or pagerCounter = 1 Then
                    newPagerDetail = New PagerDetail()
                    newPagerDetail.Strip = pagerCounter
                    If pagerCounter \ 10 = _currentStrip \ 10 Then
                        newPagerDetail.Text = CStr(pagerCounter)
                        If pagerCounter = _currentStrip Then
                            _SelectedIndex = TempSelectedIndex
                        End If
                        TempSelectedIndex += +1
                    Else
                        newPagerDetail.Text = CStr(pagerCounter)
                        TempSelectedIndex += +1
                    End If
                    ZpagerItems.Add(newPagerDetail)
                End If
            Next


            ' Creates the next item
            If _stripCount > 1 AndAlso _currentStrip < _stripCount Then
                newPagerDetail = New PagerDetail()
                newPagerDetail.Strip = _currentStrip + 1
                newPagerDetail.Text = GetLanguage("Gal_Next0")
                ZpagerItems.Add(newPagerDetail)
            End If

        End Sub

#End Region

    End Class

    Public Class GalleryViewerRequest
        ' Class for using the viewer to view galleries
        Inherits BaseGalleryRequest

#Region "Private variables"

        Private _req As HttpRequest

        ' Stored reference to ItemIndex of Browseable Items collection
        Private _currentItemIndex As Integer

        Private _currentItem As Integer
        Private _nextItem As Integer
        Private _previousItem As Integer
		Private _currentstrip As Integer

#End Region

#Region "Public Properties"


        Public ReadOnly Property CurrentItem() As GalleryFile
            Get
                Dim File As GalleryFile
                Try
                    File = CType(MyBase.Folder.List.Item(_currentItemIndex), GalleryFile)
                Catch ex As Exception
                    Dim Request As System.Web.HttpRequest
                    Request = HttpContext.Current.Request
                    SendHttpException("404", "Not Found", Request)
                End Try
                Return File
            End Get
        End Property

        Public ReadOnly Property CurrentItemNumber() As Integer
            Get
                Return _currentItem + 1
            End Get
        End Property

		Public ReadOnly Property currentstrip() As Integer
            Get
                Return _currentstrip
            End Get
        End Property
		
		
		Public ReadOnly Property currentItemIndex() As Integer
            Get
                Return _currentItemIndex
            End Get
        End Property
		
        Public ReadOnly Property NextItem() As Integer
            Get
                Return _nextItem
            End Get
        End Property

        Public ReadOnly Property PreviousItem() As Integer
            Get
                Return _previousItem
            End Get
        End Property

#End Region

#Region "Public Methods"

        Public Sub New(ByVal ModuleID As Integer)
            MyBase.New(ModuleID)

            ' Don't want to continue processing if this is an invalid path
            If Not GalleryConfig.IsValidPath Then
                Exit Sub
            End If

            _req = HttpContext.Current.Request

            ' Determine initial item to be viewed.
            If Not _req("currentitem") Is Nothing Then
                Try
                    _currentItem = CInt(_req("currentitem"))
                Catch ex As Exception
                    _currentItem = 0
                End Try

                If _currentItem > MyBase.Folder.BrowsableItems.Count - 1 Then ' 0-based
                    _currentItem = MyBase.Folder.BrowsableItems.Count - 1 ' 0-based
                ElseIf _currentItem < 0 Then
                    _currentItem = 0
                End If
            Else
                _currentItem = 0
            End If

            ' Grab the index of the item in the folder.list collection
            If MyBase.Folder.IsBrowsable Then
                _currentItemIndex = CInt(MyBase.Folder.BrowsableItems(_currentItem))
				Else 
				_currentItemIndex = 0
            End If

            ' Assign next and previous properties
            _nextItem = _currentItem + 1
            _previousItem = _currentItem - 1

            ' Validate that we haven't passed the end
            If _nextItem > MyBase.Folder.BrowsableItems.Count - 1 Then
                _nextItem = 0
            End If

            ' Validate we haven't passed the beginning
            If _previousItem < 0 Then
                _previousItem = MyBase.Folder.BrowsableItems.Count - 1
            End If
			
			' MyBase.Folder.BrowsableItems.Count
			' MyBase.Folder.Size
			
			_currentstrip = CINT(System.Math.Ceiling((_currentItemIndex +1) / GalleryConfig.ItemsPerStrip) )
			
        End Sub

#End Region

    End Class

    Public Class BaseGalleryRequest
        ' Base class for generic gallery browsing

#Region "Private Variables"

        Private ZmoduleID As Integer
        Private ZgalleryConfig As GalleryConfig

        Private _req As HttpRequest

        Private ZfolderPaths As New ArrayList()
        Private _path As String
        Private Zfolder As GalleryFolder

#End Region

#Region "Public Properties"

        Public ReadOnly Property GalleryConfig() As GalleryConfig
            Get
                Return ZgalleryConfig
            End Get
        End Property

        Public ReadOnly Property ModuleID() As Integer
            Get
                Return ZmoduleID
            End Get
        End Property

        Public ReadOnly Property Path() As String
            ' Full querystring path
            Get
                Return _path
            End Get
        End Property

        Public ReadOnly Property Folder() As GalleryFolder
            ' Instance of current gallery folder being requested
            Get
                Return Zfolder
            End Get
        End Property


#End Region

#Region "Public Functions"

        Public Sub New(ByVal ModuleID As Integer)
            'Dim some vars for this section only
            Dim paths() As String
            Dim pathCounter As Integer
            Dim newFolderDetail As FolderDetail
            Dim intermediatePaths() As String

            ZmoduleID = ModuleID
            ZgalleryConfig = GalleryConfig.GetGalleryConfig(ModuleID)

            If Not ZgalleryConfig.IsValidPath() Then
                ' Dont want to continue processing if the path is invalid.
                Exit Sub
            End If

            ' Grab the current request context
            _req = HttpContext.Current.Request

            ' Get the path
            _path = HttpUtility.UrlDecode(_req("path"))

            ' Init the root folder            
            Zfolder = ZgalleryConfig.RootFolder

            ' Create the root path information
            newFolderDetail = New FolderDetail()
            newFolderDetail.Name = ZgalleryConfig.GalleryTitle
            ZfolderPaths.Add(newFolderDetail)

            ' Logic to determine path structure
            If Not _path Is Nothing Then

                Try
                    ' Split the input into distinct paths
                    paths = Split(_path, "/")
                    intermediatePaths = New String(paths.GetUpperBound(0)) {}

                    ' Navigate the path structure
                    For pathCounter = 0 To paths.GetUpperBound(0)
                        ' Get it from memory or populate
                        Zfolder = CType(Zfolder.List.Item(paths(pathCounter)), GalleryFolder)

                        ' Gotta do it differently for the first path
                        If pathCounter = 0 Then
                            intermediatePaths(pathCounter) = paths(pathCounter)
                        Else
                            intermediatePaths(pathCounter) = intermediatePaths(pathCounter - 1) & "/" & paths(pathCounter)
                        End If

                        ' Create the folder details for this folder
                        newFolderDetail = New FolderDetail()
                        newFolderDetail.Name = Zfolder.Title
                        newFolderDetail.URL = intermediatePaths(pathCounter)
                        ZfolderPaths.Add(newFolderDetail)

                        ' Stop here because we've got to populate the folder first
                        If Not Zfolder.IsPopulated Then
                            Zfolder.Populate()
                            ' Exit For
                        End If
                    Next
                Catch ex As Exception ' an incorrect folder structure probably returned
                    ' Keep the last known good folder
                End Try
				
            End If

        End Sub

        Public Function FolderPaths() As ArrayList
            ' Returns hierarchy of folder paths to current path
	          Return ZfolderPaths

        End Function

#End Region

    End Class

End Namespace