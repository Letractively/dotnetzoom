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

Public Class TTT_ForumSmiley

    Inherits System.Web.UI.Page

    
    Protected WithEvents dlStrip As System.Web.UI.WebControls.DataList
    Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
    Protected WithEvents tbStrip As System.Web.UI.HtmlControls.HtmlTable

    'Protected Zrequest As GalleryRequest
    Protected Zrequest As ForumSmilies
    Protected ZgalleryConfig As GalleryConfig
    Protected ZforumConfig As ForumConfig

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

			Dim objCSS As Control = page.FindControl("CSS")
			Dim objTTTCSS As Control = page.FindControl("TTTCSS")
            Dim objLink As System.Web.UI.LiteralControl
			Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            If (Not objCSS Is Nothing) and (objTTTCSS Is Nothing) Then
                    ' put in the ttt.css
					objLink = New System.Web.UI.LiteralControl("TTTCSS")
					If Request.IsAuthenticated Then
					Dim UserCSS as ForumUser
					UserCSS = ForumUser.GetForumUser(Int16.Parse(Context.User.Identity.Name))
					Select Case UserCSS.Skin
					case "Jardin Floral"
                        objLink.Text = "<link href=""" & glbPath & "images/TTT/skin1/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    Case "Stibnite"
                        objLink.Text = "<link href=""" & glbPath & "images/TTT/skin2/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    Case "Algues bleues"
                        objLink.Text = "<link href=""" & glbPath & "images/TTT/skin3/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					Case Else
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
					End Select
					else
					objLink.text = "<link href=""" & _portalSettings.UploadDirectory & "skin/ttt.css"" type=""text/css"" rel=""stylesheet"">"
                    End If
					objCSS.Controls.Add(objLink)
            End If

        Dim ModuleID As Integer = Int32.Parse(HttpContext.Current.Request("mid"))

        ZforumConfig = ForumConfig.GetForumConfig(ModuleID)

        If Not ZforumConfig.AvatarModuleID > 0 Then
            lblInfo.Text = GetLanguage("F_NoConfig")
            Return
        End If

        Zrequest = ForumSmilies.GetSmileys(ZforumConfig.AvatarModuleID)
        ZgalleryConfig = Zrequest.GalleryConfig

        dlStrip.RepeatColumns = ZgalleryConfig.StripWidth
        dlStrip.ItemStyle.Width = New WebControls.Unit(Int(ZgalleryConfig.StripWidth), UnitType.Percentage)
        dlStrip.DataSource = Zrequest.smileyItems
        dlStrip.DataBind()

    End Sub

    Private Sub dlStrip_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlStrip.ItemCreated
        Dim form As String = Request.QueryString("formname")
        Dim idParent As String = Request.QueryString("idParent")
        Dim smiley As ImageButton = CType(e.Item.FindControl("Smiley"), ImageButton)
        Dim itemInfo As IGalleryObjectInfo = CType(Zrequest.smileyItems.Item(e.Item.ItemIndex), IGalleryObjectInfo)
        Dim argument As String = Replace(itemInfo.Title, "Untitled", ":Untitled:") 'to handles untitled smiley

        If Not itemInfo.IsFolder Then
            Dim sb As New System.Text.StringBuilder()
            sb.Append("window.opener.SetParentValue('")
            sb.Append(form)
            sb.Append("','")
            sb.Append(idParent)
            sb.Append("','")
            sb.Append(argument)
            sb.Append("');")

            If Not smiley Is Nothing Then
                smiley.Attributes.Add("OnClick", sb.ToString)
            End If
        End If
    End Sub

End Class