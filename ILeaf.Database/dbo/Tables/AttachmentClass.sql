CREATE TABLE [dbo].[AttachmentClass] (
    [AttachmentId] BIGINT NOT NULL,
    [ClassId]      BIGINT NOT NULL,
    CONSTRAINT [PK_AttachmentClass] PRIMARY KEY CLUSTERED ([AttachmentId] ASC, [ClassId] ASC),
    CONSTRAINT [FK_AttachmentClass_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_AttachmentClass_ClassInfos] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[ClassInfos] ([Id])
);

