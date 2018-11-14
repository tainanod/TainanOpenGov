CREATE TABLE [dbo].[Card] (
    [CardId]      UNIQUEIDENTIFIER CONSTRAINT [DF_Card_CardId] DEFAULT (newid()) NOT NULL,
    [Title]       NVARCHAR (150)   NOT NULL,
    [Contents]    NVARCHAR (MAX)   NULL,
    [IconId]      UNIQUEIDENTIFIER NOT NULL,
    [Color]       INT              CONSTRAINT [DF_Card_Color] DEFAULT ((1)) NOT NULL,
    [WebUrl]      NVARCHAR (500)   NULL,
    [Sort]        INT              CONSTRAINT [DF_Card_Sort] DEFAULT ((0)) NOT NULL,
    [Type]        INT              CONSTRAINT [DF_Card_Type] DEFAULT ((0)) NOT NULL,
    [IsEnabled]   BIT              CONSTRAINT [DF_Card_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedBy]   NVARCHAR (128)   NULL,
    [CreatedDate] DATETIME         CONSTRAINT [DF_Card_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]   NVARCHAR (128)   NULL,
    [UpdatedDate] DATETIME         CONSTRAINT [DF_Card_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    [IsDelete]    BIT              CONSTRAINT [DF__Card__IsDelete__2D32A501] DEFAULT ((1)) NOT NULL,
    [IsPublish]   BIT              CONSTRAINT [DF_Card_IsPublish] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_dbo.Card] PRIMARY KEY CLUSTERED ([CardId] ASC)
);








GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否可以刪除', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'IsDelete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'相關連結', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'WebUrl';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'卡片類型 0：其它 1：政府資料 2：行程公開 3：警報告示 4：Open1999 5：管線圖資 6：野生台南 7：重大會議 8：公民論壇 9：市政提案', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'單元名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'Title';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否啟用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'單元icon的PhotoId', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'IconId';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'卡片內容HtmlTag', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'Contents';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'卡片顏色', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'Color';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'卡片ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'CardId';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否公開', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Card', @level2type = N'COLUMN', @level2name = N'IsPublish';

