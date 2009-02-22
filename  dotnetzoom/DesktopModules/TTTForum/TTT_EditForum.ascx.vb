'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' =======================================================================================


Imports DotNetZoom

Public Class TTT_EditForum
    Inherits PortalModuleControl

	Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
	
    Dim _editPage As String = glbPath & "DeskTopModules/TTTForum/TTT_EditForumPost.ascx"
    Public Enum ForumEditType
        ForumPost
        ForumAdmin
        GlobalSettings
        ForumModerate
        ForumModerateAdmin
        ForumPrivateMessage
        ForumUserAdmin
    End Enum

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
        'Put user code to initialize the page here
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        If Not (Request.Params("editpage") Is Nothing) Then
            Dim editType As Integer = CInt(Request.Params("editpage"))
            Select Case editType
                Case ForumEditType.ForumPost
                    If Context.Request.IsAuthenticated Then
                        _editPage = glbPath & "DeskTopModules/TTTForum/TTT_EditForumPost.ascx"
                        Title1.DisplayHelp = "DisplayHelp_EditForumPost"
                    Else
                        Response.Redirect(FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.SSL, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "def=Register"), True)
                    End If
            End Select
            If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Then
                Select Case editType
                    Case ForumEditType.ForumAdmin
                        _editPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumAdmin.ascx"
                        Title1.DisplayHelp = "DisplayHelp_ForumAdmin"
                    Case ForumEditType.GlobalSettings
                        _editPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumSettings.ascx"
                        Title1.DisplayHelp = "DisplayHelp_ForumSettings"
                    Case ForumEditType.ForumModerate
                        _editPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumModerate.ascx"
                        Title1.DisplayHelp = "DisplayHelp_ForumModerate"
                    Case ForumEditType.ForumModerateAdmin
                        _editPage = glbPath & "DeskTopModules/TTTForum/TTT_ModerateAdmin.ascx"
                        Title1.DisplayHelp = "DisplayHelp_ModerateAdmin"
                    Case ForumEditType.ForumPrivateMessage
                        _editPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumPMS.ascx"
                        Title1.DisplayHelp = "DisplayHelp_ForumPMS"
                    Case ForumEditType.ForumUserAdmin
                        _editPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumUserAdmin.ascx"
                        Title1.DisplayHelp = "DisplayHelp_ForumUserAdmin"
                End Select
            Else
                EditDenied()
            End If
        End If
        Dim objModule As PortalModuleControl = CType(CType(Me.Page, BasePage).LoadModule(_editPage), PortalModuleControl)
            If Not objModule Is Nothing Then
            	objModule.ModuleConfiguration = Me.ModuleConfiguration
				 Dim _Setting as Hashtable = PortalSettings.GetSiteSettings(_portalSettings.PortalID)
				If _Setting("editModuleContainer") <> "" then
					Dim arrContainer As Array = SplitContainer(_Setting("editModuleContainer"), _portalSettings.UploadDirectory,  IIf(_Setting("editcontainerAlignment") <> "", _Setting("editcontainerAlignment"), ""), IIf(_Setting("editcontainerColor") <> "", _Setting("editcontainerColor"), ""), IIf(_Setting("editcontainerBorder") <> "", _Setting("editcontainerBorder"), ""))
					dim pnlafter As New System.Web.UI.LiteralControl()
		  			dim pnlbefore As New System.Web.UI.LiteralControl()
					pnlbefore.text = arrContainer(0)
					Controls.Add(pnlbefore)
					pnlafter.Text = arrContainer(1)
					Controls.Add(objModule)
					Controls.Add(pnlafter)
				else
				Controls.Add(objModule)
				end if
        End If
        ForumConfig.SetSkinCSS(Me.Page)
    End Sub

End Class