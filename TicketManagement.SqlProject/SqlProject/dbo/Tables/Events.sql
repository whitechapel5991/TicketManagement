CREATE TABLE [dbo].[Events] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](120) NOT NULL,
    [BeginDate] [datetime] NOT NULL,
    [EndDate] [datetime] NOT NULL,
    [Description] [nvarchar](max) NOT NULL,
    [Published] [bit] NOT NULL,
    [LayoutId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Events] PRIMARY KEY ([Id])
)
