----------------------------------------------------
-- Delete dbo.UsersOnline_PrivateMessages
----------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UsersOnline_PrivateMessages]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[UsersOnline_PrivateMessages]
GO
----------------------------------------------------
-- dbo.DeletePortalInfo
----------------------------------------------------

ALTER procedure dbo.DeletePortalInfo
@PortalID int
as
declare @UserId int
select @UserID = min(UserID)
from   UserPortals
where  PortalId = @PortalID
while @UserID is not null
begin
  delete
  from   UserPortals
  where  UserID = @UserID
  and    PortalId = @PortalID
  delete
  from   PrivateMessages
  where  SenderID = @UserID
  delete
  from   PrivateMessages
  where  ReceiverID = @UserID
  delete
  from   UsersOnline_Buddies
  where  UserID = @UserID
  delete
  from   UsersOnline_Buddies
  where  BuddyID = @UserID
  delete
  from   Users
  where  UserID = @UserID
  
  select @UserID = min(UserID)
  from   UserPortals
  where  PortalId = @PortalID
  and    UserID > @UserID
end
delete
from   Files
where  PortalId = @PortalID
delete
from   Files
where  PortalId = -1
delete
from   Portals
where  PortalID = @PortalID

GO
----------------------------------------------------
-- dbo.DeleteUser
----------------------------------------------------

ALTER procedure dbo.DeleteUser
@PortalId int,
@UserID   int
as
declare @RoleId int
if not exists ( select 1 from Portals where AdministratorId = @UserID )
begin
  delete
  from   UserPortals
  where  PortalId = @PortalId
  and    UserID = @UserID
  select @RoleId = min(RoleId)
  from   Roles
  where  PortalId = @PortalId
  while @RoleId is not null
  begin
    delete
    from   UserRoles
    where  UserId = @UserId
    and    RoleId = @RoleId
    select @RoleId = min(RoleId)
    from   Roles
    where  PortalId = @PortalId
    and    RoleId > @RoleId
  end
  if not exists ( select 1 from UserPortals where UserId = @UserID )
  begin
    delete
    from   Users
    where  UserID = @UserID
  end
end
  delete
  from   PrivateMessages
  where  SenderID = @UserID
  delete
  from   PrivateMessages
  where  ReceiverID = @UserID
  delete
  from   UsersOnline_Buddies
  where  UserID = @UserID
  delete
  from   UsersOnline_Buddies
  where  BuddyID = @UserID
GO
----------------------------------------------------
-- dbo.TTTForum_ForumCreateUpdateDelete
----------------------------------------------------

ALTER PROCEDURE [dbo].[TTTForum_ForumCreateUpdateDelete]
(
	
	@ForumGroupID int,
	@PortalID	int,
	@ModuleID	int,
	@Name		nvarchar(256),
	@Description	nvarchar(256),
	@CreatedByUser int,
	@IsModerated	bit,
	@IsActive	bit,
	@IsIntegrated	bit,
	@IntegratedGallery	int,
	@IntegratedAlbumName nvarchar(256),
	@IsPrivate	bit,
	@AuthorizedRoles	nvarchar(256),
	@Action	int,
	@ForumId	int,
    @OutputForumID int OUT
)
AS
-- CREATE
IF @Action = 0
BEGIN
	DECLARE @SortOrder int
	SET @SortOrder = ISNULL((SELECT MAX(SortOrder) + 1 FROM TTTForum_Forums WHERE ForumGroupID = @ForumGroupID), 0)
	-- Create a new forum
	INSERT INTO 
		TTTForum_Forums 
		(
			ForumGroupID,
			PortalID,
			ModuleID,
			Name,	
			Description,
			CreatedByUser,
			IsModerated,
			IsActive,
			IsIntegrated,
			IntegratedGallery,
			IntegratedAlbumName,
			IsPrivate,
			AuthorizedRoles,
			SortOrder
		)
	VALUES 
		(			
			@ForumGroupID,
			@PortalID,
			@ModuleID,
			@Name	,
			@Description,
			@CreatedByUser,
			@IsModerated,
			@IsActive,
			@IsIntegrated,
			@IntegratedGallery,
			@IntegratedAlbumName	,
			@IsPrivate,
			@AuthorizedRoles,	
			@SortOrder
		)
		EXEC TTTForum_UpdateForumCount @ForumGroupID, 1
		SET @OutputForumID = @@IDENTITY
END
		
-- UPDATE
ELSE IF @Action = 1
BEGIN
	IF EXISTS(SELECT ForumID FROM TTTForum_Forums WHERE ForumID = @ForumID AND ForumGroupID = @ForumGroupID)
	BEGIN
		UPDATE
			TTTForum_Forums
		SET
			Name = @Name,
			Description = @Description,
			CreatedByUser = @CreatedByUser,
			IsModerated = @IsModerated,
			IsActive = @IsActive,
			IsIntegrated = @IsIntegrated,
			IntegratedGallery = @IntegratedGallery,
			IntegratedAlbumName = @IntegratedAlbumName,
			IsPrivate = @IsPrivate,
			AuthorizedRoles = @AuthorizedRoles
		
		WHERE ForumID = @ForumID	
	END
