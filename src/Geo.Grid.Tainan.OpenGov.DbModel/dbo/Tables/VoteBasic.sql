CREATE TABLE [dbo].[VoteBasic] (
    [BasicId]     UNIQUEIDENTIFIER CONSTRAINT [DF_VoteBasic_BasicId] DEFAULT (newid()) NOT NULL,
    [GroupId]     INT              CONSTRAINT [DF_VoteBasic_GroupId] DEFAULT ((0)) NOT NULL,
    [Name]        NVARCHAR (50)    CONSTRAINT [DF_VoteBasic_Name] DEFAULT ('') NOT NULL,
    [Sort]        INT              CONSTRAINT [DF_VoteBasic_Sort] DEFAULT ((0)) NOT NULL,
    [IsEnabled]   BIT              CONSTRAINT [DF_VoteBasic_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedDate] DATETIME         CONSTRAINT [DF_VoteBasic_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [IsExplain]   BIT              CONSTRAINT [DF_VoteBasic_IsExplain] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.VoteBasic] PRIMARY KEY CLUSTERED ([BasicId] ASC),
    CONSTRAINT [FK_dbo.VoteBasic_dbo.VoteBasicGroup_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[VoteBasicGroup] ([GroupId]) ON DELETE CASCADE
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'個資分類編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasic', @level2type = N'COLUMN', @level2name = N'GroupId';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasic', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasic', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasic', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasic', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否需填寫內容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasic', @level2type = N'COLUMN', @level2name = N'IsExplain';




GO
CREATE NONCLUSTERED INDEX [IX_GroupId]
    ON [dbo].[VoteBasic]([GroupId] ASC);

