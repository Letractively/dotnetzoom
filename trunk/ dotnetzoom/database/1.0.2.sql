if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ModuleDefinitionsNames_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1) ALTER TABLE [dbo].[ModuleDefinitionsNames] DROP CONSTRAINT [FK_ModuleDefinitionsNames_language]
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ModuleDefinitionsNames_ModuleDefinitions]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1) ALTER TABLE [dbo].[ModuleDefinitionsNames] DROP CONSTRAINT [FK_ModuleDefinitionsNames_ModuleDefinitions]
ALTER TABLE [dbo].[ModuleDefinitionsNames]
    ADD CONSTRAINT [FK_ModuleDefinitionsNames_language] FOREIGN KEY( [Language] )
    REFERENCES [dbo].[language] ( [Language] )
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
ALTER TABLE [dbo].[ModuleDefinitionsNames]
    ADD CONSTRAINT [FK_ModuleDefinitionsNames_ModuleDefinitions] FOREIGN KEY( [ModuleDefID] )
    REFERENCES [dbo].[ModuleDefinitions] ( [ModuleDefID] )
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
GO
updatelanguagecontext 'fr','HS_EnableSSLInfo','SSL sera appliqué sur certaine page', 'HostSettings'
GO
updatelanguagecontext 'fr','HS_EnableSSL','Forcer SSL', 'HostSettings'
GO
updatelanguagecontext 'fr','SS_Use_SSL','Utiliser SSL', 'SiteSettings'
GO
updatelanguagecontext 'en','SS_Use_SSL','Use SSL', 'SiteSettings'
GO
updatelanguagecontext 'en','HS_EnableSSLInfo','Will force SSL on certain page like login', 'HostSettings'
GO
updatelanguagecontext 'en','HS_EnableSSL','Force SSL', 'HostSettings'
GO


IF NOT EXISTS (SELECT TABLE_NAME,COLUMN_NAME
                         FROM INFORMATION_SCHEMA.COLUMNS
                         WHERE TABLE_NAME=N'Tabs' AND COLUMN_NAME = N'ssl' )
begin
ALTER table [dbo].[Tabs] add [ssl] [bit] NOT NULL DEFAULT ((0))
end
GO
IF NOT EXISTS (SELECT TABLE_NAME,COLUMN_NAME
                         FROM INFORMATION_SCHEMA.COLUMNS
                         WHERE TABLE_NAME=N'Portals' AND COLUMN_NAME = N'ssl' )
begin
ALTER table [dbo].[Portals] add [ssl] [bit] NOT NULL DEFAULT ((0))
end
GO
IF NOT EXISTS (SELECT TABLE_NAME,COLUMN_NAME
                         FROM INFORMATION_SCHEMA.COLUMNS
                         WHERE TABLE_NAME=N'ModuleDefinitions' AND COLUMN_NAME = N'ssl' )
begin
ALTER table [dbo].[ModuleDefinitions] add [ssl] [bit] NOT NULL DEFAULT ((0))
end
GO
IF NOT EXISTS (SELECT TABLE_NAME,COLUMN_NAME
                         FROM INFORMATION_SCHEMA.COLUMNS
                         WHERE TABLE_NAME=N'ModuleDefinitions' AND COLUMN_NAME = N'defedit' )
begin
ALTER table [dbo].[ModuleDefinitions] add [defedit] [bit] NOT NULL DEFAULT ((0))
end
GO
update ModuleDefinitions
set    ssl = 1
where  FriendlyName = 'Register' 
or FriendlyName = 'Signup' 
or FriendlyName = 'User Roles'
or FriendlyName = 'Gestion usagers' 
or FriendlyName = 'Paramètres d''hébergement' 
or FriendlyName = 'Paramètres du site' 
or FriendlyName = 'Gestion usagers' 
or FriendlyName = 'Register' 
GO
update ModuleDefinitions
set    defedit = 1
where  FriendlyName = 'Onglets' 
or FriendlyName = 'Register' 
or FriendlyName = 'Edit Access Denied' 
or FriendlyName = 'Access Denied' 
or FriendlyName = 'Privacy' 
or FriendlyName = 'Terms' 
or FriendlyName = 'Demo' 
or FriendlyName = 'Signup' 
or FriendlyName = 'Gestion fichiers' 
or FriendlyName = 'User Roles'
or FriendlyName = 'Bienvenue' 
or FriendlyName = 'Fournisseurs' 
or FriendlyName = 'Banner'
or FriendlyName = 'Module' 
or FriendlyName = 'Gestion usagers' 
or FriendlyName = 'PrivateMessages'
or FriendlyName = 'Manage UDT'
or FriendlyName = 'UserInfo'
or FriendlyName = 'Vendor Feedback'
or FriendlyName = 'Liste membres'
GO
----------------------------------------------------
-- dbo.GetAdminModuleDefinition
----------------------------------------------------

