CREATE TABLE [dbo].[AttachmentAccount] (
    [AccessableUsers_Id]       BIGINT     NOT NULL,
    [AccessableAttachments_Id] BIGINT     NOT NULL,
    [hf]                       NCHAR (10) NULL,
    CONSTRAINT [PK_AttachmentAccount] PRIMARY KEY CLUSTERED ([AccessableUsers_Id] ASC, [AccessableAttachments_Id] ASC),
    CONSTRAINT [FK_AttachmentAccount_Accounts] FOREIGN KEY ([AccessableUsers_Id]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_AttachmentAccount_Attachments] FOREIGN KEY ([AccessableAttachments_Id]) REFERENCES [dbo].[Attachments] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_AttachmentAccount_Attachments]
    ON [dbo].[AttachmentAccount]([AccessableAttachments_Id] ASC);

