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

Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Namespace DotNetZoom

    Public MustInherit Class MapGoogle
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents lblLocation As System.Web.UI.WebControls.Label
        Protected WithEvents lblAddress As System.Web.UI.WebControls.Label
        Protected WithEvents cboSize As System.Web.UI.WebControls.DropDownList
        Protected WithEvents colSize As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents hypDirections As System.Web.UI.WebControls.HyperLink
		Protected WithEvents hypmap As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cboZoom As System.Web.UI.WebControls.DropDownList
        Protected WithEvents colZoom As System.Web.UI.HtmlControls.HtmlTableCell
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents hypMapImage As System.Web.UI.WebControls.Literal
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
			
			cboSize.Items.FindByValue("width: 250px; height: 150px").Text = GetLanguage("MapGoogle_small")
			cboSize.Items.FindByValue("width: 500px; height: 300px").Text = GetLanguage("MapGoogle_big")

            If Not Page.IsPostBack Then

                If CType(Settings("location"), String) <> "" Then
                    lblLocation.Text = CType(Settings("location"), String)
                    lblAddress.Text = FormatAddress(CType(Settings("unit"), String), CType(Settings("street"), String), CType(Settings("city"), String), CType(Settings("region"), String), CType(Settings("country"), String), CType(Settings("postalcode"), String))
                    cboSize.Items.FindByValue("width: 250px; height: 150px").Selected = True
                    cboZoom.Items.FindByValue("16").Selected = True
                    BuildMapImage()
                    hypDirections.Text = GetLanguage("MapGoogle_directions") 
					hypDirections.Text = GetLanguage("MapGoogle_directions_tooltip")
                    hypDirections.NavigateUrl = CType(Settings("directionURL"), String)
                End If

            End If

        End Sub



        Private Sub BuildMapImage()

            Dim blnDisplayMap As Boolean = False


            If CType(Settings("displaymap"), String) <> "" Then
                blnDisplayMap = CType(Settings("displaymap"), Boolean)
            End If
            If blnDisplayMap Then
                hypMapImage.Text = CType(settings("script"), String)
                hypMapImage.Visible = True
				hypmap.Visible = False
            Else
			    hypMap.Visible = True
                hypMap.Text = GetLanguage("MapGoogle_show_map")
				hypMap.tooltip =  GetLanguage("MapGoogle_show_tooltip")
            End If


            If hypMapImage.text = "" Then
                colSize.Visible = False
                colZoom.Visible = False
                hypMapImage.Visible = False
            Else
                colSize.Visible = True
                colZoom.Visible = True
				' ), 13);   zoom to change
				' width: 500px; height: 300px Size to change
				
				hypMapImage.Text = replace(hypMapImage.Text, "width: 400px; height: 200px" , cboSize.SelectedItem.Value)
				hypMapImage.Text = replace(hypMapImage.Text, "), 15);" , "),  " & cboZoom.SelectedItem.Value & ");")
                hypMapImage.Visible = True
            End If

        End Sub
 
        Private Sub hypMap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles hypMap.Click
		BuildMapImage()
		hypMapImage.Visible = True
		hypMap.Visible = False
		hypMapImage.Text = CType(settings("script"), String)
        End Sub


        Private Sub cboSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSize.SelectedIndexChanged
            BuildMapImage()
        End Sub

        Private Sub cboZoom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboZoom.SelectedIndexChanged
            BuildMapImage()
        End Sub

    End Class

End Namespace