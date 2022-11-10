IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [PlaceRatings] (
    [GooglePlaceId] nvarchar(450) NOT NULL,
    [RatingCount] bigint NOT NULL,
    [Rating] real NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_PlaceRatings] PRIMARY KEY ([GooglePlaceId])
);
GO

CREATE TABLE [RouteRatings] (
    [GoogleRouteHash] nvarchar(450) NOT NULL,
    [RatingCount] bigint NOT NULL,
    [Rating] real NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_RouteRatings] PRIMARY KEY ([GoogleRouteHash])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221110124419_InitialMigration', N'7.0.0');
GO

COMMIT;
GO