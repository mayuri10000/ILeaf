/*  Enums.cs  项目中用到的枚举类型
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月8日
 */
namespace ILeaf.Core.Enums
{
    /// <summary>
    /// 聊天消息类型
    /// </summary>
    public enum ChatMessageType
    {
        Text,
        Image,
        File,
        Audio,
        Link
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        Undefined,
        Male,
        Female
    }

    public enum UserType
    {
        Unregistered,
        Uncomplete,
        Student,
        Teacher
    }

    public enum AppointmentVisiblity
    {
        Public,
        ClassmatesGroupmatesAndFriends,
        GroupmatesAndFriends,
        FriendsOnly,
        Private
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum OrderingType
    {
        Ascending,
        Descending
    }

    /// <summary>
    /// 群组类型
    /// </summary>
    public enum GroupType
    {
        StudyGroup,
        TeacherGroup,
        Class,
        ClassGroup,
        School
    }

    /// <summary>
    /// 课程变动消息类型
    /// </summary>
    public enum CourseChangeType
    {
        Cancelled,
        TimeModified,
        DateModified,
        TeacherChanged,
        ClassroomChanged,
    }
}
