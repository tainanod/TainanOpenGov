CREATE TABLE [dbo].[MdQuestion] (
    [QstId]       UNIQUEIDENTIFIER NOT NULL,
    [GroupId]     UNIQUEIDENTIFIER NOT NULL,
    [QstSorting]  INT              NOT NULL,
    [QstAnsType]  INT              NOT NULL,
    [QstQuestion] NVARCHAR (300)   NULL,
    [QstScore]    INT              NOT NULL,
    [EditDate]    DATETIME         NOT NULL,
    [Editer]      UNIQUEIDENTIFIER NULL,
    [IsEnable]    BIT              NOT NULL,
    [IsRequired]  BIT              NOT NULL,
    CONSTRAINT [PK_dbo.MdQuestion] PRIMARY KEY CLUSTERED ([QstId] ASC),
    CONSTRAINT [FK_dbo.MdQuestion_dbo.MdQuestionGroup_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[MdQuestionGroup] ([GroupId])
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



GO
CREATE NONCLUSTERED INDEX [IX_GroupId]
    ON [dbo].[MdQuestion]([GroupId] ASC);

