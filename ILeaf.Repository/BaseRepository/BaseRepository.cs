/*  BaseRepository.cs  数据访问层基类
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

using ILeaf.Core.Enums;
using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ILeaf.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new() 
    {
        protected string _entitySetName;

        public IDbContextWrapper BaseDB { get; set; }

        public BaseRepository() : this(new DbContextWrapper()) { }
        public BaseRepository(IDbContextWrapper db)
        {
            BaseDB = db;
            _entitySetName = EntitySetKeys.Keys[typeof(T)];
        }
        


        #region IBaseRepository<T> 成员
        public virtual bool IsInsert(T obj)
        {
            var entry = BaseDB.BaseDataContext.Entry(obj);
            return entry.State == EntityState.Added || entry.State == EntityState.Detached; 
        }

        public virtual IQueryable<T> GeAll<TK>(Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null)
        {
            string sql = string.Format("SELECT VALUE c FROM {0} AS c ", _entitySetName);
            return BaseDB.BaseDataContext.Set<T>()
                        .Includes(includes)
                        .OrderBy(orderBy, orderingType).AsQueryable();
        }

        public virtual PagedList<T> GetObjectList<TK>(Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, int pageIndex, int pageCount, string[] includes = null)
        {
            string sql = string.Format("SELECT VALUE c FROM {0} AS c ", _entitySetName);
            int skipCount = PagedList<T>.GetSkipRecord(pageIndex, pageCount);
            int totalCount = -1;
            List<T> result = null;
            
            IQueryable<T> resultList = BaseDB.BaseDataContext
                                        .Set<T>()
                                        .Includes(includes)
                                        .Where(where)
                                        .OrderBy(orderBy, orderingType);
            if (pageCount > 0 && pageIndex > 0)
            {
                resultList = resultList.Skip(skipCount).Take(pageCount);
                totalCount = this.ObjectCount(where, null); 
            }
            
            
            result = resultList.ToList();
            
            
            PagedList<T> list = new PagedList<T>(result, pageIndex, pageCount, totalCount, skipCount);
            return list;
        }

        public virtual T GetFirstOrDefaultObject(Expression<Func<T, bool>> where, string[] includes = null)
        {
            string sql = string.Format("SELECT VALUE c FROM {0} AS c ", _entitySetName);
            return BaseDB.BaseDataContext
                 .Set<T>()
                 .Includes(includes).FirstOrDefault(where);
        }

        public virtual T GetFirstOrDefaultObject<TK>(Expression<Func<T, bool>> where, Expression<Func<T, TK>> orderBy, OrderingType orderingType, string[] includes = null)
        {
            string sql = string.Format("SELECT VALUE c FROM {0} AS c ", _entitySetName);
            return BaseDB.BaseDataContext
                 .Set<T>()
                 .Includes(includes).Where(where).OrderBy(orderBy, orderingType).FirstOrDefault();
        }

        public virtual T GetObjectById(long id, string[] includes)
        {
            T obj = BaseDB.BaseDataContext.Set<T>().Find(id);
            return obj;
        }
        
        public virtual int ObjectCount(Expression<Func<T, bool>> where, string[] includes = null)
        {
            string sql = string.Format("SELECT VALUE c FROM {0} AS c ", _entitySetName);
            int count = 0;
            var query = BaseDB.BaseDataContext
                 .Set<T>()
                 .Includes(includes);
                  count = query.Count(where);
            
            
            return count;
        }

        public virtual decimal GetSum(Expression<Func<T, bool>> where, Func<T, decimal> sum, string[] includes = null)
        {
            string sql = string.Format("SELECT VALUE c FROM {0} AS c ", _entitySetName);
            var result = BaseDB.BaseDataContext
                 .Set<T>()
                 .Includes(includes).Where(where).Sum(sum);
            return result;
        }

        public virtual void Add(T obj)
        {
            BaseDB.BaseDataContext.Set<T>().Add(obj);
            this.SaveChanges();
        }

        public virtual void Update(T obj)
        {
            this.SaveChanges();
        }

        public virtual void Save(T obj)
        {
            if (this.IsInsert(obj))
            {
                this.Add(obj);
            }
            else
            {
                this.Update(obj);
            }
        }

        public virtual void SaveChanges()
        {
            BaseDB.BaseDataContext.SaveChanges();//TODO: SaveOptions.
        }

        public virtual void Delete(T obj)
        {
            BaseDB.BaseDataContext.Set<T>().Remove(obj);
            this.SaveChanges();
        }


        public void CloseConnection()
        {
            BaseDB.CloseConnection();
        }
        

        #endregion
    }
}