ALTER procedure [dbo].[GetAdminModuleDefinition]

@Language nvarchar(5),
@ModuleDefID int
as
select ModuleDefinitions.FriendlyName,
	   ModuleDefinitions.DesktopSrc,
       ModuleDefinitions.HelpSrc,
       ModuleDefinitions.AdminOrder,
       ModuleDefinitions.EditSrc,
       ModuleDefinitions.Secure,
       ModuleDefinitions.ssl,
       ModuleDefinitions.defedit,
       ModuleDefinitions.AdminTabIcon,
       ModuleDefinitions.EditModuleIcon,
       ModuleDefinitions.IsPremium,
		'ModuleTitle' = case when M1.FriendlyName <> '' then M1.FriendlyName else ModuleDefinitions.FriendlyName end,
		'Description'  = case when M1.Description <> '' then M1.Description else ModuleDefinitions.Description end
  from   ModuleDefinitions
  left outer join ModuleDefinitionsNames M1 on ModuleDefinitions.ModuleDefID = M1.ModuleDefID and M1.Language = @Language
  where  ModuleDefinitions.ModuleDefID = @ModuleDefID and (ModuleDefinitions.IsAdmin = 1 or ModuleDefinitions.IsHost = 1)
GO
----------------------------------------------------
-- dbo.GetSingleModuleDefinitionByName
----------------------------------------------------

ALTER procedure [dbo].[GetSingleModuleDefinitionByName]
@Language nvarchar(5),
@FriendlyName nvarchar(128)
as
select
  ModuleDefinitions.ModuleDefID,
  ModuleDefinitions.EditSrc,
  ModuleDefinitions.Secure,
  ModuleDefinitions.ssl,
  ModuleDefinitions.defedit,
  ModuleDefinitions.EditModuleIcon,
  ModuleDefinitions.FriendlyName,
  'ModuleTitle' = case when M1.FriendlyName <> '' then M1.FriendlyName else ModuleDefinitions.FriendlyName end,
  'Description'  = case when M1.Description <> '' then M1.Description else ModuleDefinitions.Description end
  from   ModuleDefinitions
  left outer join ModuleDefinitionsNames M1 on ModuleDefinitions.ModuleDefID = M1.ModuleDefID and M1.Language = @Language
  where  ModuleDefinitions.FriendlyName = @FriendlyName
GO
----------------------------------------------------
-- dbo.GetPortalSettings
----------------------------------------------------

ALTER procedure [dbo].[GetPortalSettings]

@PortalAlias nvarchar(50),
@TabID       int,
@Language    nvarchar(5)

as

declare @PortalID int
declare @VerifyTabID int

/* convert PortalAlias to PortalID */

select @PortalID = null

select @PortalID = PortalID
from   PortalAlias
where  PortalAlias.PortalAlias = @PortalAlias

if @PortalID is null
begin
  select @PortalID = PortalID
  from   PortalAlias
  where  PortalAlias.PortalAlias = '.default' /* use default site */
end

select @VerifyTabID = null

/* verify the TabID belongs to the portal */
if @TabID <> 0
begin
  select @VerifyTabID = Tabs.TabID
  from   Tabs
  left outer join Portals on Tabs.PortalID = Portals.PortalID
  where  TabId = @TabId
  and    ( Portals.PortalID = @PortalID or Tabs.PortalId is null )
end
else
begin
  select @VerifyTabID = null
end

/* get the TabID if none provided */
if @VerifyTabID is null
begin
  select @TabID = Tabs.TabID
  from Tabs
  inner join Portals on Tabs.PortalID = Portals.PortalID
  where Portals.PortalID = @PortalID
  and Tabs.TabOrder = 1  
end

