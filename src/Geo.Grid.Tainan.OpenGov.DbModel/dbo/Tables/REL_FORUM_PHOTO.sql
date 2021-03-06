CREATE TABLE [dbo].[REL_FORUM_PHOTO] (
    [FORUM_ID] UNIQUEIDENTIFIER NOT NULL,
    [PHOTO_ID] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.REL_FORUM_PHOTO] PRIMARY KEY CLUSTERED ([FORUM_ID] ASC, [PHOTO_ID] ASC),
    CONSTRAINT [FK_dbo.REL_FORUM_PHOTO_dbo.FORUM_FORUM_ID] FOREIGN KEY ([FORUM_ID]) REFERENCES [dbo].[FORUM] ([FORUM_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.REL_FORUM_PHOTO_dbo.PHOTO_PHOTO_ID] FOREIGN KEY ([PHOTO_ID]) REFERENCES [dbo].[PHOTO] ([PHOTO_ID]) ON DELETE CASCADE
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_PHOTO_ID]
    ON [dbo].[REL_FORUM_PHOTO]([PHOTO_ID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FORUM_ID]
    ON [dbo].[REL_FORUM_PHOTO]([FORUM_ID] ASC);

