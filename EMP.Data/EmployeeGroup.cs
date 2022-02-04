using System;
using System.Collections.Generic;

#nullable disable

namespace EMP.Data
{
    public partial class EmployeeGroup
    {
        public EmployeeGroup()
        {
            Employee = new HashSet<EmpGroupList>();
        }

        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public string Name { get; set; }
        public string IconImg { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual ICollection<EmpGroupList> Employee { get; set; }
    }
}
