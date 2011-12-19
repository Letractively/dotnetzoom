----------------------------------------------------
-- Update
----------------------------------------------------
if not exists ( select * from language where language = 'fr' )
begin
insert language (
       Language,
   Description,
   CultureCode,
   Encoding, 
   HomePage, 
   FriendlyHomePage, 
   AdminRole, 
   AdminRoleDesc, 
   UserRole, 
   UserRoleDesc 
 )
 values (
   'fr',
   'Français (France)',
   'fr-FR',
   'utf-8', 
   'Accueil', 
   'accueil', 
   'Administrateur', 
   'Administration du portail', 
   'Membre', 
   'Usagers inscrits' 
 )
 end
else
begin
 update language
 set 
 Language = 'fr',
 Description = 'Français (France)',
 CultureCode = 'fr-FR',
 Encoding = 'utf-8', 
   HomePage = 'Accueil', 
   FriendlyHomePage = 'accueil', 
   AdminRole = 'Administrateur', 
   AdminRoleDesc = 'Administration du portail', 
   UserRole = 'Membre', 
   UserRoleDesc = 'Usagers inscrits' 
 where language = 'fr'
 end
GO
updatelanguagecontext 'fr','address_region','Province', 'Address'
GO
updatelanguagecontext 'fr','address_city','Ville', 'Address'
GO
updatelanguagecontext 'fr','address_street','Rue', 'Address'
GO
updatelanguagecontext 'fr','address_app','App.#', 'Address'
GO
updatelanguagecontext 'fr','address_postal_code','Code Postal', 'Address'
GO
updatelanguagecontext 'fr','address_telephone','Téléphone', 'Address'
GO
updatelanguagecontext 'fr','address_region_us','États', 'Address'
GO
updatelanguagecontext 'fr','address_postal_code_us','Zip code', 'Address'
GO
updatelanguagecontext 'fr','address_region_ca','Province', 'Address'
GO
updatelanguagecontext 'fr','address_postal_code_ca','Code Postal', 'Address'
GO
updatelanguagecontext 'fr','address_country','Pays', 'Address'
GO
updatelanguagecontext 'fr','address_fax','Fax', 'Address'
GO
updatelanguagecontext 'fr','address_Email','Courriel', 'Address'
GO
updatelanguagecontext 'fr','address_WebSite','Siteweb', 'Address'
GO
updatelanguagecontext 'fr','Demo_Create_Portal','{PortalName} vous offre la possibilité de créer un site web avec {space} d''espace disque pour un période d''essais de {days} jours.  À tout moment si vous désirez avoir de l''aide ou plus d''information S.V.P. me contacter par courriel {AdministratorEmail}', 'AdminMenu'
GO
updatelanguagecontext 'fr','paramêtres','paramètres', 'AdminMenu'
GO
updatelanguagecontext 'fr','haut','Haut', 'AdminMenu'
GO
updatelanguagecontext 'fr','bas','Bas', 'AdminMenu'
GO
updatelanguagecontext 'fr','gauche','Gauche', 'AdminMenu'
GO
updatelanguagecontext 'fr','droite','Droite', 'AdminMenu'
GO
updatelanguagecontext 'fr','actualiser','Actualiser', 'AdminMenu'
GO
updatelanguagecontext 'fr','voir l''aide','Voir l''aide', 'AdminMenu'
GO
updatelanguagecontext 'fr','cacher l''aide','Cacher l''aide', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_menu','Menu Admin', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_services','Cliquer ici pour voir les services offerts', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_profile','Cliquer ici pour modifier votre profil', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_hide_option','Cacher les options de modification des modules', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_show_option','Afficher les options de modification des modules', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_edit_tab','Modifier&nbsp;la page', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_add_tab','Ajouter&nbsp;une&nbsp;page', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_add_module','Ajouter&nbsp;un&nbsp;module', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_clear_caches','Effacer&nbsp;les&nbsp;antémémoires', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_menu_hide','Faire disparaitre le menu', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_hide_info','Faire disparaitre', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_StartHost','Début section webmestre', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_StartPortal','Début section portail', 'AdminMenu'
GO
updatelanguagecontext 'fr','admin_menu_title','Administration du site web', 'AdminMenu'
GO
updatelanguagecontext 'fr','title_TopToolTip','Déplacer le module un niveau supérieur', 'AdminMenu'
GO
updatelanguagecontext 'fr','top','Niveau supérieur', 'AdminMenu'
GO
updatelanguagecontext 'fr','title_bottomToolTip','Déplacer le module un niveau inférieur', 'AdminMenu'
GO
updatelanguagecontext 'fr','bottom','Niveau inférieur', 'AdminMenu'
GO
updatelanguagecontext 'fr','select_this_image','choisir cette image', 'Album'
GO
updatelanguagecontext 'fr','click_to_erase','Cliquer pour effacer l''îcone', 'Album'
GO
updatelanguagecontext 'fr','album_show_icone','Afficher les icônes d''une certaine grandeur seulement', 'Album'
GO
updatelanguagecontext 'fr','label_clicks','Clicks', 'Announcement'
GO
updatelanguagecontext 'fr','see_more','voir&nbsp;plus&nbsp;...', 'Announcement'
GO
updatelanguagecontext 'fr','label_description','Description', 'Announcement'
GO
updatelanguagecontext 'fr','need_description','Vous devez saisir une description de l''annonce', 'Announcement'
GO
updatelanguagecontext 'fr','label_selectlink','Choisir un lien Externe', 'Announcement'
GO
updatelanguagecontext 'fr','label_link','&nbsp;Lien externe', 'Announcement'
GO
updatelanguagecontext 'fr','label_selectIlink','Choisir un lien Interne', 'Announcement'
GO
updatelanguagecontext 'fr','label_Ilink','&nbsp;Lien interne', 'Announcement'
GO
updatelanguagecontext 'fr','label_SelectFile','Choisir un fichier&nbsp;', 'Announcement'
GO
updatelanguagecontext 'fr','label_File','&nbsp;Lien fichier', 'Announcement'
GO
updatelanguagecontext 'fr','command_upload','Télécharger', 'Announcement'
GO
updatelanguagecontext 'fr','label_expiration','Expiration: YYYY-MM-DD', 'Announcement'
GO
updatelanguagecontext 'fr','command_calendar','Calendrier', 'Announcement'
GO
updatelanguagecontext 'fr','label_vieworder','Ordre de parution', 'Announcement'
GO
updatelanguagecontext 'fr','label_update_by','Dernière mise à jour par', 'Announcement'
GO
updatelanguagecontext 'fr','label_update_the','le', 'Announcement'
GO
updatelanguagecontext 'fr','label_see_stat','Voir statistiques?', 'Announcement'
GO
updatelanguagecontext 'fr','header_date','date', 'Announcement'
GO
updatelanguagecontext 'fr','need_title','Vous devez saisir un titre pour faire une annonce', 'Announcement'
GO
updatelanguagecontext 'fr','label_title','Titre', 'Announcement'
GO
updatelanguagecontext 'fr','label_syndicate','*syndication', 'Announcement'
GO
updatelanguagecontext 'fr','Mail_Add_File','Joindre un fichier', 'BulkEMail'
GO
updatelanguagecontext 'fr','Mail_Portal_Role','Profil', 'BulkEMail'
GO
updatelanguagecontext 'fr','Mail_Info','<strong>Note:</strong> Le courriel sera expédié en texte ou Html selon l''option.', 'BulkEMail'
GO
updatelanguagecontext 'fr','Mail_Send','Envoyer le courriel', 'BulkEMail'
GO
updatelanguagecontext 'fr','Mail_Send_Again','Faire parvenir d''autre courriel', 'BulkEMail'
GO
updatelanguagecontext 'fr','Mail_Send_To','Le courriel a été expédié:', 'BulkEMail'
GO
updatelanguagecontext 'fr','today','Aujourd''hui', 'Calendar'
GO
updatelanguagecontext 'fr','send','Envoyer', 'Command'
GO
updatelanguagecontext 'fr','show','Afficher', 'Command'
GO
updatelanguagecontext 'fr','filemanager_security1','{AdministratorEmail}</p>', 'Command'
GO
updatelanguagecontext 'fr','filemanager_security2','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button value="Fermer" onClick="javascript:window.close();">', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_portal','Voulez vous vraiment effacer le portail?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_premium','Si vous voulez ajouter un module Premium à votre portail, s.v.p. contacter votre webmestre', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_unsubscribe','Voulez vous vraiment annuler votre inscription ?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_delete_icone','Voulez vous vraiment effacer l''îcone. Un îcone par défaut sera utilisé, sinon vous pouvez en télécharger un nouveau.', 'Command'
GO
updatelanguagecontext 'fr','Cmd_Reload','Actualiser', 'Command'
GO
updatelanguagecontext 'fr','New_Directory','Nouveau', 'Command'
GO
updatelanguagecontext 'fr','FileExtNotAllowed','<br>L''extension du fichier {fileext} n''est pas autorisé.<br>Vous pouvez seulement utiliser les extensions suivantes :<br>{allowedext}', 'Command'
GO
updatelanguagecontext 'fr','UnZipFile','Décompresser le fichier ZIP?', 'Command'
GO
updatelanguagecontext 'fr','erase','Effacer', 'Command'
GO
updatelanguagecontext 'fr','F_ArticleID','ID article', 'Command'
GO
updatelanguagecontext 'fr','approuve','Approuver', 'Command'
GO
updatelanguagecontext 'fr','reject','Refuser', 'Command'
GO
updatelanguagecontext 'fr','preview','Aperçu', 'Command'
GO
updatelanguagecontext 'fr','download','Télécharger', 'Command'
GO
updatelanguagecontext 'fr','return','Retour', 'Command'
GO
updatelanguagecontext 'fr','upload','Télécharger', 'Command'
GO
updatelanguagecontext 'fr','install','Installer', 'Command'
GO
updatelanguagecontext 'fr','update','Mettre à jour', 'Command'
GO
updatelanguagecontext 'fr','modifier','Modifier', 'Command'
GO
updatelanguagecontext 'fr','add','Ajouter', 'Command'
GO
updatelanguagecontext 'fr','enregistrer','Enregistrer', 'Command'
GO
updatelanguagecontext 'fr','annuler','Annuler', 'Command'
GO
updatelanguagecontext 'fr','visualiser','Visualiser', 'Command'
GO
updatelanguagecontext 'fr','editer','Editer', 'Command'
GO
updatelanguagecontext 'fr','admin_m_add','Ajouter module', 'Command'
GO
updatelanguagecontext 'fr','admin_option_hide','Cacher options', 'Command'
GO
updatelanguagecontext 'fr','admin_option_show','Voir options', 'Command'
GO
updatelanguagecontext 'fr','admin_tab_add','Ajouter page', 'Command'
GO
updatelanguagecontext 'fr','admin_tab_edit','Modifier page', 'Command'
GO
updatelanguagecontext 'fr','admin_txt','Menu admin', 'Command'
GO
updatelanguagecontext 'fr','admin_tab_delete','Effacer la page', 'Command'
GO
updatelanguagecontext 'fr','filemanager_security','<p>Votre profil de sécurité ne vous donne pas accès à cette page.</p><p>Si vous croyez que vous devriez avoir accès à cette page <b>veuillez faire une demande au webmestre du site web {PortalName} </b>', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_erasealbum','Dans l''album vous avez {items} items. Voulez vous vraiment tous les effacer?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm','Voulez vous vraiment l''effacer?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_file_or_rep','Voulez vous effacer ces fichiers ou dossiers?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_erase_role','Voulez vous vraiment effacer ce profil de sécurité ?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_erase_userrole','Voulez vous vraiment enlever ce profil de sécurité à cet usager ?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_erase','Voulez vous vraiment les effacer?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_erase_message','Voulez vous vraiment effacer le message?', 'Command'
GO
updatelanguagecontext 'fr','request_confirm_avatar','Voulez vous continuer avec la configuration de la galerie des avatars?', 'Command'
GO
updatelanguagecontext 'fr','select_icone','choisir un icône', 'Command'
GO
updatelanguagecontext 'fr','delete','Supprimer', 'Command'
GO
updatelanguagecontext 'fr','syndicate','Contenu en syndication', 'Command'
GO
updatelanguagecontext 'fr','admin_caches_x','Purger', 'Command'
GO
updatelanguagecontext 'fr','admin_delete_tab','Effacer page', 'Command'
GO
updatelanguagecontext 'fr','OK','OK', 'Command'
GO
updatelanguagecontext 'fr','Clear','Reset', 'Command'
GO
updatelanguagecontext 'fr','Selected','Sélectionné', 'Command'
GO
updatelanguagecontext 'fr','Highlight','Prévisualisation', 'Command'
GO
updatelanguagecontext 'fr','TwoClickToSelect','Double clicks pour choisir', 'Command'
GO
updatelanguagecontext 'fr','Select_Color','Choisir une couleur', 'Command'
GO
updatelanguagecontext 'fr','Add_Icone','Insérer un icone', 'Command'
GO
updatelanguagecontext 'fr','Add_Image','Insérer une image', 'Command'
GO
updatelanguagecontext 'fr','Flash_Player','Gallery Flash Player', 'Command'
GO
updatelanguagecontext 'fr','Media_Player','Media Player', 'Command'
GO
updatelanguagecontext 'fr','Slideshow_Player','Slideshow', 'Command'
GO
updatelanguagecontext 'fr','Viewer_Image','Visionneuve', 'Command'
GO
updatelanguagecontext 'fr','File_Exploreur','Exploreur', 'Command'
GO
updatelanguagecontext 'fr','File_Exploreur_Warning','Vous avez fermé la fenètre principale.', 'Command'
GO
updatelanguagecontext 'fr','Name','Nom', 'Contact'
GO
updatelanguagecontext 'fr','contact_title','Rôle', 'Contact'
GO
updatelanguagecontext 'fr','contact_email','Courriel', 'Contact'
GO
updatelanguagecontext 'fr','contact_telephone','Téléphone', 'Contact'
GO
updatelanguagecontext 'fr','label_message_body','Message', 'Discussion'
GO
updatelanguagecontext 'fr','label_message_object','Sujet', 'Discussion'
GO
updatelanguagecontext 'fr','label_message_user','Auteur', 'Discussion'
GO
updatelanguagecontext 'fr','label_message_date','Date', 'Discussion'
GO
updatelanguagecontext 'fr','Reply','Répondre', 'Discussion'
GO
updatelanguagecontext 'fr','label_close','Fermer', 'Discussion'
GO
updatelanguagecontext 'fr','label_no_answer','Aucune réponse', 'Discussion'
GO
updatelanguagecontext 'fr','label_open','Ouvrir', 'Discussion'
GO
updatelanguagecontext 'fr','label_anonymous','Visiteur', 'Discussion'
GO
updatelanguagecontext 'fr','new_message_re','RE:', 'Discussion'
GO
updatelanguagecontext 'fr','message_le','le', 'Discussion'
GO
updatelanguagecontext 'fr','message_de','De', 'Discussion'
GO
updatelanguagecontext 'fr','header_title','Titre', 'Documents'
GO
updatelanguagecontext 'fr','header_what','Quoi', 'Documents'
GO
updatelanguagecontext 'fr','header_when','Quand', 'Documents'
GO
updatelanguagecontext 'fr','header_size','(KB)', 'Documents'
GO
updatelanguagecontext 'fr','select_internal_doc','Choisir un document interne', 'Documents'
GO
updatelanguagecontext 'fr','internal_doc','Document interne', 'Documents'
GO
updatelanguagecontext 'fr','select_external_doc','Choisir un document externe', 'Documents'
GO
updatelanguagecontext 'fr','external_doc','Document externe', 'Documents'
GO
updatelanguagecontext 'fr','type_doc','Catégorie', 'Documents'
GO
updatelanguagecontext 'fr','header_who','Qui', 'Documents'
GO
updatelanguagecontext 'fr','channel_syndicate','* Syndication cannal RSS', 'Documents'
GO
updatelanguagecontext 'fr','Account_Mod','Modification dossier', 'EmailNotice'
GO
updatelanguagecontext 'fr','Bad_IPTXT','Tentative d''intrusion par IP : {0}
Url de : {1}
Url demandé : {2}
User Agent : {3}
User ID : {4}
L''adresse IP sera bloqué pour une heure', 'EmailNotice'
GO
updatelanguagecontext 'fr','Bad_IP','Tentative d''intrusion', 'EmailNotice'
GO
updatelanguagecontext 'fr','Register_request','Demande d''adhésion', 'EmailNotice'
GO
updatelanguagecontext 'fr','Reset_PasswordTXT','Cher {0},

