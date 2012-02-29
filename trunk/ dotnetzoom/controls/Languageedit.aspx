<%@ Page Language="VB" Debug="false" Inherits="DotNetZoom.BasePage"%>
<%@ Import Namespace="DotNetZoom" %>


<script runat="server">

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"
        If Request.IsAuthenticated Then
            If Context.User.Identity.Name = _portalSettings.SuperUserId Then
                If Not Page.IsPostBack Then
                    btnLanguageEdit.Text = GetLanguage("btnLanguageEdit") & " " & GetLanguage("N")
                    btnLanguageEditAll.Text = GetLanguage("btnLanguageEditAll") & " " & GetLanguage("N")
                    grdLanguage.Columns(0).HeaderText = GetLanguage("language_param")
                    grdLanguage.Columns(1).HeaderText = GetLanguage("language_change")
                End If
            Else
                AccessDenied()
            End If
        Else
            AccessDenied()
        End If
End Sub


    Private Sub AccessDenied()
        RegisterBADip(Request.UserHostAddress)
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
        Dim Admin As New AdminDB()
        lblTerms.Visible = True
        btnLanguageEdit.Visible = False
        btnLanguageEditAll.Visible = False
        grdLanguage.Visible = False
        lblTerms.Text = ProcessLanguage(Admin.GetSinglelonglanguageSettings(GetLanguage("N"), "AccessDeniedInfo"), Page)
    End Sub
    
Private Sub ShowNewOnly()
		  Dim Admin As New AdminDB()
          Dim dr As System.Data.SqlClient.SqlDataReader
		  Dim _Newlanguage As New HashTable
		  Dim _language As HashTable = HttpContext.Current.Session("LanguageTable")

         Dim myEnumerator As IDictionaryEnumerator = _language.GetEnumerator()
         While myEnumerator.MoveNext()
            dr = Admin.GetNewlanguage(GetLanguage("N"), myEnumerator.Key.ToString)
            If dr.Read Then
                If dr.GetString(2) = "New" Then
                    _Newlanguage.Add(myEnumerator.Key, myEnumerator.Value)
                End If
                
                
                If ddlContext.Items.FindByValue(dr.GetString(2)) Is Nothing Then
                    ddlContext.Items.Add(dr.GetString(2))
                         
                End If
                
            Else
                _Newlanguage.Add(myEnumerator.Key, myEnumerator.Value)
            End If
            dr.Close()
        End While
        ddlContext.SelectedIndex = 0
        ddlContext.Visible = False
		 HttpContext.Current.Session("LanguageTable") =  _Newlanguage
End Sub


Public Sub ShowNew_OnClick(ByVal sender As Object, ByVal e As EventArgs) 
			
		   If Request.IsAuthenticated then	
           Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		   If Context.User.Identity.Name = _portalSettings.SuperUserId then
		   grdLanguage.Visible = True
		   ShowNewOnly()
	       Dim dt As New System.Data.DataTable()
		   Dim _language As HashTable = HttpContext.Current.Session("LanguageTable")
     	   dt.Columns.Add(New System.Data.DataColumn("SettingName", GetType(String)))
       	   dt.Columns.Add(New System.Data.DataColumn("SettingValue", GetType(String)))
   		   Dim dr As System.Data.DataRow

         Dim myEnumerator As IDictionaryEnumerator = _language.GetEnumerator()
         While myEnumerator.MoveNext()
		    dr = dt.NewRow()
           dr(0) = myEnumerator.Key.ToString
		   dr(1) = myEnumerator.Value.ToString
   	   	   dt.Rows.Add(dr)
        End While
			

		   Dim dv As New System.Data.DataView(dt)
		   dv.Sort = "SettingName"
		   grdLanguage.DataSource =  dv
           grdLanguage.DataBind()
		   end if
		   end if

        btnLanguageEditAll.Visible = False
        btnLanguageEdit.Visible = False
	End Sub
	
