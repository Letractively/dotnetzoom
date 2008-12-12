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

Namespace DotNetZoom

    Public MustInherit Class DesktopPortalFooter
        Inherits System.Web.UI.UserControl
        
        Protected WithEvents lblFooter As System.Web.UI.WebControls.Label
        Protected WithEvents hypHost As System.Web.UI.WebControls.HyperLink
        Protected WithEvents hypTerms As System.Web.UI.WebControls.HyperLink
        Protected WithEvents hypPrivacy As System.Web.UI.WebControls.HyperLink
		
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
			
			If portalSettings.GetSiteSettings(_portalSettings.PortalID).ContainsKey(GetLanguage("N") & "_FooterText") then
			lblFooter.Text = portalSettings.GetSiteSettings(_portalSettings.PortalID)(GetLanguage("N") & "_FooterText")
			if lblFooter.Text <> "" then
			lblFooter.Visible = True
			end if
			else
            If _portalSettings.FooterText <> "" Then
                lblFooter.Text = _portalSettings.FooterText
				lblFooter.Visible = True
            Else
                lblFooter.Text = "Copyright (c) " & Year(Now()) & " " & _portalSettings.PortalName
				lblFooter.Visible = True
            End If
			End If
			
            hypHost.Text = replace(GetLanguage("hypHost"), "{hostname}",  portalSettings.GetHostSettings("HostTitle"))
            hypHost.NavigateUrl = AddHTTP(portalSettings.GetHostSettings("HostURL"))
            hypTerms.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Terms"
            hypTerms.Text = GetLanguage("hypTerms")
			hypPrivacy.NavigateUrl = "~" & GetDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&def=Privacy"
			hypPrivacy.Text = GetLanguage("hypPrivacy")
			
		' modification par rene boulard pour enlever le id tag
        lblFooter.ID = ""
		hypHost.ID = ""
		hypTerms.ID = ""
		hypPrivacy.ID = ""
		
		End Sub

		
   End Class

End Namespace