CREATE TABLE [dbo].[ChatMessages] (
    [MessageType]    TINYINT       NOT NULL,
    [MessageContent] VARCHAR (MAX) NOT NULL,
    [SenderId]       BIGINT        NOT NULL,
    [ReceiverId]     BIGINT        NOT NULL,
    [IsSendToGroup]  BIT           NOT NULL,
    [SendTime]       DATETIME      NOT NULL,
    CONSTRAINT [FK_ChatMessages_Account] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_ChatMessages_Account1] FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_ChatMessages_Groups] FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[Groups] ([Id])
);

