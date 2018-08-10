/*  EntitySetKeys.cs  与ORM实体类对应的实体集
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILeaf.Core.Models
{
    public static class EntitySetKeys
    {
        public static EntitySetKeysDictionary Keys = new EntitySetKeysDictionary();
    }
    /// <summary>
    /// 与ORM实体类对应的实体集
    /// </summary>
    public class EntitySetKeysDictionary : Dictionary<Type, string>
    {
        public EntitySetKeysDictionary()
        {
            //初始化的时候从ORM中自动读取实体集名称及实体类别名称
            var clientProperties = typeof(ILeafEntities).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            var properities = new List<PropertyInfo>();
            properities.AddRange(clientProperties);

            foreach (var prop in properities)
            {
                try
                {
                    if (prop.PropertyType.Name.IndexOf("DbSet") != -1 && prop.PropertyType.GetGenericArguments().Length > 0)
                    {
                        this[prop.PropertyType.GetGenericArguments()[0]] = prop.Name;
                    }
                }
                catch { }
            }

        }

        new public string this[Type entityType]
        {
            get
            {
                if (!base.ContainsKey(entityType))
                {
                    throw new Exception("未找到实体类型");
                }
                return base[entityType];
            }
            set
            {
                base[entityType] = value;
            }
        }
    }
}
