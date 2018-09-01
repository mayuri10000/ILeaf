CREATE TABLE [dbo].[SelectableCourseStudents] (
    [CourseId]  BIGINT NOT NULL,
    [StudentId] BIGINT NOT NULL,
    CONSTRAINT [PK_SelectableCourseStudents] PRIMARY KEY CLUSTERED ([CourseId] ASC, [StudentId] ASC),
    CONSTRAINT [FK_SelectableCourseStudents_Accounts] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_SelectableCourseStudents_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

