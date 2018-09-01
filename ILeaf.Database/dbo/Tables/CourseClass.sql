CREATE TABLE [dbo].[CourseClass] (
    [ClassId]  BIGINT NOT NULL,
    [CourseId] BIGINT NOT NULL,
    CONSTRAINT [PK_CourseClass] PRIMARY KEY CLUSTERED ([ClassId] ASC, [CourseId] ASC),
    CONSTRAINT [FK_CourseClass_ClassInfos] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[ClassInfos] ([Id]),
    CONSTRAINT [FK_CourseClass_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

