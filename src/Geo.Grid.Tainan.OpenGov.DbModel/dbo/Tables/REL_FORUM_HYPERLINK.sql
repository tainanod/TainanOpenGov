CREATE TABLE [dbo].[REL_FORUM_HYPERLINK] (
    [FORUM_ID]     UNIQUEIDENTIFIER NOT NULL,
    [HYPERLINK_ID] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.REL_FORUM_HYPERLINK] PRIMARY KEY CLUSTERED ([FORUM_ID] ASC, [HYPERLINK_ID] ASC),
    CONSTRAINT [FK_dbo.REL_FORUM_HYPERLINK_dbo.FORUM_FORUM_ID] FOREIGN KEY ([FORUM_ID]) REFERENCES [dbo].[FORUM] ([FORUM_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.REL_FORUM_HYPERLINK_dbo.HYPERLINK_HYPERLINK_ID] FOREIGN KEY ([HYPERLINK_ID]) REFERENCES [dbo].[HYPERLINK] ([HYPERLINK_ID]) ON DELETE CASCADE
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_HYPERLINK_ID]
    ON [dbo].[REL_FORUM_HYPERLINK]([HYPERLINK_ID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FORUM_ID]
    ON [dbo].[REL_FORUM_HYPERLINK]([FORUM_ID] ASC);

