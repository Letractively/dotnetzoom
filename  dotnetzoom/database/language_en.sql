----------------------------------------------------
-- Update
----------------------------------------------------
if not exists ( select * from language where language = 'en' )
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
   'en',
   'Anglais (États-Unis)',
   'en-US',
   'utf-8', 
   'Accueil', 
   'accueil', 
   'Administrateurs', 
   'Administration du portail', 
   'Membres adhérents', 
   'Membres adhérents' 
 )
 end
else
begin
 update language
 set 
 Language = 'en',
 Description = 'Anglais (États-Unis)',
 CultureCode = 'en-US',
 Encoding = 'utf-8', 
   HomePage = 'Accueil', 
   FriendlyHomePage = 'accueil', 
   AdminRole = 'Administrateurs', 
   AdminRoleDesc = 'Administration du portail', 
   UserRole = 'Membres adhérents', 
   UserRoleDesc = 'Membres adhérents' 
 where language = 'en'
 end
GO
updatelanguagecontext 'en','address_app','App.#', 'Address'
GO
updatelanguagecontext 'en','address_city','City', 'Address'
GO
updatelanguagecontext 'en','address_country','Country', 'Address'
GO
updatelanguagecontext 'en','address_Email','E-Mail', 'Address'
GO
updatelanguagecontext 'en','address_fax','Fax', 'Address'
GO
updatelanguagecontext 'en','address_postal_code','Postal Code', 'Address'
GO
updatelanguagecontext 'en','address_postal_code_ca','Postal Code', 'Address'
GO
updatelanguagecontext 'en','address_postal_code_us','Zip code', 'Address'
GO
updatelanguagecontext 'en','address_region','Province', 'Address'
GO
updatelanguagecontext 'en','address_region_ca','Province', 'Address'
GO
updatelanguagecontext 'en','address_region_us','State', 'Address'
GO
updatelanguagecontext 'en','address_street','Street', 'Address'
GO
updatelanguagecontext 'en','address_telephone','Phone number', 'Address'
GO
updatelanguagecontext 'en','address_WebSite','Web Site', 'Address'
GO
updatelanguagecontext 'en','actualiser','Refresh', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_add_module','Add&nbsp;a&nbsp;module', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_add_tab','Add a new page', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_clear_caches','Erase memory caches', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_edit_tab','Edit page', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_hide_info','Hide info', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_hide_option','Hide admin modules options', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_menu','Admin menu', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_menu_hide','Close the window', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_menu_title','Portal administration', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_profile','Click here to edit your profile', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_services','Click here to view the membership services', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_show_option','Show edit module options', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_StartHost','Start Host menu', 'AdminMenu'
GO
updatelanguagecontext 'en','admin_StartPortal','Start Portal menu', 'AdminMenu'
GO
updatelanguagecontext 'en','paramêtres','Settings', 'AdminMenu'
GO
updatelanguagecontext 'en','haut','Up', 'AdminMenu'
GO
updatelanguagecontext 'en','gauche','Left', 'AdminMenu'
GO
updatelanguagecontext 'en','voir l''aide','See help', 'AdminMenu'
GO
updatelanguagecontext 'en','bas','Down', 'AdminMenu'
GO
updatelanguagecontext 'en','cacher l''aide','Hide help', 'AdminMenu'
GO
updatelanguagecontext 'en','Demo_Create_Portal','{PortalName} offers you the possibility to create a Web site with {space} of disk space for a trial period of {days} days. At any time if you need help or more information please to contact me by e-mail {AdministratorEmail}', 'AdminMenu'
GO
updatelanguagecontext 'en','droite','Right', 'AdminMenu'
GO
updatelanguagecontext 'en','top','Up a level', 'AdminMenu'
GO
updatelanguagecontext 'en','title_TopToolTip','Move the module up a level', 'AdminMenu'
GO
updatelanguagecontext 'en','bottom','Down a level', 'AdminMenu'
GO
updatelanguagecontext 'en','title_bottomToolTip','Move the module down a level', 'AdminMenu'
GO
updatelanguagecontext 'en','click_to_erase','Click to erase the icon', 'Album'
GO
updatelanguagecontext 'en','select_this_image','select this image', 'Album'
GO
updatelanguagecontext 'en','album_show_icone','Show only icons of a certain size', 'Album'
GO
updatelanguagecontext 'en','need_title','You need a title', 'Announcement'
GO
updatelanguagecontext 'en','need_description','You need a description', 'Announcement'
GO
updatelanguagecontext 'en','header_date','Date', 'Announcement'
GO
updatelanguagecontext 'en','see_more','see more ...', 'Announcement'
GO
updatelanguagecontext 'en','label_expiration','End: YYYY-MM-DD', 'Announcement'
GO
updatelanguagecontext 'en','label_File','&nbsp;File link', 'Announcement'
GO
updatelanguagecontext 'en','label_see_stat','See stats?', 'Announcement'
GO
updatelanguagecontext 'en','label_SelectFile','Select a file&nbsp;', 'Announcement'
GO
updatelanguagecontext 'en','label_selectIlink','Select an internal link', 'Announcement'
GO
updatelanguagecontext 'en','label_selectlink','Select an external link', 'Announcement'
GO
updatelanguagecontext 'en','label_syndicate','*syndicate', 'Announcement'
GO
updatelanguagecontext 'en','label_title','Title', 'Announcement'
GO
updatelanguagecontext 'en','label_update_by','Last updated by', 'Announcement'
GO
updatelanguagecontext 'en','label_update_the','the', 'Announcement'
GO
updatelanguagecontext 'en','command_calendar','Calendar', 'Announcement'
GO
updatelanguagecontext 'en','command_upload','Upload a new file', 'Announcement'
GO
updatelanguagecontext 'en','label_Ilink','&nbsp;Internal link', 'Announcement'
GO
updatelanguagecontext 'en','label_link','&nbsp;External link', 'Announcement'
GO
updatelanguagecontext 'en','label_clicks','Clicks', 'Announcement'
GO
updatelanguagecontext 'en','label_description','Description', 'Announcement'
GO
updatelanguagecontext 'en','label_vieworder','Set view order', 'Announcement'
GO
updatelanguagecontext 'en','Mail_Add_File','Attach a file', 'BulkEMail'
GO
updatelanguagecontext 'en','Mail_Info','<strong>Note:</strong> The E-Mail will be sent in text or HTML, according to the option selected.', 'BulkEMail'
GO
updatelanguagecontext 'en','Mail_Portal_Role','Membership Role', 'BulkEMail'
GO
updatelanguagecontext 'en','Mail_Send','Send the E-Mail', 'BulkEMail'
GO
updatelanguagecontext 'en','Mail_Send_Again','Send another E-Mail', 'BulkEMail'
GO
updatelanguagecontext 'en','Mail_Send_To','The E-Mail was send:', 'BulkEMail'
GO
updatelanguagecontext 'en','today','Today', 'Calendar'
GO
updatelanguagecontext 'en','update','Update', 'Command'
GO
updatelanguagecontext 'en','upload','Upload', 'Command'
GO
updatelanguagecontext 'en','show','Show', 'Command'
GO
updatelanguagecontext 'en','TwoClickToSelect','Double clicks to select', 'Command'
GO
updatelanguagecontext 'en','UnZipFile','Decompress ZIP?', 'Command'
GO
updatelanguagecontext 'en','return','Return', 'Command'
GO
updatelanguagecontext 'en','Select_Color','Select a colour', 'Command'
GO
updatelanguagecontext 'en','select_icone','select icone', 'Command'
GO
updatelanguagecontext 'en','Selected','Selected', 'Command'
GO
updatelanguagecontext 'en','send','Send', 'Command'
GO
updatelanguagecontext 'en','Slideshow_Player','Slideshow', 'Command'
GO
updatelanguagecontext 'en','Highlight','Preview', 'Command'
GO
updatelanguagecontext 'en','install','Install', 'Command'
GO
updatelanguagecontext 'en','Clear','Clear', 'Command'
GO
updatelanguagecontext 'en','delete','Erase', 'Command'
GO
updatelanguagecontext 'en','editer','Edit', 'Command'
GO
updatelanguagecontext 'en','download','Download', 'Command'
GO
updatelanguagecontext 'en','enregistrer','Update', 'Command'
GO
updatelanguagecontext 'en','erase','Erase', 'Command'
GO
updatelanguagecontext 'en','Cmd_Reload','Reload', 'Command'
GO
updatelanguagecontext 'en','Viewer_Image','Image viewer', 'Command'
GO
updatelanguagecontext 'en','visualiser','Preview', 'Command'
GO
updatelanguagecontext 'en','modifier','Edit', 'Command'
GO
updatelanguagecontext 'en','request_confirm_erasealbum','You have {items} items in your album. Do you really want to erase them all?', 'Command'
GO
updatelanguagecontext 'en','FileExtNotAllowed','<br>This type of file {fileext} is not allowed on the portal.<br>You may only use the following file type :<br>{allowedext}', 'Command'
GO
updatelanguagecontext 'en','filemanager_security','<p>Your security profile does not allow you access to this page.</p><p>If you think you should be allowed access <b>please make a request to the webmaster of {PortalName} </b>', 'Command'
GO
updatelanguagecontext 'en','filemanager_security1','{AdministratorEmail}<img src="/images/uoUnreadPriv.gif" border="0" height="10" width="14" alt="courriel" Title="Click here to request permission"></a></p>', 'Command'
GO
updatelanguagecontext 'en','filemanager_security2','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button value="Close" onClick="javascript:window.close();">', 'Command'
GO
updatelanguagecontext 'en','Flash_Player','Gallery Flash Player', 'Command'
GO
updatelanguagecontext 'en','File_Exploreur','File Exploreur', 'Command'
GO
updatelanguagecontext 'en','File_Exploreur_Warning','You closed the maim window.', 'Command'
GO
updatelanguagecontext 'en','preview','Preview', 'Command'
GO
updatelanguagecontext 'en','reject','Reject', 'Command'
GO
updatelanguagecontext 'en','Media_Player','Media Player', 'Command'
GO
updatelanguagecontext 'en','request_confirm','Do you want to erase?', 'Command'
GO
updatelanguagecontext 'en','request_confirm_avatar','Do you want to configure the avatar gallery?', 'Command'
GO
updatelanguagecontext 'en','request_confirm_delete_icone','Do you want to erase the icon.  A default icon will be used instead.  You will be able to download a new one later on also.', 'Command'
GO
updatelanguagecontext 'en','request_confirm_erase','Do you want to erase?', 'Command'
GO
updatelanguagecontext 'en','request_confirm_erase_message','Do you want to erase this message?', 'Command'
GO
updatelanguagecontext 'en','request_confirm_erase_role','Do you wan to erase this membership role?', 'Command'
GO
updatelanguagecontext 'en','request_confirm_erase_userrole','Do you want to retrieve this security role to this nember?', 'Command'
GO
updatelanguagecontext 'en','OK','OK', 'Command'
GO
updatelanguagecontext 'en','syndicate','Create RSS File', 'Command'
GO
updatelanguagecontext 'en','New_Directory','New', 'Command'
GO
updatelanguagecontext 'en','add','Add', 'Command'
GO
updatelanguagecontext 'en','Add_Icone','Add a icon', 'Command'
GO
updatelanguagecontext 'en','Add_Image','Add an image', 'Command'
GO
updatelanguagecontext 'en','annuler','Cancel', 'Command'
GO
updatelanguagecontext 'en','approuve','Approuve', 'Command'
GO
updatelanguagecontext 'en','request_confirm_file_or_rep','Do you want to erase the files or directories?', 'Command'
GO
updatelanguagecontext 'en','F_ArticleID','ID article', 'Command'
GO
updatelanguagecontext 'en','admin_caches_x','Refresh', 'Command'
GO
updatelanguagecontext 'en','admin_m_add','Add module', 'Command'
GO
updatelanguagecontext 'en','admin_option_hide','Hide options', 'Command'
GO
updatelanguagecontext 'en','admin_option_show','Show options', 'Command'
GO
updatelanguagecontext 'en','admin_tab_add','Add page', 'Command'
GO
updatelanguagecontext 'en','admin_tab_edit','Edit page', 'Command'
GO
updatelanguagecontext 'en','admin_txt','Admin menu', 'Command'
GO
updatelanguagecontext 'en','admin_delete_tab','Delete page', 'Command'
GO
updatelanguagecontext 'en','admin_tab_delete','Delete the page', 'Command'
GO
updatelanguagecontext 'en','SS_Tigra_Edit','Edit menu', 'Command'
GO
updatelanguagecontext 'en','request_confirm_portal','Do you want to erase this portal?', 'Command'
GO
updatelanguagecontext 'en','request_confirm_premium','In order to change your options and extras your need to contact the webmaster', 'Command'
GO
updatelanguagecontext 'en','request_confirm_unsubscribe','Do you want to resign your membership?', 'Command'
GO
updatelanguagecontext 'en','Name','Name', 'Contact'
GO
updatelanguagecontext 'en','contact_email','Email', 'Contact'
GO
updatelanguagecontext 'en','contact_telephone','Telephone', 'Contact'
GO
updatelanguagecontext 'en','contact_title','Title', 'Contact'
GO
updatelanguagecontext 'en','label_anonymous','Anonymous', 'Discussion'
GO
updatelanguagecontext 'en','label_close','Closed', 'Discussion'
GO
updatelanguagecontext 'en','label_message_body','Message', 'Discussion'
GO
updatelanguagecontext 'en','label_message_date','Date', 'Discussion'
GO
updatelanguagecontext 'en','label_message_object','Subjet', 'Discussion'
GO
updatelanguagecontext 'en','label_message_user','Author', 'Discussion'
GO
updatelanguagecontext 'en','label_no_answer','No answer', 'Discussion'
GO
updatelanguagecontext 'en','label_open','Open', 'Discussion'
GO
updatelanguagecontext 'en','message_de','From', 'Discussion'
GO
updatelanguagecontext 'en','message_le','the', 'Discussion'
GO
updatelanguagecontext 'en','new_message_re','RE:', 'Discussion'
GO
updatelanguagecontext 'en','Reply','Answer', 'Discussion'
GO
updatelanguagecontext 'en','header_when','When', 'Documents'
GO
updatelanguagecontext 'en','header_size','(KB)', 'Documents'
GO
updatelanguagecontext 'en','header_title','Title', 'Documents'
GO
updatelanguagecontext 'en','header_what','What', 'Documents'
GO
updatelanguagecontext 'en','external_doc','External document', 'Documents'
GO
updatelanguagecontext 'en','internal_doc','Internal document', 'Documents'
GO
updatelanguagecontext 'en','channel_syndicate','* RSS channel', 'Documents'
GO
updatelanguagecontext 'en','header_who','Who', 'Documents'
GO
updatelanguagecontext 'en','select_internal_doc','Select an internal document', 'Documents'
GO
updatelanguagecontext 'en','select_external_doc','Select an external document', 'Documents'
GO
updatelanguagecontext 'en','type_doc','Type', 'Documents'
GO
updatelanguagecontext 'en','Demo_Portal','Demo Portal', 'EmailNotice'
GO
updatelanguagecontext 'en','Email_Test','test for E-Mail SMTP Setup', 'EmailNotice'
GO
updatelanguagecontext 'en','Member_Quit','Membership was revoqued', 'EmailNotice'
GO
updatelanguagecontext 'en','Account_Mod','Profile Modification', 'EmailNotice'
GO
updatelanguagecontext 'en','Mod_Password','Password changed', 'EmailNotice'
GO
updatelanguagecontext 'en','Vendor_request','Vendor request', 'EmailNotice'
GO
updatelanguagecontext 'en','New_Portal','New Portal', 'EmailNotice'
GO
updatelanguagecontext 'en','Bad_IP','Hacking attempt', 'EmailNotice'
GO
updatelanguagecontext 'en','Bad_IPTXT','Hacking attempt by IP : {0}
Url from : {1}
Url requested : {2}
User Agent : {3}
User ID : {4}
The IP will be bloqued for an hour', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_BodyMessageTXT','<b>Subject: {0}</b><br><b>Message:</b><br>{1}<br>Click<a href="{2}"><b> here </b></a>to go read this message.', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_Moderated_approved','Post approuver:', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_Moderated_erased','Post erased by moderator:', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_Moderated_message','Post waiting for approval:', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_Moderated_messageTXT','A new thread was started <b>{0}</b> in the forum <b>{1}</b>.<br>for your approuval.<br>Click <a href="{2}">here</a> to go to the forum.', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_Moderated_refused','Post refused by moderator:', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_ModMessageTXT','The message <b>{0}</b> was modified by <b>{1}</b><br> in the forum <b>{2}</b>.<br>', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_Moderated_approvedTXT','To {0}<br>you saved this message : <b>{1}</b> in the forum <b>{2}</b>.<br>This forum is moderated.<br>The E-Mail is to inform you that your message was <b>approuved</b> the {3}<br><br>Thank your for your contribution from <br><b>{4}</b>', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_Moderated_refusedTXT','To {0}<br>You saved a new post : <b>{1}</b> in the forum <b>{2}</b>.<br>The forum est moderated.<br>This E-Mail is to advise your that your message was <b>refused</b> the {3}<br><br>Thank you for your participation from <b>{4}</b>', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_NewMessage','New Post:', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_NewMessageTXT','A new post from <b>{0}</b><br>is available in forum <b>{1}</b>.<br>', 'EmailNotice'
GO
updatelanguagecontext 'en','Forum_ReplayMessageTXT','A reply to the post <b>{0}</b><br>was made by <b>{1}</b><br>in the forum <b>{2}</b>.<br>', 'EmailNotice'
GO
updatelanguagecontext 'en','Register_request','Register request', 'EmailNotice'
GO
updatelanguagecontext 'en','PMS_message','Private message on', 'EmailNotice'
GO
updatelanguagecontext 'en','Password_Notice','Your password pour', 'EmailNotice'
GO
updatelanguagecontext 'en','Reset_PasswordTXT','Dear {0},

