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

    Public MustInherit Class HtmlModule
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal

        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents HtmlHolder As System.Web.UI.WebControls.Literal
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
        ' The Page_Load event handler on this User Control is
        ' used to render a block of HTML or text to the page.  
        ' The text/HTML to render is stored in the HtmlText 
        ' database table.  This method uses the DotNetZoom.HtmlTextDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

 			
			Dim _language As HashTable = HttpContext.Current.Items("Language")
			Title1.EditText = _Language("editer")

            ' Obtain the selected item from the HtmlText table
			
			' Check to see if available in Cache
			Dim TempKey as String = GetDBname & "ModuleID_" & CStr(ModuleId)
			Dim context As HttpContext = HttpContext.Current
			Dim content as String = Context.Cache(TempKey)
            If content Is Nothing Then
            ' If this object has not been instantiated yet, we need to grab it
             	Dim objHTML As New HtmlTextDB()
            	Dim dr As SqlDataReader = objHTML.GetHtmlText(ModuleId)
	            If dr.Read() Then
                    ' Dynamically add the file content into the page
                    content = Server.HtmlDecode(CType(dr("DesktopHtml"), String))
                    content = Replace(content, "[GUID]", _portalSettings.UploadDirectory)

		        End If
            ' Close the datareader
            dr.Close()
           
		   If Content <> "" then
            Context.Cache.Insert(TempKey, content, CDp(_PortalSettings.PortalID, _PortalSettings.ActiveTab.Tabid, ModuleID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, nothing)
		   End If
		 End if
		if not content is nothing then
		HtmlHolder.Text = content.ToString()
		end if
        End Sub



    End Class

End Namespace