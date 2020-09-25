CREATE TABLE [dbo].[Areas] (
    [Id] [int] NOT NULL IDENTITY,
    [Description] [nvarchar](200) NOT NULL,
    [CoordX] [int] NOT NULL,
    [CoordY] [int] NOT NULL,
    [LayoutId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Areas] PRIMARY KEY ([Id])
)
