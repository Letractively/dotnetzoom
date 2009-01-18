'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2008
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
Imports System.Data.SqlClient

Namespace DotNetZoom

    Public Class Portals

        Inherits DotNetZoom.PortalModuleControl
    	Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents grdPortals As System.Web.UI.WebControls.DataGrid

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
            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
            Title1.DisplayHelp = "DisplayHelp_Portals"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Verify that the current user has access to access this page
            If Not PortalSecurity.IsSuperUser Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If
			grdPortals.Columns(1).HeaderText = GetLanguage("P_PortalName")
			grdPortals.Columns(2).HeaderText = GetLanguage("P_Alias")
			grdPortals.Columns(3).HeaderText = GetLanguage("P_Users")
			grdPortals.Columns(4).HeaderText = GetLanguage("P_Disk_Space")
			grdPortals.Columns(5).HeaderText = GetLanguage("P_Host_Fee")
			grdPortals.Columns(6).HeaderText = GetLanguage("P_EndDate")
            BindData()

        End Sub

        Private Sub BindData()

            Dim objAdmin As New AdminDB()

            grdPortals.DataSource = objAdmin.GetPortals
            grdPortals.DataBind()

        End Sub

        Public Function FormatPortal(ByVal PortalName As Object, ByVal PortalAlias As Object) As String
            FormatPortal = "<a href=""" & GetPortalDomainName(PortalAlias.ToString) & """>" & PortalName.ToString & "</a>"
        End Function

        Public Function FormatPortalAlias(ByVal PortalID As Object) As SqlDataReader
            Dim objAdmin As New AdminDB()
            Return objAdmin.GetPortalAlias(PortalID)
        End Function



    End Class

End Namespace