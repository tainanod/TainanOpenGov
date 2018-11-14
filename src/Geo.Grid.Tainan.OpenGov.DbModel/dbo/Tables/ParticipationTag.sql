CREATE TABLE [dbo].[ParticipationTag] (
    [TagId]           UNIQUEIDENTIFIER CONSTRAINT [DF_ParticipationTag_TagId] DEFAULT (newid()) NOT NULL,
    [TagName]         NVARCHAR (50)    NOT NULL,
    [Sort]            INT              CONSTRAINT [DF_ParticipationTag_SORT] DEFAULT ((0)) NOT NULL,
    [ParticipationId] UNIQUEIDENTIFIER NOT NULL,
    [IsEnabled]       BIT              CONSTRAINT [DF_ParticipationTag_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedBy]       NVARCHAR (128)   CONSTRAINT [DF_ParticipationTag_CreatedBy] DEFAULT ('') NOT NULL,
    [CreatedDate]     DATETIME         CONSTRAINT [DF_ParticipationTag_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]       NVARCHAR (128)   CONSTRAINT [DF_ParticipationTag_UpdatedBy] DEFAULT ('') NOT NULL,
    [UpdatedDate]     DATETIME         CONSTRAINT [DF_ParticipationTag_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ParticipationTag] PRIMARY KEY CLUSTERED ([TagId] ASC),
    CONSTRAINT [FK_ParticipationTag_Participation] FOREIGN KEY ([ParticipationId]) REFERENCES [dbo].[Participation] ([ParticipationId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'ParticipationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'Sort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標籤名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'TagName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標籤編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationTag', @level2type = N'COLUMN', @level2name = N'TagId';

