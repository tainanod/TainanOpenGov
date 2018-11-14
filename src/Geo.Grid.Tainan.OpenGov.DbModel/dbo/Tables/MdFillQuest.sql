CREATE TABLE [dbo].[MdFillQuest] (
    [FillId]          UNIQUEIDENTIFIER NOT NULL,
    [InfoId]          UNIQUEIDENTIFIER NOT NULL,
    [MdFillUserId]    UNIQUEIDENTIFIER NOT NULL,
    [QstId]           UNIQUEIDENTIFIER NOT NULL,
    [SetId]           UNIQUEIDENTIFIER NOT NULL,
    [FillDesc]        NVARCHAR (3000)  NULL,
    [FillScore]       INT              NOT NULL,
    [EditDate]        DATETIME         NOT NULL,
    [Editer]          UNIQUEIDENTIFIER NULL,
    [IsEnable]        BIT              DEFAULT ((0)) NOT NULL,
    [MdFillUserEmail] NVARCHAR (128)   DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_dbo.MdFillQuest] PRIMARY KEY CLUSTERED ([FillId] ASC),
    CONSTRAINT [FK_dbo.MdFillQuest_dbo.MdQuestion_QstId] FOREIGN KEY ([QstId]) REFERENCES [dbo].[MdQuestion] ([QstId])
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
CREATE NONCLUSTERED INDEX [IX_QstId]
    ON [dbo].[MdFillQuest]([QstId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'填寫人eMail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdFillQuest', @level2type = N'COLUMN', @level2name = N'MdFillUserEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdFillQuest', @level2type = N'COLUMN', @level2name = N'IsEnable';

