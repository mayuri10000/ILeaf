CREATE TABLE [dbo].[ClassInfos] (
    [Id]           BIGINT       IDENTITY (1, 1) NOT NULL,
    [SchoolId]     INT          NOT NULL,
    [ClassName]    VARCHAR (50) NULL,
    [Year]         SMALLINT     NOT NULL,
    [Major]        VARCHAR (50) NULL,
    [InstructorId] BIGINT       NULL,
    CONSTRAINT [PK_ClassInfos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClassInfos_SchoolInfos] FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[SchoolInfos] ([SchoolId])
);

