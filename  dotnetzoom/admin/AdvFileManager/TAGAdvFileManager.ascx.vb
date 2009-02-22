Imports System.IO

Namespace DotNetZoom
	Public MustInherit Class TAGAdvFileManager
		Inherits System.Web.UI.UserControl

		Protected WithEvents DirPanel As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents AdminPanel As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents DirCreate As System.Web.UI.WebControls.TextBox
		Protected WithEvents imgDirOK As TAG.WebControls.RolloverImageButton
		Protected WithEvents imgDirCancel As TAG.WebControls.RolloverImageButton
		Protected WithEvents lblerror As System.Web.UI.WebControls.Label
		Protected WithEvents pnlToolBar As System.Web.UI.WebControls.panel
		Protected WithEvents Image1 As System.Web.UI.WebControls.Image
		Protected WithEvents Table1 As System.Web.UI.WebControls.Table
		Protected WithEvents FileExp As TAGFileExplorer
		Protected WithEvents Image2 As System.Web.UI.WebControls.Image
		Protected WithEvents Image3 As System.Web.UI.WebControls.Image
		Protected WithEvents imgRefresh As TAG.WebControls.RolloverImageButton
		Protected WithEvents imgNewFolder As TAG.WebControls.RolloverImageButton
		Protected WithEvents imgUpload As TAG.WebControls.RolloverImageButton
		Protected WithEvents imgDownload As TAG.WebControls.RolloverImageButton
		Protected WithEvents imgDelete As TAG.WebControls.RolloverImageButton
		Protected WithEvents imgRename As TAG.WebControls.RolloverImageButton
        Protected WithEvents imgParentDir As TAG.WebControls.RolloverImageButton

		

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
			Dim incScript As String = String.Format("<script Language=""javascript"" SRC=""{0}""></script>", ResolveUrl("dialog.js"))
			Page.RegisterClientScriptBlock("FileManager", incScript)

            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim retScript As String = "<script language=""javascript"">" & vbCrLf & "<!--" & vbCrLf
			retScript &= "function retVal()" & vbCrLf & "{" & vbCrLf
			retScript &= vbTab & Page.GetPostBackEventReference(imgRefresh) & ";" & vbCrLf & "}" & vbCrLf
			retScript &= "--></script>"
			Page.RegisterClientScriptBlock("FileManagerRefresh", retScript)

			imgDelete.Attributes.Add("onClick", "JavaScript:return confirm('" & rtesafe(GetLanguage("request_confirm")) & "');")
  			Dim click As String = String.Format("openDialog('{0}', 600, 200, retVal);return false", ResolveUrl("TAGFileUploadDialog.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabID)), imgRefresh.ClientID)
			imgUpload.Attributes.Add("onclick", click)
			If Session("message") <> "" then
				lblerror.text = Session("message")
				Session("message") = ""
			end if

			If Not Page.IsPostBack then
			DirPanel.Visible = False
			AdminPanel.Visible = True
			end if
			InitToolbar()
		End Sub

		Private Sub InitToolbar()

		Dim objAdmin As New AdminDB()
		Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		Dim tmpUploadRoles As String = ""
		If Not CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("uploadroles"), String) Is Nothing Then
   			tmpUploadRoles = CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("uploadroles"), String)
		End If

            If Session("FCKeditor:UserFilesPath") = "" And ((Request.IsAuthenticated = False) Or (PortalSecurity.IsInRoles(tmpUploadRoles) = False)) Then
                imgNewFolder.Visible = False
                imgUpload.Visible = False
                imgRename.Visible = False
                imgDelete.Visible = False
            End If

            imgParentDir.ToolTip = GetLanguage("F_ParentDir")
            imgParentDir.StatusText = GetLanguage("F_ParentDir")
            imgRefresh.Tooltip = GetLanguage("F_Refresh")
            imgRefresh.StatusText = GetLanguage("F_Refresh")
            imgNewFolder.Tooltip = GetLanguage("F_New_Folder")
            imgNewFolder.StatusText = GetLanguage("F_New_Folder")
            imgUpload.Tooltip = GetLanguage("F_Upload")
            imgUpload.StatusText = GetLanguage("F_Upload")
            imgDownload.Tooltip = GetLanguage("F_Download")
            imgDownload.StatusText = GetLanguage("F_Download")
            imgDelete.Tooltip = GetLanguage("F_delete")
            imgDelete.StatusText = GetLanguage("F_delete")
            imgRename.Tooltip = GetLanguage("F_Rename")
            imgRename.StatusText = GetLanguage("F_Rename")
            imgDownload.Enabled = True
            imgRename.Enabled = True
            imgDelete.Enabled = True
        End Sub

		Private Sub imgParentDir_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgParentDir.Click
			FileExp.NavigateParentDir()
		End Sub

		Private Sub FileExp_DirChanged(ByVal sender As Object, ByVal e As DotNetZoom.DirChangedEventArgs) Handles FileExp.DirChanged
			If e.IsRoot Then
				imgParentDir.Enabled = False
			Else
				imgParentDir.Enabled = True
			End If
		End Sub

		Private Sub FileExp_FileClicked(ByVal sender As Object, ByVal e As DotNetZoom.FileClickedEventArgs) Handles FileExp.FileClicked
            
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objSecurity As New PortalSecurity()
            Dim crypto As String = Server.UrlEncode(objSecurity.Encrypt(Application("cryptokey"), e.FullFileName))

            Response.Redirect(ResolveUrl("TAGFileDownload.aspx") & "?File=" & crypto & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId))
		    
		End Sub

		Private Sub imgRefresh_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRefresh.Click
			FileExp.Refresh()
		End Sub

		Private Sub imgDelete_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDelete.Click
			FileExp.DeleteSelected()
		End Sub

		Private Sub imgDownload_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDownload.Click
			FileExp.DownloadFile()
		End Sub

		Private Sub imgRename_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRename.Click
			FileExp.EditFileFolderName()
		End Sub

		Private Sub imgDirOK_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDirOK.Click
			If DirCreate.Text <> "" then
			' need to check for char
		
			If IsNotAlpha(DirCreate.Text) then
			lblerror.Text = GetLanguage("Gal_AlbumNo")
			Dim Admin As New AdminDB()
			DirCreate.Text = Admin.convertstringtounicode(DirCreate.Text)
			else
			FileExp.CreateNewFolder( DirCreate.Text)
			DirPanel.Visible = False
			AdminPanel.Visible = True
			end if
			Else
			lblerror.Text = GetLanguage("Need_Directory_Name") 
			end if
		End Sub

		Private Sub imgDirCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDirCancel.Click
			DirCreate.Text = ""
			lblerror.Text = ""
			DirPanel.Visible = False
			AdminPanel.Visible = True
		End Sub
		
		
		
		Private Sub imgNewFolder_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNewFolder.Click
		DirPanel.Visible = True
		AdminPanel.Visible = False
		DirCreate.Text = GetLanguage("New_Directory")
		lblerror.Text = ""
		End Sub
	End Class
End Namespace