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

Imports System.IO

Namespace DotNetZoom

    Public Class EditImage
        Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected WithEvents optExternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents optInternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents txtExternal As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboInternal As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink
        Protected WithEvents txtAlt As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtArtist As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtCopyright As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents valAltText As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents valWidth As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents valHeight As System.Web.UI.WebControls.RegularExpressionValidator 
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton

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

        '****************************************************************
        '
        ' The Page_Load event on this Page is used to obtain the ModuleId
        ' of the image module to edit.
        '
        ' It then uses the ASP.NET configuration system to populate the page's
        ' edit controls with the image details.
        '
        '****************************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Title1.DisplayHelp = "DisplayHelp_EditImage"
			' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim fileName as String
			cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdUpload.Text = GetLanguage("upload")
			valAltText.ErrorMessage =  "<br>" + GetLanguage("need_alt")
			valwidth.ErrorMessage =  "<br>" + GetLanguage("need_number")
			valheight.ErrorMessage =  "<br>" + GetLanguage("need_number")
            If optExternal.Checked = False And optInternal.Checked = False Then
                optInternal.Checked = True
            End If

            EnableControls()

            If Page.IsPostBack = False Then

                ' load the list of files found in the upload directory
                cmdUpload.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & TabId & "&def=Gestion fichiers"
                Dim FileList As ArrayList = GetFileList(_portalSettings.PortalId, glbImageFileTypes)
                cboInternal.DataSource = FileList
                cboInternal.DataBind()

                If ModuleId <> -1 Then

                    Dim settings As Hashtable

                    ' Get settings from the database
                    settings = PortalSettings.GetModuleSettings(ModuleId)

                    If InStr(1, CType(settings("src"), String), "://") = 0 Then
						fileName = Server.MapPath(_portalSettings.UploadDirectory & CType(Settings("src"), String))
                        optInternal.Checked = True
                        optExternal.Checked = False
                        EnableControls()
                        If cboInternal.Items.Contains(New ListItem(CType(settings("src"), String))) Then
                            cboInternal.Items.FindByText(CType(settings("src"), String)).Selected = True
                        End If
						If File.Exists(FileName) then
						Dim Exif As New ExifWorks(FileName)
        				txtArtist.Text = Exif.Artist
						If TxtArtist.Text = "" then
						
						Dim objUser As New UsersDB()
                		Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, Int32.Parse(Context.User.Identity.Name))
	
                		' Read first row from database
                		If dr.Read() Then
                    	TxtArtist.Text = dr("FirstName").ToString
                        TxtArtist.Text +=  " " & dr("LastName").ToString
               			 End If
                		dr.Close()
						end if 
        				txtCopyright.Text = Exif.Copyright
						If TxtCopyright.Text = "" then
						TxtCopyright.Text = GetDomainName(Request)
						end if
       					txtDescription.Text = Exif.Description
        				txtTitle.Text = Exif.Title
				        Exif.Dispose()
						end if
                    Else
                        optInternal.Checked = False
                        optExternal.Checked = True
                        EnableControls()
                        txtExternal.Text = CType(settings("src"), String)
                    End If
                    
                    txtAlt.Text = CType(settings("alt"), String)
                    txtWidth.Text = CType(settings("width"), String)
                    txtHeight.Text = CType(settings("height"), String)

                End If

                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = "~" & GetDocument() & "?tabid=" & TabId

            End If

        End Sub


        '****************************************************************
        '
        ' The cmdUpdate_Click event handler on this Page is used to save
        ' the settings to the ModuleSettings database table.  It  uses the
        ' DotNetZoom.AdminDB() data component to encapsulate the data
        ' access functionality.
        '
        '****************************************************************

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            ' Update settings in the database
            Dim objAdmin As New AdminDB()
            Dim strImage As String
			Dim fileName as String


            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If txtExternal.Text <> "" Then
                strImage = AddHTTP(txtExternal.Text)
            Else
                strImage = cboInternal.SelectedItem.Text
				fileName = Server.MapPath(_portalSettings.UploadDirectory & strImage)
				
				If File.Exists(FileName) then
				Dim Exif As New ExifWorks(FileName)
		        Exif.Artist = txtArtist.Text
        		Exif.Copyright = txtCopyright.Text
        		Exif.Description = txtDescription.Text
        		Exif.Title = txtTitle.Text
        		Dim BMP As System.Drawing.Bitmap = Exif.GetBitmap()
        		BMP.Save(FileName & ".tmp")
        		BMP.Dispose()
        		Exif.Dispose()
		        System.IO.File.Delete(FileName)
        		System.IO.File.Move(FileName & ".tmp", FileName)
 				End If
                Dim dr As SqlDataReader = objAdmin.GetSingleFile(cboInternal.SelectedItem.Text, _portalSettings.PortalId)
                If dr.Read Then
                    If txtWidth.Text = "" Then
                        txtWidth.Text = dr("Width").ToString
                    End If
                    If txtHeight.Text = "" Then
                        txtHeight.Text = dr("Height").ToString
                    End If
                End If
                dr.Close()
            End If

            objAdmin.UpdateModuleSetting(ModuleId, "src", strImage)
            objAdmin.UpdateModuleSetting(ModuleId, "alt", txtAlt.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "width", txtWidth.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "height", txtHeight.Text)
            ' Redirect back to the portal home page
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub


        '****************************************************************
        '
        ' The cmdCancel_Click event handler on this Page is used to cancel
        ' out of the page, and return the user back to the portal home
        ' page.
        '
        '****************************************************************

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub EnableControls()
            If optExternal.Checked Then
                cboInternal.ClearSelection()
                cboInternal.Enabled = False
                txtExternal.Enabled = True
            Else
                cboInternal.Enabled = True
                txtExternal.Text = ""
                txtExternal.Enabled = False
            End If
        End Sub

    End Class

End Namespace