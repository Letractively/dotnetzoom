<%@ Control Language="vb" codebehind="demo.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.demo" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:literal id="lbldemo" runat="server" enableviewstate="false">
<div align="left" class="header">
<p align="left">
{Directives}
</p>
</div>
<div align="left" class="header">
<span class="Head">Info importante</span>
<ol>
	<li>Vous devez choisir un nom pour votre nouveau site.  Pour un site d'essais l'adresse URL sera un sous répertoire du site web {DomainName}.  Tel que http://{DomainName}/votenom/</li>
	<li>Vous devez identifier, votre nom ainsi que votre prénom.</li>
	<li>Vous devez obligatoirement nous donner votre adresse de courriel.  Nous vous ferons parvenir par courriel un hyperlien pour activer votre compte d'administrateur de votre nouveau site web.</li>
	<li>Vous devez choisir un thème pour votre site,  pour les visonner avant de choisir vous avez juste à cliquer ici.&nbsp;&nbsp;<b><i>{lblpreview}</i></b></li>
	<li>Vous devez choisir un code d'accès ainsi qu'un môt de passe pour votre compte d'administrateur.  Pour des questions de sécurité nous vous demandons de choisir un môt de passe avec au moins 8 lettres ou chiffres et de même pour votre code d'accès. </li>
	<li>Vous devez accepter les termes et conditions d'utilisation. Après l'acceptation par l'hyperlien <b>j'accepte</b> dans le bas de la page, vous serez redirigé sur une nouvelle page pour completer la création de votre nouveau site web.</li>
	<li>Une fois que vous avez complété votre inscription et que votre nouveau site web a été créé.  Vous recevrez un <b>courriel avec un <font color="#ff0000">hyperlien</font></b> que vous devrez utiliser pour la première fois afin d'activer votre première session en tant qu'administrateur de votre site web.</li>
</ol>
</div>
<div align="left" class="header">
<span class="Head">Acceptation des conditions générales d'utilisation entre vous et {PortalName}
</span>
<p align="left">
Vous ne pouvez utiliser le site web {PortalName} qu'à condition de garantir que vous ne l'utiliserez pas à  des fins illicites ou interdites par ces termes, conditions d'utilisation et mentions d'avertissement.<br>
<a href="http://{DomainName}/{language}.default.aspx?edit=control&def=Signup&guid={lblGUID}" title="Acceptation des conditions générales d'utilisation et après l'acceptation de ces termes vous pourrez créer un site d'essais">&nbsp;<b><font color="#ff0000">j'accepte</font></b></a>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://{DomainName}/" title="Retour">&nbsp;<b><font color="#669900">je refuse</font></b></a>
</p>
</div>
</asp:literal>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>