CREATE TABLE [dbo].[ParticipationDocument] (
    [DocumentId]   UNIQUEIDENTIFIER CONSTRAINT [DF_ParticipationDocument_DOCUMENT_ID] DEFAULT (newid()) NOT NULL,
    [Size]         BIGINT           NOT NULL,
    [FileName]     NVARCHAR (255)   NOT NULL,
    [FileType]     NVARCHAR (255)   NOT NULL,
    [Alt]          NVARCHAR (255)   NULL,
    [DocumentType] INT              CONSTRAINT [DF_ParticipationDocument_DOCUMENT_TYPE] DEFAULT ((0)) NOT NULL,
    [IsEnabled]    BIT              CONSTRAINT [DF_ParticipationDocument_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedBy]    NVARCHAR (128)   CONSTRAINT [DF_ParticipationDocument_CreatedBy] DEFAULT ('') NOT NULL,
    [CreatedDate]  DATETIME         CONSTRAINT [DF_ParticipationDocument_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]    NVARCHAR (128)   CONSTRAINT [DF_ParticipationDocument_UpdatedBy] DEFAULT ('') NOT NULL,
    [UpdatedDate]  DATETIME         CONSTRAINT [DF_ParticipationDocument_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ParticipationDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件類別, 0:一般文件, 1:市府回應', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件內容說明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'Alt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件類型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'FileType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件檔名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'FileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件檔案大小', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationDocument', @level2type = N'COLUMN', @level2name = N'DocumentId';

