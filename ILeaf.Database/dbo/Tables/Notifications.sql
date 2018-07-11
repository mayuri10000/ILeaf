CREATE TABLE [dbo].[Notifications] (
    [NotificationContent] VARCHAR (MAX) NOT NULL,
    [SenderId]            BIGINT        NOT NULL,
    [ReceiverId]          BIGINT        NOT NULL,
    [IsSendToGroup]       BIT           NOT NULL,
    [SendTime]            DATETIME      NOT NULL,
    CONSTRAINT [FK_Notifications_Account] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_Notifications_Account1] FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_Notifications_Groups] FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[Groups] ([Id])
);