Your password was modified following a cryptographic problem.
You will need to use the following information to enter the site for your next visit:
Portal URL:{1}
UserName:       {2}
Password:', 'EmailNotice'
GO
updatelanguagecontext 'en','Portal_Expired','The hosting aggrement for your {PortalName} ended {ExpiryDate}. For more information {AdministratorEmail}.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_city_name','City name is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','bad_trial_fee','You need to have a valid trial fee', 'ErrorMessage'
GO
updatelanguagecontext 'en','bad_trial_fee0','Trial fee must be greater than 0', 'ErrorMessage'
GO
updatelanguagecontext 'en','bad_trial_period','The information for the trial period is not valid', 'ErrorMessage'
GO
updatelanguagecontext 'en','bad_trial_period0','Must be greater then 0', 'ErrorMessage'
GO
updatelanguagecontext 'en','bad_value_services','The value for the membership fee is not appropriate', 'ErrorMessage'
GO
updatelanguagecontext 'en','bad_value_services0','membership fee must be superior to 0', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_country_code','A country is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_country_name','The country is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_CPM_number','You need a valid CPM number', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_decimal','You need to use a valid decimal number', 'ErrorMessage'
GO
updatelanguagecontext 'en','bad_billing_period','The billing period is not valid', 'ErrorMessage'
GO
updatelanguagecontext 'en','bad_billing_period0','The billing period must be greater than 0', 'ErrorMessage'
GO
updatelanguagecontext 'en','Need_Directory_Name','You need to have a valid name to create a new directory', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_email','An E-Mail is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_exposition_number','You need a valid number of expositions', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_firstname','A first name is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_username','You need a proper username.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_username_minimum','You need to chose a username with a minimum of 8 letter and number.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_valid_description','A description is needed', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_valid_emai','The E-Mail addresse is not valid', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_valid_email','A valid E-Mail address is required.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_vendor_name','The compagny name is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_integer','You need to use a valid number only', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_lastname','A name is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_location_mapquest','You need to have a location', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_module_def','Enter a new definition for the module', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_number','Need a  valid number', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_number_purchase','You need to specify the number of units you want to puchase', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_object_message','You need a title for the message', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_password','A password is required.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_password_confirm','You must validate your password.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_password_match','Your password do not match try again.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_password_minimum','You must have a password with a minimum of 8 letters or number.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_portal_name','A portal name is required.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_postal_code','The postal code is needed', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_region_code','The province is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_region_name','The province is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_role_name','You need a Role Name.', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_start_date','A date is needed', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_street_name','Street name and the civic number are required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_tab_name','A name for the page is needed', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_telephone','The phone number is needed', 'ErrorMessage'
GO
updatelanguagecontext 'en','not_a_date','Bad date!', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_a_valide_name','You need a valid name', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_alt','An alternate text is required', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_banner_name','You need a name for the banner', 'ErrorMessage'
GO
updatelanguagecontext 'en','need_body_message','You need to have some text un the message!', 'ErrorMessage'
GO
updatelanguagecontext 'en','Valid_Boolean','You can only use True or False', 'ErrorMessage'
GO
updatelanguagecontext 'en','Valid_Date_Format','The accepted date format is AAAA/MM/JJ', 'ErrorMessage'
GO
updatelanguagecontext 'en','SiteLogLimited','The site log history is limited to {days} days.', 'ErrorMessage'
GO
updatelanguagecontext 'en','SiteLogOff','The webmaster deactivated this option on your Portal', 'ErrorMessage'
GO
updatelanguagecontext 'en','select_icone_tooltip','I recommand to user an icone of 20px X 20px max!', 'Events'
GO
updatelanguagecontext 'en','end_date','End date', 'Events'
GO
updatelanguagecontext 'en','label_icone','Icone', 'Events'
GO
updatelanguagecontext 'en','calendar','Calendar', 'Events'
GO
updatelanguagecontext 'en','calendar_cel_height','Cell Height', 'Events'
GO
updatelanguagecontext 'en','calendar_cel_width','Cell width', 'Events'
GO
updatelanguagecontext 'en','label_alt','Alternative text', 'Events'
GO
updatelanguagecontext 'en','label_period','Period', 'Events'
GO
updatelanguagecontext 'en','events_day','Days(s)', 'Events'
GO
updatelanguagecontext 'en','events_format','Show in a', 'Events'
GO
updatelanguagecontext 'en','events_interval','Interval', 'Events'
GO
updatelanguagecontext 'en','events_list','List', 'Events'
GO
updatelanguagecontext 'en','events_month','Month', 'Events'
GO
updatelanguagecontext 'en','events_week','Week (s)', 'Events'
GO
updatelanguagecontext 'en','events_year','Year(s)', 'Events'
GO
updatelanguagecontext 'en','start_date','Start date', 'Events'
GO
updatelanguagecontext 'en','start_hour','Time', 'Events'
GO
updatelanguagecontext 'en','exif_Aperture','Diaphragm:        F', 'Exif'
GO
updatelanguagecontext 'en','exif_artist','Artist:', 'Exif'
GO
updatelanguagecontext 'en','exif_copyright','Copyright:', 'Exif'
GO
updatelanguagecontext 'en','exif_date_time','Date Time:', 'Exif'
GO
updatelanguagecontext 'en','exif_date_time_created','Original:', 'Exif'
GO
updatelanguagecontext 'en','exif_date_time_digitized','Digitized:', 'Exif'
GO
updatelanguagecontext 'en','exif_date_time_last_mod','General:', 'Exif'
GO
updatelanguagecontext 'en','exif_description','Description:', 'Exif'
GO
updatelanguagecontext 'en','exif_equipement','Camera:', 'Exif'
GO
updatelanguagecontext 'en','exif_equipement_maker','Producer:', 'Exif'
GO
updatelanguagecontext 'en','exif_equipement_model','Model:', 'Exif'
GO
updatelanguagecontext 'en','exif_ExposureMeteringMode','Exposure Mode:', 'Exif'
GO
updatelanguagecontext 'en','exif_ExposureProgram','Exposition:', 'Exif'
GO
updatelanguagecontext 'en','exif_ExposureTime','Exposure Time:', 'Exif'
GO
updatelanguagecontext 'en','exif_flash','Flash:', 'Exif'
GO
updatelanguagecontext 'en','exif_FocalLength','Focal:', 'Exif'
GO
updatelanguagecontext 'en','exif_iso','ISO  sensitivity:', 'Exif'
GO
updatelanguagecontext 'en','exif_LightSource','Light Source(WB):', 'Exif'
GO
updatelanguagecontext 'en','exif_orientation','Orientation:', 'Exif'
GO
updatelanguagecontext 'en','exif_photo_param','Photo settings:', 'Exif'
GO
updatelanguagecontext 'en','exif_resolution','Resolution:', 'Exif'
GO
updatelanguagecontext 'en','exif_software','Software:', 'Exif'
GO
updatelanguagecontext 'en','exif_SubjectDistance','Distance:', 'Exif'
GO
updatelanguagecontext 'en','exif_title','Title:', 'Exif'
GO
updatelanguagecontext 'en','exif_width_height','Dimensions:', 'Exif'
GO
updatelanguagecontext 'en','answer','Answer :', 'Faqs'
GO
updatelanguagecontext 'en','question','Q.', 'Faqs'
GO
updatelanguagecontext 'en','label_question','Question', 'Faqs'
GO
updatelanguagecontext 'en','label_answer','Answer', 'Faqs'
GO
updatelanguagecontext 'en','email','E-Mail', 'Feedback'
GO
updatelanguagecontext 'en','your_name','Your Name', 'Feedback'
GO
updatelanguagecontext 'en','tooltip_feedback','Update the default E-mail!', 'Feedback'
GO
updatelanguagecontext 'en','tooltip_send','Send the E-Mail', 'Feedback'
GO
updatelanguagecontext 'en','to','to', 'Feedback'
GO
updatelanguagecontext 'en','object','Subject', 'Feedback'
GO
updatelanguagecontext 'en','FeedBack_New_Add','Please indicate your E-Mail address', 'Feedback'
GO
updatelanguagecontext 'en','FeedBack_Mail_Send','The E-Mail was send', 'Feedback'
GO
updatelanguagecontext 'en','F_Refresh','Refresh', 'File'
GO
updatelanguagecontext 'en','F_delete','Erase the file or directory', 'File'
GO
updatelanguagecontext 'en','F_UnzipFile','Decompress ZIP ?', 'File'
GO
updatelanguagecontext 'en','F_Rename','Rename the file or directory', 'File'
GO
updatelanguagecontext 'en','F_New_Folder','Create a new directory', 'File'
GO
updatelanguagecontext 'en','F_ParentDir','Parent directory', 'File'
GO
updatelanguagecontext 'en','F_Download','Download files(s)', 'File'
GO
updatelanguagecontext 'en','F_FileName','File:', 'File'
GO
updatelanguagecontext 'en','F_cmdSynchronize','Update the file data base', 'File'
GO
updatelanguagecontext 'en','F_Directory','Directory', 'File'
GO
updatelanguagecontext 'en','F_btnUpload','Upload', 'File'
GO
updatelanguagecontext 'en','F_Header_Created','Created the', 'File'
GO
updatelanguagecontext 'en','F_Header_Mod','Changed the', 'File'
GO
updatelanguagecontext 'en','F_Header_Name','Name', 'File'
GO
updatelanguagecontext 'en','F_Header_Size','Size', 'File'
GO
updatelanguagecontext 'en','F_Header_Type','Type', 'File'
GO
updatelanguagecontext 'en','F_Update','Update the upload permissions', 'File'
GO
updatelanguagecontext 'en','F_Upload','Upload files(s) in this directory', 'File'
GO
updatelanguagecontext 'en','F_Upload_Auth','Upload authorisation', 'File'
GO
updatelanguagecontext 'en','F_UploadFile','Upload', 'File'
GO
updatelanguagecontext 'en','F_UploadFileList','First file', 'File'
GO
updatelanguagecontext 'en','F_UploadInDirec','The files will be uploaded in directory :', 'File'
GO
updatelanguagecontext 'en','WriteError','It was impossible to save the file. Make sure your have proper authorisation to write in the directory.', 'File'
GO
updatelanguagecontext 'en','cmdRemove','Erase', 'File'
GO
updatelanguagecontext 'en','hypHost','Designed by  {hostname}', 'Footer'
GO
updatelanguagecontext 'en','hypPrivacy','Privacy Statement', 'Footer'
GO
updatelanguagecontext 'en','hypTerms','Terms Of Use', 'Footer'
GO
updatelanguagecontext 'en','F_SendEMail','E-MAIL', 'Forum'
GO
updatelanguagecontext 'en','cmdSearch','Search', 'Forum'
GO
updatelanguagecontext 'en','cmdSubscribe','Subscribe', 'Forum'
GO
updatelanguagecontext 'en','cmdHome','Home', 'Forum'
GO
updatelanguagecontext 'en','cmdModerate','Moderate', 'Forum'
GO
updatelanguagecontext 'en','cmdNewTopic','Add new thread', 'Forum'
GO
updatelanguagecontext 'en','cmdPMS','Private messages', 'Forum'
GO
updatelanguagecontext 'en','cmdProfile','Profile', 'Forum'
GO
updatelanguagecontext 'en','Search_Subject','Subject', 'Forum'
GO
updatelanguagecontext 'en','FZuserAIM','AIM', 'Forum'
GO
updatelanguagecontext 'en','FZuserICQ','ICQ', 'Forum'
GO
updatelanguagecontext 'en','Search_Text','Text', 'Forum'
GO
updatelanguagecontext 'en','Search_SelectAllForumGroup','Select all forum groups', 'Forum'
GO
updatelanguagecontext 'en','F_UserAdmin','Admin. users', 'Forum'
GO
updatelanguagecontext 'en','F_UserAdress','Addresse', 'Forum'
GO
updatelanguagecontext 'en','F_UserAIM','AIM', 'Forum'
GO
updatelanguagecontext 'en','F_UserAlias','Alias', 'Forum'
GO
updatelanguagecontext 'en','F_UserCode','User Code', 'Forum'
GO
updatelanguagecontext 'en','F_UserCountry','Country', 'Forum'
GO
updatelanguagecontext 'en','F_UserEmail','E-Mail addresse?', 'Forum'
GO
updatelanguagecontext 'en','F_UserEmail0','E-Mail', 'Forum'
GO
updatelanguagecontext 'en','F_UserEmailInfo','Select this option to show your E-Mail in your profile.', 'Forum'
GO
updatelanguagecontext 'en','F_UserICQ','ICQ', 'Forum'
GO
updatelanguagecontext 'en','F_UserID','ID user', 'Forum'
GO
updatelanguagecontext 'en','F_UserInfoUser','User info', 'Forum'
GO
updatelanguagecontext 'en','F_UserInterest','Hobbies', 'Forum'
GO
updatelanguagecontext 'en','F_UserMode','Moderator', 'Forum'
GO
updatelanguagecontext 'en','F_UserMSN','MSN', 'Forum'
GO
updatelanguagecontext 'en','F_UserName','Name', 'Forum'
GO
updatelanguagecontext 'en','F_UserOwnAv','Use your avatar', 'Forum'
GO
updatelanguagecontext 'en','F_UserPMS','Private messages?', 'Forum'
GO
updatelanguagecontext 'en','F_UserProfile','User profile', 'Forum'
GO
updatelanguagecontext 'en','F_UserProfileParam','User settings', 'Forum'
GO
updatelanguagecontext 'en','F_UserRTE','Rich Text Editor?', 'Forum'
GO
updatelanguagecontext 'en','F_UserRTEInfo','Word with Firefox and Explorer.', 'Forum'
GO
updatelanguagecontext 'en','F_UserSelect','Select a user', 'Forum'
GO
updatelanguagecontext 'en','F_UserSelectAv','Select an avatar', 'Forum'
GO
updatelanguagecontext 'en','F_UserShowUO','On line?', 'Forum'
GO
updatelanguagecontext 'en','F_UserShowUOInfo','Select this option to show when you are online.', 'Forum'
GO
updatelanguagecontext 'en','F_UserSignature','Signature?', 'Forum'
GO
updatelanguagecontext 'en','F_UserStats','Statistics', 'Forum'
GO
updatelanguagecontext 'en','F_UserThread','Follow thread?', 'Forum'
GO
updatelanguagecontext 'en','F_UserTime','Time Zone?', 'Forum'
GO
updatelanguagecontext 'en','F_UserTime0','Time Zone', 'Forum'
GO
updatelanguagecontext 'en','F_UserTrust','Trusted User?', 'Forum'
GO
updatelanguagecontext 'en','F_UserUploadAv','Upload your avatar', 'Forum'
GO
updatelanguagecontext 'en','F_UserURL','URL', 'Forum'
GO
updatelanguagecontext 'en','F_UserWork','Work', 'Forum'
GO
updatelanguagecontext 'en','F_UserYAHOO','YAHOO', 'Forum'
GO
updatelanguagecontext 'en','F_Validate','Validate', 'Forum'
GO
updatelanguagecontext 'en','F_Validated','To check', 'Forum'
GO
updatelanguagecontext 'en','F_VisitUserSite','Visit', 'Forum'
GO
updatelanguagecontext 'en','F_VisitWWWof','Visit the web site of', 'Forum'
GO
updatelanguagecontext 'en','F_Who','by', 'Forum'
GO
updatelanguagecontext 'en','F_WhoThere','Who''s there', 'Forum'
GO
updatelanguagecontext 'en','go','Go', 'Forum'
GO
updatelanguagecontext 'en','F_StatsNewPost','new posts.', 'Forum'
GO
updatelanguagecontext 'en','F_StatsNewThread','new thread and', 'Forum'
GO
updatelanguagecontext 'en','F_StatsNPost','posts were added', 'Forum'
GO
updatelanguagecontext 'en','F_StatsNThread','threads and', 'Forum'
GO
updatelanguagecontext 'en','F_StatsTop10','The 10 most active users are', 'Forum'
GO
updatelanguagecontext 'en','F_StatsUser','users contributed to', 'Forum'
GO
updatelanguagecontext 'en','F_sThread','Thread', 'Forum'
GO
updatelanguagecontext 'en','F_Stiky','To see', 'Forum'
GO
updatelanguagecontext 'en','F_SubForum','Forum subscription', 'Forum'
GO
updatelanguagecontext 'en','F_SubParamEMail','E-Mail notice settings', 'Forum'
GO
updatelanguagecontext 'en','F_SubParamEMailInfo','(You will be advised by E-Mail of new messages posted in the forum you subscribed.)', 'Forum'
GO
updatelanguagecontext 'en','F_Subscribe','Subscribe', 'Forum'
GO
updatelanguagecontext 'en','F_Thread','Threads', 'Forum'
GO
updatelanguagecontext 'en','F_ThreadP','Thread per page', 'Forum'
GO
updatelanguagecontext 'en','F_Today','Today', 'Forum'
GO
updatelanguagecontext 'en','F_ToLarge','The file must be smaller than {maxFileSize} KB.', 'Forum'
GO
updatelanguagecontext 'en','F_ToSee','Pin this message?', 'Forum'
GO
updatelanguagecontext 'en','F_TreeView','Tree view', 'Forum'
GO
updatelanguagecontext 'en','F_UnSubscribe','Cancel the subscription...', 'Forum'
GO
updatelanguagecontext 'en','FZEditPost','Edit Post', 'Forum'
GO
updatelanguagecontext 'en','Quote_Wrote','wrote', 'Forum'
GO
updatelanguagecontext 'en','F_SelectModerated','Select moderators', 'Forum'
GO
updatelanguagecontext 'en','FZpage','Page', 'Forum'
GO
updatelanguagecontext 'en','F_SendPMS','PRIVATE MESSAGE', 'Forum'
GO
updatelanguagecontext 'en','Forum_Search','Advanced search', 'Forum'
GO
updatelanguagecontext 'en','Select_Forum_Search','Select all', 'Forum'
GO
updatelanguagecontext 'en','FZuserMSN','MSN', 'Forum'
GO
updatelanguagecontext 'en','FZuserYAHOO','YAHOO', 'Forum'
GO
updatelanguagecontext 'en','F_SelectAdmin','Select administration', 'Forum'
GO
updatelanguagecontext 'en','F_Contributed','{username} contributed {numpost} posts.  The last one was {datelast}.', 'Forum'
GO
updatelanguagecontext 'en','F_ModerateForum','Moderated Forum', 'Forum'
GO
updatelanguagecontext 'en','F_StatsMostPost','The thread with the most posts is', 'Forum'
GO
updatelanguagecontext 'en','F_Home','Forum home', 'Forum'
GO
updatelanguagecontext 'en','F_Image','Image', 'Forum'
GO
updatelanguagecontext 'en','F_ImgDir','Images directory', 'Forum'
GO
updatelanguagecontext 'en','F_LastModified','(Modified by {author} the {datetime})', 'Forum'
GO
updatelanguagecontext 'en','F_ListOf_F','Forum lists', 'Forum'
GO
updatelanguagecontext 'en','F_lnkProfile','See profile', 'Forum'
GO
updatelanguagecontext 'en','F_LocalTime','Local time', 'Forum'
GO
updatelanguagecontext 'en','F_LPost','Last message', 'Forum'
GO
updatelanguagecontext 'en','F_MaxImage','The maximum image size must be between {maxHeight} x {maxWidth} pixels.', 'Forum'
GO
updatelanguagecontext 'en','F_Message','Post', 'Forum'
GO
updatelanguagecontext 'en','F_ModerateAdmin','Admin. moderated forum', 'Forum'
GO
updatelanguagecontext 'en','F_Moderated','Moderated?', 'Forum'
GO
updatelanguagecontext 'en','F_ModerateEMail','E-Mail', 'Forum'
GO
updatelanguagecontext 'en','F_aAThread','Active', 'Forum'
GO
updatelanguagecontext 'en','F_aAThread0','Normal', 'Forum'
GO
updatelanguagecontext 'en','F_Active','Active', 'Forum'
GO
updatelanguagecontext 'en','F_Add','Add', 'Forum'
GO
updatelanguagecontext 'en','F_AddImo','Add smily', 'Forum'
GO
updatelanguagecontext 'en','F_AddNewForum','Add a new forum', 'Forum'
GO
updatelanguagecontext 'en','F_AddNewGroup','Add a new group', 'Forum'
GO
updatelanguagecontext 'en','F_AddNewThread','Add a new post', 'Forum'
GO
updatelanguagecontext 'en','F_AdminModerate','Admin moderated forum', 'Forum'
GO
updatelanguagecontext 'en','F_AdminParam','Forum settings', 'Forum'
GO
updatelanguagecontext 'en','F_AdminUser','Admin forum users', 'Forum'
GO
updatelanguagecontext 'en','F_AliasUsed','This alias is already used by another user', 'Forum'
GO
updatelanguagecontext 'en','F_Anonymous','Your not registered.', 'Forum'
GO
updatelanguagecontext 'en','F_AnonymousClick','You can register here', 'Forum'
GO
updatelanguagecontext 'en','F_Answer','Replies', 'Forum'
GO
updatelanguagecontext 'en','F_approve','Approuve', 'Forum'
GO
updatelanguagecontext 'en','F_Cancel','Cancel', 'Forum'
GO
updatelanguagecontext 'en','F_CheckMail','Send me an E-Mail if a Post is added', 'Forum'
GO
updatelanguagecontext 'en','F_ClickToModProfile','Click here to edit your profile', 'Forum'
GO
updatelanguagecontext 'en','F_Close','Close', 'Forum'
GO
updatelanguagecontext 'en','F_AvInfo2','You must create a photo gallery before you can integrate it the a forum.<br>Once the gallery created go to the gallery settings and check the <b>This is the avatars gallery?</b>', 'Forum'
GO
updatelanguagecontext 'en','F_cmdAdmin','Admin', 'Forum'
GO
updatelanguagecontext 'en','F_Down','Down', 'Forum'
GO
updatelanguagecontext 'en','F_Down1','Down one level', 'Forum'
GO
updatelanguagecontext 'en','F_AThread','Very busy thread!', 'Forum'
GO
updatelanguagecontext 'en','F_AThread0','Actif thread', 'Forum'
GO
updatelanguagecontext 'en','F_Audit','Audit', 'Forum'
GO
updatelanguagecontext 'en','F_Auteur','Author', 'Forum'
GO
updatelanguagecontext 'en','F_AuthRoles','Autorization', 'Forum'
GO
updatelanguagecontext 'en','F_Avatar','Avatar', 'Forum'
GO
updatelanguagecontext 'en','F_AvDir','Avatars directory', 'Forum'
GO
updatelanguagecontext 'en','F_AvID','ID Avatars Module', 'Forum'
GO
updatelanguagecontext 'en','F_AvInfo','Click here to configure the avatar gallery!', 'Forum'
GO
updatelanguagecontext 'en','F_AvInfo1','automatic configuration of the avatars gallery...', 'Forum'
GO
updatelanguagecontext 'en','F_AvMaxS','Max size Avatar', 'Forum'
GO
updatelanguagecontext 'en','F_AvYes','Allow Avatars?', 'Forum'
GO
updatelanguagecontext 'en','F_Bad_Ext','Only those file type {fileext} are accepted.', 'Forum'
GO
updatelanguagecontext 'en','Search_SelectAllForum','Select all', 'Forum'
GO
updatelanguagecontext 'en','F_FileNameUsed','The file name is already used, please chose another one', 'Forum'
GO
updatelanguagecontext 'en','F_FlatView','Flat view', 'Forum'
GO
updatelanguagecontext 'en','F_ForumAdmin','Admin. forum', 'Forum'
GO
updatelanguagecontext 'en','F_ForumFormat','Forum format settings', 'Forum'
GO
updatelanguagecontext 'en','F_ForumGroup','Group', 'Forum'
GO
updatelanguagecontext 'en','F_ForumGroupS','Forum group', 'Forum'
GO
updatelanguagecontext 'en','F_ForumID','ID Forum', 'Forum'
GO
updatelanguagecontext 'en','F_ForumName','Forum name', 'Forum'
GO
updatelanguagecontext 'en','F_ForumSearch','SearchForums', 'Forum'
GO
updatelanguagecontext 'en','F_ForumSetting','Global Forum settings', 'Forum'
GO
updatelanguagecontext 'en','F_From','From', 'Forum'
GO
updatelanguagecontext 'en','F_GalHeight','Image height', 'Forum'
GO
updatelanguagecontext 'en','F_GalImgT','Image type', 'Forum'
GO
updatelanguagecontext 'en','F_GalInt','Integrated gallery settings', 'Forum'
GO
updatelanguagecontext 'en','F_GalIntInfo','(Use the gallery settings)', 'Forum'
GO
updatelanguagecontext 'en','F_GalleryParam','Gallery settings', 'Forum'
GO
updatelanguagecontext 'en','F_GalleryParam1','use the gallery settings', 'Forum'
GO
updatelanguagecontext 'en','F_GalleryParam2','Gallery integration?', 'Forum'
GO
updatelanguagecontext 'en','F_GalleryParam3','ID gallery', 'Forum'
GO
updatelanguagecontext 'en','F_GalleryParam4','Album name', 'Forum'
GO
updatelanguagecontext 'en','F_GalWith','Image width', 'Forum'
GO
updatelanguagecontext 'en','F_GoToPMS','Go to private messages module', 'Forum'
GO
updatelanguagecontext 'en','F_GotPMS','No new private messages', 'Forum'
GO
updatelanguagecontext 'en','F_Group','Forum', 'Forum'
GO
updatelanguagecontext 'en','F_GroupAdmin','Admin forum', 'Forum'
GO
updatelanguagecontext 'en','F_GroupID','ID Group', 'Forum'
GO
updatelanguagecontext 'en','F_GroupName','Group name', 'Forum'
GO
updatelanguagecontext 'en','F_Groups','Forums', 'Forum'
GO
updatelanguagecontext 'en','F_Edit','Edit', 'Forum'
GO
updatelanguagecontext 'en','F_EditMode','Edit mode', 'Forum'
GO
updatelanguagecontext 'en','F_EditPost','Edit the post', 'Forum'
GO
updatelanguagecontext 'en','F_EditYPost','Edit your post', 'Forum'
GO
updatelanguagecontext 'en','F_EditYPostA','Edit', 'Forum'
GO
updatelanguagecontext 'en','F_EMailAdd','E-Mail Addresse', 'Forum'
GO
updatelanguagecontext 'en','F_EMailAut','Automatic E-Mail', 'Forum'
GO
updatelanguagecontext 'en','F_EMailDom','Domain name', 'Forum'
GO
updatelanguagecontext 'en','F_EMailFormat','E-Mail Format', 'Forum'
GO
updatelanguagecontext 'en','F_EMailInfo','(The system can automatically send an E-Mail to users when a new message is posted)', 'Forum'
GO
updatelanguagecontext 'en','F_EMailInfo1','Allow the forum to automatically send an E-Mail notice to users when a post is added or modified.', 'Forum'
GO
updatelanguagecontext 'en','F_EMailInfo2','Show the E-Mail address in the <b>to:</b> field or use the automatic address:', 'Forum'
GO
updatelanguagecontext 'en','F_EMailS','E-Mail notice?', 'Forum'
GO
updatelanguagecontext 'en','F_EMailYes','E-Mail notice settings', 'Forum'
GO
updatelanguagecontext 'en','F_ErasePost','Erase post', 'Forum'
GO
updatelanguagecontext 'en','F_Expand','Open', 'Forum'
GO
updatelanguagecontext 'en','F_Pinned','Pinned', 'Forum'
GO
updatelanguagecontext 'en','F_PinnedPost','Important Post', 'Forum'
GO
updatelanguagecontext 'en','F_PModified','Post was modified', 'Forum'
GO
updatelanguagecontext 'en','F_PModifiedA','Modified', 'Forum'
GO
updatelanguagecontext 'en','F_PMSCount','You have {pmscount} messages in your inbox', 'Forum'
GO
updatelanguagecontext 'en','F_PMSShort','Short cut', 'Forum'
GO
updatelanguagecontext 'en','F_Post','Posts', 'Forum'
GO
updatelanguagecontext 'en','F_PostDate','Date', 'Forum'
GO
updatelanguagecontext 'en','F_Posted','Posted:', 'Forum'
GO
updatelanguagecontext 'en','F_PostFrom','Post from', 'Forum'
GO
updatelanguagecontext 'en','F_PostPage','Post per page', 'Forum'
GO
updatelanguagecontext 'en','F_PostReply','Answer to the post', 'Forum'
GO
updatelanguagecontext 'en','F_PostReplyA','Answer', 'Forum'
GO
updatelanguagecontext 'en','F_PostSubject','Subject:', 'Forum'
GO
updatelanguagecontext 'en','F_Prev','Previous', 'Forum'
GO
updatelanguagecontext 'en','F_Preview','Post preview', 'Forum'
GO
updatelanguagecontext 'en','F_Private','Private?', 'Forum'
GO
updatelanguagecontext 'en','F_QuotePost','Quote this post', 'Forum'
GO
updatelanguagecontext 'en','F_QuotePostA','Quote', 'Forum'
GO
updatelanguagecontext 'en','F_RE','Re:', 'Forum'
GO
updatelanguagecontext 'en','F_NewMessages','New message(s)', 'Forum'
GO
updatelanguagecontext 'en','F_NewMessages0','New', 'Forum'
GO
updatelanguagecontext 'en','F_NewMP','New message in <b>{forum}</b>', 'Forum'
GO
updatelanguagecontext 'en','F_NewPost','New thread', 'Forum'
GO
updatelanguagecontext 'en','F_NewPostA','New', 'Forum'
GO
updatelanguagecontext 'en','F_Next','Next', 'Forum'
GO
updatelanguagecontext 'en','F_NoAvrRep','It was impossible to find the directory for the avatars, this option is not available', 'Forum'
GO
updatelanguagecontext 'en','F_NoConfig','It was impossible to find the settings for the avatars gallery!', 'Forum'
GO
updatelanguagecontext 'en','F_NoGal','The Gallery is not setup for this forum!', 'Forum'
GO
updatelanguagecontext 'en','F_NoGalR','The gallery directory cannot be found, this option is not available', 'Forum'
GO
updatelanguagecontext 'en','F_NoMessage','No message in the thread', 'Forum'
GO
updatelanguagecontext 'en','F_none','None', 'Forum'
GO
updatelanguagecontext 'en','F_NoNewMessage','No new message', 'Forum'
GO
updatelanguagecontext 'en','F_NoNewMessage0','No new', 'Forum'
GO
updatelanguagecontext 'en','F_NoPost','No post', 'Forum'
GO
updatelanguagecontext 'en','F_NoSpaceLeft','There is not space left, please contact the webmaster.', 'Forum'
GO
updatelanguagecontext 'en','F_NotAvailable','Not available at this time use a simple search', 'Forum'
GO
updatelanguagecontext 'en','F_NoThread','No thread', 'Forum'
GO
updatelanguagecontext 'en','F_NoUser','No user found!', 'Forum'
GO
updatelanguagecontext 'en','F_NoUserFound','The user does not exist please select another one!', 'Forum'
GO
updatelanguagecontext 'en','F_NumberOf_F','Number of forum', 'Forum'
GO
updatelanguagecontext 'en','F_Object','Subject', 'Forum'
GO
updatelanguagecontext 'en','F_Page','Page:', 'Forum'
GO
updatelanguagecontext 'en','F_Paging','Page {pagenum} of {numpage}', 'Forum'
GO
updatelanguagecontext 'en','F_ReturnMod','Return to the moderator admin page...', 'Forum'
GO
updatelanguagecontext 'en','F_RNotice','Mail notice', 'Forum'
GO
updatelanguagecontext 'en','F_Saving','Wait I am saving your POST!!!!', 'Forum'
GO
updatelanguagecontext 'en','F_SBy','Started by', 'Forum'
GO
updatelanguagecontext 'en','F_Search','Search all forum', 'Forum'
GO
updatelanguagecontext 'en','F_Search1','Search from here', 'Forum'
GO
updatelanguagecontext 'en','F_SearchResult','Search result', 'Forum'
GO
updatelanguagecontext 'en','F_SeeDetails','See details ...', 'Forum'
GO
updatelanguagecontext 'en','F_Seen','Vues', 'Forum'
GO
updatelanguagecontext 'en','F_SeeProfileof','See profile of', 'Forum'
GO
updatelanguagecontext 'en','F_Select','Select a forum', 'Forum'
GO
updatelanguagecontext 'en','F_SelectAction','<Select an action>', 'Forum'
GO
updatelanguagecontext 'en','F_SelectAvatar','<Select Avatar>', 'Forum'
GO
updatelanguagecontext 'en','F_SelectIcon','Select a smiley', 'Forum'
GO
updatelanguagecontext 'en','F_SendEMailTo','Send a message to', 'Forum'
GO
updatelanguagecontext 'en','F_SendEMailToUser','Send an E-Mail to', 'Forum'
GO
updatelanguagecontext 'en','F_SendPMSTo','Write a message to', 'Forum'
GO
updatelanguagecontext 'en','F_Since','Since', 'Forum'
GO
updatelanguagecontext 'en','F_Skin','Skin', 'Forum'
GO
updatelanguagecontext 'en','F_Stats','Update stats', 'Forum'
GO
updatelanguagecontext 'en','F_Stats24','In the last 24 hours', 'Forum'
GO
updatelanguagecontext 'en','F_StatsForum','Forum Stats', 'Forum'
GO
updatelanguagecontext 'en','F_StatsH','(hour)', 'Forum'
GO
updatelanguagecontext 'en','F_StatsMost','The most vue thread is', 'Forum'
GO
updatelanguagecontext 'en','F_CountForumGroup','{forumscount} forums in {groupscount} forums groups', 'Forum'
GO
updatelanguagecontext 'en','F_Created','Created the', 'Forum'
GO
updatelanguagecontext 'en','F_CreatedBy','Created by', 'Forum'
GO
updatelanguagecontext 'en','F_CreatedOn','the', 'Forum'
GO
updatelanguagecontext 'en','F_UOError','It was impossible to find the info the Users Online.', 'Forum'
GO
updatelanguagecontext 'en','F_UOInt','User Online integration', 'Forum'
GO
updatelanguagecontext 'en','F_UOuse','Use User Online?', 'Forum'
GO
updatelanguagecontext 'en','F_Up','Up', 'Forum'
GO
updatelanguagecontext 'en','F_Up1','Up one level', 'Forum'
GO
updatelanguagecontext 'en','F_ModerateNotice','E-Mail notice', 'Forum'
GO
updatelanguagecontext 'en','F_Moderators','Forum Moderators', 'Forum'
GO
updatelanguagecontext 'en','F_NeedF','You must select a forum before posting a new thread', 'Forum'
GO
updatelanguagecontext 'en','F_NeedObject','Need an object', 'Forum'
GO
updatelanguagecontext 'en','F_Deleted','The post was deleted by {authorName} the :', 'Forum'
GO
updatelanguagecontext 'en','F_DeletedBy','The post : <b>{object}</b> was deleted by {authorName}', 'Forum'
GO
updatelanguagecontext 'en','F_Desc','Description', 'Forum'
GO
updatelanguagecontext 'en','F_Remove','Remove', 'Forum'
GO
updatelanguagecontext 'en','F_ImgSettings','Images settings', 'Forum'
GO
updatelanguagecontext 'en','Search_Alias','User name', 'Forum'
GO
updatelanguagecontext 'en','F_AdminAvatar','Avatars gallery', 'Forum'
GO
updatelanguagecontext 'en','select_avatar_tooltip','Go to and edit avatars gallery', 'Forum'
GO
updatelanguagecontext 'en','select_avatar','Avatars gallery', 'Forum'
GO
updatelanguagecontext 'en','F_VisitWWW','WWW', 'Forum'
GO
updatelanguagecontext 'en','Gal_AgAL','This is the avatars gallery?', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Items','Items', 'Gallery'
GO
updatelanguagecontext 'en','Gal_KB','(KB)', 'Gallery'
GO
updatelanguagecontext 'en','Gal_lnkshow','See diaporama...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Loading','Loading the diaporama ......', 'Gallery'
GO
updatelanguagecontext 'en','Gal_MaxFile','Max file size', 'Gallery'
GO
updatelanguagecontext 'en','Gal_MaxFileSize','The file size must be less than {MaxFileSize} (KB)', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ModA','Edit album', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ModG','Edit gallery', 'Gallery'
GO
updatelanguagecontext 'en','Gal_MThumbsH','Max thumb hight', 'Gallery'
GO
updatelanguagecontext 'en','Gal_MThumbsW','Max thumb width', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Name','Name', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Next','See next image ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Next0','&raquo;', 'Gallery'
GO
updatelanguagecontext 'en','Gal_NoConfig','The gallery is not setup yet!', 'Gallery'
GO
updatelanguagecontext 'en','Gal_NoFile','There is no file!', 'Gallery'
GO
updatelanguagecontext 'en','Gal_NoImage','There is not image in the album', 'Gallery'
GO
updatelanguagecontext 'en','Gal_NoSpaceLeft','You do not have any disk space left to upload this file.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Page','Page:', 'Gallery'
GO
updatelanguagecontext 'en','Gal_PopDia','POPup diaporama?', 'Gallery'
GO
updatelanguagecontext 'en','Gal_PortalQuota','Portal space ( Used: <b>{SpaceUsed} MB</b> )', 'Gallery'
GO
updatelanguagecontext 'en','Gal_PortalQuota1','Portal space ( Maximum: <b>{Quota}MB</b>  Used: <b>{SpaceUsed}MB</b>  Available: <b>{SpaceLeft}MB</b>', 'Gallery'
GO
updatelanguagecontext 'en','Gal_PortalQuota2','<br>You do not have any disk space left to upload new files.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Prev','See previous image ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Prev0','&laquo;', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Private','Private gallery', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Prop','Owner', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Quota','Quota in (MB)', 'Gallery'
GO
updatelanguagecontext 'en','Gal_QuotaInfo','Album space ( Used: <b>{SpaceUsed} MB</b> )', 'Gallery'
GO
updatelanguagecontext 'en','Gal_QuotaInfo1','Album space ( Maximum: <b>{Quota}MB</b>  Used: <b>{SpaceUsed}MB</b>  Available: <b>{SpaceLeft}MB</b>', 'Gallery'
GO
updatelanguagecontext 'en','Gal_QuotaInfo2','<br>You do not have any disk space left for this Album.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_QuotaTip','Album quota in (MB), 0=no limit', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Refuse','This is a private gallery.  Only the owner of the gallery can edit it. <br>You may contact the webmaster for further information.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SaveA','Save album', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Select','Select', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SelectA','<Choisir album>', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SelectF','<Choisir Forum>', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SelectProp','Select owner', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SelF','Select a forum group', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SetUp','Settings', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SetUp1','Edit Albums/Images', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SetUpAdmin','Admin settings', 'Gallery'
GO
updatelanguagecontext 'en','Gal_show','See this album in diaporama ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_showLink','Diaporama', 'Gallery'
GO
updatelanguagecontext 'en','Gal_SOwner','Select the gallery owner ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ThumbsH','# thumbs high', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ThumbsW','# thumbs width', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Title','Gallery title', 'Gallery'
GO
updatelanguagecontext 'en','Gal_TitleI','Title', 'Gallery'
GO
updatelanguagecontext 'en','Gal_TxtOp','Text format options', 'Gallery'
GO
updatelanguagecontext 'en','Gal_UpdateConf','Update the configuration...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_UpdateTip','Save modifications and return to the photo album', 'Gallery'
GO
updatelanguagecontext 'en','Gal_URL','URL', 'Gallery'
GO
updatelanguagecontext 'en','Gal_UserParam','Gallery users settings', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Album','Album', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AlbumAdd','The new album was added.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AlbumExist','The album already exist!  Please use another name.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AlbumNo','Only use letter in the directory name.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AllowD','Allow uploads?', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Browser','See this album in browsing mode ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_BrowserLink','Browser', 'Gallery'
GO
updatelanguagecontext 'en','Gal_btnAddAlt','Add', 'Gallery'
GO
updatelanguagecontext 'en','Gal_btnAddTip','Add this file to the upload list ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_CacheGen','Creating the gallery ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_CacheRed','If this page does not redirect you automtically,', 'Gallery'
GO
updatelanguagecontext 'en','Gal_CacheS','Put the gallery in memory cashes.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Cancel','Cancel without saving and return to the album.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_CancelTip','Return to the album without saving.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Cat','Cathegory', 'Gallery'
GO
updatelanguagecontext 'en','Gal_CatV','Category', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Clear','Refresh the gallery ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Click','Click here ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Cont','Click to continue ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_DefaultCategory','Image;Film;Musique;Flash', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Desc','Description', 'Gallery'
GO
updatelanguagecontext 'en','Gal_DiaS','Diaporama speed', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Dim','Dimension', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Dis','Discuss in forum...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Edit','Edit ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_EditFile','Edit file', 'Gallery'
GO
updatelanguagecontext 'en','Gal_EditRes','Edit Icon ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ErrorDir','The directory must be within your portal.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ErrorF','You must make sure that a forum group and a forum is created before attempting to integrate a gallery.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ErrorSelectAF','You have to select a forum and an album for the integration.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ErrorSelectL','Select an album from the list.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ExtM','Films extensions', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ExtVF','Files extensions', 'Gallery'
GO
updatelanguagecontext 'en','Gal_FFilm','Film', 'Gallery'
GO
updatelanguagecontext 'en','Gal_FFlash','Flash file', 'Gallery'
GO
updatelanguagecontext 'en','Gal_File','File', 'Gallery'
GO
updatelanguagecontext 'en','Gal_FileInfo','<FONT color=#000080>
* Zip<br>
* Image: {fileext}<br>
* Movie: {movieext}<br>
* Flash: .swf <br>
* Max Size: {MaxFileSize} KB <br>
</FONT>', 'Gallery'
GO
updatelanguagecontext 'en','Gal_FileInfo1','<FONT color=#000080>
There are <b>{FileItemsCount}</b> files <b>{SubAlbumItems}</b> sub-albums in this directory.</FONT><br>', 'Gallery'
GO
updatelanguagecontext 'en','Gal_FileOE','Error: It was impossible to open the item.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_FileOEN','There is already a file with the same name!  Please try to upload again with a new name.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_FileType','File size', 'Gallery'
GO
updatelanguagecontext 'en','Gal_FileUpInfo','Files to add, Click <b>upload</b> button to complete.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgBaseUrl','Base URL (images)', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgChangeSize','Edit the image size', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgInfo','Image {imgnum} of {albumnum} {imgname} ({imgsize}KB)', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgKeepP','Keep private?', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgKeepS','Keep source?', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgMaxH','Max hight', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgMaxW','Max width', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgQual','Quality', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgQualInfo','Will only compress jpg file, 20 = smaller file size 80 = larger file size', 'Gallery'
GO
updatelanguagecontext 'en','Gal_ImgRatioInfo','The image will be saved with the same aspect ratio than the original.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_InfoPre','Showing {start} - {end} of {total} item(s) total', 'Gallery'
GO
updatelanguagecontext 'en','Gal_IntF','Integrate the album to the forum?', 'Gallery'
GO
updatelanguagecontext 'en','Gal_IntFF','Integrate', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Invalid_FileType','The file type is not valid.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Add','Add', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AddF','Add an album to : {folderURL}', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AddFF','Add some files to the album : {folderURL}', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AddFile','Add file', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AddFileTip','Click here to add a new file to the album.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AddFolder','Add Album', 'Gallery'
GO
updatelanguagecontext 'en','Gal_AddFolderTip','Click here to add a new directory to the album.', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Admin','Gallery configuration settings ...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_Admin1','Add an image/album...', 'Gallery'
GO
updatelanguagecontext 'en','Gal_MaxFileKB','The file <b>{FileName}</b> you try to upload is {FileSize} (KB) but the maximum allowed is {MaxFileSize} (KB).', 'Gallery'
GO
updatelanguagecontext 'en','help','Help', 'Global'
GO
updatelanguagecontext 'en','True','True', 'Global'
GO
updatelanguagecontext 'en','login','Login', 'Global'
GO
updatelanguagecontext 'en','no_module_info','There is not info available on this module.', 'Global'
GO
updatelanguagecontext 'en','Membership_Serv','Membership services', 'Global'
GO
updatelanguagecontext 'en','module_info','Module Info :', 'Global'
GO
updatelanguagecontext 'en','error','Error', 'Global'
GO
updatelanguagecontext 'en','list_none','<None>', 'Global'
GO
updatelanguagecontext 'en','False','False', 'Global'
GO
updatelanguagecontext 'en','exit','Exit', 'Global'
GO
updatelanguagecontext 'en','*N','N', 'Global'
GO
updatelanguagecontext 'en','*O','Y', 'Global'
GO
updatelanguagecontext 'en','need_help','Need help', 'Global'
GO
updatelanguagecontext 'en','all','all', 'Global'
GO
updatelanguagecontext 'en','MapGoogle_big','Large', 'GoogleMap'
GO
updatelanguagecontext 'en','MapGoogle_small','Small', 'GoogleMap'
GO
updatelanguagecontext 'en','MapGoogle_zoom','Zoom', 'GoogleMap'
GO
updatelanguagecontext 'en','MapGoogle_size','Size', 'GoogleMap'
GO
updatelanguagecontext 'en','MapGoogle_directions_tooltip','Go to Google Map', 'GoogleMap'
GO
updatelanguagecontext 'en','MapGoogle_directions','Go to Google map to find directions', 'GoogleMap'
GO
updatelanguagecontext 'en','GetGoogleAPI','Get Google API', 'GoogleMap'
GO
updatelanguagecontext 'en','GoogleAPI','Google API', 'GoogleMap'
GO
updatelanguagecontext 'en','GoogleDisplayPointer','Display Pointer', 'GoogleMap'
GO
updatelanguagecontext 'en','GoogleDisplayResize','Display Resize', 'GoogleMap'
GO
updatelanguagecontext 'en','GoogleDisplayType','Display Type', 'GoogleMap'
GO
updatelanguagecontext 'en','GoogleGenerateLatLong','Generate Lat Long', 'GoogleMap'
GO
updatelanguagecontext 'en','GoogleGenerateScript','Generate  Script', 'GoogleMap'
GO
updatelanguagecontext 'en','GoogleLatLong','Lat Long', 'GoogleMap'
GO
updatelanguagecontext 'en','MapGoogle_location','Map title', 'GoogleMap'
GO
updatelanguagecontext 'en','MapGoogle_show_map_edit','Display edit', 'GoogleMap'
GO
updatelanguagecontext 'en','need_API_MapGoogle','You need an API from GoogleMap', 'GoogleMap'
GO
updatelanguagecontext 'en','need_LatLong_MapGoogle','A Lat and Long need to be entered', 'GoogleMap'
GO
updatelanguagecontext 'en','need_Script_MapGoogle','A script need to be generated or entered', 'GoogleMap'
GO
updatelanguagecontext 'en','need_location_MapGoogle','Where is the map from?', 'GoogleMap'
GO
updatelanguagecontext 'en','HS_EnableSSLInfo','Will force SSL on certain page like login', 'HostSettings'
GO
updatelanguagecontext 'en','HS_EnableSSL','Force SSL', 'HostSettings'
GO
updatelanguagecontext 'en','MailSend','the E-Mail was send', 'HostSettings'
GO
updatelanguagecontext 'en','ViewState_memory','Memory', 'HostSettings'
GO
updatelanguagecontext 'en','ViewState_SQL','SQL', 'HostSettings'
GO
updatelanguagecontext 'en','WhiteSpace_Only','White Space', 'HostSettings'
GO
updatelanguagecontext 'en','WhiteSpaceHTML','HTML', 'HostSettings'
GO
updatelanguagecontext 'en','WhiteSpace_ALL','White Space and HTML', 'HostSettings'
GO
updatelanguagecontext 'en','HS_Currency','Currency', 'HostSettings'
GO
updatelanguagecontext 'en','HS_DemoAllow','Allow the creation of a demo portal?', 'HostSettings'
GO
updatelanguagecontext 'en','HS_DemoPeriod','Demo period in (days)', 'HostSettings'
GO
updatelanguagecontext 'en','HS_DNZVersion','Deactivate showing DotNetZoom version?', 'HostSettings'
GO
updatelanguagecontext 'en','HS_EmailHost','Host E-Mail address', 'HostSettings'
GO
updatelanguagecontext 'en','HS_ErrorReporting','Send error by E-Mail?', 'HostSettings'
GO
updatelanguagecontext 'en','HS_ErrorReportingInfo','(send error messages to the Host E-Mail address)', 'HostSettings'
GO
updatelanguagecontext 'en','HS_ext_upload','File extension allow for Upload', 'HostSettings'
GO
updatelanguagecontext 'en','HS_GO_Processor','Go to payment processor site', 'HostSettings'
GO
updatelanguagecontext 'en','HS_HostFee','Hosting fee', 'HostSettings'
GO
updatelanguagecontext 'en','HS_HostName','Hosting site title', 'HostSettings'
GO
updatelanguagecontext 'en','HS_HTMLClean','Web page size reduction', 'HostSettings'
GO
updatelanguagecontext 'en','HS_LogSQL','See SQL update log', 'HostSettings'
GO
updatelanguagecontext 'en','HS_Need_Email','<br>you must have a E-Mail address for the host.', 'HostSettings'
GO
updatelanguagecontext 'en','HS_NoLog','The SQL script was not executed for this version.', 'HostSettings'
GO
updatelanguagecontext 'en','HS_Paiement','Payment method', 'HostSettings'
GO
updatelanguagecontext 'en','HS_PaiementCode','Access Code payment processor', 'HostSettings'
GO
updatelanguagecontext 'en','HS_PaiementPassword','Password payment processor', 'HostSettings'
GO
updatelanguagecontext 'en','HS_PassordCrypto','Cryptographic Password Key', 'HostSettings'
GO
updatelanguagecontext 'en','HS_PortalSpace','Space&nbsp;( MB)', 'HostSettings'
GO
updatelanguagecontext 'en','HS_Proxy','Proxy Server', 'HostSettings'
GO
updatelanguagecontext 'en','HS_ProxyPort','Proxy Port', 'HostSettings'
GO
updatelanguagecontext 'en','HS_SiteLog','Site log (days)', 'HostSettings'
GO
updatelanguagecontext 'en','HS_SMTP','SMTP Server settings', 'HostSettings'
GO
updatelanguagecontext 'en','HS_SMTP_Code','User Code', 'HostSettings'
GO
updatelanguagecontext 'en','HS_SMTP_Password','Password', 'HostSettings'
GO
updatelanguagecontext 'en','HS_SMTP_Server','SMTP server', 'HostSettings'
GO
updatelanguagecontext 'en','HS_test_email','Test E-Mail configuration', 'HostSettings'
GO
updatelanguagecontext 'en','HS_TimeHost','Server Time', 'HostSettings'
GO
updatelanguagecontext 'en','HS_TimeZoneHost','Server TimeZone', 'HostSettings'
GO
updatelanguagecontext 'en','HS_URLHost','Host URL', 'HostSettings'
GO
updatelanguagecontext 'en','HS_ViewState','ViewState settings', 'HostSettings'
GO
updatelanguagecontext 'en','html','html', 'HTML'
GO
updatelanguagecontext 'en','HTML_ADDtxt','Add some text ...', 'HTML'
GO
updatelanguagecontext 'en','text','text', 'HTML'
GO
updatelanguagecontext 'en','SearchSummary','Search Summary', 'HTML'
GO
updatelanguagecontext 'en','AlternateDetailSummary','Key words', 'HTML'
GO
updatelanguagecontext 'en','ForSearchHTML','Information summary for use in the seach module', 'HTML'
GO
updatelanguagecontext 'en','no','no', 'Iframe'
GO
updatelanguagecontext 'en','auto','auto', 'Iframe'
GO
updatelanguagecontext 'en','iframe_border','Border', 'Iframe'
GO
updatelanguagecontext 'en','iframe_height','Height', 'Iframe'
GO
updatelanguagecontext 'en','iframe_scroll','Scroll', 'Iframe'
GO
updatelanguagecontext 'en','iframe_source','Source', 'Iframe'
GO
updatelanguagecontext 'en','iframe_title','Title', 'Iframe'
GO
updatelanguagecontext 'en','iframe_width','Width', 'Iframe'
GO
updatelanguagecontext 'en','browser_non_compatible','It was not possible to show this page since your web explorer is not compatible.', 'Iframe'
GO
updatelanguagecontext 'en','yes','yes', 'Iframe'
GO
updatelanguagecontext 'en','image_alt_text','Alternate text', 'Image'
GO
updatelanguagecontext 'en','image_artist','Artist', 'Image'
GO
updatelanguagecontext 'en','image_copyright','Copyright', 'Image'
GO
updatelanguagecontext 'en','image_description','Description', 'Image'
GO
updatelanguagecontext 'en','image_external','External image', 'Image'
GO
updatelanguagecontext 'en','image_height','Height', 'Image'
GO
updatelanguagecontext 'en','image_iternal','Internal image', 'Image'
GO
updatelanguagecontext 'en','image_select_external','Select an external image', 'Image'
GO
updatelanguagecontext 'en','image_select_iternal','Select an external image', 'Image'
GO
updatelanguagecontext 'en','image_title','Title', 'Image'
GO
updatelanguagecontext 'en','image_width','Width', 'Image'
GO
updatelanguagecontext 'en','ImageDeleted','Image {imagename} was erased', 'ImageManager'
GO
updatelanguagecontext 'en','ImageDeleteError','There is an error!', 'ImageManager'
GO
updatelanguagecontext 'en','ImageMaxSize','The quality setting must be between 20-80 and the maximum image size is 200px', 'ImageManager'
GO
updatelanguagecontext 'en','ImageQualityRange','Must be between 20 to 80', 'ImageManager'
GO
updatelanguagecontext 'en','IconesFrom','icon from :', 'ImageManager'
GO
updatelanguagecontext 'en','IconesTo','to :', 'ImageManager'
GO
updatelanguagecontext 'en','UploadSuccessMessage','Upload succesfull', 'ImageManager'
GO
updatelanguagecontext 'en','NoFileMessage','No file selected', 'ImageManager'
GO
updatelanguagecontext 'en','NoFileToDeleteMessage','No file to erase', 'ImageManager'
GO
updatelanguagecontext 'en','NoFolderFoundMessage','Error, it was impossible to find the image directory', 'ImageManager'
GO
updatelanguagecontext 'en','NoFolderSpecifiedMessage','No directory', 'ImageManager'
GO
updatelanguagecontext 'en','NoImagesMessage','No image', 'ImageManager'
GO
updatelanguagecontext 'en','NoSpaceLeft','You do not have enough disk space left', 'ImageManager'
GO
updatelanguagecontext 'en','error_panel_title','Error', 'ImageManager'
GO
updatelanguagecontext 'en','ErrorGDI','Error, this is not an image, or the file selected is not a valid image.', 'ImageManager'
GO
updatelanguagecontext 'en','InvalidFileTypeMessage','The file type is not valid, you can only use the following file type: jpg gif png tif or bmp', 'ImageManager'
GO
updatelanguagecontext 'en','InvalidFileTypeMessage2','The file type is not valide, you can only use GIF or JPG', 'ImageManager'
GO
updatelanguagecontext 'en','ImageRangeTo','Must be between 20px to 200px max.', 'ImageManager'
GO
updatelanguagecontext 'en','ImageRangeToSave','Must be between 10 to 200', 'ImageManager'
GO
updatelanguagecontext 'en','ImageRangeToSave800','Must be between 10 to 800', 'ImageManager'
GO
updatelanguagecontext 'en','MagicFileInfo','You can personalize the appearance of your directory and your video files.  You can assign an image to a directory to replace the default icon by uploading an image gif or jpg.', 'ImageManager'
GO
updatelanguagecontext 'en','MagicFileInfo1','Please use a good quality image betwenn a minimum of 20px to a maximum of 200px.', 'ImageManager'
GO
updatelanguagecontext 'en','MagicFileMessage','Default icon will be use<br>Otherwise upload a new one', 'ImageManager'
GO
updatelanguagecontext 'en','MagicFileMessage1','The file was change, It is possible that your will not see a difference.  If so <b><font color=""#ff0000"">purge you web browser cache</font></b>.', 'ImageManager'
GO
updatelanguagecontext 'en','MagicFileMessage2','', 'ImageManager'
GO
updatelanguagecontext 'en','LabelMaxHeightImage','Max image height :', 'ImageManager'
GO
updatelanguagecontext 'en','LabelMaxWImage','Max image width :', 'ImageManager'
GO
updatelanguagecontext 'en','LabelQuaImage','Image quality :', 'ImageManager'
GO
updatelanguagecontext 'en','LabelUploadDeleteCancel','Upload a new icon, erase or return :', 'ImageManager'
GO
updatelanguagecontext 'en','LabelUploadDeleteCancel1','Upload a new image, erase or return :', 'ImageManager'
GO
updatelanguagecontext 'en','ImageRangeFrom','Must be between 5px to 180px max.', 'ImageManager'
GO
updatelanguagecontext 'en','LanguageSettings1','Edit - Erase- Create', 'Language'
GO
updatelanguagecontext 'en','language','English', 'Language'
GO
updatelanguagecontext 'en','language_add','Add an item', 'Language'
GO
updatelanguagecontext 'en','language_change','Edit', 'Language'
GO
updatelanguagecontext 'en','language_clicktoadd','Click here to add', 'Language'
GO
updatelanguagecontext 'en','language_clicktocreate','Click here to create a new language', 'Language'
GO
updatelanguagecontext 'en','language_clicktodel','Click here to erase the language ->', 'Language'
GO
updatelanguagecontext 'en','language_clicktogenerate','Click here to create a script for the new language', 'Language'
GO
updatelanguagecontext 'en','language_context','Context', 'Language'
GO
updatelanguagecontext 'en','language_create','Create a script for language ', 'Language'
GO
updatelanguagecontext 'en','language_del','Erase a language', 'Language'
GO
updatelanguagecontext 'en','Language_Edit_Param','Language text', 'Language'
GO
updatelanguagecontext 'en','language_new','Add a new language', 'Language'
GO
updatelanguagecontext 'en','language_param','Param', 'Language'
GO
updatelanguagecontext 'en','language_see','See', 'Language'
GO
updatelanguagecontext 'en','language_value','Info', 'Language'
GO
updatelanguagecontext 'en','culturecode','en-US', 'Language'
GO
updatelanguagecontext 'en','btnLanguageEdit','Edit only new language text', 'Language'
GO
updatelanguagecontext 'en','btnLanguageEditAll','Edit all language text', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings10','Time Zones', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings2','Help settings', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings3','Language settings', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings4','Country codes', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings5','Admin menu', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings6','Currencies codes', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings7','Frequencies codes', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings8','Site log', 'Language'
GO
updatelanguagecontext 'en','LanguageSettings9','Region codes', 'Language'
GO
updatelanguagecontext 'en','ScriptGenerated','SQL Script created:', 'Language'
GO
updatelanguagecontext 'en','LanguageERROR','The following word - {0} - is missing from the language table {1}', 'Language'
GO
updatelanguagecontext 'en','select_internal_link','Select an internal link', 'Links'
GO
updatelanguagecontext 'en','select_external_link','Select an external link', 'Links'
GO
updatelanguagecontext 'en','select_file_link','Select a file link', 'Links'
GO
updatelanguagecontext 'en','link_description','Description', 'Links'
GO
updatelanguagecontext 'en','link_new_window','Open in a new window?', 'Links'
GO
updatelanguagecontext 'en','link_vue_order','Order', 'Links'
GO
updatelanguagecontext 'en','links_info','...', 'Links'
GO
updatelanguagecontext 'en','links_info_tooltip','See more info!', 'Links'
GO
updatelanguagecontext 'en','links_label_list_format','List format', 'Links'
GO
updatelanguagecontext 'en','links_label_list_type','List type', 'Links'
GO
updatelanguagecontext 'en','links_label_see_info','Show the link information', 'Links'
GO
updatelanguagecontext 'en','links_options_dropdown','Drop down menu', 'Links'
GO
updatelanguagecontext 'en','links_options_horizontal','Horizontal', 'Links'
GO
updatelanguagecontext 'en','links_options_list','List', 'Links'
GO
updatelanguagecontext 'en','links_options_no','No', 'Links'
GO
updatelanguagecontext 'en','links_options_vertical','Vertical', 'Links'
GO
updatelanguagecontext 'en','links_options_yes','Yes', 'Links'
GO
updatelanguagecontext 'en','links_title','Title', 'Links'
GO
updatelanguagecontext 'en','internal_link','Internal link', 'Links'
GO
updatelanguagecontext 'en','file_link','File link', 'Links'
GO
updatelanguagecontext 'en','external_link','External link', 'Links'
GO
updatelanguagecontext 'en','links_label_CSSClass','CSS Class for link', 'Links'
GO
updatelanguagecontext 'en','RegisterMessage','Please enter your User Name', 'Login'
GO
updatelanguagecontext 'en','RegisterMessage1','The user code does not exist', 'Login'
GO
updatelanguagecontext 'en','RegisterMessage2','We send you an E-Mail with your password.', 'Login'
GO
updatelanguagecontext 'en','RegisterMessage3','Your password or User Name are not valid.  The password is case sensitive.', 'Login'
GO
updatelanguagecontext 'en','RegisterMessage4','Enter your validation code', 'Login'
GO
updatelanguagecontext 'en','RegisterMessage5','The code is not good', 'Login'
GO
updatelanguagecontext 'en','RegisterMessage6','It was not possible to send the requested information.', 'Login'
GO
updatelanguagecontext 'en','Label_enter','Enter', 'Login'
GO
updatelanguagecontext 'en','Label_Password','Password', 'Login'
GO
updatelanguagecontext 'en','Button_Enter','Enter', 'Login'
GO
updatelanguagecontext 'en','Button_EnterTooltip','Click here to enter.', 'Login'
GO
updatelanguagecontext 'en','Button_Password','Forgot my password', 'Login'
GO
updatelanguagecontext 'en','Button_PasswordTooltip','Click here to receive a password reminder by E-Mail.', 'Login'
GO
updatelanguagecontext 'en','Button_Register','Register', 'Login'
GO
updatelanguagecontext 'en','Button_RegisterTooltip','Click here to register.', 'Login'
GO
updatelanguagecontext 'en','Label_UserName','User Code', 'Login'
GO
updatelanguagecontext 'en','Label_ValidCode','Validation code', 'Login'
GO
updatelanguagecontext 'en','Login_Keep','Keep in memory', 'Login'
GO
updatelanguagecontext 'en','Signin_hide','Click here to close the window.', 'Login'
GO
updatelanguagecontext 'en','mapquest_big','Large', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_directions','Directions', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_directions_tooltip','Go to Map Quest.', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_location','Location', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_show_map','See map', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_show_map_edit','Show the map?', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_show_tooltip','Click here to show the map!', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_size','Size', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_small','Small', 'MapQuest'
GO
updatelanguagecontext 'en','mapquest_zoom','Zoom', 'MapQuest'
GO
updatelanguagecontext 'en','MS_Directives','*Directives', 'ModuleSetting'
GO
updatelanguagecontext 'en','MS_Module_Desc','Module description', 'ModuleSetting'
GO
updatelanguagecontext 'en','MS_EDIT_URL','Edit', 'ModuleSetting'
GO
updatelanguagecontext 'en','MS_HELP_URL','Help', 'ModuleSetting'
GO
updatelanguagecontext 'en','MS_Bonus','Bonus?', 'ModuleSetting'
GO
updatelanguagecontext 'en','MS_BonusTxt','Bonus', 'ModuleSetting'
GO
updatelanguagecontext 'en','ms_cache','Memory cache time (sec.)', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_cache_info','Module cache is recommanded for static module, like Text-HTML.', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_center','Center', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_close','Close', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_color','Colour', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_contener','Module SKIN', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_contener_default','Default?', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_contener_default_info','To make this the default SKIN for the web site.  This SKIN will be used by new module or module  without a SKIN defined.', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_contener_error1','You must have [MODULE] in the SKIN definition.', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_contener_error2','An HTML error was detected in the SKIN', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_contener_global','Global?', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_contener_global_info','This SKIN will replace all the modules SKIN.', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_contener_setup','Align', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_custom','Personalize?', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_all_users','All users', 'ModuleSettings'
GO
updatelanguagecontext 'en','MS_ModuleDescription','Description', 'ModuleSettings'
GO
updatelanguagecontext 'en','MS_ModuleName','Module name', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_move_to_tab','Move to tab', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_non_authorized','Anonymous users', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_none','None', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_open','Open', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_right','Right', 'ModuleSettings'
GO
updatelanguagecontext 'en','MS_Script','Installation Script', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_see','See', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_see_info','Will overide the page setting if différent', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_select_color','Select a Colour', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_SelectModuleSkin','Select a SKIN', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_show_title','Show Module Title?', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_showall_tabs','Show this module on all pages?', 'ModuleSettings'
GO
updatelanguagecontext 'en','MS_SRC_URL','Desktop', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_title','Title', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_icone','Icone', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_icone_info','You should use an icone of  20px X 20px maximum', 'ModuleSettings'
GO
updatelanguagecontext 'en','MS_IconeHelp','Max size recommanded is  20px X 20px', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_language','Language', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_left','Left', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_margin','Border', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_mod','Edit privileges', 'ModuleSettings'
GO
updatelanguagecontext 'en','ms_edit_contener','Edit Module SKIN', 'ModuleSettings'
GO
updatelanguagecontext 'en','Title_class','Title text Class', 'ModuleSettings'
GO
updatelanguagecontext 'en','TitleHeader_class','Title header Class', 'ModuleSettings'
GO
updatelanguagecontext 'en','ModuleWrapper','Exterior', 'ModuleSettings'
GO
updatelanguagecontext 'en','ModuleTitle','Title', 'ModuleSettings'
GO
updatelanguagecontext 'en','ModuleOnly','Interior', 'ModuleSettings'
GO
updatelanguagecontext 'en','registration_ok','Registering result', 'ModuleTitle'
GO
updatelanguagecontext 'en','privacy_declaration','PRIVACY STATEMENT', 'ModuleTitle'
GO
updatelanguagecontext 'en','PrivateMessages','Private Messages', 'ModuleTitle'
GO
updatelanguagecontext 'en','UserInfoModule','Users information', 'ModuleTitle'
GO
updatelanguagecontext 'en','Site_Help','Help', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_CacheModToolTip','To refresh and erase module memory caches.', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_CacheModToolTip0','To refresh and erase module memory caches.', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_CacheToolTip','To refresh and purge memory caches', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_Dec','Close', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_denied','Denied', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_DownToolTip','Move module down', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_enter','Enter', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_expired','Portal expired', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_HideInfo','Hide module information', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_Inc','Expand', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_LeftToolTip','Move left', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_manage_user_table','Manage user tables', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_ParamModif','Edit module settings', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_RightToolTip','Move to right', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_ShowInfo','Show module info', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_ShowParam','Module settings', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_sql','SQL', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_terms','TERMS OF USE', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_UpToolTip','Move up', 'ModuleTitle'
GO
updatelanguagecontext 'en','title_vendor_feedback','Vendor feedback', 'ModuleTitle'
GO
updatelanguagecontext 'en','HelpInfo','Help', 'ModuleTitle'
GO
updatelanguagecontext 'en','create_portal','Create portal', 'ModuleTitle'
GO
updatelanguagecontext 'en','MyBuddiesModule','My Buddies', 'ModuleTitle'
GO
updatelanguagecontext 'en','EditAccessDenied','Access Denied', 'ModuleTitle'
GO
updatelanguagecontext 'en','UserListModule','Membership list', 'ModuleTitle'
GO
updatelanguagecontext 'en','directory_from','Directory', 'PortalBanner'
GO
updatelanguagecontext 'en','label_banner_order','Order for the banner', 'PortalBanner'
GO
updatelanguagecontext 'en','label_banner_source','Source for the banner', 'PortalBanner'
GO
updatelanguagecontext 'en','label_banner_type','Type of banner', 'PortalBanner'
GO
updatelanguagecontext 'en','label_search','Search', 'PortalBanner'
GO
updatelanguagecontext 'en','label_see_comments','See comments?', 'PortalBanner'
GO
updatelanguagecontext 'en','Banner_Mail4','message', 'PortalBanner'
GO
updatelanguagecontext 'en','Banner_Mail5','Click here to go to the private message section', 'PortalBanner'
GO
updatelanguagecontext 'en','banner_register','Register', 'PortalBanner'
GO
updatelanguagecontext 'en','banners_host','Host', 'PortalBanner'
GO
updatelanguagecontext 'en','banners_portal','Portal', 'PortalBanner'
GO
updatelanguagecontext 'en','register_more_info','You must enter with you User Name and password. If you are already registered. <a class="CommandButton" href="{httplogin}">Enter</a> now. Otherwise, <a class="CommandButton" href="{httpregister}">Register.</a>', 'PortalBanner'
GO
updatelanguagecontext 'en','register_no','You cannot register to our service', 'PortalBanner'
GO
updatelanguagecontext 'en','Banner_Mail3','messages', 'PortalBanner'
GO
updatelanguagecontext 'en','allow_vendors_register','Allow vendors to register?', 'PortalBanner'
GO
updatelanguagecontext 'en','banner_ClickProfile','Click here to search for a user or modify your profile', 'PortalBanner'
GO
updatelanguagecontext 'en','Banner_Mail','you have', 'PortalBanner'
GO
updatelanguagecontext 'en','Banner_Mail0','new message', 'PortalBanner'
GO
updatelanguagecontext 'en','Banner_Mail1','new messages', 'PortalBanner'
GO
updatelanguagecontext 'en','Banner_Mail2','if you want to read your messages you need to click on this image', 'PortalBanner'
GO
updatelanguagecontext 'en','P_Alias','Alias', 'PortalInfo'
GO
updatelanguagecontext 'en','P_Disk_Space','Disk space', 'PortalInfo'
GO
updatelanguagecontext 'en','P_EndDate','End date', 'PortalInfo'
GO
updatelanguagecontext 'en','P_Host_Fee','Hosting fee', 'PortalInfo'
GO
updatelanguagecontext 'en','P_PortalName','Portal name', 'PortalInfo'
GO
updatelanguagecontext 'en','P_Users','Users', 'PortalInfo'
GO
updatelanguagecontext 'en','ManageRoles','Manage account', 'PortalRoles'
GO
updatelanguagecontext 'en','Demo_Trial_Fee','Trial fee', 'PortalRoles'
GO
updatelanguagecontext 'en','Billing_Frequency','Billing frequency', 'PortalRoles'
GO
updatelanguagecontext 'en','Billing_Period','Billing period(each)', 'PortalRoles'
GO
updatelanguagecontext 'en','ServicesFee','Services fee', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_AllRoles','All roles', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Assignation_Auto','Auto assignation?', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Assignation_Public','Public?', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Description','Description', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Expiry_Date','End date', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_ExpiryDate','End date', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Frequency','Each', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_FrequencyP','Period', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Header_Auto','Auto', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Header_Public','Public', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Name','Name', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_PortalRole','Security Role', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Trial','Trial', 'PortalRoles'
GO
updatelanguagecontext 'en','Role_Upload','Is allowed to upload', 'PortalRoles'
GO
updatelanguagecontext 'en','Verified_Register_info','<b>*Note:</b> The portal registration is verified. You will receive an E-Mail with a validation code after you complete this form.  You will have to use this validation code to enter the portal the first time.', 'Register'
GO
updatelanguagecontext 'en','UserName_Already_Used','You must use another User Name since <u>{username}</u> is already used.<br>Select another User Name.', 'Register'
GO
updatelanguagecontext 'en','Valid_IP_Not_Saved','Code not saved', 'Register'
GO
updatelanguagecontext 'en','Valid_IP_Saved','Code sauved', 'Register'
GO
updatelanguagecontext 'en','Valid_IP_Security','The IP Code are not valid.  Use a code between 0.0.0.1 and 255.255.255.255', 'Register'
GO
updatelanguagecontext 'en','Valid_IP_Security1','The IP Code from: must be equal or lower than the IP Code to:', 'Register'
GO
updatelanguagecontext 'en','PersonnalInfo','Personal information', 'Register'
GO
updatelanguagecontext 'en','Public_Register_info','<b>*Note:</b> The portal registration is public. You will have access to the portal after you complete this form.', 'Register'
GO
updatelanguagecontext 'en','Private_Info','<b>*Note:</b> The portal registration is private. The site administrator will validate your request and will reply by E-Mail.', 'Register'
GO
updatelanguagecontext 'en','Register_Required_Info','All the fields identified with (*) are required.', 'Register'
GO
updatelanguagecontext 'en','Register_Title','Registration form', 'Register'
GO
updatelanguagecontext 'en','Required_Info','<b>*Note:</b> All the fields identified with (*) are required.', 'Register'
GO
updatelanguagecontext 'en','rss_account_info','Domain\username', 'RSS'
GO
updatelanguagecontext 'en','rss_account_password','Password', 'RSS'
GO
updatelanguagecontext 'en','rss_css_external','External CSS', 'RSS'
GO
updatelanguagecontext 'en','rss_css_internal','Internal CSS', 'RSS'
GO
updatelanguagecontext 'en','rss_CSS_source','News Feed Style Sheet', 'RSS'
GO
updatelanguagecontext 'en','rss_external','External', 'RSS'
GO
updatelanguagecontext 'en','rss_general_info','<b>*Note:</b> You may want to see the available RSS feeds at&nbsp;<b><a href="http://w.moreover.com/categories/category_list_rss.html" target="_new">Moreover.Com</a></b>', 'RSS'
GO
updatelanguagecontext 'en','rss_internal','Internal', 'RSS'
GO
updatelanguagecontext 'en','rss_news_source','News Feed Source', 'RSS'
GO
updatelanguagecontext 'en','rss_noconnect_admin','It was not possible to connect to {xmlsrc}. Plase make  sure the URL or the proxy settings are valid.', 'RSS'
GO
updatelanguagecontext 'en','rss_noconnect_error','The RSS link is not available. Error message : {errormessage}', 'RSS'
GO
updatelanguagecontext 'en','rss_security','Security Options (optional)', 'RSS'
GO
updatelanguagecontext 'en','rss_security_account','Account Information', 'RSS'
GO
updatelanguagecontext 'en','search_header_desc','Description', 'Search'
GO
updatelanguagecontext 'en','search_header_table','Table', 'Search'
GO
updatelanguagecontext 'en','search_header_title','Title', 'Search'
GO
updatelanguagecontext 'en','search_header_update','Update', 'Search'
GO
updatelanguagecontext 'en','search_header_who','By', 'Search'
GO
updatelanguagecontext 'en','Search_max_result','Max. number of result', 'Search'
GO
updatelanguagecontext 'en','Search_max_width','Max. width of title', 'Search'
GO
updatelanguagecontext 'en','Search_max_width_desc','Max. size for description', 'Search'
GO
updatelanguagecontext 'en','search_search','Search', 'Search'
GO
updatelanguagecontext 'en','Search_show_audit','See stats?', 'Search'
GO
updatelanguagecontext 'en','Search_show_breadcrum','See breadcrums?', 'Search'
GO
updatelanguagecontext 'en','Search_show_desc','See the description?', 'Search'
GO
updatelanguagecontext 'en','Search_sql_table','Table', 'Search'
GO
updatelanguagecontext 'en','SD_Title','Demo portal setup', 'Signup'
GO
updatelanguagecontext 'en','S_User_Name','For security reason you must use a User Name with at least 8 letters or numbers<br>', 'Signup'
GO
updatelanguagecontext 'en','S_Wait','A moment please!', 'Signup'
GO
updatelanguagecontext 'en','S_CreatePortal','Create Portal', 'Signup'
GO
updatelanguagecontext 'en','S_EMail','You need to have a valid E-Mail address!<br>', 'Signup'
GO
updatelanguagecontext 'en','S_F_Email','E-Mail', 'Signup'
GO
updatelanguagecontext 'en','S_F_LastName','Name', 'Signup'
GO
updatelanguagecontext 'en','S_F_Name','First name', 'Signup'
GO
updatelanguagecontext 'en','S_F_Password','Password', 'Signup'
GO
updatelanguagecontext 'en','S_F_UserCode','User Code', 'Signup'
GO
updatelanguagecontext 'en','S_F_Wait','Click here if you are not redirected in 10 sec...', 'Signup'
GO
updatelanguagecontext 'en','S_F_Wait_Info','We are setting your web portal. You should received in the few minutes an E-Mail with directives for logging in the first time.', 'Signup'
GO
updatelanguagecontext 'en','S_InstalPortal','Portal setup&nbsp;', 'Signup'
GO
updatelanguagecontext 'en','S_NameAlreadyUsed','The domain name http://{PortalAlias} is already used.  Please select a new domain name.', 'Signup'
GO
updatelanguagecontext 'en','S_NameAlreadyUsed1','You need to chose another portal name since this one is already used.', 'Signup'
GO
updatelanguagecontext 'en','S_Need_Names','Your name and first name are required for your registration<br>', 'Signup'
GO
updatelanguagecontext 'en','S_Need_Portal_Name','A portal name is required<br>', 'Signup'
GO
updatelanguagecontext 'en','S_Password','For security reason you must use a Password with at least 8 letters or numbers.<br>', 'Signup'
GO
updatelanguagecontext 'en','S_PortalAdmin','Portal Administrator settings', 'Signup'
GO
updatelanguagecontext 'en','S_PortalName','Portal name', 'Signup'
GO
updatelanguagecontext 'en','S_PortalTemplate','Template', 'Signup'
GO
updatelanguagecontext 'en','S_PortalType','Portal type', 'Signup'
GO
updatelanguagecontext 'en','S_PortalTypeChild','Child', 'Signup'
GO
updatelanguagecontext 'en','S_PortalTypeParent','Parent', 'Signup'
GO
updatelanguagecontext 'en','S_Title','Add a new portal', 'Signup'
GO
updatelanguagecontext 'en','Confirm_Password','Confirm', 'Signup'
GO
updatelanguagecontext 'en','No_accent_In_Name','The portal name cannot have space or punctuation in it.', 'Signup'
GO
updatelanguagecontext 'en','GetLanguage("SS_LoginModuleSkin")','Login', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Site_Description','Site Description', 'SiteSettings'
GO
updatelanguagecontext 'en','optBannerAdvertising_0','None', 'SiteSettings'
GO
updatelanguagecontext 'en','optBannerAdvertising_1','Site', 'SiteSettings'
GO
updatelanguagecontext 'en','optBannerAdvertising_2','Host', 'SiteSettings'
GO
updatelanguagecontext 'en','optUserRegistration_0','None', 'SiteSettings'
GO
updatelanguagecontext 'en','optUserRegistration_1','Private', 'SiteSettings'
GO
updatelanguagecontext 'en','optUserRegistration_2','Public', 'SiteSettings'
GO
updatelanguagecontext 'en','optUserRegistration_3','Validated', 'SiteSettings'
GO
updatelanguagecontext 'en','cmdRenew','Click here to renew your portal subscription', 'SiteSettings'
GO
updatelanguagecontext 'en','chkDemoDomain','yourname', 'SiteSettings'
GO
updatelanguagecontext 'en','MS_ModuleDeleteSkin','Erase all the modules SKINS?', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Guest','Anonymous', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Head_Demo','URL to create a new portal', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Head_Demo_Allow','Allow cration of a demo portal?', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Head_Language','Linguistic settings', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Head_Language_Info','Language info to edit', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Head_PortalInfo','Hosting information', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Admin','Administrater', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Allow_Demo_Create','Allow the creation of a web portal', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Background','Background image', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Contener','Modules SKIN', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Currency','Currency', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Demo_Domain','Domain for the Portal', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Email','E-Mail notice', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Language_Default','Default language', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Language_ToUse','Language to use', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Logo','Logo', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Mod_Info','Edit', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_PortalBasicFee','Basic hosting fee', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_PortalExtraFee','Module extra fee', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Processor','Payment processor', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Processor_Code','Access Code payment processor', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Processor_Password','Password payment processor', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_ReplaceLOGO','Alternative logo info.', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_SkinEdit','Edit SKIN', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Tigra','Edit TIGRA menu settings', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_TimeZone','Time Zone', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_TotalPortalFee','Total hosting fee', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_TypeRegistration','Registration type', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_UserInfo','Show user info', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Label_Vendors','Vendors source', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_lnkcontainer','Get a Module SKIN', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_LoginModuleSkin','Login', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Month','Month', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_PortalAlias','Alias portal', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_PortalCSS_Tooltip','Edit CSS file', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_PortalDiskSpace','Space (MB)', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_PortalExpired','Portal end date', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_PortalSkin_Tooltip','Edit the default SKIN definition.', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Privacy_Text','Privacy Statement', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Privacy_Tooltip','Edit Privacy Statement', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Renewal','- Portal Renewall ( Basic Host Fee: {HostFee} + Modules bonus: {ModuleFee} = Total Fee: {TotalFee} )', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Site_Keywords','Key words', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Site_Title','Title', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Site_Title_Bottom','Footer text', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_SiteLogDays','Site log (days)', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_SiteModuleSkin','Portal', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Terms_Text','Terms Of Use', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Terms_Tooltip','Edit Terms Of Use', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_TTTCSS_Tooltip','Edit Gallery and Forum CSS file.', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_txtlogin','Directives to enter', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_txtRegistration','Directives to register', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_txtSignup','Text included in the registration E-Mail', 'SiteSettings'
GO
updatelanguagecontext 'en','SiteSettings1','Skinning', 'SiteSettings'
GO
updatelanguagecontext 'en','SiteSettings2','General', 'SiteSettings'
GO
updatelanguagecontext 'en','SiteSettings3','Localization', 'SiteSettings'
GO
updatelanguagecontext 'en','SiteSettings4','Demo Portal', 'SiteSettings'
GO
updatelanguagecontext 'en','SiteSettings5','Subscription', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_AdminModuleSkin','Admin', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_cmdGoogle','Send to Google', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_cmdProcessor','Go to the payment vendor web site.', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_DemoDirectives','Demo portal creation directives', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_EditModuleSkin','Edit', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_EditSkin_Tooltip','Edit admin SKIN page definition.', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_ToolTipModuleSkin','ToolTip', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Use_SSL','Use SSL', 'SiteSettings'
GO
updatelanguagecontext 'en','Skin_FileErased','The file was erased', 'SkinEdit'
GO
updatelanguagecontext 'en','Skin_FileSaved','The file was saved', 'SkinEdit'
GO
updatelanguagecontext 'en','Skin_NeedContentpane','{contentpane} must be between &lt;form&gt; and &lt;/form&gt;', 'SkinEdit'
GO
updatelanguagecontext 'en','Skin_NeedForm','&lt;form&gt;&lt;/form&gt; must be in sequence on the page.', 'SkinEdit'
GO
updatelanguagecontext 'en','Skin_NeedForm2','&lt;form&gt;&lt;/form&gt; must be on the page.', 'SkinEdit'
GO
updatelanguagecontext 'en','SkinFileNoExist','The file does not exist!', 'SkinEdit'
GO
updatelanguagecontext 'en','SQL_Execute','Execute the script', 'SQL'
GO
updatelanguagecontext 'en','SQL_ExecuteCMD','Execute the SQL request', 'SQL'
GO
updatelanguagecontext 'en','SQL_Info','To execute a script on this data base or another one', 'SQL'
GO
updatelanguagecontext 'en','SQL_Connection','Put in the SQL DataBase Connection string if using another DataBase', 'SQL'
GO
updatelanguagecontext 'en','SQL_NeedExtension','You need to use a .sql file type', 'SQL'
GO
updatelanguagecontext 'en','SQL_NoFile','No file to process', 'SQL'
GO
updatelanguagecontext 'en','Nothing_To_Do','Nothing to do', 'SQL'
GO
updatelanguagecontext 'en','Stat_StartDate','Start date', 'Statistique'
GO
updatelanguagecontext 'en','Stat_ToGet','What report', 'Statistique'
GO
updatelanguagecontext 'en','Stat_Calendar','Calender', 'Statistique'
GO
updatelanguagecontext 'en','Stat_Display','See', 'Statistique'
GO
updatelanguagecontext 'en','Stat_EndDate','End date', 'Statistique'
GO
updatelanguagecontext 'en','Sub_Every','every', 'Subscription'
GO
updatelanguagecontext 'en','FrequencyCode_D','Day', 'Subscription'
GO
updatelanguagecontext 'en','FrequencyCode_M','Month', 'Subscription'
GO
updatelanguagecontext 'en','FrequencyCode_N','None', 'Subscription'
GO
updatelanguagecontext 'en','FrequencyCode_O','Once', 'Subscription'
GO
updatelanguagecontext 'en','FrequencyCode_W','Week', 'Subscription'
GO
updatelanguagecontext 'en','FrequencyCode_Y','Year', 'Subscription'
GO
updatelanguagecontext 'en','free_memory','Available memory', 'SystemStat'
GO
updatelanguagecontext 'en','number_of_connections','Number of connections', 'SystemStat'
GO
updatelanguagecontext 'en','number_of_restart','ASP.NET Restart', 'SystemStat'
GO
updatelanguagecontext 'en','day','Day', 'SystemStat'
GO
updatelanguagecontext 'en','browser','Browser', 'SystemStat'
GO
updatelanguagecontext 'en','cache_memory_used','Memory caches used', 'SystemStat'
GO
updatelanguagecontext 'en','ip_address','IP addresse', 'SystemStat'
GO
updatelanguagecontext 'en','millisecond','Millisecond', 'SystemStat'
GO
updatelanguagecontext 'en','minute','Minute', 'SystemStat'
GO
updatelanguagecontext 'en','server_up_time','Server up time', 'SystemStat'
GO
updatelanguagecontext 'en','seconde','Seconde', 'SystemStat'
GO
updatelanguagecontext 'en','hour','Hour', 'SystemStat'
GO
updatelanguagecontext 'en','ts_css','CSS File', 'TabSettings'
GO
updatelanguagecontext 'en','ts_disable','Deactivate page link?', 'TabSettings'
GO
updatelanguagecontext 'en','ts_disableinfo','The link will not be available in the menu', 'TabSettings'
GO
updatelanguagecontext 'en','ts_editcss','Edit CSS', 'TabSettings'
GO
updatelanguagecontext 'en','ts_editcssinfo','Edit the CSS file', 'TabSettings'
GO
updatelanguagecontext 'en','ts_editmenuparam','Edit the menu setup', 'TabSettings'
GO
updatelanguagecontext 'en','ts_editskin','Edit SKIN', 'TabSettings'
GO
updatelanguagecontext 'en','ts_editskininfo','Edit SKIN file', 'TabSettings'
GO
updatelanguagecontext 'en','ts_icone','Icone', 'TabSettings'
GO
updatelanguagecontext 'en','ts_parenttab','Parent page', 'TabSettings'
GO
updatelanguagecontext 'en','ts_skin','SKIN File', 'TabSettings'
GO
updatelanguagecontext 'en','ts_TabAdminRole','Edit privileges', 'TabSettings'
GO
updatelanguagecontext 'en','ts_tableleftwidth','Width of left pane', 'TabSettings'
GO
updatelanguagecontext 'en','ts_tablerightwidth','Width of right pane', 'TabSettings'
GO
updatelanguagecontext 'en','ts_tabname','Name of page', 'TabSettings'
GO
updatelanguagecontext 'en','ts_tabtemplate','Page template', 'TabSettings'
GO
updatelanguagecontext 'en','ts_TabViewRole','See', 'TabSettings'
GO
updatelanguagecontext 'en','ts_visible','Visible?', 'TabSettings'
GO
updatelanguagecontext 'en','ts_visibleinfo','Only use if the page does not have child pages', 'TabSettings'
GO
updatelanguagecontext 'en','Generate_XML','Create a template', 'TabSettings'
GO
updatelanguagecontext 'en','ts_xmltemplate','XML Template', 'TabSettings'
GO
updatelanguagecontext 'en','tigra_Block_Left','Block_Left', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Block_Top','Block_Top', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Centrer','Center', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Fermeture','Closing', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Hauteur','Hight', 'Tigra'
GO
updatelanguagecontext 'en','tigra_help','To generate the settings necessary for a vertical menu, you will be able to change the setting on line and test the menu before saving it. To see the menu us the button <b>Test menu</b>', 'Tigra'
GO
updatelanguagecontext 'en','tigra_help1','To generate the settings necessary for a horizontal menu, you will be able to change the setting on line and test the menu before saving it.  To see the menu us the button <b>Test menu</b>', 'Tigra'
GO
updatelanguagecontext 'en','tigra_help2','Take the menu settings and created a new menu file.', 'Tigra'
GO
updatelanguagecontext 'en','tigra_help3','To save the menu settings.  You must click on the <b>Test menu</b> prior so saving.', 'Tigra'
GO
updatelanguagecontext 'en','tigra_horizontal','Horizontal', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Largeur','Width', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Left','Left', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu1','MENU 1er level', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu1.1','Menu position in hight from top left corner', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu1.2','Menu position in width from top left corner', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu1.3','Vertical difference in pixel between cells', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu1.4','Horizontal difference in pixel between cells', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu1.5','Cell hight in pixel', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu1.6','Cell width in pixel', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu2','MENU 2nd level', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu2.1','Menu position relative to top menu', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu3','MENU 3rd level', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu3.1','Sub menu position relative to top menu', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu4','To put the menu in the center you have to use a value higher than 0.  Use the with of the menu, or the width of the logo (if you put the logo in the middle also)', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu4.1','Time milliseconds before the menu open', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Menu4.2','Time in milliseconds before the menu close after leaving it.', 'Tigra'
GO
updatelanguagecontext 'en','tigra_OpenClose','Opening/Closing', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Ouverture','Opening', 'Tigra'
GO
updatelanguagecontext 'en','tigra_return','Return', 'Tigra'
GO
updatelanguagecontext 'en','tigra_tester','Test menu', 'Tigra'
GO
updatelanguagecontext 'en','tigra_Top','Top', 'Tigra'
GO
updatelanguagecontext 'en','tigra_vertical','Vertical', 'Tigra'
GO
updatelanguagecontext 'en','OrderBy','Order By', 'UDT'
GO
updatelanguagecontext 'en','Int32','Integer', 'UDT'
GO
updatelanguagecontext 'en','Boolean','Boolean', 'UDT'
GO
updatelanguagecontext 'en','Decimal','Decimal', 'UDT'
GO
updatelanguagecontext 'en','String','String', 'UDT'
GO
updatelanguagecontext 'en','DateTime','DateTime', 'UDT'
GO
updatelanguagecontext 'en','ManageTableUDT','Manage tables', 'UDT'
GO
updatelanguagecontext 'en','grdFields_Title','Title', 'UDT'
GO
updatelanguagecontext 'en','grdFields_Type','Type', 'UDT'
GO
updatelanguagecontext 'en','grdFields_visible','Visible', 'UDT'
GO
updatelanguagecontext 'en','AddField','Add a new field', 'UDT'
GO
updatelanguagecontext 'en','Move_Field_Down','Down', 'UDT'
GO
updatelanguagecontext 'en','Move_Field_Up','Up', 'UDT'
GO
updatelanguagecontext 'en','U_Autorized','Autorized', 'User'
GO
updatelanguagecontext 'en','U_Created_Date','Created date', 'User'
GO
updatelanguagecontext 'en','U_LastLogin_Date','Last login', 'User'
GO
updatelanguagecontext 'en','U_QUIT_site','Quit', 'User'
GO
updatelanguagecontext 'en','U_SecurityControl','Login security', 'User'
GO
updatelanguagecontext 'en','U_SecurityContryCode','Country Code', 'User'
GO
updatelanguagecontext 'en','U_SecurityIPFROM','IP from', 'User'
GO
updatelanguagecontext 'en','U_SecurityIPTO','IP to', 'User'
GO
updatelanguagecontext 'en','U_Subscribe','Register', 'User'
GO
updatelanguagecontext 'en','ManageUserRoles','Manage security roles', 'User'
GO
updatelanguagecontext 'en','UO_AddList','Add to my buddy list', 'UserOnline'
GO
updatelanguagecontext 'en','UO_AddToList','Add to my buddy list', 'UserOnline'
GO
updatelanguagecontext 'en','UO_AllUsers','All users', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Anonymous','Guests', 'UserOnline'
GO
updatelanguagecontext 'en','UO_ASC','ASC', 'UserOnline'
GO
updatelanguagecontext 'en','UO_BadUserID','The user ID is not valid', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Body','Buddy?', 'UserOnline'
GO
updatelanguagecontext 'en','UO_by','by', 'UserOnline'
GO
updatelanguagecontext 'en','UO_click_connect','Click here to enter', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Date_received','Received date', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Date_Send','Send date', 'UserOnline'
GO
updatelanguagecontext 'en','UO_DESC','DESC', 'UserOnline'
GO
updatelanguagecontext 'en','UO_erase','Erase all selected messages', 'UserOnline'
GO
updatelanguagecontext 'en','UO_FindUser','Find a user', 'UserOnline'
GO
updatelanguagecontext 'en','UO_FirstPage','First', 'UserOnline'
GO
updatelanguagecontext 'en','UO_From','from', 'UserOnline'
GO
updatelanguagecontext 'en','UO_InBox','Inbox', 'UserOnline'
GO
updatelanguagecontext 'en','UO_InfoUserName','User information', 'UserOnline'
GO
updatelanguagecontext 'en','UO_IsBody','Is buddy', 'UserOnline'
GO
updatelanguagecontext 'en','UO_KeepNew','Keep as new', 'UserOnline'
GO
updatelanguagecontext 'en','UO_LastOn','Last visit', 'UserOnline'
GO
updatelanguagecontext 'en','UO_LastPage','Last', 'UserOnline'
GO
updatelanguagecontext 'en','UO_ListUserName','{username} body list', 'UserOnline'
GO
updatelanguagecontext 'en','UO_LRegistered','Last', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Members','Members', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Message','Message', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Message_Notice','{FullName},
You received a message from {SenderName}.
To read this message : {MessageURL}', 'UserOnline'
GO
updatelanguagecontext 'en','UO_ModInfo','Edit user settings', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Name','Name', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Need_Object','You must have an object and some text in the body!<br>', 'UserOnline'
GO
updatelanguagecontext 'en','UO_NeverConnected','Never connected', 'UserOnline'
GO
updatelanguagecontext 'en','UO_NewMessage','New message', 'UserOnline'
GO
updatelanguagecontext 'en','UO_NextPage','Next', 'UserOnline'
GO
updatelanguagecontext 'en','UO_no_connect','You must be connected to use this module', 'UserOnline'
GO
updatelanguagecontext 'en','UO_no_pref','You do not have any buddies', 'UserOnline'
GO
updatelanguagecontext 'en','UO_No_User','* is not a user.<br>', 'UserOnline'
GO
updatelanguagecontext 'en','UO_NoBody','No buddy.', 'UserOnline'
GO
updatelanguagecontext 'en','UO_NoMessage','No messages', 'UserOnline'
GO
updatelanguagecontext 'en','UO_NotFound','Not Found!', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Notice_Object','Message on {portalname}', 'UserOnline'
GO
updatelanguagecontext 'en','UO_NotRead','New', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Object','Subject', 'UserOnline'
GO
updatelanguagecontext 'en','UO_offline','Off line', 'UserOnline'
GO
updatelanguagecontext 'en','UO_OldMessage','Read', 'UserOnline'
GO
updatelanguagecontext 'en','UO_online','On line', 'UserOnline'
GO
updatelanguagecontext 'en','UO_OnLine_Now','On line now', 'UserOnline'
GO
updatelanguagecontext 'en','UO_OnLineNow','On line?', 'UserOnline'
GO
updatelanguagecontext 'en','UO_OnlineOnly','Only users on line', 'UserOnline'
GO
updatelanguagecontext 'en','UO_OnlyBody','Only my buddies', 'UserOnline'
GO
updatelanguagecontext 'en','UO_or','or', 'UserOnline'
GO
updatelanguagecontext 'en','UO_OutBox','Outbox', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Page','Page', 'UserOnline'
GO
updatelanguagecontext 'en','UO_pm','Private messages', 'UserOnline'
GO
updatelanguagecontext 'en','UO_PrevPage','Previous', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Re','RE:', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Read','Old', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Registered','Membership', 'UserOnline'
GO
updatelanguagecontext 'en','UO_ReMessage','<br>----------Original Message-------------<br>', 'UserOnline'
GO
updatelanguagecontext 'en','UO_RemovePU','Erase buddy', 'UserOnline'
GO
updatelanguagecontext 'en','UO_reply','Reply', 'UserOnline'
GO
updatelanguagecontext 'en','UO_SearchOptions','Search optons', 'UserOnline'
GO
updatelanguagecontext 'en','UO_See','See', 'UserOnline'
GO
updatelanguagecontext 'en','UO_see_message','Click here to see your message', 'UserOnline'
GO
updatelanguagecontext 'en','UO_see_user','See info of {username}.', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Select','Select', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Send','Send', 'UserOnline'
GO
updatelanguagecontext 'en','UO_SendMessage','Send private message', 'UserOnline'
GO
updatelanguagecontext 'en','UO_Since','Since', 'UserOnline'
GO
updatelanguagecontext 'en','UO_SinceEver','Since ever!', 'UserOnline'
GO
updatelanguagecontext 'en','UO_to','To', 'UserOnline'
GO
updatelanguagecontext 'en','UO_ToRead','to read', 'UserOnline'
GO
updatelanguagecontext 'en','UO_TRegistered','Total', 'UserOnline'
GO
updatelanguagecontext 'en','UO_UserName','User name', 'UserOnline'
GO
updatelanguagecontext 'en','UO_write','Write', 'UserOnline'
GO
updatelanguagecontext 'en','UO_YRegistered','Yesterday', 'UserOnline'
GO
updatelanguagecontext 'en','UOSearchHelp','Use * to make a search', 'UserOnline'
GO
updatelanguagecontext 'en','Users_Delete','Delete non authorized users', 'Users'
GO
updatelanguagecontext 'en','Vender_Clicks','Clicks', 'Vendors'
GO
updatelanguagecontext 'en','Vender_End','End', 'Vendors'
GO
updatelanguagecontext 'en','Vender_Start','Start', 'Vendors'
GO
updatelanguagecontext 'en','Vender_Type','Type', 'Vendors'
GO
updatelanguagecontext 'en','noFeedback','No feedback available for this vendor', 'Vendors'
GO
updatelanguagecontext 'en','Vender_URL','URL', 'Vendors'
GO
updatelanguagecontext 'en','Vender_View','Vues', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_Add','Add a banner', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_CPM','CPM', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_EndDate','* End date', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_Image','Image', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_Imp','Impressions', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_Name','Name of banner', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_Optional','* = optional', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_Publicity','Publicity by Banner', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_StartDate','* Start date', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_Type','Banner type', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner_URL','* URL', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner1','Banner', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner2','Button (small)', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner3','Button (medium)', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner4','Button (large)', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Banner5','Very large', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Classification','Classifications', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Header_LastRequest','Last request', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Header_Request','Request', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Header_Search','Search', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_KeyWord','Key word', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Name','Vendor name', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Non_Aut','Non authorized', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_ThankYou','Thank you for your contribution.', 'Vendors'
GO
updatelanguagecontext 'en','VendorBannerInfo','The name of the banner will appear has an alternate text for the image.<br><br>If there is not URL info about the vendor will appear instead.<br><br>For an unlimited number of exposition put (0).<br><br>', 'Vendors'
GO
updatelanguagecontext 'en','VendorBannerInfo1','The CPM is the cost for 1000 expositions of the banner.<br><br>Select a start an end date to set a specific period when the banner will be displayed.', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_Add','Add a vendor', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_AddName','Add', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_Bussiness','Compagny', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_Clicks','Numbers of clicks', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_cmddelete','Erase non authorized vendors', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_Directives','Registering directives', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_Edit_Classifications','Edit vendors classifications', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_Fax','Fax', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_Info_List','The vendors list is used to manage vendors services and vendors banner publicity.', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_Logo','Logo', 'Vendors'
GO
updatelanguagecontext 'en','vendors_message_feedback1','You must be a member to write a feedback.<br>', 'Vendors'
GO
updatelanguagecontext 'en','vendors_message_feedback2','If you are not already a member, you can register here. <b><a href=" {httpregister}">Register</a></b>.<br><br>', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_OptionsEdit','Edit vendors options', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_SeeClicks','See stats?', 'Vendors'
GO
updatelanguagecontext 'en','vendors_user','User', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_WebSite','Web Site', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_WelcomeMessage','Welcome message vendors', 'Vendors'
GO
updatelanguagecontext 'en','Vendors_WhoCanSubscribe','Who can register?', 'Vendors'
GO
updatelanguagecontext 'en','lnkDirections','Directions', 'Vendors'
GO
updatelanguagecontext 'en','lnkFeedback','Feedback', 'Vendors'
GO
updatelanguagecontext 'en','lnkmap','map', 'Vendors'
GO
updatelanguagecontext 'en','Oriflamme','Banner', 'Vendors'
GO
updatelanguagecontext 'en','feedback_pos','Positive', 'Vendors'
GO
updatelanguagecontext 'en','Positive_Feedback','Positive feedback', 'Vendors'
GO
updatelanguagecontext 'en','vendors_date','date', 'Vendors'
GO
updatelanguagecontext 'en','Negative_Feedback','Negative feedback', 'Vendors'
GO
updatelanguagecontext 'en','feedback_neg','Negatif', 'Vendors'
GO
updatelanguagecontext 'en','Vendor_Register','Vendor Registration', 'Vendors'
GO
updatelanguagecontext 'en','w_personalise','Personalise?', 'Weather'
GO
updatelanguagecontext 'en','XML_data','XML Data File', 'XML'
GO
updatelanguagecontext 'en','XML_external','External', 'XML'
GO
updatelanguagecontext 'en','XML_internal','Internal', 'XML'
GO
updatelanguagecontext 'en','XML_transform','XSL Transformation File', 'XML'
GO
UpdatelonglanguageSetting 'en','Access Denied','Either you are not currently logged in, or you do not have access to this content.<br><br>
For help please contact {AdministratorEmail}.', null 
GO
UpdatelonglanguageSetting 'en','AccessDeniedInfo','<table align="center" width="80%" border="0">
<tr>
<td>
<br>
<br>
Either you are not currently logged in, or you do not have access to this content.<br><br>
For help please contact {AdministratorEmail}.
</td></tr></table>', null 
GO
UpdatelonglanguageSetting 'en','Demo_Portal_Info','<div align="left" class="normal">
<p align="left">
{Directives}
</p>
<br><br>
</div>
<div align="left" class="normal">
<span class="Head">NOTE</span>
<br><br>
<ol>
	<li>You will need de chose a name for your site.  The URL will be a sub domain of {DomainName}.  Like http://{DomainName}/votenom/</li>
	<li>You will need to identify yourself, your name will be required.</li>
	<li>You will need to give us a valide E-Mail.  We will send you a confirmation E-Mail with a validation link.</li>
	<li>You will need to select a template.  You can view the available template here&nbsp;&nbsp;<b><i>{lblpreview}</i></b></li>
	<li>You will need to select a valide user code and password.  For security reason they will have to be a minimum of 8 letters or numbers each. </li>
	<li>You must accept the temps of Use. Click on the link below to do so.</li>
