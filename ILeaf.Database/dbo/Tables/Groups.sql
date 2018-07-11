CREATE TABLE [dbo].[Groups] (
    [Id]        BIGINT       NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [Type]      TINYINT      NOT NULL,
    [HeadmanId] BIGINT       NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groups_Account] FOREIGN KEY ([HeadmanId]) REFERENCES [dbo].[Account] ([Id])
);

