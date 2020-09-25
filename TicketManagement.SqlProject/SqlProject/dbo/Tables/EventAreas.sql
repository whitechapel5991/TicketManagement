CREATE TABLE [dbo].[EventAreas] (
    [Id] [int] NOT NULL IDENTITY,
    [Description] [nvarchar](200) NOT NULL,
    [CoordX] [int] NOT NULL,
    [CoordY] [int] NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
    [EventId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.EventAreas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Events_EventAreas] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE CASCADE
)