/* First, get Out Params */
select 'PortalAlias' = @PortalAlias,
       Portals.PortalID,
       Portals.GUID,
       Portals.PortalName,
       Portals.LogoFile,
       Portals.FooterText,
       Portals.ExpiryDate,
       Portals.UserRegistration,
       Portals.BannerAdvertising,
       Portals.Currency,
       Portals.AdministratorId,
       Users.Email,
       Portals.HostFee,
       Portals.HostSpace,
       Portals.AdministratorRoleId,
       Portals.RegisteredRoleId,
       Portals.Description,
       Portals.KeyWords,
       Portals.BackgroundFile,
       Portals.SiteLogHistory,
       Portals.TimeZone,
       Portals.ssl,
       'SuperUserId' = ( select UserID from Users where IsSuperUser = 1 ),
       Tabs.TabID,
       Tabs.TabOrder,
       'TabName' = case when R1.TabName <> '' then R1.TabName else Tabs.TabName end,
       Tabs.FriendlyTabName,
       Tabs.css,
       Tabs.skin,
       'Tabssl' = Tabs.ssl,
       Tabs.AuthorizedRoles,
       Tabs.AdministratorRoles,
       Tabs.ShowFriendly,
       Tabs.LeftPaneWidth,
       Tabs.RightPaneWidth,
       Tabs.IsVisible,
       Tabs.DisableLink,
       'ParentId' = isnull(Tabs.ParentID,-1),
       Tabs.Level,
       Tabs.IconFile,
       'HasChildren' = case when exists (select 1 from Tabs T2 where T2.ParentId = Tabs.TabId) then 'true' else 'false' end
from   Tabs
inner join Portals on Portals.PortalID = @PortalID
inner join Users on Portals.AdministratorId = Users.UserId
left outer join TabsName R1 on Tabs.TabID = R1.TabID and R1.Language = @Language
where  Tabs.TabID = @TabID

/* Then, get the DataTable of module info */
select Modules.*, ModuleDefinitions.*
from   Modules
inner join ModuleDefinitions on Modules.ModuleDefID = ModuleDefinitions.ModuleDefID
inner join Tabs on Modules.TabID = Tabs.TabID
where   (Modules.Language = @Language or Modules.Language = '' or Modules.Language is null) and
 (Modules.TabID = @TabID  or    (Modules.AllTabs = 1 and Tabs.PortalID = @PortalID))
order by ModuleOrder, ModuleID
GO
----------------------------------------------------
-- dbo.GetPortalTabs /* Get Tabs list */
----------------------------------------------------

ALTER procedure [dbo].[GetPortalTabs]
@PortalID   int,
@Language    nvarchar(5)
as

select 'TabName' = case when R1.TabName <> '' then R1.TabName else Tabs.TabName end,
       FriendlyTabName,
       ShowFriendly,
       AuthorizedRoles,
       AdministratorRoles,
       Tabs.TabID,
       TabOrder,
       IsVisible,
	   Tabs.ssl,
       DisableLink,
       'ParentId' = isnull(Tabs.ParentID,-1),
       Tabs.Level,
       Tabs.IconFile,
       'HasChildren' = case when exists (select 1 from Tabs T2 where T2.ParentId = Tabs.TabId) then 'true' else 'false' end
from   Tabs
left outer join TabsName R1 on Tabs.TabID = R1.TabID and R1.Language = @Language
where  Tabs.PortalID = @PortalId
order  by TabOrder, TabName
GO
DELETE FROM [dbo].[CodeProcessor]
      WHERE processor not like '%paypal%'
GO
if not exists ( select * from [dbo].[CodeProcessor] where processor = 'SandBoxPayPal' )
begin
  insert into [dbo].[CodeProcessor] (
    processor,
    URL  ) 
  values (
    'SandBoxPayPal',
    'http://www.sandbox.paypal.com'
    )
end
GO
if not exists ( select * from [dbo].[HostSettings] where SettingName = 'chkEnableSSL' )
begin
  insert into [dbo].[HostSettings] (
    SettingName,
    SettingValue  ) 
  values (
    'chkEnableSSL',
    'N'
    )
end
GO

----------------------------------------------------
-- dbo.UpdatePortalInfo
----------------------------------------------------

