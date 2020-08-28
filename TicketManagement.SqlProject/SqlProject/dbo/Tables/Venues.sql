CREATE TABLE [dbo].[Venues]
(
	[Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (120) NOT NULL,
    [Address]     NVARCHAR (200) NOT NULL,
    [Phone]       NVARCHAR (30)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
