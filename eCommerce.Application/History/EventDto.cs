using System.Collections.Generic;
using System;

namespace eCommerce.Application.History
{
    public class EventDto
    {
        public string Type { get; set; }
        public Dictionary<string, string> Args { get; set; }
        public DateTime Created { get; set; }
        public string CorrelationID { get; set; }
    }
}