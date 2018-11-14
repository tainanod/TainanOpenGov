CREATE TABLE [dbo].[RelParticipationActivity] (
    [ActivityId] UNIQUEIDENTIFIER NOT NULL,
    [DocumentId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RelParticipationActivity] PRIMARY KEY CLUSTERED ([ActivityId] ASC, [DocumentId] ASC),
    CONSTRAINT [FK_RelParticipationActivity_ParticipationActivity] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[ParticipationActivity] ([ActivityId]),
    CONSTRAINT [FK_RelParticipationActivity_ParticipationDocument] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[ParticipationDocument] ([DocumentId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationActivity', @level2type = N'COLUMN', @level2name = N'DocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活動編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationActivity', @level2type = N'COLUMN', @level2name = N'ActivityId';

