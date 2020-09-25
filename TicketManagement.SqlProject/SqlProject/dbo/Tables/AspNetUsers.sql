﻿CREATE TABLE [dbo].[AspNetUsers] (
    [Id] [int] NOT NULL IDENTITY,
    [TimeZone] [nvarchar](100) NOT NULL,
    [Language] [nvarchar](10) NOT NULL,
    [FirstName] [nvarchar](20) NOT NULL,
    [Surname] [nvarchar](20) NOT NULL,
    [Balance] [decimal](18, 2) NOT NULL,
    [Email] [nvarchar](256),
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [nvarchar](max),
    [SecurityStamp] [nvarchar](max),
    [PhoneNumber] [nvarchar](max),
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEndDateUtc] [datetime],
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    [UserName] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY ([Id])
)
GO
CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName])