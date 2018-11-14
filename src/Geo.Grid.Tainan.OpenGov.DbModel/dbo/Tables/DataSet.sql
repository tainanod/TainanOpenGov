CREATE TABLE [dbo].[DataSet] (
    [SetId]        UNIQUEIDENTIFIER NOT NULL,
    [TypeId]       UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (255)   NOT NULL,
    [ContactName]  NVARCHAR (255)   NOT NULL,
    [ContactTel]   NVARCHAR (255)   NOT NULL,
    [WebUrl]       NVARCHAR (400)   NOT NULL,
    [Contents]     NVARCHAR (MAX)   NOT NULL,
    [Info]         NVARCHAR (MAX)   NOT NULL,
    [Sort]         INT              NOT NULL,
    [IsEnabled]    BIT              NOT NULL,
    [IsDeleted]    BIT              NOT NULL,
    [CreatedDate]  DATETIME         NOT NULL,
    [CreatedBy]    NVARCHAR (128)   NOT NULL,
    [UpdatedDate]  DATETIME         NOT NULL,
    [UpdatedBy]    NVARCHAR (128)   NOT NULL,
    [UnitId]       UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    [ContactEmail] NVARCHAR (800)   DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_dbo.DataSet] PRIMARY KEY CLUSTERED ([SetId] ASC),
    CONSTRAINT [FK_dbo.DataSet_dbo.DataSetType_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[DataSetType] ([TypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DataSet_dbo.DataSetUnit_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[DataSetUnit] ([UnitId]) ON DELETE CASCADE
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'類別', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'TypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'業管單位編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'UnitId';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'聯繫窗口', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'ContactName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'聯絡電話', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'ContactTel';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'連結位址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'WebUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用指南-md', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'Contents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'簡介', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'Info';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否刪除', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'IsDeleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSet', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
CREATE NONCLUSTERED INDEX [IX_UnitId]
    ON [dbo].[DataSet]([UnitId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TypeId]
    ON [dbo].[DataSet]([TypeId] ASC);