ALTER procedure [dbo].[UpdatePortalInfo]
@PortalID           int,
@PortalName         nvarchar(128),
@PortalAlias        nvarchar(200) = null,
@LogoFile           nvarchar(50) = null,
@FooterText         nvarchar(100) = null,
@ExpiryDate         datetime = null,
@UserRegistration   int = null,
@BannerAdvertising  int = null,
@Currency           char(3) = null,
@AdministratorId    int = null,
@HostFee            money = 0,
@HostSpace          int = null,
@PaymentProcessor   nvarchar(50) = null,
@ProcessorUserId    nvarchar(50) = null,
@ProcessorPassword  nvarchar(50) = null,
@Description        nvarchar(500) = null,
@KeyWords           nvarchar(500) = null,
@BackgroundFile     nvarchar(50) = null,
@SiteLogHistory     int = null,
@TimeZone			int,
@SSl				bit
as
update Portals
set    PortalName = @PortalName,
       PortalAlias = isnull(@PortalAlias,PortalAlias),
       LogoFile = @LogoFile,
       FooterText = @FooterText,
       ExpiryDate = @ExpiryDate,
       UserRegistration = @UserRegistration,
       BannerAdvertising = @BannerAdvertising,
       Currency = @Currency,
       AdministratorId = @AdministratorId,
       HostFee = @HostFee,
       HostSpace = @HostSpace,
       PaymentProcessor = @PaymentProcessor,
       ProcessorUserId = @ProcessorUserId,
       ProcessorPassword = @ProcessorPassword,
       Description = @Description,
       KeyWords = @KeyWords,
       BackgroundFile = @BackgroundFile,
       SiteLogHistory = @SiteLogHistory,
       TimeZone = @TimeZone,
	   ssl = @SSL
where  PortalID = @PortalID
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetSiteModule]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure [dbo].[GetSiteModule]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdatePortalAlias]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure [dbo].[UpdatePortalAlias]
GO
----------------------------------------------------
-- dbo.UpdatePortalAlias
----------------------------------------------------

create procedure [dbo].[UpdatePortalAlias]
@PortalID int,
@PortalAlias nvarchar(50),
@SubPortal bit,
@ssl   bit
as
if not exists ( select * from PortalAlias where PortalAlias = @PortalAlias )
begin
insert into PortalAlias (
  PortalID,
  PortalAlias,
  SubPortal,
  ssl
)
values (
  @PortalID,
  @PortalAlias,
  @SubPortal,
  @ssl
)
end
else
begin
  update PortalAlias
  set PortalID = @PortalID, Subportal = @Subportal, ssl = @ssl
  where PortalAlias = @PortalAlias
end
GO
if Not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PortalAlias]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
CREATE TABLE [dbo].[PortalAlias](
	[PortalID] [int] NOT NULL,
	[PortalAlias] [nvarchar](50) NOT NULL,
	[SubPortal] [bit] NOT NULL CONSTRAINT [DF_PortalAlias_SubPortal]  DEFAULT ((0)),
	[ssl] [bit] NOT NULL CONSTRAINT [DF_PortalAlias_ssl]  DEFAULT ((0)),
 CONSTRAINT [PK_PortalAlias] PRIMARY KEY CLUSTERED 
(
	[PortalAlias] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
if Not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PortalAlias]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
ALTER TABLE [dbo].[PortalAlias]  WITH CHECK ADD  CONSTRAINT [FK_PortalAlias_Portals] FOREIGN KEY([PortalID])
REFERENCES [dbo].[Portals] ([PortalID])
ON DELETE CASCADE
GO
if Not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PortalAlias]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
ALTER TABLE [dbo].[PortalAlias] CHECK CONSTRAINT [FK_PortalAlias_Portals]
GO
declare @PortalId int
declare @index int
declare @TempAlias Nvarchar(200)
declare @slice nvarchar(200)
select @PortalId = min(PortalId)
from   Portals
while @PortalId is not null
begin
	select @TempAlias = PortalAlias
    from   Portals
    where  PortalId = @PortalId

	select @index = 1

	while @index != 0 and @TempAlias is not null

	begin
		select @index = charindex(',' ,@TempAlias)
			if @index !=0
				select @slice = left(@TempAlias,@index - 1)
			else
				select @slice = @TempAlias
		exec updatePortalAlias @PortalID, @slice, 0, 0
		select @TempAlias = right(@TempAlias,len(@TempAlias) - @index)
	if len(@TempAlias) = 0 break
	end 
	select @PortalId = min(PortalId)
	from   Portals 
where   PortalId > @PortalId
end
GO
----------------------------------------------------
-- dbo.GetPortalByAlias
----------------------------------------------------
----------------------------------------------------
-- dbo.GetPortalByAlias
----------------------------------------------------

