CREATE TABLE [dbo].[Banner] (
    [BannerId]     UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (255)   NOT NULL,
    [WebUrl]       NVARCHAR (400)   NOT NULL,
    [Target]       BIT              NOT NULL,
    [IsTopEnabled] BIT              NOT NULL,
    [IsDeleted]    BIT              NOT NULL,
    [PhotoId]      UNIQUEIDENTIFIER NOT NULL,
    [IsEnabled]    BIT              NOT NULL,
    [CreatedBy]    NVARCHAR (128)   NOT NULL,
    [CreatedDate]  DATETIME         NOT NULL,
    [UpdatedBy]    NVARCHAR (128)   NOT NULL,
    [UpdatedDate]  DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.Banner] PRIMARY KEY CLUSTERED ([BannerId] ASC)
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'連結網址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'WebUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'連結方式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'Target';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'圖片管理', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'PhotoId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'置頂', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'IsTopEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否顯示-0不顯；1:顯', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否刪除-0:不刪；1:刪', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'IsDeleted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banner', @level2type = N'COLUMN', @level2name = N'CreatedBy';

