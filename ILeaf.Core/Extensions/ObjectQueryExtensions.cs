/*  StringExtensions.cs  数据库查询扩展方法
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月8日
 */

using ILeaf.Core.Enums;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace ILeaf.Core.Extensions
{
    public static class ObjectQueryExtensions
    {
        /// <summary>
        /// 添加多个Include()对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static DbQuery<T> Includes<T>(this DbQuery<T> obj, string[] includes) where T : class
        {
            //** 用法：entities.Users.Includes(new string[]{ "Roles","Products" }).ToList()
            if (includes == null)
                return obj;

            foreach (var item in includes)
            {
                obj = obj.Include(item);
            }
            return obj;
        }

        /// <summary>
        /// 委托排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="orderBy"></param>
        /// <param name="isAscending">是否升序排列</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T, TK>(this IQueryable<T> obj, Expression<Func<T, TK>> orderBy, OrderingType orderingType) where T : class
        {
            if (orderBy == null)
                throw new Exception("OrderBy can not be Null！");

            return orderingType == OrderingType.Ascending ? obj.OrderBy(orderBy) : obj.OrderByDescending(orderBy);
        }
    }
}
