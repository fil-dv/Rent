USE Landlord
GO

CREATE TABLE [dbo].[MyUsers] (
    [Id]                   INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL
);
   GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[MyUsers]([UserName] ASC);
  
	----------------------------------------------------------------
	GO
	CREATE TABLE [dbo].[MyRoles] (
    [Id]   INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    [Name] NVARCHAR (256) NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[MyRoles]([Name] ASC);


	-------------------------------------------------------------

	CREATE TABLE [dbo].[MyUserRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_dbo.MyUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.MyUserRoles_dbo.MyRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[MyRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.MyUserRoles_dbo.MyUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[MyUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[MyUserRoles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[MyUserRoles]([RoleId] ASC);

	----------------------------------------------------------------

	CREATE TABLE [dbo].[MyUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        INT NOT NULL,
    CONSTRAINT [PK_dbo.MyUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.MyUserLogins_dbo.MyUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[MyUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[MyUserLogins]([UserId] ASC);

	----------------------------------------------------------------

	CREATE TABLE [dbo].[MyUserClaims] (
    [Id]   INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    [UserId]     INT NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [FK_dbo.MyUserClaims_dbo.MyUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[MyUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[MyUserClaims]([UserId] ASC);

	






