/*  IBaseService.cs  业务逻辑层基类接口
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

using ILeaf.Core.Enums;
using ILeaf.Core.Models;
using ILeaf.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ILeaf.Service
{
    public interface IBaseService<T>  where T : class, new()// global::System.Data.Objects.DataClasses.EntityObject, new()
    {
        IBaseRepository<T> BaseRepository { get; set; }
        bool IsInsert(T obj);
        T GetObject(long id, string[] includes = null);
        T GetObject(Expression<Func<T, bool>> where, string[] includes = null);
        T GetObject<TK>(Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null);
        PagedList<T> GetFullList<TK>(Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null);
        PagedList<T> GetObjectList<TK>(int pageIndex, int pageCount, Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null);
        int GetCount(Expression<Func<T, bool>> where, string[] includes = null);
        decimal GetSum(Expression<Func<T, bool>> where, Func<T, decimal> sum, string[] includes = null);
        /// <summary>
        /// 强制将实体设置为Modified状态
        /// </summary>
        /// <param name="obj"></param>
        void TryDetectChange(T obj);
        void SaveObject(T obj);
        void DeleteObject(long id);
        void DeleteObject(T obj);
        void DeleteAll(IEnumerable<T> objects);
        void CloseConnection();
    }
}
