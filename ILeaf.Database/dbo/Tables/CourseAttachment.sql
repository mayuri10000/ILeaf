CREATE TABLE [dbo].[CourseAttachment] (
    [CourseId]     BIGINT   NOT NULL,
    [Time]         DATETIME NOT NULL,
    [AttachmentId] BIGINT   NOT NULL,
    CONSTRAINT [FK_CourseAttachment_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_CourseAttachment_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

