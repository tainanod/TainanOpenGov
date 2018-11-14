CREATE TABLE [dbo].[RelParticipationDiscuss] (
    [ParticipationId] UNIQUEIDENTIFIER NOT NULL,
    [DiscussId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RelParticipationDiscuss] PRIMARY KEY CLUSTERED ([ParticipationId] ASC, [DiscussId] ASC),
    CONSTRAINT [FK_RelParticipationDiscuss_Participation] FOREIGN KEY ([ParticipationId]) REFERENCES [dbo].[Participation] ([ParticipationId]),
    CONSTRAINT [FK_RelParticipationDiscuss_ParticipationDiscuss] FOREIGN KEY ([DiscussId]) REFERENCES [dbo].[ParticipationDiscuss] ([DiscussId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'討論區編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'DiscussId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationDiscuss', @level2type = N'COLUMN', @level2name = N'ParticipationId';

