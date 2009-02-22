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
Imports System.IO
Imports System.Xml

Namespace DotNetZoom

    Public MustInherit Class SiteLog
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents cboReportType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtEndDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdDisplay As System.Web.UI.WebControls.LinkButton
        Protected WithEvents grdLog As System.Web.UI.WebControls.DataGrid
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label

        Protected WithEvents cmdStartCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEndCalendar As System.Web.UI.WebControls.HyperLink
        Protected WithEvents valStartDate As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents valEndDate As System.Web.UI.WebControls.CompareValidator
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

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
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If Not PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                EditDenied()
            End If

            Title1.DisplayHelp = "DisplayHelp_SiteLog"
            ' If this is the first visit to the page, bind the role data to the datalist
            If Page.IsPostBack = False Then

                cmdStartCalendar.Text = GetLanguage("Stat_Calendar")
                cmdEndCalendar.Text = GetLanguage("Stat_Calendar")
                cmdDisplay.Text = GetLanguage("Stat_Display")
                cmdStartCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtStartDate)
                cmdEndCalendar.NavigateUrl = AdminDB.InvokePopupCal(txtEndDate)

                Select Case _portalSettings.SiteLogHistory
                    Case -1 ' unlimited
                    Case 0
                        lblMessage.Text = GetLanguage("SiteLogOff")
                    Case Else
                        lblMessage.Text = GetLanguage("SiteLogLimited")
                        lblMessage.Text = Replace(GetLanguage("SiteLogLimited"), "{days}", _portalSettings.SiteLogHistory.ToString)
                End Select

                Dim objAdmin As New AdminDB()

                cboReportType.DataSource = objAdmin.GetSiteLogReports(GetLanguage("N"))
                cboReportType.DataBind()
                cboReportType.SelectedIndex = 0

                txtStartDate.Text = formatansidate(DateAdd(DateInterval.Day, -6, Date.Today).ToString("yyyy-MM-dd"))
                txtEndDate.Text = formatansidate(DateAdd(DateInterval.Day, 1, Date.Today).ToString("yyyy-MM-dd"))

            End If
        End Sub

        Private Sub cmdDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDisplay.Click
            BindData()
        End Sub

        Private Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim strPortalAlias As String

            strPortalAlias = GetPortalDomainName(PortalAlias, Request, False)
            If InStr(1, strPortalAlias, "/") <> 0 Then ' child portal
                strPortalAlias = Left(strPortalAlias, InStrRev(strPortalAlias, "/") - 1)
            End If

            Dim strStartDate As String = txtStartDate.Text
            If strStartDate <> "" Then
                strStartDate = CheckDateSql(strStartDate) & " 00:00"
            End If
            Dim strEndDate As String = txtEndDate.Text
            If strEndDate <> "" Then
                strEndDate = CheckDateSql(strEndDate) & " 23:59"
            End If
            Dim objAdmin As New AdminDB()
            grdLog.DataSource = objAdmin.GetSiteLog(_portalSettings.PortalId, strPortalAlias, Int32.Parse(cboReportType.SelectedItem.Value), strStartDate, strEndDate)
            grdLog.DataBind()
        End Sub

    End Class

End Namespace