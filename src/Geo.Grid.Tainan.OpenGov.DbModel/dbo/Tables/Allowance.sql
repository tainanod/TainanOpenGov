CREATE TABLE [dbo].[Allowance] (
    [AllowanceId]    UNIQUEIDENTIFIER CONSTRAINT [DF_Allowance_AllowanceID] DEFAULT (newid()) NOT NULL,
    [AllowanceCode]  NVARCHAR (10)    NOT NULL,
    [Name]           NVARCHAR (MAX)   NOT NULL,
    [Age]            NVARCHAR (MAX)   NULL,
    [AgeMin]         INT              NULL,
    [AgeMax]         INT              NULL,
    [LiveIn]         NVARCHAR (MAX)   NULL,
    [IsLiveIn]       NVARCHAR (MAX)   NULL,
    [LiveDays]       NVARCHAR (MAX)   NULL,
    [IsLiveDays]     NVARCHAR (MAX)   NULL,
    [Identity1]      NVARCHAR (MAX)   NULL,
    [Identity2]      NVARCHAR (MAX)   NULL,
    [Income]         NVARCHAR (MAX)   NULL,
    [IncomeValue]    INT              NULL,
    [Movable]        NVARCHAR (MAX)   NULL,
    [MovableValue]   INT              NULL,
    [Immovable]      NVARCHAR (MAX)   NULL,
    [ImmovableValue] INT              NULL,
    [Others]         NVARCHAR (MAX)   NULL,
    [Docs]           NVARCHAR (MAX)   NULL,
    [Receiver]       NVARCHAR (MAX)   NULL,
    [Contact]        NVARCHAR (MAX)   NULL,
    [MoreInfo]       NVARCHAR (MAX)   NULL,
    [SourceId]       UNIQUEIDENTIFIER NULL,
    [DataId]         INT              CONSTRAINT [DF_Allowance_DataId] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Allowance] PRIMARY KEY CLUSTERED ([AllowanceId] ASC),
    CONSTRAINT [FK_Allowance_AllowanceSource] FOREIGN KEY ([SourceId]) REFERENCES [dbo].[AllowanceSource] ([SourceId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'開放資料的唯一ID(_id)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'DataId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'資料集來源，*待完成資料同步功能後應改為 NOT NULL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'SourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'詳細資訊', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'MoreInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'聯繫方式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Contact';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收件/洽辦單位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Receiver';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'應備文件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Docs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'其他條件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Others';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'不動產條件(上限)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'ImmovableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'不動產條件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Immovable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'動產條件(上限)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'MovableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'動產條件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Movable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收入條件(上限)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'IncomeValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收入條件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Income';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'身份2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Identity2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'身份1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Identity1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'居住日數(是否大於183日)，是否', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'IsLiveDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'居住日數', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'LiveDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'設籍條件(行政區)，是否', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'IsLiveIn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'設籍條件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'LiveIn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'對象年齡(上限)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'AgeMax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'對象年齡(下限)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'AgeMin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'對象年齡', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'補助名稱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'補助流水號', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'AllowanceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'補助ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Allowance', @level2type = N'COLUMN', @level2name = N'AllowanceId';