ALTER procedure [dbo].[GetPortalByAlias]
@PortalAlias nvarchar(50)
as
declare @PortalID int
select @PortalID = null
select @PortalID = PortalID
from   PortalAlias
where  PortalAlias = @PortalAlias
if @PortalID is null
begin
select @PortalID = PortalID
from   PortalAlias
where  PortalAlias = '.default'
if @PortalID is not null
begin
exec UpdatePortalAlias @PortalID, @PortalAlias, 0, 0
end
end
select 'PortalID' = @PortalID

GO
----------------------------------------------------
-- dbo.GetPortalByTab
----------------------------------------------------

ALTER procedure [dbo].[GetPortalByTab]
@TabID int,
@PortalAlias nvarchar(50)
 
as
declare @PortalID int
select @PortalID = -1
select @PortalID = PortalID
from   Tabs
where  TabID = @TabID
if @PortalID is null /* SuperTab */
begin
  select 'PortalAlias' = @PortalAlias
end
else
begin
  select PortalAlias
  from   PortalAlias
  inner join Tabs on PortalAlias.PortalID = Tabs.PortalID
  where  TabID = @TabID
  and    PortalAlias = @PortalAlias
end

GO
----------------------------------------------------
-- dbo.AddPortalInfo
----------------------------------------------------
ALTER procedure [dbo].[AddPortalInfo]
@Language           nvarchar(5),
@HomePage			bit,
@PortalName         nvarchar(128),
@PortalAlias        nvarchar(200),
@Currency           char(3) = null,
@FirstName          nvarchar(100),
@LastName           nvarchar(100),
@Username           nvarchar(100),
@Password           nvarchar(50),
@Email              nvarchar(100),
@ExpiryDate         datetime = null,
@HostFee            money = 0,
@HostSpace          int = null,
@SiteLogHistory     int = null,
@PortalID           int OUTPUT

as

declare @UserId int
declare @AdministratorRoleId int
declare @RegisteredRoleId    int
declare @SuperUserId int
declare @TimeZone int

declare @HomePageNV nvarchar(50)
declare @HomePageFR nvarchar(50)

declare @RoleName nvarchar(50)
declare @RoleDescription nvarchar(1000)

begin transaction

Select @TimeZone = SettingValue  FROM HostSettings  WHERE HostSettings.SettingName = 'TimeZone'

insert into Portals (
  PortalName,
  PortalAlias,
  LogoFile,
  FooterText,
  ExpiryDate,
  UserRegistration,
  BannerAdvertising,
  Currency,
  AdministratorId,
  HostFee,
  HostSpace,
  AdministratorRoleId,
  RegisteredRoleId,
  Description,
  KeyWords,
  BackgroundFile,
  SiteLogHistory,
  TimeZone
)
values (
  @PortalName,
  @PortalAlias,
  null,
  null,
  @ExpiryDate,
  0,
  0,
  @Currency,
  null,
  @HostFee,
  @HostSpace,
  null,
  null,
  @PortalName,
  @PortalName,
  null,
  @SiteLogHistory,
  @TimeZone
)

select @PortalID = @@IDENTITY


select @RoleName = Language.AdminRole From Language where Language.language = @Language
select @RoleDescription = Language.AdminRoleDesc From Language where Language.language = @Language


insert into Roles (
  PortalID,
  RoleName,
  Description,
  ServiceFee,
  BillingPeriod,
  BillingFrequency,
  TrialFee,
  TrialPeriod,
  TrialFrequency,
  IsPublic,
  AutoAssignment
)
values (
  @PortalID,
  @RoleName,
  @RoleDescription,
  0,
  null,
  'M',
  null,
  null,
  'N',
  0,
  0
)

select @AdministratorRoleId = @@IDENTITY

select @RoleName = Language.UserRole From Language where Language.language = @Language
select @RoleDescription = Language.UserRoleDesc From Language where Language.language = @Language


insert into Roles (
  PortalID,
  RoleName,
  Description,
  ServiceFee,
  BillingPeriod,
  BillingFrequency,
  TrialFee,
  TrialPeriod,
  TrialFrequency,
  IsPublic,
  AutoAssignment
)
values (
  @PortalID,
  @RoleName,
  @RoleDescription,
  0,
  null,
  'N',
  null,
  null,
  'N',
  0,
  1
)

select @RegisteredRoleId = @@IDENTITY

