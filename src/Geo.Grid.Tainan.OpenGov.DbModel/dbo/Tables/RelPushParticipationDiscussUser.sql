CREATE TABLE [dbo].[RelPushParticipationDiscussUser] (
    [Id]        NVARCHAR (128)   NOT NULL,
    [DiscussId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RelPushParticipationDiscussUser] PRIMARY KEY CLUSTERED ([Id] ASC, [DiscussId] ASC),
    CONSTRAINT [FK_RelPushParticipationDiscussUser_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_RelPushParticipationDiscussUser_ParticipationDiscuss] FOREIGN KEY ([DiscussId]) REFERENCES [dbo].[ParticipationDiscuss] ([DiscussId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'討論區編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelPushParticipationDiscussUser', @level2type = N'COLUMN', @level2name = N'DiscussId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'推文使用者ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelPushParticipationDiscussUser', @level2type = N'COLUMN', @level2name = N'Id';

