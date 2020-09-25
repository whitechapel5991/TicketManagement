CREATE TABLE [dbo].[EventSeats] (
    [Id] [int] NOT NULL IDENTITY,
    [Row] [int] NOT NULL,
    [Number] [int] NOT NULL,
    [State] [int] NOT NULL,
    [EventAreaId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.EventSeats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Areas_EventSeats] FOREIGN KEY ([EventAreaId]) REFERENCES [dbo].[EventAreas] ([Id]) ON DELETE CASCADE
)
