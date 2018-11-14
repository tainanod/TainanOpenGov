CREATE TABLE [dbo].[ParticipationAnonymousClick] (
    [AnonymousId]     NVARCHAR (128)   NOT NULL,
    [ClickId]         NVARCHAR (128)   NOT NULL,
    [ClickType]       NVARCHAR (128)   NOT NULL,
    [ParticipationId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ParticipationAnonymousClick] PRIMARY KEY CLUSTERED ([AnonymousId] ASC, [ClickId] ASC, [ClickType] ASC),
    CONSTRAINT [FK_ParticipationAnonymousClick_Participation] FOREIGN KEY ([ParticipationId]) REFERENCES [dbo].[Participation] ([ParticipationId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'市政參與編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationAnonymousClick', @level2type = N'COLUMN', @level2name = N'ParticipationId';

