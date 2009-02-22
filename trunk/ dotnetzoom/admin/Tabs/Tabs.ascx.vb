'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Namespace DotNetZoom

    Public MustInherit Class Tabs
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lstTabs As System.Web.UI.WebControls.ListBox
        Protected WithEvents cmdUp As System.Web.UI.WebControls.ImageButton
        Protected WithEvents cmdDown As System.Web.UI.WebControls.ImageButton
        Protected WithEvents cmdLeft As System.Web.UI.WebControls.ImageButton
        Protected WithEvents cmdRight As System.Web.UI.WebControls.ImageButton
        Protected WithEvents cmdEdit As System.Web.UI.WebControls.ImageButton
        Protected WithEvents cmdView As System.Web.UI.WebControls.ImageButton
    	Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected arrPortalTabs As ArrayList

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

        '*******************************************************
        '
        ' The Page_Load server event handler on this user control is used
        ' to populate the current tab settings from the database
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Title1.DisplayHelp = "DisplayHelp_Tabs"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add1.gif"" alt=""*"" style=""border-width:0px;"">"

            arrPortalTabs = GetPortalTabs(portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N")), , True)

            ' If this is the first visit to the page, bind the tab data to the page listbox
            If Page.IsPostBack = False Then

                lstTabs.DataSource = arrPortalTabs
                lstTabs.DataBind()
				cmdUp.AlternateText = GetLanguage("haut")
       			cmdDown.AlternateText = GetLanguage("bas")
				cmdLeft.AlternateText = GetLanguage("gauche")
        		cmdRight.AlternateText = GetLanguage("droite")
        		cmdEdit.AlternateText = GetLanguage("admin_edit_tab")
        		cmdView.AlternateText = GetLanguage("ms_see")
				cmdUp.ToolTip = GetLanguage("haut")
       			cmdDown.ToolTip = GetLanguage("bas")
				cmdLeft.ToolTip = GetLanguage("gauche")
        		cmdRight.ToolTip = GetLanguage("droite")
        		cmdEdit.ToolTip = GetLanguage("modifier")
        		cmdView.ToolTip = GetLanguage("ms_see")


            End If

        End Sub

        '*******************************************************
        '
        ' The UpDown_Click server event handler on this page is
        ' used to change the tab display order in the current level 
        '
        '*******************************************************

        Private Sub UpDown_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles cmdDown.Click, cmdUp.Click

            If lstTabs.SelectedIndex <> -1 Then

                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                Dim objTab As TabItem = CType(arrPortalTabs(lstTabs.SelectedIndex), TabItem)

                Dim objAdmin As New AdminDB()

                Select Case CType(sender, ImageButton).CommandName
                    Case "up"
                        objAdmin.UpdatePortalTabOrder(portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N")), objTab.TabId, objTab.ParentId, , -1)
                    Case "down"
                        objAdmin.UpdatePortalTabOrder(portalSettings.Getportaltabs(_PortalSettings.PortalID, GetLanguage("N")), objTab.TabId, objTab.ParentId, , 1)
                End Select

                ' Redirect to this site to refresh
				ClearPortalCache(_portalSettings.PortalId)
                Response.Redirect(GetFullDocument() & "?tabid=" & TabId & "&" & GetAdminPage(), True)

            End If

        End Sub

        '*******************************************************
        '
        ' The Rightleft_Click server event handler on this page is
        ' used to move a tab up or down one hierarchical level
        '
        '*******************************************************
        Private Sub RightLeft_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles cmdLeft.Click, cmdRight.Click

            If lstTabs.SelectedIndex <> -1 Then

                ' Obtain PortalSettings from Current Context
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

                Dim objTab As TabItem = CType(arrPortalTabs(lstTabs.SelectedIndex), TabItem)

                Dim objAdmin As New AdminDB()

                Select Case CType(sender, ImageButton).CommandName
                    Case "left"
                        objAdmin.UpdatePortalTabOrder(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), objTab.TabId, objTab.ParentId, -1)
                    Case "right"
                        objAdmin.UpdatePortalTabOrder(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), objTab.TabId, objTab.ParentId, 1)
                End Select

                ' Redirect to this site to refresh
                ClearPortalCache(_portalSettings.PortalId)
                Response.Redirect(GetFullDocument() & "?tabid=" & TabId & "&" & GetAdminPage(), True)

            End If

        End Sub


        '*******************************************************
        '
        ' The EditBtn_Click server event handler is used to edit
        ' the selected tab within the portal
        '
        '*******************************************************

        Private Sub EditBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles cmdEdit.Click

            ' Redirect to edit page of currently selected tab
            If lstTabs.SelectedIndex <> -1 Then

            ' Redirect to module settings page
            Dim t As TabItem = CType(arrPortalTabs(lstTabs.SelectedIndex), TabItem)
			Dim objAdmin As New AdminDB()
            Dim dr As SqlDataReader = objAdmin.GetTabById(t.TabId, GetLanguage("N"))
			Dim Objtab As New TabStripDetails()
            Objtab.ShowFriendly = False
            Objtab.FriendlyTabName = ""
			
            While dr.Read
                    Objtab.ShowFriendly = Boolean.Parse(dr("ShowFriendly").ToString)
                    Objtab.FriendlyTabName = IIf(IsDBNull(dr("FriendlyTabName")), "", dr("FriendlyTabName"))
            End While
            dr.Close()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
	
			ClearPortalCache(_portalSettings.PortalId)
                Response.Redirect(FormatFriendlyURL(Objtab.FriendlyTabName, Objtab.ssl, Objtab.ShowFriendly, t.TabId.ToString, "def=Onglets&action=edit"), True)

            End If

        End Sub

        Private Sub ViewBtn_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles cmdView.Click

            ' Redirect to edit page of currently selected tab
            If lstTabs.SelectedIndex <> -1 Then

            ' Redirect to module settings page
            Dim t As TabItem = CType(arrPortalTabs(lstTabs.SelectedIndex), TabItem)
				
			Dim objAdmin As New AdminDB()
            Dim dr As SqlDataReader = objAdmin.GetTabById(t.TabId, GetLanguage("N"))
			Dim Objtab As New TabStripDetails()
            Objtab.ShowFriendly = False
            Objtab.FriendlyTabName = ""
			
            While dr.Read
                    Objtab.ShowFriendly = Boolean.Parse(dr("ShowFriendly").ToString)
                    Objtab.FriendlyTabName = IIf(IsDBNull(dr("FriendlyTabName")), "", dr("FriendlyTabName"))
            End While
            dr.Close()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			ClearPortalCache(_portalSettings.PortalId)	
                Response.Redirect(FormatFriendlyURL(Objtab.FriendlyTabName, Objtab.ssl, Objtab.ShowFriendly, t.TabId.ToString, ""), True)
 
            End If

        End Sub

    End Class

End Namespace