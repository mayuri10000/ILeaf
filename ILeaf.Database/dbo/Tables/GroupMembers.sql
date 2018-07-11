CREATE TABLE [dbo].[GroupMembers] (
    [GroupId]       BIGINT NOT NULL,
    [IsMemberGroup] BIT    NOT NULL,
    [MemberId]      BIGINT NOT NULL,
    CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id]),
    CONSTRAINT [FK_GroupMembers_Groups1] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Groups] ([Id])
);

