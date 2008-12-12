dotnetzoom
Pour une nouvelle Installation

1. .NET Framework doit �tre install� (msdn.microsoft.com/netframework/) (version 1.1 ou 2.0)
2. SQL Server 2005, SQL Express, SQL Server 2000 ou MSDE 2000 doit �tre install� et disponible � partir du serveur web (http://www.microsoft.com/sql/msde/default.asp).  Pour une installation maison, SQL Express ou MSDE sont appropri�s.  Assurez vous d�inscrire votre mot de passe pour le compte sa, aussi le mot de passe est sensible aux minuscules et majuscules donc faire attention lors de la cr�ation de l�instance SQL MSDE.
3. D�zipper le fichier dotnetzoom.zip dans le r�pertoire souche du server web C:\inetpub\wwwroot  la structure de r�pertoire suivante sera cr�e.
C:\inetpub\wwwroot\bin
C:\inetpub\wwwroot\portal
C:\inetpub\wwwroot\database
C:\inetpub\wwwroot\Components
C:\inetpub\wwwroot\controls
C:\inetpub\wwwroot\DesktopModules
C:\inetpub\wwwroot\Documentation
C:\inetpub\wwwroot\FCKeditor
C:\inetpub\wwwroot\images
C:\inetpub\wwwroot\javascript
C:\inetpub\wwwroot\Portals
C:\inetpub\wwwroot\PrivateAssemblies
C:\inetpub\wwwroot\templates

3. Cr�er une nouvelle base de donn�e.
4.  Aller dans le r�pertoire C:\inetpub\wwwroot et modifier le param�tre <add key="connectionString" value="SERVER=(local); database=dotnetzoom; uid=sa; pwd=votremotdepasse;" />  dans le fichier web.config.  Modifier le param�tre database, uid et pwd pour y refl�ter les bonnes information de connection � votre base de donn�e.

5. Aller dans votre fureteur et inscrivez http://localhost (pour une installation locale) ou un autre nom de domaine selon votre configuration.  Le logiciel dotnetzoom s�installera automatiquement.

6 .Ouverture d'une session administrateur:

Code d'acc�s: admin
Mot de passe: admin

* Utiliser l'option du menu Admin pour g�rer les options du portail web. Assurez vous de modifier le m�t de passe pour l'administrateur ainsi que d'inscrire une adresse de courriel valide.

Ouverture d'une session webmestre:

Code d'acc�s: webmestre
Mot de passe: webmestre

* Utiliser l'option du menu Host pour g�rer les options globales pour tous les portails du serveur. Assurez vous de modifier le m�t de passe pour le webmestre ainsi que d'inscrire une adresse de courriel valide.