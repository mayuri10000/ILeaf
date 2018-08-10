using ILeaf.Core.Models;
using ILeaf.Repository;
using System;

namespace ILeaf.Service
{
    public interface IClassInfoService : IBaseService<ClassInfo>
    {
        ClassInfo GetClassByName(string name, long schoolId);
        bool CheckIfClassNameExisted(string name, long schoolId);
    }

    public class ClassInfoService : BaseService<ClassInfo>,IClassInfoService
    {
        public ClassInfoService(ClassInfoRepository repo) : base(repo)
        {
        }

        public ClassInfo GetClassByName(string name,long schoolId)
        {
            return GetObject(c => c.SchoolId == schoolId && c.ClassName == name);
        }

        public bool CheckIfClassNameExisted(string name,long schoolId)
        {
            return GetClassByName(name, schoolId) != null;
        }

        public override void SaveObject(ClassInfo obj)
        {
            if (BaseRepository.IsInsert(obj) && CheckIfClassNameExisted(obj.ClassName, obj.SchoolId))
                throw new Exception("当前学校已存在该班级，请勿重复添加！");
            base.SaveObject(obj);
        }
    }
}
