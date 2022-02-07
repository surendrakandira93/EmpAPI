using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Dto
{
    public class ShipmentAddDto
    {
        public Guid Id { get; set; }
        public string LoginId { get; set; }
        public Guid EmpId { get; set; }
        public int Broker { get; set; }
        public string Platform { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string APIKey { get; set; }
    }

    public class ShipmentDto
    {
        public Guid Id { get; set; }
        public string LoginId { get; set; }
        public Guid EmpId { get; set; }
        public int Broker { get; set; }
        public string Platform { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsLive { get; set; }
        public string APIKey { get; set; }
    }
}
