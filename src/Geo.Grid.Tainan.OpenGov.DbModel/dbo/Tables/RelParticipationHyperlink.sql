CREATE TABLE [dbo].[RelParticipationHyperlink] (
    [ParticipationId] UNIQUEIDENTIFIER NOT NULL,
    [HyperlinkId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RelParticipationHyperlink] PRIMARY KEY CLUSTERED ([ParticipationId] ASC, [HyperlinkId] ASC),
    CONSTRAINT [FK_RelParticipationHyperlink_Participation] FOREIGN KEY ([ParticipationId]) REFERENCES [dbo].[Participation] ([ParticipationId]),
    CONSTRAINT [FK_RelParticipationHyperlink_ParticipationHyperlink] FOREIGN KEY ([HyperlinkId]) REFERENCES [dbo].[ParticipationHyperlink] ([HyperlinkId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'連結網址編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'HyperlinkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'ParticipationId';

