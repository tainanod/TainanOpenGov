CREATE TABLE [dbo].[VoteRelBasicGroup] (
    [VoteId]  UNIQUEIDENTIFIER NOT NULL,
    [GroupId] INT              NOT NULL,
    CONSTRAINT [PK_dbo.VoteRelBasicGroup] PRIMARY KEY CLUSTERED ([VoteId] ASC, [GroupId] ASC),
    CONSTRAINT [FK_dbo.VoteRelBasicGroup_dbo.Vote_VoteId] FOREIGN KEY ([VoteId]) REFERENCES [dbo].[Vote] ([VoteId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.VoteRelBasicGroup_dbo.VoteBasicGroup_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[VoteBasicGroup] ([GroupId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_VoteId]
    ON [dbo].[VoteRelBasicGroup]([VoteId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_GroupId]
    ON [dbo].[VoteRelBasicGroup]([GroupId] ASC);

