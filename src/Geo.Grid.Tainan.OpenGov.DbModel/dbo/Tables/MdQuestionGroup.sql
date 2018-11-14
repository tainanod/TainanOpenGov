CREATE TABLE [dbo].[MdQuestionGroup] (
    [GroupId]    UNIQUEIDENTIFIER NOT NULL,
    [GroupTitle] NVARCHAR (500)   NULL,
    [InfoId]     UNIQUEIDENTIFIER NOT NULL,
    [GroupDesc]  NVARCHAR (500)   NULL,
    [GroupSort]  INT              NOT NULL,
    [IsEnable]   BIT              NOT NULL,
    [EditDate]   DATETIME         NOT NULL,
    [Editer]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_dbo.MdQuestionGroup] PRIMARY KEY CLUSTERED ([GroupId] ASC),
    CONSTRAINT [FK_dbo.MdQuestionGroup_dbo.MdQuestInfo_InfoId] FOREIGN KEY ([InfoId]) REFERENCES [dbo].[MdQuestInfo] ([InfoId])
);




GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_InfoId]
    ON [dbo].[MdQuestionGroup]([InfoId] ASC);

