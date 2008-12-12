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

    Public MustInherit Class VendorFeedback
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents lblVendor As System.Web.UI.WebControls.Label
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label

        Protected WithEvents cmdFeedback As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlFeedback As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents cboValue As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtComment As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton

        Protected WithEvents lstFeedback As System.Web.UI.WebControls.DataList
        Protected WithEvents cmdBack As System.Web.UI.WebControls.LinkButton
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Private VendorId As Integer = 0

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
        ' obtain a DataReader of banner information from the Banners
        ' table, and then databind the results to a templated DataList
        ' server control.  It uses the DotNetZoom.BannerDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************'

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If IsNumeric(Request.Params("VendotId")) Then
                VendorId = Int32.Parse(Request.Params("VendorId"))
            End If
			Title1.DisplayTitle = getlanguage("title_vendor_feedback")

			cboValue.Items.FindByValue("1").Text = getlanguage("feedback_pos")
			cboValue.Items.FindByValue("-1").Text = getlanguage("feedback_neg")
			cmdUpdate.Text = getLanguage("enregistrer")
			cmdCancel.Text = getlanguage("annuler")
			cmdDelete.Text = getlanguage("delete")
			cmdFeedback.Text = getlanguage("lnkFeedback")
			cmdBack.Text = getlanguage("return")
            If Not Page.IsPostBack Then

                If Not Request.IsAuthenticated Then
                    lblMessage.Text = processLanguage(getlanguage("vendors_message_feedback1") + getlanguage("vendors_message_feedback2"), page)
                    cmdFeedback.Visible = False
                Else
                    lblMessage.Text = ""
                    cmdFeedback.Visible = True
					Title1.DisplayHelp = "DisplayHelp_VendorFeedBack"
                End If

                Dim objVendor As New VendorsDB()

                Dim dr As SqlDataReader = objVendor.GetSingleVendor(VendorId)
                If dr.Read Then
                    lblVendor.Text = dr("VendorName").ToString
                End If
                dr.Close()

                lstFeedback.DataSource = objVendor.GetVendorFeedback(VendorId)
                lstFeedback.DataBind()

                ' Store URL Referrer to return to portal
                Dim strReferrer As String = Request.UrlReferrer.ToString()
                If InStr(1, strReferrer, "&search=") Then
                    strReferrer = Left(strReferrer, InStr(1, strReferrer, "&search=") - 1)
                End If
                ViewState("UrlReferrer") = strReferrer & "&search=" & Request.QueryString("search")

            End If

        End Sub

        Private Sub cmdBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdBack.Click
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Private Sub cmdFeedback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFeedback.Click

            Dim objVendor As New VendorsDB()

            Dim dr As SqlDataReader = objVendor.GetSingleVendorFeedback(VendorId, Int32.Parse(Context.User.Identity.Name))
            If dr.Read Then
                cboValue.Items.FindByValue(dr("Value")).Selected = True
                txtComment.Text = dr("Comment").ToString
            End If
            dr.Close()

            pnlFeedback.Visible = True

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
            Dim objVendor As New VendorsDB()
            objVendor.UpdateVendorFeedback(VendorId, Int32.Parse(Context.User.Identity.Name), txtComment.Text, Int32.Parse(cboValue.SelectedItem.Value))

            ' Redirect back to the portal home page
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

        Private Sub lstFeedback_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles lstFeedback.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem
                    Select Case Int32.Parse(e.Item.DataItem("Value").ToString)
                        Case "1"
                            CType(e.Item.FindControl("lblValue"), Label).Text += "<img src=""" & IIf(Request.ApplicationPath = "/", "", Request.ApplicationPath) & "/images/ratingplus.gif"" border=""0"" alt=""" & GetLanguage("Positive_Feedback") & """>"
                        Case "-1"
                            CType(e.Item.FindControl("lblValue"), Label).Text += "<img src=""" & IIf(Request.ApplicationPath = "/", "", Request.ApplicationPath) & "/images/ratingminus.gif"" border=""0"" alt=""" & GetLanguage("Negative_Feedback") & """>"
                    End Select
                    CType(e.Item.FindControl("lblDate"), Label).Text = "&nbsp;&nbsp;<b>" & getlanguage("vendors_date") & ":</b>&nbsp;" & e.Item.DataItem("Date").ToString
                    CType(e.Item.FindControl("lblUser"), Label).Text = "&nbsp;&nbsp;<b>" & getlanguage("vendors_user") & ":</b>&nbsp;" & FormatEmail(e.Item.DataItem("Email"), page, e.Item.DataItem("FullName").ToString) 
					CType(e.Item.FindControl("lblComment"), Label).Text = e.Item.DataItem("Comment").ToString & "<br>"
            End Select
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

            pnlFeedback.Visible = False

        End Sub

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
            Dim objVendor As New VendorsDB()
            objVendor.DeleteVendorFeedback(VendorId, Int32.Parse(Context.User.Identity.Name))

            ' Redirect back to the portal home page
            Response.Redirect(CStr(ViewState("UrlReferrer")), True)
        End Sub

    End Class

End Namespace