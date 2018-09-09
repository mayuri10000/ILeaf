CREATE TABLE [dbo].[ChatMessages] (
    [Id]          BIGINT        NOT NULL,
    [MessageBody] VARCHAR (MAX) NOT NULL,
    [MessageType] TINYINT       NOT NULL,
    [SendTime]    DATETIME      NOT NULL,
    [SenderId]    BIGINT        NOT NULL,
    [RecipientId] BIGINT        NULL,
    [GroupId]     BIGINT        NULL,
    CONSTRAINT [PK_ChatMessages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ChatMessages_Accounts] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_ChatMessages_Accounts1] FOREIGN KEY ([RecipientId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_ChatMessages_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ChatMessages_Accounts]
    ON [dbo].[ChatMessages]([SenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ChatMessages_Accounts1]
    ON [dbo].[ChatMessages]([RecipientId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ChatMessages_Groups]
    ON [dbo].[ChatMessages]([GroupId] ASC);

