using System.Collections.Generic;
using System;

namespace eCommerce.Domain.Core
{
    public abstract class DomainEvent
    {
        public string Type { get { return GetType().Name; } }

        public DateTime Created { get; private set; }

        public Dictionary<string, object> Args { get; private set; }

        public string CorrelationID { get; set; }

        public DomainEvent()
        {
            Created = DateTime.Now;
            Args = new Dictionary<string, object>();
        }

        public abstract void Flatten();
    }
}