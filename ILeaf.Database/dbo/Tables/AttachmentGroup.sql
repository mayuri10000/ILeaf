CREATE TABLE [dbo].[AttachmentGroup] (
    [AttachmentId] BIGINT NOT NULL,
    [GroupId]      BIGINT NOT NULL,
    CONSTRAINT [FK_AttachmentGroup_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_AttachmentGroup_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);

