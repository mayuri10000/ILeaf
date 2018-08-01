CREATE TABLE [dbo].[GroupMembers] (
    [GroupId]  BIGINT NOT NULL,
    [MemberId] BIGINT NOT NULL,
    CONSTRAINT [FK_GroupMembers_Accounts] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);