END
-- DELETE
ELSE IF @Action = 2
BEGIN
	DELETE TTTForum_Forums WHERE ForumID = @ForumID
	EXEC TTTForum_UpdateForumCount @ForumGroupID, 0
END
GO
----------------------------------------------------
-- dbo.TTTForum_ForumGroupCreateUpdateDelete
----------------------------------------------------

ALTER PROCEDURE [dbo].[TTTForum_ForumGroupCreateUpdateDelete]
(
	@ForumGroupID int,
	@Name		nvarchar(256),
	@PortalID	int,
	@ModuleID	int,
	@CreatedByUser int,
	@Action 	int,
    @CreatedForumGroupID int out
)
AS
-- CREATE
IF @Action = 0
BEGIN
	DECLARE @SortOrder int
	SET @SortOrder = ISNULL((SELECT MAX(SortOrder) + 1 FROM TTTForum_ForumGroups), 0)
	-- Create a new forum group
	INSERT INTO 
		TTTForum_ForumGroups 
		(
			Name,
			PortalID,
			ModuleID,
			CreatedByUser,
			SortOrder
		)
	VALUES 
		(
			@Name,
			@PortalID,
			@ModuleID,
			@CreatedByUser,
			@SortOrder
		)
	
	SET @CreatedForumGroupID = @@IDENTITY
END
-- UPDATE
ELSE IF @Action = 1
BEGIN
	IF EXISTS(SELECT ForumGroupID FROM TTTForum_ForumGroups WHERE ForumGroupID = @ForumGroupID)
	BEGIN
		UPDATE
			TTTForum_ForumGroups
		SET
			Name = @Name,
			PortalID = @PortalID,
			ModuleID = @ModuleID,
			CreatedByUser = @CreatedByUser					
		WHERE ForumGroupID = @ForumGroupID	
	END
END
-- DELETE
ELSE IF @Action = 2
BEGIN
	DELETE TTTForum_ForumGroups WHERE ForumGroupID = @ForumGroupID
END
GO
----------------------------------------------------
-- dbo.TTTForum_GetAvatarsModule
----------------------------------------------------

ALTER procedure [dbo].[TTTForum_GetAvatarsModule]
(
	@ForumID	nvarchar(20)
)
as
SELECT 	ModuleID
FROM 		ModuleSettings
WHERE 	SettingName = 'IsAvatarsGallery'
AND		SettingValue = @ForumID
GO
updatelanguagecontext 'fr','top','Niveau supérieur', 'AdminMenu'
GO
updatelanguagecontext 'fr','title_TopToolTip','Déplacer le module un niveau supérieur', 'AdminMenu'
GO
updatelanguagecontext 'fr','bottom','Niveau inférieur', 'AdminMenu'
GO
updatelanguagecontext 'fr','title_bottomToolTip','Déplacer le module un niveau inférieur', 'AdminMenu'
GO
updatelanguagecontext 'fr','F_AdminAvatar','Avatars gallerie', 'Forum'
GO
updatelanguagecontext 'en','top','Up a level', 'AdminMenu'
GO
updatelanguagecontext 'en','title_TopToolTip','Move the module up a level', 'AdminMenu'
GO
updatelanguagecontext 'en','bottom','Down a level', 'AdminMenu'
GO
updatelanguagecontext 'en','title_bottomToolTip','Move the module down a level', 'AdminMenu'
GO
updatelanguagecontext 'en','F_AdminAvatar','Avatars gallery', 'Forum'
GO
updatelanguagecontext 'fr','select_avatar_tooltip','Aller et modifier la gallerie des avatars', 'Forum'
GO
updatelanguagecontext 'fr','select_avatar','Avatars gallerie', 'Forum'
GO
updatelanguagecontext 'en','select_avatar_tooltip','Go to and edit avatars gallery', 'Forum'
GO
updatelanguagecontext 'en','select_avatar','Avatars gallery', 'Forum'
GO
updatelanguagecontext 'fr','admin_caches_x','Purger', 'Command'
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
updatelanguagecontext 'fr','admin_delete_tab','Effacer page', 'Command'
GO
updatelanguagecontext 'fr','admin_tab_delete','Effacer la page', 'Command'
GO
updatelanguagecontext 'fr','SS_Tigra_Edit','Modifier menu', 'Command'
GO
updatelanguagecontext 'fr','ts_xmltemplate','Gabarit XML', 'TabSettings'
GO
updatelanguagecontext 'en','ts_xmltemplate','XML Template', 'TabSettings'
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