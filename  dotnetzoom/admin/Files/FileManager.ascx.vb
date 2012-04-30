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

    Public MustInherit Class FileManager
        Inherits DotNetZoom.PortalModuleControl

    
        Protected WithEvents lblDiskSpace As System.Web.UI.WebControls.Label
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
        ' The Page_Load server event handler on this user control is used
        ' to populate the current roles settings from the configuration system
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Title1.EditText = GetLanguage("upload")
            Title1.OptionsText = GetLanguage("F_cmdSynchronize") + "<br>" + GetLanguage("F_Update")
            Title1.EditIMG = "<img  src=""" & glbPath & "admin/advfileManager/images/upload.gif"" alt=""*"" style=""border-width:0px;"">"
            Title1.DisplayHelp = "DisplayHelp_FileManager"
            BindData()
        End Sub

        '*******************************************************
        '
        ' The BindData helper method is used to bind the list of
        ' users for this portal to an asp:DropDownList server control
        '
        '*******************************************************

        Sub BindData()

            Dim SpaceUsed As Double

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim objAdmin As New AdminDB()
            Dim strFolder As String

            If Not (Request.Params("hostpage") Is Nothing) Then
                strFolder = Request.MapPath(glbSiteDirectory)
            Else
                strFolder = Request.MapPath(_portalSettings.UploadDirectory)
            End If
			
			
			SpaceUsed = objAdmin.GetDirectorySpaceUsed(strFolder)
            SpaceUsed = SpaceUsed / 1048576
			
            If (Request.Params("hostpage") Is Nothing) Then
                    If _portalSettings.HostSpace = 0 Then
                    lblDiskSpace.Text = Replace(GetLanguage("Gal_PortalQuota"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
                Else
					lblDiskSpace.Text = Replace(GetLanguage("Gal_PortalQuota1"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
					lblDiskSpace.Text = Replace(lblDiskSpace.Text, "{Quota}", Format(_portalSettings.HostSpace, "#,##0.00"))
					lblDiskSpace.Text = Replace(lblDiskSpace.Text, "{SpaceLeft}", Format(_portalSettings.HostSpace - SpaceUsed, "#,##0.00"))
                End If
            Else
                    lblDiskSpace.Text = Replace(GetLanguage("Gal_PortalQuota"), "{SpaceUsed}", Format(SpaceUsed, "#,##0.00"))
            End If
        End Sub
    End Class

End Namespace