</ol>
<br><br>
</div>
<div align="left" class="normal">
<br><br>
<span class="Head">Conditions of use between you and {PortalName}
</span>
<br><br>
<p align="left">
As a condition of your use of the {PortalName} Web Site, you warrant to {PortalName} that you will not use the {PortalName} Web Site for any purpose that is unlawful or prohibited by these terms, conditions, and notices. You may not use the {PortalName} Web Site in any manner which could damage, disable, overburden, or impair the {PortalName} Web Site or interfere with any other party''s use and enjoyment of the {PortalName} Web Site.
</p><p>
<a href="http://{DomainName}/{language}.default.aspx?edit=control&def=Signup&guid={lblGUID}" title="I accept the conditions of use">&nbsp;<b><font color="#ff0000">I accept</font></b></a>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://{DomainName}/" title="Return">&nbsp;<b><font color="#669900">I refuse</font></b></a>
</p>
</div>', null 
GO
UpdatelonglanguageSetting 'en','Demo_Portal_Info1','There are two types of portals which can be created using this application:<br><br><li><b>Parent Portals</b> are sites which have a unique URL ( ie. www.domain.com ) associated to them. This generally involves purchasing a Domain Name from an Internet Registrar, setting the Primary/Secondary DNS entries to point to the Hosting Provider''s DNS Server, and having your Hosting Provider map the Domain Name to the IP Address of your account. An example of a valid Parent Portal Name is <b>www.mydomain.com</b>. You can also use the IP Address of your site without a Domain Name ( ie. <b>65.174.86.217</b> ). If you need to have multiple Domain Names pointing to the same portal then you can seperate them with commas ( ie. <b>www.domain1.com,www.domain2.com</b> ). Do not create a Parent Portal until all of the DNS mappings are in place or else you will not be able to access your portal.<br><br><li><b>Child Portals</b> are subhosts of your Hosting Provider account. Essentially this means a directory is created on the web server which allows the site to be accessed through a URL address which includes a Parent domain name as well as the directory name ( ie. www.domain.com/directory ). An example of a valid Child Portal Name is <b>{DomainName}/portalname</b>. A Child Portal can be converted into a Parent Portal at any time by simply modifying the Portal Alias entry.<br><br>*Please Note: Portals will be created using the default properties defined in the Host Settings module ( HostFee, HostSpace, DemoPeriod ). Once the portal is created, these properties can be modified in the Site Settings module.<br><br>', null 
GO
UpdatelonglanguageSetting 'en','Demo_Portal_Info2','You can install your very own DotNetZoom website and experiment with its advanced features {DemoPeriod}for a période of {days} days{/DemoPeriod}. Your demo website is full featured and provides you with 20 MB of disk space.
<br><br><b>*Note:</b> Your Website Name must be a single word and cannot contain spaces or punctuation characters. The URL for your portal will take the form of <b>{DomainName}/portalname</b><br><br>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_AccessDenied','This issue occurs because your security no longer matches the security required.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_AdminMenu','<P>A menu page titled ''Admin'' with  ( 8 ) child pages is visible to users Site Administrators. These pages are displayed to the right of existing pages in the menu and provide access to these Administrator tools:</P>
<UL>
<LI>Site Settings; 
<LI> Page Management; 
<LI>Security Role management; 
<LI>User Account management; 
<LI>Vendor and banner management; 
<LI>Site Log reports; 
<LI>Newsletter creation and sending; 
<LI>File management; 
</LI></UL>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_BulkEmail','Administrators can send a bulk email to all users belonging to a particular Security Role or to any email address. The email is sent to each user separately, without revealing other users email addresses.<br><br>

