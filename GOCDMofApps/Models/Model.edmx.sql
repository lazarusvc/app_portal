
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/22/2021 10:23:49
-- Generated from EDMX file: C:\Users\lazarusa\Documents\Git Projects\GOCDMofApps\GOCDMofApps\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Apps_CategoryApps]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps] DROP CONSTRAINT [FK_Apps_CategoryApps];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_TypeApps]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps] DROP CONSTRAINT [FK_Apps_TypeApps];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_UsersRoleApps_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps_Users] DROP CONSTRAINT [FK_Apps_UsersRoleApps_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Apps_UsersRoleApps_Document]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Apps_Document] DROP CONSTRAINT [FK_Apps_UsersRoleApps_Document];
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
IF OBJECT_ID(N'[dbo].[Apps_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_Type];
GO
IF OBJECT_ID(N'[dbo].[Apps_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_Users];
GO
IF OBJECT_ID(N'[dbo].[Apps_Document]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_Document];
GO
IF OBJECT_ID(N'[dbo].[Apps_UsersRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Apps_UsersRole];
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
    [Apps_UsersRoleId] int  NOT NULL
);
GO

-- Creating table 'Apps_UsersRole'
CREATE TABLE [dbo].[Apps_UsersRole] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------