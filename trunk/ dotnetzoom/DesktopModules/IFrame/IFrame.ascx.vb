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

Namespace DotNetZoom

    Public MustInherit Class IFrame
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents lblIFrame As System.Web.UI.WebControls.Label
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
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

			

   			Title1.EditText = getlanguage("editer")
            Dim strSrc As String = CType(Settings("src"), String)
            Dim strHeight As String = CType(Settings("height"), String)
            Dim strWidth As String = CType(Settings("width"), String)
            Dim strScrolling As String = CType(Settings("scrolling"), String)
            Dim strBorder As String = CType(Settings("border"), String)
            Dim strTitle as String = CType(Settings("title"), String)
			If strBorder = "oui" then 
			strBoder = "1" 
			else 
			StrBorder = "0" 
			end If
            If strSrc <> "" Then
                lblIFrame.Text = "<iframe frameborder=""" & strBorder & """ src=""" & strSrc & """ height=""" & strHeight & """ width=""" & strWidth & """ title=""" & strTitle &  """ scrolling=""" & strScrolling & """>" & GetLanguage("browser_non_compatible") & "</iframe>"
            End If

        End Sub

    End Class

End Namespace