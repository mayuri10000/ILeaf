CREATE TABLE [dbo].[AttachmentClass] (
    [AttachmentId] BIGINT NOT NULL,
    [ClassId]      BIGINT NOT NULL,
    CONSTRAINT [FK_AttachmentClass_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_AttachmentClass_ClassInfos] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[ClassInfos] ([Id])
);

