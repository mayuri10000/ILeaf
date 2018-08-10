using ILeaf.Core.Models;
using ILeaf.Repository;
using System.Collections.Generic;

namespace ILeaf.Service
{
    public interface ISchoolInfoService : IBaseService<SchoolInfo>
    {
        SchoolInfo GetSchoolByName(string name);
        List<SchoolInfo> GetAllSchoolsInProvince(string province);
    }

    public class SchoolInfoService:BaseService<SchoolInfo>,ISchoolInfoService
    {
        public SchoolInfoService(SchoolInfoRepository repo) : base(repo) { }

        public SchoolInfo GetSchoolByName(string name)
        {
            return GetObject(s => s.SchoolName == name);
        }

        public List<SchoolInfo> GetAllSchoolsInProvince(string province)
        {
            return GetObjectList(0, 0, s => s.Province == province, s => s.SchoolId, Core.Enums.OrderingType.Ascending);
        }
    }
}
