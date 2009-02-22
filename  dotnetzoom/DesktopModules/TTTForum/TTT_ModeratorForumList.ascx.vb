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

    Public MustInherit Class TTT_ModeratorForumList

        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents message As System.Web.UI.WebControls.Literal
        Protected WithEvents grdForums As System.Web.UI.WebControls.DataGrid
        Private _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Private ZuserID As Integer
        Protected WithEvents btnSave As System.Web.UI.WebControls.Button
        Public Shared ZmoderateForums As ForumModerateCollection

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
        '*******************************************************
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

 			
			Dim ImageFolder As String = ForumConfig.SkinImageFolder()
			
					

            If IsNumeric(Request.Params("userid")) Then
                ZuserID = Int16.Parse(Request.Params("userid"))
            End If

            If Not Page.IsPostBack Then
				btnSave.Text = GetLanguage("enregistrer")
				btnSave.ToolTip = GetLanguage("enregistrer")	
                BindModerateForum()

            End If
		    
        End Sub

        Private Sub BindModerateForum()
            Dim dbForumUser As New ForumUserDB()

			grdForums.Columns(0).HeaderText = GetLanguage("F_ModerateForum")
            grdForums.Columns(1).HeaderText = GetLanguage("F_Created")
            grdForums.Columns(2).HeaderText = GetLanguage("F_EMailS")
            grdForums.DataSource = dbForumUser.TTTForum_Moderate_GetForumsByUser(ZuserID)
            grdForums.DataBind()


        End Sub

	Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
		Dim grdItem As DataGridItem
        Dim dbForum As New ForumDB()
		message.Text = GetLanguage("enregistrer")
				For Each grdItem In grdForums.Items
					Dim chkItem As Control = grdItem.FindControl("Email")
					If CType(chkItem, CheckBox).Checked = True Then
            		dbForum.TTTForum_Moderate_UserCreateUpdateDelete(Convert.ToInt16(grdItem.Cells(3).Text), ZuserID, True, 1)
				    Else
             		dbForum.TTTForum_Moderate_UserCreateUpdateDelete(Convert.ToInt16(grdItem.Cells(3).Text), ZuserID, False, 1)
					End If
				Next
	End Sub

End Class



End Namespace