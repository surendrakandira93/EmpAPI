using System;
using System.Collections.Generic;

#nullable disable

namespace EMP.Data
{
    public partial class EmpGroupList
    {
        public Guid Id { get; set; }
        public Guid EmpId { get; set; }
        public Guid GroupId { get; set; }
        public int InviteType { get; set; }

        public virtual EmployeeGroup Group { get; set; }
    }
}
