/*  IBaseRepository.cs  数据访问层基类接口
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

using ILeaf.Core.Models;
using ILeaf.Core.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ILeaf.Repository
{
    public interface IBaseRepository<T>  where T : class, new()// global::System.Data.Objects.DataClasses.EntityObject, new()
    {
        IDbContextWrapper BaseDB { get; set; }
        bool IsInsert(T obj);
        IQueryable<T> GeAll<TK>(Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null);
        T GetObjectById(long id, string[] includes = null);
        //T GetObjectByGuid(Guid guid, string[] includes = null);
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="where">搜索条件</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount">当pageCount小于等于0时不分页</param>
        /// <returns></returns>
        PagedList<T> GetObjectList<TK>(Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, int pageIndex, int pageCount, string[] includes = null);
        T GetFirstOrDefaultObject(Expression<Func<T, bool>> where, string[] includes = null);
        T GetFirstOrDefaultObject<TK>(Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null);
        int ObjectCount(Expression<Func<T, bool>> where, string[] includes = null);
        decimal GetSum(Expression<Func<T, bool>> where, Func<T, decimal> sum, string[] includes = null);
        //object ObjectSum(Expression<Func<T, bool>> where, Func<T, object> sumBy, string[] includes);
        void Add(T obj);
        void Update(T obj);
        /// <summary>
        /// 此方法会自动判断应当执行更新(Update)还是添加(Add)
        /// </summary>
        /// <param name="obj"></param>
        void Save(T obj);
        void Delete(T obj);
        //void DeleteAll(IEnumerable<T> objs);
        void SaveChanges();
        void CloseConnection();
    }
}
