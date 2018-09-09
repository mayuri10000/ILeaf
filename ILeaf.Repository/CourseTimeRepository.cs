using ILeaf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILeaf.Repository
{
    public interface ICourseTimeRepository : IBaseRepository<CourseTime>
    {
    }

    public class CourseTimeRepository :BaseRepository<CourseTime> , ICourseTimeRepository
    {
    }
}
