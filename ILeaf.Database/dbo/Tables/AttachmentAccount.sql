CREATE TABLE [dbo].[AttachmentAccount] (
    [AttachmentId] BIGINT NOT NULL,
    [AccountId]    BIGINT NOT NULL,
    CONSTRAINT [FK_AttachmentAccount_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_AttachmentAccount_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id])
);

