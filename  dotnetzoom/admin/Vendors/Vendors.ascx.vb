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

    Public MustInherit Class Vendors

        Inherits DotNetZoom.PortalModuleControl
    	Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Protected WithEvents grdVendors As System.Web.UI.WebControls.DataGrid
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdEditModule As System.Web.UI.WebControls.HyperLink
        Protected WithEvents cmdEditOptions As System.Web.UI.WebControls.HyperLink


        Dim strFilter As String

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
            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
			Title1.DisplayHelp = "DisplayHelp_Vendors"
            If Not Page.IsPostBack Then
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")
                cmdDelete.Text = GetLanguage("Vendors_cmddelete")
			End If
			
			
            ' Edit Options
            cmdEditOptions.NavigateUrl = GetFullDocument() & "?edit=control&tabid=" & tabId & "&" & GetAdminPage() & "&options=1"
            cmdEditOptions.Text = GetLanguage("paramêtres")
            cmdEditOptions.ToolTip = GetLanguage("Vendors_OptionsEdit")



            ' Edit vendors
            cmdEditModule.NavigateUrl = GetFullDocument() & "?edit=control&tabid=" & tabId & "&" & GetAdminPage()
              cmdEditModule.ToolTip = GetLanguage("Vendors_Add")
			  cmdEditModule.Text = GetLanguage("add")

			
            If Not Request.Params("filter") Is Nothing Then
                strFilter = Request.Params("filter")
            Else
                strFilter = " "
            End If

            BindData()

        End Sub

        Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objVendors As New VendorsDB()
            Dim ds As DataSet

            If Not (Request.Params("hostpage") Is Nothing) Then
                ds = ConvertDataReaderToDataSet(objVendors.GetVendors(, strFilter))
            Else
                ds = ConvertDataReaderToDataSet(objVendors.GetVendors(_portalSettings.PortalId, strFilter))
            End If

            grdVendors.DataSource = ds
            grdVendors.PageSize = ds.Tables(0).Rows.Count + 1
            grdVendors.DataBind()
			
			
			grdVendors.Columns(1).HeaderText = GetLanguage("Vendor_Name")
			grdVendors.Columns(2).HeaderText = GetLanguage("F_UserAdress")
			grdVendors.Columns(3).HeaderText = GetLanguage("address_telephone")
			grdVendors.Columns(4).HeaderText = GetLanguage("Vendors_Fax")
			grdVendors.Columns(5).HeaderText = GetLanguage("address_Email")
			grdVendors.Columns(6).HeaderText = GetLanguage("U_Autorized")
			grdVendors.Columns(7).HeaderText = GetLanguage("Oriflamme")


        End Sub

        Public Function FormatURL(ByVal strKeyName As String, ByVal strKeyValue As String)  As String
            FormatURL = EditURL(strKeyName, strKeyValue) & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=" , "") & IIf(strFilter <> "", "&filter=" & strFilter, "")
        End Function

        Public Function DisplayAddress(ByVal Unit As Object, ByVal Street As Object, ByVal City As Object, ByVal Region As Object, ByVal Country As Object, ByVal PostalCode As Object)  As String
            DisplayAddress = FormatAddress(Unit, Street, City, Region, Country, PostalCode)
        End Function

        Public Function DisplayEmail(ByVal Email As Object)  As String
            DisplayEmail = FormatEmail(Email, page)
        End Function

        Private Sub grdVendors_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdVendors.ItemCreated

            Dim intCounter As Integer
            Dim objLinkButton As LinkButton

            If e.Item.ItemType = ListItemType.Pager Then
                Dim objCell As TableCell = CType(e.Item.Controls(0), TableCell)
                objCell.Controls.Clear()

                For intCounter = Asc("A") To Asc("Z")
                    objLinkButton = New LinkButton()
                    objLinkButton.Text = Chr(intCounter)
                    objLinkButton.CssClass = "CommandButton"
                    objLinkButton.CommandName = "filter"
                    objLinkButton.CommandArgument = objLinkButton.Text
                    objCell.Controls.Add(objLinkButton)
                   
                Next

                objLinkButton = New LinkButton()
                objLinkButton.Text = "(" & GetLanguage("all") & ")"
                objLinkButton.CssClass = "CommandButton"
                objLinkButton.CommandName = "filter"
                objLinkButton.CommandArgument = ""
                objCell.Controls.Add(objLinkButton)
                

                objLinkButton = New LinkButton()
                objLinkButton.Text = "(" & GetLanguage("Vendor_Non_Aut") & ")"
                objLinkButton.CssClass = "CommandButton"
                objLinkButton.CommandName = "filter"
                objLinkButton.CommandArgument = "-"
                objCell.Controls.Add(objLinkButton)
            End If

        End Sub

        Private Sub grdVendors_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdVendors.ItemCommand
            If e.CommandName = "filter" Then
                strFilter = e.CommandArgument
                BindData()
            End If
        End Sub

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objVendor As New VendorsDB()
            If Not (Request.Params("hostpage") Is Nothing) Then
                objVendor.DeleteVendors()
            Else
                objVendor.DeleteVendors(_portalSettings.PortalId)
            End If
            BindData()
        End Sub
		
		
    End Class

End Namespace