CREATE TABLE [dbo].[GroupAppointments] (
    [AppointmentId] BIGINT NOT NULL,
    [GroupId]       BIGINT NOT NULL,
    CONSTRAINT [FK_GroupAppointments_Appointments] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointments] ([Id]),
    CONSTRAINT [FK_GroupAppointments_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);

