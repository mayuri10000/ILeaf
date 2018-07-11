/*  Entities.cs  数据库模型的扩展构造函数，可以通过连接字符串构造数据库模型对象
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月8日
 */

namespace ILeaf.Core.Models
{
    public partial class Entities
    {
        public Entities(string nameOrConnectionString) : base(nameOrConnectionString) { }
    }
}