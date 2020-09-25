CREATE TABLE [dbo].[Seats] (
    [Id] [int] NOT NULL IDENTITY,
    [Row] [int] NOT NULL,
    [Number] [int] NOT NULL,
    [AreaId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Seats] PRIMARY KEY ([Id])
)
