using System;
using System.Collections.Generic;
using System.Text;
using Trestle.Core.Entities;

namespace Trestle.Core.Data
{
    public class BusinessEntityFactory
    {
        public static BaseEntity CreateObject(System.Type type)
        {
            return (BaseEntity)Activator.CreateInstance(type);
        }

        public static BaseEntity CreateObject(System.Type type, IEntityMap map, System.Data.IDataReader data)
        {
            BaseEntity obj = (BaseEntity)Activator.CreateInstance(type);
            map.MapData(obj, data);
            return obj;
        }

        public static List<BaseEntity> CreateArray(System.Type type)
        {
            return new List<BaseEntity>();
        }

        public static List<BaseEntity> CreateArray(System.Type type, IEntityMap map, System.Data.IDataReader data)
        {

            List<BaseEntity> list = new List<BaseEntity>();

            while (data.Read())
            {
                list.Add(BusinessEntityFactory.CreateObject(type, map, data));
            }

            return list;
        }
    }
}
