CREATE TABLE [dbo].[Notifications] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Text]        VARCHAR (MAX) NULL,
    [SentTime]    DATETIME      NOT NULL,
    [SenderId]    BIGINT        NULL,
    [RecipientId] BIGINT        NULL,
    [GroupId]     BIGINT        NULL,
    [Section]     VARCHAR (15)  NULL,
    [Level]       TINYINT       NOT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Notification_Accounts]
    ON [dbo].[Notifications]([SenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Notification_Accounts1]
    ON [dbo].[Notifications]([RecipientId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Notification_Groups]
    ON [dbo].[Notifications]([GroupId] ASC);

