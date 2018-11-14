CREATE TABLE [dbo].[VoteFillBasic] (
    [UserId]      NVARCHAR (128)   NOT NULL,
    [BasicId]     UNIQUEIDENTIFIER NOT NULL,
    [VoteId]      UNIQUEIDENTIFIER NOT NULL,
    [BasicDesc]   NVARCHAR (500)   NOT NULL,
    [GroupId]     INT              NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.VoteFillBasic] PRIMARY KEY CLUSTERED ([UserId] ASC, [BasicId] ASC, [VoteId] ASC)
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillBasic', @level2type = N'COLUMN', @level2name = N'VoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillBasic', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'個資分類編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillBasic', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillBasic', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'個資編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillBasic', @level2type = N'COLUMN', @level2name = N'BasicId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'填寫內容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillBasic', @level2type = N'COLUMN', @level2name = N'BasicDesc';

