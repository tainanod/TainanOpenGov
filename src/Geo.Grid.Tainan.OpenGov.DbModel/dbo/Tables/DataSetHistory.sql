CREATE TABLE [dbo].[DataSetHistory] (
    [HistoryId]   UNIQUEIDENTIFIER NOT NULL,
    [SetId]       UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (255)   NOT NULL,
    [Contents]    NVARCHAR (MAX)   NOT NULL,
    [Info]        NVARCHAR (MAX)   NOT NULL,
    [Sort]        INT              NOT NULL,
    [IsEnabled]   BIT              NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [CreatedBy]   NVARCHAR (128)   NOT NULL,
    [UpdatedDate] DATETIME         NOT NULL,
    [UpdatedBy]   NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_dbo.DataSetHistory] PRIMARY KEY CLUSTERED ([HistoryId] ASC),
    CONSTRAINT [FK_dbo.DataSetHistory_dbo.DataSet_SetId] FOREIGN KEY ([SetId]) REFERENCES [dbo].[DataSet] ([SetId]) ON DELETE CASCADE
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題(版號)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用指南-md', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'Contents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'簡介', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'Info';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetHistory', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
CREATE NONCLUSTERED INDEX [IX_SetId]
    ON [dbo].[DataSetHistory]([SetId] ASC);

