CREATE TABLE [dbo].[VoteOption] (
    [OptionId]    UNIQUEIDENTIFIER NOT NULL,
    [VoteId]      UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (255)   NOT NULL,
    [Sort]        INT              NOT NULL,
    [IsEnabled]   BIT              NOT NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [CreatedBy]   NVARCHAR (128)   NOT NULL,
    [UpdatedDate] DATETIME         NOT NULL,
    [UpdatedBy]   NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_dbo.VoteOption] PRIMARY KEY CLUSTERED ([OptionId] ASC),
    CONSTRAINT [FK_dbo.VoteOption_dbo.Vote_VoteId] FOREIGN KEY ([VoteId]) REFERENCES [dbo].[Vote] ([VoteId]) ON DELETE CASCADE
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteOption', @level2type = N'COLUMN', @level2name = N'VoteId';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteOption', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteOption', @level2type = N'COLUMN', @level2name = N'Sort';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteOption', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteOption', @level2type = N'COLUMN', @level2name = N'CreatedDate';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteOption', @level2type = N'COLUMN', @level2name = N'CreatedBy';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteOption', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteOption', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
CREATE NONCLUSTERED INDEX [IX_VoteId]
    ON [dbo].[VoteOption]([VoteId] ASC);

