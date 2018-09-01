CREATE TABLE [dbo].[CourseChange] (
    [CourseId]     BIGINT       NOT NULL,
    [CourseTime]   DATETIME     NOT NULL,
    [ChangeType]   TINYINT      NOT NULL,
    [ChangedValue] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CourseChange] PRIMARY KEY CLUSTERED ([CourseId] ASC, [CourseTime] ASC, [ChangeType] ASC, [ChangedValue] ASC),
    CONSTRAINT [FK_CourseChange_Courses] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id])
);

