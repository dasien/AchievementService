
using System;

namespace Trestle.Core.Entities
{
    public interface IEntity
    {
        int Id
        {
            get;
            set;
        }

        string CreateUserId
        {
            get;
            set;
        }

        DateTime CreateDateTime
        {
            get;
            set;
        }

        string UpdateUserId
        {
            get;
            set;
        }

        DateTime UpdateDateTime
        {
            get;
            set;
        }
    }
}
