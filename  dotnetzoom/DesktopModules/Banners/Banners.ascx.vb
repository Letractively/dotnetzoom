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

    Public MustInherit Class Banners
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal

        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
        Protected WithEvents lstBanners As System.Web.UI.WebControls.DataList
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
        ' The Page_Load event handler on this User Control is used to
        ' obtain a DataReader of banner information from the Banners
        ' table, and then databind the results to a templated DataList
        ' server control.  It uses the DotNetZoom.BannerDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************'

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim intBannerTypeId As Integer
            Dim intBanners As Integer = 1
			Title1.EditText = GetLanguage("editer")

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objVendor As New VendorsDB()

			

			
            If CType(Settings("bannertype"), String) <> "" Then
                intBannerTypeId = Int32.Parse(CType(Settings("bannertype"), String))
            Else
                intBannerTypeId = -1
            End If

            If CType(Settings("bannercount"), String) <> "" Then
                intBanners = Int32.Parse(CType(Settings("bannercount"), String))
            End If
			
            Dim dr As SqlDataReader
            If CType(Settings("bannersource"), String) = "L" Then
                dr = objVendor.FindBanners(_portalSettings.PortalId, intBannerTypeId, _portalSettings.PortalId, intBanners)
            Else
                dr = objVendor.FindBanners(_portalSettings.PortalId, intBannerTypeId, , intBanners)
            End If
            lstBanners.DataSource = dr
            lstBanners.DataBind()
            dr.Close()


            If lstBanners.Items.Count = 0 Then
                lstBanners.Visible = False
            End If
			lstBanners.id = ""
        End Sub

        Function FormatImagePath(ByVal ImageFile As String) As String

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If InStr(1, ImageFile, "://") = 0 Then
                If CType(Settings("bannersource"), String) = "L" Then
                    ImageFile = _portalSettings.UploadDirectory & ImageFile
                Else
                    ImageFile = glbSiteDirectory & ImageFile
                End If
            End If

            Return ImageFile

        End Function

    End Class

End Namespace