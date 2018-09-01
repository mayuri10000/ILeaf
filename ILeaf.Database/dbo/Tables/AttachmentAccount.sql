CREATE TABLE [dbo].[AttachmentAccount] (
    [AttachmentId] BIGINT NOT NULL,
    [AccountId]    BIGINT NOT NULL,
    CONSTRAINT [PK_AttachmentAccount] PRIMARY KEY CLUSTERED ([AttachmentId] ASC, [AccountId] ASC),
    CONSTRAINT [FK_AttachmentAccount_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_AttachmentAccount_Attachments] FOREIGN KEY ([AttachmentId]) REFERENCES [dbo].[Attachments] ([Id])
);

