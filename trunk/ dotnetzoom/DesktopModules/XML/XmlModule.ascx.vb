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

    Public MustInherit Class XmlModule
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents xmlContent As System.Web.UI.WebControls.Xml
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
			
			Try
				Dim doc As System.Xml.XmlDocument = New System.Xml.XmlDocument()
                If InStr(1, xmlsrc, "://") = 0 Then
                    xmlsrc = _portalSettings.UploadDirectory & xmlsrc
					doc.Load(Server.MapPath(xmlsrc))
				else
				doc.Load(xmlsrc)
                End If

                xmlContent.Document = doc
			Catch ex As System.Exception
			End Try
            End If

            Dim xslsrc As String = CType(Settings("xslsrc"), String)
			
            If xslsrc <> "" Then
			Try
				Dim trans As System.Xml.Xsl.XslTransform = New System.Xml.Xsl.XslTransform
                If InStr(1, xslsrc, "://") = 0 Then
                    xslsrc = _portalSettings.UploadDirectory & xslsrc
					trans.Load(Server.MapPath(xslsrc))
				else 
				trans.Load(xslsrc)
                End If
                xmlContent.Transform = trans 
			Catch ex As System.Exception
			End Try
            End If
        End Sub

    End Class

	
End Namespace