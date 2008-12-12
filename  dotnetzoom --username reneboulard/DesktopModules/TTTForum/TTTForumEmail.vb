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

Imports System.Web
Imports System.Web.Mail
Imports System.Text
Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

#Region "ForumEmail"

    Public Class ForumEmail
        Inherits MailMessage

        Private _PostID As Integer
        Private _Post As ForumPostInfo
        Private _ParentPost As ForumPostInfo
        Private _Thread As ForumThreadInfo
        Private _Forum As ForumItemInfo
        Private _Group As ForumGroupInfo
        Private Zconfig As ForumConfig 
        Private Zuser As ForumUser
        Private _ParentUser As ForumUser
        Private _Action As String
        Private _EmailType As ForumEmailType = ForumEmailType.PostNormal

        Public Enum ForumEmailType
            PostNormal
            PostModerate
            PostApprove
            PostReject
            PostDelete
        End Enum

        Public Sub New()
            MyBase.New()

        End Sub 'New

        Private Sub GenerateSender()
            Dim senderAddress As String = ""
            If _EmailType = ForumEmailType.PostNormal Then
			    senderAddress  = Zconfig.AutomatedEmailAddress
			    If Zconfig.ShowSenderAddressOnEmail and Zuser.Email <> "" Then
                    senderAddress = Zuser.Email
                ElseIf Zconfig.EmailDomain <> "" Then
				    Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                    senderAddress = _portalSettings.ActiveTab.TabName & "." & _Group.Name & "@" & Zconfig.EmailDomain
                End If
				else
				SenderAddress = ForumConfig.DefaultEMailAddress
            End If
			If senderAddress = "" then
			   SenderAddress = ForumConfig.DefaultEMailAddress
			end if
            From = senderAddress
        End Sub

        Private Sub GenerateSubject()
            Dim mailSubject As New StringBuilder()

            Select Case _EmailType
                    Case ForumEmailType.PostNormal
                        mailSubject.Append( GetLanguage("Forum_NewMessage") & " ")
					Case ForumEmailType.PostModerate
						mailSubject.Append( GetLanguage("Forum_Moderated_message") & " ")
            		Case ForumEmailType.PostApprove
						mailSubject.Append( GetLanguage("Forum_Moderated_approved") & " ")
            		Case ForumEmailType.PostReject
						mailSubject.Append(GetLanguage("Forum_Moderated_refused") & " ")
            		Case ForumEmailType.PostDelete
						mailSubject.Append(GetLanguage("Forum_Moderated_erased") & " ")
                End Select
			

            mailSubject.Append("[")
            mailSubject.Append(_Group.Name & "][")
            mailSubject.Append(_Forum.Name & "][")
            mailSubject.Append(_Thread.Subject & "]")
            mailSubject.Append("'")
            mailSubject.Append(_Post.Subject)
            mailSubject.Append("'")

            Subject = mailSubject.ToString
        End Sub

        Private Sub GenerateRecipients()
            Dim recipients As String = ""

            If _EmailType = ForumEmailType.PostNormal Then
                ' normal:
                recipients = GetDistList()
                If (_ParentPost.Notify And _ParentPost.User.UserID <> _Post.User.UserID) Then
                    If Not InStr(recipients, _ParentUser.Email) > 0 Then
                        recipients += _ParentUser.Email
                    End If
                End If

            ElseIf _EmailType = ForumEmailType.PostModerate Then
                ' moderators: notify about submitted post
                recipients = GetModeratorList()

            Else
                ' author: post's approved, deleted or rejected
                recipients = Zuser.Email

            End If

            If Right(recipients, 1) = ";" Then
                recipients = Left(recipients, (Len(recipients) - 1))
            End If

            Me.Bcc = recipients

        End Sub

        Private Sub GenerateBody()
			Dim context As HttpContext = HttpContext.Current
            Dim bodyForumText As New ForumText(HttpContext.Current.Server.HtmlDecode(_Post.Body), Zconfig)
            Dim sb As New StringBuilder()
            Dim fullAvatarURL As String = "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") 

            If _EmailType = ForumEmailType.PostNormal Then
			
			
                Select Case _Action
                    Case "new"
                        sb.AppendFormat(GetLanguage("Forum_NewMessageTXT"), Zuser.Alias, _Forum.Name)
                    Case "edit"
                        sb.AppendFormat(GetLanguage("Forum_ModMessageTXT"), _Post.Subject, Zuser.Alias, _Forum.Name)
                    Case Else
                        sb.AppendFormat(GetLanguage("Forum_ReplayMessageTXT") , _ParentPost.Subject, Zuser.Alias, _Forum.Name)
                End Select
                sb.AppendFormat(GetLanguage("Forum_BodyMessageTXT"), _Post.Subject, bodyForumText.Process(fullAvatarURL), UrlContentBase )

            ElseIf _EmailType = ForumEmailType.PostModerate Then

                sb.AppendFormat(GetLanguage("Forum_Moderated_messageTXT") , Zuser.Alias, _Forum.Name, UrlContentBase)

            ElseIf _EmailType = ForumEmailType.PostApprove Then
			    ' ViewerId DateTime
                sb.AppendFormat( GetLanguage("Forum_Moderated_approvedTXT"), _Post.PostDate.ToString, _Post.Subject, _Forum.Name, ProcessLanguage("{date}"), _Forum.Name.ToString)
            Else
				' Refuse or Delete
                sb.AppendFormat(GetLanguage("Forum_Moderated_refusedTXT") , _Post.PostDate.ToString, _Post.Subject, _Forum.Name, ProcessLanguage("{date}"), _Forum.Name.ToString)

            End If

            Dim fText As New ForumText(sb.ToString, Zconfig)
            If Not Zconfig.HTMLMailFormat Then
                fText.FormatPlainText()
            End If

            Body = fText.Text
        End Sub

        Public Sub Generate()

            
            _Post = ForumPostInfo.GetPostInfo(_PostID)
            _Thread = ForumThreadInfo.GetThreadInfo(_Post.ThreadID)
            _Forum = ForumItemInfo.GetForumInfo(_Thread.ForumID)
            _Group = ForumGroupInfo.GetGroupInfo(_Forum.ForumGroupID)
            Zconfig = ForumConfig.GetForumConfig(_Group.ModuleID)
            Zuser = _Post.User

            If _Post.ParentPostID > 0 Then
                _ParentPost = ForumPostInfo.GetPostInfo(_Post.ParentPostID)
                If _Post.LastModifiedDate = #1/1/2000# Then 'edit post
                    _Action = "reply"
                Else
                    _Action = "edit"
                End If

            Else
                _ParentPost = _Post
                _Action = "new"
            End If
            _ParentUser = _ParentPost.User

            GenerateSender()
            GenerateRecipients()
            GenerateSubject()
            GenerateBody()

            If Zconfig.HTMLMailFormat Then
                BodyFormat = MailFormat.Html
            End If

        End Sub

        Private Function GetDistList() As String
            Dim dbForum As New ForumDB()
            Dim distList As New StringBuilder()

            Dim dr As SqlDataReader = dbForum.TTTForum_TrackingThreadEmails(_Post.PostID)
            While dr.Read
                distList.Append(ConvertString(dr("Email")) & ";")
            End While

            dr.Close()
            Return distList.ToString

        End Function

        Private Function GetModeratorList() As String
            Dim dbForum As New ForumDB()
            Dim modList As New StringBuilder()
            Dim moderatorCollection As ForumModeratorCollection
            Dim moderator As ForumModerator

            moderatorCollection = New ForumModeratorCollection(_Forum.ForumID)
            For Each moderator In moderatorCollection
                If moderator.EmailNotification Then
                    modList.Append(moderator.Email & ";")
                End If
            Next

            Return modList.ToString

        End Function

        Public Property PostID() As Integer
            Get
                Return _PostID
            End Get
            Set(ByVal Value As Integer)
                _PostID = Value
            End Set
        End Property

        Public Property Action() As String
            Get
                Return _Action
            End Get
            Set(ByVal Value As String)
                _Action = Value
            End Set
        End Property

        Public Property EmailType() As ForumEmailType
            Get
                Return _EmailType
            End Get
            Set(ByVal Value As ForumEmailType)
                _EmailType = Value
            End Set
        End Property


    End Class 'ForumEmail

#End Region

End Namespace