Bulk mail can be either plain text or HTML format. Attachments from your File Manager can be added to the email (only images could be selected at the time of writing).', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Demo','As a condition of your use of the {PortalName} Web Site, you warrant to {PortalName} that you will not use the {PortalName} Web Site for any purpose that is unlawful or prohibited by these terms, conditions, and notices. You may not use the {PortalName} Web Site in any manner which could damage, disable, overburden, or impair the {PortalName} Web Site or interfere with any other party''s use and enjoyment of the {PortalName} Web Site.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_DiscussDetails','The Discussions module renders a group of message threads on a specific topic. Discussion includes a Read/Reply Message page, which allows authorized users to reply to existing messages, add new messages or delete existing messages.<br><br>

The following details are automatically recorded and displayed for each discussion:<br><br>

    * First Name and Last Name of the user who posted the message, or the last user to update the message.<br>
    * Date and Time the message was added or last updated.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Discussions','Read the content of a discussion thread.<br><br>

   1. Click on the linked message title to read that message. The related message will now be displayed. To display replies to a thread, click the Maximize maximize.gif button.<br>
   2. Click Cancel to return to the module.<br>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditAccessDenied','Either you are not currently logged in, or you do not have access to this content.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditAlbum','Help information Edit Album', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditAnnoncement','The Announcements Module produces a list of simple text announcements consisting of a title and brief description. Options include a read more link to a file, page or other site, announcement publish date and expiration date. Link click tracking and logging are also available. Announcements are ordered from newest to oldest; unless a view order is specified.<br />
