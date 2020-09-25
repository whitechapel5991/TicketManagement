CREATE TABLE [dbo].[Venues] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](50),
    [Description] [nvarchar](120) NOT NULL,
    [Address] [nvarchar](200) NOT NULL,
    [Phone] [nvarchar](30),
    CONSTRAINT [PK_dbo.Venues] PRIMARY KEY ([Id])
)
