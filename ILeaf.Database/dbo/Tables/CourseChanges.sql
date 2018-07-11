CREATE TABLE [dbo].[CourseChanges] (
    [CourseId]    BIGINT        NOT NULL,
    [Time]        DATETIME      NOT NULL,
    [SubmitTime]  DATETIME      NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [Xml]         XML           NOT NULL,
    CONSTRAINT [FK_CourseChanges_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

