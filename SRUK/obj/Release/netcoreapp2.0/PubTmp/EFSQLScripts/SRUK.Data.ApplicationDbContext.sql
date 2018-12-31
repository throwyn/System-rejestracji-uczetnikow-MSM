IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Email] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [LockoutEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [PasswordHash] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'00000000000000_CreateIdentitySchema', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    DROP INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    DROP INDEX [RoleNameIndex] ON [AspNetRoles];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    DROP INDEX [UserNameIndex] ON [AspNetUsers];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Organisation] nvarchar(100) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    CREATE TABLE [Messages] (
        [Id] bigint NOT NULL IDENTITY,
        [Content] nvarchar(200) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [EditDate] datetime2 NOT NULL,
        [ReceiverId] nvarchar(450) NULL,
        [SenderId] nvarchar(450) NULL,
        CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    CREATE INDEX [IX_Messages_ReceiverId] ON [Messages] ([ReceiverId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    CREATE INDEX [IX_Messages_SenderId] ON [Messages] ([SenderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181025181340_MessagesFirstMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181025181340_MessagesFirstMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026151639_CreateEditDates')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026151639_CreateEditDates')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EditDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026151639_CreateEditDates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181026151639_CreateEditDates', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE TABLE [Season] (
        [Id] bigint NOT NULL IDENTITY,
        [CreationDate] datetime2 NOT NULL,
        [EditDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [LogoFileName] nvarchar(max) NOT NULL,
        [MainImageFileName] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Season] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE TABLE [Paper] (
        [Id] bigint NOT NULL IDENTITY,
        [AuthorId] nvarchar(450) NULL,
        [CreationDate] datetime2 NOT NULL,
        [EditDate] datetime2 NOT NULL,
        [IsPaid] bit NOT NULL,
        [SeasonId] bigint NOT NULL,
        [Status] smallint NOT NULL,
        [Title] nvarchar(200) NOT NULL,
        CONSTRAINT [PK_Paper] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Paper_AspNetUsers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Paper_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE TABLE [PaperVersion] (
        [Id] bigint NOT NULL IDENTITY,
        [CreationDate] datetime2 NOT NULL,
        [EditDate] datetime2 NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [PaperId] bigint NOT NULL,
        [Status] smallint NOT NULL,
        CONSTRAINT [PK_PaperVersion] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PaperVersion_Paper_PaperId] FOREIGN KEY ([PaperId]) REFERENCES [Paper] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE TABLE [Review] (
        [Id] bigint NOT NULL IDENTITY,
        [Comment] nvarchar(max) NULL,
        [CreationDate] datetime2 NOT NULL,
        [CriticId] nvarchar(450) NULL,
        [EditDate] datetime2 NOT NULL,
        [EditorialErrors] bit NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [IsPositive] bit NOT NULL,
        [IsPulp] bit NOT NULL,
        [PaperVersionId] bigint NOT NULL,
        [RepeatReview] bit NOT NULL,
        [TechnicalErrors] bit NOT NULL,
        CONSTRAINT [PK_Review] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Review_AspNetUsers_CriticId] FOREIGN KEY ([CriticId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Review_PaperVersion_PaperVersionId] FOREIGN KEY ([PaperVersionId]) REFERENCES [PaperVersion] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE INDEX [IX_Paper_AuthorId] ON [Paper] ([AuthorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE INDEX [IX_Paper_SeasonId] ON [Paper] ([SeasonId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE INDEX [IX_PaperVersion_PaperId] ON [PaperVersion] ([PaperId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE INDEX [IX_Review_CriticId] ON [Review] ([CriticId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    CREATE INDEX [IX_Review_PaperVersionId] ON [Review] ([PaperVersionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026163847_CreateBetaModelMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181026163847_CreateBetaModelMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_AspNetUsers_SenderId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    ALTER TABLE [Paper] DROP CONSTRAINT [FK_Paper_AspNetUsers_AuthorId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    ALTER TABLE [Review] DROP CONSTRAINT [FK_Review_AspNetUsers_CriticId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    DROP INDEX [IX_Review_CriticId] ON [Review];
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'CriticId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Review] ALTER COLUMN [CriticId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_Review_CriticId] ON [Review] ([CriticId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    DROP INDEX [IX_Paper_AuthorId] ON [Paper];
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Paper') AND [c].[name] = N'AuthorId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Paper] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Paper] ALTER COLUMN [AuthorId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_Paper_AuthorId] ON [Paper] ([AuthorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    DROP INDEX [IX_Messages_SenderId] ON [Messages];
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Messages') AND [c].[name] = N'SenderId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Messages] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Messages] ALTER COLUMN [SenderId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_Messages_SenderId] ON [Messages] ([SenderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    ALTER TABLE [Messages] ADD CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    ALTER TABLE [Paper] ADD CONSTRAINT [FK_Paper_AspNetUsers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AspNetUsers] ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    ALTER TABLE [Review] ADD CONSTRAINT [FK_Review_AspNetUsers_CriticId] FOREIGN KEY ([CriticId]) REFERENCES [AspNetUsers] ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181026170402_RequiredUsersUpdateMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181026170402_RequiredUsersUpdateMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    ALTER TABLE [Season] ADD [IsDeleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    ALTER TABLE [Review] ADD [IsDeleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    ALTER TABLE [PaperVersion] ADD [IsDeleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    ALTER TABLE [Paper] ADD [IsDeleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    DROP INDEX [IX_Messages_ReceiverId] ON [Messages];
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Messages') AND [c].[name] = N'ReceiverId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Messages] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Messages] ALTER COLUMN [ReceiverId] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_Messages_ReceiverId] ON [Messages] ([ReceiverId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsDeleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    CREATE TABLE [UserDetailsViewModel] (
        [Id] nvarchar(450) NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [EditDate] datetime2 NOT NULL,
        [Email] nvarchar(max) NULL,
        [EmailConfirmed] bit NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [LockoutEnd] datetimeoffset NOT NULL,
        [Organisation] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [Role] nvarchar(max) NULL,
        [StatusMessage] nvarchar(max) NULL,
        CONSTRAINT [PK_UserDetailsViewModel] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    CREATE TABLE [UserDTO] (
        [Id] nvarchar(450) NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [EditDate] datetime2 NOT NULL,
        [Email] nvarchar(max) NULL,
        [EmailConfirmed] bit NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [LockoutEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NOT NULL,
        [NormalizedEmail] nvarchar(max) NULL,
        [NormalizedUserName] nvarchar(max) NULL,
        [Organisation] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [UserName] nvarchar(max) NULL,
        CONSTRAINT [PK_UserDTO] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    ALTER TABLE [Messages] ADD CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030112516_IsDeletedColumnMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181030112516_IsDeletedColumnMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030114751_IMessedInDbContextMigration')
BEGIN
    DROP TABLE [UserDetailsViewModel];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030114751_IMessedInDbContextMigration')
BEGIN
    DROP TABLE [UserDTO];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030114751_IMessedInDbContextMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181030114751_IMessedInDbContextMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030131147_TestTimestampMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Timestamp] rowversion NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030131147_TestTimestampMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181030131147_TestTimestampMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030131918_RemoveTimestampMigration')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'Timestamp');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Timestamp];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030131918_RemoveTimestampMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181030131918_RemoveTimestampMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030133045_AddGetUtcDateToCreationDateMigration')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Season') AND [c].[name] = N'CreationDate');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Season] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Season] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [Season] ADD DEFAULT (getutcdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030133045_AddGetUtcDateToCreationDateMigration')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'CreationDate');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Review] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [Review] ADD DEFAULT (getutcdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030133045_AddGetUtcDateToCreationDateMigration')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'PaperVersion') AND [c].[name] = N'CreationDate');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [PaperVersion] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [PaperVersion] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [PaperVersion] ADD DEFAULT (getutcdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030133045_AddGetUtcDateToCreationDateMigration')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Paper') AND [c].[name] = N'CreationDate');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Paper] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Paper] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [Paper] ADD DEFAULT (getutcdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030133045_AddGetUtcDateToCreationDateMigration')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Messages') AND [c].[name] = N'CreationDate');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Messages] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Messages] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [Messages] ADD DEFAULT (getutcdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030133045_AddGetUtcDateToCreationDateMigration')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'CreationDate');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT (getutcdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181030133045_AddGetUtcDateToCreationDateMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181030133045_AddGetUtcDateToCreationDateMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    ALTER TABLE [PaperVersion] DROP CONSTRAINT [FK_PaperVersion_Paper_PaperId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    ALTER TABLE [Review] DROP CONSTRAINT [FK_Review_PaperVersion_PaperVersionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    ALTER TABLE [PaperVersion] DROP CONSTRAINT [PK_PaperVersion];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    EXEC sp_rename N'PaperVersion', N'PaperVerison';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    EXEC sp_rename N'PaperVerison.IX_PaperVersion_PaperId', N'IX_PaperVerison_PaperId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    ALTER TABLE [PaperVerison] ADD CONSTRAINT [PK_PaperVerison] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    ALTER TABLE [PaperVerison] ADD CONSTRAINT [FK_PaperVerison_Paper_PaperId] FOREIGN KEY ([PaperId]) REFERENCES [Paper] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    ALTER TABLE [Review] ADD CONSTRAINT [FK_Review_PaperVerison_PaperVersionId] FOREIGN KEY ([PaperVersionId]) REFERENCES [PaperVerison] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181106134053_AdedReviewsToPaperVersionMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181106134053_AdedReviewsToPaperVersionMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181108132558_RemovedIsPaidAddedNIPAndDegreeMigration')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Paper') AND [c].[name] = N'IsPaid');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Paper] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Paper] DROP COLUMN [IsPaid];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181108132558_RemovedIsPaidAddedNIPAndDegreeMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Degree] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181108132558_RemovedIsPaidAddedNIPAndDegreeMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [VATID] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181108132558_RemovedIsPaidAddedNIPAndDegreeMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181108132558_RemovedIsPaidAddedNIPAndDegreeMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181110194408_addOriginalFileNameMigration')
BEGIN
    ALTER TABLE [PaperVerison] ADD [OriginalFileName] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181110194408_addOriginalFileNameMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181110194408_addOriginalFileNameMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181110215343_AddCommentMigration')
BEGIN
    CREATE TABLE [Comment] (
        [Id] bigint NOT NULL IDENTITY,
        [AuthorId] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [EditDate] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [PaperVersionId] bigint NOT NULL,
        CONSTRAINT [PK_Comment] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comment_AspNetUsers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Comment_PaperVerison_PaperVersionId] FOREIGN KEY ([PaperVersionId]) REFERENCES [PaperVerison] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181110215343_AddCommentMigration')
BEGIN
    CREATE INDEX [IX_Comment_AuthorId] ON [Comment] ([AuthorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181110215343_AddCommentMigration')
BEGIN
    CREATE INDEX [IX_Comment_PaperVersionId] ON [Comment] ([PaperVersionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181110215343_AddCommentMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181110215343_AddCommentMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111143552_NullableReviewOptionsMigration')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'TechnicalErrors');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Review] ALTER COLUMN [TechnicalErrors] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111143552_NullableReviewOptionsMigration')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'RepeatReview');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Review] ALTER COLUMN [RepeatReview] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111143552_NullableReviewOptionsMigration')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'IsPulp');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Review] ALTER COLUMN [IsPulp] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111143552_NullableReviewOptionsMigration')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'IsPositive');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Review] ALTER COLUMN [IsPositive] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111143552_NullableReviewOptionsMigration')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'FileName');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Review] ALTER COLUMN [FileName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111143552_NullableReviewOptionsMigration')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'EditorialErrors');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Review] ALTER COLUMN [EditorialErrors] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111143552_NullableReviewOptionsMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181111143552_NullableReviewOptionsMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181112181856_AddOriginalReviewFileNameMigration')
BEGIN
    ALTER TABLE [Review] ADD [OriginalFileName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181112181856_AddOriginalReviewFileNameMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181112181856_AddOriginalReviewFileNameMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration')
BEGIN
    ALTER TABLE [Season] ADD [Name] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration')
BEGIN
    ALTER TABLE [Review] ADD [CompletionDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration')
BEGIN
    ALTER TABLE [Review] ADD [Deadline] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration')
BEGIN
    ALTER TABLE [Review] ADD [Status] tinyint NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'PaperVerison') AND [c].[name] = N'Status');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [PaperVerison] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [PaperVerison] ALTER COLUMN [Status] tinyint NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration')
BEGIN
    ALTER TABLE [PaperVerison] ADD [Comment] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Paper') AND [c].[name] = N'Status');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Paper] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [Paper] ALTER COLUMN [Status] tinyint NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181114185652_AddDeadlineCompletionDateCommentChangeSatusesToByteMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [Paper] DROP CONSTRAINT [FK_Paper_AspNetUsers_AuthorId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [Paper] DROP CONSTRAINT [FK_Paper_Season_SeasonId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    DROP TABLE [Comment];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    DROP TABLE [Messages];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    DROP INDEX [IX_Paper_AuthorId] ON [Paper];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Paper') AND [c].[name] = N'AuthorId');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Paper] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [Paper] DROP COLUMN [AuthorId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    EXEC sp_rename N'Review.IsPulp', N'Unsuitable', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    EXEC sp_rename N'Paper.SeasonId', N'ParticipancyId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    EXEC sp_rename N'Paper.IX_Paper_SeasonId', N'IX_Paper_ParticipancyId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [Paper] ADD [SentToPrintDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Address] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [City] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Country] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [OrganisationAdderss] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [PostalCode] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    CREATE TABLE [Participancy] (
        [Id] bigint NOT NULL IDENTITY,
        [ConferenceParticipation] bit NOT NULL,
        [CreationDate] datetime2 NOT NULL DEFAULT (getutcdate()),
        [EditDate] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Publication] bit NOT NULL,
        [SeasonId] bigint NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Participancy] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Participancy_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Participancy_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    CREATE INDEX [IX_Participancy_SeasonId] ON [Participancy] ([SeasonId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    CREATE INDEX [IX_Participancy_UserId] ON [Participancy] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    ALTER TABLE [Paper] ADD CONSTRAINT [FK_Paper_Participancy_ParticipancyId] FOREIGN KEY ([ParticipancyId]) REFERENCES [Participancy] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181120190336_CompletelyRebuildModelAddingParticipationMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181120190336_CompletelyRebuildModelAddingParticipationMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181130121358_ChangeUtcTimeToServerTimeMigration')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Season') AND [c].[name] = N'CreationDate');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Season] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [Season] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [Season] ADD DEFAULT (getdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181130121358_ChangeUtcTimeToServerTimeMigration')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Review') AND [c].[name] = N'CreationDate');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [Review] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [Review] ADD DEFAULT (getdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181130121358_ChangeUtcTimeToServerTimeMigration')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Participancy') AND [c].[name] = N'CreationDate');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Participancy] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [Participancy] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [Participancy] ADD DEFAULT (getdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181130121358_ChangeUtcTimeToServerTimeMigration')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'PaperVerison') AND [c].[name] = N'CreationDate');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [PaperVerison] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [PaperVerison] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [PaperVerison] ADD DEFAULT (getdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181130121358_ChangeUtcTimeToServerTimeMigration')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Paper') AND [c].[name] = N'CreationDate');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Paper] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [Paper] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [Paper] ADD DEFAULT (getdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181130121358_ChangeUtcTimeToServerTimeMigration')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'CreationDate');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT (getdate()) FOR [CreationDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181130121358_ChangeUtcTimeToServerTimeMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181130121358_ChangeUtcTimeToServerTimeMigration', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181222144301_SeasonEditedMigration')
BEGIN
    EXEC sp_rename N'Season.LogoFileName', N'Location', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181222144301_SeasonEditedMigration')
BEGIN
    ALTER TABLE [Season] ADD [ConferenceEndDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181222144301_SeasonEditedMigration')
BEGIN
    ALTER TABLE [Season] ADD [ConferenceStartDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181222144301_SeasonEditedMigration')
BEGIN
    ALTER TABLE [Season] ADD [EditionNumber] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181222144301_SeasonEditedMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181222144301_SeasonEditedMigration', N'2.0.3-rtm-10026');
END;

GO

