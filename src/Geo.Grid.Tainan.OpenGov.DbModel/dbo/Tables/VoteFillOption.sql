CREATE TABLE [dbo].[VoteFillOption] (
    [UserId]      NVARCHAR (128)   NOT NULL,
    [OptionId]    UNIQUEIDENTIFIER NOT NULL,
    [VoteId]      UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.VoteFillOption] PRIMARY KEY CLUSTERED ([UserId] ASC, [OptionId] ASC)
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillOption', @level2type = N'COLUMN', @level2name = N'UserId';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'選項編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillOption', @level2type = N'COLUMN', @level2name = N'OptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillOption', @level2type = N'COLUMN', @level2name = N'VoteId';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteFillOption', @level2type = N'COLUMN', @level2name = N'CreatedDate';



