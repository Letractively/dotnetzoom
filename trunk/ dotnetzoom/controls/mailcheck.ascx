<%@ Control Language="VB"%>
<%@ Import Namespace="DotNetZoom" %>


<script runat="server">
         Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Request.IsAuthenticated Then
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("PortalUserOnline") <> "NO" Then
                If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("SearchUser") <> "NO" Or PortalSettings.GetSiteSettings(_portalSettings.PortalId)("UserOnline") <> "NO" Or PortalSettings.GetSiteSettings(_portalSettings.PortalId)("UserMessage") <> "NO" Then
                    Dim TempString As String = ""
                    Dim UserString As String = ""
                    Dim objMessagesDB As MessagesDB = New MessagesDB
                    Dim unreadCount As Integer = 0
                    Dim readCount As Integer = 0
                    Dim objUsersOnline As UsersOnlineDB = New UsersOnlineDB
                    Dim dbUserOnline As New ForumUserOnlineDB()
                    Dim GuestCount As Integer = 0
                    Dim MemberCount As Integer = 0
                    Dim UserCount As Integer = 0
                    Dim NewToday As Integer = 0
                    Dim NewYesterday As Integer = 0
                    Dim LatestUsername As String = ""
                    Dim LatestUserID As Integer = 0
                    lblMailCheck.Visible = True
            
                    If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("SearchUser") <> "NO" Then
                        
                        TempString = "<a href="""
                        TempString += GetFullDocument() & "?tabid="
                        TempString += _portalSettings.ActiveTab.TabId.ToString
                        TempString += "&amp;def=Liste%20membres"" title="""" onmouseover=""" & ReturnToolTip(GetLanguage("banner_ClickProfile")) & """>"
                        TempString += "<img height=""14"" width=""17"" border=""0"" src=""/images/1x1.gif"" Alt=""*"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -91px;"">"
                        TempString += "</a>&nbsp;"
                    End If
			
                    If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("UserOnline") <> "NO" Then
                        Dim myReader As System.Data.SqlClient.SqlDataReader = dbUserOnline.TTTForum_UserOnline2_Get(_portalSettings.PortalId)
                        UserString = GetLanguage("UO_online")
                        While myReader.Read
                            UserString += "<br>" & myReader("Alias").ToString & "&nbsp;(" & myReader("Location").ToString & ")"
                        End While
                        myReader.Close()
                        TempString += "<img src=""/images/1x1.gif"" title="""" style=""background: url('/images/uostrip.gif') no-repeat; background-position: 0px -105px;CURSOR: help"" onmouseover=""this.T_STICKY=true;this.T_WIDTH=100;" & "return escape('" & RTESafe(UserString) & "')" & """ border=""0"" width=""17"" height=""14"" alt=""Liste"">"
                        TempString += "&nbsp;"
                    End If
			
                    If PortalSettings.GetSiteSettings(_portalSettings.PortalId)("UserMessage") <> "NO" Then
                        UserString = ""
                        TempString += "<a href=""" & GetMessageUrl() & """><img src=""/images/"
                        objMessagesDB.GetMessageCount((New Utility).GetUserID(), unreadCount, readCount)
                        If unreadCount > 0 Then
                            UserString = " " & GetLanguage("Banner_Mail") & " "
                            UserString = UserString & unreadCount.ToString & " "
                            If unreadCount > 1 Then
                                UserString = UserString & GetLanguage("Banner_Mail1")
                            Else
                                UserString = UserString & GetLanguage("Banner_Mail0")
                            End If
                            UserString = UserString & " " & GetLanguage("Banner_Mail2")
                            TempString += "1x1.gif"" border=""0"" height=""10"" width=""17"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -309px;"" alt=""*"" Title="""" onmouseover=""" & ReturnToolTip(UserString)
                        Else
                            If readCount > 0 Then
                                UserString = GetLanguage("Banner_Mail") & " "
                                UserString = UserString & readCount.ToString
                                If readCount > 1 Then
                                    UserString = UserString & " " & GetLanguage("Banner_Mail3")
                                Else
                                    UserString = UserString & " " & GetLanguage("Banner_Mail4")
                                End If
                                UserString = UserString & " " & GetLanguage("Banner_Mail2")
                                TempString += "1x1.gif"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -159px;"" border=""0"" height=""12"" width=""18"" alt=""*"" Title="""" onmouseover=""" & ReturnToolTip(UserString)
                            Else
                                UserString = GetLanguage("Banner_Mail5")
                                TempString += "1x1.gif"" border=""0"" height=""12"" width=""18"" Alt=""*"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -147px;"" Title="""" onmouseover=""" & ReturnToolTip(UserString)
                            End If
                        End If
                        TempString = TempString & """></a>"
                    End If
                    lblMailCheck.Text = TempString & "&nbsp;"
                End If
            End If
        End If
       End Sub

        Public Shared Function ConvertInteger(ByVal value As Object) As Integer
            If value Is DBNull.Value Then
                Return 0
            Else
                Return Convert.ToInt16(value)
            End If
        End Function


		
		Protected Function GetPMSUrl(ByVal UserID As Integer) As String
		     Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

	        Dim sb As New StringBuilder()
            sb.Append(GetFullDocument() & "?edit=control&tabid=")
            sb.Append(_portalSettings.ActiveTab.TabId.ToString)
            sb.Append("&pmstabid=3")
            sb.Append("&userid=")
            sb.Append(UserID.ToString)
			Return sb.ToString
		End Function
		
        Protected Function GetMessageUrl() As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Return GetFullDocument() & "?tabid=" & _portalSettings.ActiveTab.TabId & "&amp;def=PrivateMessages"
        End Function
</script>
<asp:literal id="lblMailCheck" visible="false" runat="server" EnableViewState="false" />