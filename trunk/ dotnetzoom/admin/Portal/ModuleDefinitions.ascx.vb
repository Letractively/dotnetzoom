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
Imports System.IO

Namespace DotNetZoom

    Public Class ModuleDefinitions
        Inherits DotNetZoom.PortalModuleControl

    	Protected WithEvents MyHtmlImage As System.Web.UI.WebControls.Image
		Protected WithEvents txticone As System.Web.UI.WebControls.TextBox
		Protected WithEvents lnkicone As System.Web.UI.WebControls.hyperlink

		Protected WithEvents lblModuleDef As System.Web.UI.WebControls.Label
        Protected WithEvents tabAddModule As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents cboModule As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdUpload As System.Web.UI.WebControls.HyperLink

        Protected WithEvents tabEditModule As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents txtFriendlyName As System.Web.UI.WebControls.TextBox
		Protected WithEvents valFriendlyName As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtDesktopSrc As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtEditSrc As System.Web.UI.WebControls.TextBox
		Protected WithEvents txtHelpSrc As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkPremium As System.Web.UI.WebControls.CheckBox

        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents cmdDelete As System.Web.UI.WebControls.LinkButton
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle

        Private defId As Integer = -1

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
        ' The Page_Load server event handler on this page is used
        ' to populate the role information for the page
        '
        '*******************************************************

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
 			 Title1.DisplayHelp = "DisplayHelp_ModuleDefinitions"
            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			lnkicone.Tooltip = GetLanguage("select_icone_tooltip")	
			lnkicone.Text = GetLanguage("select_icone")	

            Dim Admin As new AdminDB()
			lblModuleDef.Text = Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "ModuleDefinitionInfo")

			cmdCancel.Text = getlanguage("annuler")
			cmdDelete.Text = getlanguage("delete")
			cmdUpload.Text = GetLanguage("upload")

            ' Verify that the current user has access to this page
            If Not PortalSecurity.IsSuperUser Then
                Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & TabId & "&def=Edit Access Denied", True)
            End If

            If IsAdminTab() And Not (HttpContext.Current.Request.Params("defid") Is Nothing) Then
                If IsNumeric(Request.Params("defId")) Then
                    defId = Int32.Parse(Request.Params("defId"))
                End If
            End If
            valFriendlyName.ErrorMessage = GetLanguage("need_module_def")
            If Page.IsPostBack = False Then

                cmdUpdate.Text = GetLanguage("enregistrer")

                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & RTESafe(GetLanguage("request_confirm")) & "');")

                cmdUpload.NavigateUrl = GetFullDocument() & "?edit=control&hostpage=&tabid=" & TabId & "&def=Gestion fichiers"

                If defId = -1 Then

                    tabAddModule.Visible = True
                    tabEditModule.Visible = False
                    cmdUpdate.Text = GetLanguage("install")
                    cmdDelete.Visible = False

                    ' load the modules to install
                    Dim strFolder As String = Request.MapPath(glbSiteDirectory)
                    If Directory.Exists(strFolder) Then
                        Dim fileEntries As String() = Directory.GetFiles(strFolder, "*.xml")
                        Dim strModule As String
                        Dim strFileName As String
                        For Each strFileName In fileEntries
                            strModule = Mid(strFileName, InStrRev(strFileName, "\") + 1)
                            strModule = Left(strModule, InStr(1, strModule, ".xml") - 1)
                            cboModule.Items.Add(New ListItem(strModule, strFileName))
                        Next
                    End If

                Else

                    tabAddModule.Visible = False
                    tabEditModule.Visible = True

                    ' Obtain the module definition to edit from the database
                    Dim objAdmin As New AdminDB()

                    Dim dr As SqlDataReader = objAdmin.GetSingleModuleDefinition(GetLanguage("N"), defId)

                    If dr.Read() Then
                        txtFriendlyName.Text = dr("FriendlyName").ToString
                        txtDesktopSrc.Text = dr("DesktopSrc").ToString
                        txtEditSrc.Text = dr("EditSrc").ToString
                        txtHelpSrc.Text = dr("HelpSrc").ToString
                        txticone.Text = IIf(IsDBNull(dr("EditModuleIcon")), "", dr("EditModuleIcon"))
                        chkPremium.Checked = dr("IsPremium")
                        txtDescription.Text = dr("Description").ToString
                        ViewState("FriendLyName") = dr("Name").ToString
                    End If

                    dr.Close()

                End If


            End If
            If txticone.Text <> "" Then
                Dim ImageURL As String
                ImageURL = "http://" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & glbSiteDirectory()
                If Not ImageURL.EndsWith("/") Then
                    ImageURL += "/"
                End If
                MyHtmlImage.ImageUrl = ImageURL & txticone.Text
                MyHtmlImage.AlternateText = txticone.Text
                MyHtmlImage.ToolTip = txticone.Text
                MyHtmlImage.Visible = True
            Else
                MyHtmlImage.Visible = False
            End If
            Dim ParentID As String = Server.HtmlEncode(txticone.ClientID)
            lnkicone.NavigateUrl = "javascript:OpenNewWindow('" + TabId.ToString + "')"


        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            Dim xmlDoc As New XmlDocument()
            Dim nodeModule As XmlNode
            Dim nodeFile As XmlNode

            If Page.IsValid = True Then

                Dim objAdmin As New AdminDB()
                Dim dr As SqlDataReader

                If defId <> -1 Then
                    Dim strEditModuleIcon As String = txticone.Text
                    objAdmin.UpdateModuleDefinition(defId, GetLanguage("N"), txtFriendlyName.Text, txtDesktopSrc.Text, txtHelpSrc.Text, "", txtEditSrc.Text, True, txtDescription.Text, strEditModuleIcon, chkPremium.Checked)
                Else ' installing a new module

                    Dim strInstaller As String = cboModule.SelectedItem.Value

                    If File.Exists(strInstaller) Then
                        xmlDoc.Load(strInstaller)

                        nodeModule = xmlDoc.SelectSingleNode("module")

                        Dim strRelativePath As String = ""
                        Dim strAbsolutePath As String = ""

                        strRelativePath = nodeModule.Item("folder").InnerText()

                        If strRelativePath <> "" Then
                            strRelativePath += "/"
                            strAbsolutePath = Server.MapPath(strRelativePath)

                            If Not Directory.Exists(strAbsolutePath) Then
                                Directory.CreateDirectory(strAbsolutePath)
                            End If

                            ' check if development
                            Dim blnDelete As Boolean = True
                            If File.Exists(strAbsolutePath & nodeModule.Item("friendlyname").InnerText & ".vbproj") Then
                                blnDelete = False
                            End If
                        End If

                        ' move uninstall script to module folder
                        Dim strUninstallScript As String = nodeModule.Item("uninstall").InnerText
                        If strUninstallScript <> "" Then
                            If File.Exists(Request.MapPath(glbSiteDirectory) & strUninstallScript) Then
                                If File.Exists(strAbsolutePath & strUninstallScript) Then
                                    File.Delete(strAbsolutePath & strUninstallScript)
                                End If
                                File.Move(Request.MapPath(glbSiteDirectory) & strUninstallScript, strAbsolutePath & strUninstallScript)
                                objAdmin.DeleteFile(strUninstallScript)
                            End If
                        End If

                        ' install files
                        For Each nodeFile In xmlDoc.SelectNodes("//module/files/file")
                            Dim strFileName As String = nodeFile.Item("name").InnerText.ToLower
                            If File.Exists(Request.MapPath(glbSiteDirectory) & strFileName) Then
                                Dim strExtension As String = Mid(strFileName, InStrRev(strFileName, ".") + 1)
                                Select Case strExtension.ToLower()
                                    Case "dll"
                                        ' move DLL to application /bin/ folder
                                        If File.Exists(GetAbsoluteServerPath(Request) + "bin\" & strFileName) Then
                                            File.Delete(GetAbsoluteServerPath(Request) + "bin\" & strFileName)
                                        End If
                                        File.Move(Request.MapPath(glbSiteDirectory) & strFileName, GetAbsoluteServerPath(Request) + "bin\" & strFileName)
                                        objAdmin.DeleteFile(strFileName)
                                    Case "sql"
                                        ' read SQL installation script
                                        Dim objStreamReader As StreamReader
                                        objStreamReader = File.OpenText(Request.MapPath(glbSiteDirectory) & strFileName)
                                        Dim strScript As String = objStreamReader.ReadToEnd
                                        objStreamReader.Close()

                                        ' execute SQL installation script
                                        objAdmin.ExecuteSQLScript(strScript)

                                        ' delete script file once executed
                                        File.Delete(Request.MapPath(glbSiteDirectory) & strFileName)
                                        objAdmin.DeleteFile(strFileName)
                                    Case Else
                                        ' move user controls to module folder
                                        If File.Exists(strAbsolutePath & strFileName) Then
                                            File.Delete(strAbsolutePath & strFileName)
                                        End If
                                        File.Move(Request.MapPath(glbSiteDirectory) & strFileName, strAbsolutePath & strFileName)
                                        objAdmin.DeleteFile(strFileName)

                                        ' add supporting modules to database
                                        If strExtension = "ascx" Then
                                            If InStr(1, nodeModule.Item("desktopsrc").InnerText, strFileName, CompareMethod.Text) = 0 And _
                                              InStr(1, nodeModule.Item("helpsrc").InnerText, strFileName, CompareMethod.Text) = 0 And _
                                              InStr(1, nodeModule.Item("editsrc").InnerText, strFileName, CompareMethod.Text) = 0 Then
                                                Dim strFriendlyName As String = Left(strFileName, InStrRev(strFileName, ".") - 1)
                                                dr = objAdmin.GetSingleModuleDefinitionByName(GetLanguage("N"), strFriendlyName)
                                                If dr.Read Then
                                                    ' upgrade
                                                    objAdmin.UpdateModuleDefinition(dr("ModuleDefID"), GetLanguage("N"), strFriendlyName, "", "", "", strRelativePath & "/" & strFileName, True, "", "", False)
                                                Else
                                                    ' new
                                                    objAdmin.AddModuleDefinition(strFriendlyName, "", "", "", strRelativePath & "/" & strFileName, True, "", "", False)
                                                End If
                                                dr.Close()

                                            End If
                                        End If
                                End Select
                            End If
                        Next

                        ' add new module definition to database
                        dr = objAdmin.GetSingleModuleDefinitionByName(GetLanguage("N"), nodeModule.Item("friendlyname").InnerText)
                        If dr.Read Then
                            ' upgrade
                            objAdmin.UpdateModuleDefinition(dr("ModuleDefID"), GetLanguage("N"), nodeModule.Item("friendlyname").InnerText, IIf(nodeModule.Item("desktopsrc").InnerText <> "", strRelativePath & nodeModule.Item("desktopsrc").InnerText, ""), IIf(nodeModule.Item("helpsrc").InnerText <> "", strRelativePath & nodeModule.Item("helpsrc").InnerText, ""), "", IIf(nodeModule.Item("editsrc").InnerText <> "", strRelativePath & nodeModule.Item("editsrc").InnerText, ""), True, nodeModule.Item("description").InnerText, nodeModule.Item("editmoduleicon").InnerText, False)
                        Else
                            ' new
                            objAdmin.AddModuleDefinition(nodeModule.Item("friendlyname").InnerText, IIf(nodeModule.Item("desktopsrc").InnerText <> "", strRelativePath & nodeModule.Item("desktopsrc").InnerText, ""), IIf(nodeModule.Item("helpsrc").InnerText <> "", strRelativePath & nodeModule.Item("helpsrc").InnerText, ""), "", IIf(nodeModule.Item("editsrc").InnerText <> "", strRelativePath & nodeModule.Item("editsrc").InnerText, ""), True, nodeModule.Item("description").InnerText, nodeModule.Item("editmoduleicon").InnerText, False)
                        End If
                        dr.Close()

                        ' move installation file to module folder ( for uninstall )
                        If cboModule.SelectedItem.Text <> "Template" Then
                            If File.Exists(strAbsolutePath & cboModule.SelectedItem.Text & ".xml") Then
                                File.Delete(strAbsolutePath & cboModule.SelectedItem.Text & ".xml")
                            End If
                            File.Move(strInstaller, strAbsolutePath & cboModule.SelectedItem.Text & ".xml")
                            objAdmin.DeleteFile(cboModule.SelectedItem.Text & ".xml")
                        End If
                    End If

                End If

                If Request.Params("tabid") Is Nothing Then
                    Response.Redirect(GetFullDocument() & "?" & GetAdminPage(), True)
                Else
                    Response.Redirect(GetFullDocument() & "?tabid=" & Request.Params("tabid") & "&" & GetAdminPage(), True)
                End If




            End If

        End Sub

        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click

            Dim xmlDoc As New XmlDocument()
            Dim nodeModule As XmlNode
            Dim nodeFile As XmlNode

            Dim objAdmin As New AdminDB()
            Dim dr As SqlDataReader

            Dim strRelativePath As String = ""
            Dim strAbsolutePath As String = ""

            If ViewState("FriendLyName") <> "Template" Then
                strRelativePath = txtDesktopSrc.Text
                strRelativePath = Left(strRelativePath, InStrRev(strRelativePath, "/") - 1)
                strAbsolutePath = Server.MapPath(strRelativePath) & "\"
            End If

            ' read the installation file
            If File.Exists(strAbsolutePath & ViewState("FriendLyName") & ".xml") Then
                xmlDoc.Load(strAbsolutePath & ViewState("FriendLyName") & ".xml")

                nodeModule = xmlDoc.SelectSingleNode("module")

                ' check if development
                Dim blnDelete As Boolean = True
                If File.Exists(strAbsolutePath & nodeModule.Item("friendlyname").InnerText & ".vbproj") Then
                    blnDelete = False
                End If

                ' remove icon
                Dim strEditModuleIcon As String = nodeModule.Item("editmoduleicon").InnerText
                If strEditModuleIcon <> "" Then
                    If File.Exists(Request.MapPath(glbSiteDirectory) & strEditModuleIcon) Then
                        File.Delete(Request.MapPath(glbSiteDirectory) & strEditModuleIcon)
                    End If
                End If

                ' remove files
                For Each nodeFile In xmlDoc.SelectNodes("//module/files/file")
                    Dim strFileName As String = nodeFile.Item("name").InnerText.ToLower
                    Dim strExtension As String = Mid(strFileName, InStrRev(strFileName, ".") + 1)
                    Select Case strExtension.ToLower()
                        Case "dll"
                            ' delete DLL from application /bin/ folder
                            If File.Exists(GetAbsoluteServerPath(Request) + "bin/" & strFileName) Then
                                File.Delete(GetAbsoluteServerPath(Request) + "bin/" & strFileName)
                            End If
                        Case Else
                            ' delete files from module folder
                            If blnDelete Then
                                If File.Exists(strAbsolutePath & strFileName) Then
                                    File.Delete(strAbsolutePath & strFileName)
                                End If
                            End If

                            ' remove supporting modules from database
                            If strExtension = "ascx" Then
                                If InStr(1, nodeModule.Item("desktopsrc").InnerText, strFileName, CompareMethod.Text) = 0 And _
                                  InStr(1, nodeModule.Item("helpsrc").InnerText, strFileName, CompareMethod.Text) = 0 And _
                                  InStr(1, nodeModule.Item("editsrc").InnerText, strFileName, CompareMethod.Text) = 0 Then
                                    Dim strFriendlyName As String = Left(strFileName, InStrRev(strFileName, ".") - 1)
                                    dr = objAdmin.GetSingleModuleDefinitionByName(GetLanguage("N"), strFriendlyName)
                                    If dr.Read Then
                                        ' delete
                                        objAdmin.DeleteModuleDefinition(dr("ModuleDefID"))
                                    End If
                                    dr.Close()
                                End If
                            End If
                    End Select
                Next

                ' execute uninstall script
                Dim strUninstallScript As String = nodeModule.Item("uninstall").InnerText
                If strUninstallScript <> "" Then
                    If File.Exists(strAbsolutePath & strUninstallScript) Then
                        ' read uninstall script
                        Dim objStreamReader As StreamReader
                        objStreamReader = File.OpenText(strAbsolutePath & strUninstallScript)
                        Dim strScript As String = objStreamReader.ReadToEnd
                        objStreamReader.Close()

                        ' execute SQL installation script
                        objAdmin.ExecuteSQLScript(strScript)

                        ' delete script file once executed
                        If blnDelete Then
                            File.Delete(strAbsolutePath & strUninstallScript)
                        End If
                    End If
                End If

                ' remove installation script and directory
                If blnDelete Then
                    File.Delete(strAbsolutePath & ViewState("FriendLyName") & ".xml")
                    Directory.Delete(strAbsolutePath)
                End If
            End If

            ' delete definition
            objAdmin.DeleteModuleDefinition(defId)

            If Request.Params("tabid") Is Nothing Then
                Response.Redirect(GetFullDocument() & "?" & GetAdminPage(), True)
            Else
                Response.Redirect(GetFullDocument() & "?tabid=" & Request.Params("tabid") & "&" & GetAdminPage(), True)
            End If



        End Sub


        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            If Request.Params("tabid") Is Nothing Then
                Response.Redirect(GetFullDocument() & "?" & GetAdminPage(), True)
            Else
                Response.Redirect(GetFullDocument() & "?tabid=" & Request.Params("tabid") & "&" & GetAdminPage(), True)
            End If
        End Sub

    End Class

End Namespace