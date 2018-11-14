CREATE TABLE [dbo].[DataSetRelExtension] (
    [SetId]       UNIQUEIDENTIFIER NOT NULL,
    [ExtensionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.DataSetRelExtension] PRIMARY KEY CLUSTERED ([SetId] ASC, [ExtensionId] ASC),
    CONSTRAINT [FK_dbo.DataSetRelExtension_dbo.DataSet_SetId] FOREIGN KEY ([SetId]) REFERENCES [dbo].[DataSet] ([SetId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.DataSetRelExtension_dbo.DataSetExtension_ExtensionId] FOREIGN KEY ([ExtensionId]) REFERENCES [dbo].[DataSetExtension] ([ExtensionId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_SetId]
    ON [dbo].[DataSetRelExtension]([SetId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ExtensionId]
    ON [dbo].[DataSetRelExtension]([ExtensionId] ASC);

