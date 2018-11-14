CREATE TABLE [dbo].[ParticipationHyperlink] (
    [HyperlinkId] UNIQUEIDENTIFIER CONSTRAINT [DF_ParticipationHyperlink_HYPERLINK_ID] DEFAULT (newid()) NOT NULL,
    [Title]       NVARCHAR (200)   NOT NULL,
    [Description] NVARCHAR (500)   NULL,
    [Url]         NVARCHAR (200)   NOT NULL,
    [IsEnabled]   BIT              CONSTRAINT [DF_ParticipationHyperlink_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedBy]   NVARCHAR (128)   CONSTRAINT [DF_ParticipationHyperlink_CreatedBy] DEFAULT ('') NOT NULL,
    [CreatedDate] DATETIME         CONSTRAINT [DF_ParticipationHyperlink_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]   NVARCHAR (128)   CONSTRAINT [DF_ParticipationHyperlink_UpdatedBy] DEFAULT ('') NOT NULL,
    [UpdatedDate] DATETIME         CONSTRAINT [DF_ParticipationHyperlink_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ParticipationHyperlink] PRIMARY KEY CLUSTERED ([HyperlinkId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'連結網址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'Url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'連結說明', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'連結名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'連結網址編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParticipationHyperlink', @level2type = N'COLUMN', @level2name = N'HyperlinkId';

