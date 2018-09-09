CREATE TABLE [dbo].[GroupAppointments] (
    [Appointments_Id] BIGINT     NOT NULL,
    [Groups_Id]       BIGINT     NOT NULL,
    [hf]              NCHAR (10) NULL,
    CONSTRAINT [PK_GroupAppointments] PRIMARY KEY CLUSTERED ([Appointments_Id] ASC, [Groups_Id] ASC),
    CONSTRAINT [FK_GroupAppointments_Appointments] FOREIGN KEY ([Appointments_Id]) REFERENCES [dbo].[Appointments] ([Id]),
    CONSTRAINT [FK_GroupAppointments_Groups] FOREIGN KEY ([Groups_Id]) REFERENCES [dbo].[Groups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_GroupAppointments_Groups]
    ON [dbo].[GroupAppointments]([Groups_Id] ASC);

