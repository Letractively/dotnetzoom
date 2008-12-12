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

Imports System.Text.RegularExpressions

Namespace DotNetZoom

    Public MustInherit Class Search
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


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

        Protected WithEvents txtSearch As System.Web.UI.WebControls.TextBox
        Protected WithEvents cmdGo As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lstResults As System.Web.UI.WebControls.DataList
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
   			Title1.EditText = getlanguage("editer")
			Title1.DisplayHelp = "DisplayHelp_Search"
			cmdGo.Text = getlanguage("go")
        End Sub

        Private Sub cmdGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGo.Click
			' Obtain PortalSettings from Current Context
             Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objSearch As New SearchDB()

            Dim intMaxResults As Integer = -1
            If CType(Settings("maxresults"), String) <> "" Then
                intMaxResults = CType(Settings("maxresults"), Integer)
            End If

            
			Dim objSecurity As New PortalSecurity()
            lstResults.DataSource = objSearch.GetResults(_portalSettings.PortalId, ModuleId, objSecurity.InputFilter(txtSearch.Text, PortalSecurity.FilterFlag.NoSQL), intMaxResults)
            lstResults.DataBind()
			
        End Sub

		
        Private Function GetSearchTerms(ByVal ModuleId As string) As String
		Dim objSecurity As New PortalSecurity()
            Return "&h=" + objSecurity.InputFilter(txtSearch.Text, PortalSecurity.FilterFlag.NoSQL) + "&m=" + ModuleId
        End Function
		
        Function FormatResult(ByVal ModuleId As Object, ByVal TabId As Object, ByVal TabName As Object, ByVal ModuleTitle As Object, ByVal TitleField As Object, ByVal DescriptionField As Object, ByVal CreatedDateField As Object, ByVal CreatedByUserField As Object) As String

            Dim strResult As String
	    	Dim TempResult As String = ""

            strResult = "<hr noshade size=""1"">"
            strResult += "<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">"

            ' title
            strResult += "<tr><td width=""100%"">"
            strResult += "<a href=""" & FormatURL() & "?tabid=" & TabId.ToString & GetSearchTerms(ModuleId.ToString) & """ class=""NormalBold"">"
            
			If Not IsDBNull(TitleField) then
			TempResult = StripHTML(TitleField.ToString)
			end if
	    	If CType(Settings("maxtitle"), String) <> "" Then
                strResult += PutSearch(Left(TempResult, CType(Settings("maxtitle"), Integer)) & "...", txtSearch.Text)
            Else
                strResult += PutSearch(TempResult, txtSearch.Text)
            End If
            strResult += "</a>"
            strResult += "</td></tr>"

            If CType(Settings("showdescription"), String) <> "False" Then
                If Not IsDBNull(DescriptionField) Then
                    TempResult = StripHTML(DescriptionField.ToString)
                    strResult += "<tr><td width=""100%"">"
                    strResult += "<span class=""Normal"">"
                    if CType(Settings("maxdescription"), String) <> "" Then
					strResult += PutSearch(Left(TempResult, CType(Settings("maxdescription"), Integer)) & "...", txtSearch.Text)
                    Else
                    strResult += PutSearch(TempResult, txtSearch.Text)
                    End If
                    strResult += "</span>"
                    strResult += "</td></tr>"
                End If
            End If

            If CType(Settings("showaudit"), String) <> "False" Then
                If IsDBNull(CreatedDateField) = False And IsDBNull(CreatedByUserField) = False Then
                    strResult += "<tr><td width=""100%"">"
                    strResult += "<span class=""Normal"">" & DotNetZoom.GetLanguage("label_update_by") & " " & CreatedByUserField.ToString  & " " & GetLanguage("label_update_the") & " " & CreatedDateField.ToString & "</span>"
 	                strResult += "</td></tr>"
                End If
            End If

            If CType(Settings("showbreadcrumbs"), String) <> "False" Then
                strResult += "<tr><td width=""100%"">"
                strResult += GetBreadCrumbsRecursively(TabId)
                strResult += "&nbsp;<span class=""Normal"">></span>&nbsp;"
                strResult += "<a href=""" & FormatURL() & "?tabid=" & TabId.ToString  & GetSearchTerms(ModuleId.ToString) & """ class=""NormalBold"">" & ModuleTitle.ToString & "</a>"
                strResult += "</td></tr>"
            End If

            strResult += "</table>"

            Return strResult

        End Function

        Function StripHTML(ByVal objHTML As Object) As String

            Dim strOutput As String = ""

            If Not IsDBNull(objHTML) Then
                strOutput = objHTML.ToString

                ' create regular expression object
                Dim objRegExp1 As Regex = New Regex("\t|\r|\n|\r\n", RegexOptions.IgnoreCase)

                ' replace all HTML tag matches with the empty string
                strOutput = objRegExp1.Replace(strOutput, "")

                Dim objRegExp12 As Regex = New Regex("<script(.|\n)+?</script>", RegexOptions.IgnoreCase)

                ' replace all HTML tag matches with the empty string
                strOutput = objRegExp12.Replace(strOutput, "")				
				
                ' create regular expression object
                Dim objRegExp3 As Regex = New Regex("<(.|\n)+?>|&lt;(.|\n)+?&gt;", RegexOptions.IgnoreCase)

                ' replace all HTML tag matches with the empty string
                strOutput = objRegExp3.Replace(strOutput, "")


                strOutput = Server.HtmlDecode(strOutput)

            End If
            Return strOutput

	  End Function

        Function PutSearch(ByVal objHTML As Object, ByVal strSearch As String) As String

            Dim strOutput As String

            If Not IsDBNull(objHTML) Then
                strOutput = objHTML.ToString
	
                ' highlight search
                strOutput = Replace(strOutput, strSearch, "<b style=""background: #FF6347; color: black"">" & strSearch & "</b>", , , CompareMethod.Text)
                If Server.HTMLEncode(strSearch) <> strSearch then
		strOutput = Replace(strOutput, Server.HTMLEncode(strSearch), "<b style=""background: #00FF7F; color: black"">" & Server.HTMLEncode(strSearch) & "</b>", , , CompareMethod.Text)
		end if
		strOutPut = Server.HtmlDecode(strOutPut)
                Return strOutput
            End If			
			
        End Function

        Private Function GetBreadCrumbsRecursively(ByVal intTabId As Integer) As String
            Dim objAdmin As New AdminDB()
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Dim strBreadCrumbs As String = ""
            Dim dr As SqlDataReader = objAdmin.GetTabById(intTabId, GetLanguage("N"))
            While dr.Read
            strBreadCrumbs += "&nbsp;<span class=""Normal"">></span>&nbsp;<a href=""" & FormatURL() & "?tabid=" & intTabId.ToString & """ class=""NormalBold"">" & dr("TabName").ToString & "</a>"
                If Not IsDBNull(dr("ParentId")) Then
                    strBreadCrumbs = GetBreadCrumbsRecursively(dr("ParentId")) & strBreadCrumbs
                End If
            End While
            dr.Close()

            Return strBreadCrumbs

        End Function

        Function FormatURL() As String

            Dim ServerPath As String

            ServerPath = Request.ApplicationPath
            If Not ServerPath.EndsWith("/") Then
                ServerPath += "/"
            End If

            Return ServerPath & GetLanguage("N") & ".default.aspx"

        End Function


    End Class

End Namespace