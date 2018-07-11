CREATE TABLE [dbo].[Account] (
    [Id]                  BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserName]            VARCHAR (50)  NOT NULL,
    [EncryptedPassword]   VARCHAR (50)  NULL,
    [PasswordSalt]        VARCHAR (50)  NULL,
    [Email]               VARCHAR (50)  NULL,
    [EmailPresent]        BIT           NOT NULL,
    [WeChatOpenId]        VARCHAR (50)  NULL,
    [WeChatOpenIdPresent] BIT           NOT NULL,
    [UserType]            TINYINT       NOT NULL,
    [IsAdmin]             BIT           NOT NULL,
    [RealName]            NVARCHAR (50) NULL,
    [HeadImg]             VARCHAR (100) NULL,
    [SchoolId]            BIGINT        NULL,
    [ClassId]             BIGINT        NULL,
    [Major]               BIGINT        NULL,
    [SchoolCardNum]       VARCHAR (50)  NULL,
    [RegisterTime]        DATETIME      NOT NULL,
    CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserDetails_Groups] FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[Groups] ([Id]),
    CONSTRAINT [FK_UserDetails_Groups1] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[Groups] ([Id])
);

