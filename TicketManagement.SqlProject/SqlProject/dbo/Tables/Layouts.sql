CREATE TABLE [dbo].[Layouts] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](50),
    [Description] [nvarchar](120) NOT NULL,
    [VenueId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Layouts] PRIMARY KEY ([Id])
)
