CREATE TABLE [dbo].[Appointments] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [CreaterUserId] BIGINT        NOT NULL,
    [Title]         VARCHAR (MAX) NOT NULL,
    [Details]       VARCHAR (MAX) NULL,
    [StartTime]     DATETIME      NOT NULL,
    [IsAllDay]      BIT           NOT NULL,
    [EndTime]       DATETIME      NULL,
    [Place]         VARCHAR (MAX) NULL,
    [CreationTime]  DATETIME      NOT NULL,
    [IsPublic]      BIT           NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Appointments_Account] FOREIGN KEY ([CreaterUserId]) REFERENCES [dbo].[Account] ([Id])
);

