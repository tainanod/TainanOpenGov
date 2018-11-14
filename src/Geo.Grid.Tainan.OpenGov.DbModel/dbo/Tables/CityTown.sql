CREATE TABLE [dbo].[CityTown] (
    [CityTownId] UNIQUEIDENTIFIER NOT NULL,
    [TownSeq]    INT              NOT NULL,
    [TownName]   NVARCHAR (50)    NULL,
    [CitySeq]    INT              NOT NULL,
    [CityName]   NVARCHAR (50)    NULL,
    [IsEnable]   BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.CityTown] PRIMARY KEY CLUSTERED ([CityTownId] ASC)
);




GO



GO



GO



GO



GO



GO


