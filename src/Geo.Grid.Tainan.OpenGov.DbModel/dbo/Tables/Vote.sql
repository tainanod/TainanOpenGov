CREATE TABLE [dbo].[Vote] (
    [VoteId]       UNIQUEIDENTIFIER NOT NULL,
    [ForumId]      UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (255)   NOT NULL,
    [Info]         NVARCHAR (MAX)   NOT NULL,
    [StartDate]    DATETIME         NOT NULL,
    [EndDate]      DATETIME         NOT NULL,
    [CanVote]      BIT              NOT NULL,
    [SelectNumber] INT              NOT NULL,
    [IsPublish]    BIT              NOT NULL,
    [IsEnabled]    BIT              NOT NULL,
    [CreatedDate]  DATETIME         NOT NULL,
    [CreatedBy]    NVARCHAR (128)   NOT NULL,
    [UpdatedDate]  DATETIME         NOT NULL,
    [UpdatedBy]    NVARCHAR (128)   NOT NULL,
    [VerifyType]   INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Vote] PRIMARY KEY CLUSTERED ([VoteId] ASC),
    CONSTRAINT [FK_dbo.Vote_dbo.FORUM_ForumId] FOREIGN KEY ([ForumId]) REFERENCES [dbo].[FORUM] ([FORUM_ID]) ON DELETE CASCADE
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'Title';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'說明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'Info';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票開始日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票結束日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否開放投票結果', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'CanVote';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'至少需投幾項', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'SelectNumber';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否開放', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'IsPublish';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否已刪除', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'IsEnabled';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
CREATE NONCLUSTERED INDEX [IX_ForumId]
    ON [dbo].[Vote]([ForumId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'驗證方式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'VerifyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公民論壇編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Vote', @level2type = N'COLUMN', @level2name = N'ForumId';

