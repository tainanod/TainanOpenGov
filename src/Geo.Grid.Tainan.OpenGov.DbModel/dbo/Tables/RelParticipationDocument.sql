CREATE TABLE [dbo].[RelParticipationDocument] (
    [DocumentId]      UNIQUEIDENTIFIER NOT NULL,
    [ParticipationId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RelParticipationDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC, [ParticipationId] ASC),
    CONSTRAINT [FK_RelParticipationDocument_Participation] FOREIGN KEY ([ParticipationId]) REFERENCES [dbo].[Participation] ([ParticipationId]),
    CONSTRAINT [FK_RelParticipationDocument_ParticipationDocument] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[ParticipationDocument] ([DocumentId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationDocument', @level2type = N'COLUMN', @level2name = N'ParticipationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationDocument', @level2type = N'COLUMN', @level2name = N'DocumentId';

