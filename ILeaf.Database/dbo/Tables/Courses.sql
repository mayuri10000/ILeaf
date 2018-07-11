CREATE TABLE [dbo].[Courses] (
    [Id]        BIGINT       IDENTITY (1, 1) NOT NULL,
    [Title]     VARCHAR (50) NOT NULL,
    [SchoolId]  BIGINT       NOT NULL,
    [TeacherId] BIGINT       NOT NULL,
    [Classroom] VARCHAR (50) NULL,
    [TermStart] DATETIME     NOT NULL,
    [Weeks]     BINARY (25)  NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Courses_Account] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_Courses_Groups] FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[Groups] ([Id])
);

