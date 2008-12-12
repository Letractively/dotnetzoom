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