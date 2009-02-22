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

    Public MustInherit Class Discussion
        Inherits DotNetZoom.PortalModuleControl

		Protected WithEvents before As System.Web.UI.WebControls.Literal
		Protected WithEvents after As System.Web.UI.WebControls.Literal


        Protected WithEvents lstDiscussions As System.Web.UI.WebControls.DataList
        Protected WithEvents pnlModuleContent As System.Web.UI.WebControls.PlaceHolder
		Protected WithEvents Title1 As DotNetZoom.DesktopModuleTitle
		Protected WithEvents cmdAdd As System.Web.UI.WebControls.Button

        Private itemIndex As Integer = -1

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
			
			

			Title1.DisplayHelp = "DisplayHelp_Discussions"
			CmdAdd.Text = GetLanguage("add")
			CmdAdd.ToolTip = GetLanguage("add")
			CmdAdd.Visible = PortalSecurity.HasEditPermissions(ModuleId)
            If IsNumeric(Request.Params("ItemIndex")) Then
                itemIndex = Int32.Parse(Request.Params("ItemIndex"))
            End If
            Title1.EditText = GetLanguage("add")
            Title1.EditIMG = "<img  src=""" & glbPath & "images/add.gif"" alt=""*"" style=""border-width:0px;"">"

            If Page.IsPostBack = False Then
                BindList()

                If itemIndex <> -1 Then
                    lstDiscussions.SelectedIndex = itemIndex
                    BindList()
                End If

            End If

        End Sub

        Sub BindList()

            ' Obtain a list of discussion messages for the module
            ' and bind to datalist

            ' Check to see if available in Cache
            Dim TempKey As String = GetDBname & "ModuleID_" & CStr(ModuleId)
            Dim context As HttpContext = HttpContext.Current
            Dim content As System.data.DataTable = context.Cache(TempKey)
            If content Is Nothing Then
                '	Item not in cache, get it manually    
                Dim discuss As New DiscussionDB()
                Dim objAdmin As New AdminDB()
                content = AdminDB.ConvertDataReaderToDataTable(discuss.GetTopLevelMessages(ModuleId))
                Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
                context.Cache.Insert(TempKey, content, CDp(_portalSettings.PortalID, _portalSettings.ActiveTab.Tabid, ModuleID), System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(2), Caching.CacheItemPriority.normal, Nothing)
            End If
            lstDiscussions.DataSource = content
            lstDiscussions.DataBind()

        End Sub

        Function GetThreadMessages() As SqlDataReader

            ' Obtain a list of discussion messages for the module
            Dim discuss As New DiscussionDB()
            Dim dr As SqlDataReader = discuss.GetThreadMessages(lstDiscussions.DataKeys(lstDiscussions.SelectedIndex).ToString())

            ' Return the filtered DataView
            Return dr

            dr.Close()

        End Function

        Private Sub lstDiscussions_Select(ByVal Sender As Object, ByVal e As DataListCommandEventArgs) Handles lstDiscussions.ItemCommand

            ' Determine the command of the button (either "select" or "collapse")
            Dim command As String = CType(e.CommandSource, ImageButton).CommandName

            ' Update asp:datalist selection index depending upon the type of command
            ' and then rebind the asp:datalist with content
            If command = "collapse" Then
                lstDiscussions.SelectedIndex = -1
            Else
                lstDiscussions.SelectedIndex = e.Item.ItemIndex
            End If

            BindList()

        End Sub

        Function NodeStyle(ByVal count As Integer) As String

            If count > 0 Then
                Return "border-width:0px; background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -407px;"
            Else
                Return "border-width:0px; background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -393px;"
            End If

        End Function

        Function NodeStyleFermer(ByVal count As Integer) As String

            If count > 0 Then
                Return "border-width:0px; background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -377px;"
            Else
                Return "border-width:0px; background: url('" & glbPath & "images/uostrip.gif') no-repeat; background-position: 0px -393px;"
            End If

        End Function
		
        Function NodetextFermer(ByVal count As Integer) As String

            If count > 0 Then
                Return GetLanguage("label_close")
            Else
                Return GetLanguage("label_no_answer")
            End If

        End Function



		
        Function NodeText(ByVal count As Integer) As String

            If count > 0 Then
                Return GetLanguage("label_open")
            Else
                Return GetLanguage("label_no_answer")
            End If

        End Function
		
		
		
        Public Function FormatUser(ByVal UserName As Object) As String
            If Not UserName.ToString = "Anonymous Anonymous" Then
                Return UserName.ToString
            Else
                Return GetLanguage("label_anonymous") 
            End If
        End Function

        Public Function FormatMultiLine(ByVal strValue As String) As String
            Return (New PortalSecurity()).InputFilter(strValue, PortalSecurity.FilterFlag.MultiLine)
        End Function

		Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAdd.Click
           ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
            Response.Redirect(GetFullDocument() & "?edit=control&tabid=" & _portalSettings.ActiveTab.TabId & "&mid=" & ModuleId.ToString)
        End Sub

		
    End Class

End Namespace