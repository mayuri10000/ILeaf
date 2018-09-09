CREATE TABLE [dbo].[CourseClass] (
    [Classes_Id] BIGINT     NOT NULL,
    [Courses_Id] BIGINT     NOT NULL,
    [hs]         NCHAR (10) NULL,
    CONSTRAINT [PK_CourseClass] PRIMARY KEY CLUSTERED ([Classes_Id] ASC, [Courses_Id] ASC),
    CONSTRAINT [FK_CourseClass_ClassInfos] FOREIGN KEY ([Classes_Id]) REFERENCES [dbo].[ClassInfos] ([Id]),
    CONSTRAINT [FK_CourseClass_Courses] FOREIGN KEY ([Courses_Id]) REFERENCES [dbo].[Courses] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CourseClass_Courses]
    ON [dbo].[CourseClass]([Courses_Id] ASC);

