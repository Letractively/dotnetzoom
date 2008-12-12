<%@ Control Language="vb" codebehind="Terms.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.Terms" %>
<%@ Register TagPrefix="FCKeditorV2" Namespace="FredCK.FCKeditorV2" Assembly="FredCK.FCKeditorV2" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:placeholder id="pnlRichTextBox" Runat="server" Visible="False">
<div align="center">
<FCKeditorV2:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></FCKeditorV2:FCKeditor>
</div>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" visible="true" runat="server" ></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" visible="true" runat="server"  CausesValidation="False"></asp:LinkButton>
<br>
</p>

</asp:placeholder>
<asp:placeholder id="Terms" Runat="server" Visible="False">
<asp:Label EnableViewState="false" id="lblTerms" runat="server" cssclass="Normal">
<table align="center" width="80%" border="0" >
<tr>
<td>
<span class="ItemTitle" style="font: bolder medium"> Conditions générales d'utilisation entre vous et {PortalName}</span> 
<br>
<br>
<span class="ItemTitle"> COMPTE D'UTILISATEUR, MOT DE PASSE ET SÉCURITÉ</span> 
<br>
<br>
Si vous ouvrez un compte sur le site web {PortalName}, vous devez compléter le formulaire
d'inscription avec des informations actuelles, complètes et exactes, comme le formulaire
d'inscription en question vous y invite. Vous choisirez ensuite un mot de passe et
un nom d'utilisateur. Vous êtes entièrement responsable du maintien de la confidentialité
de votre mot de passe et de votre nom d'utilisateur. Vous êtes en outre entièrement
responsable de toute activité ayant lieu sous votre compte. Vous vous engagez à  avertir
immédiatement {PortalName} de toute utilisation non autorisée de votre compte, ou
de toute autre atteinte à  la sécurité. {PortalName} ne pourra en aucun cas être tenue
pour responsable d'un quelconque dommage que vous subiriez du fait de l'utilisation
par autrui de votre mot de passe ou de votre compte, que vous ayez eu connaissance
ou non de cette utilisation. Néanmoins, votre responsabilité pourrait être engagée
si {PortalName} ou un tiers subissait des dommages dus à l'utilisation par autrui
de votre compte ou de votre mot de passe. Il ne vous est jamais permis d'utiliser
le compte d'autrui sans l'autorisation du titulaire du compte. 
<br>
<br>
<span class="ItemTitle"> MODIFICATION DES PRÉSENTES CONDITIONS D'UTILISATION</span> 
<br>
<br>
{PortalName} se réserve le droit de modifier les termes, conditions et mentions d'avertissement
applicables au site et Services qui vous sont proposés. Il vous incombe de consulter
régulièrement ces termes et conditions d'utilisation. Votre utilisation renouvelée
du site web {PortalName} constitue l'acceptation de votre part de tous ces termes,
conditions d'utilisation et mentions d'avertissement. 
<br>
<br>
<span class="ItemTitle"> UTILISATION LIMITÉE À DES FINS PERSONNELLES ET NON COMMERCIALES</span> 
<br>
<br>
Sauf indication contraire, le site web {PortalName} est destiné à  être utilisés à 
des fins personnelles et non commerciales. Vous n'êtes pas autorisé à  modifier, copier,
distribuer, transmettre, diffuser, représenter, reproduire, publier, concéder sous
licence, créer des oeuvres dérivées, transférer ou vendre tout information, logiciel,
produit ou service obtenu à  partir de ce site web. Vous ne pouvez pas utiliser le
site web {PortalName} à  des fins commerciales, sans l'autorisation expresse, écrite
et préalable, de {PortalName}. 
<br>
<br>
<span class="ItemTitle"> LIENS VERS DES SITES TIERS </span> 
<br>
<br>
Le site web {PortalName} peut contenir des images de, et des liens vers des sites Web
gérés par des tiers. {PortalName} n'exerce aucun contrôle sur ces sites et n'assume
aucune responsabilité quant à leur contenu, ni notamment quant au contenu des liens
présentés dans ces sites, ou encore aux modifications ou mises à jour apportées à 
ces sites. 
<br>
<br>
<span class="ItemTitle"> UTILISATION ILLICITE OU INTERDITE</span> 
<br>
<br>
Vous ne pouvez utiliser le site web {PortalName} qu'à  condition de garantir que vous
ne l'utiliserez pas à  des fins illicites ou interdites par ces termes, conditions
d'utilisation et mentions d'avertissement. 
<br>
<br>
<span class="ItemTitle">QUESTIONS</span>
<br>
<br>
Si vous avez des questions ou des commentaires au sujet des conditions d'utilisation, n'hésitez
pas à vous adresser {PortalName} par courriel à {AdministratorEmail}.
<br>
<br>
</td>
</tr>
</table>
</asp:Label>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>