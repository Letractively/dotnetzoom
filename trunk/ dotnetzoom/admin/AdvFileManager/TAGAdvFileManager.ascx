<%@ Control Language="vb" autoeventwireup="false" codebehind="TAGAdvFileManager.ascx.vb" Inherits="DotNetZoom.TAGAdvFileManager" targetschema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="tag" Namespace="TAG.WebControls" Assembly="TAG.WebControls" %>
<%@ Register TagPrefix="TAGFMan" TagName="TAGFileExplorer" Src="TAGFileExplorer.ascx" %>

<script language="javascript" type="text/javascript"><!--
function SelectAllCheckboxes()
{

	var blnChecked;
	blnChecked = document.getElementById('chkAllcheckbox').checked;
	
	for(intIndex=0; intIndex<document.forms[0].length; intIndex++)
	{
		if (document.forms[0].elements[intIndex].type == "checkbox")
		{
			document.forms[0].elements[intIndex].checked = blnChecked;
		}
	}
}
//--></script>




<table width="615px" style="font-size: 10px;">
<tr>
<td>
<asp:label id="lblerror" cssclass="NormalRed" runat="server" EnableViewState="false"></asp:label>
</td>
</tr>
</table>
<asp:panel id="pnlToolBar" Wrap="False" cssclass="ExplorerHeader"  width="615px" runat="server" >
<asp:PlaceHolder id="AdminPanel" Runat="server">
    <TAG:ROLLOVERIMAGEBUTTON id="imgParentDir" width="16px" runat="server" ImageALIGN="middle" height="16px"  RolloverImageURL="~/Admin/AdvFileManager/images/ParentFolder_Rollover.gif" DisabledImageURL="~/Admin/AdvFileManager/images/ParentFolder_Disabled.gif"  EnabledImageURL="~/Admin/AdvFileManager/images/ParentFolder.gif"></TAG:ROLLOVERIMAGEBUTTON>
    <TAG:ROLLOVERIMAGEBUTTON id="imgRefresh" width="16px" runat="server" ImageALIGN="middle" height="16px"  RolloverImageURL="~/Admin/AdvFileManager/images/Recycle_Rollover.gif" DisabledImageURL="~/Admin/AdvFileManager/images/Recycle_Disabled.gif"  EnabledImageURL="~/Admin/AdvFileManager/images/Recycle.gif"></TAG:ROLLOVERIMAGEBUTTON>
    <asp:Image id="Image1" width="8px" runat="server" enableviewstate="false" ImageALIGN="middle" ImageURL="~/Admin/AdvFileManager/images/ToolSep.gif" height="24px"></asp:Image>
    <TAG:ROLLOVERIMAGEBUTTON id="imgNewFolder" width="16px" runat="server" ImageALIGN="middle" height="16px" RolloverImageURL="~/Admin/AdvFileManager/images/NewFolder_Rollover.gif" DisabledImageURL="~/Admin/AdvFileManager/images/NewFolder_Disabled.gif" EnabledImageURL="~/Admin/AdvFileManager/images/NewFolder.gif"></TAG:ROLLOVERIMAGEBUTTON>
    <TAG:ROLLOVERIMAGEBUTTON id="imgUpload" width="16px" runat="server" ImageALIGN="middle" height="16px"  RolloverImageURL="~/Admin/AdvFileManager/images/Upload_Rollover.gif" DisabledImageURL="~/Admin/AdvFileManager/images/Upload_Disabled.gif" EnabledImageURL="~/Admin/AdvFileManager/images/Upload.gif"></TAG:ROLLOVERIMAGEBUTTON>
    <TAG:ROLLOVERIMAGEBUTTON id="imgDownload" width="16px" runat="server" ImageALIGN="middle" height="16px"  RolloverImageURL="~/Admin/AdvFileManager/images/Download_Rollover.gif" DisabledImageURL="~/Admin/AdvFileManager/images/Download_Disabled.gif" EnabledImageURL="~/Admin/AdvFileManager/images/Download.gif"></TAG:ROLLOVERIMAGEBUTTON>
    <asp:Image id="Image2" width="8px" runat="server" enableviewstate="false" ImageALIGN="middle" ImageURL="~/Admin/AdvFileManager/images/ToolSep.gif" height="24px"></asp:Image>
    <TAG:ROLLOVERIMAGEBUTTON id="imgDelete" width="16px" runat="server"  ImageALIGN="middle" height="16px"  RolloverImageURL="~/Admin/AdvFileManager/images/Delete_Rollover.gif" DisabledImageURL="~/Admin/AdvFileManager/images/Delete_Disabled.gif"  EnabledImageURL="~/Admin/AdvFileManager/images/Delete.gif"></TAG:ROLLOVERIMAGEBUTTON>
    <TAG:ROLLOVERIMAGEBUTTON id="imgRename" width="16px" runat="server"  ImageALIGN="middle" height="16px" RolloverImageURL="~/Admin/AdvFileManager/images/Rename_Rollover.gif" DisabledImageURL="~/Admin/AdvFileManager/images/Rename_Disabled.gif"  EnabledImageURL="~/Admin/AdvFileManager/images/Rename.gif"></TAG:ROLLOVERIMAGEBUTTON>
    <asp:Image id="Image3" width="8px" runat="server" enableviewstate="false" ImageALIGN="middle" ImageURL="~/Admin/AdvFileManager/images/ToolSep.gif" height="24px"></asp:Image>
    &nbsp;&nbsp;<%= Replace(Session("RelativeDir"), "\", "/") & "/" %>
</asp:PlaceHolder>
<asp:PlaceHolder id="DirPanel" Runat="server">
    <asp:TextBox id="DirCreate" runat="server" width="200px" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'></asp:TextBox>
    <tag:RolloverImageButton id="imgDirOK" runat="server" CommandName="DirOK" width="16px" height="16px" DisabledImageURL="~/Admin/AdvFileManager/images/OK_Disabled.gif" EnabledImageURL="~/Admin/AdvFileManager/images/OK.gif" RolloverImageURL="~/Admin/AdvFileManager/images/OK_Rollover.gif" StatusText="Creer" ToolTip="Creer"></tag:RolloverImageButton>
    <tag:RolloverImageButton id="imgDirCancel" runat="server" CommandName="DirCancel" width="16px" height="16px" DisabledImageURL="~/Admin/AdvFileManager/images/Delete_Disabled.gif" EnabledImageURL="~/Admin/AdvFileManager/images/Delete.gif" RolloverImageURL="~/Admin/AdvFileManager/images/Delete_Rollover.gif" StatusText="Annuler" ToolTip="Annuler"></tag:RolloverImageButton>
</asp:PlaceHolder>
</asp:panel>
<asp:table id="Table1" style="font-size: 10px;" runat="server" CellPadding="0" CellSpacing="0">
    <asp:TableRow>
        <asp:TableCell>
            <TAGFMan:TAGfileexplorer id="FileExp" runat="server" RelativeDir="" AllowRecursiveDelete="true" RoundTripOnCheck="true"></TAGFMan:TAGfileexplorer>
        </asp:TableCell>
    </asp:TableRow>
</asp:table>