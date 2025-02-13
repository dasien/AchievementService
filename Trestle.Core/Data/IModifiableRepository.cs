using System.Data;
using Trestle.Core.Entities;

namespace Trestle.Core.Data
{
    public interface IModifiableRepository : IRepository
    {
        IEntity Insert(IDbConnection con, IEntity entity);
        IEntity Insert(IDbConnection con, IDbTransaction txn, int timeout, IEntity entity);
        void Update(IDbConnection con, IEntity entity);
        void Update(IDbConnection con, IDbTransaction txn, int timeout, IEntity entity);
        void Delete(IDbConnection con, IEntity entity);
        void Delete(IDbConnection con, IDbTransaction txn, int timeout, IEntity entity);
    }
}
