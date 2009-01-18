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

    Public Class SQL
        Inherits DotNetZoom.PortalModuleControl
		Protected WithEvents cmdBrowseScript As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents grdResults As System.Web.UI.WebControls.DataGrid
        Protected WithEvents txtQuery As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtConnectionString As System.Web.UI.WebControls.TextBox
		Protected WithEvents cmdExecute As System.Web.UI.WebControls.LinkButton
        Protected WithEvents SQLcmdExecute As System.Web.UI.WebControls.LinkButton
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' Verify that the current user has access to access this page
            If Not PortalSecurity.IsSuperUser Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If

            Title1.DisplayTitle = GetLanguage("title_sql")
            Title1.DisplayHelp = "DisplayHelp_SQL"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			cmdExecute.Text = GetLanguage("SQL_ExecuteCMD")
			SQLcmdExecute.Text = GetLanguage("SQL_Execute")

            Try
                SetFormFocus(txtQuery)
            Catch
                'control not there or error setting focus
            End Try

        End Sub

        Private Sub cmdExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExecute.Click

            If txtQuery.Text <> "" Then

                Dim objAdmin As New AdminDB()

                grdResults.DataSource = objAdmin.GetSQL(txtQuery.Text)
                grdResults.DataBind()

            End If

        End Sub
 
        Private Sub SQLcmdExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SQLcmdExecute.Click

		
		    Dim strFileName As String
            Dim strFileNamePath As String
            Dim strExtension As String = ""
			Dim strScript As String = ""

 	
            If Page.IsPostBack Then
                If cmdBrowseScript.PostedFile.FileName <> "" Then
				   Dim objStreamReader As StreamReader
				   strFileName = System.IO.Path.GetFileName(cmdBrowseScript.PostedFile.FileName)
                   strFileNamePath = Request.MapPath("~/database/") & strFileName
                    strExtension = ""
                    If InStr(1, strFileNamePath, ".") Then
                        strExtension = Mid(strFileNamePath, InStrRev(strFileNamePath, ".") + 1).ToLower
                    End If
				If strExtension <> "sql" then
				txtQuery.Text = GetLanguage("SQL_NeedExtension") 
				else
				Try
				txtQuery.Text = ""
                If File.Exists(strFileNamePath) Then
				   File.Delete(strFileNamePath)
				txtQuery.Text = txtQuery.Text & GetLanguage("OK")
                End If
				cmdBrowseScript.PostedFile.SaveAs(strFileNamePath)
				objStreamReader = File.OpenText(strFileNamePath)
                strScript = objStreamReader.ReadToEnd
                objStreamReader.Close()
				Catch
                ' save error - can happen if the security settings are incorrect
                txtQuery.Text = txtQuery.Text & GetLanguage("WriteError") & "<br>"
                End Try
				If strScript <> "" then
				Dim objAdmin As New AdminDB()
				If txtConnectionString.Text <> "" then
				txtQuery.Text = txtQuery.Text & objAdmin.ExecuteSQLScriptInAnotherDB(txtConnectionString.Text, strScript)
				else
                txtQuery.Text = txtQuery.Text & objAdmin.ExecuteSQLScript(strScript)
				end If 
				else
				txtQuery.Text = txtQuery.Text & GetLanguage("Nothing_To_Do")
				end if
				end If	
				else 
				txtQuery.Text = GetLanguage("SQL_NoFile")
				End If

            End If 
        End Sub

    End Class

End Namespace