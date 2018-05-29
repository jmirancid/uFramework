using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uFramework.Entities
{
    [Serializable]
    public abstract class Entity
    {
        public virtual object Id { get; set; }
    }
}
