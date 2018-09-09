CREATE TABLE [dbo].[AttachmentCourses] (
    [AttachmentId] BIGINT   NOT NULL,
    [CourseId]     BIGINT   NOT NULL,
    [CourseTime]   DATETIME NOT NULL,
    CONSTRAINT [PK_AttachmentCourses] PRIMARY KEY CLUSTERED ([AttachmentId] ASC, [CourseId] ASC, [CourseTime] ASC),
    CONSTRAINT [FK_AttachmentCourse_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_AttachmentCourse_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_AttachmentCourse_Courses]
    ON [dbo].[AttachmentCourses]([CourseId] ASC);

