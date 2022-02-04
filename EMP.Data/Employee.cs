using System;
using System.Collections.Generic;

#nullable disable

namespace EMP.Data
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeTechnologies = new HashSet<EmployeeTechnology>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBrith { get; set; }
        public string Gender { get; set; }
        public string ImageURL { get; set; }
        public string LinkedinURL { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual ICollection<EmployeeTechnology> EmployeeTechnologies { get; set; }
    }
}
