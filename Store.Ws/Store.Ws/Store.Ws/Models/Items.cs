using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Ws.Models
{
    public class Items
    {
        public Guid ClientID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
}