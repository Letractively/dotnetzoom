'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by Ren� Boulard ( http://www.reneboulard.qc.ca)'
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

    Public MustInherit Class ModuleDefs
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents grdDefinitions As System.Web.UI.WebControls.DataGrid
		
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
        ' to populate the current defs settings from the configuration system
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Title1.DisplayHelp = "DisplayHelp_ModuleDefs"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
            ' Verify that the current user has access to this page
            If Not PortalSecurity.IsSuperUser Then
                EditDenied()
            End If

			grdDefinitions.Columns(1).HeaderText = GetLanguage("MS_ModuleName")
			grdDefinitions.Columns(2).HeaderText = GetLanguage("MS_Module_Desc")
			grdDefinitions.Columns(3).HeaderText = GetLanguage("MS_BonusTxt")

			
			
            BindData()

        End Sub

        '*******************************************************
        '
        ' The BindData helper method is used to bind the list of
        ' module definitions for this portal to an asp:datalist server control
        '
        '*******************************************************

        Sub BindData()

            ' Get the portal's defs from the database
            Dim admin As New AdminDB()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            grdDefinitions.DataSource = admin.GetModuleDefinitions(_portalSettings.PortalId, GetLanguage("N"))
            grdDefinitions.DataBind()

        End Sub

		
    End Class

End Namespace