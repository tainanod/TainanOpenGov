CREATE TABLE [dbo].[VoteBasicGroup] (
    [GroupId]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) CONSTRAINT [DF_VoteBasicGroup_Name] DEFAULT ('') NOT NULL,
    [Sort]        INT           CONSTRAINT [DF_VoteBasicGroup_Sort] DEFAULT ((0)) NOT NULL,
    [IsEnabled]   BIT           CONSTRAINT [DF_VoteBasicGroup_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedDate] DATETIME      CONSTRAINT [DF_VoteBasicGroup_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_dbo.VoteBasicGroup] PRIMARY KEY CLUSTERED ([GroupId] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasicGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasicGroup', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasicGroup', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'VoteBasicGroup', @level2type = N'COLUMN', @level2name = N'CreatedDate';

