CREATE TABLE [dbo].[Engineering] (
    [GovernmentAenciesCode] NVARCHAR (100)   NOT NULL,
    [Code]                  NVARCHAR (100)   NOT NULL,
    [GovernmentAencies]     NVARCHAR (200)   NOT NULL,
    [Name]                  NVARCHAR (300)   NOT NULL,
    [Amount]                DECIMAL (20)     NOT NULL,
    [Supervision]           NVARCHAR (200)   NOT NULL,
    [Factory]               NVARCHAR (200)   NOT NULL,
    [Category]              NVARCHAR (100)   NOT NULL,
    [CityTown]              NVARCHAR (100)   NOT NULL,
    [Geom]                  [sys].[geometry] NOT NULL,
    [Address]               NVARCHAR (MAX)   NOT NULL,
    [Summary]               NVARCHAR (MAX)   NULL,
    [ActualStartDate]       DATETIME         NOT NULL,
    [TargetCompleteDate]    DATETIME         NULL,
    [ChangeCompleteDate]    DATETIME         NULL,
    [ProgressDate]          DATETIME         NOT NULL,
    [TargetProgress]        DECIMAL (5, 2)   NOT NULL,
    [ActualProgress]        DECIMAL (5, 2)   NOT NULL,
    [Discrepancy]           DECIMAL (5, 2)   NOT NULL,
    [Status]                NVARCHAR (100)   NOT NULL,
    [BehindReason]          NVARCHAR (MAX)   NULL,
    [Analysis]              NVARCHAR (MAX)   NULL,
    [Solution]              NVARCHAR (MAX)   NULL,
    [ImproveTermDate]       DATETIME         NULL,
    [ActualCompleteDate]    DATETIME         NULL,
    [CreatedBy]             NVARCHAR (128)   NOT NULL,
    [CreatedDate]           DATETIME         CONSTRAINT [DF_Engineering_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]             NVARCHAR (128)   NOT NULL,
    [UpdatedDate]           DATETIME         CONSTRAINT [DF_Engineering_UpdatedDate] DEFAULT (getdate()) NOT NULL,
    [IsEnabled]             BIT              CONSTRAINT [DF_Engineering_IsEnabled] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Engineering] PRIMARY KEY CLUSTERED ([GovernmentAenciesCode] ASC, [Code] ASC)
);








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否啟用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'IsEnabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最後更新日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最後更新人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建立人員', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'實際完工日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'ActualCompleteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'改進期限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'ImproveTermDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'解決辦法', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Solution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'原因分析', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Analysis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'落後因素', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'BehindReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'狀態', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'差異(百分比)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Discrepancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'實際進度(百分比)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'ActualProgress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'預定進度(百分比)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'TargetProgress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'進度月份', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'ProgressDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'變更後預定完工日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'ChangeCompleteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'原合約預定完工日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'TargetCompleteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'實際開工日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'ActualStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工程概要', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Summary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'詳細地點', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'X座標、Y座標', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Geom';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標案類別', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Category';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'得標廠商', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Factory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'監造單位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Supervision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'決標金額(仟元)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Amount';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'標案名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'執行機關', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'GovernmentAencies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'編號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'執行機關代碼', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'GovernmentAenciesCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'縣市鄉鎮', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Engineering', @level2type = N'COLUMN', @level2name = N'CityTown';

