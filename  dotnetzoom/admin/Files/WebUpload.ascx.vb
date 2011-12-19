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

Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO

Namespace DotNetZoom

    Public Class WebUpload
        Inherits DotNetZoom.PortalModuleControl

        Protected WithEvents cmdSynchronize As System.Web.UI.WebControls.LinkButton
        Protected WithEvents chkUploadRoles As System.Web.UI.WebControls.CheckBoxList
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents options As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents Upload As System.Web.UI.WebControls.PlaceHolder
    	Protected WithEvents cmdCancelOptions As System.Web.UI.WebControls.LinkButton		
		
        Protected WithEvents CanUpload As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents cmdBrowse As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents lstFiles As System.Web.UI.WebControls.ListBox
        Protected WithEvents cmdAdd As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdRemove As System.Web.UI.WebControls.LinkButton
        Protected WithEvents chkUnzip As System.Web.UI.WebControls.CheckBox
        
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
		Protected WithEvents lblRootDir As System.Web.UI.WebControls.Label
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
        Public Shared arrFiles As ArrayList = New ArrayList()
		
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
			
			Dim SpaceUsed As Double
			Dim strFolder As STring 

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            If Not (Request.Params("hostpage") Is Nothing) Then
                strFolder = Request.MapPath(glbSiteDirectory)
            Else
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
            End If

			SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)
            SpaceUsed = SpaceUsed / 1048576
			
			If (((SpaceUsed) <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Or (Not (Request.Params("hostpage") Is Nothing)) Then
			else
			CanUpload.Visible = False
			lblMessage.Text = GetLanguage("Gal_PortalQuota2")
			end If        
		
        End Sub

#End Region

        '*******************************************************
        '
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Title1.DisplayHelp = "DisplayHelp_WebUpload"
			cmdCancelOptions.Text = GetLanguage("annuler")
		    cmdSynchronize.Text = GetLanguage("F_cmdSynchronize")
			cmdUpdate.Text = GetLanguage("F_Update")


            Dim objAdmin As New AdminDB()

            Dim tmpUploadRoles As String = ""
            If Not CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("uploadroles"), String) Is Nothing Then
                tmpUploadRoles = CType( portalSettings.GetSiteSettings(_portalSettings.PortalID)("uploadroles"), String)
            End If

			          
            If Request.IsAuthenticated = False Then
                EditDenied()
            End If

            If PortalSecurity.IsInRoles(tmpUploadRoles) = False Then
                CanUpload.Visible = False
                lblMessage.Text = ProcessLanguage(objAdmin.GetSinglelonglanguageSettings(GetLanguage("N"), "Security_CannotUpload"), Page)
            End If



            If Page.IsPostBack = False Then
                ' Store URL Referrer to return to portal
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferrer") = Request.UrlReferrer.ToString()
                Else
                    ViewState("UrlReferrer") = FormatFriendlyURL(_portalSettings.ActiveTab.FriendlyTabName, _portalSettings.ActiveTab.ssl, _portalSettings.ActiveTab.ShowFriendly, _portalSettings.ActiveTab.TabId.ToString)
                End If


                lstFiles.Visible = False
                If Request.Params("options") <> "" Then

                    If Not PortalSecurity.IsInRole(_portalSettings.AdministratorRoleId.ToString) Or Not IsAdminTab() Then
                        'Autorisation
                        EditDenied()
                    End If
                    options.Visible = True
                    Upload.Visible = False
                    chkUploadRoles.Items.Clear()
                    Dim settings As Hashtable = PortalSettings.GetModuleSettings(ModuleId)
                    Dim UploadRoles As String = ""
                    If Not CType(settings("uploadroles"), String) Is Nothing Then
                        UploadRoles = CType(settings("uploadroles"), String)
                    End If

                    Dim objUser As New UsersDB()

                    Dim dr As SqlDataReader = objUser.GetPortalRoles(_portalSettings.PortalId, GetLanguage("N"))
                    While dr.Read()
                        Dim item As New ListItem()
                        item.Text = CType(dr("RoleName"), String)
                        item.Value = dr("RoleID").ToString()
                        If InStr(1, UploadRoles, item.Value & ";") Or item.Value = _portalSettings.AdministratorRoleId.ToString Then
                            item.Selected = True
                        End If

                        chkUploadRoles.Items.Add(item)

                    End While

                    dr.Close()
                Else
                    Upload.Visible = True
                    options.Visible = False
                End If

                ' initialize file list
                arrFiles.Clear()
                chkUnzip.Checked = True
                chkUnzip.Text = "<label for=""" & chkUnzip.ClientID & """>" & GetLanguage("UnZipFile") & "</label>"
                cmdAdd.Text = GetLanguage("upload")
                cmdRemove.Text = GetLanguage("cmdRemove")
                cmdRemove.Visible = False
                chkUnzip.Text = GetLanguage("F_UnzipFile")
                cmdCancel.Text = GetLanguage("return")
                If Session("RootDir") <> "" Then
                    lblRootDir.Text = Session("RootDir") & Session("RelativeDir")
                    lblRootDir.Text = Replace(lblRootDir.Text, GetAbsoluteServerPath(Request), "")
                    lblRootDir.Text = "/" + Replace(lblRootDir.Text, "\", "/")
                End If
            End If

        End Sub

        Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
            ' Obtain PortalSettings from Current Context
            ' Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			
            If Page.IsPostBack Then
               If cmdBrowse.PostedFile.FileName <> "" Then
            Dim strFileName As String
            Dim strFileNamePath As String
            Dim strExtension As String = ""
            lblMessage.Text = ""
			' Rene Boulard validation de télécharger
			Dim StrFolder as String
			Dim RootFolder As String
			Dim SpaceUsed as Double			
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()

			

			
            If Not (Request.Params("hostpage") Is Nothing) Then
                strFolder = Request.MapPath(glbSiteDirectory)
            Else
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
            End If

			If Session("RootDir") <> "" then
			RootFolder = Session("RootDir") & Session("RelativeDir") & "\" 
			else
			RootFolder = strFolder
			end if
			
			SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)

                'Gets the file name
                strFileName = System.IO.Path.GetFileName(cmdBrowse.PostedFile.FileName).Tolower()

                If ((((SpaceUsed + cmdBrowse.PostedFile.ContentLength) / 1048576) <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Or (Not (Request.Params("hostpage") Is Nothing)) Then

                    
                     strFileNamePath = RootFolder & strFileName

                    strExtension = ""
                    If InStr(1, strFileNamePath, ".") Then
                        strExtension = Mid(strFileNamePath, InStrRev(strFileNamePath, ".") + 1).ToLower
                    End If

                    If InStr(1, "," & portalSettings.GetHostSettings("FileExtensions").ToString, "," & strExtension) <> 0 Or Not (Request.Params("hostpage") Is Nothing) Then
                        'Save Uploaded file to server
                        Try
                            If File.Exists(strFileNamePath) Then
							SpacedUsed = SpaceUsed - New FileInfo(strFileNamePath).Length
                            File.Delete(strFileNamePath)
                            End If
                            cmdBrowse.PostedFile.SaveAs(strFileNamePath)
							SpaceUsed = SpaceUsed + cmdBrowse.PostedFile.ContentLength
						If strExtension = "zip" And chkUnzip.Checked = True Then
						SpaceUsed = UnZipFile(strFileNamePath ,  RootFolder, StrFolder, SpaceUsed) 
                        Else
						lstFiles.Items.Add(strFileName)
						cmdRemove.Visible = True
						lstFiles.Visible = True
					    If RootFolder = strFolder then
                        AddFile(strFileNamePath, strExtension, cmdBrowse.PostedFile.ContentType)
						end if
                    End If
						
						
                        Catch
                            ' save error - can happen if the security settings are incorrect
                            lblMessage.Text += "<br>" & GetLanguage("WriteError")
                        End Try
                    Else
                    ' restricted file type
		            lblMessage.Text += "<br>" & replace(GetLanguage("FileExtNotAllowed"), "{fileext}", strFileName)
					lblMessage.Text = replace(lblMessage.Text, "{allowedext}", " ( *." & Replace(portalSettings.GetHostSettings("FileExtensions").ToString, ",", ", *.") & " ).")
                    End If
                Else ' file too large
				lblMessage.Text += "<br>" & GetLanguage("Gal_NoSpaceLeft")
                End If

			Session("dt") = Nothing
			objAdmin.AddDirectory( strFolder, SpaceUsed )
            End If
		 end if
        End Sub

        Private Sub cmdRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
			Dim StrFolder as String
			Dim RootFolder As String
			Dim strFileNamePath As String
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		
            If Not (Request.Params("hostpage") Is Nothing) Then
                strFolder = Request.MapPath(glbSiteDirectory)
            Else
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
            End If

			If Session("RootDir") <> "" then
			RootFolder = Session("RootDir") & Session("RelativeDir") & "\" 
			else
			RootFolder = strFolder
			end if
			strFileNamePath = RootFolder & lstFiles.SelectedItem.Text
            If File.Exists(strFileNamePath) Then
               File.Delete(strFileNamePath)
            End If


            If lstFiles.Items.Count <> 0 Then
                lstFiles.Items.Remove(lstFiles.SelectedItem.Text)
				If lstFiles.Items.Count = 0 then
					cmdRemove.Visible = False
					lstFiles.Visible = False
				end if
            End If
        End Sub


		Private Function UnZipFile(ByVal strFileNamePath As STring, ByVal RootFolder As String, ByVal StrFolder As String, ByVal SpaceUsed as Double) As Double
		
            Dim strExtension As String = ""
           
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		                  		
                                ' save zip file name
                                Dim strSaveFileNamePath As String = strFileNamePath

                                Dim objZipEntry As ZipEntry
								Dim objZipInputStream As New ZipInputStream(File.OpenRead(strFileNamePath))
                                objZipEntry = objZipInputStream.GetNextEntry
                                While Not objZipEntry Is Nothing
                                    If ((((SpaceUsed + objZipEntry.Size) / 1048576) <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Or (Not (Request.Params("hostpage") Is Nothing)) Then
                                        strFileName = Path.GetFileName(objZipEntry.Name)
										lstFiles.Items.Add(strFileName)
										cmdRemove.Visible = True
										lstFiles.Visible = True

                                        If strFileName <> "" Then
                                            strFileNamePath = RootFolder & strFileName
                                            
                                            strExtension = ""
                                            If InStr(1, strFileNamePath, ".") Then
                                                strExtension = Mid(strFileNamePath, InStrRev(strFileNamePath, ".") + 1).ToLower
                                            End If

                                            If InStr(1, "," & portalSettings.GetHostSettings("FileExtensions").ToString, "," & strExtension) <> 0 Or  Not (Request.Params("hostpage") Is Nothing) Then
                                                If File.Exists(strFileNamePath) Then
												SpacedUsed = SpaceUsed - New FileInfo(strFileNamePath).Length
                                                File.Delete(strFileNamePath)
												End If
                                                Dim objFileStream As FileStream = File.Create(strFileNamePath)

                                                Dim intSize As Integer = 2048
                                                Dim arrData(2048) As Byte

                                                intSize = objZipInputStream.Read(arrData, 0, arrData.Length)
                                                While intSize > 0
                                                    objFileStream.Write(arrData, 0, intSize)
                                                    intSize = objZipInputStream.Read(arrData, 0, arrData.Length)
                                                End While

                                                objFileStream.Close()
												If RootFolder = strFolder then
                                                AddFile(strFileNamePath, strExtension)
												end if
												SpaceUsed = SpaceUsed + New FileInfo(strFileNamePath).Length
                                            Else
                                                ' restricted file type
								           lblMessage.Text += "<br>" & replace(GetLanguage("FileExtNotAllowed"), "{fileext}", strFileName)
											lblMessage.Text = replace(lblMessage.Text, "{allowedext}", " ( *." & Replace(portalSettings.GetHostSettings("FileExtensions").ToString, ",", ", *.") & " ).")
                                            End If
                                        End If
                                    Else ' file too large
									lblMessage.Text += "<br>" & GetLanguage("Gal_NoSpaceLeft")
                                    End If

                                    objZipEntry = objZipInputStream.GetNextEntry
                                End While
                                objZipInputStream.Close()

                                ' delete the zip file
								SpacedUsed = SpaceUsed - New FileInfo(strFileNamePath).Length
                                File.Delete(strSaveFileNamePath)
								Return SpaceUsed
		
		End Function
		
        Private Sub AddFile(ByVal strFileNamePath As String, ByVal strExtension As String, Optional ByVal strContentType As String = "")

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim strFileName As String = Path.GetFileName(strFileNamePath)

            If strContentType = "" Then
                Select Case strExtension.ToLower()
                    Case "txt" : strContentType = "text/plain"
                    Case "htm", "html" : strContentType = "text/html"
                    Case "rtf" : strContentType = "text/richtext"
                    Case "jpg", "jpeg" : strContentType = "image/jpeg"
                    Case "gif" : strContentType = "image/gif"
                    Case "bmp" : strContentType = "image/bmp"
                    Case "mpg", "mpeg" : strContentType = "video/mpeg"
                    Case "avi" : strContentType = "video/avi"
                    Case "pdf" : strContentType = "application/pdf"
                    Case "doc", "dot" : strContentType = "application/msword"
                    Case "csv", "xls", "xlt" : strContentType = "application/x-msexcel"
                    Case Else : strContentType = "application/octet-stream"
                End Select
            End If

            Dim imgImage As System.Drawing.Image
            Dim strWidth As String = ""
            Dim strHeight As String = ""

            If InStr(1, glbImageFileTypes, strExtension) Then
                Try
                    imgImage = System.Drawing.Image.FromFile(strFileNamePath)
                    strHeight = imgImage.Height
                    strWidth = imgImage.Width
                    imgImage.Dispose()
                Catch
                    ' error loading image file
                    strContentType = "application/octet-stream"
                End Try
            End If

            Dim objAdmin As New AdminDB()

            If Not (Request.Params("hostpage") Is Nothing) Then
                objAdmin.AddFile(-1, strFileName, strExtension, New FileInfo(strFileNamePath).Length, strWidth, strHeight, strContentType)

            Else
                objAdmin.AddFile(_portalSettings.PortalId, strFileName, strExtension, New FileInfo(strFileNamePath).Length, strWidth, strHeight, strContentType)
            End If

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)
        End Sub

		
        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            Dim admin As New AdminDB()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim item As ListItem

            ' Construct Authorized View Roles
            Dim UploadRoles As String = ""
            For Each item In chkUploadRoles.Items
                If item.Selected Then
                    UploadRoles = UploadRoles & item.Value & ";"
                End If
            Next item
            If UploadRoles <> "" Then
                If InStr(1, UploadRoles, _portalSettings.AdministratorRoleId.ToString & ";") = 0 Then
                    UploadRoles += _portalSettings.AdministratorRoleId.ToString & ";"
                End If
            End If

            admin.UpdatePortalSetting(_PortalSettings.PortalID, "uploadroles", UploadRoles)
			
        End Sub

        Private Sub cmdSynchronize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSynchronize.Click
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()

            If Not (Request.Params("hostpage") Is Nothing) Then
                objAdmin.SynchronizeFiles(-1, Request.MapPath(glbSiteDirectory))
            Else
                objAdmin.SynchronizeFiles(_portalSettings.PortalId, Request.MapPath(_portalSettings.UploadDirectory))
            End If

        End Sub
	
	
        Private Sub cmdCancelOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelOptions.Click
			  Response.Redirect(CType(Viewstate("UrlReferrer"), String), True)        
        End Sub			
		
		
    End Class

End Namespace