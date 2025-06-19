
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/15/2025 15:11:49
-- Generated from EDMX file: C:\Users\lazarusa\Documents\Git Projects\GOCDMofApps\GOCDMofApps\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GOCDMoFApps];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Apps_CategoryApps]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps] DROP CONSTRAINT [FK_Apps_CategoryApps];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_processesApps_REF_processes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps_REF_processes] DROP CONSTRAINT [FK_Apps_processesApps_REF_processes];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_reportsApps_reports_params]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps_reports_params] DROP CONSTRAINT [FK_Apps_reportsApps_reports_params];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_TypeApps]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps] DROP CONSTRAINT [FK_Apps_TypeApps];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_userRolesApps_reports]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps_reports] DROP CONSTRAINT [FK_Apps_userRolesApps_reports];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_UsersRoleApps]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps] DROP CONSTRAINT [FK_Apps_UsersRoleApps];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_UsersRoleApps_Document]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps_Document] DROP CONSTRAINT [FK_Apps_UsersRoleApps_Document];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_UsersRoleApps_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps_Users] DROP CONSTRAINT [FK_Apps_UsersRoleApps_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Apps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps];
GO
IF OBJECT_ID(N'[dbo].[Apps_Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_Category];
GO
IF OBJECT_ID(N'[dbo].[Apps_Document]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_Document];
GO
IF OBJECT_ID(N'[dbo].[Apps_processes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_processes];
GO
IF OBJECT_ID(N'[dbo].[Apps_REF_processes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_REF_processes];
GO
IF OBJECT_ID(N'[dbo].[Apps_reports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_reports];
GO
IF OBJECT_ID(N'[dbo].[Apps_reports_params]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_reports_params];
GO
IF OBJECT_ID(N'[dbo].[Apps_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_Type];
GO
IF OBJECT_ID(N'[dbo].[Apps_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_Users];
GO
IF OBJECT_ID(N'[dbo].[Apps_UsersRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_UsersRole];
GO
IF OBJECT_ID(N'[ModelStoreContainer].[vw_search]', 'U') IS NOT NULL
    DROP TABLE [ModelStoreContainer].[vw_search];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Apps'
CREATE TABLE [dbo].[Apps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [desc] nvarchar(max)  NULL,
    [Apps_CategoryId] int  NOT NULL,
    [url] nvarchar(max)  NULL,
    [Apps_TypeId] int  NOT NULL,
    [loc] nvarchar(max)  NULL,
    [featured] bit  NULL,
    [active] bit  NULL,
    [image] nvarchar(max)  NULL,
    [Apps_UsersRoleId] int  NOT NULL
);
GO

-- Creating table 'Apps_Category'
CREATE TABLE [dbo].[Apps_Category] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Apps_Type'
CREATE TABLE [dbo].[Apps_Type] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Apps_Users'
CREATE TABLE [dbo].[Apps_Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [firstName] nvarchar(max)  NULL,
    [lastName] nvarchar(max)  NULL,
    [username] nvarchar(max)  NULL,
    [Apps_UsersRoleId] int  NOT NULL
);
GO

-- Creating table 'Apps_Document'
CREATE TABLE [dbo].[Apps_Document] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [desc] nvarchar(max)  NULL,
    [media] nvarchar(max)  NULL,
    [media_type] nvarchar(max)  NULL,
    [Apps_UsersRoleId] int  NOT NULL,
    [tags] nvarchar(max)  NULL
);
GO

-- Creating table 'Apps_UsersRole'
CREATE TABLE [dbo].[Apps_UsersRole] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'vw_search'
CREATE TABLE [dbo].[vw_search] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [url] nvarchar(max)  NULL,
    [name] nvarchar(max)  NULL
);
GO

-- Creating table 'Apps_reports'
CREATE TABLE [dbo].[Apps_reports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [desc] nvarchar(max)  NOT NULL,
    [FK_REF_userRolesId] int  NOT NULL,
    [paramCheck] bit  NOT NULL
);
GO

-- Creating table 'Apps_reports_params'
CREATE TABLE [dbo].[Apps_reports_params] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [param_key] nvarchar(max)  NOT NULL,
    [param_value] nvarchar(max)  NOT NULL,
    [param_dataType] nvarchar(max)  NOT NULL,
    [FK_REF_reportsId] int  NOT NULL
);
GO

-- Creating table 'Apps_processes'
CREATE TABLE [dbo].[Apps_processes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [sp_name] nvarchar(max)  NOT NULL,
    [sp_desc] nvarchar(max)  NOT NULL,
    [sp_database] nvarchar(max)  NOT NULL,
    [sp_databse__user] nvarchar(max)  NOT NULL,
    [sp_databse__pass] nvarchar(max)  NOT NULL,
    [sp_datasource] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Apps_REF_processes'
CREATE TABLE [dbo].[Apps_REF_processes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [param_key] nvarchar(max)  NOT NULL,
    [param_value] nvarchar(max)  NOT NULL,
    [param_dataType] nvarchar(max)  NOT NULL,
    [FK_processesId] int  NOT NULL
);
GO

-- Creating table 'Identities'
CREATE TABLE [dbo].[Identities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [logo] nvarchar(max)  NOT NULL,
    [headerColor] nvarchar(max)  NOT NULL,
    [aboutDesc] nvarchar(max)  NOT NULL,
    [contactDesc] nvarchar(max)  NOT NULL,
    [footerDesc] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Apps_password'
CREATE TABLE [dbo].[Apps_password] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL,
    [notes] nvarchar(max)  NOT NULL,
    [tags] nvarchar(max)  NOT NULL,
    [Apps_UsersRoleId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Apps'
ALTER TABLE [dbo].[Apps]
ADD CONSTRAINT [PK_Apps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_Category'
ALTER TABLE [dbo].[Apps_Category]
ADD CONSTRAINT [PK_Apps_Category]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_Type'
ALTER TABLE [dbo].[Apps_Type]
ADD CONSTRAINT [PK_Apps_Type]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_Users'
ALTER TABLE [dbo].[Apps_Users]
ADD CONSTRAINT [PK_Apps_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_Document'
ALTER TABLE [dbo].[Apps_Document]
ADD CONSTRAINT [PK_Apps_Document]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_UsersRole'
ALTER TABLE [dbo].[Apps_UsersRole]
ADD CONSTRAINT [PK_Apps_UsersRole]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'vw_search'
ALTER TABLE [dbo].[vw_search]
ADD CONSTRAINT [PK_vw_search]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_reports'
ALTER TABLE [dbo].[Apps_reports]
ADD CONSTRAINT [PK_Apps_reports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_reports_params'
ALTER TABLE [dbo].[Apps_reports_params]
ADD CONSTRAINT [PK_Apps_reports_params]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_processes'
ALTER TABLE [dbo].[Apps_processes]
ADD CONSTRAINT [PK_Apps_processes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_REF_processes'
ALTER TABLE [dbo].[Apps_REF_processes]
ADD CONSTRAINT [PK_Apps_REF_processes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Identities'
ALTER TABLE [dbo].[Identities]
ADD CONSTRAINT [PK_Identities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Apps_password'
ALTER TABLE [dbo].[Apps_password]
ADD CONSTRAINT [PK_Apps_password]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Apps_CategoryId] in table 'Apps'
ALTER TABLE [dbo].[Apps]
ADD CONSTRAINT [FK_Apps_CategoryApps]
    FOREIGN KEY ([Apps_CategoryId])
    REFERENCES [dbo].[Apps_Category]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_CategoryApps'
CREATE INDEX [IX_FK_Apps_CategoryApps]
ON [dbo].[Apps]
    ([Apps_CategoryId]);
GO

-- Creating foreign key on [Apps_TypeId] in table 'Apps'
ALTER TABLE [dbo].[Apps]
ADD CONSTRAINT [FK_Apps_TypeApps]
    FOREIGN KEY ([Apps_TypeId])
    REFERENCES [dbo].[Apps_Type]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_TypeApps'
CREATE INDEX [IX_FK_Apps_TypeApps]
ON [dbo].[Apps]
    ([Apps_TypeId]);
GO

-- Creating foreign key on [Apps_UsersRoleId] in table 'Apps_Users'
ALTER TABLE [dbo].[Apps_Users]
ADD CONSTRAINT [FK_Apps_UsersRoleApps_Users]
    FOREIGN KEY ([Apps_UsersRoleId])
    REFERENCES [dbo].[Apps_UsersRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_UsersRoleApps_Users'
CREATE INDEX [IX_FK_Apps_UsersRoleApps_Users]
ON [dbo].[Apps_Users]
    ([Apps_UsersRoleId]);
GO

-- Creating foreign key on [Apps_UsersRoleId] in table 'Apps_Document'
ALTER TABLE [dbo].[Apps_Document]
ADD CONSTRAINT [FK_Apps_UsersRoleApps_Document]
    FOREIGN KEY ([Apps_UsersRoleId])
    REFERENCES [dbo].[Apps_UsersRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_UsersRoleApps_Document'
CREATE INDEX [IX_FK_Apps_UsersRoleApps_Document]
ON [dbo].[Apps_Document]
    ([Apps_UsersRoleId]);
GO

-- Creating foreign key on [Apps_UsersRoleId] in table 'Apps'
ALTER TABLE [dbo].[Apps]
ADD CONSTRAINT [FK_Apps_UsersRoleApps]
    FOREIGN KEY ([Apps_UsersRoleId])
    REFERENCES [dbo].[Apps_UsersRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_UsersRoleApps'
CREATE INDEX [IX_FK_Apps_UsersRoleApps]
ON [dbo].[Apps]
    ([Apps_UsersRoleId]);
GO

-- Creating foreign key on [FK_REF_reportsId] in table 'Apps_reports_params'
ALTER TABLE [dbo].[Apps_reports_params]
ADD CONSTRAINT [FK_Apps_reportsApps_reports_params]
    FOREIGN KEY ([FK_REF_reportsId])
    REFERENCES [dbo].[Apps_reports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_reportsApps_reports_params'
CREATE INDEX [IX_FK_Apps_reportsApps_reports_params]
ON [dbo].[Apps_reports_params]
    ([FK_REF_reportsId]);
GO

-- Creating foreign key on [FK_REF_userRolesId] in table 'Apps_reports'
ALTER TABLE [dbo].[Apps_reports]
ADD CONSTRAINT [FK_Apps_userRolesApps_reports]
    FOREIGN KEY ([FK_REF_userRolesId])
    REFERENCES [dbo].[Apps_UsersRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_userRolesApps_reports'
CREATE INDEX [IX_FK_Apps_userRolesApps_reports]
ON [dbo].[Apps_reports]
    ([FK_REF_userRolesId]);
GO

-- Creating foreign key on [FK_processesId] in table 'Apps_REF_processes'
ALTER TABLE [dbo].[Apps_REF_processes]
ADD CONSTRAINT [FK_Apps_processesApps_REF_processes]
    FOREIGN KEY ([FK_processesId])
    REFERENCES [dbo].[Apps_processes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_processesApps_REF_processes'
CREATE INDEX [IX_FK_Apps_processesApps_REF_processes]
ON [dbo].[Apps_REF_processes]
    ([FK_processesId]);
GO

-- Creating foreign key on [Apps_UsersRoleId] in table 'Apps_password'
ALTER TABLE [dbo].[Apps_password]
ADD CONSTRAINT [FK_Apps_UsersRoleApps_password]
    FOREIGN KEY ([Apps_UsersRoleId])
    REFERENCES [dbo].[Apps_UsersRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Apps_UsersRoleApps_password'
CREATE INDEX [IX_FK_Apps_UsersRoleApps_password]
ON [dbo].[Apps_password]
    ([Apps_UsersRoleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------