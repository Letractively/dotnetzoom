'=======================================================================================
' TTTGALLERY MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' Original version by:              DAVID BARRETT http://www.davidbarrett.net
' A modified version for DNN by:    KENNYRICE
' This version for TTTCompany       http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' Slideshow component:              FREEK VAN OORT
' Flash Player component:           TYLER JENSEN
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
'========================================================================================

Imports DotNetZoom

Public Class TTT_EditGallery
    Inherits PortalModuleControl

	Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
	
    Dim _editPage As String = glbPath & "DeskTopModules/TTTGallery/TTT_GalleryEditAlbum.ascx"

    Public Enum GalleryEditType
        GalleryAlbum
        GalleryAdmin
        GalleryEditAlbum
        GalleryEditFile
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

        
        If IsNumeric(Request.Params("editpage")) Then
            Dim editType As Integer = CInt(Request.Params("editpage"))
            Select Case editType
                Case GalleryEditType.GalleryEditAlbum
                    _editPage = glbPath & "DeskTopModules/TTTGallery/TTT_GalleryEditAlbum.ascx"
                    Title1.DisplayHelp = "DisplayHelp_EditAlbum"
                Case GalleryEditType.GalleryEditFile
                    _editPage = glbPath & "DeskTopModules/TTTGallery/TTT_GalleryEditFile.ascx"
                    Title1.DisplayHelp = "DisplayHelp_EditFile"
                Case GalleryEditType.GalleryAdmin
                    _editPage = glbPath & "DeskTopModules/TTTGallery/TTT_GalleryAdmin.ascx"
                    Title1.DisplayHelp = "DisplayHelp_GalleryAdmin"
            End Select
        End If

			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If (Not objCSS Is Nothing) and (objTTTCSS Is Nothing) Then
                    ' put in the ttt.css
					objLink = New System.Web.UI.LiteralControl("TTTCSS")
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    objCSS.Controls.Add(objLink)
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
    End Sub

End Class