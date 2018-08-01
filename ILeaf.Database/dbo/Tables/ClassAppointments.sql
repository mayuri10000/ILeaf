CREATE TABLE [dbo].[ClassAppointments] (
    [AppointmentId] BIGINT NOT NULL,
    [ClassId]       BIGINT NOT NULL,
    CONSTRAINT [FK_ClassAppointments_Appointments] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointments] ([Id]),
    CONSTRAINT [FK_ClassAppointments_ClassInfos] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[ClassInfos] ([Id])
);

