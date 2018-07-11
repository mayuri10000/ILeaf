CREATE TABLE [dbo].[Attachments] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [UploaderId]  BIGINT        NULL,
    [FileName]    VARCHAR (50)  NOT NULL,
    [Size]        BIGINT        NOT NULL,
    [StoragePath] VARCHAR (MAX) NOT NULL,
    [ExpireTime]  DATETIME      NOT NULL,
    [MD5Hash]     NVARCHAR (32) NOT NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Attachments_Account] FOREIGN KEY ([UploaderId]) REFERENCES [dbo].[Account] ([Id])
);

