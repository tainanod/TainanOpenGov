CREATE TABLE [dbo].[ShowCaseRelPhoto] (
    [CaseId]  UNIQUEIDENTIFIER NOT NULL,
    [PhotoId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.ShowCaseRelPhoto] PRIMARY KEY CLUSTERED ([CaseId] ASC, [PhotoId] ASC),
    CONSTRAINT [FK_dbo.ShowCaseRelPhoto_dbo.PHOTO_PhotoId] FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[PHOTO] ([PHOTO_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.ShowCaseRelPhoto_dbo.ShowCase_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[ShowCase] ([CaseId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_PhotoId]
    ON [dbo].[ShowCaseRelPhoto]([PhotoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CaseId]
    ON [dbo].[ShowCaseRelPhoto]([CaseId] ASC);

