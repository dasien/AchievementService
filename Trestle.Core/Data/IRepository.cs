using System.Data;
using System.Collections.Generic;
using Trestle.Core.Entities;

namespace Trestle.Core.Data
{
    public interface IRepository
    {
        IEntity GetByPrimaryKey(IDbConnection con, List<object> primaryKeys);
        IEntity GetByPrimaryKey(IDbConnection con, IDbTransaction txn, int timeout, List<object> primaryKeys);
    }
}
