CREATE TABLE [dbo].[Participation] (
    [ParticipationId] UNIQUEIDENTIFIER CONSTRAINT [DF_Participation_FORUM_ID] DEFAULT (newid()) NOT NULL,
    [Category]        NVARCHAR (100)   NOT NULL,
    [DataSetUnitId]   UNIQUEIDENTIFIER NOT NULL,
    [Subject]         NVARCHAR (300)   NOT NULL,
    [Description]     NVARCHAR (4000)  NOT NULL,
    [Announcement]    NVARCHAR (4000)  NULL,
    [EnableDiscuss]   BIT              CONSTRAINT [DF_Participation_IsEnableDiscuss] DEFAULT ((0)) NOT NULL,
    [OpenDate]        DATETIME         NOT NULL,
    [CloseDate]       DATETIME         NOT NULL,
    [IsMothball]      BIT              CONSTRAINT [DF_Participation_IsMothball] DEFAULT ((0)) NOT NULL,
    [IsEnabled]       BIT              CONSTRAINT [DF_Participation_IsEnabled1] DEFAULT ((1)) NOT NULL,
    [CreatedBy]       NVARCHAR (128)   CONSTRAINT [DF_Participation_CreatedBy] DEFAULT ('') NOT NULL,
    [CreatedDate]     DATETIME         CONSTRAINT [DF_Participation_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]       NVARCHAR (128)   CONSTRAINT [DF_Participation_UpdatedBy] DEFAULT ('') NOT NULL,
    [UpdatedDate]     DATETIME         CONSTRAINT [DF_Participation_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Participation] PRIMARY KEY CLUSTERED ([ParticipationId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否封存', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'IsMothball';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'截止時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'CloseDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'發佈時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'OpenDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否開放討論區', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'EnableDiscuss';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'討論區公告', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'Announcement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'Subject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主辦單位編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'DataSetUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主題類別', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'Category';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Participation', @level2type = N'COLUMN', @level2name = N'ParticipationId';

