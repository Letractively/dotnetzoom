'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' =======================================================================================
Imports System.IO

Imports System
Imports System.Web
Imports System.Web.UI
Imports Microsoft.VisualBasic


Namespace DotNetZoom

    Public Class TTT_ForumSearch
        Inherits PortalModuleControl

        Protected WithEvents cmdBack As System.Web.UI.WebControls.Button
		Protected WithEvents cmdSearch As System.Web.UI.WebControls.Button
        Protected WithEvents txtSearch As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtObject As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAlias As System.Web.UI.WebControls.TextBox
		Protected WithEvents lstGroup As System.Web.UI.WebControls.DataList
        Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtEndDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdStartCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEndCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents valStartDate As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents valEndDate As System.Web.UI.WebControls.CompareValidator


		
       
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

 			cmdBack.Text = GetLanguage("return")
			cmdBack.Tooltip = GetLanguage("return")
			cmdSearch.Text = GetLanguage("cmdSearch")
			cmdSearch.Tooltip = GetLanguage("cmdSearch")
			Dim ImageFolder As String = ForumConfig.SkinImageFolder()

            If Not Page.IsPostBack Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If

                cmdStartCalendar.Text = GetLanguage("Stat_Calendar")
                cmdEndCalendar.Text = GetLanguage("Stat_Calendar")
                cmdStartCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtStartDate)
                cmdEndCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtEndDate)
                txtStartDate.Text = formatansidate(DateAdd(DateInterval.Day, -6, Date.Today).ToString("yyyy-MM-dd"))
                txtEndDate.Text = formatansidate(DateAdd(DateInterval.Day, 1, Date.Today).ToString("yyyy-MM-dd"))

                Dim dbForum As New ForumDB
                lstGroup.DataSource = dbForum.TTTForum_GetGroups(_portalSettings.PortalId, ModuleId)
                lstGroup.DataBind()



            End If
			
			
		
        End Sub

        Function GetForums(ByVal groupID As Integer) As DataView

	    Dim dt As New DataTable()
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("ForumID", GetType(String)))
        dt.Columns.Add(New DataColumn("Description", GetType(String)))
		dt.Columns.Add(New DataColumn("Name", GetType(String)))

		
        Dim dbForum As New ForumDB()
	    Dim Result As SqlDataReader = dbForum.TTTForum_GetForums(groupID)

        While result.Read()

		dr = dt.NewRow()
		' Check security to make sure

		If result("IsActive") Then
        	If Not result("IsPrivate") Then
			' Accept
	       	dr(0) =  result("ForumID")
			dr(1) =  result("Description")
			dr(2) =  result("Name")
			dt.Rows.Add(dr)
			else
            	If PortalSecurity.IsInRoles(result("AuthorizedRoles")) = True Then
                ' accept 
		       	dr(0) =  result("ForumID")
				dr(1) =  result("Description")
				dr(2) =  result("Name")
				dt.Rows.Add(dr)
                End If
            End If
		end if

        End While  
		result.Close()		

        Dim dv As New DataView(dt)

            Return dv
        End Function		
		

        Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
   			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim strSearch As String = txtSearch.Text
			Dim strObject As String = txtObject.Text
			Dim strAlias As String = txtAlias.Text
			Dim redirectURL As String = String.Empty
			' Redirect user to search page
            If (strSearch.Length > 0) Then
                strSearch = strSearch.Replace("&", ":amp:")
				strSearch = strSearch.Replace("'", "''")
			end if
			
            If (strObject.Length > 0) Then
                strObject = strObject.Replace("&", ":amp:")
				strObject = strObject.Replace("'", "''")
			end if
			
			
            If (strAlias.Length > 0) Then
                strAlias = strAlias.Replace("&", ":amp:")
			end if
			
			Dim strStartDate As String = txtStartDate.Text
            If strStartDate <> "" Then
                strStartDate = CheckDateSql(strStartDate) & " 00:00"
			else
			strStartDate = "19000101 00:00"
            End If
            Dim strEndDate As String = txtEndDate.Text
            If strEndDate <> "" Then
                strEndDate = CheckDateSql(strEndDate) & " 23:59"
			else
			strEndDate = "99991231 23:59"
            End If

            Dim ItemCollection As DataListItemCollection
            Dim CurrentItem As DataListItem

            ItemCollection = lstGroup.Items
			Dim Message As String = ""
            For Each CurrentItem In ItemCollection
                If CurrentItem.ItemType = ListItemType.AlternatingItem Or CurrentItem.ItemType = ListItemType.Item Then
            		Dim ForumCollection As DataListItemCollection
            		Dim ForumItem As DataListItem
					Dim ForumList As WebControls.DataList = CurrentItem.Controls(3)
		            ForumCollection = ForumList.Items

					For Each ForumItem In ForumCollection
	                If ForumItem.ItemType = ListItemType.AlternatingItem Or ForumItem.ItemType = ListItemType.Item Then
                    Dim chkSearch As HtmlInputCheckBox
                    chkSearch = ForumItem.Controls(1)
					' chkSearch = DirectCast(CurrentItem.FindControl("chkSearch"), HtmlInputCheckBox)
					If Not (chkSearch is nothing) then 
                    If chkSearch.Checked Then
                    Message += chkSearch.Value + ":"
                    End If
					End if
					End if
					Next

				End If
            Next
			
			message = message.trim(":"c)
	
            redirectURL = TTTUtils.GetURL(GetFullDocument(), Page, String.Format("tabid={0}&action=search&forumsid=" + Message + "&startdate=" + strStartDate + "&enddate=" + strEndDate + "&searchobject=" + strObject + "&searchterms=" + strSearch + "&useralias=" & strAlias, _portalSettings.ActiveTab.TabId.ToString), "forumpage=&scope=&threadid=&postid=&threadpage=&threadspage=&searchpage=")

            Page.Response.Redirect(redirectURL)

        End Sub


		
		Sub Check_Clicked(sender As Object, e As EventArgs) 

            Dim ItemCollection As DataListItemCollection
            Dim CurrentItem As DataListItem

            ItemCollection = lstGroup.Items
			Dim Message As String = ""
            For Each CurrentItem In ItemCollection
                If CurrentItem.ItemType = ListItemType.AlternatingItem Or CurrentItem.ItemType = ListItemType.Item Then
            		Dim ForumCollection As DataListItemCollection
            		Dim ForumItem As DataListItem
					Dim chkForum As WebControls.CheckBox = CurrentItem.Controls(1)
					Dim ForumList As WebControls.DataList = CurrentItem.Controls(3)
		            ForumCollection = ForumList.Items

					For Each ForumItem In ForumCollection
	                If ForumItem.ItemType = ListItemType.AlternatingItem Or ForumItem.ItemType = ListItemType.Item Then
                    Dim chkSearch As HtmlInputCheckBox
                    chkSearch = ForumItem.Controls(1)
					If Not (chkSearch is nothing) then 
					chkSearch.Checked = chkForum.Checked
                    If chkSearch.Checked Then
                     Message += chkSearch.Value + ":"
                    End If
					End if
					End if
					Next

				End If
            Next
			
			
			
           ' Dim objStream As StreamWriter
           ' objStream = File.CreateText(Server.MapPath("\ItemClick.log"))
           ' objStream.WriteLine(message)
           ' objStream.Close()			
		
      End Sub
		
		
        Protected Sub lstGroup_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles lstGroup.ItemCommand
            Dim ItemCollection As DataListItemCollection
            Dim CurrentItem As DataListItem

            ItemCollection = lstGroup.Items
			Dim Message As String = ""
            For Each CurrentItem In ItemCollection
                If CurrentItem.ItemType = ListItemType.AlternatingItem Or CurrentItem.ItemType = ListItemType.Item Then
            		Dim ForumCollection As DataListItemCollection
            		Dim ForumItem As DataListItem
					Dim ForumList As WebControls.DataList = CurrentItem.Controls(1)
		            ForumCollection = ForumList.Items

					For Each ForumItem In ForumCollection
	                If ForumItem.ItemType = ListItemType.AlternatingItem Or ForumItem.ItemType = ListItemType.Item Then
                    Dim chkSearch As HtmlInputCheckBox
                    chkSearch = ForumItem.Controls(1)
					If Not (chkSearch is nothing) then 
                    If chkSearch.Checked Then
                     Message += chkSearch.Value + ":"
                    End If
					End if
					End if
					Next

				End If
            Next
			
			

        End Sub	


	
        Private Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String))
        End Sub
    End Class

End Namespace