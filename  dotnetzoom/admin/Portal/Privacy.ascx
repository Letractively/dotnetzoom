<%@ Control Language="vb" codebehind="Privacy.ascx.vb" autoeventwireup="false" Explicit="True" Inherits="DotNetZoom.Privacy" %>
<%@ Register TagPrefix="Editor"  Namespace="dotnetzoom" Assembly="DotNetZoom" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="~/controls/DesktopModuleTitle.ascx" %>
<portal:title id="Title1" runat="server"></portal:title>
<asp:literal id="before" runat="server" EnableViewState="false" ></asp:literal>
<asp:placeholder id="pnlRichTextBox" Runat="server" Visible="False">
<div align="center">
<editor:FCKeditor id="FCKeditor1" BasePath="~/FCKeditor/" runat="server"></editor:FCKeditor>
</div>
<p align="left">
    <asp:LinkButton class="CommandButton" id="cmdUpdate" visible="true" runat="server"></asp:LinkButton>
    &nbsp; 
    <asp:LinkButton class="CommandButton" id="cmdCancel" visible="true" runat="server" CausesValidation="False"></asp:LinkButton>
</p>
</asp:placeholder>
<asp:placeholder id="privacy" Runat="server" Visible="False">
<asp:Label EnableViewState="false" id="lblPrivacy" runat="server" cssclass="Normal">
<table align="center" width="80%" border="0" >
<tr>
<td>
<br >
<br>
<span class="ItemTitle" style="font: bolder medium">Déclaration de confidentialité</span>
<br>
<br>
{PortalName} est déterminé à offrir un site Web qui respectent la vie privée des visiteurs.
Cette page résume la politique et les pratiques de {PortalName} en ce qui concerne
la protection des renseignements personnels sur le sites Web.
<br>
<br>
<span class="ItemTitle">Renseignements personnels</span>
<br>
<br>
Le site Web de {PortalName} ne saisis pas automatiquement de renseignements personnels
vous concernant expressément comme votre nom, votre numéro de téléphone ou votre adresse
électronique. Nous aurons accès à ce genre de renseignements uniquement si vous nous
les fournissez en les inscrivant dans une section protégée du site. Tous les renseignements
personnels conservés ou saisis par le {PortalName} sont protégés en vertu de la Loi
sur la protection des renseignements personnels. Cela signifie qu'à chaque point de
saisie, on vous demandera votre consentement avant de recueillir des renseignements
vous concernant et on vous informera des fins pour lesquelles ces renseignements sont
saisis et de la façon dont vous pouvez exercer votre droit d'accès à ces renseignements.
<br>
<br>
<span class="ItemTitle">Logiciel de protection</span>
<br>
<br>
{PortalName} utilise un logiciel qui surveille la transmission des données sur le
réseau pour déceler toute tentative non autorisée de télécharger ou de modifier des
renseignements ou de causer d'autres dommages. Ce logiciel reçoit et inscrit le protocole
Internet (PI) de l'ordinateur qui est entré en communication avec notre site Web,
la date et l'heure de la visite et les pages consultées. Nous n'essayons pas d'établir
de liens entre ces adresses et l'identité des personnes qui visitent notre site, à
moins que nous n'ayons décelé une manoeuvre visant à endommager le site.
<br>
<br>
<span class="ItemTitle">Utilisation des témoins (cookies)</span>
<br>
<br>
{PortalName} utilise à l'occasion des « témoins » afin de déterminer comment les visiteurs
utilisent ce site ou les sites qu'ils ont consultés précédemment. Les « témoins »
que nous utilisons ne nous permettent pas d'identifier des personnes. Ils servent
à compiler des statistiques sur les habitudes de transmission des données et à évaluer
l'efficacité du site. Avant d'utiliser un « témoin », nous vous en informerons afin
que vous ayez la possibilité de le refuser; un tel refus n'aura aucun effet sur le
rendement du site et aucune limite ne sera imposée à votre capacité de consulter des
renseignements sur le site. (Un « témoin » est un fichier qui peut être placé à votre 
insu sur votre unité de disque dur et qui sert à surveiller les visites que vous faites à un site.) 
<br>
<br>
<span class="ItemTitle">Sécurité du système</span>
<br>
<br>
L'information relative à chaque visiteur est utilisée par {PortalName} pour répondre
à vos demandes ou assurer la sécurité du système. Nous n'utilisons pas cette information
pour créer des profils pouvant être identifiés individuellement et nous ne la divulguons
à personne de {PortalName}.
<br>
<br>
<span class="ItemTitle">Questions</span>
<br>
<br>
Si vous avez des questions ou des commentaires au sujet de cette politique ou de la
façon dont est appliqué la Loi sur la protection des renseignements personnels, n'hésitez
pas à vous adresser {PortalName} par courriel à {AdministratorEmail}.
Si notre réponse à vos préoccupations au sujet de la protection des renseignements
personnels ne vous satisfait pas, vous pouvez communiquer avec le <a href="mailto:info@privcom.gc.ca">Commissariat
à la protection de la vie privée.</a>
<br>
<br>
<br>
</td>
</tr>
</table>
</asp:Label>
</asp:placeholder>
<asp:literal id="after" runat="server" EnableViewState="false" ></asp:literal>