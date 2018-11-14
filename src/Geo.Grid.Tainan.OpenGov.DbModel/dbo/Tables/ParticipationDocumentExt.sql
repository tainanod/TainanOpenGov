CREATE TABLE [dbo].[ParticipationDocumentExt] (
    [DocumentId] UNIQUEIDENTIFIER NOT NULL,
    [Original]   VARBINARY (MAX)  NOT NULL,
    CONSTRAINT [PK_ParticipationDocumentExt] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_ParticipationDocumentExt_ParticipationDocument] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[ParticipationDocument] ([DocumentId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'檔案內容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocumentExt', @level2type = N'COLUMN', @level2name = N'Original';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocumentExt', @level2type = N'COLUMN', @level2name = N'DocumentId';

