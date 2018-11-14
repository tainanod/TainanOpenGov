CREATE TABLE [dbo].[ShowCaseUrl] (
    [UrlId]       UNIQUEIDENTIFIER NOT NULL,
    [CaseId]      UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (400)   NOT NULL,
    [WebUrl]      NVARCHAR (800)   NOT NULL,
    [Sort]        INT              NOT NULL,
    [IsEnabled]   BIT              NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [CreatedBy]   NVARCHAR (128)   NOT NULL,
    [UpdatedDate] DATETIME         NOT NULL,
    [UpdatedBy]   NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_dbo.ShowCaseUrl] PRIMARY KEY CLUSTERED ([UrlId] ASC),
    CONSTRAINT [FK_dbo.ShowCaseUrl_dbo.ShowCase_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[ShowCase] ([CaseId]) ON DELETE CASCADE
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShowCaseUrl', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'網址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShowCaseUrl', @level2type = N'COLUMN', @level2name = N'WebUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShowCaseUrl', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShowCaseUrl', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShowCaseUrl', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShowCaseUrl', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShowCaseUrl', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShowCaseUrl', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
CREATE NONCLUSTERED INDEX [IX_CaseId]
    ON [dbo].[ShowCaseUrl]([CaseId] ASC);

