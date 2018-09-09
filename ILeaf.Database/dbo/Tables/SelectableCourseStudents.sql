CREATE TABLE [dbo].[SelectableCourseStudents] (
    [SelectedStudent_Id] BIGINT     NOT NULL,
    [SelectedCourses_Id] BIGINT     NOT NULL,
    [hf]                 NCHAR (10) NULL,
    CONSTRAINT [PK_SelectableCourseStudents] PRIMARY KEY CLUSTERED ([SelectedStudent_Id] ASC, [SelectedCourses_Id] ASC),
    CONSTRAINT [FK_SelectableCourseStudents_Accounts] FOREIGN KEY ([SelectedStudent_Id]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_SelectableCourseStudents_Courses] FOREIGN KEY ([SelectedCourses_Id]) REFERENCES [dbo].[Courses] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_SelectableCourseStudents_Courses]
    ON [dbo].[SelectableCourseStudents]([SelectedCourses_Id] ASC);

