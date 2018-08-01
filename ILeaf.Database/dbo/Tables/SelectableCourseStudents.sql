CREATE TABLE [dbo].[SelectableCourseStudents] (
    [CourseId]  BIGINT NOT NULL,
    [StudentId] BIGINT NOT NULL,
    CONSTRAINT [FK_SelectableCourseStudents_Accounts] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_SelectableCourseStudents_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

