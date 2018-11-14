CREATE TABLE [dbo].[RelParticipationDiscussTag] (
    [DiscussId] UNIQUEIDENTIFIER CONSTRAINT [DF_RelParticipationDiscussTag_DiscussId] DEFAULT (newid()) NOT NULL,
    [TagId]     UNIQUEIDENTIFIER CONSTRAINT [DF_RelParticipationDiscussTag_TagId] DEFAULT (newid()) NOT NULL,
    CONSTRAINT [FK_RelParticipationDiscussTag_ParticipationDiscuss] FOREIGN KEY ([DiscussId]) REFERENCES [dbo].[ParticipationDiscuss] ([DiscussId]),
    CONSTRAINT [FK_RelParticipationDiscussTag_ParticipationTag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[ParticipationTag] ([TagId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標籤編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationDiscussTag', @level2type = N'COLUMN', @level2name = N'TagId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'討論區編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationDiscussTag', @level2type = N'COLUMN', @level2name = N'DiscussId';

