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

    Public MustInherit Class Links
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents pnlList As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lstLinks As System.Web.UI.WebControls.DataList
        Protected WithEvents pnlDropdown As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents cboLinks As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdEdit As System.Web.UI.WebControls.ImageButton
        Protected WithEvents cmdInfo As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdGo As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			

            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
			cmdEdit.tooltip = getLanguage("modifier")
			cmdEdit.alternateText = getLanguage("modifier")
			cmdGo.Text = getLanguage("go")
			cmdGo.ToolTip = getLanguage("go")
			cmdInfo.Text = getLanguage("links_info")
			cmdInfo.tooltip = getLanguage("links_info_tooltip")
			' Check to see if available in Cache
			Dim TempKey as String = GetDBname & "ModuleID_" & CStr(ModuleId)
			Dim context As HttpContext = HttpContext.Current
			Dim content as system.data.DataTable = Context.Cache(TempKey)
            If content Is Nothing Then
			'	Item not in cache, get it manually    
            Dim objLinks As New LinkDB()
			Dim objAdmin As New AdminDB()
			content = AdminDB.ConvertDataReaderToDataTable(objLinks.GetLinks(ModuleId))
            Context.Cache.Insert(TempKey, content, CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, ModuleID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
  			End If
			
				
				
                If CType(Settings("linkcontrol"), String) = "D" Then
                    pnlDropdown.Visible = True
                    If IsEditable Then
                        cmdEdit.Visible = True
                    Else
                        cmdEdit.Visible = False
                    End If
                   If CType(Settings("displayinfo"), String) = "Y" Then
                        cmdInfo.Visible = True
                    Else
                        cmdInfo.Visible = False
                    End If
                    cboLinks.DataSource = content
                    cboLinks.DataBind()
                Else
                    pnlList.Visible = True
                    If CType(Settings("linkview"), String) = "H" Then
                        lstLinks.RepeatDirection = RepeatDirection.Horizontal
                    Else
                        lstLinks.RepeatDirection = RepeatDirection.Vertical
                    End If
                    lstLinks.DataSource = content
                    lstLinks.DataBind()
                End If

        End Sub

		Function GetClass() As String
		return IIf(Settings("linkClass") <> "",  CType(Settings("linkClass"), String), "Normal")
		End Function
		
        Function FormatURL(ByVal Link As String, ByVal ID As Integer) As String

            If InStr(1, Link, "://") = 0 Then
                If IsNumeric(Link) Then ' internal tab link
                    Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                    Link = _portalSettings.HTTP & "/" & GetLanguage("N") & ".default.aspx?tabid=" & Link
                End If
            End If

            Dim objSecurity As New PortalSecurity()
            Dim crypto As String = "tabid=" & TabId & "&table=Links&field=ItemID&id=" & ID.ToString & "&link=" & Server.UrlEncode(Link)
            Return glbPath & GetLanguage("N") & ".default.aspx?linkclick=" + Server.UrlEncode(objSecurity.Encrypt(Application("cryptokey"), crypto))

            ' Return glbPath() & "admin/Portal/LinkClick.aspx?tabid=" & TabId & "&table=Links&field=ItemID&id=" & ID.ToString & "&link=" & Server.UrlEncode(Link)

        End Function

        Private Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdEdit.Click
            Response.Redirect(EditURL("ItemID", cboLinks.SelectedItem.Value), True)
        End Sub

        Private Sub cmdInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdInfo.Click

            Dim objLinks As New LinkDB()
            Dim dr As SqlDataReader = objLinks.GetSingleLink(Integer.Parse(cboLinks.SelectedItem.Value), ModuleId)
            If dr.Read Then
                lblDescription.Text = server.HtmlDecode(dr("Description").ToString)
            End If
            dr.Close()

        End Sub

        Private Sub cmdGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGo.Click

            Dim strURL As String = ""

            Dim objLinks As New LinkDB()
            Dim dr As SqlDataReader = objLinks.GetSingleLink(Integer.Parse(cboLinks.SelectedItem.Value), ModuleId)
            If dr.Read Then
                strURL = FormatURL(dr("URL").ToString, Integer.Parse(cboLinks.SelectedItem.Value))
            End If
            dr.Close()

            Response.Redirect(strURL, True)
        End Sub

    End Class

End Namespace