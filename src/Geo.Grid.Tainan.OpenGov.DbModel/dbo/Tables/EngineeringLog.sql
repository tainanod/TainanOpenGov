CREATE TABLE [dbo].[EngineeringLog] (
    [LogId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [LogMessage]  NVARCHAR (MAX) NOT NULL,
    [FileName]    NVARCHAR (100) NOT NULL,
    [CreatedBy]   NVARCHAR (128) NOT NULL,
    [CreatedDate] DATETIME       CONSTRAINT [DF_EngineeringLog_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [IsEnabled]   BIT            CONSTRAINT [DF_EngineeringLog_IsEnabled] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_EngineeringLog] PRIMARY KEY CLUSTERED ([LogId] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否啟用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EngineeringLog', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EngineeringLog', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EngineeringLog', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'檔案名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EngineeringLog', @level2type = N'COLUMN', @level2name = N'FileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'原始資料,CSV檔案格式,不換行(;分隔)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EngineeringLog', @level2type = N'COLUMN', @level2name = N'LogMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工程標案匯入紀錄編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EngineeringLog', @level2type = N'COLUMN', @level2name = N'LogId';