if @HomePage = 1 begin

select @HomePageNV = Language.HomePage From Language where Language.language = @Language
select @HomePageFR = Language.FriendlyHomePage From Language where Language.language = @Language

insert into Tabs (
    PortalID,
    TabOrder,
    TabName,
    AuthorizedRoles,
    AdministratorRoles,
    FriendlyTabName,
    ShowFriendly,
    LeftPaneWidth,
    RightPaneWidth,
    IsVisible,
    ParentId,
    IconFile,
    Level
) 
values (
    @PortalID,
    1,
    @HomePageNV,
    '-1;',
    null,
    @HomePageFR,
    1,
    '200',
    '200',
    1,
    null,
    null,
    0
)
end


select @UserId = null


select @SuperUserId = UserId
from   Users
where  IsSuperUser = 1
and    Username = @Username

/* validate to make sure not using superusername */


select @UserId = Users.UserId
from   UserPortals
inner join Users on UserPortals.UserId = Users.UserId
where  PortalID = @PortalID
and    Username = @Username



if @UserId is null
begin
  insert into Users (
    FirstName,
 LastName,
    Username, 
    Password,
    Email
  )
  values (
    @FirstName,
    @LastName,
    @Username,
    @Password,
    @Email
  )

  select @UserId = @@IDENTITY
end


insert into UserPortals (
  UserId,
  PortalId,
  Authorized,
  CreatedDate,
  LastLoginDate
)
values (
  @UserId,
  @PortalID,
  1,
  getdate(),
  getdate()
)

if not exists ( select 1 from UserRoles where UserId = @UserId and RoleID = @AdministratorRoleId )
begin
  insert into UserRoles (
    UserId,
    RoleId,
    ExpiryDate
  )
  values (
    @UserId,
    @AdministratorRoleId, /* Administrators */
    null
  )
end

if not exists ( select 1 from UserRoles where UserId = @UserId and RoleID = @RegisteredRoleId )
begin
  insert into UserRoles (
    UserId,
    RoleId,
    ExpiryDate
  )
  values (
    @UserId,
    @RegisteredRoleId, /* Registered */
    null
  )
end

update Portals
set    AdministratorId = @UserId,
       AdministratorRoleId = @AdministratorRoleId,
       RegisteredRoleId = @RegisteredRoleId
where  PortalID = @PortalID

 
exec UpdatePortalSetting @PortalId, 'container', '<table cellpadding="2" cellspacing="0" width="100%"[COLOR][BORDER]><tr><td[ALIGN]>[MODULE]</td></tr></table>'



declare @index int
declare @TempAlias Nvarchar(200)
declare @slice nvarchar(200)
select @TempAlias = @PortalAlias
select @index = 1
while @index != 0 and @TempAlias is not null
	begin
		select @index = charindex(',' ,@TempAlias)
			if @index !=0
				select @slice = left(@TempAlias,@index - 1)
			else
				select @slice = @TempAlias
		exec updatePortalAlias @PortalID, @slice, 0, 0
		select @TempAlias = right(@TempAlias,len(@TempAlias) - @index)
	if len(@TempAlias) = 0 break
	end 



if @@error <> 0
  rollback transaction
else
  commit transaction

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetPortalAlias]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure [dbo].[GetPortalAlias]
GO
----------------------------------------------------
-- dbo.GetPortalAlias
----------------------------------------------------

CREATE procedure [dbo].[GetPortalAlias]
@PortalID int
as
select *
from   PortalAlias
where  PortalID = @PortalID
ORDER BY PortalAlias
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeletePortalAlias]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure [dbo].[DeletePortalAlias]
GO
----------------------------------------------------
-- dbo.DeletePortalAlias
----------------------------------------------------

CREATE procedure [dbo].[DeletePortalAlias]
@PortalAlias nvarchar(50)
as
  delete 
  from   PortalAlias
  where  PortalAlias = @PortalAlias

GO
----------------------------------------------------
-- dbo.GetPortals
----------------------------------------------------

ALTER procedure [dbo].[GetPortals]
as
select PortalID, PortalName, HostSpace, HostFee ,ExpiryDate,
    'PortalAlias' = (select max(PortalAlias) from portalalias where  PortalAlias.PortalId = Portals.PortalId ),
	'Users' = ( select count(*) from UserPortals where UserPortals.PortalId = Portals.PortalId )
from   Portals
order by PortalName 

