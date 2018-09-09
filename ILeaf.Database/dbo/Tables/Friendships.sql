CREATE TABLE [dbo].[Friendships] (
    [Account1]   BIGINT NOT NULL,
    [Account2]   BIGINT NOT NULL,
    [IsAccepted] BIT    NOT NULL,
    CONSTRAINT [PK_Friendships] PRIMARY KEY CLUSTERED ([Account1] ASC, [Account2] ASC),
    CONSTRAINT [FK_Friendship_Accounts] FOREIGN KEY ([Account1]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_Friendship_Accounts1] FOREIGN KEY ([Account2]) REFERENCES [dbo].[Accounts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Friendship_Accounts1]
    ON [dbo].[Friendships]([Account2] ASC);

