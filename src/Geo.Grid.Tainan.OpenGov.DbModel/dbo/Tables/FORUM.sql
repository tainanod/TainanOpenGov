CREATE TABLE [dbo].[FORUM] (
    [FORUM_ID]      UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [CATEGORY]      NVARCHAR (100)   NOT NULL,
    [SUBJECT]       NVARCHAR (300)   NOT NULL,
    [HOLDER]        NVARCHAR (100)   NULL,
    [DESCRIPTION]   NVARCHAR (4000)  NOT NULL,
    [OPEN_DATE]     DATETIME         NOT NULL,
    [CLOSE_DATE]    DATETIME         NOT NULL,
    [IS_ENABLED]    BIT              NOT NULL,
    [CREATED_BY]    NVARCHAR (100)   NOT NULL,
    [CREATED_DATE]  DATETIME         NOT NULL,
    [UPDATE_BY]     NVARCHAR (100)   NOT NULL,
    [UPDATE_DATE]   DATETIME         NOT NULL,
    [FORUM_TYPE]    INT              DEFAULT ((0)) NOT NULL,
    [UPPER_ID]      UNIQUEIDENTIFIER NULL,
    [ANNOUNCEMENT]  NVARCHAR (4000)  NULL,
    [DataSetUnitId] UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    CONSTRAINT [PK_dbo.FORUM] PRIMARY KEY CLUSTERED ([FORUM_ID] ASC),
    CONSTRAINT [FK_dbo.FORUM_dbo.DataSetUnit_DataSetUnitId] FOREIGN KEY ([DataSetUnitId]) REFERENCES [dbo].[DataSetUnit] ([UnitId])
);








GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主題', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORUM', @level2type = N'COLUMN', @level2name = N'SUBJECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORUM', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'發佈時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORUM', @level2type = N'COLUMN', @level2name = N'OPEN_DATE';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'截止時間', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORUM', @level2type = N'COLUMN', @level2name = N'CLOSE_DATE';


GO
CREATE NONCLUSTERED INDEX [IX_DataSetUnitId]
    ON [dbo].[FORUM]([DataSetUnitId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主辦單位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORUM', @level2type = N'COLUMN', @level2name = N'HOLDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主題類別', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORUM', @level2type = N'COLUMN', @level2name = N'CATEGORY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'討論區公告', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORUM', @level2type = N'COLUMN', @level2name = N'ANNOUNCEMENT';

