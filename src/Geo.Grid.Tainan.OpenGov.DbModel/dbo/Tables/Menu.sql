CREATE TABLE [dbo].[Menu] (
    [MenuId]      UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Controller]  NVARCHAR (MAX)   NOT NULL,
    [Action]      NVARCHAR (MAX)   NOT NULL,
    [Area]        NVARCHAR (MAX)   NULL,
    [Sort]        INT              NOT NULL,
    [IsEnabled]   BIT              NOT NULL,
    [CreatedBy]   NVARCHAR (128)   NULL,
    [CreatedDate] DATETIME         NOT NULL,
    [UpdatedBy]   NVARCHAR (128)   NULL,
    [UpdatedDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.Menu] PRIMARY KEY CLUSTERED ([MenuId] ASC)
);

