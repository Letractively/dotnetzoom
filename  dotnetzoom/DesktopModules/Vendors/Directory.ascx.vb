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

Namespace DotNetZoom
    Public MustInherit Class ServiceDirectory
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents txtSearch As System.Web.UI.WebControls.TextBox
        Protected WithEvents lnkSearch As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lstVendors As System.Web.UI.WebControls.DataList
        Protected WithEvents cmdSignup As System.Web.UI.WebControls.LinkButton
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
        ' The Page_Load event handler on this User Control is used to
        ' obtain a DataReader of contact information from the Vendors
        ' table, and then databind the results to a DataGrid
        ' server control.  It uses the DotNetZoom.VendorsDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************'
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			
 
   			Title1.EditText = getlanguage("editer")
			lnkSearch.Text = getlanguage("go")
			cmdSignup.Text = getlanguage("banner_register")
			
			
            If Not Page.IsPostBack Then
                If CType(Settings("directorysignup"), Boolean) = False Then
                    cmdSignup.Visible = False
                End If

                If Not Request.Params("search") Is Nothing Then
                    txtSearch.Text = Request.Params("search")
                    BindData()
                End If
            End If

        End Sub

        Private Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
            BindData()
        End Sub

        Private Sub lstVendors_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles lstVendors.ItemDataBound
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim strURL As String

            strURL = "VendorClickThrough.aspx?tabid=" & TabId & "&mid=" & ModuleId
            strURL += "&VendorId=" & e.Item.DataItem("VendorId").ToString

            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    CType(e.Item.FindControl("lnkVendorName"), HyperLink).Text = e.Item.DataItem("VendorName").ToString
                    If e.Item.DataItem("Website").ToString <> "" Then
                        CType(e.Item.FindControl("lnkVendorName"), HyperLink).NavigateUrl = strURL & "&link=name"
                    End If

                    CType(e.Item.FindControl("lblAddress"), Label).Text = FormatAddress(e.Item.DataItem("Unit"), e.Item.DataItem("Street"), e.Item.DataItem("City"), e.Item.DataItem("Region"), e.Item.DataItem("Country"), e.Item.DataItem("PostalCode"))
                    If e.Item.DataItem("Telephone").ToString <> "" Then
                        CType(e.Item.FindControl("lblTelephone"), Label).Text = "<br>" & e.Item.DataItem("Telephone").ToString
                    End If
                    If e.Item.DataItem("Fax").ToString <> "" Then
                        CType(e.Item.FindControl("lblFax"), Label).Text = "<br>" & e.Item.DataItem("Fax").ToString & " (fax)"
                    End If
                    If e.Item.DataItem("Email").ToString <> "" Then
                        CType(e.Item.FindControl("lblEmail"), Label).Text = "<br>" & FormatEmail(e.Item.DataItem("Email"), page)
                    End If
                    If e.Item.DataItem("Website").ToString <> "" Then
                        CType(e.Item.FindControl("lnkWebsite"), HyperLink).Text = "<br>" & e.Item.DataItem("Website").ToString
                        CType(e.Item.FindControl("lnkWebsite"), HyperLink).NavigateUrl = strURL & "&link=url"
                    End If

                    CType(e.Item.FindControl("lnkMap"), HyperLink).NavigateUrl = strURL & "&link=map"
                    CType(e.Item.FindControl("lnkMap"), HyperLink).Visible = True
					CType(e.Item.FindControl("lnkMap"), HyperLink).Text = getlanguage("lnkmap")

                    CType(e.Item.FindControl("lnkDirections"), HyperLink).NavigateUrl = strURL & "&link=directions"
                    CType(e.Item.FindControl("lnkDirections"), HyperLink).Visible = True
			 		CType(e.Item.FindControl("lnkDirections"), HyperLink).Text = getlanguage("lnkDirections")

                    If CType(Settings("directoryfeedback"), Boolean) Then
                        CType(e.Item.FindControl("lnkFeedback"), HyperLink).NavigateUrl = strURL & "&link=feedback&search=" & txtSearch.Text
                        CType(e.Item.FindControl("lnkFeedback"), HyperLink).Text = getlanguage("lnkFeedback")
                        CType(e.Item.FindControl("lnkFeedback"), HyperLink).Visible = True
						If e.Item.DataItem("Feedback").ToString <> "" Then
                            Select Case Int32.Parse(e.Item.DataItem("Feedback").ToString)
                                Case Is > 0
                                    CType(e.Item.FindControl("lnkFeedback"), HyperLink).Text += "&nbsp;<img src=""" & IIf(Request.ApplicationPath = "/", "", Request.ApplicationPath) & "/images/ratingplus.gif"" border=""0"" alt=""" + getlanguage("lnkFeedback") + ": +" & e.Item.DataItem("Feedback").ToString & """>"
                                Case Is < 0
                                    CType(e.Item.FindControl("lnkFeedback"), HyperLink).Text += "&nbsp;<img src=""" & IIf(Request.ApplicationPath = "/", "", Request.ApplicationPath) & "/images/ratingminus.gif"" border=""0"" alt=""" + getlanguage("lnkFeedback") + ": " & e.Item.DataItem("Feedback").ToString & """>"
                                Case Else
                                    CType(e.Item.FindControl("lnkFeedback"), HyperLink).Text += "&nbsp;<img src=""" & IIf(Request.ApplicationPath = "/", "", Request.ApplicationPath) & "/images/ratingzero.gif"" border=""0"" alt=""" + getlanguage("lnkFeedback") + ": " & e.Item.DataItem("Feedback").ToString & """>"
                            End Select
                        Else
                            CType(e.Item.FindControl("lnkFeedback"), HyperLink).Text += "&nbsp;<img src=""" & IIf(Request.ApplicationPath = "/", "", Request.ApplicationPath) & "/images/ratingzero.gif"" border=""0"" alt=""" + getlanguage("noFeedback") + """>"
                        End If
                    End If

                    If e.Item.DataItem("LogoFile").ToString <> "" Then
                        If CType(Settings("directorysource"), String) = "L" Then
                            CType(e.Item.FindControl("lnkLogo"), HyperLink).ImageUrl = _portalSettings.UploadDirectory & e.Item.DataItem("LogoFile").ToString
                        Else
                            CType(e.Item.FindControl("lnkLogo"), HyperLink).ImageUrl = glbSiteDirectory & e.Item.DataItem("LogoFile").ToString
                        End If
                        CType(e.Item.FindControl("lnkLogo"), HyperLink).NavigateUrl = strURL & "&link=logo"
                    End If
            End Select
        End Sub

        Private Sub BindData()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objVendors As New VendorsDB()

            If CType(Settings("directorysource"), String) = "L" Then
                lstVendors.DataSource = objVendors.FindVendors(_portalSettings.PortalId, _portalSettings.PortalId, txtSearch.Text)
            Else
                lstVendors.DataSource = objVendors.FindVendors(_portalSettings.PortalId, , txtSearch.Text)
            End If

            lstVendors.DataBind()

        End Sub

        Private Sub cmdSignup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSignup.Click
            Response.Redirect("~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Fournisseurs", True)
        End Sub

    End Class

End Namespace