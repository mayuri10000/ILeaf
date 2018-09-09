CREATE TABLE [dbo].[CourseChanges] (
    [CourseId]     BIGINT       NOT NULL,
    [CourseTime]   DATETIME     NOT NULL,
    [ChangeType]   TINYINT      NOT NULL,
    [ChangedValue] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CourseChanges] PRIMARY KEY CLUSTERED ([CourseId] ASC, [CourseTime] ASC, [ChangeType] ASC, [ChangedValue] ASC),
    CONSTRAINT [FK_CourseChange_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

