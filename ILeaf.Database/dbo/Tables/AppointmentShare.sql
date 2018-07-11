CREATE TABLE [dbo].[AppointmentShare] (
    [AppointmentId]  BIGINT NOT NULL,
    [IsShareToGroup] BIT    NOT NULL,
    [ShareToId]      BIGINT NOT NULL,
    CONSTRAINT [FK_AppointmentShare_Account] FOREIGN KEY ([ShareToId]) REFERENCES [dbo].[Account] ([Id]),
    CONSTRAINT [FK_AppointmentShare_Appointments] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointments] ([Id]),
    CONSTRAINT [FK_AppointmentShare_Groups] FOREIGN KEY ([ShareToId]) REFERENCES [dbo].[Groups] ([Id])
);

