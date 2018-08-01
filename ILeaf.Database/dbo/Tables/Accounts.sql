CREATE TABLE [dbo].[Accounts] (
    [Id]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserName]          VARCHAR (50)  NOT NULL,
    [RealName]          VARCHAR (50)  NULL,
    [Email]             VARCHAR (50)  NULL,
    [WeChatOpenId]      VARCHAR (50)  NULL,
    [EncryptedPassword] VARCHAR (100) NULL,
    [PasswordSalt]      VARCHAR (50)  NULL,
    [UserType]          TINYINT       NOT NULL,
    [IsAdmin]           BIT           NOT NULL,
    [HeadImgUrl]        VARCHAR (100) NULL,
    [SchoolId]          INT           NULL,
    [ClassId]           BIGINT        NULL,
    [SchoolCardNum]     VARCHAR (50)  NULL,
    [RegistionTime]     DATETIME      NOT NULL,
    [ThisLoginTime]     DATETIME      NULL,
    [LastLoginTime]     DATETIME      NULL,
    [ThisLoginIP]       VARCHAR (15)  NULL,
    [LastLoginIP]       VARCHAR (15)  NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Accounts_ClassInfos] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[ClassInfos] ([Id]),
    CONSTRAINT [FK_Accounts_SchoolInfos] FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[SchoolInfos] ([SchoolId])
);

