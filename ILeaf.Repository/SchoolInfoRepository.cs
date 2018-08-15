using ILeaf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILeaf.Repository
{
    public interface ISchoolInfoRepository : IBaseRepository<SchoolInfo>
    {
    }

    public class SchoolInfoRepository : BaseRepository<SchoolInfo>
    {
    }
}
