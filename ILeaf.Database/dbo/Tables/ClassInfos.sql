CREATE TABLE [dbo].[ClassInfos] (
    [Id]           BIGINT       NOT NULL,
    [SchoolId]     INT          NOT NULL,
    [ClassName]    VARCHAR (50) NULL,
    [Year]         SMALLINT     NOT NULL,
    [Major]        VARCHAR (50) NULL,
    [InstructorId] BIGINT       NOT NULL,
    CONSTRAINT [PK_ClassInfos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClassInfos_Accounts] FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_ClassInfos_SchoolInfos] FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[SchoolInfos] ([SchoolId])
);

