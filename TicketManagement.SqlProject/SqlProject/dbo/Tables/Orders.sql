CREATE TABLE [dbo].[Orders] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [int] NOT NULL,
    [EventSeatId] [int] NOT NULL,
    [Date] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Orders] PRIMARY KEY ([Id])
)