<br />
<p>Add an announcement with a link to a file located on the site. The file link will be represented as a read more link after the description of the announcement.</p>
<ol>
    <li><em>Add</em> an <strong>Announcements</strong> module, or <em>go to</em> an existing <strong>Announcements</strong> module.   </li>
    <li><em>Select </em><strong>Add New Announcement</strong> from the Module Menu.  </li>
    <li>In the <strong>Title</strong> field, <em>enter</em> a title for the announcement.  </li>
    <li>At <strong>Add Date?</strong> select one of the following options:
    <ol>
        <li><em>Check </em>the check box to add the current date and time, or   </li>
        <li><em>Uncheck </em>the check box to remove the current day and date from beside the title.</li>
    </ol>
    </li>
    <li>At <strong>Description</strong>, <em>enter</em> a description for the announcement.  </li>
    <li>At <strong>Link / Link Type</strong>, <em>select</em> <strong>File ( A File on your Site </strong>).  </li>
    <li>At <strong>Link</strong>, <em>perform</em> one of the following:
    <ol type="a">
        <li>If the file has already been uploaded to the Admin &gt; File Manager:
        <ol type="i">
            <li>At <strong>Link / File Location</strong>, select the folder within the Admin &gt; File Manager where the file was uploaded.  </li>
            <li>At <strong>Link / File Name</strong>, <em>select</em> the file from the drop down list.</li>
        </ol>
        </li>
        <li>If the file has not been uploaded to the Admin &gt; File Manager:
        <ol type="i">
            <li>i. <em>Click</em> <u>Upload New File</u>.   </li>
            <li>ii. At <strong>Link / File Location</strong>, <em>select</em> the folder within the Admin &gt; File Manager where the file will be uploaded to.  </li>
            <li>iii. At <strong>Link / File Name</strong>, <em>click</em> Browse.  </li>
            <li>iv. <em>Locate</em> the required file and <em>double click</em> on the file name to select.   </li>
            <li>v. <em>Click</em> <u>Save Uploaded File</u>.</li>
        </ol>
        </li>
    </ol>
    </li>
    <li>At <strong>Track Number Of Times This Link Is Clicked? </strong>(optional), <em>check </em>the check box if required. Selecting this option will display the number of times this link is click on this screen once the record has been updated.  </li>
    <li>At <strong>Log The User, Date, And Time For Every Link Click</strong> (optional), <em>check </em>the check box if required. Selecting this option will add a Link Log to this screen once the record has been updated.   </li>
    <li>At <strong>Open Link In New Browser Window?</strong> (optional), <em>check </em>the check box to display the link in a new web browser. If the box is unchecked the link will open in the existing web browser.  </li>
    <li>At <strong>Expires</strong> (optional) <em>click</em> <u>Calendar</u> and select the expiry date for the Announcement.  </li>
    <li>In the <strong>View Order</strong> field (optional), <em>enter</em> a number to indicate the position of this announcement. For example, <strong>0</strong> = First announcement; <strong>1</strong> = Second announcement, etc. This setting overrides the default order. By default, announcements are order from the most recently added or updated to the least recently updated or added.   </li>
    <li><em>Click</em> <u>Update</u>.</li>
</ol>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditBaners','Help information Edit Baners', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditBanners','Help information Edit Banners', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditContacts','The Contacts module renders contact information for a group of people, for example a project team. Records are displayed alphabetically by Contact Name and each contact record includes the contact''s name, role, email address and two telephone numbers.<br />
<br />
A contact''s email address automatically renders as a ''mailto'' hyperlink which is cloaked to prevent harvesting by spam bots.<br />
<br />
<p>Add a new contact to the Contacts module.</p>
<ol>
    <li> 						<em>Add</em> a <strong>Contacts</strong> module, or <em>go to</em> an existing <strong>Contacts</strong> module.   </li>
    <li> 						<em>Select </em> 						<strong>Add New Contact </strong>from the module menu.  </li>
    <li>In the <strong>Name</strong><strong></strong>field, <em>enter</em> the contact name. E.g. Jill Black.   </li>
    <li>In the <strong>Role</strong> field (optional), <em>enter</em> the contact''s role within the group. E.g. Company Secretary.  </li>
    <li>In the <strong>Email</strong> field (optional), <em>enter</em> the contact''s email address. E.g. <a href="mailto:Jill.Black@domain.com">Jill.Black@domain.com</a></li>
    <li>In the <strong>Telephone 1 </strong>field (optional), <em>enter</em> the contact''s primary telephone number.  </li>
    <li>In the <strong>Telephone 2</strong> field (optional), <em>enter</em> the contact''s secondary telephone number.  </li>
    <li> 						<em>Click</em> 						<u>Update</u>.</li>
</ol>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditDirectory','Help information Edit Directory', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditDocs','The Documents Module produces a list of documents with links to view (depending on a user''s file associations) or download the document. Documents can be located within the site, or can be a link to a document on another web site. Each document listing displays a document title and category.<br><br>

The name of the authorized user who last updated the document, the date that the document was added or last updated, and the file size (internal documents only) are automatically rendered for each listing. Link tracking and logging are also available.<br><br>
View the document associated with a document record.<br><br>

   1. Click the linked title of the document. The document will be displayed in either the current or a new web site browser.<br><br>
Enables downloading of a listed document.<br><br>

   1. Click Download located beside the required document.<br>
   2. Click the Save button.<br>
   3. Navigate to the location where the file will be saved.<br>
   4. Click the Save button.<br>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditEvents','The Events module displays upcoming events as a list in chronological order or in calendar format. Each event listing includes a title, description, and date. Display of an image and the event time is optional.<br><br>

Each event can be set to automatically expire on a particular date, or re-occur by any specified number of days, weeks, months or years. Height and width properties for calendar cells can be set.<br><br>
<OL>
<LI><I>Select&nbsp;</I><STRONG>Add New Event</STRONG> from the module menu. 
<LI>In the <STRONG>Title</STRONG> field, <I>enter</I> a title for the event. 

<LI>In the <STRONG>Description</STRONG> field, <I>enter</I> a description of the event. 
<LI>At <STRONG>Image</STRONG> (optional), <I>perform</I> one of the following: 
<OL type=a>
<LI>If the image has already been uploaded to the Admin &gt; File Manager: 

<OL type=1>
<LI>At <STRONG>Image / File Location</STRONG>, <I>select</I> the folder&nbsp;where the image is located. 
<LI>At <STRONG>Image / File Name</STRONG>, <I>select</I> the image from the drop down list.</LI></OL>
<LI>If the file has not been uploaded to the Admin &gt; File Manager: 

<OL type=1>
<LI><I>Click</I> <U>Upload New<STRONG> </STRONG>File</U>. 
<LI>At <STRONG>Image / File Location</STRONG>, <I>select</I> a folder to upload the image to. 
<LI>At <STRONG>Image / File Name</STRONG>, <I>click</I> <STRONG>Browse</STRONG>. 

<LI><I>Locate</I> and <I>select&nbsp;</I>the required image. 
<LI><I>Click</I> <U>Upload New File</U>.</LI></OL></LI></OL>
<LI>In the <STRONG>Alternate Text</STRONG> (required field when <STRONG>Image</STRONG> is set) field, <I>enter</I> the text that will appear as a ''tool tip'' when the image is moused over by a visitor. 

<LI>At <STRONG>Occurs Every</STRONG> (optional), <I>select</I> how frequently the event reoccurs. Select either <STRONG>Days</STRONG>, <STRONG>Weeks</STRONG>, <STRONG>Months</STRONG>, <STRONG>Years</STRONG> from the dropdown box and enter a number to the left. Please Note: reoccurrence only works in calendar display (see 1.) In list display only the start date is displayed. 
<LI>At <STRONG>Start Date</STRONG>, <I>click</I> <U>Calendar</U> and select the start date for the event. 

<LI>At <STRONG>Time</STRONG> (optional), <I>enter</I> a time in this format: 09:00 AM. 
<LI>At <STRONG>Expiry Date</STRONG> (optional), <I>click</I> <U>Calendar</U> and then select the expiry date for the event. The event will expire on this date. 
<LI><I>Click</I> <U>Update</U>.</LI></OL>

<P><STRONG>Note</STRONG>: If the Event module is set to Calendar view, the calendar will display the current month, not the month of the new event.</P>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditFAQ','<h1>About The FAQs Module</h1>

<p>The FAQs Module allows Authorized Users to manage a list of Frequently Asked Questions and their corresponding answers. The question is displayed as a link, requiring the user to click on a question to view the corresponding answer.</p>

<h1>Add an FAQ</h1>

<ol><li>Add an <b>FAQs</b> module, or go to an existing <b>FAQs</b> module. </li>
<li>Click <b>Add New FAQ</b>. </li>
<li>At <b>Question</b>, enter the question.</li>
<li>At <b>Answer</b>, enter an answer to the question.</li>

<li>Click <b>Update</b>.</li></ol>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditFile','Help information Edit File', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditHTML','The Text/HTML module renders plain text, rich text or HTML. The module provides the following five different editing views:<br />
<br />
Basic Text Box / Text<br />
<br />
Use this view to enter basic text and hand code simple HTML. The default text style is the Normal style, as set in the style sheet. Hitting the Enter key creates a new paragraph. Do not paste HTML into this view.<br />
<br />
<span class="SubHead">Add Basic Text To The Text/HTML Module</span><br />
<span class="Normal">Add basic text including basic html into the Basic Text Box.
<ol>
    <li><em>Add</em> a <strong>Text/HTML</strong> module or <em>go to</em> an existing <strong>Text/HTML</strong> module. </li>
    <li><em>Select </em><strong>Edit Text</strong> from the module menu. </li>
    <li><em>Select</em> the <strong>Basic Text Box</strong> above the Rich Text Editor. </li>
    <li><em>Select</em><strong>Text</strong> below the text box. </li>
    <li><em>Type</em> or <em>paste</em> in the text. Hit <strong>Enter</strong> to create a line break. HTML tags can be used. </li>
    <li>In the <strong>Search Summary</strong> field (optional), <em>enter</em> a summary to help the <strong>Search</strong> module and Search skin object search this content. </li>
    <li><em>Click</em><u>Preview</u> (optional) to preview to text at any time. </li>
    <li><em>Click</em><u>Update</u>.</li>
</ol>
</span><br />
<span class="Normal">
<p>Add Rich Text to the Text/HTML module.</p>
<ol type="1">
    <li><em>Add</em> a <strong>Text/HTML</strong> module or <em>go to</em> an existing <strong>Text/HTML</strong> module.  </li>
    <li><em>Select </em><strong>Edit Text</strong> from the module menu.   </li>
    <li><em>Select</em> <strong>Rich Text Editor</strong> (located above the window).  </li>
    <li><em>Select</em> <strong>Design</strong> (located below the window).  </li>
    <li><em>Enter</em> your text and add images, links, etc. See Standard Modules &gt; Working with Common Module Tools &gt; Rich Text Editor for more details.   </li>
    <li>In the <strong>Search Summary</strong> field (optional), <em>enter</em> a summary to help the Search module and Search skin object search this content.  </li>
    <li><em>Click</em> <u>Preview</u> (optional) to preview to text at any time.  </li>
    <li><em>Click</em> <u>Update</u>.</li>
