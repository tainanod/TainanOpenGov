﻿CREATE TABLE [dbo].[DataSetType] (
    [TypeId]      UNIQUEIDENTIFIER CONSTRAINT [DF_DataSetType_TypeId] DEFAULT (newid()) NOT NULL,
    [Title]       NVARCHAR (255)   NOT NULL,
    [Sort]        INT              NOT NULL,
    [IsEnabled]   BIT              NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [CreatedBy]   NVARCHAR (128)   NOT NULL,
    [UpdatedDate] DATETIME         NOT NULL,
    [UpdatedBy]   NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_dbo.DataSetType] PRIMARY KEY CLUSTERED ([TypeId] ASC)
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetType', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetType', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetType', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetType', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetType', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetType', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DataSetType', @level2type = N'COLUMN', @level2name = N'UpdatedBy';

