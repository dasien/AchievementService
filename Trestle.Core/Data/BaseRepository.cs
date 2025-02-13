using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Trestle.Core.Entities;

namespace Trestle.Core.Data
{
    public abstract class BaseRepository<T> : IRepository where T : BaseEntity
    {
        public abstract IEntity GetById(IDbConnection con, int Id);
        public abstract IEnumerable<IEntity> GetList(IDbConnection con);

        public virtual IEntity CreateEntity(IDataReader reader)
        {
            return default;
        }

        protected IEnumerable<IEntity> GetRecords(IDbCommand command)
        {
            List<IEntity> retVal = new List<IEntity>();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    retVal.Add(CreateEntity(reader));
                }
            }

            return retVal;
        }

        protected IEntity GetRecord(IDbCommand command)
        {
            IEntity retVal = null;

            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    retVal = CreateEntity(reader);
                }
            }

            return retVal;
        }
    }
}
