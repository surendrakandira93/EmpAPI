using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Data
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public Guid EmpId { get; set; }
        public string LoginId { get; set; }
        public int Broker { get; set; }
        public string Platform { get; set; }
        public DateTime Expiry { get; set; }        
        public bool IsLive { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string APIKey { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        
    }
}