Votre mot de passe a été modifié a cause d''une difficulté cryptographique du système.
Nous vous demandons d''utiliser les informations suivantes lors de votre prochaine visite:
Adresse web portail:{1}
Code d''accès:       {2}
Mot de passe:', 'EmailNotice'
GO
updatelanguagecontext 'fr','Member_Quit','adhésion révoquée', 'EmailNotice'
GO
updatelanguagecontext 'fr','Mod_Password','Mot de passe modifié', 'EmailNotice'
GO
updatelanguagecontext 'fr','Email_Test','test de la configuration du courriel', 'EmailNotice'
GO
updatelanguagecontext 'fr','Demo_Portal','portail web de démonstration', 'EmailNotice'
GO
updatelanguagecontext 'fr','New_Portal','nouveau site web', 'EmailNotice'
GO
updatelanguagecontext 'fr','Password_Notice','Votre mot de passe pour', 'EmailNotice'
GO
updatelanguagecontext 'fr','Vendor_request','Demande d''un fournisseur', 'EmailNotice'
GO
updatelanguagecontext 'fr','PMS_message','Message sur', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_NewMessage','Nouveau message:', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_Moderated_message','Message en attente d''approbation:', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_Moderated_approved','Message approuvé:', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_Moderated_refused','Message refusé par modérateur:', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_Moderated_erased','Message effacé par modérateur:', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_NewMessageTXT','Un nouveau message de <b>{0}</b><br>est disponible dans le forum <b>{1}</b>.<br>', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_ModMessageTXT','Le message <b>{0}</b> a été modifié par <b>{1}</b><br>dans le forum <b>{2}</b>.<br>', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_ReplayMessageTXT','Une réponse au message <b>{0}</b><br>a été fait par <b>{1}</b><br>dans le forum <b>{2}</b>.<br>', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_BodyMessageTXT','<b>Sujet: {0}</b><br><b>Message:</b><br>{1}<br>Cliquer<a href="{2}"><b> ici </b></a>pour aller lire ce message.', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_Moderated_messageTXT','Un nouveau fil de discussion a été créé <b>{0}</b> dans le forum <b>{1}</b>.<br>pour votre approbation.<br>Cliquer <a href="{2}">ici</a> pour aller au forum."', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_Moderated_approvedTXT','A {0}<br>vous avez sauvegardé ce message : <b>{1}</b> dans le forum <b>{2}</b>.<br>Ce forum est dirigé.<br>Ce courriel est pour vous informer que votre message est <b>approuvé</b> : {3}<br><br>Merci de votre participation du personnel de <br><b>{4}</b>', 'EmailNotice'
GO
updatelanguagecontext 'fr','Forum_Moderated_refusedTXT','A {0}<br>Vous avez créé un nouveau message : <b>{1}</b> dans le forum <b>{2}</b>.<br>Ce forum est dirigé.<br>Ce courriel est pour vous informer que votre message est <b>refusé</b> : {3}<br><br>Merci de votre participation du personnel de <b>{4}</b>', 'EmailNotice'
GO
updatelanguagecontext 'fr','SiteLogOff','Le webmestre a désactivé cette fonction pour votre portail.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','SiteLogLimited','Le webmestre a limité à {days} jours d''information d''utilisation.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','Portal_Expired','L''entente d''hébergement pour le site web {PortalName} prend fin le {ExpiryDate}. Pour plus d''information {AdministratorEmail}.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','Need_Directory_Name','Vous devez saisir un nom pour créer un nouveau répertoire', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_integer','Vous devez utiliser un nombre valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_decimal','Vous devez utiliser un nombre avec décimal valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','Valid_Date_Format','Vous devez utiliser un format de date valide tel que AAAA/MM/JJ', 'ErrorMessage'
GO
updatelanguagecontext 'fr','Valid_Boolean','Vous devez utiliser une valeur True/False seulement', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_city_name','Le nom de la ville est requis.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_telephone','Votre numéro de téléphone est requis.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_module_def','Entrer la nouvelle définition du module', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_portal_name','Le nom du portail est requis.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_firstname','Un prénom est requis.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_lastname','Votre nom est requis.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_password_confirm','Vous devez confirmer la validité du mot de passe.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_password_match','Le mot de passe ne concorde pas.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_vendor_name','Le nom de la compagnie est requise', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_a_valide_name','Vous devez utiliser un nom valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_object_message','Vous devez saisir un titre pour le message', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_body_message','Vous devez saisir du texte pour le corps du message', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_location_mapquest','Vous devez indiquer un endroit', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_number','Doit être un numéro valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_country_code','Le code du pays est requis', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_region_code','La province est requise.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_username','Un code d''accès est requis.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_password_minimum','Vous devez choisir un mot de passe avec un minimum de 8 lettres ou chiffres', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_password','Un mot de passe est requis.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_email','Une adresse de courriel est requise.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','not_a_date','Mauvaise date!', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_number_purchase','Vous devez indiquer le nombre d''unités que vous voulez acheter', 'ErrorMessage'
GO
updatelanguagecontext 'fr','bad_value_services0','les frais de services doit être supérieur à 0', 'ErrorMessage'
GO
updatelanguagecontext 'fr','bad_billing_period','La période n''est pas valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','bad_billing_period0','La période de facturation doit être supérieur à  0', 'ErrorMessage'
GO
updatelanguagecontext 'fr','bad_trial_fee','Vous devez saisir un frais d''essais valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','bad_trial_fee0','Les frais d''essais doivent être plus que 0', 'ErrorMessage'
GO
updatelanguagecontext 'fr','bad_trial_period','La valeur n''est pas valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','bad_trial_period0','Vous devez saisir un valeur de plus que 0', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_region_name','Le nom de la province est requis.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_street_name','Le nom de la rue est requis', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_postal_code','Le code postal est requis', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_username_minimum','Vous devez choisir un code d''accès avec un minimum de 8 lettres ou chiffres', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_valid_emai','L''adresse de courriel n''est pas valide.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_tab_name','Un nom de page est requis', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_role_name','Vous devez saisir un nom.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','bad_value_services','La valeur des frais de services n''est pas bonne', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_banner_name','Vous devez saisir un nom pour l''oriflamme', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_exposition_number','Vous devez saisir un nombre d''expositions valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_CPM_number','Vous devez saisir un nombre CPM valide', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_valid_description','une description est requise', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_alt','un texte alternatif est requis', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_start_date','Une date de début est requise', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_country_name','Le nom du pays est requis!', 'ErrorMessage'
GO
updatelanguagecontext 'fr','nossl','Normalement cette page ne devrait pas être chiffrée. Le site Web s''est correctement identifié et les informations saisies sur cette page ne pourront pas facilement être lues lors de leur acheminement. (Cliquer sur OK pour continuer..)', 'ErrorMessage'
GO
updatelanguagecontext 'fr','ssl','Normalement cette page devrait être chiffrée. Donc, les informations que vous enverrez et recevrez pourraient éventuellement être lues lors de leur acheminement. (Cliquer sur OK pour aller vers une page sécurisée ...)', 'ErrorMessage'
GO
updatelanguagecontext 'fr','need_valid_email','Une adresse de courriel valide est requise.', 'ErrorMessage'
GO
updatelanguagecontext 'fr','calendar','Calendrier', 'Events'
GO
updatelanguagecontext 'fr','events_format','Montrer le format', 'Events'
GO
updatelanguagecontext 'fr','events_list','Liste', 'Events'
GO
updatelanguagecontext 'fr','calendar_cel_width','Largeur&nbsp;cellule', 'Events'
GO
updatelanguagecontext 'fr','calendar_cel_height','Hauteur cellule', 'Events'
GO
updatelanguagecontext 'fr','select_icone_tooltip','Il est recommandé d''utiliser un icône de  20px X 20px maximum', 'Events'
GO
updatelanguagecontext 'fr','events_interval','Intervalles', 'Events'
GO
updatelanguagecontext 'fr','label_icone','Icône', 'Events'
GO
updatelanguagecontext 'fr','label_alt','Texte alternatif', 'Events'
GO
updatelanguagecontext 'fr','label_period','Périodicité', 'Events'
GO
updatelanguagecontext 'fr','events_day','Jour(s)', 'Events'
GO
updatelanguagecontext 'fr','events_week','Semaine (s)', 'Events'
GO
updatelanguagecontext 'fr','events_month','Mois', 'Events'
GO
updatelanguagecontext 'fr','events_year','Année (s)', 'Events'
GO
updatelanguagecontext 'fr','start_date','Date début', 'Events'
GO
updatelanguagecontext 'fr','start_hour','Heure', 'Events'
GO
updatelanguagecontext 'fr','end_date','Date&nbsp;fin', 'Events'
GO
updatelanguagecontext 'fr','exif_width_height','Dimensions:', 'Exif'
GO
updatelanguagecontext 'fr','exif_resolution','Résolution:', 'Exif'
GO
updatelanguagecontext 'fr','exif_orientation','Orientation:', 'Exif'
GO
updatelanguagecontext 'fr','exif_title','Titre:', 'Exif'
GO
updatelanguagecontext 'fr','exif_description','Description:', 'Exif'
GO
updatelanguagecontext 'fr','exif_copyright','Copyright:', 'Exif'
GO
updatelanguagecontext 'fr','exif_artist','Propriétaire:', 'Exif'
GO
updatelanguagecontext 'fr','exif_equipement','Appareil Photo:', 'Exif'
GO
updatelanguagecontext 'fr','exif_equipement_maker','Fabricant:', 'Exif'
GO
updatelanguagecontext 'fr','exif_equipement_model','Modèle:', 'Exif'
GO
updatelanguagecontext 'fr','exif_software','Logiciel:', 'Exif'
GO
updatelanguagecontext 'fr','exif_date_time','Date et heure:', 'Exif'
GO
updatelanguagecontext 'fr','exif_date_time_last_mod','General:', 'Exif'
GO
updatelanguagecontext 'fr','exif_date_time_created','Original:', 'Exif'
GO
updatelanguagecontext 'fr','exif_date_time_digitized','Numérisée:', 'Exif'
GO
updatelanguagecontext 'fr','exif_photo_param','Paramètres de la photographie:', 'Exif'
GO
updatelanguagecontext 'fr','exif_ExposureTime','Temps de pose:', 'Exif'
GO
updatelanguagecontext 'fr','exif_ExposureProgram','Exposition:', 'Exif'
GO
updatelanguagecontext 'fr','exif_ExposureMeteringMode','Mode exposition:', 'Exif'
GO
updatelanguagecontext 'fr','exif_Aperture','Diaphragme:        F', 'Exif'
GO
updatelanguagecontext 'fr','exif_iso','ISO sensitivité:', 'Exif'
GO
updatelanguagecontext 'fr','exif_SubjectDistance','Distance:', 'Exif'
GO
updatelanguagecontext 'fr','exif_FocalLength','Focale:', 'Exif'
GO
updatelanguagecontext 'fr','exif_flash','Flash:', 'Exif'
GO
updatelanguagecontext 'fr','exif_LightSource','Source lumière(WB):', 'Exif'
GO
updatelanguagecontext 'fr','question','Q.', 'Faqs'
GO
updatelanguagecontext 'fr','answer','Réponse :', 'Faqs'
GO
updatelanguagecontext 'fr','label_question','Question', 'Faqs'
GO
updatelanguagecontext 'fr','label_answer','Réponse', 'Faqs'
GO
updatelanguagecontext 'fr','to','À', 'Feedback'
GO
updatelanguagecontext 'fr','email','Courriel', 'Feedback'
GO
updatelanguagecontext 'fr','your_name','Votre nom', 'Feedback'
GO
updatelanguagecontext 'fr','object','Objet', 'Feedback'
GO
updatelanguagecontext 'fr','tooltip_feedback','Enregister l''adresse de courriel par défaut!', 'Feedback'
GO
updatelanguagecontext 'fr','tooltip_send','Faire parvenir le courriel au webmestre!', 'Feedback'
GO
updatelanguagecontext 'fr','FeedBack_Mail_Send','Votre message a été envoyé', 'Feedback'
GO
updatelanguagecontext 'fr','FeedBack_New_Add','Veuillez nous indiquer votre adresse de courriel', 'Feedback'
GO
updatelanguagecontext 'fr','F_New_Folder','Créer un nouveau dossier', 'File'
GO
updatelanguagecontext 'fr','F_Upload','Télécharger fichier(s) dans ce dossier', 'File'
GO
updatelanguagecontext 'fr','F_Download','Télécharger les fichier(s)', 'File'
GO
updatelanguagecontext 'fr','F_delete','Supprimer fichier(s)/Dossier(s)', 'File'
GO
updatelanguagecontext 'fr','F_Rename','Renommer ce fichier/dossier', 'File'
GO
updatelanguagecontext 'fr','F_Header_Mod','Modifié le', 'File'
GO
updatelanguagecontext 'fr','F_Header_Created','Créé le', 'File'
GO
updatelanguagecontext 'fr','F_Header_Size','Taille', 'File'
GO
updatelanguagecontext 'fr','F_cmdSynchronize','Mettre à jour la basse de donné', 'File'
GO
updatelanguagecontext 'fr','F_Upload_Auth','Autorisation de télécharger', 'File'
GO
updatelanguagecontext 'fr','F_Update','Mettre à jour les autorisations de télécharger', 'File'
GO
updatelanguagecontext 'fr','F_Refresh','Actualiser', 'File'
GO
updatelanguagecontext 'fr','WriteError','Le fichier n''a pas été sauvegarder.  Assurez vous d''avoir l''accès écriture au répertoire.', 'File'
GO
updatelanguagecontext 'fr','F_ParentDir','Dossier parent', 'File'
GO
updatelanguagecontext 'fr','F_Header_Type','Type', 'File'
GO
updatelanguagecontext 'fr','F_Header_Name','Nom', 'File'
GO
updatelanguagecontext 'fr','F_Directory','Répertoire', 'File'
GO
updatelanguagecontext 'fr','F_FileName','Fichier:', 'File'
GO
updatelanguagecontext 'fr','F_btnUpload','Télécharger', 'File'
GO
updatelanguagecontext 'fr','F_UploadInDirec','Le téléchargement des fichiers se fera dans le répertoire :', 'File'
GO
updatelanguagecontext 'fr','F_UploadFile','Télécharger fichier', 'File'
GO
updatelanguagecontext 'fr','F_UploadFileList','Premier fichier', 'File'
GO
updatelanguagecontext 'fr','cmdRemove','Effacer', 'File'
GO
updatelanguagecontext 'fr','F_UnzipFile','Décompresser fichier ZIP?', 'File'
GO
updatelanguagecontext 'fr','hypHost','Réalisé par {hostname}', 'Footer'
GO
updatelanguagecontext 'fr','hypTerms','Conditions générales', 'Footer'
GO
updatelanguagecontext 'fr','hypPrivacy','Déclarations de confidentialité', 'Footer'
GO
updatelanguagecontext 'fr','F_NewMP','Nouveau message dans le forum <b>{forum}</b>', 'Forum'
GO
updatelanguagecontext 'fr','F_ModerateForum','Forum dirigé', 'Forum'
GO
updatelanguagecontext 'fr','F_Created','Crée le', 'Forum'
GO
updatelanguagecontext 'fr','F_NeedF','Vous devez choisir un forum avant d''ajouter un nouveau sujet de discussion', 'Forum'
GO
updatelanguagecontext 'fr','F_NoGal','La galerie n''est pas configurée sur ce forum!', 'Forum'
GO
updatelanguagecontext 'fr','F_NoGalR','Le répertoire de la galerie est introuvable, cette fonction est désactivée', 'Forum'
GO
updatelanguagecontext 'fr','F_EditPost','Modifier le message', 'Forum'
GO
updatelanguagecontext 'fr','F_QuotePost','Citer ce message', 'Forum'
GO
updatelanguagecontext 'fr','F_QuotePostA','Citer', 'Forum'
GO
updatelanguagecontext 'fr','F_PostReply','Répondre au message', 'Forum'
GO
updatelanguagecontext 'fr','F_PostReplyA','Répondre', 'Forum'
GO
updatelanguagecontext 'fr','F_PMSShort','Raccourci', 'Forum'
GO
updatelanguagecontext 'fr','F_ForumSearch','Recherche Forums', 'Forum'
GO
updatelanguagecontext 'fr','F_CheckMail','Me faire parvenir un courriel si un message est ajouté', 'Forum'
GO
updatelanguagecontext 'fr','F_SendPMSTo','Écrire un message à', 'Forum'
GO
updatelanguagecontext 'fr','F_SeeProfileof','Voir le profile de', 'Forum'
GO
updatelanguagecontext 'fr','F_VisitWWWof','Visiter le site web de', 'Forum'
GO
updatelanguagecontext 'fr','F_Posted','Affiché:', 'Forum'
GO
updatelanguagecontext 'fr','F_PModified','Message a été modifié', 'Forum'
GO
updatelanguagecontext 'fr','F_PModifiedA','Modifié', 'Forum'
GO
updatelanguagecontext 'fr','F_LastModified','(Modifié par {author} le {datetime})', 'Forum'
GO
updatelanguagecontext 'fr','F_ErasePost','Effacer le message', 'Forum'
GO
updatelanguagecontext 'fr','F_NotAvailable','Pas disponibile présentement. S.V.P. utiliser la recherche simple', 'Forum'
GO
updatelanguagecontext 'fr','F_Stats','Mise à jour stats', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsH','(heure)', 'Forum'
GO
updatelanguagecontext 'fr','F_UOInt','Integration User Online', 'Forum'
GO
updatelanguagecontext 'fr','F_UOuse','Utiliser User Online?', 'Forum'
GO
updatelanguagecontext 'fr','F_GalInt','Paramètres intégrés galerie', 'Forum'
GO
updatelanguagecontext 'fr','F_GalIntInfo','(Utiliser la configuration des paramètres de la galeries)', 'Forum'
GO
updatelanguagecontext 'fr','F_GalHeight','Hauteur image', 'Forum'
GO
updatelanguagecontext 'fr','F_GalWith','Largeur image', 'Forum'
GO
updatelanguagecontext 'fr','F_GalImgT','Type d''image', 'Forum'
GO
updatelanguagecontext 'fr','F_UserName','Nom', 'Forum'
GO
updatelanguagecontext 'fr','F_UserRTE','Éditeur texte enrichi?', 'Forum'
GO
updatelanguagecontext 'fr','F_UserRTEInfo','Fonctionne avec Microsoft Internet Explorer et Mozilla FireFox.', 'Forum'
GO
updatelanguagecontext 'fr','F_UserSelectAv','Choisir avatar', 'Forum'
GO
updatelanguagecontext 'fr','F_UserOwnAv','Utiliser son avatar', 'Forum'
GO
updatelanguagecontext 'fr','F_UserUploadAv','Télécharger son avatar', 'Forum'
GO
updatelanguagecontext 'fr','F_Avatar','Avatar', 'Forum'
GO
updatelanguagecontext 'fr','F_Skin','Habillage', 'Forum'
GO
updatelanguagecontext 'fr','F_UserEmail','Adresse de courriel?', 'Forum'
GO
updatelanguagecontext 'fr','F_UserEmailInfo','Cocher cette option pour que votre adresse de courriel soit visible et afficher aux autres usagers.', 'Forum'
GO
updatelanguagecontext 'fr','F_UserShowUO','En ligne?', 'Forum'
GO
updatelanguagecontext 'fr','F_UserShowUOInfo','Cocher cette option pour que votre pseudonyme soit affiché quand vous serez en ligne.', 'Forum'
GO
updatelanguagecontext 'fr','F_UserSignature','Signature?', 'Forum'
GO
updatelanguagecontext 'fr','F_UserTime','Fuseau horaire?', 'Forum'
GO
updatelanguagecontext 'fr','F_VisitUserSite','Visiter', 'Forum'
GO
updatelanguagecontext 'fr','F_SendEMailToUser','Faire parvenir un courriel à', 'Forum'
GO
updatelanguagecontext 'fr','F_UserProfile','Profil usager', 'Forum'
GO
updatelanguagecontext 'fr','F_UserProfileParam','Paramètres usager', 'Forum'
GO
updatelanguagecontext 'fr','F_Select','Choisir le forum', 'Forum'
GO
updatelanguagecontext 'fr','F_Close','Fermer', 'Forum'
GO
updatelanguagecontext 'fr','F_PostPage','Message par page', 'Forum'
GO
updatelanguagecontext 'fr','F_Since','Depuis', 'Forum'
GO
updatelanguagecontext 'fr','F_Validated','A validé', 'Forum'
GO
updatelanguagecontext 'fr','F_ModerateNotice','Avis par courriel', 'Forum'
GO
updatelanguagecontext 'fr','F_ModerateEMail','Courriel', 'Forum'
GO
updatelanguagecontext 'fr','F_UserAdress','Adresse', 'Forum'
GO
updatelanguagecontext 'fr','F_UserCountry','Pay', 'Forum'
GO
updatelanguagecontext 'fr','F_UserTime0','Fuseau horaire', 'Forum'
GO
updatelanguagecontext 'fr','F_AvMaxS','Taille max Avatar', 'Forum'
GO
updatelanguagecontext 'fr','F_GotPMS','Vous n''avez aucun message dans votre boîte de réception', 'Forum'
GO
updatelanguagecontext 'fr','F_PMSCount','Vous avez {pmscount} messages dans votre boîte de réception', 'Forum'
GO
updatelanguagecontext 'fr','F_UOError','Impossible de trouver l''info sur les usagers en ligne.', 'Forum'
GO
updatelanguagecontext 'fr','F_Anonymous','Vous n''êtes pas incrit.', 'Forum'
GO
updatelanguagecontext 'fr','F_AnonymousClick','Vous pouvez vous incrire ici', 'Forum'
GO
updatelanguagecontext 'fr','F_approve','Approuver', 'Forum'
GO
updatelanguagecontext 'fr','F_AvDir','Répertoire Avatars', 'Forum'
GO
updatelanguagecontext 'fr','F_AvID','ID Module Avatars', 'Forum'
GO
updatelanguagecontext 'fr','F_AvInfo','Cliquer ici pour configurer la galeries des avatars!', 'Forum'
GO
updatelanguagecontext 'fr','F_AvInfo1','auto configuration de la galerie des Avatars...', 'Forum'
GO
updatelanguagecontext 'fr','F_AvInfo2','Vous devez créer une galerie de photos avant de pouvoir l''intéger au forum.<br>Une fois la galerie de photos créer aller dans l''option paramètres et cocher la case ""Ceci est la galerie des avatars?', 'Forum'
GO
updatelanguagecontext 'fr','F_AvYes','Permettre Avatars?', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailYes','Paramètres avis courriel', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailInfo','(Pour aviser les usagers des nouveaux messages le forum peut faire parvenir un courriel aux usagers)', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailS','Avis courriel?', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailInfo1','Permettre au forum de faire parvenir un                                     avis par courriel lors d''ajout d''un message ou modification.', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailAdd','Adresse courriel', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailInfo2','Faire paraître l''adresse de courriel personnel de l''usager dans le champ <b>de:</b> du courriel sortant sinon utiliser l''adresse automatique:', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailAut','Adresse automatique', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailDom','Domaine courriel', 'Forum'
GO
updatelanguagecontext 'fr','F_EMailFormat','Format courriel', 'Forum'
GO
updatelanguagecontext 'fr','F_ForumFormat','Paramètres d''affichage', 'Forum'
GO
updatelanguagecontext 'fr','F_ThreadP','Fils par page', 'Forum'
GO
updatelanguagecontext 'fr','F_Validate','Valider', 'Forum'
GO
updatelanguagecontext 'fr','F_PostDate','Date', 'Forum'
GO
updatelanguagecontext 'fr','F_AddNewThread','Ajouter un nouveau fil de discussion', 'Forum'
GO
updatelanguagecontext 'fr','F_Moderators','Modérateurs forum', 'Forum'
GO
updatelanguagecontext 'fr','F_Remove','Enlever', 'Forum'
GO
updatelanguagecontext 'fr','F_SeeDetails','Voir détails ...', 'Forum'
GO
updatelanguagecontext 'fr','F_Deleted','Le message a été supprimé par {authorName} le :', 'Forum'
GO
updatelanguagecontext 'fr','F_RE','Re:', 'Forum'
GO
updatelanguagecontext 'fr','F_NeedObject','Un message doit avoir un sujet', 'Forum'
GO
updatelanguagecontext 'fr','cmdModerate','Diriger', 'Forum'
GO
updatelanguagecontext 'fr','F_SelectIcon','Choisir émoticon', 'Forum'
GO
updatelanguagecontext 'fr','F_NoConfig','Impossible de trouver la configuration de la galerie des avatars', 'Forum'
GO
updatelanguagecontext 'fr','F_Stats24','Dans les derniers 24 heures', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsMost','Le fil de discussion le plus vue est', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsMostPost','Le fil de discussion avec le plus de messages est', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsTop10','Les 10 usagers les plus actifs sont', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsUser','usagers ont contribués', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsNThread','fils de discussions et', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsNPost','messages ont été ajoutés.', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsNewThread','nouveaux fils de discussions et', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsNewPost','nouveaux messages.', 'Forum'
GO
updatelanguagecontext 'fr','F_SubForum','Abonnement forum', 'Forum'
GO
updatelanguagecontext 'fr','F_SubParamEMail','Paramètres avis courriel', 'Forum'
GO
updatelanguagecontext 'fr','F_SubParamEMailInfo','(Vous serez aviser par courriel des nouveaux messages selon les abonnements que vous ferez.)', 'Forum'
GO
updatelanguagecontext 'fr','F_Subscribe','S''abonner', 'Forum'
GO
updatelanguagecontext 'fr','F_UnSubscribe','Annuler l''abonnement...', 'Forum'
GO
updatelanguagecontext 'fr','F_Audit','Audit', 'Forum'
GO
updatelanguagecontext 'fr','F_AdminUser','Admin usagers forum', 'Forum'
GO
updatelanguagecontext 'fr','F_UserID','ID usager', 'Forum'
GO
updatelanguagecontext 'fr','F_lnkProfile','Voir profil', 'Forum'
GO
updatelanguagecontext 'fr','F_UserCode','Code d''accès', 'Forum'
GO
updatelanguagecontext 'fr','F_UserAlias','Pseudonyme', 'Forum'
GO
updatelanguagecontext 'fr','F_UserMode','Modérateur', 'Forum'
GO
updatelanguagecontext 'fr','F_UserThread','Suivre fils discussions?', 'Forum'
GO
updatelanguagecontext 'fr','F_UserPMS','Messagerie?', 'Forum'
GO
updatelanguagecontext 'fr','F_UserTrust','Confiance?', 'Forum'
GO
updatelanguagecontext 'fr','F_UserWork','Travail', 'Forum'
GO
updatelanguagecontext 'fr','F_UserInterest','Champs d''intérêts', 'Forum'
GO
updatelanguagecontext 'fr','F_UserInfoUser','Information usager', 'Forum'
GO
updatelanguagecontext 'fr','F_UserEmail0','Courriel', 'Forum'
GO
updatelanguagecontext 'fr','F_UserURL','URL', 'Forum'
GO
updatelanguagecontext 'fr','F_UserMSN','MSN', 'Forum'
GO
updatelanguagecontext 'fr','F_UserYAHOO','YAHOO', 'Forum'
GO
updatelanguagecontext 'fr','F_UserAIM','AIM', 'Forum'
GO
updatelanguagecontext 'fr','F_UserICQ','ICQ', 'Forum'
GO
updatelanguagecontext 'fr','F_UserStats','Statistiques', 'Forum'
GO
updatelanguagecontext 'fr','F_SendEMailTo','Faire parvenir un message à', 'Forum'
GO
updatelanguagecontext 'fr','F_Contributed','{username} a contribué {numpost} messages.  Dernier message écrit le {datelast}.', 'Forum'
GO
updatelanguagecontext 'fr','F_LocalTime','heure locale', 'Forum'
GO
updatelanguagecontext 'fr','F_NoAvrRep','Impossible de trouver le répertoire des avatars, cette fonctions n''est pas activée', 'Forum'
GO
updatelanguagecontext 'fr','F_SelectAvatar','<Choisir Avatar>', 'Forum'
GO
updatelanguagecontext 'fr','F_AliasUsed','Le pseudonyme est déjà  utilisé par un autre usager', 'Forum'
GO
updatelanguagecontext 'fr','F_FileNameUsed','Ce nom de fichier est déjà  utilisé, veuillez en choissier un autre', 'Forum'
GO
updatelanguagecontext 'fr','F_NoSpaceLeft','Aucune espace disque de disponible, contacter le webmestre', 'Forum'
GO
updatelanguagecontext 'fr','F_ClickToModProfile','Cliquer ici pour modifier votre profil usager pour le forum...', 'Forum'
GO
updatelanguagecontext 'fr','F_EditMode','Mode édition', 'Forum'
GO
updatelanguagecontext 'fr','F_cmdAdmin','Admin', 'Forum'
GO
updatelanguagecontext 'fr','F_ImgDir','Répertoire Images', 'Forum'
GO
updatelanguagecontext 'fr','F_GoToPMS','Aller dans la messagerie', 'Forum'
GO
updatelanguagecontext 'fr','F_UserSelect','Choisir usager', 'Forum'
GO
updatelanguagecontext 'fr','F_NewMessages','Nouveau(x) message(s)', 'Forum'
GO
updatelanguagecontext 'fr','F_NewMessages0','Nouveau(x)', 'Forum'
GO
updatelanguagecontext 'fr','F_NoNewMessage','Aucun nouveau message', 'Forum'
GO
updatelanguagecontext 'fr','F_NoNewMessage0','Aucun nouveau', 'Forum'
GO
updatelanguagecontext 'fr','F_none','Aucun', 'Forum'
GO
updatelanguagecontext 'fr','F_NoPost','Aucun résultat', 'Forum'
GO
updatelanguagecontext 'fr','F_NoUser','Aucun usager trouvé!', 'Forum'
GO
updatelanguagecontext 'fr','F_NoUserFound','Le nom du destinataire n''existe pas, en choisir un autre!', 'Forum'
GO
updatelanguagecontext 'fr','F_SearchResult','Resultats recherche', 'Forum'
GO
updatelanguagecontext 'fr','F_PostFrom','Message de', 'Forum'
GO
updatelanguagecontext 'fr','F_DeletedBy','Sujet du message {object} Le message a été supprimé par {authorName}', 'Forum'
GO
updatelanguagecontext 'fr','F_CountForumGroup','{forumscount} forums dans {groupscount} sections', 'Forum'
GO
updatelanguagecontext 'fr','F_sThread','Fil de discussion', 'Forum'
GO
updatelanguagecontext 'fr','F_SBy','Débuté par', 'Forum'
GO
updatelanguagecontext 'fr','F_Answer','Réponses', 'Forum'
GO
updatelanguagecontext 'fr','F_Seen','Vues', 'Forum'
GO
updatelanguagecontext 'fr','F_Prev','Précédent', 'Forum'
GO
updatelanguagecontext 'fr','F_Next','Prochain', 'Forum'
GO
updatelanguagecontext 'fr','F_NoThread','Aucun fil de discussion', 'Forum'
GO
updatelanguagecontext 'fr','F_Paging','Page {pagenum} de {numpage}', 'Forum'
GO
updatelanguagecontext 'fr','F_PinnedPost','Message important', 'Forum'
GO
updatelanguagecontext 'fr','F_Pinned','Important', 'Forum'
GO
updatelanguagecontext 'fr','F_AThread','Fils de discussion très actif!', 'Forum'
GO
updatelanguagecontext 'fr','F_aAThread','Actif', 'Forum'
GO
updatelanguagecontext 'fr','F_AThread0','Fil de discussion actif', 'Forum'
GO
updatelanguagecontext 'fr','F_aAThread0','Normal', 'Forum'
GO
updatelanguagecontext 'fr','F_Page','Page:', 'Forum'
GO
updatelanguagecontext 'fr','F_NewPost','Nouveau fil de discussion', 'Forum'
GO
updatelanguagecontext 'fr','F_NewPostA','Nouveau', 'Forum'
GO
updatelanguagecontext 'fr','F_EditYPost','Modifier votre message', 'Forum'
GO
updatelanguagecontext 'fr','F_EditYPostA','Modifier', 'Forum'
GO
updatelanguagecontext 'fr','F_Who','par', 'Forum'
GO
updatelanguagecontext 'fr','F_PostSubject','Fil de discussion:', 'Forum'
GO
updatelanguagecontext 'fr','F_NoMessage','Aucun message dans ce fil de discussion', 'Forum'
GO
updatelanguagecontext 'fr','F_Auteur','Auteur', 'Forum'
GO
updatelanguagecontext 'fr','F_Object','Sujet', 'Forum'
GO
updatelanguagecontext 'fr','F_ToSee','Message à voir?', 'Forum'
GO
updatelanguagecontext 'fr','F_AddImo','Ajouter émoticon', 'Forum'
GO
updatelanguagecontext 'fr','F_Image','Image', 'Forum'
GO
updatelanguagecontext 'fr','F_RNotice','Avis de réponses', 'Forum'
GO
updatelanguagecontext 'fr','F_Preview','Aperçu du message', 'Forum'
GO
updatelanguagecontext 'fr','F_Saving','un instant, je sauvegarde!!!!', 'Forum'
GO
updatelanguagecontext 'fr','F_From','Auteur', 'Forum'
GO
updatelanguagecontext 'fr','F_Message','Message', 'Forum'
GO
updatelanguagecontext 'fr','F_AdminModerate','Admin forum dirigé', 'Forum'
GO
updatelanguagecontext 'fr','cmdPMS','Messagerie', 'Forum'
GO
updatelanguagecontext 'fr','cmdProfile','Préférences', 'Forum'
GO
updatelanguagecontext 'fr','cmdSubscribe','S''abonner', 'Forum'
GO
updatelanguagecontext 'fr','cmdSearch','Recherche', 'Forum'
GO
updatelanguagecontext 'fr','cmdHome','Accueil', 'Forum'
GO
updatelanguagecontext 'fr','cmdNewTopic','Nouveau', 'Forum'
GO
updatelanguagecontext 'fr','F_Search1','Rechercher ici', 'Forum'
GO
updatelanguagecontext 'fr','F_Search','Rechercher tout', 'Forum'
GO
updatelanguagecontext 'fr','F_FlatView','Vue à plat', 'Forum'
GO
updatelanguagecontext 'fr','F_TreeView','Vue par arbre', 'Forum'
GO
updatelanguagecontext 'fr','F_Group','Forum', 'Forum'
GO
updatelanguagecontext 'fr','F_Thread','Fils de discussions', 'Forum'
GO
updatelanguagecontext 'fr','F_Post','Messages', 'Forum'
GO
updatelanguagecontext 'fr','F_LPost','Dernier message', 'Forum'
GO
updatelanguagecontext 'fr','F_GroupAdmin','Admin forum', 'Forum'
GO
updatelanguagecontext 'fr','F_ForumSetting','Paramètres globaux', 'Forum'
GO
updatelanguagecontext 'fr','F_UserAdmin','Admin. usagers', 'Forum'
GO
updatelanguagecontext 'fr','F_ForumAdmin','Admin. forum', 'Forum'
GO
updatelanguagecontext 'fr','F_ModerateAdmin','Admin. forum dirigé', 'Forum'
GO
updatelanguagecontext 'fr','F_Home','Accueil forum', 'Forum'
GO
updatelanguagecontext 'fr','F_AdminParam','Paramètres du forum', 'Forum'
GO
updatelanguagecontext 'fr','F_ForumID','ID Forum', 'Forum'
GO
updatelanguagecontext 'fr','F_Active','Actif', 'Forum'
GO
updatelanguagecontext 'fr','F_ForumName','Nom Forum', 'Forum'
GO
updatelanguagecontext 'fr','F_ForumGroup','Section', 'Forum'
GO
updatelanguagecontext 'fr','F_Desc','Description', 'Forum'
GO
updatelanguagecontext 'fr','F_CreatedBy','Creé par', 'Forum'
GO
updatelanguagecontext 'fr','F_Private','Privé?', 'Forum'
GO
updatelanguagecontext 'fr','F_AuthRoles','Autorisation', 'Forum'
GO
updatelanguagecontext 'fr','F_Moderated','Dirigé?', 'Forum'
GO
updatelanguagecontext 'fr','F_GalleryParam','Paramètres de galarie', 'Forum'
GO
updatelanguagecontext 'fr','F_GalleryParam1','utiliser la gestion des paramètres de la galerie', 'Forum'
GO
updatelanguagecontext 'fr','F_GalleryParam2','Intégration galerie?', 'Forum'
GO
updatelanguagecontext 'fr','F_GalleryParam3','ID galerie', 'Forum'
GO
updatelanguagecontext 'fr','F_GalleryParam4','Nom album', 'Forum'
GO
updatelanguagecontext 'fr','F_ReturnMod','Revenir à la page de gestion dirigée...', 'Forum'
GO
updatelanguagecontext 'fr','F_SelectAction','<Choisir une action>', 'Forum'
GO
updatelanguagecontext 'fr','F_ForumGroupS','Section forum', 'Forum'
GO
updatelanguagecontext 'fr','F_Cancel','Annuler', 'Forum'
GO
updatelanguagecontext 'fr','F_Edit','Modifier', 'Forum'
GO
updatelanguagecontext 'fr','F_Up','Haut', 'Forum'
GO
updatelanguagecontext 'fr','F_Down','Bas', 'Forum'
GO
updatelanguagecontext 'fr','F_AddNewForum','Ajouter un nouveau forum', 'Forum'
GO
updatelanguagecontext 'fr','F_Add','Ajouter', 'Forum'
GO
updatelanguagecontext 'fr','F_AddNewGroup','Ajouter une nouvelle section', 'Forum'
GO
updatelanguagecontext 'fr','F_Up1','Monter d''un niveau', 'Forum'
GO
updatelanguagecontext 'fr','F_Down1','Baisser d''un niveau', 'Forum'
GO
updatelanguagecontext 'fr','F_Expand','Ouvrir', 'Forum'
GO
updatelanguagecontext 'fr','F_GroupName','Nom de la section', 'Forum'
GO
updatelanguagecontext 'fr','F_GroupID','ID Section', 'Forum'
GO
updatelanguagecontext 'fr','F_NumberOf_F','Nombre de forum', 'Forum'
GO
updatelanguagecontext 'fr','F_ListOf_F','Liste des forum', 'Forum'
GO
updatelanguagecontext 'fr','F_CreatedOn','le', 'Forum'
GO
updatelanguagecontext 'fr','F_Groups','Forums', 'Forum'
GO
updatelanguagecontext 'fr','F_StatsForum','Statistiques forum', 'Forum'
GO
updatelanguagecontext 'fr','F_WhoThere','Qui est la', 'Forum'
GO
updatelanguagecontext 'fr','Quote_Wrote','a écrit', 'Forum'
GO
updatelanguagecontext 'fr','F_ToLarge','Le fichier doit être moins de {maxFileSize} KB.', 'Forum'
GO
updatelanguagecontext 'fr','F_Stiky','Message à voir', 'Forum'
GO
updatelanguagecontext 'fr','F_Today','Aujourd''hui', 'Forum'
GO
updatelanguagecontext 'fr','F_Bad_Ext','Seulement les fichiers {fileext} sont autorisés.', 'Forum'
GO
updatelanguagecontext 'fr','F_MaxImage','L''image ne peut être plus que {maxHeight} x {maxWidth} pixels.', 'Forum'
GO
updatelanguagecontext 'fr','Forum_Replied','Réponse au message', 'Forum'
GO
updatelanguagecontext 'fr','go','Go', 'Forum'
GO
updatelanguagecontext 'fr','F_SelectModerated','Choisir moderateur', 'Forum'
GO
updatelanguagecontext 'fr','select_avatar_tooltip','Aller et modifier la gallerie des avatars', 'Forum'
GO
updatelanguagecontext 'fr','select_avatar','Avatars gallerie', 'Forum'
GO
updatelanguagecontext 'fr','F_VisitWWW','WWW', 'Forum'
GO
updatelanguagecontext 'fr','FZuserMSN','MSN', 'Forum'
GO
updatelanguagecontext 'fr','FZuserYAHOO','YAHOO', 'Forum'
GO
updatelanguagecontext 'fr','FZuserAIM','AIM', 'Forum'
GO
updatelanguagecontext 'fr','FZuserICQ','ICQ', 'Forum'
GO
updatelanguagecontext 'fr','FZpage','Page', 'Forum'
GO
updatelanguagecontext 'fr','F_SelectAdmin','Adminstration du Forum', 'Forum'
GO
updatelanguagecontext 'fr','F_SendEMail','COURRIEL', 'Forum'
GO
updatelanguagecontext 'fr','F_AdminAvatar','Avatars gallerie', 'Forum'
GO
updatelanguagecontext 'fr','Select_Forum_Search','Choisir un Forum ou une Section pour la recherche', 'Forum'
GO
updatelanguagecontext 'fr','Search_SelectAllForum','Choisir tous les forums de la section', 'Forum'
GO
updatelanguagecontext 'fr','Search_SelectAllForumGroup','Choisir tous les forums de toutes les sections', 'Forum'
GO
updatelanguagecontext 'fr','Forum_Search','Recherche avancée', 'Forum'
GO
updatelanguagecontext 'fr','Search_Alias','Auteur', 'Forum'
GO
updatelanguagecontext 'fr','Search_Subject','Sujet', 'Forum'
GO
updatelanguagecontext 'fr','Search_Text','Message', 'Forum'
GO
updatelanguagecontext 'fr','FZEditPost','Modifier le message', 'Forum'
GO
updatelanguagecontext 'fr','F_SendPMS','MESSAGE', 'Forum'
GO
updatelanguagecontext 'fr','F_ImgSettings','Paramètres pour les images', 'Forum'
GO
updatelanguagecontext 'fr','Gal_MaxFileKB','Le fichier <b>{FileName}</b> est {FileSize} (KB) mais la taille maximum pour télécharger est de {MaxFileSize} (KB).', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_InfoBule','Afficher les info bulles? ', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_btnAddTip','Ajouter à la liste pour télécharger le fichier indiqué ...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_btnAddAlt','Ajouter', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_FileInfo1','<FONT color=#000080>
Il y a <b>{FileItemsCount}</b> fichiers <b>{SubAlbumItems}</b> sous albums dans ce dossier.</FONT><br>', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_FileOE','Erreur: Il a été impossible d''ouvrir cet item', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_FileOEN','Il y a déjà un fichier avec le même nom. Renommer votre fichier avant d''essayer de le télécharger à nouveau!', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Add','Ajouter', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AddF','Ajouter un album à : {folderURL}', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AddFF','Ajouter un ou des fichiers à l''album : {folderURL}', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_EditFile','Modifier fichier', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Loading','Chargement.. du diaporama ......', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Prev','Voir l''image précédente...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Next','Voir l''image suivante...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgInfo','Image {imgnum} de {albumnum} {imgname} ({imgsize}KB)', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_NoImage','Il n''y a aucune image dans l''album', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Page','Page:', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_NoFile','Il n''y a aucun fichier!', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Prev0','&laquo;', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Next0','&raquo;', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_NoConfig','Il n''y a pas de configuration de galerie pour ce module!', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Click','Cliquer ici...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_NoSpaceLeft','Vous n''avez plus d''espace disque de disponible pour télécharger ce fichier.', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_QuotaInfo2','<br>Vous n''avez plus d''espace disque Album de disponible pour télécharger des fichiers.', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_PortalQuota2','<br>Vous n''avez plus d''espace disque Portail de disponible pour télécharger des fichiers.', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_PortalQuota1','Espace Portail ( Maximum: <b>{Quota}MB</b>  Utilisée: <b>{SpaceUsed}MB</b>  Disponible: <b>{SpaceLeft}MB</b>', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_PortalQuota','Espace Portail ( Utilisée: <b>{SpaceUsed} MB</b> )', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_InfoPre','Présentation {start} - {end} de {total} item(s) total', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AlbumAdd','Le nouvel album a été ajouté', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AlbumNo','Le nom du nouveau répertoire ne doit pas contenir de caractère accentué ou de ponctuation', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AlbumExist','L''album existe déjà, veuillez utiliser un autre nom', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Clear','Rafraîchir l''affichage de la galerie...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Admin','Paramètres de configuration de la galerie...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SetUp','Paramètres', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Admin1','Ajouter un image/album...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SetUp1','Modifier Albums/Images', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Browser','Voir cet album en mode furetage...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_BrowserLink','Fureteur', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_show','Voir cet album en mode diaporama...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_showLink','Diaporama', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_lnkshow','Voir diaporama...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Dis','Discussion dans le forum...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_EditRes','Modifier Icon ...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Edit','Modifier...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SOwner','Choisir le propriétaire de la galerie...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_UpdateConf','Mettre à jour la configuration...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Select','Choisir', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SetUpAdmin','Paramètres Admin', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgBaseUrl','URL souche (images)', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Quota','Quota en (MB)', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_QuotaTip','Quota de l''album en (MB), 0=pas de limite', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_MaxFile','Taille Max d''un fichier', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_KB','(KB)', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgChangeSize','Modifier la taille de l''image', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgRatioInfo','l''images sera sauvegarder avec le même ratio hauteur/largeur que l''originale', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgQual','Qualité', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgQualInfo','S''applique seulement au fichier jpg, 20 = fichier plus petit 80 = fichier plus volumineux', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgMaxH','Hauteur Max', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgMaxW','Largeur Max', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgKeepS','Garder source?', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ImgKeepP','Garder privé?', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Prop','Propriétaire', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SelectProp','Choisir propriétaire', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_CacheS','Mettre en cache au départ', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_UserParam','Paramètres usager de la galerie', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Title','Titre de la galerie', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Desc','Description', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_TxtOp','Option d''affichage du texte', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_IntF','Intégration au forum?', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SelF','Choisir une section du forum', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_IntFF','Intégration', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ThumbsW','# miniature de large', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ThumbsH','# miniature de haut', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_MThumbsW','Largeur Max miniature', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_MThumbsH','Hauteur Max miniature', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_CatV','Valeurs catégorie', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ExtVF','Extensions fichiers', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ExtM','Extensions films', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_DiaS','Vitesse diaporama', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_PopDia','POPup diaporama?', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AllowD','Permettre téléchargement?', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AgAL','Ceci est la galerie des avatars?', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ModG','Modifier galerie', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ModA','Modifier album', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_URL','URL', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Name','Nom', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_TitleI','Titre', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Cat','Catégories', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SaveA','Sauvegarder l''album', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Cancel','Annuler sans sauvegarder et revenir à l''album', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_FileType','Types fichier', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_File','Fichier', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_FileUpInfo','Fichiers à ajouter, pour compléter vous devez cliquer sur le bouton <b>télécharger</b>', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Album','Album', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AddFolder','Ajouter Album', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AddFolderTip','Cliquer ici pour ajouter un nouveau répertoire à votre album', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AddFile','Ajouter fichier', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_AddFileTip','Cliquer ici pour ajouter un nouveau fichier à votre album', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Private','Galerie privée', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_CancelTip','Revenir à l''album photo sans sauvegarder', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_UpdateTip','Sauvegarder les modifications et revenir à l''album photo', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Refuse','Ceci est une galerie privée.  Seulement le propriétaire de cette galerie peut la modifier ou faire des changements. <br>Contacter le webmestre pour plus d''information', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_FileInfo','<FONT color=#000080>
* Zip<br>
* Image: {fileext}<br>
* Movie: {movieext}<br>
* Flash: .swf <br>
* Taille Max: {MaxFileSize} KB <br>
</FONT>', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Items','Items', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_QuotaInfo','Espace Album ( Utilisée: <b>{SpaceUsed} MB</b> )', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_QuotaInfo1','Espace Album ( Maximum: <b>{Quota}MB</b>  Utilisée: <b>{SpaceUsed}MB</b>  Disponible: <b>{SpaceLeft}MB</b>', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ErrorDir','Vous pouvez seulement spécifier un répertoire dans votre portail', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ErrorF','Vous devez vous assurer qu''il y a une ou des sections de forums de créer ansi que des forums avant de faire une intégration', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SelectF','<Choisir Forum>', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_SelectA','<Choisir album>', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ErrorSelectAF','Vous devez choisir une forum ainsi qu''un album pour l''integration', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_ErrorSelectL','Vous devez choisir une integration avec la liste', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Dim','Dimension', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_FFlash','Fichier Flash', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_FFilm','Film', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_DefaultCategory','Image;Film;Musique;Flash', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_MaxFileSize','La taille du fichier doit être moins de {MaxFileSize} (KB)', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Invalid_FileType','Le type de fichier est invalide', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_Cont','cliquer pour continuer...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_CacheGen','La galerie se crée...', 'Gallery'
GO
updatelanguagecontext 'fr','Gal_CacheRed','Si cette page ne vous redirige pas automatiquement,', 'Gallery'
GO
updatelanguagecontext 'fr','all','tout', 'Global'
GO
updatelanguagecontext 'fr','list_none','<Aucun>', 'Global'
GO
updatelanguagecontext 'fr','help','Aide', 'Global'
GO
updatelanguagecontext 'fr','need_help','besoins d''aide', 'Global'
GO
updatelanguagecontext 'fr','exit','Fermer la session', 'Global'
GO
updatelanguagecontext 'fr','login','Ouvrir une session', 'Global'
GO
updatelanguagecontext 'fr','error','Erreur', 'Global'
GO
updatelanguagecontext 'fr','module_info','Info Module :', 'Global'
GO
updatelanguagecontext 'fr','no_module_info','Il n''y a aucune information de disponible sur ce module.', 'Global'
GO
updatelanguagecontext 'fr','Membership_Serv','Services aux membres', 'Global'
GO
updatelanguagecontext 'fr','True','Vrai', 'Global'
GO
updatelanguagecontext 'fr','False','Faux', 'Global'
GO
updatelanguagecontext 'fr','*O','O', 'Global'
GO
updatelanguagecontext 'fr','*N','N', 'Global'
GO
updatelanguagecontext 'fr','GoogleDisplayPointer','Afficher un icône sur la carte', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_show_map_edit','Voir la carte?', 'GoogleMap'
GO
updatelanguagecontext 'fr','need_location_MapGoogle','Vous devez saisir une location', 'GoogleMap'
GO
updatelanguagecontext 'fr','need_API_MapGoogle','Un API de Google est requis!', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_show_map','Voir la carte', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_show_tooltip','Afficher la carte', 'GoogleMap'
GO
updatelanguagecontext 'fr','GoogleAPI','API Google', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_location','Location', 'GoogleMap'
GO
updatelanguagecontext 'fr','GetGoogleAPI','Aller chercher l''API de Google', 'GoogleMap'
GO
updatelanguagecontext 'fr','GoogleLatLong','Lat - Long', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_zoom','Zoom', 'GoogleMap'
GO
updatelanguagecontext 'fr','GoogleGenerateLatLong','Générer une coordonnée', 'GoogleMap'
GO
updatelanguagecontext 'fr','GoogleGenerateScript','Générer un script', 'GoogleMap'
GO
updatelanguagecontext 'fr','need_LatLong_MapGoogle','Vous devez founir les coordonnée', 'GoogleMap'
GO
updatelanguagecontext 'fr','need_Script_MapGoogle','Vous devez fournir un script', 'GoogleMap'
GO
updatelanguagecontext 'fr','GoogleDisplayResize','Afficher la barre de navigation sur la carte', 'GoogleMap'
GO
updatelanguagecontext 'fr','GoogleDisplayType','Afficher la sélection de vue sur la carte', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_big','Large', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_directions','Directions', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_directions_tooltip','Aller au site Google Map', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_size','Grandeur', 'GoogleMap'
GO
updatelanguagecontext 'fr','MapGoogle_small','Petite', 'GoogleMap'
GO
updatelanguagecontext 'fr','WhiteSpace_ALL','HTML et Espace Blanc', 'HostSettings'
GO
updatelanguagecontext 'fr','ViewState_memory','ViewState en mémoire', 'HostSettings'
GO
updatelanguagecontext 'fr','WhiteSpace_Only','Espace blanc seulement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_EnableSSLInfo','SSL sera appliqué sur certaine page', 'HostSettings'
GO
updatelanguagecontext 'fr','ViewState_SQL','ViewState dans SQL', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_EnableSSL','Forcer SSL', 'HostSettings'
GO
updatelanguagecontext 'fr','MailSend','le courriel a été envoyé', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_HostName','Titre du serveur d''hébergement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_URLHost','URL du serveur d''hébergement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_EmailHost','Courriel du serveur d''hébergement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_TimeHost','Heure du serveur', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_TimeZoneHost','Fuseau Horaire du serveur', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_ViewState','Gestion du ViewState', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_HTMLClean','Réduction de la page web', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_Paiement','Méthode de paiement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_PaiementCode','Code d''accès fournisseur paiement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_PaiementPassword','Mot de passe fournisseur paiement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_HostFee','Frais d''hébergement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_Currency','Devise', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_PortalSpace','Espace&nbsp;( MB)', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_SiteLog','Journal d''utilisation (jours)', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_DemoPeriod','Période de démo&nbsp;(jours)', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_DemoAllow','Permettre la création de portail démo?', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_DNZVersion','Désactivé l''affichage de la version du logiciel dans la page titre?', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_ErrorReporting','Permettre l''enregistrement&nbsp;des erreurs?', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_ErrorReportingInfo','(faire parvenir les messages d''erreur du portail à  l''adresse de courriel du serveur d''hébergement)', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_PassordCrypto','Clef de chiffrage du mot de passe', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_Proxy','Serveur Proxy', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_ProxyPort','Port Proxy', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_SMTP','Paramètre du serveur SMTP', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_SMTP_Server','Serveur SMTP', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_SMTP_Code','Code usager', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_SMTP_Password','Môt Passe', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_ext_upload','Validation type de fichier pour téléchargement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_LogSQL','Voir le log de mise à jours pour la version', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_GO_Processor','Aller au site web du fournisseur de paiement', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_test_email','Tester la configuration du courriel', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_NoLog','Le script n''a pas été exécuté pour cette version.', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_Need_Email','<br>vous devez fournir le nom de l''adresse courriel du serveur d''hébergement', 'HostSettings'
GO
updatelanguagecontext 'fr','WhiteSpaceHTML','Reduction HTML', 'HostSettings'
GO
updatelanguagecontext 'fr','html','html', 'HTML'
GO
updatelanguagecontext 'fr','text','texte', 'HTML'
GO
updatelanguagecontext 'fr','HTML_ADDtxt','Ajouter du contenu...', 'HTML'
GO
updatelanguagecontext 'fr','AlternateDetailSummary','Môts clées', 'HTML'
GO
updatelanguagecontext 'fr','ForSearchHTML','Information facultative pour la recherche', 'HTML'
GO
updatelanguagecontext 'fr','SearchSummary','Sommaire', 'HTML'
GO
updatelanguagecontext 'fr','auto','auto', 'Iframe'
GO
updatelanguagecontext 'fr','no','non', 'Iframe'
GO
updatelanguagecontext 'fr','yes','oui', 'Iframe'
GO
updatelanguagecontext 'fr','browser_non_compatible','Il est imposible d''afficher cette page puisque votre fureteur n''est pas compatible.  Je vous suggère d''utiliser Internet Explorer 5.0 ou 6.0', 'Iframe'
GO
updatelanguagecontext 'fr','iframe_source','Source', 'Iframe'
GO
updatelanguagecontext 'fr','iframe_width','Largeur', 'Iframe'
GO
updatelanguagecontext 'fr','iframe_height','Hauteur', 'Iframe'
GO
updatelanguagecontext 'fr','iframe_title','Titre', 'Iframe'
GO
updatelanguagecontext 'fr','iframe_scroll','Défilement', 'Iframe'
GO
updatelanguagecontext 'fr','iframe_border','Bordure', 'Iframe'
GO
updatelanguagecontext 'fr','image_select_iternal','Choisir une image interne', 'Image'
GO
updatelanguagecontext 'fr','image_iternal','Image interne', 'Image'
GO
updatelanguagecontext 'fr','image_select_external','Choisir une image externe', 'Image'
GO
updatelanguagecontext 'fr','image_external','Image externe', 'Image'
GO
updatelanguagecontext 'fr','image_alt_text','Texte alternatif', 'Image'
GO
updatelanguagecontext 'fr','image_artist','Artiste', 'Image'
GO
updatelanguagecontext 'fr','image_copyright','Copyright', 'Image'
GO
updatelanguagecontext 'fr','image_description','Description', 'Image'
GO
updatelanguagecontext 'fr','image_title','Titre', 'Image'
GO
updatelanguagecontext 'fr','image_width','Largeur', 'Image'
GO
updatelanguagecontext 'fr','image_height','Hauteur', 'Image'
GO
updatelanguagecontext 'fr','NoFileMessage','Aucun dossier choisi', 'ImageManager'
GO
updatelanguagecontext 'fr','UploadSuccessMessage','Téléchargement réussi', 'ImageManager'
GO
updatelanguagecontext 'fr','NoImagesMessage','Aucune image', 'ImageManager'
GO
updatelanguagecontext 'fr','NoFolderSpecifiedMessage','Pas de répertoire', 'ImageManager'
GO
updatelanguagecontext 'fr','NoSpaceLeft','Pas suffisament d''espace disque', 'ImageManager'
GO
updatelanguagecontext 'fr','NoFileToDeleteMessage','Pas de fichier à effacer', 'ImageManager'
GO
updatelanguagecontext 'fr','InvalidFileTypeMessage','Le type de fichier est invalide, vous pouvez seulement télécharger des fichiers jpg gif png tif ou bmp', 'ImageManager'
GO
updatelanguagecontext 'fr','InvalidFileTypeMessage2','Le type de fichier est invalide, vous devez utiliser seulement un GIF ou JPG', 'ImageManager'
GO
updatelanguagecontext 'fr','NoFolderFoundMessage','Erreur, impossible de trouver le répertoire des images', 'ImageManager'
GO
updatelanguagecontext 'fr','MagicFileMessage','L''îcone par défaut sera utilisé<br>Sinon télécharger en un nouveau', 'ImageManager'
GO
updatelanguagecontext 'fr','MagicFileInfo1','Assurer vous d''utiliser un fichier image de la bonne dimension soit de 20px minimum à 200px maximum.', 'ImageManager'
GO
updatelanguagecontext 'fr','ErrorGDI','Erreur, ceci n''est pas une image, ou le fichier n''est pas un format d''image valide.', 'ImageManager'
GO
updatelanguagecontext 'fr','LabelUploadDeleteCancel1','Télécharger une nouvelle image, effacer ou annuler :', 'ImageManager'
GO
updatelanguagecontext 'fr','ImageMaxSize','La qualité doit être entre 20-80 et la grandeur maximum de l''image est de 200px', 'ImageManager'
GO
updatelanguagecontext 'fr','MagicFileMessage1','Le fichier a été modifié, il est possible que vous ne voyez pas de différence à l''écran, vous devez <b><font color=""#ff0000"">purger votre cache</font></b> sur votre fureteur, puisque vous voyez un fichier en cache sur votre ordinateur.', 'ImageManager'
GO
updatelanguagecontext 'fr','ImageDeleted','L''image {imagename} a été effacé', 'ImageManager'
GO
updatelanguagecontext 'fr','ImageDeleteError','Il y a eu une erreur!', 'ImageManager'
GO
updatelanguagecontext 'fr','LabelUploadDeleteCancel','Télécharger un nouvel icône, effacer ou annuler :', 'ImageManager'
GO
updatelanguagecontext 'fr','LabelMaxHeightImage','Hauteur max de l''image :', 'ImageManager'
GO
updatelanguagecontext 'fr','LabelMaxWImage','Largeur max de l''image :', 'ImageManager'
GO
updatelanguagecontext 'fr','LabelQuaImage','Qualité de l''image :', 'ImageManager'
GO
updatelanguagecontext 'fr','ImageRangeToSave800','Doit être entre 10 à 800', 'ImageManager'
GO
updatelanguagecontext 'fr','IconesFrom','les icônes de :', 'ImageManager'
GO
updatelanguagecontext 'fr','IconesTo','à :', 'ImageManager'
GO
updatelanguagecontext 'fr','ImageRangeFrom','Doit être entre 5px à 180px max.', 'ImageManager'
GO
updatelanguagecontext 'fr','ImageRangeTo','Doit être entre 20px à 200px max.', 'ImageManager'
GO
updatelanguagecontext 'fr','ImageQualityRange','Doit être entre 20 à 80', 'ImageManager'
GO
updatelanguagecontext 'fr','ImageRangeToSave','Doit être entre 10 à 200', 'ImageManager'
GO
updatelanguagecontext 'fr','MagicFileMessage2','Pour puger vos fichier en cache vous devez utiliser l''option - outils - options internet... - supprimer les fichiers,   une fois supprimer vous devez actualiser votre fenêtre."', 'ImageManager'
GO
updatelanguagecontext 'fr','MagicFileInfo','Vous pouvez personnaliser l''apparence des répertoires ainsi que des fichiers vidéo.  Pour assigner une image à un répertoire à la place de l''icône par défaut vous devez télécharger une image gif ou jpg.', 'ImageManager'
GO
updatelanguagecontext 'fr','error_panel_title','Erreur', 'ImageManager'
GO
updatelanguagecontext 'fr','language_create','Générer un script SQL de ', 'Language'
GO
updatelanguagecontext 'fr','language_clicktocreate','Cliquer ici pour créer un nouvelle langue', 'Language'
GO
updatelanguagecontext 'fr','language_clicktoadd','Cliquer ici pour ajouter', 'Language'
GO
updatelanguagecontext 'fr','language_clicktogenerate','Cliquer ici pour générer un script SQL de la langue', 'Language'
GO
updatelanguagecontext 'fr','language','Français', 'Language'
GO
updatelanguagecontext 'fr','language_param','Param', 'Language'
GO
updatelanguagecontext 'fr','language_change','Modifier', 'Language'
GO
updatelanguagecontext 'fr','language_new','Ajouter une nouvelle langue', 'Language'
GO
updatelanguagecontext 'fr','language_add','Ajouter un élément', 'Language'
GO
updatelanguagecontext 'fr','language_context','Contexte', 'Language'
GO
updatelanguagecontext 'fr','language_value','Info', 'Language'
GO
updatelanguagecontext 'fr','language_see','Voir', 'Language'
GO
updatelanguagecontext 'fr','culturecode','fr-CA', 'Language'
GO
updatelanguagecontext 'fr','language_clicktodel','Cliquer ici pour effacer la langue ->', 'Language'
GO
updatelanguagecontext 'fr','language_del','Effacer une langue', 'Language'
GO
updatelanguagecontext 'fr','ScriptGenerated','Fichier script sql généré:', 'Language'
GO
updatelanguagecontext 'fr','LanguageERROR','Ce môt - {0} - n''est pas dans la table de la langue {1}', 'Language'
GO
updatelanguagecontext 'fr','btnLanguageEditAll','Afficher tous les rubriques', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings4','Codes Pays', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings6','Codes argent', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings7','Codes fréquences', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings8','Code rapports statistiques', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings9','Codes régions', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings10','Zones horaire', 'Language'
GO
updatelanguagecontext 'fr','btnLanguageEdit','Afficher seulement les nouvelles rubriques', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings5','Info modules admin', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings1','Ajouter, Effacer', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings2','Rubrique d''aide', 'Language'
GO
updatelanguagecontext 'fr','LanguageSettings3','Rubriques générales', 'Language'
GO
updatelanguagecontext 'fr','Language_Edit_Param','Modification rubriques', 'Language'
GO
updatelanguagecontext 'fr','links_label_CSSClass','Classe CSS pour l''hyperlien', 'Links'
GO
updatelanguagecontext 'fr','links_info','...', 'Links'
GO
updatelanguagecontext 'fr','links_info_tooltip','Voir plus d''info sur!', 'Links'
GO
updatelanguagecontext 'fr','links_options_list','Liste', 'Links'
GO
updatelanguagecontext 'fr','links_options_dropdown','Menu déroulant', 'Links'
GO
updatelanguagecontext 'fr','links_label_list_type','Type de liste', 'Links'
GO
updatelanguagecontext 'fr','links_label_list_format','Format d''affichage de la liste', 'Links'
GO
updatelanguagecontext 'fr','links_label_see_info','Afficher l''information de l''hyperlien', 'Links'
GO
updatelanguagecontext 'fr','links_options_vertical','Vertical', 'Links'
GO
updatelanguagecontext 'fr','links_options_horizontal','Horizontal', 'Links'
GO
updatelanguagecontext 'fr','links_options_yes','Oui', 'Links'
GO
updatelanguagecontext 'fr','links_options_no','Non', 'Links'
GO
updatelanguagecontext 'fr','links_title','Titre', 'Links'
GO
updatelanguagecontext 'fr','select_external_link','Choisir un Hyperlien extérieur', 'Links'
GO
updatelanguagecontext 'fr','select_internal_link','Choisir un Hyperlien interne', 'Links'
GO
updatelanguagecontext 'fr','external_link','Hyperlien extérieur', 'Links'
GO
updatelanguagecontext 'fr','internal_link','Hyperlien interne', 'Links'
GO
updatelanguagecontext 'fr','select_file_link','Choisir un Hyperlien de fichier', 'Links'
GO
updatelanguagecontext 'fr','file_link','Hyperlien de fichier', 'Links'
GO
updatelanguagecontext 'fr','link_description','Description', 'Links'
GO
updatelanguagecontext 'fr','link_vue_order','Ordre de vue', 'Links'
GO
updatelanguagecontext 'fr','link_new_window','Afficher dans une nouvelle fenêtre?', 'Links'
GO
updatelanguagecontext 'fr','Label_enter','Ouvrir une session', 'Login'
GO
updatelanguagecontext 'fr','Label_UserName','Code d''accès', 'Login'
GO
updatelanguagecontext 'fr','Login_Keep','Garder en mémoire', 'Login'
GO
updatelanguagecontext 'fr','Button_Enter','Entrer', 'Login'
GO
updatelanguagecontext 'fr','Button_Register','S''inscrire', 'Login'
GO
updatelanguagecontext 'fr','Button_Password','Récupérer mot de passe', 'Login'
GO
updatelanguagecontext 'fr','Button_PasswordTooltip','Cliquer ici pour recevoir par courriel votre môt de passe.', 'Login'
GO
updatelanguagecontext 'fr','Button_RegisterTooltip','Cliquer ici pour vous inscrire.', 'Login'
GO
updatelanguagecontext 'fr','RegisterMessage1','Le code d''accès n''existe pas', 'Login'
GO
updatelanguagecontext 'fr','RegisterMessage2','Nous vous avons fait parvenir votre mot de passe à votre adresse de courriel.', 'Login'
GO
updatelanguagecontext 'fr','RegisterMessage3','Votre code d''accès ou mot de passe sont invalides essayer de nouveau.  La validation de votre mot de passe est sensible aux minuscules et majuscules.', 'Login'
GO
updatelanguagecontext 'fr','RegisterMessage4','Entrer votre code de validation', 'Login'
GO
updatelanguagecontext 'fr','RegisterMessage5','Le Code n''est pas bon', 'Login'
GO
updatelanguagecontext 'fr','Signin_hide','Cliquer ici pour fermer la fenêtre.', 'Login'
GO
updatelanguagecontext 'fr','Label_Password','Mot de passe', 'Login'
GO
updatelanguagecontext 'fr','Label_ValidCode','Code de validation', 'Login'
GO
updatelanguagecontext 'fr','Button_EnterTooltip','Cliquer ici pour ouvrir la session.', 'Login'
GO
updatelanguagecontext 'fr','RegisterMessage','SVP saisissez votre code d''accès', 'Login'
GO
updatelanguagecontext 'fr','RegisterMessage6','Il a été impossible de vous faire parvenir un courriel avec les informations demandées.', 'Login'
GO
updatelanguagecontext 'fr','MS_Script','Script d''instalation', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_ModuleName','Nom du module', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_SRC_URL','Source bureau', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_EDIT_URL','Modifier source', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_HELP_URL','Fichier aide', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_IconeHelp','Il est recommandé d''utiliser un icône de 20px X 20px maximum', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_Bonus','Bonus?', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_Module_Desc','Description du module', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_BonusTxt','Bonus', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_ModuleDescription','Description', 'ModuleSettings'
GO
updatelanguagecontext 'fr','MS_Directives','*Directives', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_select_color','Choisir une couleur', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_SelectModuleSkin','Choisir un habillage', 'ModuleSettings'
GO
updatelanguagecontext 'fr','TitleHeader_class','CSS class du titre', 'ModuleSettings'
GO
updatelanguagecontext 'fr','Title_class','CSS class du texte titre', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ModuleOnly','Intérieur', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ModuleTitle','Titre', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ModuleWrapper','Extérieur', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_contener_default_info','Pour sauvegarder cette habillage comme celui par défaut pour le site, donc cette habillage sera utilisé lorsqu''il n''y aura pas d''habillage de définie pour un module.', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_contener_global','Global?', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_move_to_tab','Déplacer vers la page', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_contener_error1','Vous devez avoir [MODULE] dans la définition de l''habillage.', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_contener_error2','Une erreur de code HTML existe dans le container', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_non_authorized','Visiteurs anonymes', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_all_users','Tous', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_title','Titre', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_language','Langue', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_icone','Icône', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_icone_info','Il est recommandé d''utiliser un icône de  20px X 20px maximum', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_show_title','Afficher le titre du module?', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_showall_tabs','Afficher le module sur toutes les pages?', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_custom','Personnalisation?', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_open','Ouvert', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_close','Fermer', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_none','Aucun', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_cache','Mise en cache (sec.)', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_cache_info','La mise en cache est recommandée pour les modules statiques, la période de mise en cache est en seconde', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_see_info','A une priorité sur les paramètres de la page', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_see','Voir', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_mod','Modifier', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_contener','Habillage du module', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_contener_setup','Enlignement', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_left','Gauche', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_center','Centre', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_right','Droite', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_color','Couleur', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_margin','Marge', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_edit_contener','Modifier l''habillage du module', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_contener_default','Par défaut?', 'ModuleSettings'
GO
updatelanguagecontext 'fr','ms_contener_global_info','L''habillage de tous les modules seront effacés et l''habillage par défaut sera utilisé à la place.', 'ModuleSettings'
GO
updatelanguagecontext 'fr','EditAccessDenied','Restriction d''accès', 'ModuleTitle'
GO
updatelanguagecontext 'fr','MyBuddiesModule','Information usagers préférés', 'ModuleTitle'
GO
updatelanguagecontext 'fr','UserInfoModule','Information membres', 'ModuleTitle'
GO
updatelanguagecontext 'fr','UserListModule','Liste membres', 'ModuleTitle'
GO
updatelanguagecontext 'fr','Site_Help','Aide', 'ModuleTitle'
GO
updatelanguagecontext 'fr','HelpInfo','Aide', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_ShowParam','Paramètres d''affichage du module', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_ShowInfo','Afficher l''information sur le module', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_HideInfo','Cacher l''information sur le module', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_ParamModif','Modifier les paramètres du module', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_UpToolTip','Déplacer le module vers le haut', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_DownToolTip','Déplacer le module vers le bas', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_LeftToolTip','Déplacer le module vers la gauche', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_RightToolTip','Déplacer le module vers la droite', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_CacheToolTip','Pour actualiser l''affichage du module et effacer l''antémémoire.', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_CacheModToolTip','Pour actualiser l''affichage et effacer l''antémémoire du module.', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_CacheModToolTip0','Pour actualiser l''affichage et effacer les antémémoires du module.', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_Dec','Réduire', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_Inc','Agrandir', 'ModuleTitle'
GO
updatelanguagecontext 'fr','registration_ok','Résultat de l''inscription', 'ModuleTitle'
GO
updatelanguagecontext 'fr','create_portal','Création d''un Portail', 'ModuleTitle'
GO
updatelanguagecontext 'fr','privacy_declaration','DÉCLARATIONS DE CONFIDENTIALITÉ', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_sql','SQL', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_terms','CONDITIONS GÉNÉRALES', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_denied','Refus', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_expired','Portail périmé', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_enter','Ouvrir une session', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_manage_user_table','Gestion des tables usager', 'ModuleTitle'
GO
updatelanguagecontext 'fr','title_vendor_feedback','Rétroaction fournisseur', 'ModuleTitle'
GO
updatelanguagecontext 'fr','PrivateMessages','Messagerie', 'ModuleTitle'
GO
updatelanguagecontext 'fr','banners_host','serveur', 'PortalBanner'
GO
updatelanguagecontext 'fr','banners_portal','portail', 'PortalBanner'
GO
updatelanguagecontext 'fr','label_banner_type','Type d''enseigne', 'PortalBanner'
GO
updatelanguagecontext 'fr','label_banner_order','Ordre de l''enseigne', 'PortalBanner'
GO
updatelanguagecontext 'fr','label_banner_source','Source de l''enseigne', 'PortalBanner'
GO
updatelanguagecontext 'fr','directory_from','Source du répertoire', 'PortalBanner'
GO
updatelanguagecontext 'fr','label_see_comments','Voir les commentaires?', 'PortalBanner'
GO
updatelanguagecontext 'fr','allow_vendors_register','Autoriser les fournisseurs à s''inscrire?', 'PortalBanner'
GO
updatelanguagecontext 'fr','label_search','Rechercher', 'PortalBanner'
GO
updatelanguagecontext 'fr','banner_register','S''inscrire', 'PortalBanner'
GO
updatelanguagecontext 'fr','register_no','Pour le moment, vous ne pouvez pas vous inscrire à notre service', 'PortalBanner'
GO
updatelanguagecontext 'fr','register_more_info','Vous devez vous identifier avec votre code d''accès et votre mot de passe. Si vous êtes déjà inscrit, S.V.P. <a class="CommandButton" href="{httplogin}">Entrer</a> maintenant. Sinon, <a class="CommandButton" href="{httpregister}">inscrivez vous.</a>', 'PortalBanner'
GO
updatelanguagecontext 'fr','Banner_Mail2','si vous désirez lire vos messages vous devez cliquer sur cet icône', 'PortalBanner'
GO
updatelanguagecontext 'fr','Banner_Mail3','messages', 'PortalBanner'
GO
updatelanguagecontext 'fr','Banner_Mail4','message', 'PortalBanner'
GO
updatelanguagecontext 'fr','Banner_Mail5','Vous devez cliquer sur cet icône pour avoir accès à la messagerie', 'PortalBanner'
GO
updatelanguagecontext 'fr','banner_ClickProfile','Cliquer ici pour faire une recherche pour trouver un usager ou pour modifier votre profil', 'PortalBanner'
GO
updatelanguagecontext 'fr','Banner_Mail','vous avez', 'PortalBanner'
GO
updatelanguagecontext 'fr','Banner_Mail0','nouveau message', 'PortalBanner'
GO
updatelanguagecontext 'fr','Banner_Mail1','nouveaux messages', 'PortalBanner'
GO
updatelanguagecontext 'fr','P_PortalName','Nom du portail', 'PortalInfo'
GO
updatelanguagecontext 'fr','P_Alias','Alias', 'PortalInfo'
GO
updatelanguagecontext 'fr','P_Users','Utilisateurs', 'PortalInfo'
GO
updatelanguagecontext 'fr','P_Disk_Space','Espace disque', 'PortalInfo'
GO
updatelanguagecontext 'fr','P_Host_Fee','Frais d''hébergement', 'PortalInfo'
GO
updatelanguagecontext 'fr','P_EndDate','Date d''échéance', 'PortalInfo'
GO
updatelanguagecontext 'fr','ServicesFee','Frais de services', 'PortalRoles'
GO
updatelanguagecontext 'fr','Billing_Period','Période de facturation (chaque)', 'PortalRoles'
GO
updatelanguagecontext 'fr','Billing_Frequency','Fréquence de facturation', 'PortalRoles'
GO
updatelanguagecontext 'fr','Demo_Trial_Fee','Frais d''essais', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Assignation_Public','Publique?', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Assignation_Auto','Auto assignation?', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Upload','Autorisation à télécharger', 'PortalRoles'
GO
updatelanguagecontext 'fr','ManageRoles','Gérer les comptes', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Trial','Essai', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Expiry_Date','Date d''expiration', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Name','Nom', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Description','Description', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Frequency','Chaque', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_FrequencyP','Période', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Header_Public','Publique', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_Header_Auto','Auto', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_PortalRole','Profil de sécurité', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_ExpiryDate','Date d''expiration', 'PortalRoles'
GO
updatelanguagecontext 'fr','Role_AllRoles','Tous les profils', 'PortalRoles'
GO
updatelanguagecontext 'fr','PersonnalInfo','Information personnelle', 'Register'
GO
updatelanguagecontext 'fr','Required_Info','<b>*Note:</b> Tous les champs identifiés par un astérisque (*) sont exigés.', 'Register'
GO
updatelanguagecontext 'fr','Register_Title','fonctions de gestion d''accès', 'Register'
GO
updatelanguagecontext 'fr','Private_Info','<b>*Note:</b> L''adhésion au portail est privée. Le webmestre validera votre demande d''adhésion et vous informera de son acceptation par courriel.', 'Register'
GO
updatelanguagecontext 'fr','Public_Register_info','<b>*Note:</b> L''adhésion au portail est publique. Aussitôt que votre demande d''adhésion sera complétée vous aurez accès au portail.', 'Register'
GO
updatelanguagecontext 'fr','Verified_Register_info','<b>*Note:</b> L''adhésion au portail est vérifiée. Aussitôt que votre demande d''adhésion sera complétée, vous recevrez un courriel avec un code de validation unique.  Vous devrez utiliser ce code de validation pour entrer sur le portail la première fois.', 'Register'
GO
updatelanguagecontext 'fr','Register_Required_Info','Tous les champs identifiés par un astérisque (*) sont exigés.', 'Register'
GO
updatelanguagecontext 'fr','UserName_Already_Used','Vous devez choisir une autre code d''accès puisque <u>{username}</u> est déjà utilisé.<br>Choisissez un autre code d''accès.', 'Register'
GO
updatelanguagecontext 'fr','Valid_IP_Security','Les codes IP ne sont pas valide vous devez utiliser un code entre 0.0.0.1 et 255.255.255.255', 'Register'
GO
updatelanguagecontext 'fr','Valid_IP_Security1','Le CODE IP de: doit être égal ou plus bas que le CODE IP à:', 'Register'
GO
updatelanguagecontext 'fr','Valid_IP_Saved','Code sauvegarder', 'Register'
GO
updatelanguagecontext 'fr','Valid_IP_Not_Saved','Code non sauvegarder', 'Register'
GO
updatelanguagecontext 'fr','rss_general_info','<b>*Note:</b> Pour une liste exhaustive des nouvelles disponibles aller voir chez&nbsp;<b><a href="http://w.moreover.com/categories/category_list_rss.html" target="_new">Moreover.Com</a></b>', 'RSS'
GO
updatelanguagecontext 'fr','rss_noconnect_error','Le lien RSS n''est pas disponible. Message d''erreur: {errormessage}', 'RSS'
GO
updatelanguagecontext 'fr','rss_internal','Interne', 'RSS'
GO
updatelanguagecontext 'fr','rss_external','Externe', 'RSS'
GO
updatelanguagecontext 'fr','rss_css_internal','CSS Interne', 'RSS'
GO
updatelanguagecontext 'fr','rss_css_external','CSS Externe', 'RSS'
GO
updatelanguagecontext 'fr','rss_noconnect_admin','Il a été impossible de se connecter {xmlsrc}. Vous devriez valider que l''adresse est correcte ainsi que les paramètres proxy sont valide.', 'RSS'
GO
updatelanguagecontext 'fr','rss_news_source','Source des nouvelles', 'RSS'
GO
updatelanguagecontext 'fr','rss_CSS_source','Modèle CSS des nouvelles', 'RSS'
GO
updatelanguagecontext 'fr','rss_security','Options de sécurité (optionnel)', 'RSS'
GO
updatelanguagecontext 'fr','rss_security_account','Information de compte', 'RSS'
GO
updatelanguagecontext 'fr','rss_account_info','domain\Code d''accès', 'RSS'
GO
updatelanguagecontext 'fr','rss_account_password','Môt passe', 'RSS'
GO
updatelanguagecontext 'fr','search_search','Rechercher', 'Search'
GO
updatelanguagecontext 'fr','Search_show_desc','Voir la description?', 'Search'
GO
updatelanguagecontext 'fr','Search_show_audit','Voir l''audit?', 'Search'
GO
updatelanguagecontext 'fr','Search_show_breadcrum','Voir le chemin contextuel?', 'Search'
GO
updatelanguagecontext 'fr','Search_sql_table','Table', 'Search'
GO
updatelanguagecontext 'fr','search_header_title','Titre', 'Search'
GO
updatelanguagecontext 'fr','search_header_table','Table', 'Search'
GO
updatelanguagecontext 'fr','search_header_desc','Description', 'Search'
GO
updatelanguagecontext 'fr','search_header_update','Mise à jour', 'Search'
GO
updatelanguagecontext 'fr','search_header_who','Par', 'Search'
GO
updatelanguagecontext 'fr','Search_max_result','Nombre maximum de résultats de la recherche', 'Search'
GO
updatelanguagecontext 'fr','Search_max_width','Largeur maximum du titre', 'Search'
GO
updatelanguagecontext 'fr','Search_max_width_desc','Largeur maximum de la description', 'Search'
GO
updatelanguagecontext 'fr','Confirm_Password','Confirm', 'Signup'
GO
updatelanguagecontext 'fr','S_InstalPortal','Installation d''un portail&nbsp;', 'Signup'
GO
updatelanguagecontext 'fr','S_PortalType','Type de portail', 'Signup'
GO
updatelanguagecontext 'fr','S_PortalTypeParent','Parent', 'Signup'
GO
updatelanguagecontext 'fr','S_PortalTypeChild','Enfant', 'Signup'
GO
updatelanguagecontext 'fr','S_PortalName','Nom du portail', 'Signup'
GO
updatelanguagecontext 'fr','S_PortalTemplate','Gabarit', 'Signup'
GO
updatelanguagecontext 'fr','S_PortalAdmin','Paramètres de l''administrateur du portail', 'Signup'
GO
updatelanguagecontext 'fr','S_F_UserCode','Code d''accès', 'Signup'
GO
updatelanguagecontext 'fr','S_F_Password','Môt de passe', 'Signup'
GO
updatelanguagecontext 'fr','S_F_Email','Courriel', 'Signup'
GO
updatelanguagecontext 'fr','S_CreatePortal','Créer un portail', 'Signup'
GO
updatelanguagecontext 'fr','S_F_Name','Prénom', 'Signup'
GO
updatelanguagecontext 'fr','S_F_LastName','Nom', 'Signup'
GO
updatelanguagecontext 'fr','No_accent_In_Name','Le nom du portail ne doit pas contenir d''espace, de caractère accentué ou de ponctuation', 'Signup'
GO
updatelanguagecontext 'fr','S_Need_Portal_Name','Vous devez saisir un nom pour votre portail<br>', 'Signup'
GO
updatelanguagecontext 'fr','S_Need_Names','Le nom et prénom sont nécessaire pour votre enregistrement<br>', 'Signup'
GO
updatelanguagecontext 'fr','S_User_Name','Pour une question de sécurité nous vous demandons de choisir un codes d''accès avec un minimum de 8 lettres ou chiffres<br>', 'Signup'
GO
updatelanguagecontext 'fr','S_Password','Pour une question de sécurité nous vous demandons de choisir un môt de passe avec un minimum de 8 lettres ou chiffres.<br>', 'Signup'
GO
updatelanguagecontext 'fr','S_EMail','Vous devez fournir une adresse de courriel valide!<br>', 'Signup'
GO
updatelanguagecontext 'fr','S_NameAlreadyUsed','Le nom de domaine http://{PortalAlias} est déjà utilisé.  Vous devez choisir un autre nom de sous domaine.', 'Signup'
GO
updatelanguagecontext 'fr','S_NameAlreadyUsed1','Vous devez utiliser un autre nom de portail puisque celui-ci est déjà  utilisé.', 'Signup'
GO
updatelanguagecontext 'fr','S_F_Wait','Cliquer ici si vous n''êtes pas redirigé d''ici 10 sec...', 'Signup'
GO
updatelanguagecontext 'fr','S_Title','Ajouter un nouveau portail', 'Signup'
GO
updatelanguagecontext 'fr','SD_Title','Inscription portail démonstration', 'Signup'
GO
updatelanguagecontext 'fr','S_Wait','Un instant S.V.P.', 'Signup'
GO
updatelanguagecontext 'fr','S_F_Wait_Info','Nous créons votre portail web. Vous allez recevoir dans les prochaine minutes un courriel avec de l''information pour entrer sur votre site web la premère fois.', 'Signup'
GO
updatelanguagecontext 'fr','SS_Month','Mois', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Guest','Visiteur', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Renewal','- Renouvellement du Portail ( Frais de base: {HostFee} + Modules bonus: {ModuleFee} = Frais totaux: {TotalFee} )', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_SiteLogDays','Journal d''utilisation (jours)', 'SiteSettings'
GO
updatelanguagecontext 'fr','SiteSettings1','Habillage', 'SiteSettings'
GO
updatelanguagecontext 'fr','chkDemoDomain','votrenom', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_LoginModuleSkin','Login', 'SiteSettings'
GO
updatelanguagecontext 'fr','SiteSettings3','Localisation', 'SiteSettings'
GO
updatelanguagecontext 'fr','SiteSettings4','PortailDémo', 'SiteSettings'
GO
updatelanguagecontext 'fr','SiteSettings5','Subscription', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_TotalPortalFee','Total frais d''hébergement', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_PortalDiskSpace','Espace (MB)', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_PortalExpired','Date de fin du portail', 'SiteSettings'
GO
updatelanguagecontext 'fr','cmdRenew','Cliquer ici pour renouveler votre abonnement au portail', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_PortalAlias','Alias portail', 'SiteSettings'
GO
updatelanguagecontext 'fr','MS_ModuleDeleteSkin','Effacer tous les habillages des modules?', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_SiteModuleSkin','Portail', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_AdminModuleSkin','Admin', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_EditModuleSkin','Edit', 'SiteSettings'
GO
updatelanguagecontext 'fr','GetLanguage("SS_LoginModuleSkin")','Login', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_cmdProcessor','Aller au site web fournisseur de paiement', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_cmdGoogle','Soumettre l''info à Google', 'SiteSettings'
GO
updatelanguagecontext 'fr','SiteSettings2','Général', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Logo','Image d''entête', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Background','Image d''arrière plan', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_UserInfo','Afficher info utilisateur', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_SkinEdit','Modifier mise en forme', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_PortalCSS_Tooltip','Modifier les paramètres css pour le site web', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_TTTCSS_Tooltip','Modifier les paramètres css pour le module forum et la galerie de photos', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_PortalSkin_Tooltip','Modifier le fichier de mise en page par défaut pour le site web', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_EditSkin_Tooltip','Modifier le fichier de mise en page pour les pages administrateur', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Tigra','Modifier les paramètres pour le menu TIGRA', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Contener','Habillage pour modules', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_lnkcontainer','Aller chercher une définition d''habillage', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_ReplaceLOGO','Info à afficher en remplacement de l''entête', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Admin','Administrateur', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Email','Avis courriel', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_TimeZone','Fuseau Horaire', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_TypeRegistration','Type d''inscription', 'SiteSettings'
GO
updatelanguagecontext 'fr','optUserRegistration_0','Aucun', 'SiteSettings'
GO
updatelanguagecontext 'fr','optUserRegistration_1','Privé', 'SiteSettings'
GO
updatelanguagecontext 'fr','optUserRegistration_2','Publique', 'SiteSettings'
GO
updatelanguagecontext 'fr','optUserRegistration_3','Validé', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Vendors','Publicité&nbsp;fournisseur', 'SiteSettings'
GO
updatelanguagecontext 'fr','optBannerAdvertising_0','Aucun', 'SiteSettings'
GO
updatelanguagecontext 'fr','optBannerAdvertising_1','Site', 'SiteSettings'
GO
updatelanguagecontext 'fr','optBannerAdvertising_2','Serveur', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Currency','Devise', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Processor','Méthode de paiement', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Processor_Code','Code d''accès fournisseur paiement', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Processor_Password','Mot de passe fournisseur paiement', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Head_Language','Paramètres linguistiques de base', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Language_Default','Langue par défaut', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Language_ToUse','Langue à utiliser', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Head_Language_Info','Info spécifique pour la langue', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Mod_Info','Modifier info', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Terms_Tooltip','Modifier Conditions générales', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Terms_Text','Conditions Générales', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Privacy_Tooltip','Modifier Déclarations de confidentialité', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Privacy_Text','Déclarations de confidentialité', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Site_Title','Titre', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Site_Title_Bottom','Texte de bas de page', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Site_Keywords','Mots clés', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_txtlogin','Directives d''ouverture d''une session', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_txtRegistration','Directives d''inscriptions', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_txtSignup','Info annexé au courriel d''enregistrement', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_DemoDirectives','Directives pour la création d''un portail démo', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Head_Demo','Création sous portail', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Head_Demo_Allow','Permettre la création de portail démo?', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Demo_Domain','Domaine pour le Portail', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_Allow_Demo_Create','Permettre la création d''un portail web', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Head_PortalInfo','Détails de l''hébergement', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_PortalBasicFee','Frais de base pour l''hébergement', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Label_PortalExtraFee','Frais additionnel pour les modules', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Site_Description','Description en', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_ToolTipModuleSkin','ToolTip', 'SiteSettings'
GO
updatelanguagecontext 'fr','P_ssl','ssl', 'SiteSettings'
GO
updatelanguagecontext 'fr','SS_Use_SSL','Utiliser SSL', 'SiteSettings'
GO
updatelanguagecontext 'fr','P_sub','sub', 'SiteSettings'
GO
updatelanguagecontext 'fr','Skin_FileErased','Le fichier à été effacé', 'SkinEdit'
GO
updatelanguagecontext 'fr','SkinFileNoExist','Le fichier n''existe pas', 'SkinEdit'
GO
updatelanguagecontext 'fr','Skin_NeedForm','&lt;form&gt;&lt;/form&gt; doit être en séquence sur la page', 'SkinEdit'
GO
updatelanguagecontext 'fr','Skin_NeedForm2','&lt;form&gt;&lt;/form&gt; doit être présent sur la page', 'SkinEdit'
GO
updatelanguagecontext 'fr','Skin_NeedContentpane','{contentpane} doit être présent sur la page entre &lt;form&gt; et &lt;/form&gt;', 'SkinEdit'
GO
updatelanguagecontext 'fr','Skin_FileSaved','Le fichier à été sauvegardé', 'SkinEdit'
GO
updatelanguagecontext 'fr','Nothing_To_Do','Rien a faire', 'SQL'
GO
updatelanguagecontext 'fr','SQL_Info','Pour exécuter un script sur cette base de donné ou une autre base de donné', 'SQL'
GO
updatelanguagecontext 'fr','SQL_Execute','Exécuter le script', 'SQL'
GO
updatelanguagecontext 'fr','SQL_ExecuteCMD','Exécuter requète', 'SQL'
GO
updatelanguagecontext 'fr','SQL_NoFile','Vous devez indiquer un fichier pour exécuter les scripts!', 'SQL'
GO
updatelanguagecontext 'fr','SQL_Connection','Mettre les informations de connection si nécessaire', 'SQL'
GO
updatelanguagecontext 'fr','Stat_ToGet','Type d''analyse', 'Statistique'
GO
updatelanguagecontext 'fr','Stat_StartDate','Date de début', 'Statistique'
GO
updatelanguagecontext 'fr','Stat_Calendar','Calendier', 'Statistique'
GO
updatelanguagecontext 'fr','Stat_EndDate','Date de fin', 'Statistique'
GO
updatelanguagecontext 'fr','Stat_Display','Voir', 'Statistique'
GO
updatelanguagecontext 'fr','Sub_Every','chaque', 'Subscription'
GO
updatelanguagecontext 'fr','FrequencyCode_D','Jour(s)', 'Subscription'
GO
updatelanguagecontext 'fr','FrequencyCode_M','Mois', 'Subscription'
GO
updatelanguagecontext 'fr','FrequencyCode_N','aucun', 'Subscription'
GO
updatelanguagecontext 'fr','FrequencyCode_O','paiement unique', 'Subscription'
GO
updatelanguagecontext 'fr','FrequencyCode_W','Semaines', 'Subscription'
GO
updatelanguagecontext 'fr','FrequencyCode_Y','Année(s)', 'Subscription'
GO
updatelanguagecontext 'fr','day','Jour', 'SystemStat'
GO
updatelanguagecontext 'fr','hour','Heure', 'SystemStat'
GO
updatelanguagecontext 'fr','minute','Minute', 'SystemStat'
GO
updatelanguagecontext 'fr','seconde','Seconde', 'SystemStat'
GO
updatelanguagecontext 'fr','millisecond','Millisecond', 'SystemStat'
GO
updatelanguagecontext 'fr','ip_address','Adresse IP', 'SystemStat'
GO
updatelanguagecontext 'fr','browser','Fureteur', 'SystemStat'
GO
updatelanguagecontext 'fr','number_of_connections','Nombre de connections', 'SystemStat'
GO
updatelanguagecontext 'fr','server_up_time','Serveur actif depuis', 'SystemStat'
GO
updatelanguagecontext 'fr','number_of_restart','Redémarrages ASP.NET', 'SystemStat'
GO
updatelanguagecontext 'fr','free_memory','Mémoire disponible', 'SystemStat'
GO
updatelanguagecontext 'fr','cache_memory_used','Antémémoire utilisé', 'SystemStat'
GO
updatelanguagecontext 'fr','ts_xmltemplate','Gabarit XML', 'TabSettings'
GO
updatelanguagecontext 'fr','Generate_XML','Créer gabarit', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_visibleinfo','Utiliser seulement si la page n''a pas de dépendance', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_visible','Visible?', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_skin','fichier mise en forme', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_tabname','Nom de la page', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_icone','Icône', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_parenttab','Page parente', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_css','fichier CSS', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_editcss','Modifier CSS', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_editcssinfo','Modifier les paramêtre du fichier CSS', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_editskin','Modifier SKIN', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_editskininfo','Modifier les paramêtre du fichier mise en forme', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_editmenuparam','Modifier paramètres menu', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_tabtemplate','Gabarit de la page', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_disableinfo','L''option du menu de la page sera vide', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_disable','Désactivé l''hyperlien?', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_tableleftwidth','Largeur volet gauche', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_tablerightwidth','Largeur volet droite', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_TabAdminRole','Modifier', 'TabSettings'
GO
updatelanguagecontext 'fr','ts_TabViewRole','Voir', 'TabSettings'
GO
updatelanguagecontext 'fr','tigra_vertical','Vertical', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_horizontal','Horizontal', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_tester','Tester', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu3.1','Position du sous menu relatif au menu supérieur', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu4','Pour centrer le menu utiliser une valeur supérieure à 0.  Utiliser la largeur du menu pour centrer le menu dans l''écran, ou la largeur du logo (si vous centrer le logo aussi) pour que le coin gauche du menu soit aligner avec le logo', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu4.1','Le temps en milisecondes avant que le menu ouvre', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu4.2','Le temps en milisecondes avant que le menu ferme après l''avoir quitté', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_OpenClose','Ouverture/Fermeture', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Block_Top','Block_Top', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Block_Left','Block_Left', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Top','Top', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Left','Left', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Hauteur','Hauteur', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Largeur','Largeur', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Centrer','Centrer', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Ouverture','Ouverture', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Fermeture','Fermeture', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu1','MENU 1er niveau', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu2','MENU 2ième niveau', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu3','MENU 3ième niveau', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu1.1','Position du menu hauteur coin supérieur gauche', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu1.2','Position du menu largeur coin supérieur gauche', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu1.3','Différence verticale entre cellule en pixel', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu1.4','Différence horizontale entre cellule en pixel', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu1.5','Hauteur d''une cellule en pixel', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu1.6','Largeur d''une cellule en pixel', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_Menu2.1','Position du sous menu relatif au menu supérieur', 'Tigra'
GO
updatelanguagecontext 'fr','SS_Tigra_Edit','Modifier tigra', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_help1','Pour générer les paramètres nécessaire pour la création d''un menu horizontal, après la création vous pouvez modifier les informations dans la grille de paramêtres.  Pour voir le résultat sur l''écran vous devrez utiliser le bouton <b>tester</b>', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_help2','Prendre les informations de la grille de paramètres et générer un nouveau fichier menu</b> ainsi que d''afficher à l''écran le résultat', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_help3','Pour sauvegarder dans le fichier menu.  Assurer vous de cliquer sur tester si vous avez fait des modifications à la grille de paramètres.', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_return','Revenir sans sauvegarder.', 'Tigra'
GO
updatelanguagecontext 'fr','tigra_help','Pour générer les paramètres nécessaire pour la création d''un menu vertical, après la création vous pouvez modifier les informations dans la grille de paramêtres.  Pour voir le résultat sur l''écran vous devrez utiliser le bouton <b>tester</b>', 'Tigra'
GO
updatelanguagecontext 'fr','ManageTableUDT','Administrer les tables usager', 'UDT'
GO
updatelanguagecontext 'fr','OrderBy','Ordre de tri', 'UDT'
GO
updatelanguagecontext 'fr','grdFields_visible','Visible', 'UDT'
GO
updatelanguagecontext 'fr','AddField','Ajouter un champ', 'UDT'
GO
updatelanguagecontext 'fr','grdFields_Title','Titre', 'UDT'
GO
updatelanguagecontext 'fr','grdFields_Type','Type', 'UDT'
GO
updatelanguagecontext 'fr','DateTime','Date', 'UDT'
GO
updatelanguagecontext 'fr','Int32','Nombre', 'UDT'
GO
updatelanguagecontext 'fr','Boolean','Vraie/Faux', 'UDT'
GO
updatelanguagecontext 'fr','Move_Field_Up','Haut', 'UDT'
GO
updatelanguagecontext 'fr','Move_Field_Down','Bas', 'UDT'
GO
updatelanguagecontext 'fr','String','Texte', 'UDT'
GO
updatelanguagecontext 'fr','Decimal','Décimale', 'UDT'
GO
updatelanguagecontext 'fr','Users_Delete','Effacer les usagers non autorisés', 'User'
GO
updatelanguagecontext 'fr','U_Autorized','Autorisé', 'User'
GO
updatelanguagecontext 'fr','ManageUserRoles','Gérer profils de sécurités', 'User'
GO
updatelanguagecontext 'fr','U_SecurityControl','Contrôle d''ouverture de session', 'User'
GO
updatelanguagecontext 'fr','U_SecurityContryCode','Code de pays', 'User'
GO
updatelanguagecontext 'fr','U_SecurityIPFROM','IP de', 'User'
GO
updatelanguagecontext 'fr','U_SecurityIPTO','IP à', 'User'
GO
updatelanguagecontext 'fr','U_Created_Date','Date crée', 'User'
GO
updatelanguagecontext 'fr','U_LastLogin_Date','Dernière visite', 'User'
GO
updatelanguagecontext 'fr','U_Subscribe','S''inscrire', 'User'
GO
updatelanguagecontext 'fr','U_QUIT_site','Résigner', 'User'
GO
updatelanguagecontext 'fr','UO_RemovePU','Enlever usager préféré', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_no_pref','Vous n''avez aucun usager préféré', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_no_connect','Vous devez être connecté pour utiliser ce module', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Date_Send','Date envoyée', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_to','À', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_offline','Hors ligne', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_online','En ligne', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_pm','Message(s) privé(s)', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_ToRead','à lire', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Read','Lu', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_click_connect','Cliquer ici pour ouvrir une session et avoir accès à vos messages', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_InBox','Boîte de réception', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_OutBox','Boîte d''envoi', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_write','Écrire', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_ReMessage','<br>----------Message original-------------<br>', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Re','RE:', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Need_Object','Il doit y avoir un sujet et du texte dans le message!', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_No_User','* n''est pas un usager.', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_see_user','Voir le profil de {username}.', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_From','de', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Object','Sujet', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Date_received','Date reçue', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Message_Notice','{FullName},
Vous venez de recevoir un message de {SenderName}.
Pour lire ce message : {MessageURL}', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Notice_Object','Message sur {portalname}', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_NotFound','Pas trouvé!', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_see_message','Cliquer ici pour voir le message', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_reply','Répondre', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_KeepNew','Garder comme nouveau', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_FindUser','Trouver un usager', 'UserOnline'
GO
updatelanguagecontext 'fr','UOSearchHelp','Utiliser * pour une recherche avec troncature', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Message','Message', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_ModInfo','Modifier son profil', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_UserName','Nom usager', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Name','Nom', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Since','Depuis', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_LastOn','Dernière visite', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_NotRead','Non lu', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Select','Choisir', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_AddList','Ajouter à la liste des usagers préférés', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_SendMessage','Faire parvenir un message privé', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_SearchOptions','Options de recherche', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_See','Voir', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_LRegistered','Dernier', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_TRegistered','Total', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_YRegistered','Hier', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Send','Envoyer', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_OnLineNow','En ligne?', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Body','Usager préféré?', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_NoBody','Aucun usager préféré.', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_InfoUserName','Information usager', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_ListUserName','Liste usager préféré de {username}', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_SinceEver','Depuis toujours!', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_NeverConnected','S''est jamais connecté', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_AddToList','Ajouter à sa liste d''usager préféré', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_ASC','ordre croissant', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_DESC','ordre décroissant', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_by','par', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_or','en', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_IsBody','Usager préféré', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Page','Page', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_FirstPage','Première', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_PrevPage','Précédente', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_NextPage','Suivante', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_LastPage','Dernière', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_OnlyBody','Mes préférés seulement', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Anonymous','Visiteurs', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Members','Membres', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_OnLine_Now','En ligne présentement', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_AllUsers','Tous les usagers', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_OnlineOnly','Seulement les usagers en ligne', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_Registered','Inscription', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_NewMessage','Nouveau message', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_OldMessage','Déjà lu', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_BadUserID','Le numéro usager est invalide', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_NoMessage','Aucun message', 'UserOnline'
GO
updatelanguagecontext 'fr','UO_erase','Effacer tous les messages sélectionnés', 'UserOnline'
GO
updatelanguagecontext 'fr','Positive_Feedback','Rétroaction positive', 'Vendors'
GO
updatelanguagecontext 'fr','Negative_Feedback','Rétroaction négative', 'Vendors'
GO
updatelanguagecontext 'fr','vendors_message_feedback2','Si vous êtes déjà  membre, vous pouvez vous incrire ou récupérer votre code d''usager et mot de passe dans la section d''inscription. <b><a href=" {httpregister}">vous inscrire ici</a></b>.<br><br>', 'Vendors'
GO
updatelanguagecontext 'fr','vendors_user','User', 'Vendors'
GO
updatelanguagecontext 'fr','lnkmap','carte', 'Vendors'
GO
updatelanguagecontext 'fr','lnkFeedback','Rétroaction', 'Vendors'
GO
updatelanguagecontext 'fr','feedback_pos','Positive', 'Vendors'
GO
updatelanguagecontext 'fr','feedback_neg','Négative', 'Vendors'
GO
updatelanguagecontext 'fr','vendors_message_feedback1','Si vous désirez annoter un fournisseur, vous devez être un membre du site.<br>', 'Vendors'
GO
updatelanguagecontext 'fr','noFeedback','Aucune notation de disponible sur le fournisseur', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_Name','Nom oriflamme', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_Type','Type d''oriflamme', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_Image','Image', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_URL','* URL', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_Imp','Expositions', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_CPM','CPM', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_StartDate','* Date début', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_EndDate','* Date fin', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_Optional','* = optionnel', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_Clicks','Nombre de clicks', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_SeeClicks','Voir le registre?', 'Vendors'
GO
updatelanguagecontext 'fr','VendorBannerInfo','Le nom de l''oriflamme apparaitra comme texte alternatif à  l''image.<br><br>Si il n''y a pas URL, une page avec les informations du fournisseur sera affichée.<br><br>Pour un nombre illimité d''exposition mettre (0).<br><br>', 'Vendors'
GO
updatelanguagecontext 'fr','VendorBannerInfo1','Le CPM est le coût pour 1000 expositions de l''oriflamme du fournisseur.<br><br>Utiliser la date de début et la date de fin pour déterminer quand l''oriflamme apparaitra sur le site.', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_Info_List','La liste des fournisseurs est utilisée dans le portail pour gérer le répertoire des services ainsi que la publicité par oriflamme.', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_cmddelete','Effacer les fournisseurs non autorisés', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_Edit_Classifications','Modifier les classifications fournisseurs', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Non_Aut','Non autorisé', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_OptionsEdit','Modifier les paramètres fournisseurs', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_Add','Ajouter un fournisseur', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner1','Oriflamme', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner2','Bouton (petit)', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner3','Bouton (médium)', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner4','Bouton (grand)', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner5','Très gros', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_Directives','Directives d''inscriptions', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_WhoCanSubscribe','Qui peut s''inscrire?', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_WelcomeMessage','Message nouveau fournisseur', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_Bussiness','Compagnie', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_Fax','Fax', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_WebSite','Site Web', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_Logo','Logo', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Classification','Classifications', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_KeyWord','Mots clées', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Header_Search','Recherche', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Header_Request','Demandes', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Header_LastRequest','Denière demande', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_Publicity','Publicité par oriflamme', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Banner_Add','Ajouter un oriflamme', 'Vendors'
GO
updatelanguagecontext 'fr','Vender_Type','Type', 'Vendors'
GO
updatelanguagecontext 'fr','Vender_URL','URL', 'Vendors'
GO
updatelanguagecontext 'fr','Vender_View','Vue', 'Vendors'
GO
updatelanguagecontext 'fr','Vender_Clicks','Clics', 'Vendors'
GO
updatelanguagecontext 'fr','Vender_Start','Début', 'Vendors'
GO
updatelanguagecontext 'fr','Vender_End','Fin', 'Vendors'
GO
updatelanguagecontext 'fr','lnkDirections','Directions', 'Vendors'
GO
updatelanguagecontext 'fr','Vendors_AddName','Ajouter', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_ThankYou','Merci de votre contribution.', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Name','Nom du fournisseur', 'Vendors'
GO
updatelanguagecontext 'fr','Oriflamme','Oriflamme', 'Vendors'
GO
updatelanguagecontext 'fr','Vendor_Register','Inscription fournisseur', 'Vendors'
GO
updatelanguagecontext 'fr','vendors_date','date', 'Vendors'
GO
updatelanguagecontext 'fr','w_personalise','Personaliser?', 'Weather'
GO
updatelanguagecontext 'fr','XML_data','Fichier data XML', 'XML'
GO
updatelanguagecontext 'fr','XML_internal','Interne', 'XML'
GO
updatelanguagecontext 'fr','XML_external','Externe', 'XML'
GO
updatelanguagecontext 'fr','XML_transform','Ficher de transformation XSL', 'XML'
GO
UpdatelonglanguageSetting 'fr','Access Denied','<table width="80%" border="0" align="center">
    <tbody>
        <tr>
            <td> <span style="" class="ItemTitle"> Conditions g&eacute;n&eacute;rales d''utilisation entre vous et {PortalName}</span>  <br /> <br /> <span class="ItemTitle"> COMPTE D''UTILISATEUR, MOT DE PASSE ET S&Eacute;CURIT&Eacute;</span>  <br /> <br /> Si vous ouvrez un compte sur le site web {PortalName}, vous devez compl&eacute;ter le formulaire d''inscription avec des informations actuelles, compl&egrave;tes et exactes, comme le formulaire d''inscription en question vous y invite. Vous choisirez ensuite un mot de passe et un nom d''utilisateur. Vous &ecirc;tes enti&egrave;rement responsable du maintien de la confidentialit&eacute; de votre mot de passe et de votre nom d''utilisateur. Vous &ecirc;tes en outre enti&egrave;rement responsable de toute activit&eacute; ayant lieu sous votre compte. Vous vous engagez &agrave;&nbsp; avertir imm&eacute;diatement {PortalName} de toute utilisation non autoris&eacute;e de votre compte, ou de toute autre atteinte &agrave;&nbsp; la s&eacute;curit&eacute;. {PortalName} ne pourra en aucun cas &ecirc;tre tenue pour responsable d''un quelconque dommage que vous subiriez du fait de l''utilisation par autrui de votre mot de passe ou de votre compte, que vous ayez eu connaissance ou non de cette utilisation. N&eacute;anmoins, votre responsabilit&eacute; pourrait &ecirc;tre engag&eacute;e si {PortalName} ou un tiers subissait des dommages dus &agrave;&nbsp;l''utilisation par autrui de votre compte ou de votre mot de passe. Il ne vous est jamais permis d''utiliser le compte d''autrui sans l''autorisation du titulaire du compte.  <br /> <br /> <span class="ItemTitle"> MODIFICATION DES PR&Eacute;SENTES CONDITIONS D''UTILISATION</span>  <br /> <br /> {PortalName} se r&eacute;serve le droit de modifier les termes, conditions et mentions d''avertissement applicables au site et Services qui vous sont propos&eacute;s. Il vous incombe de consulter r&eacute;guli&egrave;rement ces termes et conditions d''utilisation. Votre utilisation renouvel&eacute;e du site web {PortalName} constitue l''acceptation de votre part de tous ces termes, conditions d''utilisation et mentions d''avertissement.  <br /> <br /> <span class="ItemTitle"> UTILISATION LIMIT&Eacute;E &Agrave; DES FINS PERSONNELLES ET NON COMMERCIALES</span>  <br /> <br /> Sauf indication contraire, le site web {PortalName} est destin&eacute; &agrave;&nbsp; &ecirc;tre utilis&eacute;s &agrave;&nbsp; des fins personnelles et non commerciales. Vous n''&ecirc;tes pas autoris&eacute; &agrave;&nbsp; modifier, copier, distribuer, transmettre, diffuser, repr&eacute;senter, reproduire, publier, conc&eacute;der sous licence, cr&eacute;er des oeuvres d&eacute;riv&eacute;es, transf&eacute;rer ou vendre tout information, logiciel, produit ou service obtenu &agrave;&nbsp; partir de ce site web. Vous ne pouvez pas utiliser le site web {PortalName} &agrave;&nbsp; des fins commerciales, sans l''autorisation expresse, &eacute;crite et pr&eacute;alable, de {PortalName}.  <br /> <br /> <span class="ItemTitle"> LIENS VERS DES SITES TIERS </span>  <br /> <br /> Le site web {PortalName} peut contenir des images de, et des liens vers des sites Web g&eacute;r&eacute;s par des tiers. {PortalName} n''exerce aucun contr&ocirc;le sur ces sites et n''assume aucune responsabilit&eacute; quant &agrave;&nbsp;leur contenu, ni notamment quant au contenu des liens pr&eacute;sent&eacute;s dans ces sites, ou encore aux modifications ou mises &agrave; jour apport&eacute;es &agrave;&nbsp; ces sites.  <br /> <br /> <span class="ItemTitle"> UTILISATION ILLICITE OU INTERDITE</span>  <br /> <br /> Vous ne pouvez utiliser le site web {PortalName} qu''&agrave;&nbsp; condition de garantir que vous ne l''utiliserez pas &agrave;&nbsp; des fins illicites ou interdites par ces termes, conditions d''utilisation et mentions d''avertissement.  <br /> <br /> <span class="ItemTitle">QUESTIONS</span> <br /> <br /> Si vous avez des questions ou des commentaires au sujet des conditions d''utilisation, n''h&eacute;sitez pas &agrave; vous adresser {PortalName} par courriel &agrave; {AdministratorEmail}. <br /> <br /> </td>
        </tr>
    </tbody>
</table>', null 
GO
UpdatelonglanguageSetting 'fr','AccessDeniedInfo','<table align="center" width="80%" border="0">
<tr>
<td><p>&#160;</p><p>&#160;</p>
<p>Votre profil de sécurité ne vous donne pas accès à cette section du site web. Si vous croyez que vous devriez y avoir accès ou pour avoir plus d''information n''hésitez
pas à vous adresser au {PortalName} par courriel à {AdministratorEmail}.</p>
<p>&#160;</p><p>&#160;</p>
</td></tr></table>', null 
GO
UpdatelonglanguageSetting 'fr','Demo_Portal_Info','<div align="left" class="normal">
<p align="left">
{Directives}
</p>
</div>
<div align="left" class="normal">
<span class="Head">Info importante</span>
<ol>
	<li>Vous devez choisir un nom pour votre nouveau site.  Pour un site d''essais l''adresse URL sera un sous répertoire du site web {DomainName}.  Tel que http://{DomainName}/votenom/</li>
	<li>Vous devez identifier, votre nom ainsi que votre prénom.</li>
	<li>Vous devez obligatoirement nous donner votre adresse de courriel.  Nous vous ferons parvenir par courriel un hyperlien pour activer votre compte d''administrateur de votre nouveau site web.</li>
	<li>Vous devez choisir un thème pour votre site,  pour les visonner avant de choisir vous avez juste à cliquer ici.&nbsp;&nbsp;<b><i>{lblpreview}</i></b></li>
	<li>Vous devez choisir un code d''accès ainsi qu''un môt de passe pour votre compte d''administrateur.  Pour des questions de sécurité nous vous demandons de choisir un môt de passe avec au moins 8 lettres ou chiffres et de même pour votre code d''accès. </li>
	<li>Vous devez accepter les termes et conditions d''utilisation. Après l''acceptation par l''hyperlien <b>j''accepte</b> dans le bas de la page, vous serez redirigé sur une nouvelle page pour completer la création de votre nouveau site web.</li>
	<li>Une fois que vous avez complété votre inscription et que votre nouveau site web a été créé.  Vous recevrez un <b>courriel avec un <font color="#ff0000">hyperlien</font></b> que vous devrez utiliser pour la première fois afin d''activer votre première session en tant qu''administrateur de votre site web.</li>
</ol>
</div>
<div align="left" class="normal">
<span class="Head">Acceptation des conditions générales d''utilisation entre vous et {PortalName}
</span>
<p align="left">
Vous ne pouvez utiliser le site web {PortalName} qu''à condition de garantir que vous ne l''utiliserez pas à  des fins illicites ou interdites par ces termes, conditions d''utilisation et mentions d''avertissement.<br>
<a href="https://{DomainName}/{language}.default.aspx?edit=control&def=Signup&guid={lblGUID}" title="Acceptation des conditions générales d''utilisation et après l''acceptation de ces termes vous pourrez créer un site d''essais">&nbsp;<b><font color="#ff0000">j''accepte</font></b></a>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://{DomainName}/" title="Retour">&nbsp;<b><font color="#669900">je refuse</font></b></a>
</p>
</div>', null 
GO
UpdatelonglanguageSetting 'fr','Demo_Portal_Info1','Il y a deux types de portails qui peuvent être créés:<br><br><li><b>Portail parent</b> est un site avec une adresse URL unique( ie. www.domain.com ) qui lui est associé. Vous devez avoir un nom de domaine.  Vous pouvez aussi utiliser une adresse IP ( ie. <b>65.174.86.217</b> ). Si vous désirez avoir plus qu''un nom de domaine pour un portail vous devez séparer les noms de domaine par une virgule ( ie. <b>www.domain1.com,www.domain2.com</b> ). ATTENTION ne créer pas de portail parent sans avoir préalablement fait faire l''installation du mapping DNS, sinon vous ne pourrez pas avoir accès au portail.<br><br><li><b>Portail enfant</b> est un sous-répertoire du nom de domaine du compte du serveur d''hébergement ( ie. www.domain.com/repertoire ). Un example <b>{DomainName}/nomportail</b>. A portail enfant peut être modifié en portail Parent n''importe quand.  Vous devez seulement faire une modification aux paramètres Alias.<br><br>*Note: Le portail sera créer en utilisant les propriétés par défaults selon la définition des modules sur le serveur d''hébergement ( Frais d''Hébergement, Espace disque, Période d''essais ). Vous pouvez toujours faire des modifications après la création du portail dans le module administration.<br><br>', null 
GO
UpdatelonglanguageSetting 'fr','Demo_Portal_Info2','L''inscription au portail de démonstration vous permet de créer votre propre portail web pour fin d''expérimentation et d''essais{DemoPeriod} pour une période de {days} jours{/DemoPeriod}.<br><br><b>*Note:</b> Le nom que vous choisirez pour votre portail doit être sans espace ou ponctuation. L''URL de votre portail sera <b>{DomainName}/nomduportail</b><br><br>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_AccessDenied','<table width="80%" border="0" align="center">
    <tbody>
        <tr>
            <td> <span style="" class="ItemTitle"> Conditions g&eacute;n&eacute;rales d''utilisation entre vous et {PortalName}</span>  <br /> <br /> <span class="ItemTitle"> COMPTE D''UTILISATEUR, MOT DE PASSE ET S&Eacute;CURIT&Eacute;</span>  <br /> <br /> Si vous ouvrez un compte sur le site web {PortalName}, vous devez compl&eacute;ter le formulaire d''inscription avec des informations actuelles, compl&egrave;tes et exactes, comme le formulaire d''inscription en question vous y invite. Vous choisirez ensuite un mot de passe et un nom d''utilisateur. Vous &ecirc;tes enti&egrave;rement responsable du maintien de la confidentialit&eacute; de votre mot de passe et de votre nom d''utilisateur. Vous &ecirc;tes en outre enti&egrave;rement responsable de toute activit&eacute; ayant lieu sous votre compte. Vous vous engagez &agrave;&nbsp; avertir imm&eacute;diatement {PortalName} de toute utilisation non autoris&eacute;e de votre compte, ou de toute autre atteinte &agrave;&nbsp; la s&eacute;curit&eacute;. {PortalName} ne pourra en aucun cas &ecirc;tre tenue pour responsable d''un quelconque dommage que vous subiriez du fait de l''utilisation par autrui de votre mot de passe ou de votre compte, que vous ayez eu connaissance ou non de cette utilisation. N&eacute;anmoins, votre responsabilit&eacute; pourrait &ecirc;tre engag&eacute;e si {PortalName} ou un tiers subissait des dommages dus &agrave;&nbsp;l''utilisation par autrui de votre compte ou de votre mot de passe. Il ne vous est jamais permis d''utiliser le compte d''autrui sans l''autorisation du titulaire du compte.  <br /> <br /> <span class="ItemTitle"> MODIFICATION DES PR&Eacute;SENTES CONDITIONS D''UTILISATION</span>  <br /> <br /> {PortalName} se r&eacute;serve le droit de modifier les termes, conditions et mentions d''avertissement applicables au site et Services qui vous sont propos&eacute;s. Il vous incombe de consulter r&eacute;guli&egrave;rement ces termes et conditions d''utilisation. Votre utilisation renouvel&eacute;e du site web {PortalName} constitue l''acceptation de votre part de tous ces termes, conditions d''utilisation et mentions d''avertissement.  <br /> <br /> <span class="ItemTitle"> UTILISATION LIMIT&Eacute;E &Agrave; DES FINS PERSONNELLES ET NON COMMERCIALES</span>  <br /> <br /> Sauf indication contraire, le site web {PortalName} est destin&eacute; &agrave;&nbsp; &ecirc;tre utilis&eacute;s &agrave;&nbsp; des fins personnelles et non commerciales. Vous n''&ecirc;tes pas autoris&eacute; &agrave;&nbsp; modifier, copier, distribuer, transmettre, diffuser, repr&eacute;senter, reproduire, publier, conc&eacute;der sous licence, cr&eacute;er des oeuvres d&eacute;riv&eacute;es, transf&eacute;rer ou vendre tout information, logiciel, produit ou service obtenu &agrave;&nbsp; partir de ce site web. Vous ne pouvez pas utiliser le site web {PortalName} &agrave;&nbsp; des fins commerciales, sans l''autorisation expresse, &eacute;crite et pr&eacute;alable, de {PortalName}.  <br /> <br /> <span class="ItemTitle"> LIENS VERS DES SITES TIERS </span>  <br /> <br /> Le site web {PortalName} peut contenir des images de, et des liens vers des sites Web g&eacute;r&eacute;s par des tiers. {PortalName} n''exerce aucun contr&ocirc;le sur ces sites et n''assume aucune responsabilit&eacute; quant &agrave;&nbsp;leur contenu, ni notamment quant au contenu des liens pr&eacute;sent&eacute;s dans ces sites, ou encore aux modifications ou mises &agrave; jour apport&eacute;es &agrave;&nbsp; ces sites.  <br /> <br /> <span class="ItemTitle"> UTILISATION ILLICITE OU INTERDITE</span>  <br /> <br /> Vous ne pouvez utiliser le site web {PortalName} qu''&agrave;&nbsp; condition de garantir que vous ne l''utiliserez pas &agrave;&nbsp; des fins illicites ou interdites par ces termes, conditions d''utilisation et mentions d''avertissement.  <br /> <br /> <span class="ItemTitle">QUESTIONS</span> <br /> <br /> Si vous avez des questions ou des commentaires au sujet des conditions d''utilisation, n''h&eacute;sitez pas &agrave; vous adresser {PortalName} par courriel &agrave; {AdministratorEmail}. <br /> <br /> </td>
        </tr>
    </tbody>
</table>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_AddModuleDef','<h1 style="text-align: center;">Ajout nouveau module</h1>
<p>Vous pouvez ajouter un nouveau module en utilisant un gabarit xml.</p>
<p>La structure du fichier xml doit être la suivante :</p>
<p>&#160;</p>
<p>&#160;</p>
<div style="margin-left: 1em; text-indent: -2em;" class="c"><span class="m">&lt;</span><span class="m">&lt;</span><span class="t">module</span><span class="m">&gt;</span></div>
<div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;"><span class="b">&#160;</span>&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">folder</span> <span class="m">/&gt;</span></div>
</div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;"><span class="b">&#160;</span>&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">friendlyname</span><span class="m">&gt;</span><span class="m">&lt;/</span><span class="t">friendlyname</span><span class="m">&gt;</span></div>
</div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;"><span class="b">&#160;</span>&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">desktopsrc</span><span class="m">&gt;</span><span class="m">&lt;/</span><span class="t">desktopsrc</span><span class="m">&gt;</span></div>
</div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;"><span class="b">&#160;</span>&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">helpsrc</span> <span class="m">/&gt;</span></div>
</div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;"><span class="b">&#160;</span>&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">editsrc</span><span class="m">&gt;</span><span class="m">&lt;/</span><span class="t">editsrc</span><span class="m">&gt;</span></div>
</div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;"><span class="b">&#160;</span>&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">description</span><span class="m">&gt;</span><span class="m">&lt;/</span><span class="t">description</span><span class="m">&gt;</span></div>
</div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;"><span class="b">&#160;</span>&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">editmoduleicon</span> <span class="m">/&gt;</span></div>
</div>
<div class="e">
<div><span class="b">&#160;</span>&#160; <span class="m">&lt;</span><span class="t">uninstall</span> <span class="m">/&gt;</span></div>
<div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;" class="c"><a href="#" onclick="return false" onfocus="h()" class="b">-</a>&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">files</span><span class="m">&gt;</span></div>
<div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;" class="c"><a href="#" onclick="return false" onfocus="h()" class="b">-</a>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">file</span><span class="m">&gt;</span></div>
<div>
<div class="e">
<div style="margin-left: 1em; text-indent: -2em;"><span class="b">&#160;</span>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span class="m">&lt;</span><span class="t">name</span><span class="m">&gt;</span><span class="m">&lt;/</span><span class="t">name</span><span class="m">&gt;</span></div>
</div>
<div><span class="b">&#160;</span>&#160;&#160;&#160; <span class="m">&lt;/</span><span class="t">file</span><span class="m">&gt;</span></div>
</div>
</div>
<div class="e">
<div>&#160;&#160; <span class="m">&lt;/</span><span class="t">files</span><span class="m">&gt;</span></div>
</div>
</div>
</div>
</div>
<div><span class="m">&lt;/</span><span class="t">module</span><span class="m">&gt;</span></div>
</div>
</div>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_AdminMenu','<table cellspacing="3" cellpadding="3" border="0">
    <tbody>
        <tr>
            <td><img src="../../images/icon_sitesettings_36px.gif" alt="Paramètres du site" title="Paramètres du site" style="border-width: 0px;" /></td>
            <td>Paramètres du site</td>
            <td>
            <p class="normal">Les paramètres du site représentent les options locales pour votre portail. Le module vous permet d''adapter votre portail pour répondre à vos exigences.</p>
            </td>
        </tr>
        <tr>
            <td><img src="../../images/icon_tabs_34px.gif" alt="Onglets" title="Onglets" style="border-width: 0px;" /></td>
            <td>Onglets</td>
            <td>
            <p class="normal">L''administrateur peut contrôler les onglets dans le portail. Ce module permet de créer, modifier, supprimer, changer l''ordre et de changer le niveau hiérarchique des onglets.</p>
            </td>
        </tr>
        <tr>
            <td><img src="../../images/icon_securityroles_32px.gif" alt="Paramètres de sécurité" title="Paramètres de sécurité" style="border-width: 0px;" /></td>
            <td>Paramètres de sécurité</td>
            <td>
            <p class="normal">L''administrateur peut contrôler les profils de sécurité du portail. Le module vous permet d''ajouter, modifier, supprimer et contrôler les profils de sécurités ainsi que leurs attribution aux utilisateurs.</p>
            </td>
        </tr>
        <tr>
            <td><img src="../../images/icon_users_32px.gif" alt="Gestion usagers" title="Gestion usagers" style="border-width: 0px;" /></td>
            <td>Gestion usagers</td>
            <td>
            <p class="normal">Le module sert à gérer les utilisateurs enregistrés. Il vous permet d''ajouter, modifier et supprimer des comptes utilisateurs ainsi que de contrôler leurs profils de sécurité.</p>
            </td>
        </tr>
        <tr>
            <td><img src="../../images/icon_filemanager_32px.gif" alt="Gestion fichiers" title="Gestion fichiers" style="border-width: 0px;" /></td>
            <td>Gestion fichiers</td>
            <td>
            <p class="normal">Le module sert à gérer les fichiers stockés sur le portail web. Le module vous permet de télécharger, supprimer et de synchroniser la liste des fichiers avec la basse de données SQL. Il fournit également des informations sur la quantité d''espace disque utilisée et disponible.</p>
            </td>
        </tr>
        <tr>
            <td><img src="../../images/icon_vendors_32px.gif" alt="Fournisseurs" title="Fournisseurs" style="border-width: 0px;" /></td>
            <td>Fournisseurs</td>
            <td>
            <p class="normal">Le module sert à gérer les comptes fournisseurs et les oriflammes associés au portail. Ce module vous permet d''ajouter un nouveau, modifier, et supprimer un compte fournisseur.</p>
            </td>
        </tr>
        <tr>
            <td><img src="../../images/icon_sitelog_32px.gif" alt="Statistiques" title="Statistiques" style="border-width: 0px;" /></td>
            <td>Statistiques</td>
            <td>
            <p class="normal">Le module sert à voir les statistiques d''utilisations de votre portail. Plus particulièrement, qui utilise le site, quand et comment.</p>
            </td>
        </tr>
        <tr>
            <td><img src="../../images/icon_bulkmail_32px.gif" alt="Courriel" title="Courriel" style="border-width: 0px;" /></td>
            <td>Courriel</td>
            <td>
            <p class="normal">Le module sert à envoyer du courriel en bloc aux utilisateurs appartenant à un profil de sécurité particulier.</p>
            </td>
        </tr>
    </tbody>
</table>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_BulkEmail','<h1 style="text-align: center; ">Envoi de Courriel</h1><p>&#160;</p>
<p>Les administrateurs peuvent faire parvenir un courriel aux utilisateurs appartenant à un rôle de sécurité ou à une adresse de courriel en particulier. Le courriel est envoyé à chaque utilisateur séparément, sans indiquer les autres utilisateurs dans l''adresse du courriel.  Les envois peuvent être, soit en format texte ou HTML. Vous pouvez ajouter une pièce jointe au courriel à partir de votre gestionnaire de fichiers.</p><p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Demo','<table width="80%" border="0" align="center">
    <tbody>
        <tr>
            <td> <span style="" class="ItemTitle"> Conditions g&eacute;n&eacute;rales d''utilisation entre vous et {PortalName}</span>  <br /> <br /> <span class="ItemTitle"> COMPTE D''UTILISATEUR, MOT DE PASSE ET S&Eacute;CURIT&Eacute;</span>  <br /> <br /> Si vous ouvrez un compte sur le site web {PortalName}, vous devez compl&eacute;ter le formulaire d''inscription avec des informations actuelles, compl&egrave;tes et exactes, comme le formulaire d''inscription en question vous y invite. Vous choisirez ensuite un mot de passe et un nom d''utilisateur. Vous &ecirc;tes enti&egrave;rement responsable du maintien de la confidentialit&eacute; de votre mot de passe et de votre nom d''utilisateur. Vous &ecirc;tes en outre enti&egrave;rement responsable de toute activit&eacute; ayant lieu sous votre compte. Vous vous engagez &agrave;&nbsp; avertir imm&eacute;diatement {PortalName} de toute utilisation non autoris&eacute;e de votre compte, ou de toute autre atteinte &agrave;&nbsp; la s&eacute;curit&eacute;. {PortalName} ne pourra en aucun cas &ecirc;tre tenue pour responsable d''un quelconque dommage que vous subiriez du fait de l''utilisation par autrui de votre mot de passe ou de votre compte, que vous ayez eu connaissance ou non de cette utilisation. N&eacute;anmoins, votre responsabilit&eacute; pourrait &ecirc;tre engag&eacute;e si {PortalName} ou un tiers subissait des dommages dus &agrave;&nbsp;l''utilisation par autrui de votre compte ou de votre mot de passe. Il ne vous est jamais permis d''utiliser le compte d''autrui sans l''autorisation du titulaire du compte.  <br /> <br /> <span class="ItemTitle"> MODIFICATION DES PR&Eacute;SENTES CONDITIONS D''UTILISATION</span>  <br /> <br /> {PortalName} se r&eacute;serve le droit de modifier les termes, conditions et mentions d''avertissement applicables au site et Services qui vous sont propos&eacute;s. Il vous incombe de consulter r&eacute;guli&egrave;rement ces termes et conditions d''utilisation. Votre utilisation renouvel&eacute;e du site web {PortalName} constitue l''acceptation de votre part de tous ces termes, conditions d''utilisation et mentions d''avertissement.  <br /> <br /> <span class="ItemTitle"> UTILISATION LIMIT&Eacute;E &Agrave; DES FINS PERSONNELLES ET NON COMMERCIALES</span>  <br /> <br /> Sauf indication contraire, le site web {PortalName} est destin&eacute; &agrave;&nbsp; &ecirc;tre utilis&eacute;s &agrave;&nbsp; des fins personnelles et non commerciales. Vous n''&ecirc;tes pas autoris&eacute; &agrave;&nbsp; modifier, copier, distribuer, transmettre, diffuser, repr&eacute;senter, reproduire, publier, conc&eacute;der sous licence, cr&eacute;er des oeuvres d&eacute;riv&eacute;es, transf&eacute;rer ou vendre tout information, logiciel, produit ou service obtenu &agrave;&nbsp; partir de ce site web. Vous ne pouvez pas utiliser le site web {PortalName} &agrave;&nbsp; des fins commerciales, sans l''autorisation expresse, &eacute;crite et pr&eacute;alable, de {PortalName}.  <br /> <br /> <span class="ItemTitle"> LIENS VERS DES SITES TIERS </span>  <br /> <br /> Le site web {PortalName} peut contenir des images de, et des liens vers des sites Web g&eacute;r&eacute;s par des tiers. {PortalName} n''exerce aucun contr&ocirc;le sur ces sites et n''assume aucune responsabilit&eacute; quant &agrave;&nbsp;leur contenu, ni notamment quant au contenu des liens pr&eacute;sent&eacute;s dans ces sites, ou encore aux modifications ou mises &agrave; jour apport&eacute;es &agrave;&nbsp; ces sites.  <br /> <br /> <span class="ItemTitle"> UTILISATION ILLICITE OU INTERDITE</span>  <br /> <br /> Vous ne pouvez utiliser le site web {PortalName} qu''&agrave;&nbsp; condition de garantir que vous ne l''utiliserez pas &agrave;&nbsp; des fins illicites ou interdites par ces termes, conditions d''utilisation et mentions d''avertissement.  <br /> <br /> <span class="ItemTitle">QUESTIONS</span> <br /> <br /> Si vous avez des questions ou des commentaires au sujet des conditions d''utilisation, n''h&eacute;sitez pas &agrave; vous adresser {PortalName} par courriel &agrave; {AdministratorEmail}. <br /> <br /> </td>
        </tr>
    </tbody>
</table>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_DiscussDetails','<div style="text-align: center;">
<h1>&Eacute;crire un nouveau message</h1>
</div>
<p>La zone de discussion est un espace virtuel qui permet de discuter « librement » sur des sujets divers</p>
<p>Vous devez saisir un titre ainsi que du contenu dans le message avant de l''enregistrer.</p>
<p>
Les codes HTML ne sont pas permits.
</p><p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Discussions','<div style="text-align: center;">
<h1>Zone discussions</h1>
</div>
<p>&#160;</p>
La zone de discussion est un endroit pour discuter s''exprimer sur différents sujet.  Limitez vos discussions sur des sujets d''intérêts généraux. N''abusez pas de votre pouvoir. Ce n''est pas parce que l''on peut faire quelque chose que l''on a le droit de le faire et ce n''est pas parce que l''on a le droit de faire quelque chose qu''il faut le faire. Rappelez vous qu''Internet n''est pas une zone de non droit et qu''on y est rarement complètement anonyme. Sur Internet aussi vous êtes censés respecter les lois de votre pays (droits d''auteur, informations illicites, protection de l''intégrité humaine, ...).  Les participants sont personnellement responsables de leurs écrits; ils s''interdisent tout propos diffamatoire, injurieux, raciste ou susceptible de tomber sous le coup de la loi. Leur responsabilité pourra être engagée en cas de manquement à cette règle.  Toute personne désireuse de faire supprimer une discussion lui portant un préjudice personnel doit adresser sa plainte par courriel en expliquant le motif de sa demande.
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditAccessDenied','<h1>Vous n''avez pas accès à cette page.</h1><p>&#160;</p><p>&#160;</p><p>&#160;</p>
<p>Si vous n''avez pas ouvert une session vous devriez essayer maintenant.</p>
<p>Il est possible que votre session est expiré après une inactivité de plus que 20 minutes.</p>
<p>Il est aussi possible que votre accès à cette fonction ai été révoqué par le webmestre.</p><p>&#160;</p>
<p>&#160;</p><p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditAlbum','<h1 style="text-align: center;">Album photo</h1>
<h2>Paramètres Album</h2>
<ul>
    <li>URL: L''url de l''album sur le site web.</li>
    <li>NOM: Nom de l''album.</li>
    <li>Propriétaire: L''usager qui est propriétaire de l''album.</li>
    <li>Titre: Le titre est le nom qui sera affiché.</li>
    <li>Description: Une information descriptive.</li>
    <li>Catégories: Le type de fichier dans l''album.</li>
    <li>Modifier : <img src="../../images/Edit.gif" alt="" /></li>
    <li>Effacer : <img src="../../images/delete.gif" alt="" /></li>
</ul>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditAnnoncement','<div style="text-align: center;">
<h1>Babillard</h1>
<div style="text-align: left;">
<h2>Options obligatoires :</h2>
<ul>
    <li>Titre : Le titre sera affich&eacute; en premier.</li>
    <li>Description : La description suivra le titre.</li>
</ul>
<h2>Options facultatives :</h2>
<ul>
    <li>Lien fichier :&nbsp; Si vous utiliser le lien fichier un hyperlien s''affichera apr&egrave;s la description du communiqu&eacute;.</li>
    <li>Expiration :&nbsp; Si vous d&eacute;signez une date d''expiration, le communiqu&eacute; ne sera pas affich&eacute; apr&egrave;s cette date.</li>
    <li>Ordre de parution : Pour d&eacute;signer l''ordre de parution, sinon l''ordre sera en fonction de la derni&egrave;re date de mis &agrave; jours du communiqu&eacute;.</li>
    <li>Syndication : Cocher la case pour que le communiqu&eacute; soit inclus dans le fichier RSS cr&eacute;e apr&egrave;s la mise a jours du babillard.</li>
    <li>Contenu en syndication : Cliquer sur cette option pour cr&eacute;er le ficher RSS (XML)</li>
    <li>Voir statistiques : Cocher la case pour voir l''historique de clicks sur l''hyperlien &quot;voir plus&quot;. (activ&eacute; par l''option lien fichier)</li>
</ul>
</div>
</div>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditBaners','<h1 style="text-align: center;">Oriflammes</h1>
<p>Le nom de l''oriflamme apparaitra comme texte alternatif à l''image.  Si il n''y a pas URL, une page avec les informations du fournisseur sera affichée.  Pour un nombre illimité d''exposition mettre (0).  Le CPM est le coût pour 1000 expositions de l''oriflamme du fournisseur.  Utiliser la date de début et la date de fin pour déterminer quand l''oriflamme apparaitra sur le site.</p>
<p>Les oriflammes publicitaires sont là pour promouvoir tout service que ce soit en permettant à l''utilisateur qui clique dessus de se rendre directement sur l''espace du site concerné.</p>
<p>Elles sont constituées d''une image  renvoyant grâce à un lien l''utilisateur vers le site visé.</p>
<p>Les tailles des bannières respectent un "standard"[1] fixé par l''IAB : International Advertising Bureau</p>
<h3>Bannières</h3>
<ul>
    <li>468 x 60 px</li>
    <li>392 x 72 px</li>
    <li>234 x 60 px</li>
</ul>
<h3>Boutons</h3>
<ul>
    <li>120 x 60</li>
    <li>88 x 31</li>
</ul>
<h3>Bilboards</h3>
<ul>
    <li>128 x 90</li>
    <li>180 x 150</li>
    <li>300 x 250</li>
    <li>336 x 280</li>
</ul>
<h3>Carré</h3>
<ul>
    <li>125 x 125</li>
    <li>250 x 250</li>
</ul>
<h3>Skyscraper</h3>
<ul>
    <li>120 x 240</li>
    <li>120 x 600</li>
    <li>160 x 600</li>
    <li>240 x 400</li>
</ul>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditBanners','<h1 align="center">Paramètres d''affichage oriflammes</h1>
<ul>
    <li>
    <div align="left">Sources de l''enseigne : Indiquer la source des oriflammes.  Soit celle de votre site web ou celle du serveur.</div>
    </li>
    <li>
    <div align="left">Type d''enseigne :&#160; Vous avez le choix entre différentes sélection.</div>
    </li>
    <li>
    <div align="left">Nombre :&#160; Indiquer le nombre d''enseigne à afficher .  Si plus d''enseigne sont disponible que le nombre indiqué, elle seront choisi d''une façon aléatoire par le logiciel.</div>
    </li>
</ul>
<p>Les bannières publicitaires sont là pour promouvoir tout service que ce soit en permettant à l''utilisateur qui clique dessus de se rendre directement sur l''espace du site concerné.</p>
<p>Elles sont constituées d''une image  renvoyant grâce à un lien l''utilisateur vers le site visé.</p>
<p>Les tailles des bannières respectent un "standard"[1] fixé par l''IAB : International Advertising Bureau</p>
<h3>Bannières</h3>
<ul>
    <li>468 x 60 px</li>
    <li>392 x 72 px</li>
    <li>234 x 60 px</li>
</ul>
<h3>Boutons</h3>
<ul>
    <li>120 x 60</li>
    <li>88 x 31</li>
</ul>
<h3>Bilboards</h3>
<ul>
    <li>128 x 90</li>
    <li>180 x 150</li>
    <li>300 x 250</li>
    <li>336 x 280</li>
</ul>
<h3>Carré</h3>
<ul>
    <li>125 x 125</li>
    <li>250 x 250</li>
</ul>
<h3>Skyscraper</h3>
<ul>
    <li>120 x 240</li>
    <li>120 x 600</li>
    <li>160 x 600</li>
    <li>240 x 400</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditContacts','<h1 style="text-align: center;">CONTACTS</h1>
<p>Le module fournit les coordonnées d''un groupe de personnes, par exemple une équipe de projet. Les dossiers sont affichés par ordre alphabétique par le nom du contact et chaque enregistrement de comprend le nom, le rôle, l''adresse e-mail et deux numéros de téléphone.<br />
<br />
L''adresse de courriel est masqué pour prévenir la récolte par les robots des organisations de courriel indésirable.<br />
<br />
Ajouter un nouveau contact sur le module Contacts.</p>
<ol>
    <li>Sélectionnez Ajouter un nouveau contact dans le module menu.</li>
    <li>Dans le champ Nom, entrez le nom de contact. Par exemple, Jo Tremblay.</li>
    <li>Dans le champ Rôle (facultatif), entrez le rôle du contact dans le groupe. Par exemple, Secrétaire Général.</li>
    <li>Dans le champ Courriel (facultatif), entrez l''adresse électronique de contact. Par exemple, Jo.Tremblay@domain.com.</li>
    <li>Dans le champ téléphone 1 (facultatif), entrez le numéro principal de téléphone.</li>
    <li>Dans le champ téléphone 2 (facultatif), entrez le numéro secondaire de téléphone.</li>
    <li>Cliquez sur Enregistrer.</li>
</ol>
', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditDirectory','<h1>Annuaire de service fournisseurs</h1>
<ul>
    <li>
    <div align="left">Source du répertoire : Indiquer le répertoire source de l''annuaire de service des fournissuers.  Soit celle de votre site web ou celle du serveur.</div>
    </li>
    <li>
    <div align="left">Voir les commentaires :&#160; Les usagers du site pourront inscrire des commentaires sur un fournisseur si cette option est activée.</div>
    </li>
    <li>
    <div align="left">Autoriser les fournisseurs à s''inscrire :&#160; Pour permettre à un fournisseurs de d''auto inscrire.</div>
    </li>
</ul>
<p>Le module utilise la même source pour le module des oriflammes.  Donc un fournisseurs devra être inscrit dans l''annuaire de service pour ensuite utiliser l''affiche d''une bannières publicitaires.</p>
<p>Les tailles des bannières utilisées</p>
<h3>Bannières</h3>
<ul>
    <li>468 x 60 px</li>
    <li>392 x 72 px</li>
    <li>234 x 60 px</li>
</ul>
<h3>Boutons</h3>
<ul>
    <li>120 x 60</li>
    <li>88 x 31</li>
</ul>
<h3>Bilboards</h3>
<ul>
    <li>128 x 90</li>
    <li>180 x 150</li>
    <li>300 x 250</li>
    <li>336 x 280</li>
</ul>
<h3>Carré</h3>
<ul>
    <li>125 x 125</li>
    <li>250 x 250</li>
</ul>
<h3>Skyscraper</h3>
<ul>
    <li>120 x 240</li>
    <li>120 x 600</li>
    <li>160 x 600</li>
    <li>240 x 400</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditDocs','<div style="text-align: center;">
<h1>DOCUMENTS</h1>
<div style="text-align: left;">Le module produit une liste de documents avec des liens pour le voir ou télécharger le document. Les documents peuvent être situés à l''intérieur du site, ou peut-être un lien vers un document sur un autre site Web. Chaque document de la liste est affiché par titre et par catégorie.<br />
<br />
Le nom de l''utilisateur qui a fait la dernière mise à jour du document, la date à laquelle le document a été ajoutée ou mise à jour et la taille des fichiers (documents internes seulement) sont automatiquement affiché pour chaque document dans la liste. Des statistiques sur&#160; l''utilisation des documents sont également disponibles.<br />
<br />
<ul>
    <li>Le titre est obligatoire et sera afficher dans la liste pour décrire le ficher.</li>
    <li>L''option document inter/externe est pour désigner l''emplacement du ficher.</li>
    <li>L''option catégorie est pour aider les usagers à déterminer le type de fichier.</li>
    <li>L''option syndication doit être coché pour inclure le document dans la liste RSS (XML) de syndication.</li>
    <li>Pour générer à nouveau le fichier RSS (XML) vous devez utiliser l''option "Contenu en syndication"</li>
    <li>Cliquer sur l''option voir statistiques pour afficher le nombre de téléchargement du fichier.</li>
</ul>
</div>
</div>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditEvents','<h1 style="text-align: center;">Evénements</h1>
<p>Le module affiche les événements à venir dans une liste par ordre chronologique ou en format de calendrier. Chaque événement incluent le titre, la description et la date. Affichage d''une image et l''événement est facultative.<br />
<br />
Chaque événement peut avoir une date d''expiration, ou être valide pour un nombre spécifié de jours, semaines, mois ou années. La hauteur et largeur des cellules pour le calendrier peut être fixé.</p>
<p>Paramètres :</p>
<ul>
    <li>Format : Les évènements peuvent être affichés soit en liste ou dans un calendrier.</li>
    <li>Calendrier : Vous pouvez désigner la largeur et hauteur minimale des cellules du calendrier.</li>
</ul>
<p>Options obligatoires :</p>
<ul>
    <li>Titre : sera afficher pour désigner l''évènement dans la liste ou dans le calendrier si l''icône est absent.</li>
    <li>Description : sera afficher en info bulle.</li>
    <li>Texte alternatif : Pour remplacement si jamais l''icône ne s''affiche pas</li>
    <li>Date de début : Date à lequel l''évènement sera afficher.</li>
</ul>
<p>Options facultatives :</p>
<ul>
    <li>Intervalles : Choisir l''intervalle en jours, semaine, mois et le nombre.</li>
    <li>Date de fin : Pour un évènement sur plusieurs jours</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditFAQ','<div style="text-align: center;">
<h1>Questions répétitives</h1>
<div style="text-align: left;">Des <b>questions répétitives</b>, ou une <b>foire aux questions </b><b>(</b> en anglais <i><b>FAQ</b></i> pour <i>Frequently Asked Questions</i> ), est une liste faisant la synthèse des questions posées de manière récurrente sur un sujet donné, accompagnées des réponses correspondantes, que l''on rédige afin d''éviter que les mêmes questions soient toujours reposées, et d''avoir à y répondre constamment. </div>
<div style="text-align: left;">&#160;</div>
<div style="text-align: left;">Saisir une question ainsi qu''une réponse.<br />
<br />
Il n''y a aucune limite au nombre de question.<br />
<br />
Un info bulle affichera la réponse lorsque l''utilisateur fera passé la souris au dessus de la question.<br />
<p>&#160;</p><p>&#160;</p>
</div>
</div>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditFile','<h1 style="text-align: center;">Modifier fichier</h1>
<h2>Paramètres d''un fichier</h2>
<ul>
    <li>URL: L''url du fichier sur le site web.</li>
    <li>NOM: Nom du fichier.</li>
    <li>Propriétaire: L''usager qui est propriétaire du fichier.</li>
    <li>Titre: Le titre est le nom qui sera affiché.</li>
    <li>Description: Une information descriptive.</li>
    <li>Catégories: Le type de fichier dans l''album.</li>
</ul>
<p>&#160;</p><p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditHTML','<div style="text-align: center;">
<h1>HTML/TEXTE</h1>
</div>
<ul>
    <li>Option texte :&nbsp; &Eacute;diteur de base en mode texte &quot;TextBox&quot;.&nbsp; L''&eacute;diteur accepte tous les codes HTML.</li>
    <li>Option HTML : Avec firefox et microsoft exploreur, vous pourrez utilisez un &eacute;diteur HTML enrichie.</li>
</ul>
<p>&#160;</p>
<p>Si vous utilisez le module de recherche, vous pouvez saisir des informations additionnelles facultatives.</p><p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditIFrame','<h1 style="text-align: center;">IFRAME</h1>
<p>L''élément <b><i>IFRAME</i></b> permet aux auteurs d''insérer un cadre dans un bloc de texte. L''insertion d''un cadre en-ligne dans un passage textuel revient un peu à y insérer un objet via l''élément OBJECT&#160;: ces éléments permettent tous deux l''insertion d''un document <i><b>HTML</b></i> au sein d''un autre, ils peuvent tous deux être alignés sur le texte environnant, etc.</p>
<p>Les informations qui doivent être insérées en-ligne sont désignées par l''attribut src de cet élément. Par contre, le <em>contenu</em> de l''élément <b><i>IFRAME</i></b> ne devraient être affiché que par les agents utilisateurs qui ne reconnaissent pas les cadres ou qui sont configurés pour ne pas les afficher.</p>
<div>
<p>Pour les agents utilisateurs qui reconnaissent les cadres, l''exemple suivant placera un cadre en-ligne, entouré par une bordure, au milieu du texte.</p>
<pre>
  &lt;IFRAME src="foo.html" width="400" height="500"
             scrolling="auto" frameborder="1"&gt;
  [Votre agent utilisateur ne reconnaît pas les cadres ou n''est pas
  configuré pour les afficher pour l''instant. Cependant, vous pouvez visiter le
  &lt;A href="foo.html"&gt;document concerné.&lt;/A&gt;]
  &lt;/IFRAME&gt;
</pre>
</div>
<p>Les cadres en-ligne ne peuvent pas être redimensionnés (et donc n''acceptent pas l''attribut noresize).</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditImage','<div style="text-align: center;">
<h1>IMAGE</h1>
<p style="text-align: left;">L''élément <samp class="einst">IMG</samp> incorpore une image dans le document courant, à l''emplacement de la définition de l''élément. L''élément <samp class="einst">IMG</samp> n''a pas de contenu&#160;; il est généralement remplacé dans la ligne par l''image que désigne l''attribut <samp class="ainst-IMG">src</samp>, les images alignées à gauche ou à droite qui «&#160;flottent&#160;» hors de la ligne faisant exception.</p>
<div style="text-align: left;">L''attribut <samp class="ainst">alt</samp> spécifie le texte de remplacement qui sera restitué si l''image ne peut s''afficher (voir ci-dessous pour des précisions sur la manière de spécifier un texte de remplacement). Les agents utilisateurs doivent restituer le texte de remplacement quand ceux-ci ne gèrent pas les images, ne reconnaissent pas un certain type d''image ou ne sont pas configurés par afficher les images.</div>
<div style="text-align: left;">&#160;</div>
<div class="example" style="text-align: left;">L''attribut <samp class="ainst">alt</samp> fournit une description brève de l''image.</div>
<div style="text-align: left;">&#160;</div>
&#160;</div>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditLinks','<div style="text-align: center;">
<h1>HYPERLIENS</h1>
<div style="text-align: left;">
<h2>Param&egrave;tres :</h2>
<ul>
    <li>Type de liste :&nbsp; Choisir soit Liste ou Menu d&eacute;roulant selon l''option.</li>
    <li>Type de liste :&nbsp; Si le choix est une liste vous pouvez choisir si elle sera vertical ou horizontal.</li>
    <li>Information hyperlien : Pour afficher ou non la description selon l''option.</li>
    <li>Classe CSS :&nbsp; Si le choix est une liste vous pouvez d&eacute;signer une classe CSS pour formater dans un style CSS l''hyperlien.</li>
</ul>
<h2>Options :</h2>
<ul>
    <li>Titre : Cette information sera le texte de l''hyperlien.</li>
    <li>Hyperlien : Saisir ou choisir l''URL selon le choix.</li>
    <li>Description : Sera afficher en info bulle si vous avez choisi l''option liste.</li>
    <li>Ordre de vue :&nbsp; Pour modifier l''ordre dans lequel les hyperliens seront affich&eacute;s &agrave; l''&eacute;cran.</li>
    <li>Afficher dans une nouvelle fen&ecirc;tre : Choisir l''option pour qu''une nouvelle fen&ecirc;tre s''ouvre.</li>
    <li>Voir statistiques : Cocher l''option pour visionner les statistiques d''utilisation de l''hyperlien.</li>
</ul>
</div>
</div>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditMapGoogle','<div style="text-align: center;">
<h1>GOOGLE MAP</h1>
<br />
<div style="text-align: left;">Pour exploiter les donn&eacute;es Google Maps il vous faudra tout d''abord dispose d''une clef d''API. Celle-ci est gratuite et disponible instantan&eacute;ment pour tout possesseur d''un compte Google Account : il suffit alors de se rendre sur le site de l''API, cliquer sur &quot;Sign up for a Google Maps API key&quot;.<br />
<br />
Lors de votre inscription, vous devez retenir quelques d&eacute;tails importants : votre clef est limit&eacute;e &agrave; 50 000 requ&ecirc;tes de codes g&eacute;ographiques par tranche de 24h, et pr&eacute;f&eacute;rablement moins de 500&nbsp;000 pages vues. Votre carte doit &ecirc;tre accessible &agrave; tous, inchang&eacute;e, et ne peut se trouver que dans le dossier (ou l''un de ses sous-dossiers) indiqu&eacute; lors de votre inscription - sans quoi, il vous faudra une autre clef. <br />
<br />
<br />
<br />
<br />
<br />
</div>
</div>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditRole','<h1 style="text-align: center; ">Paramètre de sécurité</h1>
<ul>
    <li><b>Nom : </b>Entrer un nom unique pour identifier le rôle de sécurité.</li>
    <li><b>Description :</b> Entrer l''information pour décrire le type d''accès du rôle de sécurité et son utilisation.</li>
    <li><b>Frais de services :</b> (optionel) Entrer un montant.</li>
    <li><b>Période de facturation :</b> (optionel) Entrer un chiffre.</li>
    <li><b>Fréquence de facturation :</b> (optionel) Choisir dans la liste.</li>
    <li><b>Frais d''essais :</b> (optionel) Entrer un montant.</li>
    <li><b>Période de facturation :</b> (optionel) Entrer un chiffre.</li>
    <li><b>Fréquence de facturation :</b> (optionel) Choisir dans la liste.</li>
    <li><b>Autorisation à télécharger :</b>&#160;Cocher pour autoriser.</li>
    <li><b>Publique? :</b>&#160;Cocher pour permettre à un utilisateur de souscrire au service.</li>
    <li><b>Auto assignation? :</b>&#160;Cocher pour attribuer ce rôle de sécurité à tous les utilisateurs.</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditRSS','<div style="text-align: center;">
<h1>Syndication RSS</h1>
</div>
<ul>
    <li>Fichier RSS :&#160; Vous pouvez désigner un ficher interne ou externe.</li>
    <li>Ficher XLS : Pour mettre en forme le contenu du flux RSS.</li>
</ul>
<p>&#160;</p>
<h2>Explication :</h2>
<p><br />
Un <strong>flux RSS</strong> ou <strong>fil RSS</strong> ("RSS feed" en anglais), sigle de <strong>Really Simple Syndication</strong> (<em>souscription vraiment simple</em>), ou de <strong>Rich Site Summary</strong> (Sommaire développé de site) est un format de syndication de contenu Web, codé sous forme XML. Ce système permet de diffuser en temps réel les nouvelles des sites d''information ou des blogs, ce qui permet de rapidement consulter ces dernières sans visiter le site.</p>
<div style="text-align: center;">
<h1>Document RSS</h1>
</div>
<p>Le contenu d''un document RSS se situe toujours entre les balises <em>&lt;rss&gt;</em>. Elles possèdent obligatoirement un attribut version qui spécifie la version à laquelle le document RSS est conforme.</p>
<p>Au niveau suivant de cette balise se trouve une unique balise <em>&lt;channel&gt;</em> qui contiendra les métadonnées du flux RSS, obligatoires ou non, ainsi que la liste des contenus.</p>
<p><em><strong>1. Métadonnées</strong></em><br />
En ce qui concerne les métadonnées, trois éléments sont obligatoires :</p>
<ul>
    <li><em>&lt;title&gt;</em> : Définit le titre du flux</li>
    <li><em>&lt;link&gt;</em> : Définit l''URL du site correspondant au flux</li>
    <li><em>&lt;description&gt;</em> : Décrit succinctement le flux</li>
</ul>
<p>D''autre éléments optionnels existent comme :</p>
<ul>
    <li><em>&lt;pubDate&gt;</em> : Définit la date de publication du flux</li>
    <li><em>&lt;image&gt;</em> : Permet d''insérer une image dans le flux</li>
    <li><em>&lt;language&gt;</em> : Définit la langue du flux</li>
</ul>
<p><em><strong>2. Contenu : Description de chaque article</strong></em><br />
Pour chaque article, une balise <em>&lt;item&gt;</em> est ajoutée dans notre document. Dans cette balise se trouvent les données correspondantes à l''actualité sous forme de balise.<br />
Les balises les plus courantes sont:</p>
<ul>
    <li><em>&lt;title&gt;</em> : Définit le titre de l''actualité.</li>
    <li><em>&lt;link&gt;</em> : Définit l''URL du flux correspondant à l''actualité.</li>
    <li><em>&lt;pubDate&gt;</em> : Définit la date de l''actualité.</li>
    <li><em>&lt;description&gt;</em> : Définit une description succincte de l''actualité.</li>
</ul>
<p>D''autres balises existent comme:</p>
<ul>
    <li><em>&lt;author&gt;</em> : Définit l''adresse électronique (mail) de l''auteur.</li>
    <li><em>&lt;category&gt;</em> : Associe l''item à une catégorie.</li>
    <li><em>&lt;comments&gt;</em> : Définit l''URL d''une page de commentaire en rapport avec l''item.</li>
</ul>
<p>&#160;</p>
<div style="text-align: center;">
<h1>DOCUMENT XSL</h1>
<div style="text-align: left;">
<p><strong>XSL</strong> (<em>eXtensible Stylesheet Language</em>) est le langage de description de feuilles de style du W3C associé à XML.</p>
<p>Une feuille de style XSL est un fichier qui décrit comment doivent être présentés (c''est-à-dire affichés, imprimés, épelés) les documents XML basés sur une même DTD ou un même schéma.</p>
<p>La spécification est divisée en trois parties&#160;:</p>
<ul>
    <li>XSLT, le langage de transformation</li>
    <li>XPath, le langage de navigation dans un document XML</li>
    <li>XSL-FO, le vocabulaire XML de mise en forme</li>
</ul>
</div>
</div>
<br />', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditSearch','<h1 style="text-align: center;">Recherche</h1>
<p>Vous avez besoin d''un minimum d''une table pour effectuer une recherche dans la base de données, pour chaque tableau sélectionné, vous devez choisir les colonnes pour la recherche ainsi que l''affichage. Vous pouvez personnaliser la manière dont le résultat sera affiché.</p>
<h2>Sélectionner les tables pour la recherche</h2>
<ol>
    <li>Vous avez besoin d''un minimum d''une table à la recherche dans la base de données, pour chaque tableau sélectionné, vous devez indiquate les colonnes que vous aurez de recherche et d''affichage.</li>
    <li>Choisir une table, et cliquez sur Ajouter.</li>
    <li>Choisir une champ Titre, Description, Mise à jour et Par de la table.</li>
</ol>
<h2>Paramètres de recherche</h2>
<ol>
    <li>Nombre max. de résultats, limite le nombre de résultat, au numéro indiqué.</li>
    <li>Largeur max. du titre, limite la taille du titre au nombre de caractère indiqué.</li>
    <li>Largeur max. description, limite la taille au nombre de caractère indiqué.</li>
    <li>Voir la description, pour afficher la description sur le résultat de la recherche.</li>
    <li>Voir l''audit, pour afficher les informations sur qui a fait la dernière mise à jour.</li>
    <li>Voir le chemin contextuel, pour afficher des liens sur l''emplacement de la recherche dans le site.</li>
</ol>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditUserDefinedTable','<h1 style="text-align: center;">Tableau à définir</h1>
<p>Vous devez définir des champs avant de pouvoir saisir de l''information dans le tableau,
Utiliser l''option</p><p> <span class="CommandButton"> Administrer les tables usager</span></p><p>&#160;</p><p>&#160;</p><p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditVendors','<div style="text-align: center;">
<h1>Inscription fournisseur</h1>
<div style="text-align: left;">Il est important de saisir toutes les informations sur compagnie.&nbsp; Les usagers pourront faire une recherche, donner&nbsp; des r&eacute;troactions et chercher une direction vers votre commerce.<br />
<p>&#160;</p><p>&#160;</p><p>&#160;</p><p>&#160;</p>
</div>
</div>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_EditXML','<h1 style="text-align: center;">XML/XSL</h1>
<p><br />
Ce module rend le résultat d''un XML/XSL transforment. Les dossiers de XML et de XSL sont identifiés par leurs chemins d''UNC dans les propriétés de xmlsrc et de xslsrc du module. Le module de Xml/Xsl inclut une page d''édition, l''information est stockés dans la base de données de SQL.<br />
<br />
<strong>XML</strong> (<em>Extensible Markup Language</em> <span style="cursor: help;"><span style="font-family: monospace; font-weight: bold; font-size: small;" title="Langue&#160;: anglais">(en)</span></span><sup id="_ref-0" class="reference"><a href="http://fr.wikipedia.org/wiki/XML#_note-0" title=""></a></sup>, «&#160;langage de balisage extensible&#160;») est un langage informatique de balisage <em>générique</em>. Le W3C recommande XML pour exprimer des langages de balisages <em>spécifiques</em> (exemples&#160;: XHTML, SVG, XSLT). Son objectif initial est de faciliter l''échange automatisé de contenus entre systèmes d''informations hétérogènes, notamment, sur Internet. XML est un sous-ensemble de SGML dont il retient plusieurs principes dont&#160;: la structure d''un document XML est définissable et validable par un schéma, un document XML est entièrement transformable dans un autre document XML. Cette syntaxe est reconnaissable par son usage des <em>chevrons</em> (&lt; &gt;), elle s''applique à de plus en plus de contenus.</p>
<p><span style="font-weight: bold;">XSL</span> Cette spécification définit la syntaxe et la sémantique    de XSLT, qui est un langage permettant de transformer des documents XML en d''autres    documents XML.</p>
<p>XSLT est conçu pour être utilisé comme une partie de XSL, le langage des feuilles de style de XML. En plus de XSLT, XSL inclus un vocabulaire XML pour la spécification de formatage. XSL spécifie les règles de présentation d''un document XML en utilisant XSLT pour décrire comment le document peut être transformé en un autre document qui utilise le vocabulaire de formatage.</p>
<p>XSLT est aussi conçu pour être utilisé indépendamment de XSL. Cependant, XSLT n''est pas censé être utilisé comme un langage de transformation XML à vocation générale. Il a surtout été conçu pour les types de transformations nécessaires lorsque XSLT est utilisé comme une partie de XSL.</p>
<p>&#160;</p>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_FileManager','<h1 style="text-align: center; ">Gestion des fichiers</h1>
<p>Les tâches suivantes peuvent être effectuées : <br />
&#160;</p>
<ul>
    <li>Ajouter un nouveau ficher.</li>
    <li>Ajouter un nouveau répetoire.</li>
    <li>Télécharger un fichier.</li>
    <li>Effacer un répertoire.</li>
    <li>Effacer un fichier.</li>
    <li>Renommer un répertoire.</li>
    <li>Renommer un ficher.</li>
</ul>
<p>Les fichiers et répertoires peuvent être trier&#160;par ordre &#160;croissant ou décroissant, par nom, par type et taille.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Forum','<div style="text-align: center;"><span class="ItemTitle">UTILISATION DU FORUM {portalname}</span></div>
<ul type="disc" compact="compact">
    <li><strong>Ajouter un message: </strong>Pour ajouter un fil de discussion vous pouvez utiliser l''hyperlien en haut à gauche de l''écran <input type="submit" value="Nouveau" name="NewTopic1" title="Ajouter un nouveau fil de discussion" id="NewTopic1" class="button" /></li>
    <li><strong>Préférences: </strong>Pour modifier vos paramètres personnel, tel que votre pseudomyne, désigner un avatar pour vous identifier, choisir un éditeur de texte, changer le thème du forum, déterminer une signature automatique ainsi que la zone horaire pour vos message.<span class="CommandButton">Préférences</span></li>
    <li><strong>Messagerie: </strong>La messagerie vous permet de faire parvenir des messages aux membres. Un avis courriel sera expédié au destinataire du message pour l''informer qu''un message est disponible sur le site web. <span class="CommandButton">Messagerie</span></li>
    <li><strong>Modifier ou effacer: </strong>Vous pouvez en tout temps modifier ou effacer un de vos messages.</li>
    <li><strong>Ajout d''image: </strong>Vous pouvez ajouter des images à vos messages.</li>
</ul>
<p>&#160;</p>
<p>Le but de ce forum est de permettre aux membres d''échanger des informations entre eux.</p>
<p>Le rôle des modérateurs est de faire respecter les règles d''utilisation du forum.&#160; Un modérateur peut, si nécessaire, supprimer une discussion ou un message.</p>
<p>Les règles d''utilisation du forum&#160;sont:</p>
<p>&#160;</p>
<ul>
    <li>Exprimez-vous dans ce forum comme vous le feriez dans une assemblée.</li>
    <li>Limitez vos discussions sur des sujets d''intérêts concernant {portalname}.</li>
    <li>N''abusez pas de votre pouvoir. Ce n''est pas parce que l''on peut faire quelque chose que l''on a le droit de le faire et ce n''est pas parce que l''on a le droit de faire quelque chose qu''il faut le faire.</li>
    <li>Rappelez vous qu''Internet n''est pas une zone de non droit et qu''on y est rarement complètement anonyme. Sur Internet aussi vous êtes censés respecter les lois de votre pays (droits d''auteur, informations illicites, protection de l''intégrité humaine, ...).</li>
    <li>Les participants sont personnellement responsables de leurs écrits; ils s''interdisent tout propos diffamatoire, injurieux, raciste ou susceptible de tomber sous le coup de la loi. Leur responsabilité pourra être engagée en cas de manquement à cette règle.</li>
    <li>Toute personne désireuse de faire supprimer une discussion lui portant un préjudice personnel doit adresser sa plainte par&#160;courriel en expliquant le motif de sa demande.</li>
</ul>
<p>Voilà les quelques règles pas trop contraignantes mais qui permettront à notre forum de garder sa crédibilité.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ForumAdmin','<h1 style="text-align: center;">Administration forum</h1>
<h2><span style="font-size: 12px; font-weight: normal;" class="Apple-style-span">Le forum est séparé en section et chaque section peut avoir plusieurs forum.</span></h2>
<h2>Section</h2>
<p>Pour ajouter une nouvelle section, inscriver l''information dans le champ et cliquer sur&#160;&#160;<img src="/images/1x1.gif" style="border-width: 0px; height: 16px; width: 16px; background-image: url(http://dotnetzoom.com/images/TTT/forum.gif); background-repeat: no-repeat; background-position: 0px -16px;" alt="Ajouter" title="Ajouter une nouvelle section" /></p>
<p>Pour effacer une Section elle doit être fide de forum et vous devez cliquer sur <img title="Effacer" src="/images/1x1.gif" alt="Effacer" style="border-width: 0px; background: transparent url(/images/TTT/forum.gif) no-repeat scroll 0px -32px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; height: 16px; width: 16px;" type="image" /></p>
<p>L''ordre dans lesquelles les Sections s''affichent peut être modifié en utilisant&#160;<img src="http://dotnetzoom.com/images/up.gif" alt="" />&#160;ou&#160;<img src="http://dotnetzoom.com/images/dn.gif" alt="" /></p>
<p>Pour Modifier les informations spécifiques à une section utiliser &#160;<img src="http://dotnetzoom.com/images/plus2.gif" title="Ouvrir la section" alt="Ouvrir une section" /></p>
<p>&#160;</p>
<h2>Forum&#160;</h2>
<p>&#160;Pour ajouter un nouveau forum dans une section.</p>
<ol>
    <li>Ouvrir le panneau de la section où vous voulez ajouter un nouveau forum &#160;<img src="http://dotnetzoom.com/images/plus2.gif" title="Ouvrir la section" alt="Ouvrir une section" /></li>
    <li>Inscrire l''information dans le champ et cliquer sur&#160;&#160;&#160;<img src="/images/1x1.gif" style="border-width: 0px; height: 16px; width: 16px; background-image: url(http://dotnetzoom.com/images/TTT/forum.gif); background-repeat: no-repeat; background-position: 0px -16px;" alt="Ajouter" title="Ajouter une nouvelle section" />&#160;pour créer le nouveau forum</li>
    <li>Pour modifier les informations spécifiques au forum  cliquer sur <img title="Modifier" src="/images/1x1.gif" alt="Modifier" style="border-width: 0px; background: transparent url(/images/TTT/forum.gif) no-repeat scroll 0px -128px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; height: 16px; width: 16px;" type="image" />
    <ol>
        <li>Nom du Forum : Inscrire un nom descriptif pour le Forum</li>
        <li>Description : Inscrire une description détaillé du Froum</li>
        <li>Privé? : Ne sera disponible qu''aux membres des rôles autorisés par l''admistrateur</li>
        <li>Dirigé? : Les nouveaux messages devront être autorisé avant d''être affiché. Sauf quand le membre a été désigné de "<i><b>Confiance</b></i>".</li>
        <li>Intégration galerie : Vous pouvez désigner une galerie existante pour afficher des image dans le forum.</li>
    </ol>
    </li>
</ol>
<p>Pour effacer un forum vous devez cliquer sur <img title="Effacer" src="/images/1x1.gif" alt="Effacer" style="border-width: 0px; background: transparent url(/images/TTT/forum.gif) no-repeat scroll 0px -32px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; height: 16px; width: 16px;" type="image" />.</p>
<p>L''ordre dans lesquelles les forums s''affichent peut être modifié en utilisant&#160;<img src="/images/up.gif" alt="" />&#160;ou&#160;<img src="/images/dn.gif" alt="" /></p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ForumAnonymous','<p>&#160;</p>Vous devez ouvrir une session pour pouvoir écrire des messages dans le forum.<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ForumModerate','<h1 style="text-align: center;">Forum dirigé</h1>
<p>&#160;</p>
<ol>
    <li>Chaque section du forum dirigé affichera le nombre de message en attente d''approbation</li>
    <li>Choisir une section du forum</li>
    <li>Choisir un message pour approuver/effacer/refuser.
   <ol>
        <li>Cliquer sur le sujet du message pour le voir</li>
        <li>Choisir les messages à approuver et cliquer sur approuvé</li>
        <li>Choisir les messages à effacer et cliquer sur effacé</li>
    </ol>
    </li>
</ol>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ForumSearch','<p align="center"><span class="Head">Recherche pour le forum</span></p>
<p align="left">&nbsp;Vous pouvez&nbsp; rechercher des messages dans le forum avec une ou plusieurs des options suivantes :</p>
<ul>
    <li>Recherche dans le message </li>
    <li>Recherche dans le sujet du message </li>
    <li>Recherche des messages d''un auteur</li>
    <li>Recherche par date </li>
    <li>Recherche dans des sections ou des forums </li>
</ul>
<p>&nbsp;Si il n''y a aucune information dans un champ, cette option de recherche ne sera pas activ&eacute;e.</p>
<p>Si vous avez saisie le texte &quot;Avions&quot; dans le champ<font style="background-color: rgb(255, 102, 0);"> Messsage</font> et une date de d&eacute;but et de fin, les messages recherch&eacute;s seront&nbsp;ceux qui ont &eacute;t&eacute; r&eacute;dig&eacute;s entre la date de d&eacute;but et la date de fin seulement avec le texte &quot;Avions&quot; dans le corps du message.</p>
<p>Donc,&nbsp;si vous utilisez plusieurs champs de recherche en m&ecirc;me temps le nombre potentiel de message&nbsp;sera d''autant plus limit&eacute;.&nbsp;</p>
<p>&nbsp;</p>
<p>Exemple :&nbsp; Pour trouver tous les messages r&eacute;dig&eacute;s par &quot;Webmestre&quot; vous avez juste a saisir&nbsp;<font style="background-color: rgb(255, 255, 0);"> Webmestre</font> dans le champ<font style="background-color: rgb(255, 102, 0);"> auteur</font> et effacer tous les autre champs.</p>
<p><br />
</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ForumSettings','<h1 align="center">Paramétrisation du forum</h1>
<ul>
    <li>Paramètres pour les images :
    <ul>
        <li>Répertoire images : Le forum utilise des images pour certain des menus et options.&#160; Vous pouvez définir l''endroit ou sont situés ces images.</li>
        <li>Répertoires Avatars : L''endroit ou sont situés les images des usagers ou avatars des usagers.</li>
        <li>ID Module Avatars : Cliquer sur l''icone pour configurer la module des avatars.</li>
        <li>Permettre Avatars : Cocher cette case, pour permettre aux usagers d''utiliser les avatars.</li>
    </ul>
    </li>
    <li>&#160;     Paramètres avis courriel:
    <ul>
        <li>Avis courriel? : Cocher cette case, pour pemettre au Forum de faire parvenir un courriel au usager du forum quand un nouveau message est inscrit ou une réponse à un message est fait.</li>
        <li>Adresse courriel : Utilisé un des choix suivant
        <ul>
            <li>Adresse courriel : Cocher cette case si vous désirer faire paraître l''Adresse <b>de</b> courriel personnel de l''usager dans le champ de du courriel sortant.</li>
            <li>Adresse automatique : Adresse qui apparaitra dans le champ <b>de</b> du courriel.</li>
            <li>Domaine courriel : Domaine de courriel à utilisé dans le champ <b>de</b> du courriel.</li>
        </ul>
        </li>
        <li>Format courriel : Utiliser soit un format Texte ou Html.</li>
    </ul>
    </li>
    <li>Paramètre d''affichage :
    <ul>
        <li>Fils par page : Inscrire le nombre de fils de discussion que vous voulez afficher sur une page.</li>
        <li>Message par page : Inscrire le nombre de message que vous voulez afficher sur une page.</li>
        <li>Taille max Avatar : La taille maximal en kb d''un fichier image qu''un usager peut téléchager pour son avatar.</li>
        <li>Mise a jour stats : Le nombre d''heure entre les mise a jours des statistiques du forum.</li>
    </ul>
    </li>
    <li>Intégration User Online :
    <ul>
        <li>Utiliser User Online? : Cocher cette case pour permettre le forum d''afficher qui est en ligne ainsi que de permettre l''utilisation des message privé entre usager du forum.</li>
    </ul>
    </li>
    <li>Paramètres intégrés galerie :<br />
    <ul>
        <li>Hauteur image : Indiqué la hauteur max des fichiers images.</li>
        <li>Largeur image : Indiqué la largeur max des fichiers images.</li>
        <li>Type d''image : Indiqué le type de fichier qu''un usager pourra télécharger.</li>
    </ul>
    </li>
</ul>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ForumSubscribe','<h1 style="text-align: center;">Paramètres avis courriel</h1>
<p>Un courriel vous parviendra pour nouveau message dans les forums que vous identifiez.</p>
<ol>
    <li>Pour afficher la liste des forums d''une section cliquer sur&#160; <img alt="Ouvrir une section" title="Ouvrir la section" src="http://dotnetzoom.com/images/plus2.gif" /></li>
    <li>Pour ajouter un forum à la liste d''avis de courriel cliquer sur <img style="border-style: none; border-width: 0px;" alt="Ajouter" src="../../images/rt.gif" /></li>
    <li>Pour enlever un forum de la liste d''avis de courriel <img type="image" style="border-style: none; border-width: 0px;" alt="Enlever" src="../../images/lt.gif" title="Enlever" /></li>
</ol>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ForumUserAdmin','<h1 style="text-align: center;">Administration usagers</h1>
<p>Choisir un filtre pour afficher les usagers du forum, de a - z ou tout.</p>
<p>Cliquer sur le Code d''accès d''un usager pour avoir accès a son profil et effectuer des modifications si nécessaire.</p>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ForumUserProfile','<h1 style="text-align: center;">Profil usager</h1>
<ul>
    <li>Éditeur texte enrichi? : Cocher ici pour utilser FckEditor pour écrire vos messages dans le forum</li>
    <li>Utiliser son avatar : Pour vous permettre d''utiliser votre avatar.</li>
    <li>Habillage : Choisir un habillage dans la liste.</li>
    <li>Adresse de courriel: Cocher cette option pour permettre au autre usager du site de voir votre adresse de courriel.</li>
    <li>En ligne? : Cocher cette option pour permettre au autre usager du site de voir quand vous êtes en ligne.</li>
    <li>Signature : Information qui sera ajouté a la fin d''un message.</li>
    <li>Fuseau horaire? : Indiqué votre réseau horaire.</li>
    <li>Travail: Sera affiché seulement si complété.</li>
    <li>Champs d''intérêts : sera affiché seulement si complété.</li>
    <li>Pseudonyme : Ce mon sera utilisé pour vous identifié sur le forum aupres des autres usagers.</li>
    <li>URL, MSN, YAHOO, AIM et ICQ : Seront affichés seulement si complétés.</li>
</ul>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_GalleryAdmin','<h1>Paramètres gallerie</h1>
<p>Le module est un système de gestion des fichiers images, photos, médias.<br />
Dans chaque album vous pouvez avoir des sous albums sans avoir à créer un nouvel album sur un onglet.  Vous pouvez assigner un fichier gif ou jpg pour l''image de chaque répertoire si vous désirez.  Pour ce faire vous devez utiliser la petite baguette magique sous l''icône du répertoire.  Pour modifier l''icône par défaut, vous devrez mettre un nouvel icône dans le répertoire _res  avec le même non que le non du répertoire que vous voulez modifier l''apparence.  Vous pouvez aussi modifier l''icône pour les fichiers vidéo ou flash.<br />
<br />
Pour ajouter des fichiers média ou ajouter un sous-album vous devez utiliser l''option modifier album/images.<br />
<br />
Les fichiers seront réduits selon la tailles que vous avez déterminer dans les paramètres de l''album.</p>
<h2>Paramètres Admin  </h2>
<ul>
    <li>URL souche (images): 	  L''administrateur tu site peut modifier URL souche de l''album.</li>
    <li>Quota en (MB): L''administrateur peut modifier le Quota maximum pour l''album, 0 est illimité.</li>
    <li>Taille Max d''un fichier: 	(KB) La grosseur maximum autorisé pour télécharger un fichier.</li>
    <li>Modifier la taille de l''image: Si activé,	l''images sera sauvegarder avec le même ratio hauteur/largeur que limage originale, sinon l''image sera sauvegarder avec les dimenssions indiqué dans les champ LArgeur Max et Hauter Max.</li>
    <li>Qualité: La qualité de la compression d''un fichier JPEG, 80 = meilleure qualité, mais fichier plus gros, 50 = moins bonne qualité mais fichier moins gros.</li>
    <li>Hauteur Max et Largeur Max: Le nombre de pixel maximum qu''un fichier peut avoir, si le fichier original est plus gros il sera réduit au nombre de pixel indiqué.</li>
    <li>Garder source? 	  Si activé, le fchier original sera saugarder dans le répertoire _source de l''abum.</li>
    <li>Garder privé? 	  Si activé, l''album ne pourra être modifier que pas des usagers autorisé.</li>
    <li>Mettre en cache au départ: Si activé, permet de mettre en mémoire cache toute l''album lors d''un premier accès. Accelère les accès subséquent à l''album.</li>
</ul>
<h2>Paramètres usager de la galerie</h2>
<ul>
    <li>Titre de la galerie: Inscrire un titre pour l''album (sera affiché)</li>
    <li>Description: 	  Inscrire une description pour l''ablum (sera affiché)</li>
    <li>Option d''affichage du texte: Chosir l''information a afficher.</li>
    <li>Intégration au forum?  	  : Cliquer cette coche pour intégrer cet album photo a un forum specifique.&#160; Les usagers du forum pourront utiliser les images pour afficher sur la liste des messages du forum.</li>
    <li># miniature de large et &#160;  # miniature de haut: Sur la page titre d''un album, indiqué le nombre de miniature a afficher.</li>
    <li>Largeur Max miniature et  Hauteur Max miniature: Inscrire la taille maximum pour une miniature.&#160; Le système réduira l''image lors du téléchargement.</li>
    <li>Extensions fichiers et 	  Extensions films: 	  Indiquer les extensions permissent pour le téléchargement.</li>
    <li>Vitesse diaporama: 	Indiqué un chiffre en miliseconde pour l''attente entre photo lors d''un diaporama.</li>
    <li>POPup diaporama? : Activé pour permettre le visionnement des photos dans un écran POPup</li>
    <li>Afficher les info bulles? 	  Activé pour permettre l''affichage de l''information en info bulles.</li>
    <li>Permettre téléchargement? Activé pour permettre le téléchargement de fichier dans l''album.</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_HostSettings','<h1 style="text-align: center;">Paramètre d''hébergement</h1>
<ul>
    <li>Titre du serveur d''hébergement:</li>
    <li>URL du serveur d''hébergement:</li>
    <li>Courriel du serveur d''hébergement:</li>
    <li>Fuseau Horaire du serveur:</li>
    <li>Gestion du ViewState:</li>
    <li>Réduction de la page web:</li>
    <li>Méthode de paiement:</li>
    <li>Aller au site web du fournisseur de paiement</li>
    <li>Code d''accès fournisseur paiement:</li>
    <li>Mot de passe fournisseur paiement:</li>
    <li>Frais d''hébergement:</li>
    <li>Devise:</li>
    <li>Espace ( MB):</li>
    <li>Journal d''utilisation (jours):</li>
    <li>Période de démo (jours):</li>
    <li>Permettre la création de portail démo? 	  (/default.aspx?edit=control&amp;def=demo)</li>
    <li>Désactivé l''affichage de la version du logiciel dans la page titre?</li>
    <li>Permettre l''enregistrement des erreurs? 	  (faire parvenir les messages d''erreur du portail à l''adresse de courriel du serveur d''hébergement)</li>
    <li>Forcer SSL 	  SSL sera appliqué sur certaine page</li>
    <li>Clef de chiffrage du mot de passe:</li>
    <li>Serveur Proxy:</li>
    <li>Port Proxy:</li>
</ul>
<h2>Paramètre du serveur SMTP</h2>
<p>&#160;</p>
<ul>
    <li>Serveur SMTP:</li>
    <li>Code usager:</li>
    <li>Môt Passe:</li>
</ul>
<p>Tester la configuration du courriel</p>
<p><br />
Validation type de fichier pour téléchargement: </p>
<p><br />
Voir le log de mise à jours pour la version:.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language0','<h1 style="text-align: center;">Language</h1>
<ul>
    <li><strong>Générer un script SQL :&#160;</strong> Choisir la langue pour générer le script et cliquer sur l''option approprié.&#160; Les fichiers .sql seront sauvegarder dans le répertoire database.</li>
    <li><strong>Ajouter une nouvelle langue :</strong>&#160; Choisir la langue de : et À et cliquer sur&#160; <img alt="" style="border-style: none; border-width: 0px; height: 16px; width: 16px;" src="../../images/save.gif" title="Cliquer ici pour créer un nouvelle langue" />, la nouvelle langue sera créé avec les informations de la lnague de départ.&#160; Vous pourrez les modifier par la suite.</li>
    <li><strong>Effacer une langue :&#160;</strong> Choisir la langue à effacer et cliquer sur&#160; <img name="admin$DelLang" id="admin_DelLang" title="Cliquer ici pour effacer la langue -&gt; fr" src="../../images/delete.gif" style="border-width: 0px; height: 16px; width: 16px;" alt="" /></li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language1','<h1 style="text-align: center;">Rubrique d''aide</h1>
<p>Les rubriques d''aide sont utilisées en autre pour les fichiers d''aide ainsi que pour afficher différentes informations dans différents contexte dans le site web.&#160;&#160; Vous pouvez y inclure des codes HTML.&#160; Dans certain fichier les informations suivantes seront ajoutées dans le texte lorsque les items suivants sont indiqués :</p>
<p>&#160;</p>
<ul>
    <li> 	&#123;HostEmail} : Adresse de courriel du webmestre.</li>
    <li> 	&#123;PortalName} : Nom du portail.</li>
    <li> 	&#123;Year} : année.</li>
    <li> 	&#123;AdministratorEmail} : Adresse de courriel de l''administrateur du site actuel.</li>
    <li> 	&#123;Date} : Current date time, formated with the appropriate language code.</li>
    <li> 	&#123;httplogin} : l''URL pour entrer sur le site.</li>
    <li> 	&#123;httpregister} : l''URL pour s''inscrire.</li>
</ul>
<p>&#160;</p>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language10','Fonction de paramétrisation du site web.', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language2','<h1 style="text-align: center;">Rubriques générales</h1>
<p>Les rubriques générales sont des informations spécifiques à une langue qui sera utilisé dans un contexte particulier sur le site web.&#160; La majorité des rubriques sont spécifiques à un module.&#160; Une rubrique ne peut avoir qu''un maximum de 200 caractères.</p>
<p>Dans certaine rubrique afin de formater et d''inclure de l''information contextuelle des code entre {} doivent être inclus.&#160; Les informations seront modifiés en conséquence lors de l''affichage de la page en question. </p>
<p>Tel que : </p>
<p style="margin-left: 40px;">L''extension du fichier {fileext} n''est pas autorisé.<br />
Vous pouvez seulement utiliser les extensions suivantes :<br />
{allowedext}</p>
<p>Dans cet exemple dans le contexte du module de téléchargement {fileext} sera remplacé par l''extension du fichier que l''utilisateur essayait de télécharger et {allowdext} sera remplacé par les extensions permises.</p>
<p>&#160;</p>
<p>Donc quand vous modifier une rubrique et qu''il y a des infos entre {} assurez vous de les reproduire si vous voulez que les informations soient modifiés convenablement.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language3','<h1 style="text-align: center;">Codes Pays</h1>
<p>La table des codes de pay est utilisé pour afficher le nom du pay lors de l''inscription d''un usager.&#160; Le code ne peut être modifié mais son appelation oui.&#160; Aussi des codes de régions peuvent être associés a une code de pay. &#160;</p>
<p>&#160;</p>
<p>Choisir le pay à modifier et cliquer sur <img title="Enregistrer -&gt; pay" src="../../images/save.gif" style="border-width: 0px; height: 16px; width: 16px;"></mg> pour sauvegarder.&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language4','<h1 style="text-align: center;">Info modules admin</h1>
<p>Vous pouvez modifier les titres ainsi que la description de chacunne des options du menu de l''adminstrateur.&#160; Les information seront affichés sur le menu admin.</p>
<p>&#160;</p>
<p>&#160;</p>
<ul>
    <li>Nom du module :&#160; Titre affiché dans le menu</li>
    <li>Description du module : Information affiché dans le menu admin ainsi que dans les infos bulles.</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language5','<h1 style="text-align: center;">Codes argent</h1>
<p>Les codes argent est pour identifié la devise qui sera utilisé pour la facturation de différents service sur le site web.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language6','<h1 style="text-align: center;">Codes fréquences</h1>
<p>Les codes de fréquence sont utilisés pour déterminer la fréquences de facturation de différents services sur le site web.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language7','<h1 style="text-align: center;">Code rapports statistiques</h1>
<p>Vous pouvez modifier l''appellation de chacun des codes de rapports disponible dans le module statistiques su site web.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language8','<h1 style="text-align: center;">Codes Regions</h1>
<p>Chaque pay peut avoir des codes de région, état, département ou province associé au code de pay.&#160; Si des codes de région sont disponible ils seront affiché comme choix à un utilisateur pour sont inscription ou lors de la modification de ses informations personnel.&#160; Un code de région peut facilement être ajouté ou modifié pour un pay en particulier.</p>
<p>&#160;</p>
<ul>
    <li>Pour ajouter un code : Choisir un pay, ajouter le code et son appélation cliquer sur <img style="border-width: 0px; height: 16px; width: 16px;" src="../../images/save.gif" title="sauvegarder" alt="" /> pour sauvegarder l''information.</li>
    <li>Pour modifier un code : Cliquer sur <img style="border-width: 0px;" alt="modifier" src="../../images/edit.gif" /> du code que vous voulez modifier et cliquer sur  <img style="border-width: 0px; height: 16px; width: 16px;" src="../../images/save.gif" title="sauvegarder" alt="" /> pour sauvegarder vos modifications.</li>
    <li>Pour effacer un code : Cliquer sur <img style="border-width: 0px;" alt="modifier" src="../../images/edit.gif" /> du code que vous voulez effacer et cliquer sur <img title="Effacer -&gt; Région" src="../../images/delete.gif" style="border-width: 0px; height: 16px; width: 16px;" alt="" /> pour l''effacer.</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Language9','<h1 style="text-align: center;">Zones horaire</h1>
<p>&#160;</p>
<p>Un fuseau horaire est une zone de la surface terrestre où, à l''origine, l''heure adoptée doit être identique en tout lieu.  Ce système a été proposé par l''ingénieur et géographe montréalais Sir Sandford Fleming en 1876, avec le méridien de Greenwich comme origine des temps, la ligne de changement de date au méridien 180° (est et ouest), et en divisant le globe en 24 fuseaux horaires de même taille.  La zone couverte par un fuseau, limitée par deux méridiens distants de 15°, s''étend du pôle nord au pôle sud; elle est centrée sur un méridien dont la longitude est multiple de 15°. Le premier fuseau est donc centré sur le méridien de Greenwich. Au passage d''un fuseau à l''autre l''heure augmente ou diminue d''une heure.</p>
<p>Vous pouvez modifié l''appelation d''un fuseau horaire.&#160; Cettte information sera affichée dans les paramètres du serveur, du site et de l''usager.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ManageTabs','<h1 align="center">Paramètres page</h1>
<ul>
    <li>
    <div align="left">Nom : Le nom est obligatoire et déterminera le nom de la page .aspx pour y avoir accès par l''url.&#160; Si vous utilisez plus d''une langue pour le site, vous pouvez avoir un nom différent pour chaque langue.</div>
    </li>
    <li>
    <div align="left">Icône : Pour choisir un icône pour afficher à gauche du nom dans le menu.</div>
    </li>
    <li>
    <div align="left">Page parente : Pour déterminer la hiérachie de la page.&#160;</div>
    </li>
    <li>
    <div align="left">Fichier CSS :&#160; Si vide le fichier portal.css sera utilisé, sinon le fichier désigné sera utilisé à la place.</div>
    </li>
    <li>Mise en forme : Si vide le fichier portal.skin sera utilisé, sinon le fichier désigné sera utilisé à la place</li>
    <li>Gabarit de la page : Vous pouvez utiliser la mise en page utilisé par une autre page lors de la création d''une nouvelle page seulement.</li>
    <li>Gabarit XML : Pour utiliser un gabarit XML sauvegardé lors de la création d''une nouvelle page seulement.</li>
    <li>Visible :&#160; Pour que la page soit disponible par le menu, sinon la page et l''hyperlien ne sera pas disponible dans le menu.</li>
    <li>Hyperlien : Pour d''ésactivé l''hyperlien dans le menu.</li>
    <li>Volet gauche et droite : Chaque page à trois volet, donc pour déterminer en pixels la largeur minimal des volets gauche et droite.</li>
    <li>Modifier :&#160; Paramètres de sécurité pour désigner les comptes utilisateurs qui pourront modifier les paramètres de la page ainsi que l''ajout et la suppression de module sur la page.</li>
    <li>Voir : Paramètres de sécurité pour désigner les comptes utilisateur qui pourront consulter cette page.</li>
    <li>Créer gabarit : Créer un gabarit XML pour réutiliser lors de la création ultérieur d''une nouvelle page.</li>
</ul>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ManageTabsXML','<h1 style="text-align: center;">Gabarit XML Page</h1>
<p>Vous pouvez modifier le code xml de la page pour en faire un gabarit que vous pourrez utiliser par la suite lors de la creation d''une nouvelle page.&#160; Une fois les modifications apportés vous pouvez sauvegarder le gabarit.&#160; Le fichier xml utilsera le nom que vous lui donnerez.&#160; Le fichier sera sauvegarder dans le répertoire skin/templates de votre site.&#160; Si le fichier existe déjà il sera écrasé par le nouveau fichier. Tous les fichiers dans ce répertoire seront disponible pour populer une nouvelle page lors de sa création.</p>
<p>La structure du gabarit xml est le suivant:</p>
<p>&lt;?xml version="1.0" encoding="utf-8"?&gt;</p>
<br />
<p>&lt;portal&gt;</p>
<br />
<p>&#160;&lt;tabs language=''''&gt;</p>
<br />
<p>&#160; &lt;tab&gt;</p>
<br />
<p>&#160; &lt;name&gt;&lt;/name&gt;</p>
<br />
<p>&#160; &lt;visible&gt;&lt;/visible&gt;</p>
<br />
<p>&#160;&#160; &lt;panes&gt;</p>
<br />
<p>&#160;&#160; &lt;pane&gt;</p>
<br />
<p>&#160;&#160; &lt;name&gt;&lt;/name&gt;</p>
<br />
<p>&#160;&#160;&#160; &lt;modules&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160; &lt;module&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160; &lt;title&gt;&lt;/title&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160; &lt;definition&gt;&lt;/definition&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160; &lt;modulesettings&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160; &lt;modulesetting&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160;&#160; &lt;settingname&gt;&lt;/settingname&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160;&#160; &lt;settingvalue&gt;&lt;/settingvalue&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160;&#160; &lt;/modulesetting&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160;&#160;&#160; &lt;datas&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &lt;data&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &lt;data1&gt;&lt;/data1&gt;&lt;data2&gt;&lt;/data2&gt;&lt;data3&gt;&lt;/data3&gt;&lt;data4&gt;&lt;/data4&gt;&lt;data5&gt;&lt;/data5&gt;&lt;data6&gt;&lt;/data6&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &lt;/data&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160;&#160;&#160; &lt;/datas&gt;</p>
<br />
<p>&#160;&#160;&#160;&#160;&#160; &lt;/module&gt;</p>
<br />
<p>&#160;&#160;&#160; &lt;/modules&gt;</p>
<br />
<p>&#160;&#160; &lt;/pane&gt;</p>
<br />
<p>&#160; &lt;/panes&gt;</p>
<br />
<p>&#160;&lt;/tab&gt;</p>
<br />
<p>&#160;&lt;/tabs&gt;</p>
<br />
<p>&lt;/portal&gt;</p>
<br />', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_MAnageUserDefinedTable','<h1 style="text-align: center;">Gestion des tables usager</h1>
<ol>
    <li>Vous devez Ajouter des champs pour créer un nouvelle table</li>
    <li>Chaque champ peut contenir un type de donnée spécifique tel que :
    <ol>
        <li>Texte</li>
        <li>Nombre</li>
        <li>Nombre avec décimal</li>
        <li>Date (format ANSI)</li>
        <li>Vraie/Faux</li>
    </ol>
    </li>
    <li>L''ordre de tri peut être désigner sur un champ en ordre décroisant ou croisant.</li>
</ol>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ManageUsers','<h1 align="center">Modification</h1>
<ul>
    <li>
    <div align="left">Modification des informations personnel d''un compte usager.</div>
    </li>
    <li>
    <div align="left">Courriel :&nbsp; Assurez vous que l''adresse de courriel est valide.</div>
    </li>
    <li>
    <div align="left">M&ocirc;t de passe :&nbsp; Pour une question de s&eacute;curit&eacute; il est recommand&eacute; d''utilis&eacute; un m&ocirc;t de passe avec un minium de 8 caract&egrave;res.&nbsp; Une combination de lettre et de chiffre est recommand&eacute;.</div>
    </li>
    <li>
    <div align="left">Code d''acc&egrave;s :&nbsp; Il est possible de modifier le code d''acc&egrave;s, par contre le c&ocirc;de d''acc&egrave;s doit &ecirc;tre unique sur le site web.</div>
    </li>
</ul>
<p align="left">&nbsp;</p>
<p align="left">&nbsp;</p>
<p align="left">&nbsp;</p>
<p align="left">&nbsp;</p>
<p align="left">&nbsp;</p>
<p align="left">&nbsp;</p>
<p align="left">&nbsp;</p>
<p align="left">&nbsp;</p>
<p align="left">&nbsp;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ModerateAdmin','<h1 style="text-align: center;">Admin Forum dirigé</h1>
<p>&#160;</p>
<ol>
    <li>Ouvrir le panneau de la section où vous voulez faire les modifications au forum &#160;<img alt="Ouvrir une section" title="Ouvrir la section" src="http://dotnetzoom.com/images/plus2.gif" /></li>
    <li>Pour ajouter ou enlever des modérateur pour ce forum <img type="image" style="border-width: 0px; background: transparent url(../../images/TTT/forum.gif) no-repeat scroll 0px -176px; height: 16px; width: 16px;" alt="Choisir moderateur" src="/images/1x1.gif" title="Choisir moderateur" /></li>
    <li>Pour désigner un forum comme dirigé  cliquer sur <img type="image" style="border-width: 0px; background: transparent url(../../images/TTT/forum.gif) no-repeat scroll 0px -128px; height: 16px; width: 16px;" alt="Modifier" src="../../images/1x1.gif" title="Modifier" />
    <ol>
        <li>Pour choisir un ou des administreur utilisé les boutons de a - z ou (tout)</li>
        <li><img title="Select moderator" src="../../images/1x1.gif" alt="Select" style="border-width: 0px; background: transparent url(../../images/TTT/forum.gif) no-repeat scroll 0px -176px; height: 16px; width: 16px;" type="image" /> sera visible suite au nom d''un moderateur</li>
        <li>Pour ajouter un modérateur cliquer sur <img style="border-style: none; border-width: 0px;" alt="Ajouter" src="../../images/rt.gif" /></li>
        <li>Pour voir l''information sur le modérateur ainsi ajouté cliquer sur&#160; <img alt="Ouvrir une section" title="Ouvrir la section" src="../../images/plus2.gif" /></li>
        <li>Pour enlever un modérateur cliquer sur <img type="image" style="border-style: none; border-width: 0px;" alt="Enlever" src="../../images/lt.gif" title="Enlever" /></li>
    </ol>
    </li>
</ol>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ModuleDefinitions','<h1 style="text-align: center;">Gestion des modules</h1>
<ul>
    <li>Nom du module : Nom descriptif qui sera utilisé pour désigner le module dans le menu d''ajout de module d''une page.</li>
    <li>Description : Information qui sera utilisé pour donner de l''information sur l''utilisation du module dans le menu d''ajout de module d''une page.</li>
    <li>Source bureau : l''endroit ou le fichier de contrôle asp.net ascx est situé sur le site web. (code utilisé pour afficher le module)</li>
    <li>Modifier source : l''endroit ou le fichier de contrôle asp.net ascx est situé sur le site web. (code utilisé pour l''option modifier du module)</li>
    <li>Fichier aide : Non utilisé présentement.</li>
    <li>Icône : Icone qui sera affiché sur la page de modification du module. (Peut être vide)</li>
    <li>Bonus : Indique si un Bonus sera exigé pour l''utilisation du module par un site.
    <ul>
        <li>Ajout du bonus : Aller dans les paramètres du site section subscrition pour inscrire le montant requis pour l''utilisation du bonus.</li>
    </ul>
    </li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ModuleDefs','<h1 style="text-align: center;">Gérer les modules</h1>
<p>Vous trouvez dans ce menu, tous les modules disponible sur le serveur. </p>
<ul>
    <li>Le nom du module est ainsi que sa description est spécifique à la langue d''affichage.</li>
    <li>La colone Bonus indique si le module est gratuit ou non. </li>
</ul>
<p>Pour modifier les informations spécifique au module cliquer sur <img style="border-width: 0px;" alt="Modifier" src="../../images/edit.gif" title="Modifier" /></p>
<p>Pour ajouter un nouveau module cliquer sur <img style="border-width: 0px;" alt="*" src="../../images/add.gif" /></p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_ModuleSettings','<h1 align="center">Param&egrave;tres d''un Module</h1>
<ul>
    <li>
    <div align="left">Titre : Il est obligatoire d''avoir un titre, le titre du module appara&icirc;tra seulement si la case a cocher est activ&eacute;e.</div>
    </li>
    <li>
    <div align="left">Langue :&nbsp; Le module sera affich&eacute; seulement sur la page avec la langue choisie, par contre si l''option tout est s&eacute;lectionn&eacute;e, le module sera affich&eacute; pour toutes les langues.&nbsp; Utilisation de tout peut compliquer le positionnement du module sur la page.</div>
    </li>
    <li>
    <div align="left">D&eacute;placer :&nbsp; Il est possible de d&eacute;placer le module sur une autre page en utilisant cette option.</div>
    </li>
    <li>
    <div align="left">Personnalisation :&nbsp; Pour permettre l''option de + et - du module sur la page.</div>
    </li>
    <li>
    <div align="left">Mise en cache : Pour mettre en cache le module.&nbsp;&nbsp;Utilisation de cette option acc&eacute;l&egrave;re la cr&eacute;ation de la page puisque l''information utilis&eacute; est&nbsp;celle qui&nbsp;est en m&eacute;moire.&nbsp; Par contre,&nbsp;cette option ne devrait pas &ecirc;tre utilis&eacute; pour un module avec de l''information dynamique.</div>
    </li>
    <li>
    <div align="left">Voir : Pour d&eacute;signer les param&egrave;tres de s&eacute;curit&eacute; des comptes utilisateurs autoris&eacute;s &agrave; voir le module.</div>
    </li>
    <li>
    <div align="left">Modifier : Pour d&eacute;signer les param&egrave;tres de s&eacute;curit&eacute; des comptes utilisateur autoris&eacute;s &agrave; modifier les informations du module.</div>
    </li>
    <li>
    <div align="left">Habillage du module : Option pour cr&eacute;er et modifier l''habillage du module.</div>
    </li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_MyBuddiesModule','<h1 style="text-align: center;">Usagers préférés</h1>
<p>Information sur les usagers préférés en ligne ou non.</p>
<p>Pour désigner un usager préférés, utiliser le module de recherche d''usager et cliquer sur le nom de l''usager.</p>
<p>À partir de ce module vous pouvez visionner l''information sur l''usager préféré ainsi que d''aller dans le module de courriel pour faire parvenir un courrier à l''usager.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_NeedToLogin','<p>&#160;</p><p>&#160;</p>
<h1>Vous devez ouvrir une session pour utiliser cette option.</h1>
<p>&#160;</p><p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_PMSCompose','<div style="text-align: center;">
<h1>Écrire un nouveau message</h1>
</div>
<p>&#160;</p>
<p>Lorsque vous avez choisi le destinataire, entrez un <b>Sujet</b> puis commencez à rédiger votre message. Vous pouvez modifier la mise en forme, la police et la couleur de votre texte.</p>
<p>Lorsque vous avez terminé, cliquez sur <b>Envoyer</b> et une confirmation indiquant que votre message a été envoyé s''affichera en haut de la fenêtre.</p>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_PMSInbox','<div style="text-align: center;">
<h1>Boîte de réception</h1>
</div>
<p>Vous pouvez lire, valider les informations sur l''usager, répondre et gérer les différents message reçus</p>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_PMSOutbox','<div style="text-align: center;">
<h1>Boîte d''envoi</h1>
</div>
<p>Liste des messages que vous avez fait parvenir aux autres usager.&#160; Vous pouvez lire, valider les informations sur l''usager et effacer les messages envoyés.</p>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Portals','<h1 style="text-align: center;">Portails</h1>
<p>Une liste de tous les portails du serveur. </p>
<ul>
    <li>Modifier les paramètre d''un portail : Cliquer sur <img title="Modifier le portail" src="../../images/edit.gif" alt="modifier" style="border-width: 0px;" /> pour modifier les paramètres du portail tel que : site démo, subcription, paramètres SSL, nom de domaine.</li>
    <li>Voir Portail : Cliquer sur le nom du portail pour être rediriger sur ce portail.</li>
    <li>Ajouter un nouveau portail: Cliquer sur <img src="../../images/add.gif" alt="*" style="border-width: 0px;" /> pour ajouter un nouveau portail.</li>
</ul>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Privacy','<span class="Head">DÉCLARATION DE CONFIDENTIALITÉ</span>
<p>
La présente déclaration de confidentialité divulgue nos pratiques relatives à la protection de la vie privée. En utilisant le site web, vous consentez sans réserve à la collecte,  l''utilisation et  la divulgation de vos renseignements personnels et aux pratiques relatives à la protection de la vie privée énoncées dans la déclaration de confidentialité.  Si vous désirez plus d''information ou d''aide veuillez nous écrire.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Register','<h1>Inscription {PortalName}</h1>
<p>Tous les champs avec un <b>*&#160;</b>sont obligatoires.</p>
<p>Pour créer un mot de passe sécure et facile à mémoriser pour vous:</p>
<ol>
    <li>Ne pas utiliser des renseignements personnels. Vous ne devez jamais utiliser les renseignements personnels dans le cadre de votre mot de passe. Il est très facile de deviner votre nom, prénom, nom de l''animal, date de la naissance ou d''autres détails.</li>
    <li>Ne pas utiliser de vrais mots. Il ya des outils disponibles pour aider les attaquants à deviner votre mot de passe. Il ne faut pas longtemps pour essayer tous les mots dans le dictionnaire et ainsi trouver votre mot de passe, il est donc préférable si vous n''utilisez pas de mots réels comme mot de passe.</li>
    <li>Mélanger différents types de caractères. Vous pouvez faire un mot de passe beaucoup plus sûr en mélangeant différents types de caractères. Utilisez des majuscules avec des lettres minuscules, chiffres et même des caractères spéciaux tels que ''&amp;'' ou ''%''.</li>
    <li>Plutôt que d''essayer de se rappeler d''un mot de passe créé à l''aide de différents types de caractères, vous pouvez créer un mot de passe en utilisant par exemple la première lettre des môt dans une ligne d''une chanson ou un poème que vous aimez. Par exemple, plutôt que simplement comme un mot de passe ''an $ 1Hes'', vous pouvez prendre une phrase telle que «J''aime lire la Presse tous les samedi matin" et de le convertir en mot de passe comme'' JallP2lsm. Vous pouvez utiliser une variété de types de caractères et de créer un mot de passe qui est difficile à deviner, mais beaucoup plus facile à mémoriser pour vous.</li>
</ol>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_RegisterEdit','<h1>Modification information personnelle {PortalName}</h1>
<p>Tous les champs avec un <b>*&#160;</b>sont obligatoires.</p>
<p>Pour créer un mot de passe sécure et facile à mémoriser pour vous:</p>
<ol>
    <li>Ne pas utiliser des renseignements personnels. Vous ne devez jamais utiliser les renseignements personnels dans le cadre de votre mot de passe. Il est très facile de deviner votre nom, prénom, nom de l''animal, date de la naissance ou d''autres détails.</li>
    <li>Ne pas utiliser de vrais mots. Il ya des outils disponibles pour aider les attaquants à deviner votre mot de passe. Il ne faut pas longtemps pour essayer tous les mots dans le dictionnaire et ainsi trouver votre mot de passe, il est donc préférable si vous n''utilisez pas de mots réels comme mot de passe.</li>
    <li>Mélanger différents types de caractères. Vous pouvez faire un mot de passe beaucoup plus sûr en mélangeant différents types de caractères. Utilisez des majuscules avec des lettres minuscules, chiffres et même des caractères spéciaux tels que ''&amp;'' ou ''%''.</li>
    <li>Plutôt que d''essayer de se rappeler d''un mot de passe créé à l''aide de différents types de caractères, vous pouvez créer un mot de passe en utilisant par exemple la première lettre des môt dans une ligne d''une chanson ou un poème que vous aimez. Par exemple, plutôt que simplement comme un mot de passe ''an $ 1Hes'', vous pouvez prendre une phrase telle que «J''aime lire la Presse tous les samedi matin" et de le convertir en mot de passe comme'' JallP2lsm. Vous pouvez utiliser une variété de types de caractères et de créer un mot de passe qui est difficile à deviner, mais beaucoup plus facile à mémoriser pour vous.</li>
</ol>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_RegisterEditServices','<h1>Modification information personnelle {PortalName}</h1>
<p>Tous les champs avec un <b>*&#160;</b>sont obligatoires.</p>
<p>Pour créer un mot de passe sécure et facile à mémoriser pour vous:</p>
<ol>
    <li>Ne pas utiliser des renseignements personnels. Vous ne devez jamais utiliser les renseignements personnels dans le cadre de votre mot de passe. Il est très facile de deviner votre nom, prénom, nom de l''animal, date de la naissance ou d''autres détails.</li>
    <li>Ne pas utiliser de vrais mots. Il ya des outils disponibles pour aider les attaquants à deviner votre mot de passe. Il ne faut pas longtemps pour essayer tous les mots dans le dictionnaire et ainsi trouver votre mot de passe, il est donc préférable si vous n''utilisez pas de mots réels comme mot de passe.</li>
    <li>Mélanger différents types de caractères. Vous pouvez faire un mot de passe beaucoup plus sûr en mélangeant différents types de caractères. Utilisez des majuscules avec des lettres minuscules, chiffres et même des caractères spéciaux tels que ''&amp;'' ou ''%''.</li>
    <li>Plutôt que d''essayer de se rappeler d''un mot de passe créé à l''aide de différents types de caractères, vous pouvez créer un mot de passe en utilisant par exemple la première lettre des môt dans une ligne d''une chanson ou un poème que vous aimez. Par exemple, plutôt que simplement comme un mot de passe ''an $ 1Hes'', vous pouvez prendre une phrase telle que «J''aime lire la Presse tous les samedi matin" et de le convertir en mot de passe comme'' JallP2lsm. Vous pouvez utiliser une variété de types de caractères et de créer un mot de passe qui est difficile à deviner, mais beaucoup plus facile à mémoriser pour vous.</li>
</ol>
<p>&#160;</p>
<h1>Services {PortalName}</h1>
<p>Liste des services disponible ainsi que ceux que vous vous êtes inscrit.</p>
', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Roles','<div style="text-align: center;">
<h1>Paramètres de sécurité</h1>
<p style="text-align: left; ">DotNetZoom utilise le&#160;<b>contrôle d''accès à base de rôles. &#160;</b>Donc,<b>&#160;</b>&#160;l''accès aux ressources du site web&#160;est basée sur le rôle auquel l''utilisateur&#160;est attaché.&#160;</p>
<p style="text-align: left; ">Il y a deux types d''accès de base par rôles, soit "voir" ou "modifier".</p>
<p style="text-align: left; ">Chacune des pages ou des modules sont accessibles ou modifiables selon les rôles désignés pour cette ressource.</p>
<p style="text-align: left; ">Chacun des rôles peut être authorisé à télécharger des fichiers.</p>
<p style="text-align: left; ">Le rôle administrateur a un accès illimité à toutes les ressources du site web.</p>
<p style="text-align: left; ">&#160;</p>
</div>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Search','
<h1 align="center">Module recherche</h1>
<ul>
<li>
Vous avez juste a saisir du texte dans la boite de dialogue et cliquer sur go pour démarrer la recherche.
</li>
<li>
Les résultats seront afficher avec un hyperlien pour vous permettre d''aller à la page appropriée du site web.
</li>
<li>
Le texte de recherche sera en surbrillance une fois la recherche compléter.
</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SecurityRole','<h1 style="text-align: center; ">Gérer les comptes</h1>
<ul>
    <li><b>Code d''usager : </b>Sélectionnez l''utilisateur dans la liste.</li>
    <li><b>Rôle de sécurité :</b> Sélectionnez le rôle de sécurité dans la liste.</li>
    <li><b>Date d''expiration : </b>(facultatif) Le rôle sera valable jusqu''à la date indiquée. (Utiliser le calendrier si nécessaire</li>
    <li><b>Ajouter :</b> Une fois que toutes les options sont choisies, ajouter le rôle de sécurité désigné à l''utilisateur.</li>
</ul>
<p>&#160;</p>
<hr />
<p>&#160;</p>
<p>Chacune des pages ou des modules sont accessibles ou modifiables selon les rôles désignés pour cette ressource.</p>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Services','<h1>Services {PortalName}</h1>
<p>Liste des services disponible.  Pour adhérer à un service vous devez vous inscrire.</p>
', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Signin','<h1>Ouvrir une session</h1>
<p>Vous devez entrer un Code d''accès et mot de passe.</p>
<p>Si vous cochez l''option Garder en mémoire, la prochaine fois que vous visitez le site Web, vous serez connecté automatiquement (si vous n''avez pas effacé vos cookies)</p>
<p>Si vous ne vous souvenez plus de votre mot de passe, vous pouvez le récupérer par courriel, utilisez l''option appropriée.</p>
<p>Pour des raisons de sécurité, si vous n''avez pas entrer le bon compte ou vous avez utilisé un mauvais mot de passe, un délai sera nécessaire avant toute nouvelle tentative d''ouvrir une session</p>
<hr>
<p>Si vous éprouvez de la difficulté à ouvrir un session vous pouvez toujours contacter l''administrateur de {AdministratorEmail}</p><p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Signup','<h1 style="text-align: center;">Ajouter un nouveau portail</h1>
<p>Vous devez entrer toutes les informations afin de créer un nouveau portail.       </p>
<ul>
    <li>Nom du portail: Utilisez uniquement des caractères alphanumériques. Vous ne pouvez pas utiliser des espaces. Si le nom est déjà utilisé sur le serveur, vous devrez en utiliser un autre.</li>
    <li>Gabarit: Modèles disponibles dans une liste déroulante. Une image sera affiché pour un gabarit choisi.</li>
    <li>Prénom: Le prénom de l''administrateur du site. (obligatoire)</li>
    <li>Nom: Le nom de l''administrateur du site. (obligatoire)</li>
    <li>Code d''accès: Peut être une combinaison de n''importe quel caractère alphanumérique. Devrait être d''au moins 8 caractères de long pour des raisons de sécurité. Aussi il est fortement conseillé de combiner majuscules et minuscules et faire un mélange de chiffres et de lettres. (obligatoire)</li>
    <li>Môt de passe: Peut être une combinaison de n''importe quel caractère alphanumérique. Devrait être d''au moins 8 caractères de long pour des raisons de sécurité. Aussi il est fortement conseillé de combiner majuscules et minuscules et faire un mélange de chiffres et de lettres. (obligatoire)</li>
    <li>Courriel: Un courriel est requis pour activer le nouveau site.</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SiteLog','<h1 style="text-align: center; ">Statistiques</h1>
<p>&#160;</p>
<p>Le module sert à voir les statistiques d''utilisations de votre portail.  Plus particulièrement, qui utilise le site, quand et comment.</p>
<p>&#160;</p>
<p>Choisir un rapport avec une date de début et de fin.</p>
<ul>
    <li>Inscription par date</li>
    <li>Inscription par pays</li>
    <li>Nombre de clics</li>
    <li>Popularité par page</li>
    <li>Références au site</li>
    <li>Statistiques détaillées</li>
    <li>Statistiques hebdomadaire</li>
    <li>Statistiques horaire</li>
    <li>Statistiques mensuelle</li>
    <li>Statistiques quotidienne
    <option value="3">Utilisation par usager</option>
    </li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SiteSettings1','<h1 style="text-align: center;">Paramètres du site Habillage</h1>
<ul>
    <li>Image d''entête : Vous pouvez choisir une image à afficher dans l''entête de la page.</li>
    <li>Image d''arrière plan : Vous pouvez choisir une image qui sera utilisé pour définir l''arrière plan de la page.</li>
    <li>Info utilisateur : Vous pouvez activé les fonctions suivantes, la recherche d''usager, qui est en ligne, la n otification de message, activation du module qui est en ligne et permettre l''avis courriel d''un nouveau message.
    <table id="admin_chkUserInfo" cellspacing="0" cellpadding="0" border="0" style="font-family: Verdana, Arial; font-size: 8pt; width: 400 px; border-collapse: collapse; ">
        <tbody>
            <tr>
                <td><img height="14" width="17" alt="*" style="background-image: url(../../images/uostrip.gif); background-repeat: no-repeat; background-position: 0px -91px; " src="../../images/1x1.gif" />&#160;&#160;Recherche usagers</td>
            </tr>
            <tr>
                <td><img height="14" width="17" alt="*" style="background-image: url(../../images/uostrip.gif); background-repeat: no-repeat; background-position: 0px -105px; " src="../../images/1x1.gif" />&#160;&#160;Indique qui est en ligne</td>
            </tr>
            <tr>
                <td><img height="12" width="18" alt="*" style="background-image: url(../../images/uostrip.gif); background-repeat: no-repeat; background-color: initial; background-position: 0px -147px; " src="../../images/1x1.gif" />&#160;&#160;Hyperlien pour le courriel</td>
            </tr>
            <tr>
                <td><img height="14" width="17" alt="*" style="background-image: url(../../images/uostrip.gif); background-repeat: no-repeat; background-position: 0px -91px; " src="../../images/1x1.gif" />&#160;&#160;Active l''option de suivi de qui est en ligne &#160;</td>
            </tr>
            <tr>
                <td><img height="16" width="17" border="0" alt="*" style="background-image: url(../../images/uostrip.gif); background-repeat: no-repeat; background-position: 0px -243px; " src="../../images/1x1.gif" />&#160;&#160;Active l''option de courriel</td>
            </tr>
        </tbody>
    </table>
    </li>
    <li>Mise en forme : Vous pouvez modifier les fichiers de mise en forme CSS et SKIN.</li>
    <li>Remplacement de l''entête: Code HTML pour afficher à l''entête de la page.</li>
    <li>Habillage pour modules : Vous pouvez modifier l''habillage des modules du portail, des options administrateur, des modules d''edition, du module d''inscription et les Tooltip.</li>
</ul>
<table cellspacing="0" cellpadding="0" border="0" align="center" width="80%">
    <tbody>
        <tr>
            <td>Pour modifier l''apparence de votre site vous avez deux fichiers que vous pouvez modifier. Le fichier Portail.CSS et le fichier menu.tpl1.js. Vous trouverez ces fichiers dans votre répertoire racine de votre portail. De plus si vous utilisez le forum et la galerie vous pourrez adapter leurs styles dans le fichier ttt.css. Je vous invite à utiliser un éditeur de style css pour modifier les fichiers css. Vous pouvez facilement trouver ce type d''éditeur sur l''internet. J''utilise acehtml, ce logiciel est disponible en version partagiciel.</td>
        </tr>
    </tbody>
</table>
<p>&#160;</p>
<table cellspacing="0" cellpadding="0" border="2" align="center" width="80%">
    <tbody>
        <tr>
            <td height="100" align="center" valign="middle" colspan="3">
            <h2>Haut de page</h2>
            <br />
            #banner {text-align: left; background: ivory} - Style pour toute la section haut de page<br />
            #logo {background: url(bk.jpg)} - Style pour la tranche horizontale du logo<br />
            .LogoImage {} - Style pour l''image logo mettre width et height pour accélérer l''affichage<br />
            #BreadCrumb {background: #f5f5f5;} - pour positioner le menu alternatif<br />
            .bread {font-size: 8px; color: gray; font-family: Wingdings 3} - pour le menu alternatif</td>
        </tr>
        <tr>
            <td height="300" align="center" valign="middle">
            <h2>Panneau Gauche</h2>
            <br />
            <br />
            .LeftPane { }</td>
            <td height="300" align="center" valign="middle">
            <h2>Panneau Central</h2>
            <br />
            <br />
            .ContentPane { } <br />
            td.line { } - pour une séparation conditionnelle entre les panneaux</td>
            <td height="300" align="center" valign="middle">
            <h2>Panneau Droite</h2>
            <br />
            <br />
            .RightPane { }</td>
        </tr>
        <tr>
            <td height="100" align="center" valign="middle" colspan="3">
            <h2>Bas de page</h2>
            <br />
            <br />
            <br />
            #footer {width: 100%; text-align: center } - Pour la section bas de page<br />
            <br />
            .A1 {color: #DCDCDC;} <br />
            <br />
            .A2 {background: #FFFFF0}<br />
            <br />
            div.a2 { border-bottom: Gray 1px solid; border-left: White 1px solid; border-top: White 1px solid; border-right: Gray 1px solid; width: 750}</td>
        </tr>
    </tbody>
</table>
<p>&#160;</p>
<table cellspacing="0" cellpadding="0" border="0" align="center" width="80%">
    <tbody>
        <tr>
            <td>
            <h1>
            <div align="center">Autres éléments d''importances de la page</div>
            </h1>
            <br />
            <ul type="circle"><br />
                <li><strong>La table principale</strong> - Utiliser le paramètre CSS #maintable { } pour désigner un style qui englobera .leftpane .contentpane et .rightpane</li>
                <li><strong>Le menu</strong> - Vous trouverez un fichier menu_tp1.js dans votre répertoire racine. Ce fichier détermine le paramètres d''affichages du menu. Pour changer la position du menu vous devez modifier block_top et block_left . Le menu est très flexible vous pouvez l''afficher ou vous voulez dans l''écran vertical ou horizontal. je vous invite à aller voir la <a title="Info tigra menu" href="http://www.softcomplex.com/products/tigra_menu/docs/">documentation ici</a></li>
                <li><strong>Le panneau d''administration</strong> - Pour le panneau d''administration utiliser le paramètre CSS .admin {z-index: 100; max-width: 370px; top:0; background: silver; border: thin dotted; padding: 4; position: absolute}</li>
                <li><strong>Le panneau d''ouverture de session</strong> - Pour positionner le menu d''ouverture de session utiliser le paramètre CSS <br />
                #signin {position: absolute; background: ivory; top: 150px; right: 60px; z-index: 100; border: thin dotted black; width: 160px}</li>
            </ul>
            <br />
            <div align="center">
            <h2>Un module</h2>
            </div>
            <br />
            La structure de base d''un module est : <br />
            <br />
            <strong>&lt;TABLE cellSpacing=0 cellPadding=0 width="100%" [BORDER] [COLOR]><br />
            &#160;&#160;&lt;TR><br />
            &#160;&#160; &lt;TD [ALIGN]>&#160;&#160;&#160;&#160;[MODULE]<br />
            &#160;&#160;&#160;&lt;/TD><br />
            &#160;&#160;&lt;/TR><br />
            &lt;/TABLE></strong><br />
            <br />
            Utiliser la fonction "Modifier les paramètres du module" pour faire des modifications.<br />
            <ul type="circle"><br />
                <li>Enlignement : Remplace le paramètre [ALIGN] du module si il est présent par align="xxx".</li>
                <li>Couleur : Remplace le paramètre [COLOR] du module si il est présent par bgcolor="xxxxxx", vous devez utiliser des définitions de couleurs valide HTML.</li>
                <li>Marge : Remplace le paramètre [BORDER] du module si il est présent par border="xx".</li>
                <li>CSS class du texte titre : Vous pouvez désigner une classe CSS pour faire la mise en forme du titre du module.&#160; La classe CSS doit être définie dans un fichier css de votre page, tel que Portal.css.</li>
                <li>CSS class du titre : Vous pouvez désigner une classe CSS pour faire la mise en forme du titre du module. La classe CSS doit être définie dans un fichier css de votre page, tel que Portal.css</li>
                <li>[MODULE] : Le texte [MODULE] sera remplacé par le code HTML généré par le serveur pour le module lors de l''affiche de la page.</li>
            </ul>
            </td>
        </tr>
        <tr>
            <td>
            <table summary="Module Design Table" width="100%" bgcolor="#c1d0e8" border="2" cellpadding="2" cellspacing="0">
                <tbody>
                    <tr>
                        <td align="left">
                        <table summary="Module Design Table" width="100%" bgcolor="#c1d0e8" border="2" cellpadding="2" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="left">
                                    <table class="HeaderCell" width="100%">
                                        <tbody>
                                            <tr>
                                                <td><span class="ItemTitle">HTML/Texte</span></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table summary="Module Design Table" width="100%" bgcolor="#c1d0e8" border="2" cellpadding="2" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="left">
                                    <p>&#160;</p>
                                    <h1>[MODULE]</h1>
                                    <p>&#160;</p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            </td>
        </tr>
    </tbody>
</table>
<p>&#160;</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SiteSettings2','<h1 style="text-align: center;">Paramètre du site général</h1>
<ul>
    <li>Administrateur : désigne l''administrateur du site.</li>
    <li>Avis courriel : Facutatif, inscrire une adresse de courriel si différente que celle du compte adminstrateur.</li>
    <li>Fuseau horaire : Inscrire le fuseau horaire du site web.</li>
    <li>Type d''inscription : Choisir un type d''inscription.</li>
    <li>Publicité fournisseur : Choisir la provenance des bannières publicitaires à afficher.</li>
    <li>Devise : Choisir la devise à utiliser pour paypal.</li>
    <li>Méthode de paiement : Seulement paypal est disponible.</li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SiteSettings3','<h1>Paramètres linguistiques</h1>
<ol>
    <li>Choisir la langue par défaut, ainsi que les langues à utiliser.</li>
    <li>Choisir la langue pour modifier les paramètres spécifiques à chaque langues.
    <ul>
        <li>Conditions générales</li>
        <li>Déclarations de confidentialité</li>
        <li>Titre du portail</li>
        <li>Texte de bas de page</li>
        <li>Description du portail</li>
        <li>Mots clés</li>
        <li>Directives d''ouverture d''une session</li>
        <li>Directives d''inscriptions</li>
        <li>Informations supplémentaires ajouté au courriel d''inscription</li>
        <li>Directives pour la création d''un portail démo (si activé par le webmestre seulement)</li>
    </ul>
    </li>
</ol>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SiteSettings4','Fonction de paramétrisation du site web.', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SiteSettings5','Fonction de paramétrisation du site web.', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SiteSettingsAlias','<h1>Détails de l''hébergement</h1>
<h2>Le webmestre peut modifier les paramêtres de l''hébergement :</h2>
<ol>
    <li>Frais de base pour l''hébergement.</li>
    <li>L''espace en MB maximal pour le portail. 0 pour un illimité.</li>
    <li>Le nombre de jours pour le log tu site.</li>
    <li>Date d''expiration du portail.</li>
</ol>
<p>&#160;</p>
<h2>Alias et nom de domaine</h2>
<p>Un portail peut avoir plusieurs alias ou nom de domaine. Chaque non de domaine doit être unique sur le serveur web.&#160; Si vous inscrivez un non de domaine déjà utiliser par un autre portail il sera réassigné à ce portail.&#160; Chaque non de domaine peut être désigné pour utiliser le protocol sécurisé SSL.&#160; Assurez vous que cette fonction est activé sur le serveur web.&#160; Si vous activé cette option et que le serveur web n''est pas configuré en conséquence, ceci pourra rendre le portail inopérant.</p>
<h2>Protocol SSL</h2>
<p><span style="font-size: larger; color: rgb(255, 0, 0);">IMPORTANT : &#160;</span>Le protocol SSL doit être préalablement installé sur le serveur web. &#160;Aussi que l''option <a class="headertitle" target="_blank" title="modifier option ssl" href="fr.default.aspx?adminpage=72">forcer ssl</a> dans les paramètres d''hébergement doit être activé. &#160;Un certificat SSL ne peut utiliser qu''une adresse IP. &#160;Donc, seulement un certificat SSL ne pourra être utilisé pour une instalation de DotNetZoom. &#160; Si le certificat SSL utilisé est d''un nom de domaine différent que celui du portail, vous devrez cocher l''option sub et ssl pour ce nom de domaine. &#160;<span style="font-size: larger; color: rgb(255, 0, 0); ">Ne cocher l''option ssl que pour un nom de domaine pour lequel un certificat SSL est valide, sinon vous rendrez le portail inopérant.</span></p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SiteSettingsDemo','<h1>Paramètres pour portail demo</h1>
<p>L''option démo doit être préalablement activé dans les paramètres d''hébergement. <a target="_blank" title="Modifier les options démo dans les paramètres d''hébergement" class="headertitle" href="fr.default.aspx?adminpage=72">Vous pouvez définir les options de base.</a>   Vous pouvez autoriser la création d''un de démo. Un site de démo peut utiliser un domaine enfant, c''est-à-dire dotnetzoom / votrenom ou un sous-domaine, c''est-à-dire yourname.dotnetzoom. Pour utiliser un sous-domaine Le serveur DNS doit être convigurer en conséquence, sinon le portail demo ainsi crée sera inopérable, &#160;parceque le nouveau sous-domaine ne sera pas disponible dans la table dns.</p><p><b>*Note:</b>  Une fois que qu''un nouveau portail sera créé.  L''utilisateur ainsi que le webmestre recevra un <b>courriel avec un <font color="#ff0000">hyperlien</font></b> pour activer le portail.  Donc il est important que les paramètres SMTP soit activé dans les <a target="_blank" title="Modifier les options SMTP dans les paramètres d''hébergement" class="headertitle" href="fr.default.aspx?adminpage=72">paramètres d''hébergement.</a></p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_SQL','<p>Les tables utilisateurs contiennent les données des utilisateurs.</p><p>
Une table peut contenir jusqu''à 1024 champs dont la somme des tailles n''excède pas 8060 octets. Pour dépasser les 8060 octets il faut utiliser les types de données text, binary et image, leurs valeurs sont stockées dans un espace séparé des autres données. Ces champs ne peuvent pas être inclus dans des index classiques.</p><p>
La version 2005 de SQL Server, la limite de 8060 octets peut-être dépassée avec tous les types de longueurs variables. Une constante MAXSIZE a été introduite pour définir la taille des champs au maximum adressable par le moteur de base de données (actuellement 2 giga-octets), elle sert à indiquer la longueur maximale des champs de type varchar(max), nvarchar(max), varbinary(max) qui remplacent respectivement les types text, binary et image qui ont été déclarés obsolètes.</p><p>
Les enregistrements d''une table peuvent être caractérisés par une clef primaire. La clef primaire est toujours indexée sur SQL Server. D''autres contraintes sont également disponibles au niveau des tables tel que : les contraintes uniques, les valeurs par défaut, les contraintes de vérification (check), les clefs étrangères.</p><p>
Il est possible de définir des procédures stockées. Une procédure stockée est une suite d''instructions qui vont avoir des effets sur la base de données ou qui renvoient des valeurs.</p><p>
Les procédures stockées sous SQL Server peuvent prendre en paramètre ou retourner des entiers, des chaînes de caractère, des dates, des curseurs, des tables, des tables virtuelles et tout autre type défini dans SQL Server par défaut ou par les utilisateurs.
Les principaux avantages de l''utilisation des procédures stockées sont : la sécurité accrue, la réutilisation des plans compilés et la possibilité de gérer les dépendances entre le code SQL et les objets du moteur.</p>
', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Tabs','<h1 style="text-align: center; ">Pages</h1>
<p>Vous pouvez ajouter, modifier ou changer la hiérarchie des pages par ce menu.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_TAGFileManagerModule','DisplayHelp_TAGFileManagerModule', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Terms','<span class="Head">CONDITIONS GÉNÉRALES</span>
<p>Les présentes conditions générales constituent l''intégralité de l''entente pour l''utilisation du site web.  Les conditions générales d''utilisation du site web sont régies par les lois de la province du Québec et par les lois fédérales du Canada applicables et doivent être interprétées en vertu de ces lois.  Si vous désirez plus d''information ou d''aide veuillez nous écrire.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_UserInfoModule','<h1 style="text-align: center;">Information membres</h1>
<p>Le module affiche des informations de base sur les membres du site.</p>
<p>Vous pouvez modifier vos informations et coordonnées.</p>
<p>Ajoutez l''utilisateur à votre liste d''usagers préférés.</p>
<p>Faire parvenir un message privé.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_UserListModule','<div style="text-align: center;">
<h1>Liste des membres</h1>
<div style="text-align: left;">
<p>Affiche une liste des membres du site web selon les critères de recherche que vous spécifiez.</p>
<h2>Options de recherche :</h2>
<ul>
    <li>Faire un choix de recheche :
    <ul>
        <li>Tous les usagers</li>
        <li>En ligne seulement</li>
        <li>Usagers préférés seulement</li>
    </ul>
    </li>
    <li>Trier :
    <ul>
        <li>Nom usager</li>
        <li>Nom</li>
    </ul>
    </li>
    <li>Ordre :
    <ul>
        <li>Croissant</li>
        <li>Décroissant</li>
    </ul>
    </li>
</ul>
</div>
</div>
<br />', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Users','<h1>Gestion usagers</h1>
<p>L''administrateurs est en mesure de gérer le compte de tous les utilisateurs.</p>
<p>Les tâches suivantes peuvent être effectuées :</p>
<ul>
    <li>Ajouter un nouveau compte d''utilisateur.</li>
    <li>Modifier les détails de compte d''utilisateur.</li>
    <li>Autoriser ou unauthorizer un compte d''utilisateur.</li>
    <li>Gérer les rôles de sécurité pour les comptes d''utilisateurs.</li>
    <li>Supprimer un compte utilisateur.</li>
    <li>Supprimer tous les utilisateurs non autorisés.</li>
    <li>Voir la création et la dernière date de connexion d''un utilisateur.</li>
</ul>
<p>Pour augmenter la facilité d''utilisation un certain nombre d''options sont disponibles pour manipuler l''affichage des comptes d''utilisateurs, le filtrage alphabétique, non autorisée et de tous les comptes utilisateurs.</p>
<p>L'' utilisateurs enregistrés peut gérer son compte ainsi que les services aux membres.</p>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_VendorFeedBack','<h1 align="center">Rétroaction services d''un fournisseur</h1>
<ul>
    <li>
    <div align="left">Expérience : Positive ou négative.</div>
    </li>
    <li>
    <div align="left">Commentaires :&#160; Information sur la satisfaction générale avec un fournisseur.</div>
    </li>
</ul>', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_Vendors','L''annuaire de service expose la liste de fournisseurs maintenus dans l''étiquette d''Admin. Ce module vous permet de rechercher des fournisseurs basés sur des critères de mot clé.', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_WeatherEdit','<br />
<br />
<br />
Sources des donn&eacute;es m&eacute;t&eacute;o. Conditions routi&egrave;res Vous savez comment l''&eacute;tat des routes est important pour votre s&eacute;curit&eacute; lors de la conduite hivernale. Cartes et bulletins saisonniers des pr&eacute;visions M&eacute;t&eacute;o.<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />', null 
GO
UpdatelonglanguageSetting 'fr','DisplayHelp_WebUpload','Fonction d''ajout et de modification.', null 
GO
UpdatelonglanguageSetting 'fr','Edit Access Denied','Message pour un bris de sécurité', null 
GO
UpdatelonglanguageSetting 'fr','EditAccessDeniedInfo','Votre profil de sécurité ne vous donne pas accès à cette page. Assurez vous que vous avez <a href="{httplogin}" title="ouvrir une session"><font color="red">ouvert une session</font></a> avec votre code d''accès. Si vous croyez que vous devriez avoir accès à cette page veuillez faire une demande à l''administrateur du site.', null 
GO
UpdatelonglanguageSetting 'fr','email_account_mod_notice','Votre dossier à été modifié

Date: {date}

Prénom: {firstname}
Nom: {lastname}

App.: {app}
Rue: {street}
Ville: {city}
Province: {province}
Pay: {country}
Code Postal: {postalcode}
Telephone: {phone}

Courriel: {email}', null 
GO
UpdatelonglanguageSetting 'fr','email_new_demo_portal','<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title></title>
</head>
<body>
{FullName}<br><br>
Votre portail web a été créé. Je vous demande de lire attentivement l''information de ce message et de le sauvegarder dans un endroit sûr pour référence future.  Par l''utilisation du nouveau site web ainsi créé, vous vous engagez à respecter tous les termes, conditions d''utilisation et mentions d''avertissement.<br><br>
Adresse web portail: {PortalURL}<br>
Code d''accès administrateur: {Username}<br>
Mot de passe administrateur: {Password}<br><br>
Pour une question de sécurité pour ouvrir une session la première fois avec votre code administrateur vous devrez utiliser l''hyperlien suivant : <a href="{validationcode}"> cliquer ici pour valider de votre inscription</a><br> 
<b>ou copier cette ligne dans la bare d''adresse de votre fureteur :</b><br><br>
 {validationcode}<br><br> 
<br><br>Vous êtes entièrement responsable du maintien de la confidentialité de votre mot de passe et de votre nom d''utilisateur. Vous êtes en outre entièrement responsable de toute activité ayant lieu sous votre compte. Vous vous engagez à avertir immédiatement  {PortalName}  de toute utilisation non autorisée de votre compte, ou de toute autre atteinte à la sécurité. {PortalName} ne pourra en aucun cas être tenue pour responsable d''un quelconque dommage que vous subiriez du fait de l''utilisation par autrui de votre mot de passe ou de votre compte, que vous ayez eu connaissance ou non de cette utilisation. Néanmoins, votre responsabilité pourrait être engagée si {PortalName} ou un tiers subissait des dommages dus à l''utilisation par autrui de votre compte ou de votre mot de passe. Il ne vous est jamais permis d''utiliser le compte d''autrui sans l''autorisation du titulaire du compte.
<br><br>{PortalName} se réserve le droit de modifier les termes, conditions et mentions d''avertissement applicables au site et Services qui vous sont proposés. Il vous incombe de consulter régulièrement ces termes et conditions d''utilisation. Votre utilisation renouvelée du site web {PortalName} constitue l''acceptation de votre part de tous ces termes, conditions d''utilisation et mentions d''avertissement.
<br><br>Sauf indication contraire, le site web {PortalName} est destiné à être utilisés à des fins personnelles et non commerciales. Vous n''êtes pas autorisé à modifier, copier, distribuer, transmettre, diffuser, représenter, reproduire, publier, concéder sous licence, créer des oeuvres dérivées, transférer ou vendre tout information, logiciel, produit ou service obtenu à partir de ce site web. Vous ne pouvez pas utiliser le site web {PortalName} à des fins commerciales, sans l''autorisation expresse, écrite et préalable, de {PortalName}.
<br><br><b><font color="#ff0000">Vous ne pouvez utiliser le site web {PortalName} qu''à condition de garantir que vous ne l''utiliserez pas à des fins illicites ou interdite</font></b><br><br>
Merci de votre participation...<br><br>
<a href="mailto:{HostEmail}?subject={PortalName} Création nouveau site web">{HostEmail}</a>


</body>
</html>', null 
GO
UpdatelonglanguageSetting 'fr','email_new_portal','<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title></title>
</head>
<body>
{FullName}<br><br>
Votre portail web a été créé. Je vous demande de lire attentivement l''information de ce message et de le sauvegarder dans un endroit sûr pour référence future.  Par l''utilisation du nouveau site web ainsi créé, vous vous engagez à respecter tous les termes, conditions d''utilisation et mentions d''avertissement.<br><br>
Adresse web portail: {PortalURL}<br>
Code d''accès administrateur: {Username}<br>
Mot de passe administrateur: {Password}<br><br>
Pour une question de sécurité pour ouvrir une session la première fois avec votre code administrateur vous devrez utiliser l''hyperlien suivant : <a href="{validationcode}"> cliquer ici pour valider de votre inscription</a><br> 
<b>ou copier cette ligne dans la bare d''adresse de votre fureteur :</b><br><br>
 {validationcode}<br><br> 
Merci de votre participation...<br><br>
<a href="mailto:{HostEmail}?subject={PortalName} Création nouveau site web">{HostEmail}</a>
</body>
</html>', null 
GO
UpdatelonglanguageSetting 'fr','email_newuser_account','{FullName},

Nous sommes heureux de vous informer que vous demande d''adhésion a été accepté pour le portail web {PortalName}. Je vous demande de lire attentivement l''information de ce message et de le sauvegarder dans un endroit sûr pour référence future.

Adresse web portail: {PortalURL}
Code d''accès: {Username}
Mot de passe: {Password}
{needcode}
Code de validation:  {validationcode}
{/needcode}

Venez voir le site web.

{signupmessage}

Merci de votre participation...

{PortalName}
                    

{AdministratorEmail}', null 
GO
UpdatelonglanguageSetting 'fr','email_newuser_notice','Date: {date}

Prénom: {firstname}
Nom: {lastname}

App.: {app}
Rue: {street}
Ville: {city}
Province: {province}
Pay: {country}
Code Postal: {postalcode}
Telephone: {phone}

Courriel: {email}', null 
GO
UpdatelonglanguageSetting 'fr','email_newuser_private','{FullName},

Merci de votre demande d''adhésion au portail web {PortalName}. Je vous demande de lire attentivement l''information de ce message et de le sauvegarder dans un endroit sûr pour référence future.

Adresse web portail: {PortalURL}
Code d''accès: {Username}
Mot de passe: {Password}

Vous allez recevoir un avis d''acceptation de la part du webmestre aussitôt que votre demande d''adhésion sera validée.

{signupmessage}

Merci de votre participation...

{PortalName}
                    

{AdministratorEmail}', null 
GO
UpdatelonglanguageSetting 'fr','email_newuser_verified','{FullName},

Merci de votre demande d''adhésion au portail web {PortalName}. Je vous demande de lire attentivement l''information de ce message et de le sauvegarder dans un endroit sûr pour référence future.

Adresse web portail: {PortalURL}
Code d''accès: {Username}
Mot de passe: {Password}
Code de validation:  {validationcode}

{signupmessage}

Merci de votre participation...

{PortalName}
                    

{AdministratorEmail}', null 
GO
UpdatelonglanguageSetting 'fr','email_password_recall','{FullName},

Vous nous avez demandé de vous faire parvenir votre mot de passe pour le site web {PortalName}

Vous devez utiliser cette information pour entrer lors de votre prochaine visite:

Adresse web portail: {PortalURL}
Code d''accès:        {Username}
Mot de passe:        {Password}
{needcode}
Code de validation:  {validationcode}
{/needcode}
{notauthorized}
Statut:              Non autorisé
{/notauthorized}

Sincèrement,

webmestre

*Note: veuillez négliger ce message, si vous n''avez pas demandé de vous faire parvenir votre mot de passe.', null 
GO
UpdatelonglanguageSetting 'fr','Forum_Moderated','Merci de votre contribution, vous venez de soumettre un message dans un forum dirigé. Un                                                    administrateur validera votre soumission et l''autorisera le cas échéant. Ceci peut                                                    prendre quelques minutes ou quelques jours. Vous recevrez un courriel aussitôt que                                                    votre message sera approuvé pour diffusion sur le forum. Je vous remercie pour votre                                                    patience.', null 
GO
UpdatelonglanguageSetting 'fr','ModuleDefinitionInfo','Des modules peuvent être ajoutés à l''application en utilisant un mécanisme automatisé d''installation. Afin d''employer ce dispositif, tous les dossiers associés au module                doivent d''abord être téléchargés sur le server web en utilisant l''option de gestion des fichiers.  Des dossiers peuvent être téléchargés individuellement ou comme paquet comprimé simple de bibliothèque (*.zip). Un manuscrit d''installation (*.xml) doit être inclus avec les dossiers du module que vous souhaitez installer. Si vous souhaitez installer un module manuellement vous devrer utiliser le manuscrit d''installation "Template" ci-dessous et alors éditer les propriétés de définition pour le module de "Template" créé.', null 
GO
UpdatelonglanguageSetting 'fr','new_portal_info','<p>{FullName}</p>
<p>Votre portail web a été créé. Vous allez recevoir dans les prochaine minutes un courriel avec de l''information pour entrer sur votre site web la premère fois</p>
<p>Par l''utilisation du nouveau site web ainsi créé, vous vous engagez à respecter tous les termes, conditions d''utilisation et mentions d''avertissement.</p>
<p>Adresse web portail: {PortalURL}</p>
<p>Pour une question de sécurité pour ouvrir une session la première fois avec votre code administrateur vous devrez utiliser l''hyperlien inclus de le courriel.</p> 
<p>Vous êtes entièrement responsable du maintien de la confidentialité de votre mot de passe et de votre nom d''utilisateur. Vous êtes en outre entièrement responsable de toute activité ayant lieu sous votre compte. Vous vous engagez à avertir immédiatement  {PortalName}  de toute utilisation non autorisée de votre compte, ou de toute autre atteinte à la sécurité. {PortalName} ne pourra en aucun cas être tenue pour responsable d''un quelconque dommage que vous subiriez du fait de l''utilisation par autrui de votre mot de passe ou de votre compte, que vous ayez eu connaissance ou non de cette utilisation. Néanmoins, votre responsabilité pourrait être engagée si {PortalName} ou un tiers subissait des dommages dus à l''utilisation par autrui de votre compte ou de votre mot de passe. Il ne vous est jamais permis d''utiliser le compte d''autrui sans l''autorisation du titulaire du compte.</p>
<p>{PortalName} se réserve le droit de modifier les termes, conditions et mentions d''avertissement applicables au site et Services qui vous sont proposés. Il vous incombe de consulter régulièrement ces termes et conditions d''utilisation. Votre utilisation renouvelée du site web {PortalName} constitue l''acceptation de votre part de tous ces termes, conditions d''utilisation et mentions d''avertissement.
<br><br>Sauf indication contraire, le site web {PortalName} est destiné à être utilisés à des fins personnelles et non commerciales. Vous n''êtes pas autorisé à modifier, copier, distribuer, transmettre, diffuser, représenter, reproduire, publier, concéder sous licence, créer des oeuvres dérivées, transférer ou vendre tout information, logiciel, produit ou service obtenu à partir de ce site web. Vous ne pouvez pas utiliser le site web {PortalName} à des fins commerciales, sans l''autorisation expresse, écrite et préalable, de {PortalName}.
</p><p><b><font color="#ff0000">Vous ne pouvez utiliser le site web {PortalName} qu''à condition de garantir que vous ne l''utiliserez pas à des fins illicites ou interdite</font></b></p>
<p>
<a href="mailto:{HostEmail}?subject={PortalName} Création nouveau site web">{HostEmail}</a></p>', null 
GO
UpdatelonglanguageSetting 'fr','PortalCreationInfo','<b>*Note:</b>  Une fois que vous avez compléter votre inscription et que votre nouveau site web a été créé.  Vous recevrez un <b>courriel avec un <font color="#ff0000">hyperlien</font></b> que vous devrez utiliser la première fois que vous voudrez ouvrir une session comme administrateur de votre site.	Pour accéder au nouveau portail en tant qu''administrateur, vous devez utiliser le code d''accès                            ainsi que le mot de passe que vous venez d''inscrire au formulaire.', null 
GO
UpdatelonglanguageSetting 'fr','PortalPrivacy','<table width="80%" align="center" border="0">
    <tbody>
        <tr>
            <td><br />
            <br />
            <span class="ItemTitle">D&eacute;claration de confidentialit&eacute;</span> <br />
            <br />
            {PortalName} est d&eacute;termin&eacute; &agrave; offrir un site Web qui respectent la vie priv&eacute;e des visiteurs. Cette page r&eacute;sume la politique et les pratiques de {PortalName} en ce qui concerne la protection des renseignements personnels sur le sites Web. <br />
            <br />
            <span class="ItemTitle">Renseignements personnels</span> <br />
            <br />
            Le site Web de {PortalName} ne saisis pas automatiquement de renseignements personnels vous concernant express&eacute;ment comme votre nom, votre num&eacute;ro de t&eacute;l&eacute;phone ou votre adresse &eacute;lectronique. Nous aurons acc&egrave;s &agrave; ce genre de renseignements uniquement si vous nous les fournissez en les inscrivant dans une section prot&eacute;g&eacute;e du site. Tous les renseignements personnels conserv&eacute;s ou saisis par le {PortalName} sont prot&eacute;g&eacute;s en vertu de la Loi sur la protection des renseignements personnels. Cela signifie qu''&agrave; chaque point de saisie, on vous demandera votre consentement avant de recueillir des renseignements vous concernant et on vous informera des fins pour lesquelles ces renseignements sont saisis et de la fa&ccedil;on dont vous pouvez exercer votre droit d''acc&egrave;s &agrave; ces renseignements. <br />
            <br />
            <span class="ItemTitle">Logiciel de protection</span> <br />
            <br />
            {PortalName} utilise un logiciel qui surveille la transmission des donn&eacute;es sur le r&eacute;seau pour d&eacute;celer toute tentative non autoris&eacute;e de t&eacute;l&eacute;charger ou de modifier des renseignements ou de causer d''autres dommages. Ce logiciel re&ccedil;oit et inscrit le protocole Internet (PI) de l''ordinateur qui est entr&eacute; en communication avec notre site Web, la date et l''heure de la visite et les pages consult&eacute;es. Nous n''essayons pas d''&eacute;tablir de liens entre ces adresses et l''identit&eacute; des personnes qui visitent notre site, &agrave; moins que nous n''ayons d&eacute;cel&eacute; une manoeuvre visant &agrave; endommager le site. <br />
            <br />
            <span class="ItemTitle">Utilisation des t&eacute;moins (cookies)</span> <br />
            <br />
            {PortalName} utilise &agrave; l''occasion des &laquo; t&eacute;moins &raquo; afin de d&eacute;terminer comment les visiteurs utilisent ce site ou les sites qu''ils ont consult&eacute;s pr&eacute;c&eacute;demment. Les &laquo; t&eacute;moins &raquo; que nous utilisons ne nous permettent pas d''identifier des personnes. Ils servent &agrave; compiler des statistiques sur les habitudes de transmission des donn&eacute;es et &agrave; &eacute;valuer l''efficacit&eacute; du site. Avant d''utiliser un &laquo; t&eacute;moin &raquo;, nous vous en informerons afin que vous ayez la possibilit&eacute; de le refuser; un tel refus n''aura aucun effet sur le rendement du site et aucune limite ne sera impos&eacute;e &agrave; votre capacit&eacute; de consulter des renseignements sur le site. (Un &laquo; t&eacute;moin &raquo; est un fichier qui peut &ecirc;tre plac&eacute; &agrave; votre insu sur votre unit&eacute; de disque dur et qui sert &agrave; surveiller les visites que vous faites &agrave; un site.) <br />
            <br />
            <span class="ItemTitle">S&eacute;curit&eacute; du syst&egrave;me</span> <br />
            <br />
            L''information relative &agrave; chaque visiteur est utilis&eacute;e par {PortalName} pour r&eacute;pondre &agrave; vos demandes ou assurer la s&eacute;curit&eacute; du syst&egrave;me. Nous n''utilisons pas cette information pour cr&eacute;er des profils pouvant &ecirc;tre identifi&eacute;s individuellement et nous ne la divulguons &agrave; personne de {PortalName}. <br />
            <br />
            <span class="ItemTitle">Questions</span> <br />
            <br />
            Si vous avez des questions ou des commentaires au sujet de cette politique ou de la fa&ccedil;on dont est appliqu&eacute; la Loi sur la protection des renseignements personnels, n''h&eacute;sitez pas &agrave; vous adresser {PortalName} par courriel &agrave; {AdministratorEmail}. Si notre r&eacute;ponse &agrave; vos pr&eacute;occupations au sujet de la protection des renseignements personnels ne vous satisfait pas, vous pouvez communiquer avec le <a href="mailto:info@privcom.gc.ca">Commissariat &agrave; la protection de la vie priv&eacute;e.</a> <br />
            <br />
            <br />
            </td>
        </tr>
    </tbody>
</table>', null 
GO
UpdatelonglanguageSetting 'fr','PortalTerms','<table width="80%" align="center" border="0">
    <tbody>
        <tr>
            <td><span class="ItemTitle">Conditions g&eacute;n&eacute;rales d''utilisation entre vous et {PortalName}</span> <br />
            <br />
            <span class="ItemTitle">COMPTE D''UTILISATEUR, MOT DE PASSE ET S&Eacute;CURIT&Eacute;</span> <br />
            <br />
            Si vous ouvrez un compte sur le site web {PortalName}, vous devez compl&eacute;ter le formulaire d''inscription avec des informations actuelles, compl&egrave;tes et exactes, comme le formulaire d''inscription en question vous y invite. Vous choisirez ensuite un mot de passe et un nom d''utilisateur. Vous &ecirc;tes enti&egrave;rement responsable du maintien de la confidentialit&eacute; de votre mot de passe et de votre nom d''utilisateur. Vous &ecirc;tes en outre enti&egrave;rement responsable de toute activit&eacute; ayant lieu sous votre compte. Vous vous engagez &agrave;&nbsp; avertir imm&eacute;diatement {PortalName} de toute utilisation non autoris&eacute;e de votre compte, ou de toute autre atteinte &agrave;&nbsp; la s&eacute;curit&eacute;. {PortalName} ne pourra en aucun cas &ecirc;tre tenue pour responsable d''un quelconque dommage que vous subiriez du fait de l''utilisation par autrui de votre mot de passe ou de votre compte, que vous ayez eu connaissance ou non de cette utilisation. N&eacute;anmoins, votre responsabilit&eacute; pourrait &ecirc;tre engag&eacute;e si {PortalName} ou un tiers subissait des dommages dus &agrave;&nbsp;l''utilisation par autrui de votre compte ou de votre mot de passe. Il ne vous est jamais permis d''utiliser le compte d''autrui sans l''autorisation du titulaire du compte. <br />
            <br />
            <span class="ItemTitle">MODIFICATION DES PR&Eacute;SENTES CONDITIONS D''UTILISATION</span> <br />
            <br />
            {PortalName} se r&eacute;serve le droit de modifier les termes, conditions et mentions d''avertissement applicables au site et Services qui vous sont propos&eacute;s. Il vous incombe de consulter r&eacute;guli&egrave;rement ces termes et conditions d''utilisation. Votre utilisation renouvel&eacute;e du site web {PortalName} constitue l''acceptation de votre part de tous ces termes, conditions d''utilisation et mentions d''avertissement. <br />
            <br />
            <span class="ItemTitle">UTILISATION LIMIT&Eacute;E &Agrave; DES FINS PERSONNELLES ET NON COMMERCIALES</span> <br />
            <br />
            Sauf indication contraire, le site web {PortalName} est destin&eacute; &agrave;&nbsp; &ecirc;tre utilis&eacute;s &agrave;&nbsp; des fins personnelles et non commerciales. Vous n''&ecirc;tes pas autoris&eacute; &agrave;&nbsp; modifier, copier, distribuer, transmettre, diffuser, repr&eacute;senter, reproduire, publier, conc&eacute;der sous licence, cr&eacute;er des oeuvres d&eacute;riv&eacute;es, transf&eacute;rer ou vendre tout information, logiciel, produit ou service obtenu &agrave;&nbsp; partir de ce site web. Vous ne pouvez pas utiliser le site web {PortalName} &agrave;&nbsp; des fins commerciales, sans l''autorisation expresse, &eacute;crite et pr&eacute;alable, de {PortalName}. <br />
            <br />
            <span class="ItemTitle">LIENS VERS DES SITES TIERS </span><br />
            <br />
            Le site web {PortalName} peut contenir des images de, et des liens vers des sites Web g&eacute;r&eacute;s par des tiers. {PortalName} n''exerce aucun contr&ocirc;le sur ces sites et n''assume aucune responsabilit&eacute; quant &agrave;&nbsp;leur contenu, ni notamment quant au contenu des liens pr&eacute;sent&eacute;s dans ces sites, ou encore aux modifications ou mises &agrave; jour apport&eacute;es &agrave;&nbsp; ces sites. <br />
            <br />
            <span class="ItemTitle">UTILISATION ILLICITE OU INTERDITE</span> <br />
            <br />
            Vous ne pouvez utiliser le site web {PortalName} qu''&agrave;&nbsp; condition de garantir que vous ne l''utiliserez pas &agrave;&nbsp; des fins illicites ou interdites par ces termes, conditions d''utilisation et mentions d''avertissement. <br />
            <br />
            <span class="ItemTitle">QUESTIONS</span> <br />
            <br />
            Si vous avez des questions ou des commentaires au sujet des conditions d''utilisation, n''h&eacute;sitez pas &agrave; vous adresser {PortalName} par courriel &agrave; {AdministratorEmail}. <br />
            <br />
            </td>
        </tr>
    </tbody>
</table>', null 
GO
UpdatelonglanguageSetting 'fr','Security_CannotUpload','Votre profil de sécurité ne vous permet pas de télécharger des fichiers sur le serveur.  L''administrateur du site pourra vous donner la permission de télécharger des fichiers en modifiant votre profil de sécurité ou en modifiant l''autorisation de télécharger de votre profil de sécurité.
<p>{AdministratorEmail} Faire une demande de modification pour ajouter permission de téléchargement</a></p>', null 
GO
UpdatelonglanguageSetting 'fr','Security_Enter_Portal','Vous devez utiliser l''hyperlien que nous vous avons fait parvenir par courriel pour débloquer votre compte usager.  Par contre, si vous avez de la difficulté à ouvrir une session, pour des raisons de sécurité je vous demande d''écrire au webmestre et il se fera un plaisir de vous dépanner
{HostEmail}', null 
GO
UpdatelonglanguageSetting 'fr','Security_Enter_PortalIP','Vous ne pouvez pas ouvrir une session à partir de cet endroit.  L''origine de la requête est différente que celle à votre dossier.  Veuillez faire parvenir un courriel au webmestre pour modifier votre compte usager 
votre IP {IP}{HostEmail} ', null 
GO
UpdatelonglanguageSetting 'fr','Security_Enter_PortalWait','Le temps d''attente n''est pas encore fini, pour des raisons de sécurité vous ne pourrez plus faire d''essais d''ouverture de session pour un période les 10 prochaines minutes.  Pour demander de l''aide vous pouvez faire une demande au webmestre.  Il se fera un plaisir de vous dépanner
{HostEmail}', null 
GO
UpdatelonglanguageSetting 'fr','Security_Enter_PortalWait1','Vous avez de la difficulté à ouvrir une session, pour des raisons de sécurité vous ne pourrez plus faire d''essais d''ouverture de session avant qu''un délais minimum de 10 minutes.  Pour demander de l''aide vous pouvez faire une demande au webmestre.  Il se fera un plaisir de vous dépanner 
{HostEmail}', null 
GO
UpdatelonglanguageSetting 'fr','Welcome_Accepted_Private','Votre demande d''inscription a été enregistrée.  Vous allez recevoir un avis d''acceptation de la part du webmestre aussitôt que votre demande d''adhésion sera validée.', null 
GO
UpdatelonglanguageSetting 'fr','Welcome_Accepted_public','Votre demande d''inscription a été acceptée.', null 
GO
UpdatelonglanguageSetting 'fr','Welcome_Accepted_Verified','Votre demande d''inscription a été enregistrée.  Vous allez recevoir un courriel avec un code de validation que vous devrez utiliser pour entrer sur le site web la première fois.', null 
GO
UpdateCountryCodes 'fr','AF','Afghanistan'
GO
UpdateCountryCodes 'fr','AL','Albania'
GO
UpdateCountryCodes 'fr','DZ','Algeria'
GO
UpdateCountryCodes 'fr','AS','American Samoa'
GO
UpdateCountryCodes 'fr','AD','Andorra'
GO
UpdateCountryCodes 'fr','AO','Angola'
GO
UpdateCountryCodes 'fr','AI','Anguilla'
GO
UpdateCountryCodes 'fr','AQ','Antarctica'
GO
UpdateCountryCodes 'fr','AG','Antigua and Barbuda'
GO
UpdateCountryCodes 'fr','AR','Argentina'
GO
UpdateCountryCodes 'fr','AM','Armenia'
GO
UpdateCountryCodes 'fr','AW','Aruba'
GO
UpdateCountryCodes 'fr','AU','Australia'
GO
UpdateCountryCodes 'fr','AT','Austria'
GO
UpdateCountryCodes 'fr','AZ','Azerbaijan'
GO
UpdateCountryCodes 'fr','BS','Bahamas'
GO
UpdateCountryCodes 'fr','BH','Bahrain'
GO
UpdateCountryCodes 'fr','BD','Bangladesh'
GO
UpdateCountryCodes 'fr','BB','Barbados'
GO
UpdateCountryCodes 'fr','BY','Belarus'
GO
UpdateCountryCodes 'fr','BE','Belgium'
GO
UpdateCountryCodes 'fr','BZ','Belize'
GO
UpdateCountryCodes 'fr','BJ','Benin'
GO
UpdateCountryCodes 'fr','BM','Bermuda'
GO
UpdateCountryCodes 'fr','BT','Bhutan'
GO
UpdateCountryCodes 'fr','BO','Bolivia'
GO
UpdateCountryCodes 'fr','BA','Bosnia and Herzegovina'
GO
UpdateCountryCodes 'fr','BW','Botswana'
GO
UpdateCountryCodes 'fr','BV','Bouvet Island'
GO
UpdateCountryCodes 'fr','BR','Brazil'
GO
UpdateCountryCodes 'fr','IO','British Indian Ocean Territory'
GO
UpdateCountryCodes 'fr','VG','British Virgin Islands'
GO
UpdateCountryCodes 'fr','BN','Brunei Darussalam'
GO
UpdateCountryCodes 'fr','BG','Bulgaria'
GO
UpdateCountryCodes 'fr','BF','Burkina Faso'
GO
UpdateCountryCodes 'fr','BI','Burundi'
GO
UpdateCountryCodes 'fr','KH','Cambodia'
GO
UpdateCountryCodes 'fr','CM','Cameroon'
GO
UpdateCountryCodes 'fr','CA','Canada'
GO
UpdateRegionCodes 'fr','CA','AB','Alberta'
GO
UpdateRegionCodes 'fr','CA','BC','Colombie Britannique'
GO
UpdateRegionCodes 'fr','CA','PE','Ile du Prince Edouard'
GO
UpdateRegionCodes 'fr','CA','MB','Manitoba'
GO
UpdateRegionCodes 'fr','CA','NB','Nouveau Brunswick'
GO
UpdateRegionCodes 'fr','CA','NS','Nouvelle-Écosse'
GO
UpdateRegionCodes 'fr','CA','NU','Nunavut'
GO
UpdateRegionCodes 'fr','CA','NV','Nunavut'
GO
UpdateRegionCodes 'fr','CA','ON','Ontario'
GO
UpdateRegionCodes 'fr','CA','QC','Québec'
GO
UpdateRegionCodes 'fr','CA','SK','Saskatchewan'
GO
UpdateRegionCodes 'fr','CA','NF','Terre-Neuve'
GO
UpdateRegionCodes 'fr','CA','NT','Territoires du Nord-Ouest'
GO
UpdateRegionCodes 'fr','CA','YT','Yukon'
GO
UpdateCountryCodes 'fr','CV','Cape Verde'
GO
UpdateCountryCodes 'fr','KY','Cayman Islands'
GO
UpdateCountryCodes 'fr','CF','Central African Republic'
GO
UpdateCountryCodes 'fr','TD','Chad'
GO
UpdateCountryCodes 'fr','CL','Chile'
GO
UpdateCountryCodes 'fr','CN','China'
GO
UpdateCountryCodes 'fr','CX','Christmas Island'
GO
UpdateCountryCodes 'fr','CC','Cocos'
GO
UpdateCountryCodes 'fr','CO','Colombia'
GO
UpdateCountryCodes 'fr','KM','Comoros'
GO
UpdateCountryCodes 'fr','CG','Congo'
GO
UpdateCountryCodes 'fr','CK','Cook Islands'
GO
UpdateCountryCodes 'fr','CR','Costa Rica'
GO
UpdateCountryCodes 'fr','HR','Croatia'
GO
UpdateCountryCodes 'fr','CU','Cuba'
GO
UpdateCountryCodes 'fr','CY','Cyprus'
GO
UpdateCountryCodes 'fr','CZ','Czech Republic'
GO
UpdateCountryCodes 'fr','DK','Denmark'
GO
UpdateCountryCodes 'fr','DJ','Djibouti'
GO
UpdateCountryCodes 'fr','DM','Dominica'
GO
UpdateCountryCodes 'fr','DO','Dominican Republic'
GO
UpdateCountryCodes 'fr','TP','East Timor'
GO
UpdateCountryCodes 'fr','EC','Ecuador'
GO
UpdateCountryCodes 'fr','EG','Egypt'
GO
UpdateCountryCodes 'fr','SV','El Salvador'
GO
UpdateCountryCodes 'fr','GQ','Equatorial Guinea'
GO
UpdateCountryCodes 'fr','ER','Eritrea'
GO
UpdateCountryCodes 'fr','EE','Estonia'
GO
UpdateCountryCodes 'fr','ET','Ethiopia'
GO
UpdateCountryCodes 'fr','FK','Falkland Islands'
GO
UpdateCountryCodes 'fr','FO','Faroe Islands'
GO
UpdateCountryCodes 'fr','FJ','Fiji'
GO
UpdateCountryCodes 'fr','FI','Finland'
GO
UpdateCountryCodes 'fr','FR','France'
GO
UpdateCountryCodes 'fr','GF','French Guiana'
GO
UpdateCountryCodes 'fr','PF','French Polynesia'
GO
UpdateCountryCodes 'fr','TF','French Southern Territories'
GO
UpdateCountryCodes 'fr','GA','Gabon'
GO
UpdateCountryCodes 'fr','GM','Gambia'
GO
UpdateCountryCodes 'fr','GE','Georgia'
GO
UpdateCountryCodes 'fr','DE','Germany'
GO
UpdateCountryCodes 'fr','GH','Ghana'
GO
UpdateCountryCodes 'fr','GI','Gibraltar'
GO
UpdateCountryCodes 'fr','GR','Greece'
GO
UpdateCountryCodes 'fr','GL','Greenland'
GO
UpdateCountryCodes 'fr','GD','Grenada'
GO
UpdateCountryCodes 'fr','GP','Guadeloupe'
GO
UpdateCountryCodes 'fr','GU','Guam'
GO
UpdateCountryCodes 'fr','GT','Guatemala'
GO
UpdateCountryCodes 'fr','GN','Guinea'
GO
UpdateCountryCodes 'fr','GW','Guinea-Bissau'
GO
UpdateCountryCodes 'fr','GY','Guyana'
GO
UpdateCountryCodes 'fr','HT','Haiti'
GO
UpdateCountryCodes 'fr','HM','Heard and McDonald Islands'
GO
UpdateCountryCodes 'fr','HN','Honduras'
GO
UpdateCountryCodes 'fr','HK','Hong Kong'
GO
UpdateCountryCodes 'fr','HU','Hungary'
GO
UpdateCountryCodes 'fr','IS','Iceland'
GO
UpdateCountryCodes 'fr','IN','India'
GO
UpdateCountryCodes 'fr','ID','Indonesia'
GO
UpdateCountryCodes 'fr','IR','Iran'
GO
UpdateCountryCodes 'fr','IQ','Iraq'
GO
UpdateCountryCodes 'fr','IE','Ireland'
GO
UpdateCountryCodes 'fr','IL','Israel'
GO
UpdateCountryCodes 'fr','IT','Italy'
GO
UpdateCountryCodes 'fr','CI','Ivory Coast'
GO
UpdateCountryCodes 'fr','JM','Jamaica'
GO
UpdateCountryCodes 'fr','JP','Japan'
GO
UpdateCountryCodes 'fr','JO','Jordan'
GO
UpdateCountryCodes 'fr','KZ','Kazakhstan'
GO
UpdateCountryCodes 'fr','KE','Kenya'
GO
UpdateCountryCodes 'fr','KI','Kiribati'
GO
UpdateCountryCodes 'fr','KW','Kuwait'
GO
UpdateCountryCodes 'fr','KG','Kyrgyzstan'
GO
UpdateCountryCodes 'fr','LA','Laos'
GO
UpdateCountryCodes 'fr','LV','Latvia'
GO
UpdateCountryCodes 'fr','LB','Lebanon'
GO
UpdateCountryCodes 'fr','LS','Lesotho'
GO
UpdateCountryCodes 'fr','LR','Liberia'
GO
UpdateCountryCodes 'fr','LY','Libya'
GO
UpdateCountryCodes 'fr','LI','Liechtenstein'
GO
UpdateCountryCodes 'fr','LT','Lithuania'
GO
UpdateCountryCodes 'fr','LU','Luxembourg'
GO
UpdateCountryCodes 'fr','MO','Macau'
GO
UpdateCountryCodes 'fr','MK','Macedonia'
GO
UpdateCountryCodes 'fr','MG','Madagascar'
GO
UpdateCountryCodes 'fr','MW','Malawi'
GO
UpdateCountryCodes 'fr','MY','Malaysia'
GO
UpdateCountryCodes 'fr','MV','Maldives'
GO
UpdateCountryCodes 'fr','ML','Mali'
GO
UpdateCountryCodes 'fr','MT','Malta'
GO
UpdateCountryCodes 'fr','MH','Marshall Islands'
GO
UpdateCountryCodes 'fr','MQ','Martinique'
GO
UpdateCountryCodes 'fr','MR','Mauritania'
GO
UpdateCountryCodes 'fr','MU','Mauritius'
GO
UpdateCountryCodes 'fr','YT','Mayotte'
GO
UpdateCountryCodes 'fr','MX','Mexico'
GO
UpdateCountryCodes 'fr','FM','Micronesia'
GO
UpdateCountryCodes 'fr','MD','Moldova'
GO
UpdateCountryCodes 'fr','MC','Monaco'
GO
UpdateCountryCodes 'fr','MN','Mongolia'
GO
UpdateCountryCodes 'fr','MS','Montserrat'
GO
UpdateCountryCodes 'fr','MA','Morocco'
GO
UpdateCountryCodes 'fr','MZ','Mozambique'
GO
UpdateCountryCodes 'fr','MM','Myanmar'
GO
UpdateCountryCodes 'fr','NA','Namibia'
GO
UpdateCountryCodes 'fr','NR','Nauru'
GO
UpdateCountryCodes 'fr','NP','Nepal'
GO
UpdateCountryCodes 'fr','NL','Netherlands'
GO
UpdateCountryCodes 'fr','AN','Netherlands Antilles'
GO
UpdateCountryCodes 'fr','NC','New Caledonia'
GO
UpdateCountryCodes 'fr','NZ','New Zealand'
GO
UpdateCountryCodes 'fr','NI','Nicaragua'
GO
UpdateCountryCodes 'fr','NE','Niger'
GO
UpdateCountryCodes 'fr','NG','Nigeria'
GO
UpdateCountryCodes 'fr','NU','Niue'
GO
UpdateCountryCodes 'fr','NF','Norfolk Island'
GO
UpdateCountryCodes 'fr','KP','North Korea'
GO
UpdateCountryCodes 'fr','MP','Northern Mariana Islands'
GO
UpdateCountryCodes 'fr','NO','Norway'
GO
UpdateCountryCodes 'fr','OM','Oman'
GO
UpdateCountryCodes 'fr','PK','Pakistan'
GO
UpdateCountryCodes 'fr','PW','Palau'
GO
UpdateCountryCodes 'fr','PA','Panama'
GO
UpdateCountryCodes 'fr','PG','Papua New Guinea'
GO
UpdateCountryCodes 'fr','PY','Paraguay'
GO
UpdateCountryCodes 'fr','PE','Peru'
GO
UpdateCountryCodes 'fr','PH','Philippines'
GO
UpdateCountryCodes 'fr','PN','Pitcairn'
GO
UpdateCountryCodes 'fr','PL','Poland'
GO
UpdateCountryCodes 'fr','PT','Portugal'
GO
UpdateCountryCodes 'fr','PR','Puerto Rico'
GO
UpdateCountryCodes 'fr','QA','Qatar'
GO
UpdateCountryCodes 'fr','RE','Reunion'
GO
UpdateCountryCodes 'fr','RO','Romania'
GO
UpdateCountryCodes 'fr','RU','Russian Federation'
GO
UpdateCountryCodes 'fr','RW','Rwanda'
GO
UpdateCountryCodes 'fr','GS','S. Georgia and S. Sandwich Islands'
GO
UpdateCountryCodes 'fr','KN','Saint Kitts and Nevis'
GO
UpdateCountryCodes 'fr','LC','Saint Lucia'
GO
UpdateCountryCodes 'fr','VC','Saint Vincent and The Grenadines'
GO
UpdateCountryCodes 'fr','WS','Samoa'
GO
UpdateCountryCodes 'fr','SM','San Marino'
GO
UpdateCountryCodes 'fr','ST','Sao Tome and Principe'
GO
UpdateCountryCodes 'fr','SA','Saudi Arabia'
GO
UpdateCountryCodes 'fr','SN','Senegal'
GO
UpdateCountryCodes 'fr','SC','Seychelles'
GO
UpdateCountryCodes 'fr','SL','Sierra Leone'
GO
UpdateCountryCodes 'fr','SG','Singapore'
GO
UpdateCountryCodes 'fr','SK','Slovakia'
GO
UpdateCountryCodes 'fr','SI','Slovenia'
GO
UpdateCountryCodes 'fr','SB','Solomon Islands'
GO
UpdateCountryCodes 'fr','SO','Somalia'
GO
UpdateCountryCodes 'fr','ZA','South Africa'
GO
UpdateCountryCodes 'fr','KR','South Korea'
GO
UpdateCountryCodes 'fr','SU','Soviet Union'
GO
UpdateCountryCodes 'fr','ES','Spain'
GO
UpdateCountryCodes 'fr','LK','Sri Lanka'
GO
UpdateCountryCodes 'fr','SH','St. Helena'
GO
UpdateCountryCodes 'fr','PM','St. Pierre and Miquelon'
GO
UpdateCountryCodes 'fr','SD','Sudan'
GO
UpdateCountryCodes 'fr','SR','Suriname'
GO
UpdateCountryCodes 'fr','SJ','Svalbard and Jan Mayen Islands'
GO
UpdateCountryCodes 'fr','SZ','Swaziland'
GO
UpdateCountryCodes 'fr','SE','Sweden'
GO
UpdateCountryCodes 'fr','CH','Switzerland'
GO
UpdateCountryCodes 'fr','SY','Syria'
GO
UpdateCountryCodes 'fr','TW','Taiwan'
GO
UpdateCountryCodes 'fr','TJ','Tajikistan'
GO
UpdateCountryCodes 'fr','TZ','Tanzania'
GO
UpdateCountryCodes 'fr','TH','Thailand'
GO
UpdateCountryCodes 'fr','TG','Togo'
GO
UpdateCountryCodes 'fr','TK','Tokelau'
GO
UpdateCountryCodes 'fr','TO','Tonga'
GO
UpdateCountryCodes 'fr','TT','Trinidad and Tobago'
GO
UpdateCountryCodes 'fr','TN','Tunisia'
GO
UpdateCountryCodes 'fr','TR','Turkey'
GO
UpdateCountryCodes 'fr','TM','Turkmenistan'
GO
UpdateCountryCodes 'fr','TC','Turks and Caicos Islands'
GO
UpdateCountryCodes 'fr','TV','Tuvalu'
GO
UpdateCountryCodes 'fr','UG','Uganda'
GO
UpdateCountryCodes 'fr','UA','Ukraine'
GO
UpdateCountryCodes 'fr','AE','United Arab Emirates'
GO
UpdateCountryCodes 'fr','UK','United Kingdom'
GO
UpdateCountryCodes 'fr','US','United States'
GO
UpdateRegionCodes 'fr','US','AL','Alabama'
GO
UpdateRegionCodes 'fr','US','AK','Alaska'
GO
UpdateRegionCodes 'fr','US','AZ','Arizona'
GO
UpdateRegionCodes 'fr','US','AR','Arkansas'
GO
UpdateRegionCodes 'fr','US','CA','Californie'
GO
UpdateRegionCodes 'fr','US','NC','Caroline du Nord'
GO
UpdateRegionCodes 'fr','US','CO','Colorado'
GO
UpdateRegionCodes 'fr','US','CT','Connecticut'
GO
UpdateRegionCodes 'fr','US','DE','Delaware'
GO
UpdateRegionCodes 'fr','US','DC','District of Columbia'
GO
UpdateRegionCodes 'fr','US','FL','Floride'
GO
UpdateRegionCodes 'fr','US','GA','Georgie'
GO
UpdateRegionCodes 'fr','US','HI','Hawaii'
GO
UpdateRegionCodes 'fr','US','ID','Idaho'
GO
UpdateRegionCodes 'fr','US','IL','Illinois'
GO
UpdateRegionCodes 'fr','US','IN','Indiana'
GO
UpdateRegionCodes 'fr','US','IA','Iowa'
GO
UpdateRegionCodes 'fr','US','KS','Kansas'
GO
UpdateRegionCodes 'fr','US','KY','Kentucky'
GO
UpdateRegionCodes 'fr','US','LA','Louisiane'
GO
UpdateRegionCodes 'fr','US','ME','Maine'
GO
UpdateRegionCodes 'fr','US','MD','Maryland'
GO
UpdateRegionCodes 'fr','US','MA','Massachusetts'
GO
UpdateRegionCodes 'fr','US','MI','Michigan'
GO
UpdateRegionCodes 'fr','US','MN','Minnesota'
GO
UpdateRegionCodes 'fr','US','MS','Mississippi'
GO
UpdateRegionCodes 'fr','US','MO','Missouri'
GO
UpdateRegionCodes 'fr','US','MT','Montana'
GO
UpdateRegionCodes 'fr','US','NE','Nebraska'
GO
UpdateRegionCodes 'fr','US','NV','Nevada'
GO
UpdateRegionCodes 'fr','US','NH','New Hampshire'
GO
UpdateRegionCodes 'fr','US','NJ','New Jersey'
GO
UpdateRegionCodes 'fr','US','NM','New Mexico'
GO
UpdateRegionCodes 'fr','US','NY','New York'
GO
UpdateRegionCodes 'fr','US','ND','North Dakota'
GO
UpdateRegionCodes 'fr','US','OH','Ohio'
GO
UpdateRegionCodes 'fr','US','OK','Oklahoma'
GO
UpdateRegionCodes 'fr','US','OR','Oregon'
GO
UpdateRegionCodes 'fr','US','PA','Pennsylvania'
GO
UpdateRegionCodes 'fr','US','RI','Rhode Island'
GO
UpdateRegionCodes 'fr','US','SC','South Carolina'
GO
UpdateRegionCodes 'fr','US','SD','South Dakota'
GO
UpdateRegionCodes 'fr','US','TN','Tennessee'
GO
UpdateRegionCodes 'fr','US','TX','Texas'
GO
UpdateRegionCodes 'fr','US','UT','Utah'
GO
UpdateRegionCodes 'fr','US','VT','Vermont'
GO
UpdateRegionCodes 'fr','US','VA','Virginia'
GO
UpdateRegionCodes 'fr','US','WA','Washington'
GO
UpdateRegionCodes 'fr','US','WV','West Virginia'
GO
UpdateRegionCodes 'fr','US','WI','Wisconsin'
GO
UpdateRegionCodes 'fr','US','WY','Wyoming'
GO
UpdateCountryCodes 'fr','UY','Uruguay'
GO
UpdateCountryCodes 'fr','UM','US Minor Outlying Islands'
GO
UpdateCountryCodes 'fr','VI','US Virgin Islands'
GO
UpdateCountryCodes 'fr','UZ','Uzbekistan'
GO
UpdateCountryCodes 'fr','VU','Vanuatu'
GO
UpdateCountryCodes 'fr','VE','Venezuela'
GO
UpdateCountryCodes 'fr','VN','Viet Nam'
GO
UpdateCountryCodes 'fr','WF','Wallis and Futuna Islands'
GO
UpdateCountryCodes 'fr','EH','Western Sahara'
GO
UpdateCountryCodes 'fr','YE','Yemen'
GO
UpdateCountryCodes 'fr','YU','Yugoslavia'
GO
UpdateCountryCodes 'fr','ZR','Zaire'
GO
UpdateCountryCodes 'fr','ZM','Zambia'
GO
UpdateCountryCodes 'fr','ZW','Zimbabwe'
GO
UpdateAdminModuleDefinitions 'fr','99','Admin','Menu pour les fonctions d''administrations'
GO
UpdateAdminModuleDefinitions 'fr','14','Paramètres du site','Les paramètres du site représentent les options locales pour votre portail. Le module vous permet d''adapter votre portail pour répondre à vos exigences.'
GO
UpdateAdminModuleDefinitions 'fr','13','Pages','L''administrateur peut contrôler les pages dans le portail. Ce module permet de créer, modifier, supprimer, changer l''ordre et de changer le niveau hiérarchique des pages.'
GO
UpdateAdminModuleDefinitions 'fr','12','Paramètres de sécurité','L''administrateur peut contrôler les profils de sécurité du portail. Le module vous permet d''ajouter, modifier, supprimer et contrôler les profils de sécurités ainsi que leurs attribution aux utilisateurs.'
GO
UpdateAdminModuleDefinitions 'fr','72','Paramètres d''hébergement','Le webmestre peut gérer tous les paramètres maitre pour le site d''hébergement.'
GO
UpdateAdminModuleDefinitions 'fr','15','Gestion usagers','Le module sert à gérer les utilisateurs enregistrés. Il vous permet d''ajouter, modifier et supprimer des comptes utilisateurs ainsi que de contrôler leurs profils de sécurité.'
GO
UpdateAdminModuleDefinitions 'fr','63','Portails','Le webmestre peut contrôler les paramètres d''hébergements des portails. Ce module vous permet d''ajouter, modifier et supprimer un portail.'
GO
UpdateAdminModuleDefinitions 'fr','65','SQL','Vous pouvez saisir des requêtes et les exécuter.  Aussi, en utilisant le fureteur, vous pouvez exécuter un fichier script.'
GO
UpdateAdminModuleDefinitions 'fr','21','Gestion fichiers','Le module sert à gérer les fichiers stockés sur le portail web. Le module vous permet de télécharger, supprimer et de synchroniser la liste des fichiers avec la basse de données SQL. Il fournit également des informations sur la quantité d''espace disque utilisée et disponible.'
GO
UpdateAdminModuleDefinitions 'fr','64','Gérer les modules','Vous pouvez modifier les noms ainsi que les définitions des modules.'
GO
UpdateAdminModuleDefinitions 'fr','19','Fournisseurs','Le module sert à gérer les comptes fournisseurs et les oriflammes associés au portail. Ce module vous permet d''ajouter un nouveau, modifier, et supprimer un compte fournisseur.'
GO
UpdateAdminModuleDefinitions 'fr','27','Statistiques','Le module sert à voir les statistiques d''utilisations de votre portail.  Plus particulièrement, qui utilise le site, quand et comment.'
GO
UpdateAdminModuleDefinitions 'fr','28','Courriel','Le module sert à envoyer du courriel en bloc aux utilisateurs appartenant à un profil de sécurité particulier.'
GO
UpdateAdminModuleDefinitions 'fr','100','Language','Section pour localisation et modification des paramètres linguistique'
GO
UpdateCurrencies 'fr','CAD','Canadien (CAD)'
GO
UpdateCurrencies 'fr','EUR','Euros (EUR)'
GO
UpdateCurrencies 'fr','GBP','Livres Sterling (GBP)'
GO
UpdateCurrencies 'fr','JPY','Yen (JPY)'
GO
UpdateCurrencies 'fr','USD','U.S. (USD)'
GO
UpdateBillingFrequencyCodes 'fr','D','Jour(s)'
GO
UpdateBillingFrequencyCodes 'fr','M','Mois'
GO
UpdateBillingFrequencyCodes 'fr','N','Aucun'
GO
UpdateBillingFrequencyCodes 'fr','O','Paiement unique'
GO
UpdateBillingFrequencyCodes 'fr','W','Semaine(s)'
GO
UpdateBillingFrequencyCodes 'fr','Y','Année(s)'
GO
UpdateSiteLogReports 'fr','5','Fureteurs utilisés'
GO
UpdateSiteLogReports 'fr','10','Inscription par date'
GO
UpdateSiteLogReports 'fr','11','Inscription par pays'
GO
UpdateSiteLogReports 'fr','4','Nombre de clics'
GO
UpdateSiteLogReports 'fr','9','Popularité par page'
GO
UpdateSiteLogReports 'fr','12','Références au site'
GO
UpdateSiteLogReports 'fr','2','Statistiques détaillées'
GO
UpdateSiteLogReports 'fr','7','Statistiques hebdomadaire'
GO
UpdateSiteLogReports 'fr','6','Statistiques horaire'
GO
UpdateSiteLogReports 'fr','8','Statistiques mensuelle'
GO
UpdateSiteLogReports 'fr','1','Statistiques quotidienne'
GO
UpdateSiteLogReports 'fr','3','Utilisation par usager'
GO
UpdateTimeZoneCodes 'fr','-720','GMT -12 IDLW: Ligne internationale date ouest'
GO
UpdateTimeZoneCodes 'fr','-660','GMT -11 NT: Nome'
GO
UpdateTimeZoneCodes 'fr','-600','GMT -10 Alaska-Hawaii'
GO
UpdateTimeZoneCodes 'fr','-540','GMT -9 YST: Yukon'
GO
UpdateTimeZoneCodes 'fr','-480','GMT -8 PST: Pacifique'
GO
UpdateTimeZoneCodes 'fr','-420','GMT -7 MST: Montagnes'
GO
UpdateTimeZoneCodes 'fr','-360','GMT -6 CST: Centre'
GO
UpdateTimeZoneCodes 'fr','-300','GMT -5 EST: East Canada et U.S.'
GO
UpdateTimeZoneCodes 'fr','-240','GMT -4 AST: Atlantique'
GO
UpdateTimeZoneCodes 'fr','-210','GMT -3.5 Terre-Neuve'
GO
UpdateTimeZoneCodes 'fr','-180','GMT -3 Brasil, Argentine'
GO
UpdateTimeZoneCodes 'fr','-120','GMT -2 AT: Azores'
GO
UpdateTimeZoneCodes 'fr','-60','GMT -1 WAT: Afrique de l''ouest'
GO
UpdateTimeZoneCodes 'fr','0','GMT: Greenwich Mean Time'
GO
UpdateTimeZoneCodes 'fr','60','GMT +1 CET: Europe Centrale'
GO
UpdateTimeZoneCodes 'fr','120','GMT +2 EET: Europe de l''est'
GO
UpdateTimeZoneCodes 'fr','180','BT: Baghdad'
GO
UpdateTimeZoneCodes 'fr','210','GMT +3.5 Iran'
GO
UpdateTimeZoneCodes 'fr','240','GMT +4 Kabul'
GO
UpdateTimeZoneCodes 'fr','300','GMT +5'
GO
UpdateTimeZoneCodes 'fr','330','GMT +5.5 Inde'
GO
UpdateTimeZoneCodes 'fr','360','GMT +6'
GO
UpdateTimeZoneCodes 'fr','390','GMT +6.5 Îles Cocos'
GO
UpdateTimeZoneCodes 'fr','420','GMT +7'
GO
UpdateTimeZoneCodes 'fr','480','GMT +8 CCT: Chine'
GO
UpdateTimeZoneCodes 'fr','540','GMT +9 JST: Japon'
GO
UpdateTimeZoneCodes 'fr','600','GMT +10 GST: Guam'
GO
UpdateTimeZoneCodes 'fr','660','GMT +11'
GO
UpdateTimeZoneCodes 'fr','690','GMT +11.5 Îles Norfolk'
GO
UpdateTimeZoneCodes 'fr','720','GMT +12 NZST: Nouvelle Zealande'
GO
UpdateTimeZoneCodes 'fr','780','GMT +13 Îles Rawaki'
GO
UpdateTimeZoneCodes 'fr','840','GMT +14 Îles Gilbert, Ellice, Phoenix et Line'
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
