CREATE TABLE [dbo].[AttachmentGroup] (
    [AccessableAttachments_Id] BIGINT     NOT NULL,
    [AccessableGroups_Id]      BIGINT     NOT NULL,
    [hf]                       NCHAR (10) NULL,
    CONSTRAINT [PK_AttachmentGroup] PRIMARY KEY CLUSTERED ([AccessableAttachments_Id] ASC, [AccessableGroups_Id] ASC),
    CONSTRAINT [FK_AttachmentGroup_Attachments] FOREIGN KEY ([AccessableAttachments_Id]) REFERENCES [dbo].[Attachments] ([Id]),
    CONSTRAINT [FK_AttachmentGroup_Groups] FOREIGN KEY ([AccessableGroups_Id]) REFERENCES [dbo].[Groups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_AttachmentGroup_Groups]
    ON [dbo].[AttachmentGroup]([AccessableGroups_Id] ASC);

