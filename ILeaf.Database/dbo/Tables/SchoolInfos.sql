CREATE TABLE [dbo].[SchoolInfos] (
    [SchoolId]   INT          NOT NULL,
    [SchoolName] VARCHAR (50) NOT NULL,
    [Province]   VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_SchoolInfos] PRIMARY KEY CLUSTERED ([SchoolId] ASC)
);

