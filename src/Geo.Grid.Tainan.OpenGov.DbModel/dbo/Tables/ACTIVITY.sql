CREATE TABLE [dbo].[ACTIVITY] (
    [ACTIVITY_ID]   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [FORUM_ID]      UNIQUEIDENTIFIER NOT NULL,
    [SUBJECT]       NVARCHAR (300)   NOT NULL,
    [DESCRIPTION]   NVARCHAR (2000)  NOT NULL,
    [HOLD_DATE]     NVARCHAR (MAX)   NOT NULL,
    [LOCATION]      NVARCHAR (200)   NOT NULL,
    [IS_ENABLED]    BIT              NOT NULL,
    [CREATED_BY]    NVARCHAR (100)   NOT NULL,
    [CREATED_DATE]  DATETIME         NOT NULL,
    [UPDATE_BY]     NVARCHAR (100)   NOT NULL,
    [UPDATE_DATE]   DATETIME         NOT NULL,
    [ACTIVITY_TYPE] INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.ACTIVITY] PRIMARY KEY CLUSTERED ([ACTIVITY_ID] ASC),
    CONSTRAINT [FK_dbo.ACTIVITY_dbo.FORUM_FORUM_ID] FOREIGN KEY ([FORUM_ID]) REFERENCES [dbo].[FORUM] ([FORUM_ID])
);








GO





GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'論壇編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACTIVITY', @level2type = N'COLUMN', @level2name = N'FORUM_ID';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACTIVITY', @level2type = N'COLUMN', @level2name = N'SUBJECT';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACTIVITY', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日期與時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACTIVITY', @level2type = N'COLUMN', @level2name = N'HOLD_DATE';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地點', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACTIVITY', @level2type = N'COLUMN', @level2name = N'LOCATION';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0: 論壇活動, 1: 工作坊, 2:招募', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ACTIVITY', @level2type = N'COLUMN', @level2name = N'ACTIVITY_TYPE';




GO
CREATE NONCLUSTERED INDEX [IX_FORUM_ID]
    ON [dbo].[ACTIVITY]([FORUM_ID] ASC);

