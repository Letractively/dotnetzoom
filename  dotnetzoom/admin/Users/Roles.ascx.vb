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

    Public MustInherit Class Roles
        Inherits DotNetZoom.PortalModuleControl
    	Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected WithEvents grdRoles As System.Web.UI.WebControls.DataGrid

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
        ' to populate the current roles settings from the configuration system
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If Not PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                EditDenied()
            End If

            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
            BindData()

        End Sub

        '*******************************************************
        '
        ' The BindData helper method is used to bind the list of
        ' security roles for this portal to an asp:datalist server control
        '
        '*******************************************************'

        Sub BindData()
			Title1.DisplayHelp = "DisplayHelp_Roles"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)

			grdRoles.Columns(2).HeaderText = GetLanguage("Role_Name")
            grdRoles.Columns(3).HeaderText = GetLanguage("Role_Description")
            grdRoles.Columns(4).HeaderText = GetLanguage("ServicesFee")
            grdRoles.Columns(5).HeaderText = GetLanguage("Role_Frequency")
            grdRoles.Columns(6).HeaderText = GetLanguage("Role_FrequencyP")
            grdRoles.Columns(7).HeaderText = GetLanguage("Role_Trial")
            grdRoles.Columns(8).HeaderText = GetLanguage("Role_Frequency")
            grdRoles.Columns(9).HeaderText = GetLanguage("Role_FrequencyP")
            grdRoles.Columns(10).HeaderText = GetLanguage("Role_Header_Public")
			grdRoles.Columns(11).HeaderText = GetLanguage("Role_Header_Auto")
			
			
		
            ' Get the portal's roles from the database
            Dim objUser As New UsersDB()
            grdRoles.DataSource = objUser.GetPortalRoles(_portalSettings.PortalId , GetLanguage("N"))
            grdRoles.DataBind()

        End Sub

        Public Function FormatURL(ByVal RoleID As String) As String
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            FormatURL = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "RoleId=" & RoleID & "&def=User Roles")
            ' FormatURL(DataBinder.Eval(Container.DataItem,"RoleID"))
        End Function

		
		
    End Class

End Namespace