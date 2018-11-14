CREATE TABLE [dbo].[MdCheck] (
    [InfoId]      UNIQUEIDENTIFIER NOT NULL,
    [UserEmail]   NVARCHAR (128)   NOT NULL,
    [UserId]      NVARCHAR (128)   CONSTRAINT [DF_MdCheck_UserId] DEFAULT ('') NOT NULL,
    [IsEnabled]   BIT              CONSTRAINT [DF_MdCheck_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedDate] DATETIME         CONSTRAINT [DF_MdCheck_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_dbo.MdCheck] PRIMARY KEY CLUSTERED ([InfoId] ASC, [UserEmail] ASC)
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'匿名者編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdCheck', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'userMail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdCheck', @level2type = N'COLUMN', @level2name = N'UserEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否有效', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdCheck', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdCheck', @level2type = N'COLUMN', @level2name = N'CreatedDate';