</ol>
</span>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditIFrame','The IFrame module displays content from another site within a frame on the site. It is good ''netiquette'' (internet etiquette) to request permission of a site owner before framing their content.<br />
<br />
<span class="Normal">Set the web page to be framed.
<ol type="1">
    <li><em>Select </em><strong>Edit IFrame Properties</strong> from the module menu.  </li>
    <li>At <strong>Source</strong>, <em>enter</em> the URL of the content to be displayed. E.g. <a href="http://www.byte.com.au/">http://www.byte.com.au/</a></li>
    <li>At <strong>Width</strong>, <em>enter</em> the width for the IFrame in either pixels or as a percentage. Note: 790 is the largest pixel width recommended for an IFrame.   </li>
    <li>At <strong>Height</strong>, <em>enter</em> the height of the IFrame in pixels.  </li>
    <li>In the <strong>Title</strong> field, <em>enter</em> a title for the IFrame.  </li>
    <li>At <strong>Scrolling</strong>, <em>set</em> the scroll bar properties from the following options:
    <ol type="a">
        <li><strong>Auto</strong>: A scroll bar will be displayed only when scrolling is required.  </li>
        <li><strong>Yes</strong>: A scroll bar will be displayed at all times.  </li>
        <li><strong>No</strong>: No scroll bar is displayed. If <strong>No</strong> is selected ensure that the height and width settings for the IFrame are big enough to display the desired content as users will not be able to scroll.</li>
    </ol>
    </li>
    <li>At <strong>Border</strong>, <em>set</em> the scroll bar properties from the following options:
    <ol type="a">
        <li><strong>Yes: </strong>Displays a border around the IFrame   </li>
        <li><strong>No</strong>: No border is displayed. </li>
    </ol>
    </li>
    <li><em>Click </em><u>Update</u>. <br />
    </li>
</ol>
</span>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditImage','The Image module renders an image that has been uploaded to the Admin > File Manager. The height and width of images is exposed on the edit page, permitting Authorized User to alter the size of the rendered image.<br><br>
<span class="Normal"><p>Use only alpha-numerical characters when naming images. These can be either lower or upper case. You may use an underscore ( _ ), or a hyphen (-) between characters. A space can be left between characters, however this is not recommended. For example, the following naming conventions are permitted: </p>
<ul>
<li>image1.jpg 
</li><li>image-1.jpg 
</li><li>image_1.jpg 
</li><li>image 1.jpg</li></ul>
<p>The following characters cannot be used in images names, to do so will cause a Server Error in the Free Text Box used in the Text/HTML module:</p>
<p>~&nbsp;&nbsp;&nbsp; !&nbsp;&nbsp; @&nbsp;&nbsp; #&nbsp;&nbsp; $&nbsp;&nbsp; %&nbsp;&nbsp; ^&nbsp;&nbsp; &amp;&nbsp;&nbsp; *&nbsp;&nbsp; (&nbsp;&nbsp; )&nbsp;&nbsp; +&nbsp;&nbsp; =&nbsp;&nbsp; ‘&nbsp;&nbsp; {&nbsp;&nbsp; }&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ]&nbsp;&nbsp; [&nbsp;&nbsp; ”&nbsp;&nbsp; :&nbsp;&nbsp; ;&nbsp;&nbsp; ‘&nbsp;&nbsp; ?&nbsp;&nbsp; &gt;&nbsp;&nbsp; &lt;&nbsp;&nbsp; ,&nbsp;&nbsp; /</p>

<p>Failure to adhere to the correct naming convention will result in a script error when using the Insert Image tool in the Rich Text Editor on the Text/HTML module.</p></span>

<span class="SubHead">Display An Internal Image </span><br>
<span class="Normal"><ol type="1">
<li><i>Add</i> an <strong>Image</strong> module, or <i>go to</i> an existing <strong>Image</strong> module. 

</li><li><i>Select </i><strong>Edit Image Options</strong> from the module menu. 
</li><li>At <strong>Link Type</strong>, <i>select</i> <strong>File (An Image on Your Site)</strong>. 
</li><li>At <strong>Image</strong>, <i>perform</i> one of the following: 

<ol type="a">
<li>If the image has already been uploaded to the Admin &gt; <strong>File Manager</strong>: 
<ol type="1">
<li>At <strong>Image / File Location</strong>, <i>select</i> the folder&nbsp;where the image was uploaded. 
</li><li>At <strong>Image / File Name</strong>, <i>select</i> the image from the drop down list.</li></ol>

</li><li>If the file has not been uploaded to the Admin &gt; <strong>File Manager</strong>: 
<ol type="1">
<li><i>Click</i> <u>Upload New File</u>. 
</li><li>At <strong>Image / File Location</strong>, select the folder within where the image will be uploaded to. 
</li><li>At <strong>Image / File Name</strong>, <i>click</i> <strong>Browse</strong>. 

</li><li><i>Locate</i> and <em>select</em> the required image. 
</li><li><i>Click</i> <u>Save Uploaded File</u>.</li></ol></li></ol>
</li><li>At <strong>Alternate Text</strong>, <i>enter</i> the text that will appear as a ''tool tip'' upon mouse over. 

</li><li>At <strong>Width</strong> (optional), <i>set</i> the width of the image by entering a pixel value. E.g. 100. This will override the actual width of the image. If no value is entered the image will be rendered as its actual size. 
</li><li>At <strong>Height</strong> (optional), <i>set</i> the height of the image by entering a pixel value. E.g. 100. This will override the actual width of the image. If no value is entered the image will be rendered as its actual size. 
</li><li><i>Click</i> <u>Update</u>.</li></ol></span>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditLinks','The Links module renders a list of links to any page or file on your portal; or to an external URL. The links can be set to display in either vertically (default), horizontally, or as a drop down list. Links appear in alphabetical order by default, or can be reordered. A description of the link can be set to appear either as a tool tip upon mouse over, or as an Info Link that is displayed as a leader dot link (E.g. ...) when the ... link is clicked.<br />
<br />
<h3> SET UP THE LINK OPTIONS :</h3>
<br />
List Type : Select the type of listing you wish for the links.&nbsp; <br />
&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; The options are available are List, Drop down menu, vertical or horizontal.<br />
<br />
Link information : Select this option to show the information that you will enter in the description<br />
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; field when you will edit the link.<br />
<br />
CSS Class&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp; The CSS Class that will be used for the link.<br />
<br />
<br />
<h3> EDIT A LINK :</h3>
<br />
Title :&nbsp; Enter a title.<br />
<br />
Select one of the options :<br />
<ul>
    <li>External link :&nbsp; You must point to a valid URL</li>
    <li>Internal link :&nbsp; Select a internal page from the menu</li>
    <li>File Link :&nbsp; Select a file from the portal.&nbsp; You can also upload a file.</li>
</ul>
<br />
Description : Enter a description.<br />
<br />
Order :&nbsp; Enter the order for the link in the list.<br />
<br />
Open in a new window :&nbsp; Select if you wish the link to open in a new window.<br />
<br />
Click <a href="" class="CommandButton" id="edit_cmdUpdate">Update</a> to save you setting and return or&nbsp;<a href="" class="CommandButton" id="edit_cmdCancel">Cancel</a>&nbsp;to cancel and return.<br />
<br />
<hr style="width: 100%; height: 2px;" />
<br />
You can see how many time a link was click by selecting the See Stats? option.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditMapGoogle','The module, use the Google Maps API in order to show a map.<br />
<br />
Before you start you need to register to GoogleMap and get an API.&nbsp; Click&nbsp; <a href="http://www.google.com/apis/maps/signup.html" class="CommandButton" id="edit_hypgetAPI">Get Google API</a>.<br />
<br />
<br />
Enter the information to seek the map.&nbsp; Once the Adresse you wish to show is entered, you can generate a Lat and Long.<br />
<br />
Select the various options for the map and generate a script.&nbsp; You can edit the script to personalise the map if need be.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditRole','Enter the Security role name and a brief description of the Role.<br />
Also you can control the access to file upload for this account.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditRSS','<h1>About The News Feeds (RSS) Module</h1>
<p><strong>RSS</strong> (Really Simple Syndication) is a family of <a title="Web feed" href="http://en.wikipedia.org/wiki/Web_feed">Web feed</a> formats used to publish frequently updated content such as <a title="Blog" href="http://en.wikipedia.org/wiki/Blog">blog</a> entries, news headlines or <a title="Podcasts" href="http://en.wikipedia.org/wiki/Podcasts">podcasts</a>. An RSS document, which is called a &quot;feed,&quot; &quot;web feed,&quot; or &quot;channel,&quot; contains either a summary of content from an associated web site or the full text. RSS makes it possible for people to keep up with their favorite web sites in an automated manner that''s easier than checking them manually.</p>
<p>RSS content can be read using <a title="Software" href="http://en.wikipedia.org/wiki/Software">software</a> called an &quot;RSS reader,&quot; &quot;feed reader&quot; or an &quot;<a title="Aggregator" href="http://en.wikipedia.org/wiki/Aggregator">aggregator</a>.&quot; The user subscribes to a feed by entering the feed''s link into the reader or by clicking an RSS icon in a browser that initiates the subscription process. The reader checks the user''s subscribed feeds regularly for new content, downloading any updates that it finds.</p>
<p>The initials &quot;RSS&quot; are used to refer to the following formats:</p>
<ul>
    <li><strong>R</strong>eally <strong>S</strong>imple <strong>S</strong>yndication (RSS 2.0)</li>
    <li><a title="Resource Description Framework" href="http://en.wikipedia.org/wiki/Resource_Description_Framework">RDF</a> Site Summary (RSS 1.0 and RSS 0.90)</li>
    <li>Rich Site Summary (RSS 0.91)</li>
</ul>
<p>RSS formats are specified using <a title="XML" href="http://en.wikipedia.org/wiki/XML">XML</a>, a generic specification for the creation of data formats.</p>
<h1>Edit News Feed</h1>
<ol>
    <li>Add a <strong>News Feed</strong> module, or go to an existing <strong>News Feed</strong> module.</li>
    <li>Click <strong>Edit Newsfeed</strong>.  </li>
    <li>At <strong>News Feed Source</strong>, select <strong>Internal</strong> and <strong>Upload New File</strong>, or select <strong>External</strong> and enter the Url.  </li>
    <li>At <strong>News Feed Style Sheet</strong> (optional), select <strong>Internal</strong> and <strong>Upload New File</strong>, or select <strong>External</strong> and enter the Url.   </li>
    <li>At <strong>Security Options</strong> (optional), enter the <strong>domain\username</strong> and <strong>password</strong> details provided by your News Feed provider.  </li>
    <li>Click <strong>Update</strong>.</li>
</ol>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditSearch','Add field and modules you want the seach module to search in.<br />
<br />
You can personalise the way the result will show.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditUserDefinedTable','The User Defined Table (UDT) module enables the creation of a table which can be populated with  data.  Data types includes text, integer, decimal, date and time, date, time, true/false, email address, URL, currency and image. Columns can be set as required (mandatory) and can be made visible to Administrators only.<br><br>

The heading of each column is a link can be clicked to reorder data A-Z or Z-A. Table settings enable the Administrator to set the sort order of data and the number of items per page. Administrators can also set the maximum width of images and whether to display http:// before URLs.<br><br>
<span class="SubHead">Add UDT Columns</span><br>
<span class="Normal">
		<p>Create a user defined table by adding one or more columns. A table must be created before content can be added.</p>
		<ol type="1">
				<li>
						<i>Add</i> a <strong>User Defined Table</strong> module, or <i>go to</i> an existing <strong>User Defined Table</strong> module. 

</li>
				<li>
						<em>Select</em>
						<strong>Manage User Defined Table</strong> from the module menu. 
</li>
				<li>
						<i>Click</i>
						<u>Add New Column</u> to add a table column. 

</li>
				<li>At <strong>Required</strong>, select from the following options: 
<ol type="a"><li><i>Check <img alt="check.gif" src="/Portals/25/OnlineHelp/Images/Icons/check.gif" border="0" height="13" width="13">&nbsp;</i>the check box if entering data into this field is mandatory. 
</li><li><em>Unchecked <img alt="uncheck.gif" src="/Portals/25/OnlineHelp/Images/Icons/uncheck.gif" border="0" height="13" width="13">&nbsp;</em>the check box if entering data into this field is optional. This is the default setting.</li></ol></li>
				<li>At <strong>Visible</strong>, select from the following options: 
<ol type="a"><li><i>Check <img alt="check.gif" src="/Portals/25/OnlineHelp/Images/Icons/check.gif" border="0" height="13" width="13">&nbsp;</i>the check box if column is visible to all authorized users. This is the default setting. 
</li><li><i>Unchecked <img alt="uncheck.gif" src="/Portals/25/OnlineHelp/Images/Icons/uncheck.gif" border="0" height="13" width="13">&nbsp;</i>the check box if the column is only visible to Administrators.</li></ol></li>

				<li>In the <strong>Title</strong> field, <i>enter</i> a title for the column. (Column titles are listed across the top of the table). 
<ul></ul></li>
				<li>At <strong>Type</strong>, <i>select</i> the type of data that can be entered into and displayed for the field. The following options are available: 
<ol type="a"><li><strong>Text</strong>: Enter and display any keyboard characters. 

</li><li><strong>Integer</strong>: Enter and display a whole number. E.g. 8 
</li><li><strong>Decimal</strong>: Enter and display a decimal number. E.g. 8.50 
</li><li><strong>Date and Time</strong>: Displays a selected date and an entered time. E.g. 6/30/2006 9:00 AM 
</li><li><strong>Date</strong>: Display a selected date. E.g. 6/30/2006 
</li><li><strong>Time:</strong> Display an entered time. E.g. 12:30 PM 
</li><li><strong>True/False</strong>: Enter and display a true or false statement. E.g. False. 
</li><li><strong>Email</strong>: Enter and display a valid email address. E.g. <a href="mailto:john.doe@domain.com">john.doe@domain.com</a></li><li><strong>Currency</strong>: Enter and display a currency amount. E.g. 9.00 GBP. See Site Settings to learn how to set the currency type displayed. 

</li><li><strong>URL</strong>: Enter and display a valid URL. E.g. <a href="http://www.domain.com/">http://www.domain.com</a></li><li><strong>Image</strong>: Displays an image that has been uploaded to the web site.</li></ol></li>
				<li>At <strong>Default</strong> (optional), enter the default value for the column. This value will be automatically displayed in this field when entering data to the table. 
</li>
				<li>
						<em>Click</em> the <strong>Save</strong><img alt="Save.gif" src="/Portals/25/OnlineHelp/Images/Icons/Save.gif" border="0" height="18" width="18"> button. 

</li>
				<li>
						<i>Repeat</i> Steps 3-9 to add additional columns. 
</li>
				<li>
						<i>Click </i>
						<u>Back</u> to return to the module. </li>

		</ol></span>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditVendors','<span class="SubHead">Add A Vendor</span><br>
<span class="Normal"><p>Adds a vendor account to the Vendor direction.&nbsp; Once a new vendor is created the following additional settings will be available vendor logo, authorization, classifications, banner advertising, and affiliate referrals.&nbsp; To complete these additional fields, you must edit the vendor record.</p>
<p>Mandatory fields are marked with an asterisk (*).&nbsp; Where a check box is displayed beside a field,&nbsp; <i>uncheck</i> the check box to make the field optional or <i>check</i> the checkbox to make the field mandatory.</p>

<ol type="1">
<li><i>Navigate</i> to the Admin &gt; <strong>Vendors</strong> Page. 
</li><li>Select <strong>Add New Vendor</strong> from the module menu, or<em> click</em> <u>Add New Vendor</u> at the base of the module. 

</li><li>In the <strong>Vendor Details</strong> section, complete all fields: 
<ol type="a">
<li>In the <strong>Company*</strong> field, <i>enter</i> the company name of the vendor. 
</li><li>In the <strong>First Name*</strong> field, <i>enter</i> the first name of the contact person for the vendor. 

</li><li>In the <strong>Last Name*</strong> field, <i>enter</i> the last name of the contact person for the vendor. 
</li><li>In the <strong>Email Address*</strong> field, <i>enter</i> the email address of the contact person listed above.</li></ol>
</li><li>In the <strong>Address Details</strong> section, the following optional fields are available: 

<ol>
<li>In the <strong>Street</strong> field, <i>enter</i> the street part of the Vendor''s address.&nbsp; E.g. 10 Main Road 
</li><li>In the <strong>Unit</strong> field, <i>enter</i> the unit number.&nbsp; E.g. Unit 6, or Suite 6, etc. 

</li><li>In the <strong>City</strong> field, <i>enter</i> the Vendor''s city. E.g. Melbourne 
</li><li>In the <strong>Country</strong> field, <i>select</i> the Vendor''s country. 
</li><li>In the <strong>Region</strong> field, <i>enter</i> the Region/State/Province of the Vendor, or select from the drop down list where available.&nbsp;&nbsp; (For more details on creating regions for countries, See Host &gt; Lists). 

</li><li>In the <strong>Postal Code/Zip Code</strong> field, <i>enter</i> the Vendor''s postal code. E.g. 31234 
</li><li>In the <strong>Telephone</strong> field, <i>enter</i> the Vendor''s telephone number.&nbsp; E.g. +61 3 9421 6555 
</li><li>In the <strong>Cell</strong> field, <i>enter</i> the Vendor''s cell (mobile) number. E.g. 0400 100 100 

</li><li>In the <strong>Fax</strong> field, <i>enter</i> the Vendor''s facsimile number. E.g. + 61 3 9421 6444</li></ol>
</li><li>In the <strong>Other Details</strong> section, the following optional field is available: 
<ol>
<li>In the <strong>Website</strong> field, <i>enter</i> the Vendor''s website address. E.g. <a href="http://www.byte.com.au">www.byte.com.au</a>.</li></ol>

</li><li><i>Click</i> <u>Update</u>.</li></ol></span>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_EditXML','<h1>About the XML/XSL Module</h1>
<p>The XML/XSL Module renders the result of an XML/XSL transform. <br />
</p>
XML module is working in three steps;
<ol>
    <li> 				it queries <a href="http://en.wikipedia.org/wiki/XML">XML</a>  			Data, 			</li>
    <li> 				transforms the XML data using an <a href="http://en.wikipedia.org/wiki/XSLT">XSL  					transformation</a>, 			</li>
    <li> 				and returns the result back to the user. 				<br />
    </li>
    <li> 				All settings are stored inside DotNetZoom. 			</li>
</ol>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_FileManager','The File Manager manages the upload and maintenance of files within the portal.&nbsp;', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Forum','<span class="SubHead">Add a Forum Post</span><br />
<span class="Normal">
<p>A Forum entry (sometimes referred to as a post) is added to a Forum List. </p>
<ol type="1">
    <li><em>Click</em> on the name of the <strong>Forum List</strong> for the post. </li>
    <li><em>Click</em> the <strong>New Thread</strong>&nbsp;<input class="button" id="cmdNewTopic1" title="Add a new post" type="submit" name="cmdNewTopic1" value="Add new thread" /> button. </li>
    <li>In the <strong>Subject</strong> field, enter the subject of the thread.&nbsp; This will be the title of the post as it appears on the site.&nbsp; Choose a subject that best describes the purpose and content of the post. </li>
    <li>In the <strong>Rich Text Enter</strong>, <em>enter</em> the content of the post. </li>
    <li>At <strong>Attachments</strong>, <em>click </em>the <strong>Search</strong> button to add an attachment. </li>
    <li>At <strong>Pinned</strong>, <em>check</em> to ''pin'' the post above existing posts for this Forum List - OR - <em>uncheck</em> if the post will appear next in the list. </li>
    <li>At <strong>Notification</strong>, <em>check</em> to be notified when a reply is made to this post - OR - <em>uncheck</em> to disable notification. </li>
    <li>At <strong>Locked?</strong>, <em>check</em> to prevent others from replying to this post or <em>uncheck </em>to permit replies. </li>
    <li><em>Click</em> <u>Update</u>.&nbsp; You can now edit the Forum to add attachments. </li>
</ol>
</span><br />
<span class="SubHead">Delete A Forum Post</span><br />
<span class="Normal"><a name="_Toc107041682">Permanently</a> delete a forum post.
<ol type="1">
    <li><em>Locate</em> the post entry using the <strong>Forum</strong> menu, or by using the <u>Search</u>. </li>
    <li><em>Click</em> on the post subject. </li>
    <li><em>Click</em> the <strong>Delete</strong> <input class="button" id="delete" title="Delete post" type="submit" name="delete" value="Delete" />&nbsp;button. </li>
    <li><em>Click</em> <u>Delete</u>. A dialog box asking ''Are You Sure You Wish To Delete This Item?'' will be displayed. </li>
    <li><em>Click</em> <strong>OK</strong> to confirm deletion. </li>
</ol>
</span><br />
<span class="SubHead">Edit A Forum Post</span><br />
<span class="Normal">Users can edit their own forum posts. Administrators can edit all posts.
<ol type="1">
    <li><em>Locate</em> the post entry using the <strong>Forum</strong> menu, by using the <u>Search</u>. </li>
    <li><em>Click</em> on the post subject. </li>
    <li><em>Click</em> the <strong>Edit </strong>button. </li>
    <li><em>Edit</em> any fields as required. </li>
    <li><em>Click</em> <u>Update</u>. </li>
</ol>
</span><br />
<span class="SubHead">Quote A Forum Post</span><br />
<span class="Normal">
<p>Users can quote an existing forum post.&nbsp; Quoting a post adds the name of the poster and the content of the post to this new post. </p>
<ol type="1">
    <li><em>Locate</em> the post entry using the <strong>Forum</strong> menu, by using the <u>Search</u>. </li>
    <li><em>Click</em> on the post subject. </li>
    <li><em>Click</em> the <strong>Quote <input type="submit" name="quote" value="Quote" id="quote" title="Quote this post" class="button" />trong>&nbsp;button. </li>
    <li>Add your content. </li>
    <li><em>Click</em> <u>Update</u>. </li>
</ol>
</span><br />', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ForumAdmin','Help information Admin forum', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ForumAnonymous','You need to register in order to be able to edit and post messages.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ForumSearch','Help information forum search module', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ForumSettings','Help information Forum settings', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ForumSubscribe','Help information Forum subscription', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ForumUserAdmin','Help information Admin Forum', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ForumUserProfile','Help information user profile', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_GalleryAdmin','Help information about the Gallery administration', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_HostSettings','Set the hosting space allocated to new child sites.<br>
 Configure all settings for child hosting. <br>
 Displays the configuration details for the site.  <br>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language0','Edit Erase or Create a new language', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language1','Edit help files', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language10','Help settings for language', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language2','Edit language files', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language3','Edit country codes', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language4','Edit the admin menu information', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language5','Edit currencies code to use with Pay-Pal', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language6','Edit frequency code for subscription', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language7','Edit site log options', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language8','Edit the region code associated with a country code', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Language9','Edit time Zones information', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ManageTabs','Menu use to navigate, edit, view and manage site pages', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_MAnageUserDefinedTable','<span class="SubHead">Add UDT Columns</span><br>
<span class="Normal">
		<p>Create a user defined table by adding one or more columns. A table must be created before content can be added.</p>
		<ol type="1">
				<li>
						<i>Add</i> a <strong>User Defined Table</strong> module, or <i>go to</i> an existing <strong>User Defined Table</strong> module. 

</li>
				<li>
						<em>Select</em>
						<strong>Manage User Defined Table</strong> from the module menu. 
</li>
				<li>
						<i>Click</i>
						<u>Add New Column</u> to add a table column. 

</li>
				<li>At <strong>Required</strong>, select from the following options: 
<ol type="a"><li><i>Check <img alt="check.gif" src="/Portals/25/OnlineHelp/Images/Icons/check.gif" border="0" height="13" width="13">&nbsp;</i>the check box if entering data into this field is mandatory. 
</li><li><em>Unchecked <img alt="uncheck.gif" src="/Portals/25/OnlineHelp/Images/Icons/uncheck.gif" border="0" height="13" width="13">&nbsp;</em>the check box if entering data into this field is optional. This is the default setting.</li></ol></li>
				<li>At <strong>Visible</strong>, select from the following options: 
<ol type="a"><li><i>Check <img alt="check.gif" src="/Portals/25/OnlineHelp/Images/Icons/check.gif" border="0" height="13" width="13">&nbsp;</i>the check box if column is visible to all authorized users. This is the default setting. 
</li><li><i>Unchecked <img alt="uncheck.gif" src="/Portals/25/OnlineHelp/Images/Icons/uncheck.gif" border="0" height="13" width="13">&nbsp;</i>the check box if the column is only visible to Administrators.</li></ol></li>

				<li>In the <strong>Title</strong> field, <i>enter</i> a title for the column. (Column titles are listed across the top of the table). 
<ul></ul></li>
				<li>At <strong>Type</strong>, <i>select</i> the type of data that can be entered into and displayed for the field. The following options are available: 
<ol type="a"><li><strong>Text</strong>: Enter and display any keyboard characters. 

</li><li><strong>Integer</strong>: Enter and display a whole number. E.g. 8 
</li><li><strong>Decimal</strong>: Enter and display a decimal number. E.g. 8.50 
</li><li><strong>Date and Time</strong>: Displays a selected date and an entered time. E.g. 6/30/2006 9:00 AM 
</li><li><strong>Date</strong>: Display a selected date. E.g. 6/30/2006 
</li><li><strong>Time:</strong> Display an entered time. E.g. 12:30 PM 
</li><li><strong>True/False</strong>: Enter and display a true or false statement. E.g. False. 
</li><li><strong>Email</strong>: Enter and display a valid email address. E.g. <a href="mailto:john.doe@domain.com">john.doe@domain.com</a></li><li><strong>Currency</strong>: Enter and display a currency amount. E.g. 9.00 GBP. See Site Settings to learn how to set the currency type displayed. 

</li><li><strong>URL</strong>: Enter and display a valid URL. E.g. <a href="http://www.domain.com/">http://www.domain.com</a></li><li><strong>Image</strong>: Displays an image that has been uploaded to the web site.</li></ol></li>
				<li>At <strong>Default</strong> (optional), enter the default value for the column. This value will be automatically displayed in this field when entering data to the table. 
</li>
				<li>
						<em>Click</em> the <strong>Save</strong><img alt="Save.gif" src="/Portals/25/OnlineHelp/Images/Icons/Save.gif" border="0" height="18" width="18"> button. 

</li>
				<li>
						<i>Repeat</i> Steps 3-9 to add additional columns. 
</li>
				<li>
						<i>Click </i>
						<u>Back</u> to return to the module. </li>

		</ol></span>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ManageUsers','<p>Site Administrators are able to manage the account details of all Registered Users from the User Accounts page.</p>
<p>The following tasks can be performed on the User Accounts page:</p>
<ul>
<li>Add a new User Account 
</li><li>Edit of User Account details 
</li><li>Authorize or unauthorize a User Account 
</li><li>Manage Security Role access for User Accounts 
</li><li>Delete a User Account 
</li><li>Delete all Unauthorized Users 
</li><li>View account creation and last log in date for a User</li></ul>
<p>To increase the ease of use of the User Accounts page a number of options are available for manipulating the view of the user accounts on display. These options include:</p>
<ul>

