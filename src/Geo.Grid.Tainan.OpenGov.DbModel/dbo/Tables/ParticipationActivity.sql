CREATE TABLE [dbo].[ParticipationActivity] (
    [ActivityId]      UNIQUEIDENTIFIER CONSTRAINT [DF_ParticipationActivity_ACTIVITY_ID] DEFAULT (newid()) NOT NULL,
    [ParticipationId] UNIQUEIDENTIFIER NOT NULL,
    [Subject]         NVARCHAR (300)   NOT NULL,
    [Description]     NVARCHAR (2000)  NOT NULL,
    [HoldDate]        NVARCHAR (MAX)   NOT NULL,
    [Location]        NVARCHAR (200)   NOT NULL,
    [ActivityType]    INT              CONSTRAINT [DF_ParticipationActivity_ACTIVITY_TYPE] DEFAULT ((0)) NOT NULL,
    [IsEnabled]       BIT              CONSTRAINT [DF_ParticipationActivity_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedBy]       NVARCHAR (128)   CONSTRAINT [DF_ParticipationActivity_CreatedBy] DEFAULT ('') NOT NULL,
    [CreatedDate]     DATETIME         CONSTRAINT [DF_ParticipationActivity_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]       NVARCHAR (128)   CONSTRAINT [DF_ParticipationActivity_UpdatedBy] DEFAULT ('') NOT NULL,
    [UpdatedDate]     DATETIME         CONSTRAINT [DF_ParticipationActivity_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ParticipationActivity] PRIMARY KEY CLUSTERED ([ActivityId] ASC),
    CONSTRAINT [FK_ParticipationActivity_Participation] FOREIGN KEY ([ParticipationId]) REFERENCES [dbo].[Participation] ([ParticipationId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0: 論壇活動, 1: 工作坊', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'ActivityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地點', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'Location';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日期與時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'HoldDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'Subject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'ParticipationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活動編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationActivity', @level2type = N'COLUMN', @level2name = N'ActivityId';

