CREATE TABLE [dbo].[AttachmentGroup] (
    [AttachmentId] BIGINT NOT NULL,
    [GroupId]      BIGINT NOT NULL,
    CONSTRAINT [PK_AttachmentGroup] PRIMARY KEY CLUSTERED ([AttachmentId] ASC, [GroupId] ASC),
    CONSTRAINT [FK_AttachmentGroup_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_AttachmentGroup_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);

