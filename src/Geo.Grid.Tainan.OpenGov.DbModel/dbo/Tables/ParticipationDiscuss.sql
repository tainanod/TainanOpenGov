CREATE TABLE [dbo].[ParticipationDiscuss] (
    [DiscussId]       UNIQUEIDENTIFIER CONSTRAINT [DF_ParticipationDiscuss_DISCUSS_ID] DEFAULT (newid()) NOT NULL,
    [ParticipationId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]          NVARCHAR (128)   NOT NULL,
    [UpperId]         UNIQUEIDENTIFIER NULL,
    [Message]         NVARCHAR (4000)  NOT NULL,
    [IsTop]           BIT              CONSTRAINT [DF_ParticipationDiscuss_IS_TOP] DEFAULT ((0)) NOT NULL,
    [IsEnabled]       BIT              CONSTRAINT [DF_ParticipationDiscuss_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedBy]       NVARCHAR (128)   CONSTRAINT [DF_ParticipationDiscuss_CreatedBy] DEFAULT ('') NOT NULL,
    [CreatedDate]     DATETIME         CONSTRAINT [DF_ParticipationDiscuss_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]       NVARCHAR (128)   CONSTRAINT [DF_ParticipationDiscuss_UpdatedBy] DEFAULT ('') NOT NULL,
    [UpdatedDate]     DATETIME         CONSTRAINT [DF_ParticipationDiscuss_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ParticipationDiscuss] PRIMARY KEY CLUSTERED ([DiscussId] ASC),
    CONSTRAINT [FK_ParticipationDiscuss_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'置頂', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'IsTop';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'討論內容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上層討論區編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'UpperId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用者編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'ParticipationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'討論區編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'DiscussId';

