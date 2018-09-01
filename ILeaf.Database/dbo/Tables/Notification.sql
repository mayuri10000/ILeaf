CREATE TABLE [dbo].[Notification] (
    [Id]          BIGINT   IDENTITY (1, 1) NOT NULL,
    [Text]        TEXT     NOT NULL,
    [SentTime]    DATETIME NOT NULL,
    [SenderId]    BIGINT   NULL,
    [RecipientId] BIGINT   NOT NULL,
    [GroupId]     BIGINT   NOT NULL,
    CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Notification_Accounts] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_Notification_Accounts1] FOREIGN KEY ([RecipientId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_Notification_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);

