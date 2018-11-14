CREATE TABLE [dbo].[RelParticipationPhoto] (
    [PHOTO_ID]        UNIQUEIDENTIFIER NOT NULL,
    [ParticipationId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RelParticipationPhoto] PRIMARY KEY CLUSTERED ([PHOTO_ID] ASC, [ParticipationId] ASC),
    CONSTRAINT [FK_RelParticipationPhoto_Participation] FOREIGN KEY ([ParticipationId]) REFERENCES [dbo].[Participation] ([ParticipationId]),
    CONSTRAINT [FK_RelParticipationPhoto_PHOTO] FOREIGN KEY ([PHOTO_ID]) REFERENCES [dbo].[PHOTO] ([PHOTO_ID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationPhoto', @level2type = N'COLUMN', @level2name = N'ParticipationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'圖片編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RelParticipationPhoto', @level2type = N'COLUMN', @level2name = N'PHOTO_ID';

