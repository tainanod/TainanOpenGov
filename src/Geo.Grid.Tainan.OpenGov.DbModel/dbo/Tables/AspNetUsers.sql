CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128)   NOT NULL,
    [Email]                NVARCHAR (256)   NULL,
    [EmailConfirmed]       BIT              NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)   NULL,
    [SecurityStamp]        NVARCHAR (MAX)   NULL,
    [PhoneNumber]          NVARCHAR (MAX)   NULL,
    [PhoneNumberConfirmed] BIT              NOT NULL,
    [TwoFactorEnabled]     BIT              NOT NULL,
    [LockoutEndDateUtc]    DATETIME         NULL,
    [LockoutEnabled]       BIT              NOT NULL,
    [AccessFailedCount]    INT              NOT NULL,
    [NickName]             NVARCHAR (MAX)   NULL,
    [UserName]             NVARCHAR (256)   NOT NULL,
    [ImageUrl]             NVARCHAR (MAX)   NULL,
    [DataSetUnitId]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
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



GO



GO



GO



GO



GO


