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
updatelanguagecontext 'en','HS_EnableSSLInfo','Will force SSL on certain page like login', 'HostSettings'
GO
updatelanguagecontext 'en','HS_EnableSSL','Force SSL', 'HostSettings'
GO
ALTER table [dbo].[Tabs] add [ssl] [bit] NOT NULL DEFAULT ((0))
GO
ALTER table [dbo].[Portals] add [ssl] [bit] NOT NULL DEFAULT ((0))
GO
----------------------------------------------------
-- dbo.GetPortalSettings
----------------------------------------------------

ALTER procedure [dbo].[GetPortalSettings]

@PortalAlias nvarchar(200),
@TabID       int,
@Language    nvarchar(5)

as

declare @PortalID int
declare @VerifyTabID int

/* convert PortalAlias to PortalID */

select @PortalID = null

select @PortalID = PortalID
from   Portals
where  PortalAlias = @PortalAlias

if @PortalID is null
begin
  select @PortalID = min(PortalID)
  from   Portals
  where  PortalAlias like '%' + @PortalAlias + '%' /* multiple alias may be specified seperated by commas */
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
select Portals.PortalAlias,
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




