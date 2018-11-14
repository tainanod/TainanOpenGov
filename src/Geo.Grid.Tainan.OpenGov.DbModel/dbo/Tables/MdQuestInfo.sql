CREATE TABLE [dbo].[MdQuestInfo] (
    [InfoId]      UNIQUEIDENTIFIER NOT NULL,
    [InfoTitle]   NVARCHAR (500)   NULL,
    [InfoDesc]    NVARCHAR (3000)  NULL,
    [InfoDateSt]  DATETIME         NULL,
    [InfoDateEnd] DATETIME         NULL,
    [InfoValid]   BIT              NOT NULL,
    [InfoFooter]  NVARCHAR (500)   NULL,
    [EditDate]    DATETIME         NOT NULL,
    [Editer]      UNIQUEIDENTIFIER NULL,
    [IsEnable]    BIT              NOT NULL,
    [ForumId]     UNIQUEIDENTIFIER NOT NULL,
    [VerifyType]  INT              DEFAULT ((0)) NOT NULL,
    [IsGather]    BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.MdQuestInfo] PRIMARY KEY CLUSTERED ([InfoId] ASC),
    CONSTRAINT [FK_dbo.MdQuestInfo_dbo.FORUM_ForumId] FOREIGN KEY ([ForumId]) REFERENCES [dbo].[FORUM] ([FORUM_ID])
);






GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'問卷標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'InfoTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'問卷描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'InfoDesc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'開放填寫日期(起)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'InfoDateSt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'開放填寫日期(迄)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'InfoDateEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否開放 1:使用  0:停用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'InfoValid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'問卷結語', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'InfoFooter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'EditDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'Editer';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0刪除 1存在', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'IsEnable';


GO
CREATE NONCLUSTERED INDEX [IX_ForumId]
    ON [dbo].[MdQuestInfo]([ForumId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1:無登入、無驗證；2:無登入，需email驗證；3:需登入，無email驗證', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'VerifyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'問卷結果是否開放(統計表)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'IsGather';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所屬的開放論壇ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestInfo', @level2type = N'COLUMN', @level2name = N'ForumId';

