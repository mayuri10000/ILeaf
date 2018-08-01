CREATE TABLE [dbo].[Groups] (
    [Id]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC)
);

