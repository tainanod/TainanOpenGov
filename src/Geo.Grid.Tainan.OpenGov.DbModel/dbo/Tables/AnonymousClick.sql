CREATE TABLE [dbo].[AnonymousClick] (
    [AnonymousId] NVARCHAR (128)   NOT NULL,
    [ClickId]     NVARCHAR (128)   NOT NULL,
    [ClickType]   NVARCHAR (128)   NOT NULL,
    [ForumId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.AnonymousClick] PRIMARY KEY CLUSTERED ([AnonymousId] ASC, [ClickId] ASC, [ClickType] ASC),
    CONSTRAINT [FK_dbo.AnonymousClick_dbo.FORUM_ForumId] FOREIGN KEY ([ForumId]) REFERENCES [dbo].[FORUM] ([FORUM_ID])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_ForumId]
    ON [dbo].[AnonymousClick]([ForumId] ASC);

