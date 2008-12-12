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

Imports System.Xml
Imports System.Net
Imports System.IO

Namespace DotNetZoom
    Public MustInherit Class RssModule
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected xmlRSS As System.Web.UI.WebControls.Xml
        Protected lblMessage As System.Web.UI.WebControls.Label
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

        '*******************************************************
        '
        ' The Page_Load event handler on this User Control uses
        ' the Portal configuration system to obtain an xml document
        ' and xsl/t transform file location.  It then sets these
        ' properties on an <asp:Xml> server control.
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			 Title1.EditText = getlanguage("editer")
			
            Dim xmlsrc As String = CType(Settings("xmlsrc"), String)

            If xmlsrc <> "" Then

                If InStr(1, xmlsrc, "://") = 0 Then
                    xmlsrc = Request.Url.GetLeftPart(UriPartial.Authority) + _portalSettings.UploadDirectory & xmlsrc
                End If

                Try
                    Dim Password As String = CType(Settings("password"), String)
                    Dim UserAccount As String = Mid(CType(Settings("account"), String), InStr(CType(Settings("account"), String), "\") + 1)
                    Dim DomainName As String = ""
                    If InStr(Settings("account"), "\") <> 0 Then
                        DomainName = Left(CType(Settings("account"), String), InStr(CType(Settings("account"), String), "\") - 1)
                    End If
                    ' make remote request
                    Dim wr As HttpWebRequest = CType(WebRequest.Create(xmlsrc), HttpWebRequest)
                    If UserAccount <> "" Then
                        wr.Credentials = New NetworkCredential(UserAccount, Password, DomainName)
                    End If
                    ' set proxy server
                    If portalSettings.GetHostSettings("ProxyServer") <> "" Then
                        wr.Proxy = New WebProxy(portalSettings.GetHostSettings("ProxyServer"), Int32.Parse(portalSettings.GetHostSettings("ProxyPort")))
                    End If

                    ' set the HTTP properties
                    wr.Timeout = 10000 ' 10 seconds

                    ' read the response
                    Dim resp As WebResponse = wr.GetResponse()
                    Dim stream As stream = resp.GetResponseStream()

                    ' load XML document
                    Dim reader As XmlTextReader = New XmlTextReader(stream)
                    reader.XmlResolver = Nothing
                    Dim doc As XmlDocument = New XmlDocument()
                    doc.Load(reader)
                    xmlRSS.Document = doc
					
                Catch ex As System.Exception
                    ' connectivity issues
                    lblMessage.Text = "<center>"
                    If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Then
                        lblMessage.Text += GetLanguage("rss_noconnect_admin")
						lblMessage.Text = lblMessage.Text.Replace("{xmlsrc}", xmlsrc)
                    Else
                        lblMessage.Text += GetLanguage("rss_noconnect_error")
						lblMessage.Text = lblMessage.Text.Replace("{errormessage}", ex.Message)
                    End If
					
                    lblMessage.Text += "</center>"
                    lblMessage.Visible = True
                End Try

            End If			

		

            Try
                Dim xslsrc As String = CType(Settings("xslsrc"), String)
				Dim trans As System.Xml.Xsl.XslTransform = _
      			New System.Xml.Xsl.XslTransform

                If xslsrc <> "" Then
                    If InStr(1, xslsrc, "://") = 0 Then
                        xslsrc = _portalSettings.UploadDirectory & xslsrc
						trans.Load(Server.MapPath(xslsrc))
					else
					trans.Load(xslsrc)
                    End If
                Else ' default
                    xslsrc = "~/DesktopModules/News/RSS91.xsl"
					trans.Load(Server.MapPath(xslsrc))
                End If
				 xmlRSS.Transform = trans
				 
            Catch ex As System.Exception
			If PortalSecurity.IsInRoles(_portalSettings.AdministratorRoleId.ToString) = True Then
               lblMessage.Text += "<p><center>" & ex.Message & "</center></p>"
			end if
            End Try
			
        End Sub

    End Class

End Namespace