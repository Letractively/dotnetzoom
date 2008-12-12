'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by:
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================
Option Strict On

Imports System
Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

#Region "ForumSmilies"

    Public Class ForumSmilies
        Inherits ArrayList

        Private Const ForumSmiliesCacheKeyPrefix As String = "ForumSmilies"
        Private _galleryModuleID As Integer
        Private ZgalleryConfig As GalleryConfig
        Private _rootURL As String
        Private _smileyItems As New ArrayList()
        Private ZpagerItems As New ArrayList()
        Private _indexLookup As Hashtable = New Hashtable()

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Shadows Sub Clear()
            _indexLookup.Clear()
            MyBase.Clear()
        End Sub

        Public Shared Function GetSmileys(ByVal GalleryModuleID As Integer) As ForumSmilies
            ' Grab reference to the sessionstate object
            Dim sess As SessionState.HttpSessionState = HttpContext.Current.Session
            Dim strKey As String = GalleryModuleID.ToString

            If sess(ForumSmiliesCacheKeyPrefix & strKey) Is Nothing Then
                ' If this object has not been instantiated yet, we need to grab it
                Dim smilies As ForumSmilies = New ForumSmilies(GalleryModuleID)

                sess.Add(ForumSmiliesCacheKeyPrefix & strKey, smilies)
            End If

            Return CType(sess.Item(ForumSmiliesCacheKeyPrefix & strKey), ForumSmilies)

        End Function 'GetThreadInfo

        Public Shared Sub ResetSmilies(ByVal GalleryModuleID As Integer)

            ' Grab reference to the sessionstate object
            Dim sess As SessionState.HttpSessionState = HttpContext.Current.Session
            Dim strKey As String = GalleryModuleID.ToString

            ' and delete the settings object
            sess.Remove(ForumSmiliesCacheKeyPrefix & strKey)

        End Sub

        Public Sub New(ByVal GalleryModuleID As Integer)
            Dim request As New GalleryRequest(GalleryModuleID)
            ZgalleryConfig = request.GalleryConfig
            _rootURL = ZgalleryConfig.RootURL
            ZpagerItems = request.PagerItems

            If Not request.Folder Is Nothing Then 'BRT: added check

                Dim intCount As Integer
                For intCount = 0 To request.Folder.List.Count - 1
                    If TypeOf request.Folder.List.Item(intCount) Is GalleryFile Then
                        Dim file As GalleryFile = CType(request.Folder.List.Item(intCount), GalleryFile)

                        ' Avoid error with untitled smiley or duplicated smile title
                        If Not LCase(file.Title) = "untitled" _
                            AndAlso file.Type = IGalleryObjectInfo.ItemType.Image _
                            AndAlso _indexLookup.Item(file.Title) Is Nothing Then
                            Dim indexKey As Integer
                            indexKey = _smileyItems.Add(file)
                            indexKey += 1
                            _indexLookup.Add(file.Title, indexKey)
                        End If
                    End If
                Next

            End If

        End Sub

        Public Sub New(ByVal c As ICollection)
            MyBase.New(c)
        End Sub 'New

        Public Function FormatSmiley(ByVal Title As String) As String
            Dim index As Integer
            Dim file As GalleryFile

            ' Error handles url
            Dim smileyURL As String = BuildPath(New String(1) {_rootURL, "smiley_smile.gif"}, "/", False, True)

            ' Do validation first
            If Not LCase(Title) = "untitled" Then

                If _indexLookup.Item(Title) Is Nothing Then
                    Throw New ArgumentException("Impossible de trouver.")
                Else
                    index = CInt(_indexLookup.Item(Title))
                    file = CType(_smileyItems.Item(index), GalleryFile)
                    smileyURL = String.Format("<img src=""{0}"" align=absmiddle border=0 \>", file.URL)
                End If
            End If

            Return smileyURL

        End Function

        Public Shadows Function Item(ByVal index As Integer) As Object
            Try
                Dim obj As Object
                obj = _smileyItems.Item(index)
                Return obj
            Catch Exc As System.Exception
            End Try
        End Function

        Public Shadows Function Item(ByVal key As String) As Object
            Dim index As Integer
            Dim obj As Object

            ' Do validation first
            If _indexLookup.Item(key) Is Nothing Then
                Throw New ArgumentException("Impossible de trouver.")
            End If

            index = CInt(_indexLookup.Item(key))
            obj = _smileyItems.Item(index)

            Return obj
        End Function

        Public ReadOnly Property GalleryConfig() As GalleryConfig
            Get
                Return ZgalleryConfig
            End Get
        End Property

        Public ReadOnly Property rootURL() As String
            Get
                Return _rootURL
            End Get
        End Property
        Public ReadOnly Property smileyItems() As ArrayList
            Get
                Return _smileyItems
            End Get
        End Property

        Public ReadOnly Property pagerItems() As ArrayList
            Get
                Return ZpagerItems
            End Get
        End Property

    End Class 'ForumSmilies

#End Region


End Namespace