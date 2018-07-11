CREATE TABLE [dbo].[CourseClass] (
    [CourseId] BIGINT NOT NULL,
    [ClassId]  BIGINT NOT NULL,
    CONSTRAINT [FK_CourseClass_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]),
    CONSTRAINT [FK_CourseClass_Groups] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[Groups] ([Id])
);