<li>Adjustable number of user accounts to be displayed per page 
</li><li>Filtering by Alphabetic, Unauthorized and All user accounts 
</li><li>Search to find a user by username or email address</li></ul>
<p>The User Account page is separate to the User Account module that enables Registered Users to manage their account details and Membership Services.</p>', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ModerateAdmin','Information about the parameters for moderators in the forum', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ModuleDefinitions','The Module Definitions page allows the Host to set one or more modules as Premium, manage module properties and upload new modules.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ModuleDefs','The Module Definitions page allows the Host to set one or more modules as Premium, manage module properties and upload new modules.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_ModuleSettings','Settings relating to the Module content and permissions.  E.g. Those settings that will be the same on all pages that the Module appears.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_MyBuddiesModule','Information about my buddies module', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_NeedToLogin','Either you are not currently logged in, or you do not have access to this content.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_PMSCompose','You can write your messages using an online editor.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_PMSInbox','You view and manage your received messages.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_PMSOutbox','You can view and manage the messages your send to other users.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Portals','Manage existing portals and create new portals.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Privacy','You will find in this page the site privacy statement.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Register','You need to put in all information required', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_RegisterEdit','Modification of account', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_RegisterEditServices','Information about services', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Roles','To manage user roles.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Search','Just enter a word, phrase or text to seach and Click GO', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_SecurityRole','Information and ability to change the user allocated to the Security Role', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Services','Information about services available', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Signin','Information about signin in', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Signup','Create a new potal', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_SiteLog','Select the appropriate report your want also a start date and End Date.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_SiteSettings1','Skinning<br />
<br />
You can personalise the way the site will look.<br />
<br />
Edit portal CSS files and Skin files.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_SiteSettings2','General information about your site', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_SiteSettings3','Select the language and the various language specific information to be displayed on each page.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_SiteSettings4','Select the option for crating a demo portal.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_SiteSettings5','Subscription detail for the portal.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_SQL','Enter your SQL request.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Tabs','You can edit the pages order in the menu and the settings of each pages', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_TAGFileManagerModule','DisplayHelp_TAGFileManagerModule', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Terms','You will find in this page the terms and policy for this site.', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_UserListModule','DisplayHelp_UserListModule', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Users','Menu to manage User accounts', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_Vendors','Vendors listing', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_WeatherEdit','Options to select a weather panel', null 
GO
UpdatelonglanguageSetting 'en','DisplayHelp_WebUpload','Options for upload permission', null 
GO
UpdatelonglanguageSetting 'en','Edit Access Denied','Either you are not currently logged in, or you do not have access to this content.', null 
GO
UpdatelonglanguageSetting 'en','EditAccessDeniedInfo','Either you are not currently logged in, or you do not have access to this content.<br><br>', null 
GO
UpdatelonglanguageSetting 'en','email_account_mod_notice','Your account was modified

Date: {date}

First name: {firstname}
Last name: {lastname}

App.: {app}
Street: {street}
City: {city}
Province: {province}
Country: {country}
Postal Code: {postalcode}
Telephone: {phone}

Email: {email}', null 
GO
UpdatelonglanguageSetting 'en','email_new_demo_portal','<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title></title>
</head>
<body>
{FullName}<br><br>
You web site was created. Please read the following information carefully and be sure to save this message in a safe location for future reference.<br><br>
Portal Website Address: {PortalURL}<br>
Username: {Username}<br>
Password: {Password}<br><br>
For security reason you need to validate your account with this link: <a href="{validationcode}"> click to validate your account</a><br> 
<b>or you can copy the following validation URL in a your web browser :</b><br><br>
 {validationcode}<br><br> 
Thank you.<br><br>
<a href="mailto:{HostEmail}?subject={PortalName} New web site">{HostEmail}</a>
</body>
</html>', null 
GO
UpdatelonglanguageSetting 'en','email_new_portal','<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title></title>
</head>
<body>
{FullName}<br><br>
You web site was created. Please read the following information carefully and be sure to save this message in a safe location for future reference.<br><br>
Portal Website Address: {PortalURL}<br>
Username: {Username}<br>
Password: {Password}<br><br>
For security reason you need to validate your account with this link: <a href="{validationcode}"> click to validate your account</a><br> 
<b>or you can copy the following validation URL in a your web browser :</b><br><br>
 {validationcode}<br><br> 
Thank you.<br><br>
<a href="mailto:{HostEmail}?subject={PortalName} New web site">{HostEmail}</a>
</body>
</html>', null 
GO
UpdatelonglanguageSetting 'en','email_newuser_account','Dear {FullName},

We are pleased to advise that you have been added as a Registered User to {PortalName}. Please read the following information carefully and be sure to save this message in a safe location for future reference.

Portal Website Address: {PortalURL}
Username: {Username}
Password: {Password}
{needcode}
Validation Code:  {validationcode}
{/needcode}

Please take the opportunity to visit the website to review its content and take advantage of its many features.

{signupmessage}

Thank you, we appreciate your support...

{PortalName}
                    

{AdministratorEmail}', null 
GO
UpdatelonglanguageSetting 'en','email_newuser_notice','Date: {date}

First name: {firstname}
Nom:        {lastname}

App.:        {app}
Street:      {street}
City:        {city}
Province:    {province}
Country:     {country}
Postal Code: {postalcode}
Phone number:{phone}

Email:       {email}', null 
GO
UpdatelonglanguageSetting 'en','email_newuser_private','Dear {FullName},

Thank you for registering at {PortalName}. Please read the following information carefully and be sure to save this message in a safe location for future reference.

Portal Website Address: {PortalURL}
Username: {Username}
Password: {Password}

Your account details will be reviewed by the portal Administrator and you will receive a notification upon account activation.

{signupmessage}

Thank you.

{PortalName}

{AdministratorEmail}', null 
GO
UpdatelonglanguageSetting 'en','email_newuser_verified','Dear {FullName},

Thank you for registering at {PortalName}. Please read the following information carefully and be sure to save this message in a safe location for future reference.

Portal Website Address: {PortalURL}
Username: {Username}
Password: {Password}
Validation Code:  {validationcode}

{signupmessage}

Thank you.

{PortalName}

{AdministratorEmail}', null 
GO
UpdatelonglanguageSetting 'en','email_password_recall','Dear {FullName},

You have requested a Password Reminder from {PortalName}.

Please login using the following information:

Portal Website Address: {PortalURL}
Username:               {Username}
Password:               {Password}
{needcode}
Validation Code:        {validationcode}
{/needcode}
{notauthorized}
Status:              Non authorized
{/notauthorized}

Sincerely,
{PortalName}

*Note: If you did not request a Password Reminder, please disregard this Message.', null 
GO
UpdatelonglanguageSetting 'en','Forum_Moderated','Since you posted to a moderated forum, a forum administrator must approve your post before it will become visible.  Please be patient, this may take anywhere from a few minutes to many hours. Note that you will receive an email when your post is approved', null 
GO
UpdatelonglanguageSetting 'en','ModuleDefinitionInfo','Modules can be added using an automated installation.  In order to use this method of installation all the file associated with the module must be uploaded to the server.  Files can be uploaded individually or in a compressed zip file.  An (*.xml) script file must be included with the package you intend to install.  You may install a module manually, you have the use the "Template" option and edit the module setting once the template is created.', null 
GO
UpdatelonglanguageSetting 'en','new_portal_info','<p>{FullName}</p>
<p>Your portal was created. You will receive in the next few minutes an E-Mail with important information about your new portal.</p>
<p>By using the newly created site, you agree to respect its terms and conditions.</p>
<p>Portal URL: {PortalURL}</p>
<p>For security reason you need to validate your account by using the link provided in this E-Mail.</p>', null 
GO
UpdatelonglanguageSetting 'en','PortalCreationInfo','<b>*IMPORTANT INFORMATION:</b><br><br>
<b>Once this process completed and your site created you will receive an e-mail with a validation link</b><br><br>
<b>You will need to use this validation link in order to log-in the first time on your newly created site.</b><br><br>', null 
GO
UpdatelonglanguageSetting 'en','PortalPrivacy','<table width="80%" align="center" border="0">
    <tbody>
        <tr>
            <td><span class="ItemTitle">{PortalName} Privacy Statement</span> <br />
            <br />
{PortalName} is committed to protecting your privacy and developing 
technology that gives you the most powerful and safe online experience. This 
Statement of Privacy applies to the {PortalName} Web 
site and governs data collection and usage. By using the {PortalName} 
website, you consent to the data practices described in this statement.<br><br> 

<span class="SubHead">Collection of your Personal Information</span><br><br>
{PortalName} collects personally 
identifiable information, such as your e-mail address, name, home or work 
address or telephone number. {PortalName} also collects anonymous 
demographic information, which is not unique to you, such as your ZIP code, 
age, gender, preferences, interests and favorites.<br><br>
There is also information about your computer hardware and 
software that is automatically collected by {PortalName}. This information 
can include: your IP address, browser type, domain names, access times and 
referring Web site addresses. This information is used by {PortalName} for 
the operation of the service, to maintain quality of the service, and to 
provide general statistics regarding use of the {PortalName} Web site.<br><br> 
Please keep in mind that if you directly disclose personally identifiable 
information or personally sensitive data through {PortalName} public 
message boards, this information may be collected and used by others. Note: 
{PortalName} does not read any of your private online communications.<br><br> 
{PortalName} encourages you to review the privacy statements of Web sites 
you choose to link to from {PortalName} so that you can understand how 
those Web sites collect, use and share your information. {PortalName} is 
not responsible for the privacy statements or other content on Web sites 
outside of the {PortalName} and {PortalName} family of Web sites.<br><br>
<span class="SubHead">Use of your Personal Information</span><br><br>
{PortalName} collects and uses your personal 
information to operate the {PortalName} Web site and deliver the services 
you have requested. {PortalName} also uses your personally identifiable 
information to inform you of other products or services available from 
{PortalName} and its affiliates. {PortalName} may also contact you 
via surveys to conduct research about your opinion of current services or of 
potential new services that may be offered.<br><br>
{PortalName} does not sell, 
rent or lease its customer lists to third parties. {PortalName} may, from 
time to time, contact you on behalf of external business partners about a 
particular offering that may be of interest to you. In those cases, your unique 
personally identifiable information (e-mail, name, address, telephone number) 
is not transferred to the third party. In addition, {PortalName} may share 
data with trusted partners to help us perform statistical analysis, send you 
email or postal mail, provide customer support, or arrange for deliveries. All 
such third parties are prohibited from using your personal information except 
to provide these services to {PortalName}, and they are required to 
maintain the confidentiality of your information.<br><br>
{PortalName} does not 
use or disclose sensitive personal information, such as race, religion, or 
political affiliations, without your explicit consent.<br><br>

{PortalName} keeps 
track of the Web sites and pages our customers visit within {PortalName}, 
in order to determine what {PortalName} services are the most popular. 
This data is used to deliver customized content and advertising within 
{PortalName} to customers whose behavior indicates that they are 
interested in a particular subject area.<br><br>
{PortalName} Web sites will 
disclose your personal information, without notice, only if required to do so 
by law or in the good faith belief that such action is necessary to: (a) 
conform to the edicts of the law or comply with legal process served on 
{PortalName} or the site; (b) protect and defend the rights or property of 
{PortalName}; and, (c) act under exigent circumstances to protect the 
personal safety of users of {PortalName}, or the public.<br><br>
<span class="SubHead">Use of Cookies</span><br><br> 
The {PortalName} Web site use "cookies" to help you personalize your 
online experience. A cookie is a text file that is placed on your hard disk by 
a Web page server. Cookies cannot be used to run programs or deliver viruses to 
your computer. Cookies are uniquely assigned to you, and can only be read by a 
web server in the domain that issued the cookie to you.<br><br>
One of the primary 
purposes of cookies is to provide a convenience feature to save you time. The 
purpose of a cookie is to tell the Web server that you have returned to a 
specific page. For example, if you personalize {PortalName} pages, or 
register with {PortalName} site or services, a cookie helps 
{PortalName} to recall your specific information on subsequent visits. 
This simplifies the process of recording your personal information, such as 
billing addresses, shipping addresses, and so on. When you return to the same 
{PortalName} Web site, the information you previously provided can be 
retrieved, so you can easily use the {PortalName} features that you 
customized.<br><br>
You have the ability to accept or decline cookies. Most Web 
browsers automatically accept cookies, but you can usually modify your browser 
setting to decline cookies if you prefer. If you choose to decline cookies, you 
may not be able to fully experience the interactive features of the 
{PortalName} services or Web sites you visit.<br><br>
<span class="SubHead">Security of your Personal Information</span><br><br>
{PortalName} secures your personal information from 
unauthorized access, use or disclosure. {PortalName} secures the 
personally identifiable information you provide on computer servers in a 
controlled, secure environment, protected from unauthorized access, use or 
disclosure. When personal information (such as a credit card number) is 
transmitted to other Web sites, it is protected through the use of encryption, 
such as the Secure Socket Layer (SSL) protocol.<br><br>
<span class="SubHead">Changes to this Statement</span><br><br>

{PortalName} will occasionally update this Statement of Privacy to reflect 
company and customer feedback. {PortalName} encourages you to periodically 
review this Statement to be informed of how {PortalName} is protecting 
your information.<br><br>
<span class="SubHead">Contact Information</span><br><br>
{PortalName} welcomes your comments 
regarding this Statement of Privacy. If you believe that {PortalName} has 
not adhered to this Statement, please contact {PortalName} at {AdministratorEmail}. We 
will use commercially reasonable efforts to promptly determine and remedy the 
problem.
<br>
<br>
If you still have unresolved privacy concerns, you may contact the Privacy Commissioner of Canada at the following address:
<br>
<br>
The Office of the Privacy Commissioner of Canada<br>
112 Kent Street<br>
Ottawa, Ontario K1A 1H3<br>
1 800 282-1376<br>
www.privcom.gc.ca
            </td>
        </tr>
    </tbody>
</table>', null 
GO
UpdatelonglanguageSetting 'en','PortalTerms','<table width="80%" align="center" border="0">
    <tbody>
        <tr>
            <td><span class="ItemTitle">Terms Of Use between you and {PortalName}</span> <br />
            <br />
            The {PortalName} Web Site is comprised of various Web pages operated by {PortalName}.<br />
            <br />
            The {PortalName} Web Site is offered to you conditioned on your acceptance without modification of the terms, conditions, and notices contained herein. Your use of the {PortalName} Web Site constitutes your agreement to all such terms, conditions, and notices.<br />
            <br />
            <span class="SubHead">MODIFICATION OF THESE TERMS OF USE</span><br />
            <br />
            {PortalName} reserves the right to change the terms, conditions, and notices under which the {PortalName} Web Site is offered, including but not limited to the charges associated with the use of the {PortalName} Web Site.<br />
            <br />
            <span class="SubHead">LINKS TO THIRD PARTY SITES</span><br />
            <br />
            The {PortalName} Web Site may contain links to other Web Sites (&quot;Linked Sites&quot;). The Linked Sites are not under the control of {PortalName} and {PortalName} is not responsible for the contents of any Linked Site, including without limitation any link contained in a Linked Site, or any changes or updates to a Linked Site. {PortalName} is not responsible for webcasting or any other form of transmission received from any Linked Site. {PortalName} is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement by {PortalName} of the site or any association with its operators.<br />
            <br />
            <span class="SubHead">NO UNLAWFUL OR PROHIBITED USE</span><br />
            <br />
            As a condition of your use of the {PortalName} Web Site, you warrant to {PortalName} that you will not use the {PortalName} Web Site for any purpose that is unlawful or prohibited by these terms, conditions, and notices. You may not use the {PortalName} Web Site in any manner which could damage, disable, overburden, or impair the {PortalName} Web Site or interfere with any other party''s use and enjoyment of the {PortalName} Web Site. You may not obtain or attempt to obtain any materials or information through any means not intentionally made available or provided for through the {PortalName} Web Sites.<br />
            <br />
            <span class="SubHead">USE OF COMMUNICATION SERVICES</span><br />
            <br />
            The {PortalName} Web Site may contain bulletin board services, chat areas, news groups, forums, communities, personal web pages, calendars, and/or other message or communication facilities designed to enable you to communicate with the public at large or with a group (collectively, &quot;Communication Services&quot;), you agree to use the Communication Services only to post, send and receive messages and material that are proper and related to the particular Communication Service. By way of example, and not as a limitation, you agree that when using a Communication Service, you will not:<br />
            <br />
            <ul>
                <li>Defame, abuse, harass, stalk, threaten or otherwise violate the legal rights (such as rights of privacy and publicity) of others.<br />
                <br />
                </li>
                <li>Publish, post, upload, distribute or disseminate any inappropriate, profane, defamatory, infringing, obscene, indecent or unlawful topic, name, material or information.<br />
                <br />
                </li>
                <li>Upload files that contain software or other material protected by intellectual property laws (or by rights of privacy of publicity) unless you own or control the rights thereto or have received all necessary consents.<br />
                <br />
                </li>
                <li>Upload files that contain viruses, corrupted files, or any other similar software or programs that may damage the operation of another''s computer.<br />
                <br />
                </li>
                <li>Advertise or offer to sell or buy any goods or services for any business purpose, unless such Communication Service specifically allows such messages.<br />
                <br />
                </li>
                <li>Conduct or forward surveys, contests, pyramid schemes or chain letters.<br />
                <br />
                </li>
                <li>Download any file posted by another user of a Communication Service that you know, or reasonably should know, cannot be legally distributed in such manner.<br />
                <br />
                </li>
                <li>Falsify or delete any author attributions, legal or other proper notices or proprietary designations or labels of the origin or source of software or other material contained in a file that is uploaded.<br />
                <br />
                </li>
                <li>Restrict or inhibit any other user from using and enjoying the Communication Services.<br />
                <br />
                </li>
                <li>Violate any code of conduct or other guidelines which may be applicable for any particular Communication Service.<br />
                <br />
                </li>
                <li>Harvest or otherwise collect information about others, including e-mail addresses, without their consent.<br />
                <br />
                </li>
                <li>Violate any applicable laws or regulations. </li>
            </ul>
            {PortalName} has no obligation to monitor the Communication Services. However, {PortalName} reserves the right to review materials posted to a Communication Service and to remove any materials in its sole discretion. {PortalName} reserves the right to terminate your access to any or all of the Communication Services at any time without notice for any reason whatsoever.<br />
            <br />
            {PortalName} reserves the right at all times to disclose any information as necessary to satisfy any applicable law, regulation, legal process or governmental request, or to edit, refuse to post or to remove any information or materials, in whole or in part, in {PortalName}''s sole discretion.<br />
            <br />
            Always use caution when giving out any personally identifying information about yourself or your children in any Communication Service. {PortalName} does not control or endorse the content, messages or information found in any Communication Service and, therefore, {PortalName} specifically disclaims any liability with regard to the Communication Services and any actions resulting from your participation in any Communication Service. Managers and hosts are not authorized {PortalName} spokespersons, and their views do not necessarily reflect those of {PortalName}.<br />
            <br />
            Materials uploaded to a Communication Service may be subject to posted limitations on usage, reproduction and/or dissemination. You are responsible for adhering to such limitations if you download the materials.<br />
            <br />
            <span class="SubHead">MATERIALS PROVIDED TO {PortalName} OR POSTED AT ANY {PortalName} WEB SITE</span><br />
            <br />
            {PortalName} does not claim ownership of the materials you provide to {PortalName} (including feedback and suggestions) or post, upload, input or submit to any {PortalName} Web Site or its associated services (collectively &quot;Submissions&quot;). However, by posting, uploading, inputting, providing or submitting your Submission you are granting {PortalName}, its affiliated companies and necessary sublicensees permission to use your Submission in connection with the operation of their Internet businesses including, without limitation, the rights to: copy, distribute, transmit, publicly display, publicly perform, reproduce, edit, translate and reformat your Submission; and to publish your name in connection with your Submission.<br />
            <br />
            No compensation will be paid with respect to the use of your Submission, as provided herein. {PortalName} is under no obligation to post or use any Submission you may provide and may remove any Submission at any time in {PortalName}''s sole discretion.<br />
            <br />
            By posting, uploading, inputting, providing or submitting your Submission you warrant and represent that you own or otherwise control all of the rights to your Submission as described in this section including, without limitation, all the rights necessary for you to provide, post, upload, input or submit the Submissions.<br />
            <br />
            <span class="SubHead">LIABILITY DISCLAIMER</span><br />
            <br />
YOU EXPRESSLY UNDERSTAND AND AGREE THAT: (A) THE {PortalName} AND THE SERVICES ARE PROVIDED ON AN "AS IS" AND "AS AVAILABLE" BASIS AND THAT NEITHER OF YOUR SERVICE PROVIDERS, THEIR RESPECTIVE AFFILIATES, SUBSIDIARIES, RESELLERS, DISTRIBUTORS, CONTRIBUTORS, AGENTS AND/OR SUPPLIERS, OFFICERS, DIRECTORS NOR EMPLOYEES (THE "SERVICE PROVIDER PARTIES") MAKES ANY WARRANTIES, REPRESENTATIONS OR CONDITIONS (as used in this section "WARRANTIES") OF ANY KIND, WHETHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT AND THAT ANY SUCH WARRANTIES ARE HEREBY EXPRESSLY DISCLAIMED, AND (B) THE SERVICE PROVIDER PARTIES SPECIFICALLY MAKE NO WARRANTIES THAT THE WEBSITE OR ANY OF THE SERVICES, INCLUDING ANY CONTENT, INFORMATION, PRODUCTS OR SERVICES OBTAINED FROM OR THROUGH THE USE OF THE WEBSITE OR THE SERVICES, WILL BE PROVIDED ON AN UNINTERRUPTED, TIMELY, SECURE OR ERROR-FREE BASIS OR THAT SUCH SERVICES OR THE RESULTS DERIVED THEREFROM WILL MEET YOUR REQUIREMENTS OR EXPECTATIONS.<br><br>

YOU EXPRESSLY UNDERSTAND AND AGREE THAT IN NO EVENT SHALL ANY SERVICE PROVIDER PARTY, BE LIABLE FOR ANY DAMAGES WHATSOEVER, INCLUDING ANY DIRECT, INDIRECT, INCIDENTAL, CONSEQUENTIAL, SPECIAL OR EXEMPLARY DAMAGES, AND ANY DAMAGES FOR LOSS OF PROFITS, SAVINGS, GOODWILL OR OTHER INTANGIBLE LOSSES, REGARDLESS OF WHETHER SUCH PARTY, HAD BEEN ADVISED OF OR COULD HAVE FORESEEN THE POSSIBILITY OF SUCH DAMAGES, ARISING OUT OF OR IN CONNECTION WITH: (A) THE USE, INABILITY TO USE OR PERFORMANCE OF ANY OF THE SERVICES OR THE WEBSITE, OR (B) ANY UNAUTHORIZED ACCESS TO OR MODIFICATION TO ANY OF YOUR CONTENT OR TRANSMISSIONS, OR (C) ANY OTHER MATTER RELATING TO THE WEBSITE OR ANY OF THE SERVICES, REGARDLESS OR WHETHER ANY OF THE FOREGOING IS DETERMINED TO CONSTITUTE A FUNDAMENTAL BREACH OR FAILURE OF ESSENTIAL PURPOSE.<br><br>
THE INFORMATION, SOFTWARE, PRODUCTS, AND SERVICES INCLUDED IN OR AVAILABLE THROUGH THE {PortalName} WEB SITE MAY INCLUDE INACCURACIES OR TYPOGRAPHICAL ERRORS. CHANGES ARE PERIODICALLY ADDED TO THE INFORMATION HEREIN. {PortalName} AND/OR ITS SUPPLIERS MAY MAKE IMPROVEMENTS AND/OR CHANGES IN THE {PortalName} WEB SITE AT ANY TIME. ADVICE RECEIVED VIA THE {PortalName} WEB SITE SHOULD NOT BE RELIED UPON FOR PERSONAL, MEDICAL, LEGAL OR FINANCIAL DECISIONS AND YOU SHOULD CONSULT AN APPROPRIATE PROFESSIONAL FOR SPECIFIC ADVICE TAILORED TO YOUR SITUATION.<br />
            <br />
            {PortalName} AND/OR ITS SUPPLIERS MAKE NO REPRESENTATIONS ABOUT THE SUITABILITY, RELIABILITY, AVAILABILITY, TIMELINESS, AND ACCURACY OF THE INFORMATION, SOFTWARE, PRODUCTS, SERVICES AND RELATED GRAPHICS CONTAINED ON THE {PortalName} WEB SITE FOR ANY PURPOSE. TO THE MAXIMUM EXTENT PERMITTED BY APPLICABLE LAW, ALL SUCH INFORMATION, SOFTWARE, PRODUCTS, SERVICES AND RELATED GRAPHICS ARE PROVIDED &quot;AS IS&quot; WITHOUT WARRANTY OR CONDITION OF ANY KIND. {PortalName} AND/OR ITS SUPPLIERS HEREBY DISCLAIM ALL WARRANTIES AND CONDITIONS WITH REGARD TO THIS INFORMATION, SOFTWARE, PRODUCTS, SERVICES AND RELATED GRAPHICS, INCLUDING ALL IMPLIED WARRANTIES OR CONDITIONS OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT.<br />
            <br />
            <br />
            SERVICE CONTACT : {AdministratorEmail}<br />
            <br />
            <span class="SubHead">TERMINATION/ACCESS RESTRICTION</span><br />
            <br />
            {PortalName} reserves the right, in its sole discretion, to terminate your access to the {PortalName} Web Site and the related services or any portion thereof at any time, without notice. GENERAL To the maximum extent permitted by law, this agreement is governed by the laws of the State of Washington, U.S.A. and you hereby consent to the exclusive jurisdiction and venue of courts in King County, Washington, U.S.A. in all disputes arising out of or relating to the use of the {PortalName} Web Site. Use of the {PortalName} Web Site is unauthorized in any jurisdiction that does not give effect to all provisions of these terms and conditions, including without limitation this paragraph. You agree that no joint venture, partnership, employment, or agency relationship exists between you and {PortalName} as a result of this agreement or use of the {PortalName} Web Site. {PortalName}''s performance of this agreement is subject to existing laws and legal process, and nothing contained in this agreement is in derogation of {PortalName}''s right to comply with governmental, court and law enforcement requests or requirements relating to your use of the {PortalName} Web Site or information provided to or gathered by {PortalName} with respect to such use. If any part of this agreement is determined to be invalid or unenforceable pursuant to applicable law including, but not limited to, the warranty disclaimers and liability limitations set forth above, then the invalid or unenforceable provision will be deemed superseded by a valid, enforceable provision that most closely matches the intent of the original provision and the remainder of the agreement shall continue in effect. Unless otherwise specified herein, this agreement constitutes the entire agreement between the user and {PortalName} with respect to the {PortalName} Web Site and it supersedes all prior or contemporaneous communications and proposals, whether electronic, oral or written, between the user and {PortalName} with respect to the {PortalName} Web Site. A printed version of this agreement and of any notice given in electronic form shall be admissible in judicial or administrative proceedings based upon or relating to this agreement to the same extent an d subject to the same conditions as other business documents and records originally generated and maintained in printed form. It is the express wish to the parties that this agreement and all related documents be drawn up in English.<br />
            <br />
            <span class="SubHead">TRADEMARKS</span><br />
            <br />
            The names of actual companies and products mentioned herein may be the trademarks of their respective owners.<br />
            <br />
            The example companies, organizations, products, people and events depicted herein are fictitious. No association with any real company, organization, product, person, or event is intended or should be inferred.<br />
            <br />
            Any rights not expressly granted herein are reserved.<br />
            <br />
            If you have any questions about the terms of use, please contact {PortalName} at {AdministratorEmail}. <br />
            <br />
            </td>
        </tr>
    </tbody>
