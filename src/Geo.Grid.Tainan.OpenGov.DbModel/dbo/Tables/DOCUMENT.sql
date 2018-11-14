﻿CREATE TABLE [dbo].[DOCUMENT] (
    [DOCUMENT_ID]   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [SIZE]          BIGINT           NOT NULL,
    [FILE_NAME]     NVARCHAR (255)   NOT NULL,
    [FILE_TYPE]     NVARCHAR (255)   NOT NULL,
    [ALT]           NVARCHAR (255)   NULL,
    [IS_ENABLED]    BIT              NOT NULL,
    [CREATED_BY]    NVARCHAR (100)   NOT NULL,
    [CREATED_DATE]  DATETIME         NOT NULL,
    [UPDATE_BY]     NVARCHAR (100)   NOT NULL,
    [UPDATE_DATE]   DATETIME         NOT NULL,
    [DOCUMENT_TYPE] INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.DOCUMENT] PRIMARY KEY CLUSTERED ([DOCUMENT_ID] ASC)
);




GO



GO



GO



GO



GO



GO



GO


