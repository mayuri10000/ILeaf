CREATE TABLE [dbo].[Appointments] (
    [Id]           BIGINT       IDENTITY (1, 1) NOT NULL,
    [Title]        VARCHAR (50) NOT NULL,
    [CreatorId]    BIGINT       NOT NULL,
    [Details]      TEXT         NULL,
    [Place]        VARCHAR (50) NULL,
    [CreationTime] DATETIME     NOT NULL,
    [StartTime]    DATETIME     NOT NULL,
    [IsAllDay]     BIT          NOT NULL,
    [EndTime]      DATETIME     NULL,
    [Visibily]     TINYINT      NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Appointments_Accounts] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[Accounts] ([Id])
);

