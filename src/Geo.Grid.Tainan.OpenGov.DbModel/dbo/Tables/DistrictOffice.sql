CREATE TABLE [dbo].[DistrictOffice] (
    [OfficeId] UNIQUEIDENTIFIER CONSTRAINT [DF_DistrictOffice_OfficeId] DEFAULT (newid()) NOT NULL,
    [Name]     NVARCHAR (500)   NOT NULL,
    [Lng]      FLOAT (53)       NOT NULL,
    [Lat]      FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_DistrictOffice] PRIMARY KEY CLUSTERED ([OfficeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'緯度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DistrictOffice', @level2type = N'COLUMN', @level2name = N'Lat';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'經度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DistrictOffice', @level2type = N'COLUMN', @level2name = N'Lng';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'區公所名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DistrictOffice', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'區公所編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DistrictOffice', @level2type = N'COLUMN', @level2name = N'OfficeId';

