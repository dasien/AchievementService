using System;
using System.Collections.Generic;
using System.Text;

namespace Trestle.Core.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id {get; set;}
        public string CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public BaseEntity()
        {

        }
    }
}
