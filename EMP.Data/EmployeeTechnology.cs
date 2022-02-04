using System;
using System.Collections.Generic;

#nullable disable

namespace EMP.Data
{
    public partial class EmployeeTechnology
    {
        public Guid Id { get; set; }
        public Guid EmpId { get; set; }
        public string Name { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
