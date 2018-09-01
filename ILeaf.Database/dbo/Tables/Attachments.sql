CREATE TABLE [dbo].[Attachments] (
    [Id]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [StoragePath]        VARCHAR (150) NULL,
    [FileName]           VARCHAR (50)  NOT NULL,
    [FileSize]           BIGINT        NULL,
    [MD5Hash]            NVARCHAR (32) NULL,
    [IsPublicAttachment] BIT           NOT NULL,
    [UploaderId]         BIGINT        NOT NULL,
    [UploadTime]         DATETIME      NOT NULL,
    [ExpireTime]         DATETIME      NOT NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Attachments_Accounts] FOREIGN KEY ([UploaderId]) REFERENCES [dbo].[Accounts] ([Id])
);

