<%@ Import Namespace="DotNetZoom" %>
<script runat="server">
Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
StyleSheet.Text = "<link href=""" & _portalSettings.UploadDirectory & "skin/portal.css"" type=""text/css"" rel=""stylesheet"">"
end sub
</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
	<title><%= DotNetZoom.GetLanguage("Select_Color") %></title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<meta name="robots" content="noindex, nofollow" />
		<asp:literal id="StyleSheet" enableviewstate="false" runat="server" />
		<style TYPE="text/css">
			#ColorTable		{ cursor: pointer ; cursor: hand ; }
			#hicolor		{ height: 74px ; width: 74px ; border-width: 1px ; border-style: solid ; }
			#hicolortext	{ width: 75px ; text-align: right ; margin-bottom: 7px ; }
			#selhicolor		{ height: 20px ; width: 74px ; border-width: 1px ; border-style: solid ; }
			#selcolor		{ width: 75px ; height: 20px ; margin-top: 0px ; margin-bottom: 7px ; }
			#btnClear		{ width: 75px ; height: 22px ; margin-bottom: 6px ; }
			.ColorCell		{ height: 15px ; width: 15px ; }
		</style>
		<script type="text/javascript">
		

function OnLoad()
{
	CreateColorTable() ;

}

function CreateColorTable()
{
	// Get the target table.
	var oTable = document.getElementById('ColorTable') ;

	// Create the base colors array.
	var aColors = ['00', '33','66','99','cc','ff'] ;

	// This function combines two ranges of three values from the color array into a row.
	function AppendColorRow( rangeA, rangeB )
	{
		for ( var i = rangeA ; i < rangeA + 3 ; i++ ) 
		{ 
			var oRow = oTable.insertRow(-1) ; 

			for ( var j = rangeB ; j < rangeB + 3 ; j++ ) 
			{ 
				for ( var n = 0 ; n < 6 ; n++ ) 
				{ 
					AppendColorCell( oRow, '#' + aColors[j] + aColors[n] + aColors[i] ) ; 
				} 
			} 
		}
	}

	// This function create a single color cell in the color table.
	function AppendColorCell( targetRow, color )
	{
		var oCell = targetRow.insertCell(-1) ;
		oCell.className = 'ColorCell' ;
		oCell.bgColor = color ;
		
		oCell.onmouseover = function()
		{
			document.getElementById('hicolor').style.backgroundColor = this.bgColor ;
			document.getElementById('hicolortext').innerHTML = this.bgColor ;
		}
		
		oCell.onclick = function()
		{
			document.getElementById('selhicolor').style.backgroundColor = this.bgColor ;
			document.getElementById('selcolor').value = this.bgColor ;
		}
	}

	AppendColorRow( 0, 0 ) ;
	AppendColorRow( 3, 0 ) ;
	AppendColorRow( 0, 3 ) ;
	AppendColorRow( 3, 3 ) ;

	// Create the last row.
	var oRow = oTable.insertRow(-1) ;
	
	// Create the gray scale colors cells.
	for ( var n = 0 ; n < 6 ; n++ )
	{
		AppendColorCell( oRow, '#' + aColors[n] + aColors[n] + aColors[n] ) ; 
	}
	
	// Fill the row with black cells.
	for ( var i = 0 ; i < 12 ; i++ )
	{
		AppendColorCell( oRow, '#000000' ) ; 
	}
}

function Clear()
{
	document.getElementById('selhicolor').style.backgroundColor = '' ;
	document.getElementById('selcolor').value = '' ;
}

function ClearActual()
{
	document.getElementById('hicolor').style.backgroundColor = '' ;
	document.getElementById('hicolortext').innerHTML = '&nbsp;' ;
}

function UpdateColor()
{
	try		  { document.getElementById('selhicolor').style.backgroundColor = document.getElementById('selcolor').value ; }
	catch (e) { Clear() ; }
}

function Ok()
{
	window.top.opener.Setcolor(document.getElementById('selcolor').value) ;
	window.top.close() ;
	window.top.opener.focus() ;
}

function Cancel()
{
	window.top.close() ;
	window.top.opener.focus() ;
}
		</script>
</head>
	<body onload="OnLoad()" style="OVERFLOW: hidden">
		 <table class="OtherCellTop" cellpadding="0" cellspacing="0" border="0" width="100%" height="15%">
		 	<tr>
				<td align="center" valign="middle" width="100%">
				<span class="Head"><%= DotNetZoom.GetLanguage("Select_Color") %></span>
			</td>
			</tr>
		 </table>
		<table class="MainBorder" cellpadding="0" cellspacing="0" border="0" width="100%" height="74%">
			<tr>
				<td align="center" valign="middle">
					<table border="0" cellspacing="5" cellpadding="0" width="100%">
						<tr>
							<td valign="top" align="center" style="white-space: nowrap;" width="100%">
								<table id="ColorTable" border="0" cellspacing="0" cellpadding="0" width="270" onmouseout="ClearActual();">
								</table>
							</td>
							<td valign="top" align="left" style="white-space: nowrap;">
								<span><%= DotNetZoom.GetLanguage("Highlight") %></span>
								<div id="hicolor"></div>
								<div id="hicolortext">&nbsp;</div>
								<span><%= DotNetZoom.GetLanguage("Selected") %></span>
								<div id="selhicolor"></div>
								<input id="selcolor" type="text" maxlength="20" onchange="UpdateColor();">
								<br>
								<input id="btnClear" type="button" value="<%= DotNetZoom.GetLanguage("Clear") %>" onclick="Clear();" />
							</td>
						</tr>
					</table>
     		</td>
			</tr>
		</table>
		 <table class="OtherCellBottom" cellpadding="0" cellspacing="0" border="0" width="100%" height="11%">
		 	<tr>
		 		<td align="right">
					<input id="btnOK" type="button"  value="<%= DotNetZoom.GetLanguage("OK") %>" onclick="Ok();" />
					<input id="btnCANCEL" type="button"  value="<%= DotNetZoom.GetLanguage("return") %>" onclick="Cancel();" />
			</td>
			</tr>
		 </table>
	</body>
</html>