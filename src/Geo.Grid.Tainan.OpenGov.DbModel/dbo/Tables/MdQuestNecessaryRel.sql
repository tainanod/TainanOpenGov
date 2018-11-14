CREATE TABLE [dbo].[MdQuestNecessaryRel] (
    [SetId]      UNIQUEIDENTIFIER NOT NULL,
    [TargetType] INT              NOT NULL,
    [TargetId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.MdQuestNecessaryRel] PRIMARY KEY CLUSTERED ([SetId] ASC),
    CONSTRAINT [FK_dbo.MdQuestNecessaryRel_dbo.MdQuestSetItem_SetId] FOREIGN KEY ([SetId]) REFERENCES [dbo].[MdQuestSetItem] ([SetId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SetId]
    ON [dbo].[MdQuestNecessaryRel]([SetId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目標類型 1:題組 2:題目', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MdQuestNecessaryRel', @level2type = N'COLUMN', @level2name = N'TargetType';

