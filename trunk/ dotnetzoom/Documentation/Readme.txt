dotnetzoom
Pour une nouvelle Installation

1. .NET Framework doit être installé (msdn.microsoft.com/netframework/) (version 1.1 ou 2.0)
2. SQL Server 2005, SQL Express, SQL Server 2000 ou MSDE 2000 doit être installé et disponible à partir du serveur web (http://www.microsoft.com/sql/msde/default.asp).  Pour une installation maison, SQL Express ou MSDE sont appropriés.  Assurez vous d’inscrire votre mot de passe pour le compte sa, aussi le mot de passe est sensible aux minuscules et majuscules donc faire attention lors de la création de l’instance SQL MSDE.
3. Dézipper le fichier dotnetzoom.zip dans le répertoire souche du server web C:\inetpub\wwwroot  la structure de répertoire suivante sera crée.
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

3. Créer une nouvelle base de donnée.
4.  Aller dans le répertoire C:\inetpub\wwwroot et modifier le paramètre <add key="connectionString" value="SERVER=(local); database=dotnetzoom; uid=sa; pwd=votremotdepasse;" />  dans le fichier web.config.  Modifier le paramètre database, uid et pwd pour y refléter les bonnes information de connection à votre base de donnée.

5. Aller dans votre fureteur et inscrivez http://localhost (pour une installation locale) ou un autre nom de domaine selon votre configuration.  Le logiciel dotnetzoom s’installera automatiquement.

6 .Ouverture d'une session administrateur:

Code d'accès: admin
Mot de passe: admin

* Utiliser l'option du menu Admin pour gérer les options du portail web. Assurez vous de modifier le môt de passe pour l'administrateur ainsi que d'inscrire une adresse de courriel valide.

Ouverture d'une session webmestre:

Code d'accès: webmestre
Mot de passe: webmestre

* Utiliser l'option du menu Host pour gérer les options globales pour tous les portails du serveur. Assurez vous de modifier le môt de passe pour le webmestre ainsi que d'inscrire une adresse de courriel valide.