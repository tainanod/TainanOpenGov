CREATE TABLE [dbo].[AllowanceSource] (
    [SourceId]     UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (500)   NOT NULL,
    [Organization] NVARCHAR (500)   NOT NULL,
    [WebSite]      NVARCHAR (500)   NOT NULL,
    [ApiUrl]       NVARCHAR (500)   NOT NULL,
    [CreatedBy]    NVARCHAR (128)   NOT NULL,
    [CreatedDate]  DATETIME         CONSTRAINT [DF_AllowanceSource_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]    NVARCHAR (128)   NOT NULL,
    [UpdatedDate]  DATETIME         CONSTRAINT [DF_AllowanceSource_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    [IsEnabled]    BIT              CONSTRAINT [DF_AllowanceSource_IsEnabled] DEFAULT ((1)) NOT NULL,
    [ResourceId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_AllowanceSource] PRIMARY KEY CLUSTERED ([SourceId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'資料來源的唯一編碼，由ApiUrl取得', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'ResourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否啟用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最後更新日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最後更新人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'API', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'ApiUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'網頁連結', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'WebSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'管理組織', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'Organization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'資料集名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公益臺南-資料集來源管理', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AllowanceSource', @level2type = N'COLUMN', @level2name = N'SourceId';

