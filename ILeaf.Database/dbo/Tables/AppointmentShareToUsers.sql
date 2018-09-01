CREATE TABLE [dbo].[AppointmentShareToUsers] (
    [AppointmentId] BIGINT NOT NULL,
    [UserId]        BIGINT NOT NULL,
    [IsAccepted]    BIT    NOT NULL,
    CONSTRAINT [PK_AppointmentShareToUsers] PRIMARY KEY CLUSTERED ([AppointmentId] ASC, [UserId] ASC),
    CONSTRAINT [FK_AppointmentShareToUsers_Accounts] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_AppointmentShareToUsers_Appointments] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointments] ([Id])
);

