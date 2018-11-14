CREATE TABLE [dbo].[MdQuestSetItem] (
    [SetId]      UNIQUEIDENTIFIER NOT NULL,
    [QstId]      UNIQUEIDENTIFIER NOT NULL,
    [SetDesc]    NVARCHAR (100)   NULL,
    [SetScore]   INT              NOT NULL,
    [SetSorting] INT              NOT NULL,
    [SetNote]    BIT              NOT NULL,
    [EditDate]   DATETIME         NOT NULL,
    [Editer]     UNIQUEIDENTIFIER NULL,
    [IsEnable]   BIT              NOT NULL,
    CONSTRAINT [PK_dbo.MdQuestSetItem] PRIMARY KEY CLUSTERED ([SetId] ASC),
    CONSTRAINT [FK_dbo.MdQuestSetItem_dbo.MdQuestion_QstId] FOREIGN KEY ([QstId]) REFERENCES [dbo].[MdQuestion] ([QstId])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_QstId]
    ON [dbo].[MdQuestSetItem]([QstId] ASC);

