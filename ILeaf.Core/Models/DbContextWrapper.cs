/*  DbContextWrapper.cs  数据库对象的包装对象
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月8日
 */

using ILeaf.Core.Config;
using StructureMap;
using System.Data.Entity;

namespace ILeaf.Core.Models
{
    [PluginFamily("ClientDatabase", Scope = InstanceScope.Hybrid)]
    public interface IDbContextWrapper
    {
        /// <summary>
        /// 强制手动更改DetectChange
        /// </summary>
        bool ManualDetectChangeObject { get; set; }
        DbContext BaseDataContext { get; }
        ILeafEntities DataContext { get; }
        void CloseConnection();
    }

    [Pluggable("ClientDatabase")]
    public class DbContextWrapper : IDbContextWrapper
    {
        private ILeafEntities dataContext;
        private string lastTempDomainName = null;

        public bool ManualDetectChangeObject { get; set; }

        public DbContext BaseDataContext
        {
            get
            {
                if (dataContext == null)
                {
                    string provider = "System.Data.SqlClient";
                    var connectionString = string.Format(@"metadata=res://*/Models.ILeaf.csdl|res://*/Models.ILeaf.ssdl|res://*/Models.ILeaf.msl;provider={0};provider connection string='{1}'", provider, SiteConfig.DbConnectionString);
                    // metadata=res://*/Models.ILeaf.csdl|res://*/Models.ILeaf.ssdl|res://*/Models.ILeaf.msl;provider=System.Data.SqlClient;provider connection string="data source=DESKTOP-2LENOA4;initial catalog=ILeaf;user id=sa;password=***********;MultipleActiveResultSets=True;App=EntityFramework"
                    dataContext = new ILeafEntities(connectionString);
                }
                return dataContext;
            }
        }

        public ILeafEntities DataContext => BaseDataContext as ILeafEntities;

        public void CloseConnection()
        {
            BaseDataContext.Database.Connection.Close();
            dataContext = null;
        }
    }
}
