CREATE TABLE [dbo].[Nlog] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Application]   NVARCHAR (50)  NOT NULL,
    [Logged]        DATETIME       NOT NULL,
    [Level]         NVARCHAR (50)  NOT NULL,
    [Message]       NVARCHAR (MAX) NOT NULL,
    [UserName]      NVARCHAR (250) NULL,
    [ServerName]    NVARCHAR (MAX) NULL,
    [Port]          NVARCHAR (MAX) NULL,
    [Url]           NVARCHAR (MAX) NULL,
    [Https]         BIT            NULL,
    [ServerAddress] NVARCHAR (100) NULL,
    [RemoteAddress] NVARCHAR (100) NULL,
    [Logger]        NVARCHAR (250) NULL,
    [Callsite]      NVARCHAR (MAX) NULL,
    [Exception]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([Id] ASC)
);

