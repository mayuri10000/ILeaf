/*  BaseService.cs  业务逻辑层基类
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

using ILeaf.Core.Enums;
using ILeaf.Core.Models;
using ILeaf.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ILeaf.Service
{
    public class BaseService<T> :  IBaseService<T> where T : class, new()// global::System.Data.Objects.DataClasses.EntityObject, new()
    {
        public IBaseRepository<T> BaseRepository { get; set; }

        public BaseService(IBaseRepository<T> repo)
        {
            BaseRepository = repo;
        }


        public virtual bool IsInsert(T obj)
        {
            return BaseRepository.IsInsert(obj);
        }

        public virtual T GetObject(long id, string[] includes = null)
        {
            return BaseRepository.GetObjectById(id, includes);
        }
        

        public T GetObject(Expression<Func<T, bool>> where, string[] includes = null)
        {
            return BaseRepository.GetFirstOrDefaultObject(where, includes);
        }

        public T GetObject<TK>(Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null)
        {
            return BaseRepository.GetFirstOrDefaultObject(where, orderBy, orderingType, includes);
        }

        public PagedList<T> GetFullList<TK>(Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null)
        {
            return this.GetObjectList(0, 0, where, orderBy, orderingType, includes);
        }

        public virtual PagedList<T> GetObjectList<TK>(int pageIndex, int pageCount, Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null)
        {
            return BaseRepository.GetObjectList(where, orderBy, orderingType, pageIndex, pageCount, includes);
        }

        public virtual int GetCount(Expression<Func<T, bool>> where, string[] includes = null)
        {
            return BaseRepository.ObjectCount(where, includes);
        }

        public virtual decimal GetSum(Expression<Func<T, bool>> where, Func<T, decimal> sum, string[] includes = null)
        {
            return BaseRepository.GetSum(where, sum, includes);
        }

        /// <summary>
        /// 强制将实体设置为Modified状态
        /// </summary>
        /// <param name="obj"></param>
        public virtual void TryDetectChange(T obj)
        {
            if (!IsInsert(obj))
            {
                BaseRepository.BaseDB.BaseDataContext.Entry(obj).State = EntityState.Modified;
            }
        }

        public virtual void SaveObject(T obj)
        {
            if (BaseRepository.BaseDB.ManualDetectChangeObject)
            {
                TryDetectChange(obj);
            }
            BaseRepository.Save(obj);
        }

        public virtual void DeleteObject(long id)
        {
            T obj = this.GetObject(id, null);
            this.DeleteObject(obj);
        }
       

        public virtual void DeleteObject(T obj)
        {
            BaseRepository.Delete(obj);
        }

        public virtual void DeleteAll(IEnumerable<T> objects)
        {
            var list = objects.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                this.DeleteObject(list[i]);
            }
        }

        public virtual void CloseConnection()
        {
            BaseRepository.CloseConnection();
        }
    }
}
