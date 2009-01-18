'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================
Option Strict On

Imports DotNetZoom
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    '<ToolboxData("<{0}:TTT_UsersList runat=server></{0}:TTT_UsersList>")> _
    Public MustInherit Class TTT_UsersControl
        Inherits System.Web.UI.UserControl
        Protected WithEvents grdUsers As System.Web.UI.WebControls.DataGrid
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
        Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents Span1 As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents Table3 As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents Span2 As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
		
        Private _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Private ZforumID As Integer = 0
        Private ZmoduleID As Integer = 0
        Private _IsForumModerator As Boolean
        Private _SelectedUser As Object

        Protected ZShowUserName As Boolean = False
        Protected ZShowUserNameLink As Boolean = False
        Protected ZShowAddress As Boolean = False
        Protected ZShowEmail As Boolean = False
        Protected ZShowFullName As Boolean = False
        Protected ZShowModerateButton As Boolean = False
        Protected ZShowEditButton As Boolean = False
        Protected ZShowProperties As New ArrayList()
        Protected ZDefaultFilterString As String = " "
        Protected ZforumModerators As ForumModeratorCollection
        Protected ZstrFilter As String

       

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

        Event UserSelected As EventHandler


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

		
            If IsNumeric(Request.Params("forumid")) Then
                ZforumID = Int16.Parse(Request.Params("forumid"))
            End If

            If IsNumeric(Request.Params("mid")) Then
                ZmoduleID = Int16.Parse(Request.Params("mid"))
            End If
			If Not Page.IsPostBack then
			If not Session("filter") is Nothing then
			ZstrFilter = Session("filter").ToString
			end if
			BindUsers()
			end if
        End Sub

        Sub BindUsers()

           if ZstrFilter is nothing then
	       ZstrFilter = DefaultFilterString
		   end if		

            Dim ds As DataSet
 
            Dim dbForumUser As New ForumUserDB()
            ds = ConvertDataReaderToDataSet(dbForumUser.TTTForum_GetUsers(_portalSettings.PortalId, ZstrFilter))
			grdUsers.DataSource = ds
            grdUsers.PageSize = ds.Tables(0).Rows.Count + 1
			With grdUsers
                .Columns(0).Visible = ZShowEditButton
                .Columns(1).Visible = ZShowUserName
                .Columns(2).Visible = ZShowUserNameLink
                .Columns(3).Visible = ZShowFullName
                .Columns(4).Visible = ZShowEmail
                .Columns(5).Visible = ZShowAddress
                .Columns(6).Visible = ZShowModerateButton
				
                .Columns(1).HeaderText = GetLanguage("F_UserCode")
                .Columns(2).HeaderText = GetLanguage("F_UserCode")
                .Columns(3).HeaderText = GetLanguage("F_UserName")
                .Columns(4).HeaderText = GetLanguage("F_UserEmail0")
                .Columns(5).HeaderText = GetLanguage("F_UserAdress")
            End With
            grdUsers.DataBind()

            Dim i As Integer
            For i = 0 To ZShowProperties.Count - 1
                grdUsers.Columns.Add(CType(ZShowProperties.Item(i), BoundColumn))
            Next
	
            grdUsers.AllowPaging = True

        End Sub

        Public Sub ShowProperty(ByVal PropertyName As String, ByVal DisplayName As String, ByVal Width As Integer)
            Dim col As New BoundColumn() ' DataGridColumn
            Dim colStyle As TableItemStyle
            Dim headStyle As TableItemStyle

            With col
                .HeaderText = DisplayName
                .DataField = PropertyName
                headStyle = .HeaderStyle
                headStyle.Width = Unit.Parse(CStr(Width))
                headStyle.CssClass = "TTTAltHeader"
                colStyle = .ItemStyle
                colStyle.CssClass = "TTTRow"
            End With

            ZShowProperties.Add(col)

        End Sub

        Private Sub grdUsers_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdUsers.ItemCreated

            Dim intCounter As Integer
            Dim objLinkButton As LinkButton

            If e.Item.ItemType = ListItemType.Pager Then
                Dim objCell As TableCell = CType(e.Item.Controls(0), TableCell)
                objCell.Controls.Clear()

                For intCounter = Asc("A") To Asc("Z")
                    objLinkButton = New LinkButton()
                    objLinkButton.Text = Chr(intCounter)
                    objLinkButton.CssClass = "userlink"
					If ZstrFilter = objLinkButton.Text then
					objLinkButton.ForeColor = system.drawing.color.Red
					end if
                    objLinkButton.CommandName = "filter"
                    objLinkButton.CommandArgument = objLinkButton.Text
                    objCell.Controls.Add(objLinkButton)
                  
                Next

                objLinkButton = New LinkButton()
                objLinkButton.Text = "(" & Getlanguage("all") & ")"
                objLinkButton.CssClass = "userlink"
                objLinkButton.CommandName = "filter"
                objLinkButton.CommandArgument = ""
				If ZstrFilter = "" then
				objLinkButton.ForeColor = system.drawing.color.Red
				end if
                objCell.Controls.Add(objLinkButton)

            End If

        End Sub

        Private Sub grdUsers_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUsers.ItemCommand
            Dim cmd As String = e.CommandName
            If cmd = "filter" Then
                ZstrFilter = CType(e.CommandArgument, String)
				Session("filter") = CType(e.CommandArgument, String)
				BindUsers()
            End If
        End Sub

        Public Sub UserSelect_Click(ByVal Sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            Dim selID As Integer = Int32.Parse(CType(Sender, ImageButton).CommandArgument)
            _SelectedUser = ForumUser.GetForumUser(selID)
            RaiseEvent UserSelected(Me, e)
        End Sub


        Public ReadOnly Property SelectedUser() As Object
            Get
                Return _SelectedUser
            End Get
        End Property

        Protected Function IsNotEmpty(ByVal Value As Object) As Boolean

            Return Len(CType(Value, String)) > 0

        End Function

        Protected Function IsModerator(ByVal UserID As Object) As Boolean
            ' Check nothing if we don't show moderate button
            If Not ZShowModerateButton Then Return False

            ' Check moderator value only if a forum is specified
            If ZforumID > 0 Then
                If ZforumModerators Is Nothing Then
                    ZforumModerators = New ForumModeratorCollection(ZforumID)
                End If
                _IsForumModerator = ZforumModerators.IsForumModerator(CType(UserID, Integer))
                Return _IsForumModerator
            End If

        End Function

        Protected Function CanAdd(ByVal UserID As String) As Boolean
            
            Return (Not _IsForumModerator)

        End Function

        Protected Function CanRemove(ByVal UserID As String) As Boolean
            
            Return (_IsForumModerator)
        End Function

        Protected Function CanEdit() As Boolean
            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) _
            OrElse PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) _
            OrElse PortalSecurity.IsInRoles(portalSettings.GetEditModuleSettings(ZmoduleID).AuthorizedEditRoles.ToString) Then
                Return True
            End If

        End Function

        Protected Function FormatURL(ByVal strKeyName As String, ByVal strKeyValue As String) As String
            FormatURL = GetFullDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&mid=" & ZmoduleID.ToString & "&" & strKeyName & "=" & strKeyValue
        End Function

        Protected Function FormatUserURL(ByVal strKeyValue As String) As String
            
           FormatUserURL = TTTUtils.ForumUserAdminLink(_portalSettings.ActiveTab.TabId, ZmoduleID, Int16.Parse(strKeyValue))
            
            
        End Function

        Protected Function DisplayAddress(ByVal Unit As Object, ByVal Street As Object, ByVal City As Object, ByVal Region As Object, ByVal Country As Object, ByVal PostalCode As Object) As String
            DisplayAddress = FormatAddress("", Street, City, Region, Country, "")
        End Function

        Protected Function DisplayEmail(ByVal Email As Object) As String
            DisplayEmail = FormatEmail(Email, page)
        End Function

        
        Public Property ShowUserName() As Boolean
            Get
                Return ZShowUserName
            End Get
            Set(ByVal Value As Boolean)
                ZShowUserName = Value
            End Set
        End Property

        
        Public Property ShowUserNameLink() As Boolean
            Get
                Return ZShowUserNameLink
            End Get
            Set(ByVal Value As Boolean)
                ZShowUserNameLink = Value
            End Set
        End Property

        
        Public Property ShowFullName() As Boolean
            Get
                Return ZShowFullName
            End Get
            Set(ByVal Value As Boolean)
                ZShowFullName = Value
            End Set
        End Property

        
        Public Property ShowEmail() As Boolean
            Get
                Return ZShowEmail
            End Get
            Set(ByVal Value As Boolean)
                ZShowEmail = Value
            End Set
        End Property

        
        Public Property ShowAddress() As Boolean
            Get
                Return ZShowAddress
            End Get
            Set(ByVal Value As Boolean)
                ZShowAddress = Value
            End Set
        End Property

        
        Public Property ShowModerateButton() As Boolean
            Get
                Return ZShowModerateButton
            End Get
            Set(ByVal Value As Boolean)
                ZShowModerateButton = Value
            End Set
        End Property

        
        Public Property ShowEditButton() As Boolean
            Get
                Return ZShowEditButton
            End Get
            Set(ByVal Value As Boolean)
                ZShowEditButton = Value
            End Set
        End Property

        
        Public Property DefaultFilterString() As String
            Get
                Return ZDefaultFilterString
            End Get
            Set(ByVal Value As String)
                ZDefaultFilterString = Value
            End Set
        End Property


    End Class

End Namespace