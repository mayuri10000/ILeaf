CREATE TABLE [dbo].[GroupAppointments] (
    [AppointmentId] BIGINT NOT NULL,
    [GroupId]       BIGINT NOT NULL,
    CONSTRAINT [PK_GroupAppointments] PRIMARY KEY CLUSTERED ([AppointmentId] ASC, [GroupId] ASC),
    CONSTRAINT [FK_GroupAppointments_Appointments] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointments] ([Id]),
    CONSTRAINT [FK_GroupAppointments_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);

