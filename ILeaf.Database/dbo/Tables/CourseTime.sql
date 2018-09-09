CREATE TABLE [dbo].[CourseTime] (
    [Id]        BIGINT       IDENTITY (1, 1) NOT NULL,
    [CourseId]  BIGINT       NOT NULL,
    [Weekday]   TINYINT      NOT NULL,
    [Classroom] VARCHAR (50) NOT NULL,
    [StartTime] TIME (7)     NOT NULL,
    [EndTime]   TIME (7)     NOT NULL,
    CONSTRAINT [PK_CourseTime] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CourseTime_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

