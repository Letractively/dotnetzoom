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

Imports System.IO

Namespace DotNetZoom

    Public Class EditImage
        Inherits DotNetZoom.PortalModuleControl
		
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Protected WithEvents optExternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents optInternal As System.Web.UI.WebControls.RadioButton
        Protected WithEvents optInfoBule As System.Web.UI.WebControls.CheckBox
        Protected WithEvents optPosition As System.Web.UI.WebControls.CheckBox
        Protected WithEvents optGoogleEarth As System.Web.UI.WebControls.CheckBox
        Protected WithEvents optInternalLink As System.Web.UI.WebControls.CheckBox
        Protected WithEvents optExif As System.Web.UI.WebControls.CheckBox
        Protected WithEvents optSecure As System.Web.UI.WebControls.CheckBox
        Protected WithEvents txtExternal As System.Web.UI.WebControls.TextBox
        Protected WithEvents cboInternal As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboInternalLink As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cboInternalGPS As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink
        Protected WithEvents UploadReturn As System.Web.UI.WebControls.ImageButton
        Protected WithEvents txtAlt As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtArtist As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtCopyright As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtExif As System.Web.UI.WebControls.TextBox
        Protected WithEvents ExternalLink As System.Web.UI.WebControls.TextBox
        Protected WithEvents InfoLink As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtLatLong As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
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
            cmdUpdate.Text = GetLanguage("enregistrer")
			cmdCancel.Text = GetLanguage("annuler")
			cmdUpload.Text = GetLanguage("upload")
            If optExternal.Checked = False And optInternal.Checked = False Then
                optInternal.Checked = True
            End If

            EnableControls()
            SetUpUpload(Not Page.IsPostBack)
            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                cboInternalLink.DataSource = GetPortalTabs(PortalSettings.Getportaltabs(_portalSettings.PortalId, GetLanguage("N")), True, True)
                cboInternalLink.DataBind()
                If ModuleId <> -1 Then
                    SetUpData()
                End If
            End If

        End Sub

        Private Sub SetUpData()
            Dim settings As Hashtable
            Dim fileName As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            ' Get settings from the database
            settings = PortalSettings.GetModuleSettings(ModuleId)

            If cboInternalGPS.Items.Contains(New ListItem(CType(settings("fileGPS"), String))) Then
                cboInternalGPS.Items.FindByText(CType(settings("fileGPS"), String)).Selected = True
            End If

            If Not cboInternalLink.Items.FindByValue(CType(settings("link"), String)) Is Nothing Then
                cboInternalLink.Items.FindByValue(CType(settings("link"), String)).Selected = True
            End If


            If InStr(1, CType(settings("src"), String), "://") = 0 Then
                fileName = Server.MapPath(_portalSettings.UploadDirectory & CType(settings("src"), String))
                optInternal.Checked = True
                optExternal.Checked = False
                EnableControls()

                If cboInternal.Items.Contains(New ListItem(CType(settings("src"), String))) Then
                    cboInternal.Items.FindByText(CType(settings("src"), String)).Selected = True
                End If
                If File.Exists(fileName) Then
                    Dim Exif As New ExifWorks(fileName)
                    If CType(settings("Exif"), String) <> "" Then
                        txtExif.Text = CType(settings("Exif"), String)
                    Else
                        txtExif.Text = Exif.ToString()
                    End If
                    txtArtist.Text = Exif.Artist


                    txtCopyright.Text = Exif.Copyright
                    txtDescription.Text = Exif.Description
                    txtTitle.Text = Exif.Title
                    If CType(settings("latlong"), String) = String.Empty And Exif.Latitude(3) <> String.Empty Then
                        'txtLatLong.Text = "http://maps.google.com/maps?q=" + Exif.Latitude(3) + "," + Exif.Longitude(3) + "&t=p&z=12&output=embed"
                        'Dim objSecurity As New DotNetZoom.PortalSecurity()
                        'txtLatLong.Text = glbHTTP + "desktopmodules/maps/googlemap.aspx?ll=" + objSecurity.EncryptRijndael(DateTime.Now().ToString("yyyyMMdd"), Exif.Latitude(3) + "," + Exif.Longitude(3)) + "&tt=" + objSecurity.EncryptRijndael(DateTime.Now().ToString("yyyyMMdd"), txtTitle.Text)
                        txtLatLong.Text = Exif.Latitude(3) + "," + Exif.Longitude(3)
                    End If
                    Exif.Dispose()
                End If
            Else
                optInternal.Checked = False
                optExternal.Checked = True
                EnableControls()
                txtExternal.Text = CType(settings("src"), String)
            End If


            If txtArtist.Text = "" And CType(settings("Artist"), String) = "" Then
                Dim objUser As New UsersDB()
                Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, Int32.Parse(Context.User.Identity.Name))

                ' Read first row from database
                If dr.Read() Then
                    txtArtist.Text = dr("FirstName").ToString
                    txtArtist.Text += " " & dr("LastName").ToString
                End If
                dr.Close()
            ElseIf txtArtist.Text = "" Then
                txtArtist.Text = CType(settings("Artist"), String)
            End If

            If txtCopyright.Text = "" And CType(settings("Copyright"), String) = "" Then
                txtCopyright.Text = GetDomainName(Request)
            ElseIf txtCopyright.Text = "" Then
                txtCopyright.Text = CType(settings("Copyright"), String)
            End If

            If txtTitle.Text = "" Then
                txtTitle.Text = CType(settings("Title"), String)
            End If


            If txtDescription.Text = "" Then
                txtDescription.Text = CType(settings("infobule"), String)
            End If

            If txtLatLong.Text = "" Then
                txtLatLong.Text = CType(settings("latlong"), String)
            End If



            ' txtLatLong.Text = CType(settings("latlong"), String)
            txtAlt.Text = CType(settings("alt"), String)
            txtWidth.Text = CType(settings("width"), String)
            txtHeight.Text = CType(settings("height"), String)
            optInfoBule.Checked = CType(settings("show"), Boolean)
            optPosition.Checked = CType(settings("position"), Boolean)
            optSecure.Checked = CType(settings("secure"), Boolean)

            optGoogleEarth.Checked = CType(settings("optGoogleEarth"), Boolean)
            optInternalLink.Checked = CType(settings("optInternalLink"), Boolean)
            optExif.Checked = CType(settings("optExif"), Boolean)
            ExternalLink.Text = CType(settings("ExtLink"), String)
            InfoLink.Text = CType(settings("InfoLink"), String)

        End Sub

        Private Sub SetUpUpload(ByVal SetFile As Boolean)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            UploadReturn.Visible = True
            UploadReturn.Style.Add("display", "none")
            cmdUpload.NavigateUrl = "#"
            SetUpModuleUpload(Request.MapPath(_portalSettings.UploadDirectory), glbImageFileTypes + ",zip," + glbGoogleEarthTypes, False)
            Dim incScript As String = String.Format("<script Language=""javascript"" SRC=""{0}""></script>", ResolveUrl("/admin/advFileManager/dialog.js"))
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "FileManager", incScript)
            Dim retScript As String = "<script language=""javascript"">" & vbCrLf & "<!--" & vbCrLf
            retScript &= "function retVal()" & vbCrLf & "{" & vbCrLf
            retScript &= vbTab & Page.ClientScript.GetPostBackEventReference(UploadReturn, String.Empty) & ";" & vbCrLf & "}" & vbCrLf
            retScript &= "--></script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "FileManagerRefresh", retScript)
            Dim click As String = String.Format("openDialog('{0}', 700, 700, retVal);return false", ResolveUrl("/Admin/AdvFileManager/TAGFileUploadDialog.aspx?L=" & GetLanguage("N") & "&tabid=" & CStr(_portalSettings.ActiveTab.TabId)) & IIf(Not (Request.Params("hostpage") Is Nothing), "&hostpage=", ""), UploadReturn.ClientID)
            cmdUpload.Attributes.Add("onclick", click)
            If SetFile Then
                Dim FileList As ArrayList = GetFileList(_portalSettings.PortalId, glbImageFileTypes)
                cboInternal.DataSource = FileList
                cboInternal.DataBind()
                Dim FileListGPS As ArrayList = GetFileList(_portalSettings.PortalId, glbGoogleEarthTypes)
                cboInternalGPS.DataSource = FileListGPS
                cboInternalGPS.DataBind()
                If cboInternalGPS.Items.Count = 1 Then
                    optGoogleEarth.Checked = False
                    optGoogleEarth.Enabled = False
                End If
            End If
        End Sub

        Private Sub UploadReturn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles UploadReturn.Click
            SetUpUpload(True)
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
            Dim fileName As String


            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)


            If txtExternal.Text <> "" Then
                strImage = AddHTTP(txtExternal.Text)
            Else
                strImage = cboInternal.SelectedItem.Text
                fileName = Server.MapPath(_portalSettings.UploadDirectory & strImage)
                If File.Exists(fileName) Then
                    Dim Exif As New ExifWorks(fileName)
                    If Exif.Artist <> txtArtist.Text Or _
                       Exif.Copyright <> txtCopyright.Text Or _
                       Exif.Description <> txtDescription.Text Or _
                       Exif.Title <> txtTitle.Text Then
                        Exif.Artist = txtArtist.Text
                        Exif.Copyright = txtCopyright.Text
                        Exif.Description = txtDescription.Text
                        Exif.Title = txtTitle.Text
                        Dim BMP As System.Drawing.Bitmap = Exif.GetBitmap()
                        BMP.Save(fileName & ".tmp")
                        BMP.Dispose()
                        Exif.Dispose()
                        System.IO.File.Delete(fileName)
                        System.IO.File.Move(fileName & ".tmp", fileName)
                    Else
                        Exif.Dispose()
                    End If

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
            objAdmin.UpdateModuleSetting(ModuleId, "link", cboInternalLink.SelectedItem.Value)
            If IsNumeric(txtWidth.Text) Then
                objAdmin.UpdateModuleSetting(ModuleId, "width", txtWidth.Text)
            End If
            If IsNumeric(txtHeight.Text) Then
                objAdmin.UpdateModuleSetting(ModuleId, "height", txtHeight.Text)
            End If
            objAdmin.UpdateModuleSetting(ModuleId, "show", optInfoBule.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "position", optPosition.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "secure", optSecure.Checked.ToString)
            If cboInternalGPS.SelectedIndex > 0 And optGoogleEarth.Checked Then
                objAdmin.UpdateModuleSetting(ModuleId, "optGoogleEarth", optGoogleEarth.Checked.ToString)
                objAdmin.UpdateModuleSetting(ModuleId, "fileGPS", cboInternalGPS.SelectedItem.Text)
            Else
                objAdmin.UpdateModuleSetting(ModuleId, "optGoogleEarth", "false")
                objAdmin.UpdateModuleSetting(ModuleId, "fileGPS", "")
            End If

            objAdmin.UpdateModuleSetting(ModuleId, "optInternalLink", optInternalLink.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "optExif", optExif.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "infobule", txtDescription.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "latlong", txtLatLong.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "Artist", txtArtist.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "Copyright", txtCopyright.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "Title", txtTitle.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "Exif", txtExif.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "ExtLink", ExternalLink.Text)
            objAdmin.UpdateModuleSetting(ModuleId, "InfoLink", InfoLink.Text)
            ' Redirect back to the portal home page
            HttpContext.Current.Session("UploadInfo") = Nothing
            ClearModuleCache(ModuleId)
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
            HttpContext.Current.Session("UploadInfo") = Nothing
            Response.Redirect(CType(ViewState("UrlReferrer"), String), True)
        End Sub

        Private Sub optExternal_CheckChanges(ByVal Sender As Object, ByVal e As EventArgs) Handles optExternal.CheckedChanged
            EnableControls()
            ClearSelection()
        End Sub

        Private Sub cboInternal_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cboInternal.SelectedIndexChanged
            ClearSelection()
            SetUpSelection(cboInternal.SelectedValue)
        End Sub

        Private Sub SetUpSelection(ByVal WhatTO As String)
            Dim settings As Hashtable
            Dim fileName As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            fileName = Server.MapPath(_portalSettings.UploadDirectory & cboInternal.SelectedItem.Text)
            txtAlt.Text = cboInternal.SelectedValue
            If File.Exists(fileName) Then
                Dim Exif As New ExifWorks(fileName)
                txtExif.Text = Exif.ToString()
                txtArtist.Text = Exif.Artist
                If Exif.Latitude(3) <> String.Empty Then
                    txtLatLong.Text = Exif.Latitude(3) + "," + Exif.Longitude(3)
                End If
                txtCopyright.Text = Exif.Copyright
                txtDescription.Text = Exif.Description
                txtTitle.Text = Exif.Title
                Exif.Dispose()
            End If
            If txtArtist.Text = "" Then
                Dim objUser As New UsersDB()
                Dim dr As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, Int32.Parse(Context.User.Identity.Name))

                ' Read first row from database
                If dr.Read() Then
                    txtArtist.Text = dr("FirstName").ToString
                    txtArtist.Text += " " & dr("LastName").ToString
                End If
                dr.Close()
            End If
            txtCopyright.Text = GetDomainName(Request)

        End Sub

        Private Sub ClearSelection()
            txtExif.Text = ""
            txtArtist.Text = ""
            txtLatLong.Text = ""
            txtCopyright.Text = ""
            txtTitle.Text = ""
            txtAlt.Text = ""
            txtWidth.Text = ""
            txtHeight.Text = ""
            txtDescription.Text = ""
            ExternalLink.Text = ""
            InfoLink.Text = ""
            txtLatLong.Text = ""
            optInfoBule.Checked = False
            optPosition.Checked = False
            optSecure.Checked = False
            optGoogleEarth.Checked = False
            optInternalLink.Checked = False
            optExif.Checked = False
            Dim objAdmin As New AdminDB()
            objAdmin.UpdateModuleSetting(ModuleId, "src", "")
            objAdmin.UpdateModuleSetting(ModuleId, "alt", "")
            objAdmin.UpdateModuleSetting(ModuleId, "fileGPS", "")
            objAdmin.UpdateModuleSetting(ModuleId, "link", "")
            objAdmin.UpdateModuleSetting(ModuleId, "width", "")
            objAdmin.UpdateModuleSetting(ModuleId, "height", "")
            objAdmin.UpdateModuleSetting(ModuleId, "show", optInfoBule.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "position", optPosition.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "secure", optSecure.Checked.ToString)

            objAdmin.UpdateModuleSetting(ModuleId, "optGoogleEarth", optGoogleEarth.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "optInternalLink", optInternalLink.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "optExif", optExif.Checked.ToString)
            objAdmin.UpdateModuleSetting(ModuleId, "infobule", "")
            objAdmin.UpdateModuleSetting(ModuleId, "latlong", "")
            objAdmin.UpdateModuleSetting(ModuleId, "Artist", "")
            objAdmin.UpdateModuleSetting(ModuleId, "Copyright", "")
            objAdmin.UpdateModuleSetting(ModuleId, "Title", "")
            objAdmin.UpdateModuleSetting(ModuleId, "Exif", "")
            objAdmin.UpdateModuleSetting(ModuleId, "ExtLink", "")
            objAdmin.UpdateModuleSetting(ModuleId, "InfoLink", "")
            ClearModuleCache(ModuleId)
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