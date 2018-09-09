CREATE TABLE [dbo].[Groups] (
    [Id]           BIGINT       IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [HeadmanId]    BIGINT       NOT NULL,
    [CreationTime] DATETIME     NOT NULL,
    [Announcement] TEXT         NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groups_Accounts] FOREIGN KEY ([HeadmanId]) REFERENCES [dbo].[Accounts] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Groups_Accounts]
    ON [dbo].[Groups]([HeadmanId] ASC);

