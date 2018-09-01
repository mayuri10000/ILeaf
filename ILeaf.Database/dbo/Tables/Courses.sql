CREATE TABLE [dbo].[Courses] (
    [Id]                 BIGINT       IDENTITY (1, 1) NOT NULL,
    [Title]              VARCHAR (50) NOT NULL,
    [TeacherId]          BIGINT       NOT NULL,
    [SchoolId]           INT          NOT NULL,
    [IsSelectableCourse] BIT          NOT NULL,
    [Classroom]          VARCHAR (50) NOT NULL,
    [SemesterStart]      DATETIME     NOT NULL,
    [Weeks]              BINARY (50)  NOT NULL,
    [Weekday]            TINYINT      NOT NULL,
    [StartTime]          TIME (7)     NOT NULL,
    [EndTime]            TIME (7)     NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Courses_Accounts] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_Courses_SchoolInfos] FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[SchoolInfos] ([SchoolId])
);

