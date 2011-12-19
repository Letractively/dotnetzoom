'=======================================================================================
' TTTFORUM MODULE FOR DOTNETNUKE (1.0.10)
'======================================================================================= 
' For TTTCompany http://www.tttcompany.com
' Author: TAM TRAN MINH (tamttt - tam@tttcompany.com)
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
' =======================================================================================


Namespace DotNetZoom
    Public Class TTT_ForumDispatch
        Inherits PortalModuleControl
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


        Public Enum ForumDesktopType
            ForumMain
            ForumSearch
            ForumProfile
            ForumSubscribe
            ForumPrivateMessage
        End Enum

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Title1.EditURL = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString, "edit=control&forumid=0&action=new")
 
            Dim _DefaultPage As String = glbPath & "DeskTopModules/TTTForum/TTT_Forum.ascx"

           If Request.IsAuthenticated and ((PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True _
                OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                OrElse PortalSecurity.IsInRoles(CType(portalSettings.GetEditModuleSettings(ModuleId), ModuleSettings).AuthorizedEditRoles.ToString) = True)) Then
				Title1.DisplayHelp = "DisplayHelp_Forum"
            Else
				Title1.DisplayHelp = "DisplayHelp_ForumAnonymous"
            End If

			
			
            If IsNumeric(Request.Params("forumpage")) Then
                Dim ForumPage As Integer = CInt(Request.Params("forumpage"))
                Select Case ForumPage
                    Case ForumDesktopType.ForumMain
                        _DefaultPage = glbPath & "DeskTopModules/TTTForum/TTT_Forum.ascx"
                    Case ForumDesktopType.ForumSearch
                        _DefaultPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumSearch.ascx"
                        Title1.DisplayHelp = "DisplayHelp_ForumSearch"
                    Case ForumDesktopType.ForumProfile
                        _DefaultPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumUserProfile.ascx"
                        Title1.DisplayHelp = Nothing
                        If (PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True _
                            OrElse (PortalSecurity.IsInRoles(_portalSettings.ActiveTab.AdministratorRoles.ToString) = True) _
                            OrElse PortalSecurity.IsInRoles(CType(PortalSettings.GetEditModuleSettings(ModuleId), ModuleSettings).AuthorizedEditRoles.ToString) = True) Then
                            Dim TempUserId As Integer = -1
                            If Request.IsAuthenticated Then
                                TempUserId = CType(Context.User.Identity.Name, Integer)
                            End If
                            If IsNumeric(Request.Params("userid")) Then
                                If TempUserId = Int32.Parse(Request.Params("userid")) Or PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) Then
                                    Title1.DisplayHelp = "DisplayHelp_ForumUserProfile"
                                End If
                            End If
                        End If

                    Case ForumDesktopType.ForumSubscribe
                        If Request.IsAuthenticated = False Then
                            AccessDenied()
                        End If
                        _DefaultPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumSubscribe.ascx"
                        Title1.DisplayHelp = "DisplayHelp_ForumSubscribe"
                    Case ForumDesktopType.ForumPrivateMessage
                        If Request.IsAuthenticated = False Then
                            AccessDenied()
                        End If
                        _DefaultPage = glbPath & "DeskTopModules/TTTForum/TTT_ForumPMS.ascx"
                        Title1.DisplayHelp = "DisplayHelp_PMSInbox"
                        If Request.Params("pmsTabId") = "1" Then Title1.DisplayHelp = "DisplayHelp_PMSInbox"
                        If Request.Params("pmsTabId") = "2" Then Title1.DisplayHelp = "DisplayHelp_PMSOutbox"
                        If Request.Params("pmsTabId") = "3" Then Title1.DisplayHelp = "DisplayHelp_PMSCompose"
                    Case Else
                        _DefaultPage = glbPath & "DeskTopModules/TTTForum/TTT_Forum.ascx"
                End Select
            End If
            Dim objModule As PortalModuleControl = CType(CType(Me.Page, BasePage).LoadModule(_DefaultPage), PortalModuleControl)
            If Not objModule Is Nothing Then
            	objModule.ModuleConfiguration = Me.ModuleConfiguration
				 Dim _Setting as Hashtable = PortalSettings.GetModuleSettings(objModule.ModuleID)
				If Not _setting("ModuleContainer") Is Nothing then
					Dim arrContainer As Array = SplitContainer(CType(_Setting("ModuleContainer"), String), _portalSettings.UploadDirectory,  IIf(_Setting("containerAlignment") <> "", _Setting("containerAlignment"), ""), IIf(_Setting("containerColor") <> "", _Setting("containerColor"), ""), IIf(_Setting("containerBorder") <> "", _Setting("containerBorder"), ""))
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
End Namespace