</table>', null 
GO
UpdatelonglanguageSetting 'en','Security_CannotUpload','You do not have permission to upload files.  To request permission you need to contact
<p>{AdministratorEmail}</p>"', null 
GO
UpdatelonglanguageSetting 'en','Security_Enter_Portal','You need to use the hyperlink that was send to you in the E-Mail in order to valide your account.  If you still experience some ploblem you can contact the administrator for further help {HostEmail}', null 
GO
UpdatelonglanguageSetting 'en','Security_Enter_PortalIP','You are trying to access your account from an IP Adresse that is not allowed.  To seek help please E-Mail to (your IP {IP}) {HostEmail}', null 
GO
UpdatelonglanguageSetting 'en','Security_Enter_PortalWait','Your account been looked out for a further 10 minutes.  You or another user was trying to enter your account with an invalid password.  Please wait until this time has expired before trying to log in again.  Or contact the webmaster for help   
{HostEmail}', null 
GO
UpdatelonglanguageSetting 'en','Security_Enter_PortalWait1','We have detected a problem with your account.  You account been looked for 10 minutes for security reasons.  Please wait until this time has expired before trying to log in again.  Otherwise you may experience further delays.  For more help you may contact the webmaster at :  
{HostEmail}', null 
GO
UpdatelonglanguageSetting 'en','Welcome_Accepted_Private','Your demand been recorded.  The webmaster will analyse your request and inform you by E-Mail of the result.', null 
GO
UpdatelonglanguageSetting 'en','Welcome_Accepted_public','Your demand been accepted, you can now log in.', null 
GO
UpdatelonglanguageSetting 'en','Welcome_Accepted_Verified','Your subscription demand has been saved.  You should receive a validation code by E-Mail in the next few minutes.  You will need to use this code the first time your enter the site.', null 
GO
UpdateCountryCodes 'en','AF','Afghanistan'
GO
UpdateCountryCodes 'en','AL','Albania'
GO
UpdateCountryCodes 'en','DZ','Algeria'
GO
UpdateCountryCodes 'en','AS','American Samoa'
GO
UpdateCountryCodes 'en','AD','Andorra'
GO
UpdateCountryCodes 'en','AO','Angola'
GO
UpdateCountryCodes 'en','AI','Anguilla'
GO
UpdateCountryCodes 'en','AQ','Antarctica'
GO
UpdateCountryCodes 'en','AG','Antigua and Barbuda'
GO
UpdateCountryCodes 'en','AR','Argentina'
GO
UpdateCountryCodes 'en','AM','Armenia'
GO
UpdateCountryCodes 'en','AW','Aruba'
GO
UpdateCountryCodes 'en','AU','Australia'
GO
UpdateCountryCodes 'en','AT','Austria'
GO
UpdateCountryCodes 'en','AZ','Azerbaijan'
GO
UpdateCountryCodes 'en','BS','Bahamas'
GO
UpdateCountryCodes 'en','BH','Bahrain'
GO
UpdateCountryCodes 'en','BD','Bangladesh'
GO
UpdateCountryCodes 'en','BB','Barbados'
GO
UpdateCountryCodes 'en','BY','Belarus'
GO
UpdateCountryCodes 'en','BE','Belgium'
GO
UpdateCountryCodes 'en','BZ','Belize'
GO
UpdateCountryCodes 'en','BJ','Benin'
GO
UpdateCountryCodes 'en','BM','Bermuda'
GO
UpdateCountryCodes 'en','BT','Bhutan'
GO
UpdateCountryCodes 'en','BO','Bolivia'
GO
UpdateCountryCodes 'en','BA','Bosnia and Herzegovina'
GO
UpdateCountryCodes 'en','BW','Botswana'
GO
UpdateCountryCodes 'en','BV','Bouvet Island'
GO
UpdateCountryCodes 'en','BR','Brazil'
GO
UpdateCountryCodes 'en','IO','British Indian Ocean Territory'
GO
UpdateCountryCodes 'en','VG','British Virgin Islands'
GO
UpdateCountryCodes 'en','BN','Brunei Darussalam'
GO
UpdateCountryCodes 'en','BG','Bulgaria'
GO
UpdateCountryCodes 'en','BF','Burkina Faso'
GO
UpdateCountryCodes 'en','BI','Burundi'
GO
UpdateCountryCodes 'en','KH','Cambodia'
GO
UpdateCountryCodes 'en','CM','Cameroon'
GO
UpdateCountryCodes 'en','CA','Canada'
GO
UpdateRegionCodes 'en','CA','AB','Alberta'
GO
UpdateRegionCodes 'en','CA','BC','Colombie Britannique'
GO
UpdateRegionCodes 'en','CA','PE','Ile du Prince Edouard'
GO
UpdateRegionCodes 'en','CA','MB','Manitoba'
GO
UpdateRegionCodes 'en','CA','NB','Nouveau Brunswick'
GO
UpdateRegionCodes 'en','CA','NS','Nouvelle-Écosse'
GO
UpdateRegionCodes 'en','CA','ON','Ontario'
GO
UpdateRegionCodes 'en','CA','QC','Québec'
GO
UpdateRegionCodes 'en','CA','SK','Saskatchewan'
GO
UpdateRegionCodes 'en','CA','NF','Terre-Neuve'
GO
UpdateRegionCodes 'en','CA','NT','Territoires du Nord-Ouest'
GO
UpdateRegionCodes 'en','CA','YT','Yukon'
GO
UpdateCountryCodes 'en','CV','Cape Verde'
GO
UpdateCountryCodes 'en','KY','Cayman Islands'
GO
UpdateCountryCodes 'en','CF','Central African Republic'
GO
UpdateCountryCodes 'en','TD','Chad'
GO
UpdateCountryCodes 'en','CL','Chile'
GO
UpdateCountryCodes 'en','CN','China'
GO
UpdateCountryCodes 'en','CX','Christmas Island'
GO
UpdateCountryCodes 'en','CC','Cocos'
GO
UpdateCountryCodes 'en','CO','Colombia'
GO
UpdateCountryCodes 'en','KM','Comoros'
GO
UpdateCountryCodes 'en','CG','Congo'
GO
UpdateCountryCodes 'en','CK','Cook Islands'
GO
UpdateCountryCodes 'en','CR','Costa Rica'
GO
UpdateCountryCodes 'en','HR','Croatia'
GO
UpdateCountryCodes 'en','CU','Cuba'
GO
UpdateCountryCodes 'en','CY','Cyprus'
GO
UpdateCountryCodes 'en','CZ','Czech Republic'
GO
UpdateCountryCodes 'en','DK','Denmark'
GO
UpdateCountryCodes 'en','DJ','Djibouti'
GO
UpdateCountryCodes 'en','DM','Dominica'
GO
UpdateCountryCodes 'en','DO','Dominican Republic'
GO
UpdateCountryCodes 'en','TP','East Timor'
GO
UpdateCountryCodes 'en','EC','Ecuador'
GO
UpdateCountryCodes 'en','EG','Egypt'
GO
UpdateCountryCodes 'en','SV','El Salvador'
GO
UpdateCountryCodes 'en','GQ','Equatorial Guinea'
GO
UpdateCountryCodes 'en','ER','Eritrea'
GO
UpdateCountryCodes 'en','EE','Estonia'
GO
UpdateCountryCodes 'en','ET','Ethiopia'
GO
UpdateCountryCodes 'en','FK','Falkland Islands'
GO
UpdateCountryCodes 'en','FO','Faroe Islands'
GO
UpdateCountryCodes 'en','FJ','Fiji'
GO
UpdateCountryCodes 'en','FI','Finland'
GO
UpdateCountryCodes 'en','FR','France'
GO
UpdateCountryCodes 'en','GF','French Guiana'
GO
UpdateCountryCodes 'en','PF','French Polynesia'
GO
UpdateCountryCodes 'en','TF','French Southern Territories'
GO
UpdateCountryCodes 'en','GA','Gabon'
GO
UpdateCountryCodes 'en','GM','Gambia'
GO
UpdateCountryCodes 'en','GE','Georgia'
GO
UpdateCountryCodes 'en','DE','Germany'
GO
UpdateCountryCodes 'en','GH','Ghana'
GO
UpdateCountryCodes 'en','GI','Gibraltar'
GO
UpdateCountryCodes 'en','GR','Greece'
GO
UpdateCountryCodes 'en','GL','Greenland'
GO
UpdateCountryCodes 'en','GD','Grenada'
GO
UpdateCountryCodes 'en','GP','Guadeloupe'
GO
UpdateCountryCodes 'en','GU','Guam'
GO
UpdateCountryCodes 'en','GT','Guatemala'
GO
UpdateCountryCodes 'en','GN','Guinea'
GO
UpdateCountryCodes 'en','GW','Guinea-Bissau'
GO
UpdateCountryCodes 'en','GY','Guyana'
GO
UpdateCountryCodes 'en','HT','Haiti'
GO
UpdateCountryCodes 'en','HM','Heard and McDonald Islands'
GO
UpdateCountryCodes 'en','HN','Honduras'
GO
UpdateCountryCodes 'en','HK','Hong Kong'
GO
UpdateCountryCodes 'en','HU','Hungary'
GO
UpdateCountryCodes 'en','IS','Iceland'
GO
UpdateCountryCodes 'en','IN','India'
GO
UpdateCountryCodes 'en','ID','Indonesia'
GO
UpdateCountryCodes 'en','IR','Iran'
GO
UpdateCountryCodes 'en','IQ','Iraq'
GO
UpdateCountryCodes 'en','IE','Ireland'
GO
UpdateCountryCodes 'en','IL','Israel'
GO
UpdateCountryCodes 'en','IT','Italy'
GO
UpdateCountryCodes 'en','CI','Ivory Coast'
GO
UpdateCountryCodes 'en','JM','Jamaica'
GO
UpdateCountryCodes 'en','JP','Japan'
GO
UpdateCountryCodes 'en','JO','Jordan'
GO
UpdateCountryCodes 'en','KZ','Kazakhstan'
GO
UpdateCountryCodes 'en','KE','Kenya'
GO
UpdateCountryCodes 'en','KI','Kiribati'
GO
UpdateCountryCodes 'en','KW','Kuwait'
GO
UpdateCountryCodes 'en','KG','Kyrgyzstan'
GO
UpdateCountryCodes 'en','LA','Laos'
GO
UpdateCountryCodes 'en','LV','Latvia'
GO
UpdateCountryCodes 'en','LB','Lebanon'
GO
UpdateCountryCodes 'en','LS','Lesotho'
GO
UpdateCountryCodes 'en','LR','Liberia'
GO
UpdateCountryCodes 'en','LY','Libya'
GO
UpdateCountryCodes 'en','LI','Liechtenstein'
GO
UpdateCountryCodes 'en','LT','Lithuania'
GO
UpdateCountryCodes 'en','LU','Luxembourg'
GO
UpdateCountryCodes 'en','MO','Macau'
GO
UpdateCountryCodes 'en','MK','Macedonia'
GO
UpdateCountryCodes 'en','MG','Madagascar'
GO
UpdateCountryCodes 'en','MW','Malawi'
GO
UpdateCountryCodes 'en','MY','Malaysia'
GO
UpdateCountryCodes 'en','MV','Maldives'
GO
UpdateCountryCodes 'en','ML','Mali'
GO
UpdateCountryCodes 'en','MT','Malta'
GO
UpdateCountryCodes 'en','MH','Marshall Islands'
GO
UpdateCountryCodes 'en','MQ','Martinique'
GO
UpdateCountryCodes 'en','MR','Mauritania'
GO
UpdateCountryCodes 'en','MU','Mauritius'
GO
UpdateCountryCodes 'en','YT','Mayotte'
GO
UpdateCountryCodes 'en','MX','Mexico'
GO
UpdateCountryCodes 'en','FM','Micronesia'
GO
UpdateCountryCodes 'en','MD','Moldova'
GO
UpdateCountryCodes 'en','MC','Monaco'
GO
UpdateCountryCodes 'en','MN','Mongolia'
GO
UpdateCountryCodes 'en','MS','Montserrat'
GO
UpdateCountryCodes 'en','MA','Morocco'
GO
UpdateCountryCodes 'en','MZ','Mozambique'
GO
UpdateCountryCodes 'en','MM','Myanmar'
GO
UpdateCountryCodes 'en','NA','Namibia'
GO
UpdateCountryCodes 'en','NR','Nauru'
GO
UpdateCountryCodes 'en','NP','Nepal'
GO
UpdateCountryCodes 'en','NL','Netherlands'
GO
UpdateCountryCodes 'en','AN','Netherlands Antilles'
GO
UpdateCountryCodes 'en','NC','New Caledonia'
GO
UpdateCountryCodes 'en','NZ','New Zealand'
GO
UpdateCountryCodes 'en','NI','Nicaragua'
GO
UpdateCountryCodes 'en','NE','Niger'
GO
UpdateCountryCodes 'en','NG','Nigeria'
GO
UpdateCountryCodes 'en','NU','Niue'
GO
UpdateCountryCodes 'en','NF','Norfolk Island'
GO
UpdateCountryCodes 'en','KP','North Korea'
GO
UpdateCountryCodes 'en','MP','Northern Mariana Islands'
GO
UpdateCountryCodes 'en','NO','Norway'
GO
UpdateCountryCodes 'en','OM','Oman'
GO
UpdateCountryCodes 'en','PK','Pakistan'
GO
UpdateCountryCodes 'en','PW','Palau'
GO
UpdateCountryCodes 'en','PA','Panama'
GO
UpdateCountryCodes 'en','PG','Papua New Guinea'
GO
UpdateCountryCodes 'en','PY','Paraguay'
GO
UpdateCountryCodes 'en','PE','Peru'
GO
UpdateCountryCodes 'en','PH','Philippines'
GO
UpdateCountryCodes 'en','PN','Pitcairn'
GO
UpdateCountryCodes 'en','PL','Poland'
GO
UpdateCountryCodes 'en','PT','Portugal'
GO
UpdateCountryCodes 'en','PR','Puerto Rico'
GO
UpdateCountryCodes 'en','QA','Qatar'
GO
UpdateCountryCodes 'en','RE','Reunion'
GO
UpdateCountryCodes 'en','RO','Romania'
GO
UpdateCountryCodes 'en','RU','Russian Federation'
GO
UpdateCountryCodes 'en','RW','Rwanda'
GO
UpdateCountryCodes 'en','GS','S. Georgia and S. Sandwich Islands'
GO
UpdateCountryCodes 'en','KN','Saint Kitts and Nevis'
GO
UpdateCountryCodes 'en','LC','Saint Lucia'
GO
UpdateCountryCodes 'en','VC','Saint Vincent and The Grenadines'
GO
UpdateCountryCodes 'en','WS','Samoa'
GO
UpdateCountryCodes 'en','SM','San Marino'
GO
UpdateCountryCodes 'en','ST','Sao Tome and Principe'
GO
UpdateCountryCodes 'en','SA','Saudi Arabia'
GO
UpdateCountryCodes 'en','SN','Senegal'
GO
UpdateCountryCodes 'en','SC','Seychelles'
GO
UpdateCountryCodes 'en','SL','Sierra Leone'
GO
UpdateCountryCodes 'en','SG','Singapore'
GO
UpdateCountryCodes 'en','SK','Slovakia'
GO
UpdateCountryCodes 'en','SI','Slovenia'
GO
UpdateCountryCodes 'en','SB','Solomon Islands'
GO
UpdateCountryCodes 'en','SO','Somalia'
GO
UpdateCountryCodes 'en','ZA','South Africa'
GO
UpdateCountryCodes 'en','KR','South Korea'
GO
UpdateCountryCodes 'en','SU','Soviet Union'
GO
UpdateCountryCodes 'en','ES','Spain'
GO
UpdateCountryCodes 'en','LK','Sri Lanka'
GO
UpdateCountryCodes 'en','SH','St. Helena'
GO
UpdateCountryCodes 'en','PM','St. Pierre and Miquelon'
GO
UpdateCountryCodes 'en','SD','Sudan'
GO
UpdateCountryCodes 'en','SR','Suriname'
GO
UpdateCountryCodes 'en','SJ','Svalbard and Jan Mayen Islands'
GO
UpdateCountryCodes 'en','SZ','Swaziland'
GO
UpdateCountryCodes 'en','SE','Sweden'
GO
UpdateCountryCodes 'en','CH','Switzerland'
GO
UpdateCountryCodes 'en','SY','Syria'
GO
UpdateCountryCodes 'en','TW','Taiwan'
GO
UpdateCountryCodes 'en','TJ','Tajikistan'
GO
UpdateCountryCodes 'en','TZ','Tanzania'
GO
UpdateCountryCodes 'en','TH','Thailand'
GO
UpdateCountryCodes 'en','TG','Togo'
GO
UpdateCountryCodes 'en','TK','Tokelau'
GO
UpdateCountryCodes 'en','TO','Tonga'
GO
UpdateCountryCodes 'en','TT','Trinidad and Tobago'
GO
UpdateCountryCodes 'en','TN','Tunisia'
GO
UpdateCountryCodes 'en','TR','Turkey'
GO
UpdateCountryCodes 'en','TM','Turkmenistan'
GO
UpdateCountryCodes 'en','TC','Turks and Caicos Islands'
GO
UpdateCountryCodes 'en','TV','Tuvalu'
GO
UpdateCountryCodes 'en','UG','Uganda'
GO
UpdateCountryCodes 'en','UA','Ukraine'
GO
UpdateCountryCodes 'en','AE','United Arab Emirates'
GO
UpdateCountryCodes 'en','UK','United Kingdom'
GO
UpdateCountryCodes 'en','US','United States'
GO
UpdateRegionCodes 'en','US','AL','Alabama'
GO
UpdateRegionCodes 'en','US','AK','Alaska'
GO
UpdateRegionCodes 'en','US','AZ','Arizona'
GO
UpdateRegionCodes 'en','US','AR','Arkansas'
GO
UpdateRegionCodes 'en','US','CA','Californie'
GO
UpdateRegionCodes 'en','US','NC','Caroline du Nord'
GO
UpdateRegionCodes 'en','US','CO','Colorado'
GO
UpdateRegionCodes 'en','US','CT','Connecticut'
GO
UpdateRegionCodes 'en','US','DE','Delaware'
GO
UpdateRegionCodes 'en','US','DC','District of Columbia'
GO
UpdateRegionCodes 'en','US','FL','Floride'
GO
UpdateRegionCodes 'en','US','GA','Georgie'
GO
UpdateRegionCodes 'en','US','HI','Hawaii'
GO
UpdateRegionCodes 'en','US','ID','Idaho'
GO
UpdateRegionCodes 'en','US','IL','Illinois'
GO
UpdateRegionCodes 'en','US','IN','Indiana'
GO
UpdateRegionCodes 'en','US','IA','Iowa'
GO
UpdateRegionCodes 'en','US','KS','Kansas'
GO
UpdateRegionCodes 'en','US','KY','Kentucky'
GO
UpdateRegionCodes 'en','US','LA','Louisiane'
GO
UpdateRegionCodes 'en','US','ME','Maine'
GO
UpdateRegionCodes 'en','US','MD','Maryland'
GO
UpdateRegionCodes 'en','US','MA','Massachusetts'
GO
UpdateRegionCodes 'en','US','MI','Michigan'
GO
UpdateRegionCodes 'en','US','MN','Minnesota'
GO
UpdateRegionCodes 'en','US','MS','Mississippi'
GO
UpdateRegionCodes 'en','US','MO','Missouri'
GO
UpdateRegionCodes 'en','US','MT','Montana'
GO
UpdateRegionCodes 'en','US','NE','Nebraska'
GO
UpdateRegionCodes 'en','US','NV','Nevada'
GO
UpdateRegionCodes 'en','US','NH','New Hampshire'
GO
UpdateRegionCodes 'en','US','NJ','New Jersey'
GO
UpdateRegionCodes 'en','US','NM','New Mexico'
GO
UpdateRegionCodes 'en','US','NY','New York'
GO
UpdateRegionCodes 'en','US','ND','North Dakota'
GO
UpdateRegionCodes 'en','US','OH','Ohio'
GO
UpdateRegionCodes 'en','US','OK','Oklahoma'
GO
UpdateRegionCodes 'en','US','OR','Oregon'
GO
UpdateRegionCodes 'en','US','PA','Pennsylvania'
GO
UpdateRegionCodes 'en','US','RI','Rhode Island'
GO
UpdateRegionCodes 'en','US','SC','South Carolina'
GO
UpdateRegionCodes 'en','US','SD','South Dakota'
GO
UpdateRegionCodes 'en','US','TN','Tennessee'
GO
UpdateRegionCodes 'en','US','TX','Texas'
GO
UpdateRegionCodes 'en','US','UT','Utah'
GO
UpdateRegionCodes 'en','US','VT','Vermont'
GO
UpdateRegionCodes 'en','US','VA','Virginia'
GO
UpdateRegionCodes 'en','US','WA','Washington'
GO
UpdateRegionCodes 'en','US','WV','West Virginia'
GO
UpdateRegionCodes 'en','US','WI','Wisconsin'
GO
UpdateRegionCodes 'en','US','WY','Wyoming'
GO
UpdateCountryCodes 'en','UY','Uruguay'
GO
UpdateCountryCodes 'en','UM','US Minor Outlying Islands'
GO
UpdateCountryCodes 'en','VI','US Virgin Islands'
GO
UpdateCountryCodes 'en','UZ','Uzbekistan'
GO
UpdateCountryCodes 'en','VU','Vanuatu'
GO
UpdateCountryCodes 'en','VE','Venezuela'
GO
UpdateCountryCodes 'en','VN','Viet Nam'
GO
UpdateCountryCodes 'en','WF','Wallis and Futuna Islands'
GO
UpdateCountryCodes 'en','EH','Western Sahara'
GO
UpdateCountryCodes 'en','YE','Yemen'
GO
UpdateCountryCodes 'en','YU','Yugoslavia'
GO
UpdateCountryCodes 'en','ZR','Zaire'
GO
UpdateCountryCodes 'en','ZM','Zambia'
GO
UpdateCountryCodes 'en','ZW','Zimbabwe'
GO
UpdateAdminModuleDefinitions 'en','87','Admin','The Site Administrator has full control to build and maintain their site. Administrators have access to the Control Panel and the Admin page which has six (6) child pages that handle administration tasks.'
GO
UpdateAdminModuleDefinitions 'en','72','Host settings','Management of Host setting.'
GO
UpdateAdminModuleDefinitions 'en','63','Portals Settings','Manage existing portals and create new portals.'
GO
UpdateAdminModuleDefinitions 'en','14','Site settings','The Site Settings page enables Administrators to configure the both basic and advanced setting for the site. Settings include design, advertising, user registration settings, etc.'
GO
UpdateAdminModuleDefinitions 'en','13','Pages','The pages page offers Adminstrators management tools for managing hidden pages and page hierarchy.'
GO
UpdateAdminModuleDefinitions 'en','64','Modules settings','Add new modules and manage existing modules.'
GO
UpdateAdminModuleDefinitions 'en','12','Security Roles','This page manages Security Roles for this web site.'
GO
UpdateAdminModuleDefinitions 'en','15','User Accounts','This User Accounts page manages User Accounts for this portal.'
GO
UpdateAdminModuleDefinitions 'en','21','File explorer','The page allow you to browse the file system.'
GO
UpdateAdminModuleDefinitions 'en','19','Vendors','This page manages Vendor accounts within this web site.'
GO
UpdateAdminModuleDefinitions 'en','27','Site log','This page displays statistical reports for this web site.'
GO
UpdateAdminModuleDefinitions 'en','65','SQL','This page enable Hosts to make SQL queries to the database.'
GO
UpdateAdminModuleDefinitions 'en','28','E-Mail','This page enables the Administrators to send messages to users, email addresses and security roles.'
GO
UpdateAdminModuleDefinitions 'en','88','Language','The Languages page displays the languages that are supported on the site.

This page also provides webmaster with access to the Language Editor. As well as enabling webmaster to add new languages, the Language Editor enables the editing of emails sent by the site (e.g. User Registration, Password Reminder, and Notification of Role Assignment) as well as site wide messages (e.g. Privacy Statement, Login Instructions, Terms Of Use).'
GO
UpdateCurrencies 'en','CAD','Canadian (CAD)'
GO
UpdateCurrencies 'en','EUR','Euros (EUR)'
GO
UpdateCurrencies 'en','GBP','Pound Sterling (GBP)'
GO
UpdateCurrencies 'en','JPY','Yen (JPY)'
GO
UpdateCurrencies 'en','USD','U.S. (USD)'
GO
UpdateBillingFrequencyCodes 'en','D','Days(s)'
GO
UpdateBillingFrequencyCodes 'en','M','Month'
GO
UpdateBillingFrequencyCodes 'en','N','None'
GO
UpdateBillingFrequencyCodes 'en','O','One time paiement'
GO
UpdateBillingFrequencyCodes 'en','W','week(s)'
GO
UpdateBillingFrequencyCodes 'en','Y','Year(s)'
GO
UpdateSiteLogReports 'en','5','Browser'
GO
UpdateSiteLogReports 'en','4','Clicks'
GO
UpdateSiteLogReports 'en','1','Dayly'
GO
UpdateSiteLogReports 'en','2','Detailled'
GO
UpdateSiteLogReports 'en','6','Hourly'
GO
UpdateSiteLogReports 'en','8','Monthly'
GO
UpdateSiteLogReports 'en','9','Page'
GO
UpdateSiteLogReports 'en','12','Site referral'
GO
UpdateSiteLogReports 'en','11','Subscription by country'
GO
UpdateSiteLogReports 'en','10','Subscription by date'
GO
UpdateSiteLogReports 'en','3','User'
GO
UpdateSiteLogReports 'en','7','Weekly'
GO
UpdateTimeZoneCodes 'en','-720','GMT -12 IDLW: Ligne internationale date ouest'
GO
UpdateTimeZoneCodes 'en','-660','GMT -11 NT: Nome'
GO
UpdateTimeZoneCodes 'en','-600','GMT -10 Alaska-Hawaii'
GO
UpdateTimeZoneCodes 'en','-540','GMT -9 YST: Yukon'
GO
UpdateTimeZoneCodes 'en','-480','GMT -8 PST: Pacifique'
GO
UpdateTimeZoneCodes 'en','-420','GMT -7 MST: Montagnes'
GO
UpdateTimeZoneCodes 'en','-360','GMT -6 CST: Centre'
GO
UpdateTimeZoneCodes 'en','-300','GMT -5 EST: East Canada et U.S.'
GO
UpdateTimeZoneCodes 'en','-240','GMT -4 AST: Atlantique'
GO
UpdateTimeZoneCodes 'en','-210','GMT -3.5 Terre-Neuve'
GO
UpdateTimeZoneCodes 'en','-180','GMT -3 Brasil, Argentine'
GO
UpdateTimeZoneCodes 'en','-120','GMT -2 AT: Azores'
GO
UpdateTimeZoneCodes 'en','-60','GMT -1 WAT: Afrique de l''ouest'
GO
UpdateTimeZoneCodes 'en','0','GMT: Greenwich Mean Time'
GO
UpdateTimeZoneCodes 'en','60','GMT +1 CET: Europe Centrale'
GO
UpdateTimeZoneCodes 'en','120','GMT +2 EET: Europe de l''est'
GO
UpdateTimeZoneCodes 'en','180','BT: Baghdad'
GO
UpdateTimeZoneCodes 'en','210','GMT +3.5 Iran'
GO
UpdateTimeZoneCodes 'en','240','GMT +4 Kabul'
GO
UpdateTimeZoneCodes 'en','300','GMT +5'
GO
UpdateTimeZoneCodes 'en','330','GMT +5.5 Inde'
GO
UpdateTimeZoneCodes 'en','360','GMT +6'
GO
UpdateTimeZoneCodes 'en','390','GMT +6.5 Îles Cocos'
GO
UpdateTimeZoneCodes 'en','420','GMT +7'
GO
UpdateTimeZoneCodes 'en','480','GMT +8 CCT: Chine'
GO
UpdateTimeZoneCodes 'en','540','GMT +9 JST: Japon'
GO
UpdateTimeZoneCodes 'en','600','GMT +10 GST: Guam'
GO
UpdateTimeZoneCodes 'en','660','GMT +11'
GO
UpdateTimeZoneCodes 'en','690','GMT +11.5 Îles Norfolk'
GO
UpdateTimeZoneCodes 'en','720','GMT +12 NZST: Nouvelle Zealande'
GO
UpdateTimeZoneCodes 'en','780','GMT +13 Îles Rawaki'
GO
UpdateTimeZoneCodes 'en','840','GMT +14 Îles Gilbert, Ellice, Phoenix et Line'
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
