CREATE TABLE [dbo].[GroupMembers] (
    [GroupId]    BIGINT NOT NULL,
    [MemberId]   BIGINT NOT NULL,
    [IsAccepted] BIT    NOT NULL,
    CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED ([GroupId] ASC, [MemberId] ASC),
    CONSTRAINT [FK_GroupMembers_Accounts] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_GroupMembers_Accounts]
    ON [dbo].[GroupMembers]([MemberId] ASC);

