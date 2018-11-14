CREATE TABLE [dbo].[WaitingMail] (
    [MailSeq]     INT            IDENTITY (1, 1) NOT NULL,
    [TypeName]    NVARCHAR (400) CONSTRAINT [DF_WaitingMail_TypeName] DEFAULT ('') NOT NULL,
    [ToName]      NVARCHAR (50)  CONSTRAINT [DF_WaitingMail_ToName] DEFAULT ('') NOT NULL,
    [ToEmail]     NVARCHAR (400) CONSTRAINT [DF_WaitingMail_ToEmail] DEFAULT ('') NOT NULL,
    [FromName]    NVARCHAR (50)  CONSTRAINT [DF_WaitingMail_FromName] DEFAULT ('') NOT NULL,
    [FromEmail]   NVARCHAR (400) CONSTRAINT [DF_WaitingMail_FromEmail] DEFAULT ('') NOT NULL,
    [BccEmail]    NVARCHAR (800) CONSTRAINT [DF_WaitingMail_BccEmail] DEFAULT ('') NOT NULL,
    [Subject]     NVARCHAR (256) CONSTRAINT [DF_WaitingMail_Subject] DEFAULT ('') NOT NULL,
    [MailContent] NVARCHAR (MAX) CONSTRAINT [DF_WaitingMail_MailContent] DEFAULT ('') NOT NULL,
    [IsSend]      BIT            CONSTRAINT [DF_WaitingMail_IsSend] DEFAULT ((0)) NOT NULL,
    [CreateTime]  DATETIME       CONSTRAINT [DF_WaitingMail_CreateTime] DEFAULT (getdate()) NOT NULL,
    [SendTime]    DATETIME       NULL,
    CONSTRAINT [PK_dbo.WaitingMail] PRIMARY KEY CLUSTERED ([MailSeq] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'寄發系統', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'TypeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收件人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'ToName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收件人Email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'ToEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'寄件人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'FromName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'寄件人Email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'FromEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'密件Email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'BccEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'郵件主旨', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'Subject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'郵件內容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'MailContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否已發送', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'IsSend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'新增日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'實際發送日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WaitingMail', @level2type = N'COLUMN', @level2name = N'SendTime';