Public Sub ShowAll_OnClick(ByVal sender As Object, ByVal e As EventArgs) 
			
		   If Request.IsAuthenticated then	
           Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

		   If Context.User.Identity.Name = _portalSettings.SuperUserId then
		   grdLanguage.Visible = True
	       Dim dt As New System.Data.DataTable()
		   Dim _language As HashTable = HttpContext.Current.Session("LanguageTable")
     	   dt.Columns.Add(New System.Data.DataColumn("SettingName", GetType(String)))
       	   dt.Columns.Add(New System.Data.DataColumn("SettingValue", GetType(String)))
   		   Dim dr As System.Data.DataRow

         Dim myEnumerator As IDictionaryEnumerator = _language.GetEnumerator()
         While myEnumerator.MoveNext()
		    dr = dt.NewRow()
           dr(0) = myEnumerator.Key.ToString
		   dr(1) = myEnumerator.Value.ToString
   	   	   dt.Rows.Add(dr)
        End While
			

		   Dim dv As New System.Data.DataView(dt)
		   dv.Sort = "SettingName"
		   grdLanguage.DataSource =  dv
           grdLanguage.DataBind()
		   end if
		   end if
   		 btnLanguageEditAll.Visible = False
	   
	End Sub	
	
	Private Function GetRenameTextBox(ByVal item As DataGridItem) As TextBox
	    	Return CType(item.FindControl("txtRename"), TextBox)
	End Function
   Private Sub grdLanguage_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
			Dim objAdmin As New AdminDB()
			
			Select Case e.CommandName
				Case "EditOK"
				ObjAdmin.UpdatelanguageSetting(GetLanguage("N"), e.CommandArgument, GetRenameTextBox(e.Item).Text)
			End Select
    		' clear Table to pick up changes

	       Dim dt As New System.Data.DataTable()
		   Dim _language As HashTable = HttpContext.Current.Session("LanguageTable")
		   _language.remove(e.CommandArgument)
		   _language.add(e.CommandArgument, GetRenameTextBox(e.Item).Text)
     	   dt.Columns.Add(New System.Data.DataColumn("SettingName", GetType(String)))
       	   dt.Columns.Add(New System.Data.DataColumn("SettingValue", GetType(String)))
   		   Dim dr As System.Data.DataRow

         Dim myEnumerator As IDictionaryEnumerator = _language.GetEnumerator()
         While myEnumerator.MoveNext()
		    dr = dt.NewRow()
           dr(0) = myEnumerator.Key.ToString
		   dr(1) = myEnumerator.Value.ToString
   	   	   dt.Rows.Add(dr)
        End While
			

		   Dim dv As New System.Data.DataView(dt)
		   dv.Sort = "SettingName"
		   grdLanguage.DataSource =  dv
           grdLanguage.DataBind()
   End Sub		
</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta http-equiv="Content-Script-Type" content="text/javascript">
<title><%= DotNetZoom.GetLanguage("Language_Edit_Param") %>&nbsp;<%= DotNetZoom.GetLanguage("language") %></title>
<asp:literal id="StyleSheet" enableviewstate="false" runat="server" />
<style type="text/css">
.scroll {overflow: auto; height: 250px; border: 1px silver solid;}
</style>
</head>
<body>
<form id="Form1" method="post" runat="server">
<span class="Head">
<%= DotNetZoom.GetLanguage("Language_Edit_Param") %>&nbsp;<%= DotNetZoom.GetLanguage("language") %>
</span>&nbsp;&nbsp;
<asp:Button cssclass="button" runat="server" ID="btnLanguageEdit" onclick="ShowNew_OnClick"></asp:Button>
&nbsp;&nbsp;<asp:Button cssclass="button" runat="server" ID="btnLanguageEditAll" onclick="ShowAll_OnClick"></asp:Button>
<div id="ScrollingPanel" class="scroll">
<asp:literal EnableViewState="false" id="lblTerms" runat="server">
</asp:literal>
<asp:datagrid id="grdLanguage" runat="server"  EnableViewState="true" OnItemCommand="grdLanguage_ItemCommand" AutoGenerateColumns="false" cellspacing="4" CellPadding="4" BorderStyle="None" gridlines="none">
    <Columns>
        <asp:TemplateColumn ItemStyle-Width="200" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <span onmouseover="<%# DotNetZoom.ReturnToolTip("Param: " & DataBinder.Eval(Container, "DataItem.SettingName") & "<br>Value: " & DataBinder.Eval(Container, "DataItem.SettingValue") )  %>"><%# DataBinder.Eval(Container, "DataItem.SettingName") %></span>
        </ItemTemplate>
		</asp:TemplateColumn>
        <asp:TemplateColumn ItemStyle-Width="550" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
        <ItemTemplate>
        <asp:ImageButton tooltip='<%# DotNetZoom.GetLanguage("enregistrer") & " -> " & DataBinder.Eval(Container, "DataItem.SettingValue") %>' id="imgEditOK" ImageURL="~/admin/AdvFileManager/Images/OK.gif" CommandName="EditOK" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SettingName") %>' runat="server" EnableViewState="true" BorderWidth="0" BorderStyle="none" CausesValidation="False" ></asp:ImageButton>
        <asp:TextBox id="txtRename" runat="server" MaxLength="256" Width="460px" Text='<%# DataBinder.Eval(Container, "DataItem.SettingValue") %>'></asp:TextBox>
        </ItemTemplate>
		</asp:TemplateColumn>
    </Columns>
</asp:datagrid>
<asp:DropDownList ID="ddlContext" runat="server" Width="300"></asp:DropDownList>
</div>
</form>
</body>
</html>