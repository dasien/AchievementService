using System.Data;
using Trestle.Core.Entities;

namespace Trestle.Core.Data
{
    public interface IEntityMap
    {
        void MapData(BaseEntity obj, IDataReader rdr);
    }
}
