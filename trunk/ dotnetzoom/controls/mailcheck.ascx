<%@ Control Language="VB"%>
<%@ Import Namespace="DotNetZoom" %>


<script runat="server">
         Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
 			If Request.IsAuthenticated then 
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			If portalSettings.GetSiteSettings(_portalSettings.PortalID)("PortalUserOnline") <> "NO" then
			If portalSettings.GetSiteSettings(_portalSettings.PortalID)("SearchUser") <> "NO" or portalSettings.GetSiteSettings(_portalSettings.PortalID)("UserOnline") <> "NO" or portalSettings.GetSiteSettings(_portalSettings.PortalID)("UserMessage") <> "NO" then
            Dim TempString as String = ""
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
			lblMailCheck.visible = true
            
			If portalSettings.GetSiteSettings(_portalSettings.PortalID)("SearchUser") <> "NO" then
			TempString = "<a href="""
            TempString +=  AddHTTP(GetDomainName(Request))
            TempString +=  getdocument() & "?edit=control&amp;tabid="
            TempString +=  _portalSettings.ActiveTab.TabId.ToString
			TempString +=  "&amp;def=Liste%20membres"" title="""" onmouseover=""" & ReturnToolTip(GetLanguage("banner_ClickProfile")) & """>" 
                        TempString += "<img height=""14"" width=""17"" border=""0"" src=""images/1x1.gif"" Alt=""*"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -91px;"">"
			TempString += "</a>&nbsp;"
			end if
			
			If portalSettings.GetSiteSettings(_portalSettings.PortalID)("UserOnline") <> "NO" then
 			Dim myReader As System.Data.SqlClient.SqlDataReader = dbUserOnline.TTTForum_UserOnline2_Get(_portalSettings.PortalId)
            UserString = GetLanguage("UO_online") 
			While MyReader.Read
			 UserString +=  "<br>" & MyReader("Alias").ToString & "&nbsp;(" & MyReader("Location").ToString  & ")"
			End While
            myReader.Close()
                        TempString += "<img src=""images/1x1.gif"" title="""" style=""background: url('/images/uostrip.gif') no-repeat; background-position: 0px -105px;CURSOR: help"" onmouseover=""this.T_STICKY=true;this.T_WIDTH=100;" & "return escape('" & RTESafe(UserString) & "')" & """ border=""0"" width=""17"" height=""14"" alt=""Liste"">"
            TempString += "&nbsp;"
			End if
			
			If portalSettings.GetSiteSettings(_portalSettings.PortalID)("UserMessage") <> "NO" then
			UserString = ""
                        TempString += "<a href=""" & GetMessageUrl() & """><img src=""images/"
			objMessagesDB.GetMessageCount((New Utility).GetUserID(), unreadCount, readCount)
			If unreadCount > 0 then
			UserString = " " & GetLanguage("Banner_Mail") & " " 
			UserString = UserString & unreadCount.ToString
			If unreadCount > 1 then
			UserString = UserString & GetLanguage("Banner_Mail1")  
			else
			UserString = UserString & GetLanguage("Banner_Mail0")  			
			end if
			UserString = UserString & " " & GetLanguage("Banner_Mail2") 
                            TempString += "1x1.gif"" border=""0"" height=""10"" width=""17"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -309px;"" alt=""*"" Title="""" onmouseover=""" & ReturnToolTip(UserString)

			else
			If ReadCount > 0 then
			UserString = GetLanguage("Banner_Mail") & " " 
			UserString = UserString & readCount.ToString
			If readCount > 1 then
			UserString = UserString & " " & GetLanguage("Banner_Mail3") 
			else
			UserString = UserString & " " & GetLanguage("Banner_Mail4")	
			end if
			UserString = UserString & " " & GetLanguage("Banner_Mail2") 
                                TempString += "1x1.gif"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -159px;"" border=""0"" height=""12"" width=""18"" alt=""*"" Title="""" onmouseover=""" & ReturnToolTip(UserString)
            else
			UserString = GetLanguage("Banner_Mail5") 
                                TempString += "1x1.gif"" border=""0"" height=""12"" width=""18"" Alt=""*"" style="" background: url('/images/uostrip.gif') no-repeat; background-position: 0px -147px;"" Title="""" onmouseover=""" & ReturnToolTip(UserString)
           	End If
			End If
			TempString = TempString  & """></a>"
			end if
			lblMailCheck.text = TempString & "&nbsp;"
			end if
			end if
			end if
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
            sb.Append("~" & getdocument() & "?edit=control&tabid=")
            sb.Append(_portalSettings.ActiveTab.TabId.ToString)
            sb.Append("&pmstabid=3")
            sb.Append("&userid=")
            sb.Append(UserID.ToString)
			Return sb.ToString
		End Function
		
        Protected Function GetMessageUrl() As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Return AddHTTP(GetDomainName(Request)) & getdocument() & "?edit=control&amp;tabid=" & _portalSettings.ActiveTab.TabId & "&amp;def=PrivateMessages"
        End Function
</script>
<asp:literal id="lblMailCheck" visible="false" runat="server" EnableViewState="false" />