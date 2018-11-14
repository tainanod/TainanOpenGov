CREATE TABLE [dbo].[ShowCaseRelDataSet] (
    [CaseId] UNIQUEIDENTIFIER NOT NULL,
    [SetId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.ShowCaseRelDataSet] PRIMARY KEY CLUSTERED ([CaseId] ASC, [SetId] ASC),
    CONSTRAINT [FK_dbo.ShowCaseRelDataSet_dbo.DataSet_SetId] FOREIGN KEY ([SetId]) REFERENCES [dbo].[DataSet] ([SetId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.ShowCaseRelDataSet_dbo.ShowCase_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[ShowCase] ([CaseId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_SetId]
    ON [dbo].[ShowCaseRelDataSet]([SetId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CaseId]
    ON [dbo].[ShowCaseRelDataSet]([CaseId] ASC);

