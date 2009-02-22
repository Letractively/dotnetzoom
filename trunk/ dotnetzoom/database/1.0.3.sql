----------------------------------------------------
-- dbo.GetAdminModuleDefinitions
----------------------------------------------------

 
ALTER procedure [dbo].[GetAdminModuleDefinitions]
@Language nvarchar(5)
as
  select ModuleDefinitions.ModuleDefID,
		 ModuleDefinitions.ssl,
		 ModuleDefinitions.IsAdmin,
         ModuleDefinitions.IsHost,
         ModuleDefinitions.AdminOrder,
		ModuleDefinitions.EditModuleIcon,
        ModuleDefinitions.AdminTabIcon,         
         'FriendlyName' = case when M1.FriendlyName <> '' then M1.FriendlyName else ModuleDefinitions.FriendlyName end,
         'Description'  = case when M1.Description <> '' then M1.Description else ModuleDefinitions.Description end
  from   ModuleDefinitions
  left outer join ModuleDefinitionsNames M1 on ModuleDefinitions.ModuleDefID = M1.ModuleDefID and M1.Language = @Language
  where  ModuleDefinitions.IsAdmin = 1 or ModuleDefinitions.IsHost = 1
  order  by ModuleDefinitions.AdminOrder

GO

----------------------------------------------------
-- dbo.AddTab
----------------------------------------------------

ALTER procedure [dbo].[AddTab]
@PortalID           int,
@TabName            nvarchar(50),
@ShowFriendly         bit,
@FriendlyTabName      nvarchar(50),
@AuthorizedRoles    nvarchar (256),
@LeftPaneWidth      nvarchar(5),
@RightPaneWidth     nvarchar(5),
@IsVisible          bit,
@DisableLink        bit,
@ParentId           int,
@IconFile           nvarchar(100),
@AdministratorRoles nvarchar (256),
@TabID              int OUTPUT,
@css                nvarchar(256),
@skin               nvarchar(256),
@ssl				bit,
@AltURL             nvarchar(256)
as
if @ParentId is not null
begin
  select @IsVisible = 1
end
insert into Tabs (
    PortalID,
    TabName,
    ShowFriendly,
    FriendlyTabName,
    AuthorizedRoles,
    LeftPaneWidth,
    RightPaneWidth,
    IsVisible,
    DisableLink,
    ParentId,
    IconFile,
    AdministratorRoles,
    css,
    skin,
    ssl	

)
values (
    @PortalID,
    @TabName,
    @ShowFriendly,
    @FriendlyTabName,
    @AuthorizedRoles,
    @LeftPaneWidth,
    @RightPaneWidth,
    @IsVisible,
    @DisableLink,
    @ParentId,
    @IconFile,
    @AdministratorRoles,
    @css,
    @skin,
	@ssl	

)
select @TabID = @@IDENTITY

GO

----------------------------------------------------
-- dbo.UpdateTab
----------------------------------------------------
ALTER procedure [dbo].[UpdateTab]

@TabID              int,
@TabName            nvarchar(50),
@ShowFriendly         bit,
@FriendlyTabName      nvarchar(50),
@AuthorizedRoles    nvarchar(256),
@LeftPaneWidth      nvarchar(5),
@RightPaneWidth     nvarchar(5),
@IsVisible          bit,
@DisableLink        bit,
@ParentId           int,
@IconFile           nvarchar(100),
@AdministratorRoles nvarchar(256),
@css                nvarchar(256),
@skin               nvarchar(256),
@ssl				bit,
@AltURL             nvarchar(256)

as

declare @PortalID int

select @PortalID = PortalID
from   Tabs
where  TabID = @TabID

if (exists ( select 1 from Tabs where ParentId = @TabId )) or (@ParentId is not null)
begin
  select @IsVisible = 1
end

update Tabs
set    TabName = @TabName,
       ShowFriendly = @ShowFriendly,
       FriendlyTabName = @FriendlyTabName,
       AuthorizedRoles = @AuthorizedRoles,
       LeftPaneWidth = @LeftPaneWidth,
       RightPaneWidth = @RightPaneWidth,
       IsVisible = @IsVisible,
       DisableLink = @DisableLink,
       ParentId = @ParentId,
       IconFile = @IconFile,
       AdministratorRoles = @AdministratorRoles,
       css = @css,
       skin = @skin,
	   ssl = @ssl	
where  TabID = @TabID

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
if @PortalID is null 
begin
  select 'PortalAlias' = @PortalAlias
end
else
begin
  declare @TempPortalAlias nvarchar(50)
  select  @TempPortalAlias = null
  select @TempPortalAlias = PortalAlias
  from   PortalAlias
  where  PortalAlias.PortalID = @PortalID
  and    PortalAlias = @PortalAlias


if @TempPortalAlias is null 
begin
  select @TempPortalAlias = PortalAlias
  from   PortalAlias
  where  PortalAlias.PortalID = @PortalID
  and    PortalAlias like + '%' + @PortalAlias
end

if @TempPortalAlias is null 
begin
  select @TempPortalAlias = PortalAlias
  from   PortalAlias
  where  PortalAlias.PortalID = @PortalID
  and    PortalAlias like +  @PortalAlias + '%'
end

if @TempPortalAlias is null 
begin
  select 'PortalAlias' = @PortalAlias
end
else
begin
  select 'PortalAlias' = @TempPortalAlias
end
end

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
Declare @IsSSLAlias bit
Declare @SSLAlias nvarchar(50)
Declare @NOSSLAlias nvarchar(50)
Declare @Subportal bit

Select @Subportal = (select subportal from PortalAlias where PortalAlias.PortalAlias = @PortalAlias)
Select @IsSSLAlias =  (select ssl from PortalAlias where PortalAlias.PortalAlias = @PortalAlias) 

If @IsSSLAlias = 0
begin
Select @SSLAlias =  (select min(PortalAlias.PortalAlias) from PortalAlias where PortalAlias.PortalID = @PortalID and PortalAlias.ssl = 1 and PortalAlias.PortalAlias <> '.default' ) 
Select @NOSSLAlias = @PortalAlias
end
else
begin
Select @SSLAlias = @PortalAlias
If @Subportal = 0
begin
Select @NOSSLAlias = @PortalAlias
end
else
begin
Select @NOSSLAlias =  (select min(PortalAlias.PortalAlias) from PortalAlias where PortalAlias.PortalID = @PortalID and PortalAlias.subportal = 0 and PortalAlias.PortalAlias <> '.default') 
end
end


select 'PortalAlias' = @PortalAlias,
	   'IsSSLAlias' =  @IsSSLAlias,
	   'IsSubportal'  = @Subportal,
	   'SSLAlias' =  @SSLAlias ,
	   'NOSSLAlias' =  @NOSSLAlias,
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

