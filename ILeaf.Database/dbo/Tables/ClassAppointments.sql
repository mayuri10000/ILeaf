CREATE TABLE [dbo].[ClassAppointments] (
    [Appointments_Id] BIGINT     NOT NULL,
    [Classes_Id]      BIGINT     NOT NULL,
    [hf]              NCHAR (10) NULL,
    CONSTRAINT [PK_ClassAppointments] PRIMARY KEY CLUSTERED ([Appointments_Id] ASC, [Classes_Id] ASC),
    CONSTRAINT [FK_ClassAppointments_Appointments] FOREIGN KEY ([Appointments_Id]) REFERENCES [dbo].[Appointments] ([Id]),
    CONSTRAINT [FK_ClassAppointments_ClassInfos] FOREIGN KEY ([Classes_Id]) REFERENCES [dbo].[ClassInfos] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ClassAppointments_ClassInfos]
    ON [dbo].[ClassAppointments]([Classes_Id] ASC);

