
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/09/2018 19:53:36
-- Generated from EDMX file: D:\Documents\ILeaf\ILeaf.Core\Models\ILeaf.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ILeaf2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Accounts_ClassInfos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_ClassInfos];
GO
IF OBJECT_ID(N'[dbo].[FK_Accounts_SchoolInfos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accounts] DROP CONSTRAINT [FK_Accounts_SchoolInfos];
GO
IF OBJECT_ID(N'[dbo].[FK_Appointments_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_Appointments_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_AppointmentShareToUsers_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AppointmentShareToUsers] DROP CONSTRAINT [FK_AppointmentShareToUsers_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_AppointmentShareToUsers_Appointments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AppointmentShareToUsers] DROP CONSTRAINT [FK_AppointmentShareToUsers_Appointments];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentAccount_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttachmentAccount] DROP CONSTRAINT [FK_AttachmentAccount_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentAccount_Attachments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttachmentAccount] DROP CONSTRAINT [FK_AttachmentAccount_Attachments];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentClass_Attachments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttachmentClass] DROP CONSTRAINT [FK_AttachmentClass_Attachments];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentClass_ClassInfos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttachmentClass] DROP CONSTRAINT [FK_AttachmentClass_ClassInfos];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentCourse_Attachments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttachmentCourses] DROP CONSTRAINT [FK_AttachmentCourse_Attachments];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentCourse_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttachmentCourses] DROP CONSTRAINT [FK_AttachmentCourse_Courses];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentGroup_Attachments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttachmentGroup] DROP CONSTRAINT [FK_AttachmentGroup_Attachments];
GO
IF OBJECT_ID(N'[dbo].[FK_AttachmentGroup_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AttachmentGroup] DROP CONSTRAINT [FK_AttachmentGroup_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_Attachments_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attachments] DROP CONSTRAINT [FK_Attachments_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatMessages_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatMessages] DROP CONSTRAINT [FK_ChatMessages_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatMessages_Accounts1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatMessages] DROP CONSTRAINT [FK_ChatMessages_Accounts1];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatMessages_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatMessages] DROP CONSTRAINT [FK_ChatMessages_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassAppointments_Appointments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClassAppointments] DROP CONSTRAINT [FK_ClassAppointments_Appointments];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassAppointments_ClassInfos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClassAppointments] DROP CONSTRAINT [FK_ClassAppointments_ClassInfos];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassInfos_SchoolInfos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClassInfos] DROP CONSTRAINT [FK_ClassInfos_SchoolInfos];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseChange_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseChanges] DROP CONSTRAINT [FK_CourseChange_Courses];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseClass_ClassInfos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseClass] DROP CONSTRAINT [FK_CourseClass_ClassInfos];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseClass_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseClass] DROP CONSTRAINT [FK_CourseClass_Courses];
GO
IF OBJECT_ID(N'[dbo].[FK_Courses_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_Courses_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_Courses_SchoolInfos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_Courses_SchoolInfos];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseTime_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseTime] DROP CONSTRAINT [FK_CourseTime_Courses];
GO
IF OBJECT_ID(N'[dbo].[FK_Friendship_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Friendships] DROP CONSTRAINT [FK_Friendship_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_Friendship_Accounts1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Friendships] DROP CONSTRAINT [FK_Friendship_Accounts1];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupAppointments_Appointments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupAppointments] DROP CONSTRAINT [FK_GroupAppointments_Appointments];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupAppointments_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupAppointments] DROP CONSTRAINT [FK_GroupAppointments_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupMembers_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupMembers] DROP CONSTRAINT [FK_GroupMembers_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupMembers_Groups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupMembers] DROP CONSTRAINT [FK_GroupMembers_Groups];
GO
IF OBJECT_ID(N'[dbo].[FK_Groups_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_Groups_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_SelectableCourseStudents_Accounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SelectableCourseStudents] DROP CONSTRAINT [FK_SelectableCourseStudents_Accounts];
GO
IF OBJECT_ID(N'[dbo].[FK_SelectableCourseStudents_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SelectableCourseStudents] DROP CONSTRAINT [FK_SelectableCourseStudents_Courses];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Appointments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appointments];
GO
IF OBJECT_ID(N'[dbo].[AppointmentShareToUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AppointmentShareToUsers];
GO
IF OBJECT_ID(N'[dbo].[AttachmentAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AttachmentAccount];
GO
IF OBJECT_ID(N'[dbo].[AttachmentClass]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AttachmentClass];
GO
IF OBJECT_ID(N'[dbo].[AttachmentCourses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AttachmentCourses];
GO
IF OBJECT_ID(N'[dbo].[AttachmentGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AttachmentGroup];
GO
IF OBJECT_ID(N'[dbo].[Attachments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attachments];
GO
IF OBJECT_ID(N'[dbo].[ChatMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatMessages];
GO
IF OBJECT_ID(N'[dbo].[ClassAppointments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassAppointments];
GO
IF OBJECT_ID(N'[dbo].[ClassInfos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassInfos];
GO
IF OBJECT_ID(N'[dbo].[CourseChanges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseChanges];
GO
IF OBJECT_ID(N'[dbo].[CourseClass]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseClass];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[CourseTime]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseTime];
GO
IF OBJECT_ID(N'[dbo].[Friendships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Friendships];
GO
IF OBJECT_ID(N'[dbo].[GroupAppointments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupAppointments];
GO
IF OBJECT_ID(N'[dbo].[GroupMembers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupMembers];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO
IF OBJECT_ID(N'[dbo].[SchoolInfos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SchoolInfos];
GO
IF OBJECT_ID(N'[dbo].[SelectableCourseStudents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SelectableCourseStudents];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UserName] varchar(50)  NOT NULL,
    [RealName] varchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [WeChatOpenId] varchar(50)  NULL,
    [EncryptedPassword] varchar(100)  NULL,
    [PasswordSalt] varchar(50)  NULL,
    [Gender] tinyint  NOT NULL,
    [UserType] tinyint  NOT NULL,
    [IsAdmin] bit  NOT NULL,
    [HeadImgUrl] varchar(100)  NULL,
    [SchoolId] int  NULL,
    [ClassId] bigint  NULL,
    [SchoolCardNum] varchar(50)  NULL,
    [RegistionTime] datetime  NOT NULL,
    [ThisLoginTime] datetime  NULL,
    [LastLoginTime] datetime  NULL,
    [ThisLoginIP] varchar(15)  NULL,
    [LastLoginIP] varchar(15)  NULL
);
GO

-- Creating table 'Appointments'
CREATE TABLE [dbo].[Appointments] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NOT NULL,
    [CreatorId] bigint  NOT NULL,
    [Details] varchar(max)  NULL,
    [Place] varchar(50)  NULL,
    [CreationTime] datetime  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [IsAllDay] bit  NOT NULL,
    [EndTime] datetime  NULL,
    [Visibily] tinyint  NOT NULL
);
GO

-- Creating table 'AppointmentShareToUsers'
CREATE TABLE [dbo].[AppointmentShareToUsers] (
    [AppointmentId] bigint  NOT NULL,
    [UserId] bigint  NOT NULL,
    [IsAccepted] bit  NOT NULL
);
GO

-- Creating table 'AttachmentAccounts'
CREATE TABLE [dbo].[AttachmentAccounts] (
    [AccessableUsers_Id] bigint  NOT NULL,
    [AccessableAttachments_Id] bigint  NOT NULL,
    [hf] nchar(10)  NULL
);
GO

-- Creating table 'AttachmentClasses'
CREATE TABLE [dbo].[AttachmentClasses] (
    [AccessableAttachments_Id] bigint  NOT NULL,
    [AccessableClasses_Id] bigint  NOT NULL,
    [hf] nchar(10)  NULL
);
GO

-- Creating table 'AttachmentCourses'
CREATE TABLE [dbo].[AttachmentCourses] (
    [AttachmentId] bigint  NOT NULL,
    [CourseId] bigint  NOT NULL,
    [CourseTime] datetime  NOT NULL
);
GO

-- Creating table 'AttachmentGroups'
CREATE TABLE [dbo].[AttachmentGroups] (
    [AccessableAttachments_Id] bigint  NOT NULL,
    [AccessableGroups_Id] bigint  NOT NULL,
    [hf] nchar(10)  NULL
);
GO

-- Creating table 'Attachments'
CREATE TABLE [dbo].[Attachments] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [StoragePath] varchar(150)  NULL,
    [FileName] varchar(50)  NOT NULL,
    [FileSize] bigint  NULL,
    [MD5Hash] nvarchar(32)  NULL,
    [IsPublicAttachment] bit  NOT NULL,
    [UploaderId] bigint  NOT NULL,
    [UploadTime] datetime  NOT NULL,
    [ExpireTime] datetime  NOT NULL
);
GO

-- Creating table 'ChatMessages'
CREATE TABLE [dbo].[ChatMessages] (
    [Id] bigint  NOT NULL,
    [MessageBody] varchar(max)  NOT NULL,
    [MessageType] tinyint  NOT NULL,
    [SendTime] datetime  NOT NULL,
    [SenderId] bigint  NOT NULL,
    [RecipientId] bigint  NULL,
    [GroupId] bigint  NULL
);
GO

-- Creating table 'ClassAppointments'
CREATE TABLE [dbo].[ClassAppointments] (
    [Appointments_Id] bigint  NOT NULL,
    [Classes_Id] bigint  NOT NULL,
    [hf] nchar(10)  NULL
);
GO

-- Creating table 'ClassInfos'
CREATE TABLE [dbo].[ClassInfos] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [SchoolId] int  NOT NULL,
    [ClassName] varchar(50)  NULL,
    [Year] smallint  NOT NULL,
    [Major] varchar(50)  NULL,
    [InstructorId] bigint  NULL
);
GO

-- Creating table 'CourseChanges1'
CREATE TABLE [dbo].[CourseChanges1] (
    [CourseId] bigint  NOT NULL,
    [CourseTime] datetime  NOT NULL,
    [ChangeType] tinyint  NOT NULL,
    [ChangedValue] varchar(50)  NOT NULL
);
GO

-- Creating table 'CourseClasses'
CREATE TABLE [dbo].[CourseClasses] (
    [Classes_Id] bigint  NOT NULL,
    [Courses_Id] bigint  NOT NULL,
    [hs] nchar(10)  NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NOT NULL,
    [TeacherId] bigint  NOT NULL,
    [SchoolId] int  NOT NULL,
    [IsSelectableCourse] bit  NOT NULL,
    [SemesterStart] datetime  NOT NULL,
    [Weeks] binary(50)  NOT NULL
);
GO

-- Creating table 'CourseTimes'
CREATE TABLE [dbo].[CourseTimes] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [CourseId] bigint  NOT NULL,
    [Weekday] tinyint  NOT NULL,
    [Classroom] varchar(50)  NOT NULL,
    [StartTime] time  NOT NULL,
    [EndTime] time  NOT NULL
);
GO

-- Creating table 'Friendships'
CREATE TABLE [dbo].[Friendships] (
    [Account1] bigint  NOT NULL,
    [Account2] bigint  NOT NULL,
    [IsAccepted] bit  NOT NULL
);
GO

-- Creating table 'GroupAppointments'
CREATE TABLE [dbo].[GroupAppointments] (
    [Appointments_Id] bigint  NOT NULL,
    [Groups_Id] bigint  NOT NULL,
    [hf] nchar(10)  NULL
);
GO

-- Creating table 'GroupMembers'
CREATE TABLE [dbo].[GroupMembers] (
    [GroupId] bigint  NOT NULL,
    [MemberId] bigint  NOT NULL,
    [IsAccepted] bit  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [HeadmanId] bigint  NOT NULL,
    [CreationTime] datetime  NOT NULL,
    [Announcement] varchar(max)  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NOT NULL,
    [Text] varchar(max)  NULL,
    [SentTime] datetime  NOT NULL,
    [SenderId] bigint  NULL,
    [RecipientId] bigint  NULL,
    [GroupId] bigint  NULL,
    [Section] varchar(15)  NULL,
    [Level] tinyint  NOT NULL
);
GO

-- Creating table 'SchoolInfos'
CREATE TABLE [dbo].[SchoolInfos] (
    [SchoolId] int  NOT NULL,
    [SchoolName] varchar(50)  NOT NULL,
    [Province] varchar(10)  NOT NULL
);
GO

-- Creating table 'SelectableCourseStudents'
CREATE TABLE [dbo].[SelectableCourseStudents] (
    [SelectedStudent_Id] bigint  NOT NULL,
    [SelectedCourses_Id] bigint  NOT NULL,
    [hf] nchar(10)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [PK_Appointments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AppointmentId], [UserId] in table 'AppointmentShareToUsers'
ALTER TABLE [dbo].[AppointmentShareToUsers]
ADD CONSTRAINT [PK_AppointmentShareToUsers]
    PRIMARY KEY CLUSTERED ([AppointmentId], [UserId] ASC);
GO

-- Creating primary key on [AccessableUsers_Id], [AccessableAttachments_Id] in table 'AttachmentAccounts'
ALTER TABLE [dbo].[AttachmentAccounts]
ADD CONSTRAINT [PK_AttachmentAccounts]
    PRIMARY KEY CLUSTERED ([AccessableUsers_Id], [AccessableAttachments_Id] ASC);
GO

-- Creating primary key on [AccessableAttachments_Id], [AccessableClasses_Id] in table 'AttachmentClasses'
ALTER TABLE [dbo].[AttachmentClasses]
ADD CONSTRAINT [PK_AttachmentClasses]
    PRIMARY KEY CLUSTERED ([AccessableAttachments_Id], [AccessableClasses_Id] ASC);
GO

-- Creating primary key on [AttachmentId], [CourseId], [CourseTime] in table 'AttachmentCourses'
ALTER TABLE [dbo].[AttachmentCourses]
ADD CONSTRAINT [PK_AttachmentCourses]
    PRIMARY KEY CLUSTERED ([AttachmentId], [CourseId], [CourseTime] ASC);
GO

-- Creating primary key on [AccessableAttachments_Id], [AccessableGroups_Id] in table 'AttachmentGroups'
ALTER TABLE [dbo].[AttachmentGroups]
ADD CONSTRAINT [PK_AttachmentGroups]
    PRIMARY KEY CLUSTERED ([AccessableAttachments_Id], [AccessableGroups_Id] ASC);
GO

-- Creating primary key on [Id] in table 'Attachments'
ALTER TABLE [dbo].[Attachments]
ADD CONSTRAINT [PK_Attachments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChatMessages'
ALTER TABLE [dbo].[ChatMessages]
ADD CONSTRAINT [PK_ChatMessages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Appointments_Id], [Classes_Id] in table 'ClassAppointments'
ALTER TABLE [dbo].[ClassAppointments]
ADD CONSTRAINT [PK_ClassAppointments]
    PRIMARY KEY CLUSTERED ([Appointments_Id], [Classes_Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClassInfos'
ALTER TABLE [dbo].[ClassInfos]
ADD CONSTRAINT [PK_ClassInfos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [CourseId], [CourseTime], [ChangeType], [ChangedValue] in table 'CourseChanges1'
ALTER TABLE [dbo].[CourseChanges1]
ADD CONSTRAINT [PK_CourseChanges1]
    PRIMARY KEY CLUSTERED ([CourseId], [CourseTime], [ChangeType], [ChangedValue] ASC);
GO

-- Creating primary key on [Classes_Id], [Courses_Id] in table 'CourseClasses'
ALTER TABLE [dbo].[CourseClasses]
ADD CONSTRAINT [PK_CourseClasses]
    PRIMARY KEY CLUSTERED ([Classes_Id], [Courses_Id] ASC);
GO

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CourseTimes'
ALTER TABLE [dbo].[CourseTimes]
ADD CONSTRAINT [PK_CourseTimes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Account1], [Account2] in table 'Friendships'
ALTER TABLE [dbo].[Friendships]
ADD CONSTRAINT [PK_Friendships]
    PRIMARY KEY CLUSTERED ([Account1], [Account2] ASC);
GO

-- Creating primary key on [Appointments_Id], [Groups_Id] in table 'GroupAppointments'
ALTER TABLE [dbo].[GroupAppointments]
ADD CONSTRAINT [PK_GroupAppointments]
    PRIMARY KEY CLUSTERED ([Appointments_Id], [Groups_Id] ASC);
GO

-- Creating primary key on [GroupId], [MemberId] in table 'GroupMembers'
ALTER TABLE [dbo].[GroupMembers]
ADD CONSTRAINT [PK_GroupMembers]
    PRIMARY KEY CLUSTERED ([GroupId], [MemberId] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SchoolId] in table 'SchoolInfos'
ALTER TABLE [dbo].[SchoolInfos]
ADD CONSTRAINT [PK_SchoolInfos]
    PRIMARY KEY CLUSTERED ([SchoolId] ASC);
GO

-- Creating primary key on [SelectedStudent_Id], [SelectedCourses_Id] in table 'SelectableCourseStudents'
ALTER TABLE [dbo].[SelectableCourseStudents]
ADD CONSTRAINT [PK_SelectableCourseStudents]
    PRIMARY KEY CLUSTERED ([SelectedStudent_Id], [SelectedCourses_Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClassId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK_Accounts_ClassInfos]
    FOREIGN KEY ([ClassId])
    REFERENCES [dbo].[ClassInfos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Accounts_ClassInfos'
CREATE INDEX [IX_FK_Accounts_ClassInfos]
ON [dbo].[Accounts]
    ([ClassId]);
GO

-- Creating foreign key on [SchoolId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [FK_Accounts_SchoolInfos]
    FOREIGN KEY ([SchoolId])
    REFERENCES [dbo].[SchoolInfos]
        ([SchoolId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Accounts_SchoolInfos'
CREATE INDEX [IX_FK_Accounts_SchoolInfos]
ON [dbo].[Accounts]
    ([SchoolId]);
GO

-- Creating foreign key on [CreatorId] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [FK_Appointments_Accounts]
    FOREIGN KEY ([CreatorId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Appointments_Accounts'
CREATE INDEX [IX_FK_Appointments_Accounts]
ON [dbo].[Appointments]
    ([CreatorId]);
GO

-- Creating foreign key on [UserId] in table 'AppointmentShareToUsers'
ALTER TABLE [dbo].[AppointmentShareToUsers]
ADD CONSTRAINT [FK_AppointmentShareToUsers_Accounts]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AppointmentShareToUsers_Accounts'
CREATE INDEX [IX_FK_AppointmentShareToUsers_Accounts]
ON [dbo].[AppointmentShareToUsers]
    ([UserId]);
GO

-- Creating foreign key on [AccessableUsers_Id] in table 'AttachmentAccounts'
ALTER TABLE [dbo].[AttachmentAccounts]
ADD CONSTRAINT [FK_AttachmentAccount_Accounts]
    FOREIGN KEY ([AccessableUsers_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UploaderId] in table 'Attachments'
ALTER TABLE [dbo].[Attachments]
ADD CONSTRAINT [FK_Attachments_Accounts]
    FOREIGN KEY ([UploaderId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Attachments_Accounts'
CREATE INDEX [IX_FK_Attachments_Accounts]
ON [dbo].[Attachments]
    ([UploaderId]);
GO

-- Creating foreign key on [SenderId] in table 'ChatMessages'
ALTER TABLE [dbo].[ChatMessages]
ADD CONSTRAINT [FK_ChatMessages_Accounts]
    FOREIGN KEY ([SenderId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMessages_Accounts'
CREATE INDEX [IX_FK_ChatMessages_Accounts]
ON [dbo].[ChatMessages]
    ([SenderId]);
GO

-- Creating foreign key on [RecipientId] in table 'ChatMessages'
ALTER TABLE [dbo].[ChatMessages]
ADD CONSTRAINT [FK_ChatMessages_Accounts1]
    FOREIGN KEY ([RecipientId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMessages_Accounts1'
CREATE INDEX [IX_FK_ChatMessages_Accounts1]
ON [dbo].[ChatMessages]
    ([RecipientId]);
GO

-- Creating foreign key on [TeacherId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_Courses_Accounts]
    FOREIGN KEY ([TeacherId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Courses_Accounts'
CREATE INDEX [IX_FK_Courses_Accounts]
ON [dbo].[Courses]
    ([TeacherId]);
GO

-- Creating foreign key on [Account1] in table 'Friendships'
ALTER TABLE [dbo].[Friendships]
ADD CONSTRAINT [FK_Friendship_Accounts]
    FOREIGN KEY ([Account1])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Account2] in table 'Friendships'
ALTER TABLE [dbo].[Friendships]
ADD CONSTRAINT [FK_Friendship_Accounts1]
    FOREIGN KEY ([Account2])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Friendship_Accounts1'
CREATE INDEX [IX_FK_Friendship_Accounts1]
ON [dbo].[Friendships]
    ([Account2]);
GO

-- Creating foreign key on [MemberId] in table 'GroupMembers'
ALTER TABLE [dbo].[GroupMembers]
ADD CONSTRAINT [FK_GroupMembers_Accounts]
    FOREIGN KEY ([MemberId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupMembers_Accounts'
CREATE INDEX [IX_FK_GroupMembers_Accounts]
ON [dbo].[GroupMembers]
    ([MemberId]);
GO

-- Creating foreign key on [HeadmanId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_Groups_Accounts]
    FOREIGN KEY ([HeadmanId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Groups_Accounts'
CREATE INDEX [IX_FK_Groups_Accounts]
ON [dbo].[Groups]
    ([HeadmanId]);
GO

-- Creating foreign key on [SelectedStudent_Id] in table 'SelectableCourseStudents'
ALTER TABLE [dbo].[SelectableCourseStudents]
ADD CONSTRAINT [FK_SelectableCourseStudents_Accounts]
    FOREIGN KEY ([SelectedStudent_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AppointmentId] in table 'AppointmentShareToUsers'
ALTER TABLE [dbo].[AppointmentShareToUsers]
ADD CONSTRAINT [FK_AppointmentShareToUsers_Appointments]
    FOREIGN KEY ([AppointmentId])
    REFERENCES [dbo].[Appointments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Appointments_Id] in table 'ClassAppointments'
ALTER TABLE [dbo].[ClassAppointments]
ADD CONSTRAINT [FK_ClassAppointments_Appointments]
    FOREIGN KEY ([Appointments_Id])
    REFERENCES [dbo].[Appointments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Appointments_Id] in table 'GroupAppointments'
ALTER TABLE [dbo].[GroupAppointments]
ADD CONSTRAINT [FK_GroupAppointments_Appointments]
    FOREIGN KEY ([Appointments_Id])
    REFERENCES [dbo].[Appointments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AccessableAttachments_Id] in table 'AttachmentAccounts'
ALTER TABLE [dbo].[AttachmentAccounts]
ADD CONSTRAINT [FK_AttachmentAccount_Attachments]
    FOREIGN KEY ([AccessableAttachments_Id])
    REFERENCES [dbo].[Attachments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AttachmentAccount_Attachments'
CREATE INDEX [IX_FK_AttachmentAccount_Attachments]
ON [dbo].[AttachmentAccounts]
    ([AccessableAttachments_Id]);
GO

-- Creating foreign key on [AccessableAttachments_Id] in table 'AttachmentClasses'
ALTER TABLE [dbo].[AttachmentClasses]
ADD CONSTRAINT [FK_AttachmentClass_Attachments]
    FOREIGN KEY ([AccessableAttachments_Id])
    REFERENCES [dbo].[Attachments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AccessableClasses_Id] in table 'AttachmentClasses'
ALTER TABLE [dbo].[AttachmentClasses]
ADD CONSTRAINT [FK_AttachmentClass_ClassInfos]
    FOREIGN KEY ([AccessableClasses_Id])
    REFERENCES [dbo].[ClassInfos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AttachmentClass_ClassInfos'
CREATE INDEX [IX_FK_AttachmentClass_ClassInfos]
ON [dbo].[AttachmentClasses]
    ([AccessableClasses_Id]);
GO

-- Creating foreign key on [AttachmentId] in table 'AttachmentCourses'
ALTER TABLE [dbo].[AttachmentCourses]
ADD CONSTRAINT [FK_AttachmentCourse_Attachments]
    FOREIGN KEY ([AttachmentId])
    REFERENCES [dbo].[Attachments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CourseId] in table 'AttachmentCourses'
ALTER TABLE [dbo].[AttachmentCourses]
ADD CONSTRAINT [FK_AttachmentCourse_Courses]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AttachmentCourse_Courses'
CREATE INDEX [IX_FK_AttachmentCourse_Courses]
ON [dbo].[AttachmentCourses]
    ([CourseId]);
GO

-- Creating foreign key on [AccessableAttachments_Id] in table 'AttachmentGroups'
ALTER TABLE [dbo].[AttachmentGroups]
ADD CONSTRAINT [FK_AttachmentGroup_Attachments]
    FOREIGN KEY ([AccessableAttachments_Id])
    REFERENCES [dbo].[Attachments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AccessableGroups_Id] in table 'AttachmentGroups'
ALTER TABLE [dbo].[AttachmentGroups]
ADD CONSTRAINT [FK_AttachmentGroup_Groups]
    FOREIGN KEY ([AccessableGroups_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AttachmentGroup_Groups'
CREATE INDEX [IX_FK_AttachmentGroup_Groups]
ON [dbo].[AttachmentGroups]
    ([AccessableGroups_Id]);
GO

-- Creating foreign key on [GroupId] in table 'ChatMessages'
ALTER TABLE [dbo].[ChatMessages]
ADD CONSTRAINT [FK_ChatMessages_Groups]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMessages_Groups'
CREATE INDEX [IX_FK_ChatMessages_Groups]
ON [dbo].[ChatMessages]
    ([GroupId]);
GO

-- Creating foreign key on [Classes_Id] in table 'ClassAppointments'
ALTER TABLE [dbo].[ClassAppointments]
ADD CONSTRAINT [FK_ClassAppointments_ClassInfos]
    FOREIGN KEY ([Classes_Id])
    REFERENCES [dbo].[ClassInfos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassAppointments_ClassInfos'
CREATE INDEX [IX_FK_ClassAppointments_ClassInfos]
ON [dbo].[ClassAppointments]
    ([Classes_Id]);
GO

-- Creating foreign key on [SchoolId] in table 'ClassInfos'
ALTER TABLE [dbo].[ClassInfos]
ADD CONSTRAINT [FK_ClassInfos_SchoolInfos]
    FOREIGN KEY ([SchoolId])
    REFERENCES [dbo].[SchoolInfos]
        ([SchoolId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassInfos_SchoolInfos'
CREATE INDEX [IX_FK_ClassInfos_SchoolInfos]
ON [dbo].[ClassInfos]
    ([SchoolId]);
GO

-- Creating foreign key on [Classes_Id] in table 'CourseClasses'
ALTER TABLE [dbo].[CourseClasses]
ADD CONSTRAINT [FK_CourseClass_ClassInfos]
    FOREIGN KEY ([Classes_Id])
    REFERENCES [dbo].[ClassInfos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CourseId] in table 'CourseChanges1'
ALTER TABLE [dbo].[CourseChanges1]
ADD CONSTRAINT [FK_CourseChange_Courses]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Courses_Id] in table 'CourseClasses'
ALTER TABLE [dbo].[CourseClasses]
ADD CONSTRAINT [FK_CourseClass_Courses]
    FOREIGN KEY ([Courses_Id])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseClass_Courses'
CREATE INDEX [IX_FK_CourseClass_Courses]
ON [dbo].[CourseClasses]
    ([Courses_Id]);
GO

-- Creating foreign key on [SchoolId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_Courses_SchoolInfos]
    FOREIGN KEY ([SchoolId])
    REFERENCES [dbo].[SchoolInfos]
        ([SchoolId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Courses_SchoolInfos'
CREATE INDEX [IX_FK_Courses_SchoolInfos]
ON [dbo].[Courses]
    ([SchoolId]);
GO

-- Creating foreign key on [CourseId] in table 'CourseTimes'
ALTER TABLE [dbo].[CourseTimes]
ADD CONSTRAINT [FK_CourseTime_Courses]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseTime_Courses'
CREATE INDEX [IX_FK_CourseTime_Courses]
ON [dbo].[CourseTimes]
    ([CourseId]);
GO

-- Creating foreign key on [SelectedCourses_Id] in table 'SelectableCourseStudents'
ALTER TABLE [dbo].[SelectableCourseStudents]
ADD CONSTRAINT [FK_SelectableCourseStudents_Courses]
    FOREIGN KEY ([SelectedCourses_Id])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SelectableCourseStudents_Courses'
CREATE INDEX [IX_FK_SelectableCourseStudents_Courses]
ON [dbo].[SelectableCourseStudents]
    ([SelectedCourses_Id]);
GO

-- Creating foreign key on [Groups_Id] in table 'GroupAppointments'
ALTER TABLE [dbo].[GroupAppointments]
ADD CONSTRAINT [FK_GroupAppointments_Groups]
    FOREIGN KEY ([Groups_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupAppointments_Groups'
CREATE INDEX [IX_FK_GroupAppointments_Groups]
ON [dbo].[GroupAppointments]
    ([Groups_Id]);
GO

-- Creating foreign key on [GroupId] in table 'GroupMembers'
ALTER TABLE [dbo].[GroupMembers]
ADD CONSTRAINT [FK_GroupMembers_Groups]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------