Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO





Namespace DotNetZoom
	Public Class TAGFileUploadDialog
		Inherits System.Web.UI.Page
        Protected WithEvents CanUpload As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents Table1 As System.Web.UI.WebControls.Table
		Protected WithEvents Label1 As System.Web.UI.WebControls.Label
		Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
		Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
		Protected WithEvents htmlUploadFile As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
        Protected WithEvents chkUnzip As System.Web.UI.WebControls.CheckBox

		
#Region " Web Form Designer Generated Code "

		'This call is required by the Web Form Designer.
		<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

		End Sub

		Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
			'CODEGEN: This method call is required by the Web Form Designer
			'Do not modify it using the code editor.
			InitializeComponent()
			
			Dim SpaceUsed As Double

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Request.IsAuthenticated = False Then
       		 SendHttpException("404", "Not Found", Request)
            End If

			
            Dim objAdmin As New AdminDB()
            Dim strFolder As String = ""
            If Not (Request.Params("hostpage") Is Nothing) Then
                strFolder = Request.MapPath(glbSiteDirectory)
            Else
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
            End If


			SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)
            SpaceUsed = SpaceUsed / 1048576

			
            If (Request.Params("hostpage") Is Nothing) Then
                    If _portalSettings.HostSpace = 0 Then
                    lblMessage.Text = Replace(GetLanguage("Gal_PortalQuota"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
                Else
					lblMessage.Text = Replace(GetLanguage("Gal_PortalQuota1"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
					lblMessage.Text = Replace(lblMessage.Text, "{Quota}", Format(_portalSettings.HostSpace, "#,##0.00"))
					lblMessage.Text = Replace(lblMessage.Text, "{SpaceLeft}", Format(_portalSettings.HostSpace - SpaceUsed, "#,##0.00"))
                End If
            Else
                    lblMessage.Text = Replace(GetLanguage("Gal_PortalQuota"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
            End If
			lblMessage.Text = lblMessage.Text & "<br><br>"

			If (((SpaceUsed) <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Or (Not (Request.Params("hostpage") Is Nothing)) Then
			else
			CanUpload.Visible = False
			lblMessage.Text = lblMessage.Text & GetLanguage("Gal_PortalQuota2")
			end If        
		If Session("message") <> "" then
		lblMessage.Text += "<br>" + Session("message")
		Session("message") = ""
		end if
		End Sub

#End Region

		Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			'Put user code to initialize the page here
        If Page.IsPostBack = False Then
		        Label1.Text = GetLanguage("F_FileName")
				btnUpload.Text = GetLanguage("F_btnUpload")
				btnCancel.Text = GetLanguage("annuler")
                chkUnzip.Checked = True
                chkUnzip.Text = "<label for=""" & chkUnzip.ClientID & """>" & GetLanguage("UnZipFile") & "</label>"
		End if
  			btnCancel.Attributes.Add("onclick", "handleCancel();")
		End Sub

		Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
			Dim strFileName As String
			Dim strFileNamePath As String
			Dim strExtension As String
			' Rene Boulard validation de télécharger
			Dim StrFolder as String
			Dim RootFolder as String
			Dim SpaceUsed as Double			
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim objAdmin As New AdminDB()
			Session("message") = ""
			
			
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

			If ((((SpaceUsed + htmlUploadFile.PostedFile.ContentLength) / 1048576) <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Or (Not (Request.Params("hostpage") Is Nothing)) Then
  
			' only save the file if there is some space and the Extension is allowed
			' Gets the file name
			strFileName = System.IO.Path.GetFileName(htmlUploadFile.PostedFile.FileName).tolower()

			strFileNamePath = Session("RootDir") & Session("RelativeDir") & "\" & strFileName
            strExtension = ""
            If InStr(1, strFileNamePath, ".") Then
                     strExtension = Mid(strFileNamePath, InStrRev(strFileNamePath, ".") + 1).ToLower
            End If
            If InStr(1, "," & portalSettings.GetHostSettings("FileExtensions").ToString, "," & strExtension) <> 0 Or  Not (Request.Params("hostpage") Is Nothing) Then
			' Save Uploaded file to server
			
                  
                            If File.Exists(strFileNamePath) Then
							SpaceUsed = SpaceUsed - New FileInfo(strFileNamePath).Length
                            File.Delete(strFileNamePath)
                            End If
							htmlUploadFile.PostedFile.SaveAs(strFileNamePath)
							SpaceUsed = SpaceUsed + htmlUploadFile.PostedFile.ContentLength
                            If strExtension = "zip" And chkUnzip.Checked = True Then
                                ' save zip file name
                                Dim strSaveFileNamePath As String = strFileNamePath

                                Dim objZipEntry As ZipEntry
                                Dim objZipInputStream As New ZipInputStream(File.OpenRead(strFileNamePath))
                                objZipEntry = objZipInputStream.GetNextEntry
                                While Not objZipEntry Is Nothing
                                    If ((((SpaceUsed + objZipEntry.Size) / 1048576) <= _portalSettings.HostSpace) Or _portalSettings.HostSpace = 0) Or (Not (Request.Params("hostpage") Is Nothing)) Then
                                        strFileName = Path.GetFileName(objZipEntry.Name)

                                        If strFileName <> "" Then
                                                strFileNamePath = Session("RootDir") & Session("RelativeDir") & "\" & strFileName
                                            strExtension = ""
                                            If InStr(1, strFileNamePath, ".") Then
                                                strExtension = Mid(strFileNamePath, InStrRev(strFileNamePath, ".") + 1).ToLower
                                            End If

                                            If InStr(1, "," & portalSettings.GetHostSettings("FileExtensions").ToString, "," & strExtension) <> 0 Or  Not (Request.Params("hostpage") Is Nothing) Then
                                                If File.Exists(strFileNamePath) Then
												SpaceUsed = SpaceUsed - New FileInfo(strFileNamePath).Length
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
												If RootFolder = strFolder then
                                                AddFile(strFileNamePath, strExtension)
												end if

                                                objFileStream.Close()
                                              
												SpaceUsed = SpaceUsed + New FileInfo(strFileNamePath).Length
                                            Else
                                                ' restricted file type
                                            	Session("message")  += replace(GetLanguage("FileExtNotAllowed"), "{fileext}", strFileName)
												Session("message") = replace(Session("message"), "{allowedext}", " ( *." & Replace(portalSettings.GetHostSettings("FileExtensions").ToString, ",", ", *.") & " ).")
											End If
                                        End If
                                    Else ' file too large
                                        Session("message") += "<br>" & GetLanguage("Gal_NoSpaceLeft")
                                    End If

                                    objZipEntry = objZipInputStream.GetNextEntry
                                End While
                                objZipInputStream.Close()

                                ' delete the zip file
								SpaceUsed = SpaceUsed - New FileInfo(strFileNamePath).Length
                                File.Delete(strSaveFileNamePath)
							else
								If RootFolder = strFolder then
                        		   AddFile(strFileNamePath, strExtension, htmlUploadFile.PostedFile.ContentType)
								end if
                            End If
                    
                    Session("dt") = Nothing
					objAdmin.AddDirectory( strFolder, SpaceUsed)
			Else
            ' restricted file type
            Session("message")  += replace(GetLanguage("FileExtNotAllowed"), "{fileext}", strFileName)
			Session("message") = replace(Session("message"), "{allowedext}", " ( *." & Replace(portalSettings.GetHostSettings("FileExtensions").ToString, ",", ", *.") & " ).")
			End If
			else
			' file too large
            Session("message") += "<br>" & GetLanguage("Gal_NoSpaceLeft")
			End If
			RegisterClientScriptBlock("ClientScript", "<script language=""JavaScript"">handleOK()</Script>")
		End Sub
		
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
		
		
	End Class
End Namespace