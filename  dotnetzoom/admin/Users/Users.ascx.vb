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

    Public MustInherit Class Users

        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents grdUsers As System.Web.UI.WebControls.DataGrid
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
    	Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

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
        ' The Page_Load server event handler on this user control is used
        ' to populate the current roles settings from the configuration system
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If Not PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If


            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"
			Title1.DisplayHelp = "DisplayHelp_Users"
            If Not Page.IsPostBack Then
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & rtesafe(GetLanguage("request_confirm_erase")) & "');")
            	cmdDelete.Text = GetLanguage("Users_Delete")
			End If

            If Not Request.Params("filter") Is Nothing Then
                strFilter = Request.Params("filter")
            Else
                strFilter = " "
            End If

            BindData()

        End Sub

        '*******************************************************
        '
        ' The BindData helper method is used to bind the list of
        ' users for this portal to an asp:DropDownList server control
        '
        '*******************************************************

        Sub BindData()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Get the list of registered users from the database
            Dim objUser As New UsersDB()

            grdUsers.AllowPaging = True
            Dim ds As DataSet
            ds = ConvertDataReaderToDataSet(objUser.GetUsers(_portalSettings.PortalId, strFilter))
            grdUsers.DataSource = ds
            grdUsers.PageSize = ds.Tables(0).Rows.Count + 1

            grdUsers.DataBind()
			grdUsers.Columns(2).HeaderText = GetLanguage("Name")
            grdUsers.Columns(3).HeaderText = GetLanguage("Label_UserName")
            grdUsers.Columns(4).HeaderText = GetLanguage("F_UserAdress")
            grdUsers.Columns(5).HeaderText = GetLanguage("address_telephone")
            grdUsers.Columns(6).HeaderText = GetLanguage("address_Email")
			grdUsers.Columns(7).HeaderText = GetLanguage("U_LastLogin_Date")
			grdUsers.Columns(8).HeaderText = GetLanguage("U_Autorized")
			
        End Sub

        Public Function FormatURL(ByVal strKeyName As String, ByVal strKeyValue As String)  As String
            FormatURL = EditURL(strKeyName, strKeyValue) & IIf(strFilter <> "", "&filter=" & strFilter, "")
        End Function

		Public Function FormatURLRole(ByVal strKeyName As String, ByVal strKeyValue As String)  As String
            FormatURLRole =  EditURL(strKeyName, strKeyValue) & "&def=User Roles"
         End Function
		
		
        Public Function DisplayAddress(ByVal Unit As Object, ByVal Street As Object, ByVal City As Object, ByVal Region As Object, ByVal Country As Object, ByVal PostalCode As Object)  As String
            DisplayAddress = FormatAddress(Unit, Street, City, Region, Country, PostalCode)
        End Function

        Public Function DisplayEmail(ByVal Email As Object)  As String
            DisplayEmail = FormatEmail(Email, page)
        End Function

        Public Function DisplayLastLogin(ByVal LastLogin As Object) As String
            If Not IsDBNull(LastLogin) Then
                DisplayLastLogin = Format(LastLogin, "yyyy/MM/dd")
            Else
                DisplayLastLogin = ""
            End If
        End Function

        Private Sub grdUsers_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdUsers.ItemCreated

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
                objLinkButton.Text = "(" & Getlanguage("all") & ")"
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

        Private Sub grdUsers_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUsers.ItemCommand
            If e.CommandName = "filter" Then
                strFilter = e.CommandArgument
                BindData()
            End If
        End Sub

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

            Dim objUser As New UsersDB()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            objUser.DeleteUsers(_portalSettings.PortalId)

            BindData()

        End Sub

    End Class

End Namespace