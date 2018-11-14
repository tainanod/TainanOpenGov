CREATE TABLE [dbo].[REL_AspNetRoles_Menu] (
    [RoleId] NVARCHAR (128)   NOT NULL,
    [MenuId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.REL_AspNetRoles_Menu] PRIMARY KEY CLUSTERED ([RoleId] ASC, [MenuId] ASC),
    CONSTRAINT [FK_dbo.REL_AspNetRoles_Menu_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.REL_AspNetRoles_Menu_dbo.Menu_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [dbo].[Menu] ([MenuId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[REL_AspNetRoles_Menu]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MenuId]
    ON [dbo].[REL_AspNetRoles_Menu]([MenuId] ASC);

