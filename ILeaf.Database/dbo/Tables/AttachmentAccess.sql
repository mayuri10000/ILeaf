CREATE TABLE [dbo].[AttachmentAccess] (
    [AttachmentId]       BIGINT NOT NULL,
    [AccessorId]         BIGINT NULL,
    [IsGroup]            BIT    NOT NULL,
    [IsPublicAttachment] BIT    NOT NULL,
    CONSTRAINT [FK_AttachmentAccess_Account] FOREIGN KEY ([AccessorId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_AttachmentAccess_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_AttachmentAccess_Groups] FOREIGN KEY ([AccessorId]) REFERENCES [dbo].[Groups] ([Id])
);

