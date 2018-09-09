CREATE TABLE [dbo].[AttachmentClass] (
    [AccessableAttachments_Id] BIGINT     NOT NULL,
    [AccessableClasses_Id]     BIGINT     NOT NULL,
    [hf]                       NCHAR (10) NULL,
    CONSTRAINT [PK_AttachmentClass] PRIMARY KEY CLUSTERED ([AccessableAttachments_Id] ASC, [AccessableClasses_Id] ASC),
    CONSTRAINT [FK_AttachmentClass_Attachments] FOREIGN KEY ([AccessableAttachments_Id]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_AttachmentClass_ClassInfos] FOREIGN KEY ([AccessableClasses_Id]) REFERENCES [dbo].[ClassInfos] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_AttachmentClass_ClassInfos]
    ON [dbo].[AttachmentClass]([AccessableClasses_Id] ASC);

