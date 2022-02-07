using EMP.Data;
using EMP.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using VelocityDb;


namespace EMP.Service
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService()
        {  
            using (var db = new EmpContext())
            {
                //Ensure database is created
                db.Database.EnsureCreated();
            }
        }
        public void AddEmployee(EmployeeDto dto)
        {
            using (var db = new EmpContext())
            {

                try
                {

                    Employee employee = new Employee();
                    employee.Name = dto.Name;
                    employee.Age = dto.Age;
                    employee.DateOfBrith = dto.DateOfBrith.Value;
                    employee.City = dto.City;
                    employee.Gender = dto.Gender;
                    employee.Id = Guid.NewGuid();
                    employee.ImageURL = dto.ImageURL;
                    employee.LinkedinURL = dto.LinkedinURL;
                    employee.EmployeeTechnologies = dto.Technologies.Select(x => new EmployeeTechnology() { Id = Guid.NewGuid(), EmpId = employee.Id, Name = x }).ToList();
                    employee.Email = dto.Email;
                    employee.Password = dto.Password;
                    employee.Created = DateTime.Now;
                    employee.Modified = DateTime.Now;
                    db.Add(employee);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);

                }
            }
        }

        public void UpdateEmployee(EmployeeDto dto)
        {
            using (var db = new EmpContext())
            {

                try
                {
                    var empUpdate = db.Employee.FirstOrDefault(x => x.Id == dto.Id);
                    empUpdate.Name = dto.Name;
                    empUpdate.Age = dto.Age;
                    empUpdate.DateOfBrith = dto.DateOfBrith.Value;
                    empUpdate.City = dto.City;
                    empUpdate.Gender = dto.Gender;
                    empUpdate.ImageURL = dto.ImageURL;
                    empUpdate.LinkedinURL = dto.LinkedinURL;
                    empUpdate.EmployeeTechnologies.Clear();
                    empUpdate.EmployeeTechnologies = dto.Technologies.Select(x => new EmployeeTechnology() { Id = Guid.NewGuid(), EmpId = empUpdate.Id, Name = x }).ToList();
                    empUpdate.Modified = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);

                }
            }
        }

        public void UpdateProfileImage(EmployeeProfileImageDto dto)
        {
            using (var db = new EmpContext())
            {
                try
                {
                    var empUpdate = db.Employee.FirstOrDefault(x => x.Id == dto.Id);
                    empUpdate.ImageURL = dto.ImageURL;
                    empUpdate.Modified = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);

                }
            }
        }

        public void UpdateLinkedinURL(EmployeeProfileImageDto dto)
        {
            using (var db = new EmpContext())
            {
                try
                {
                    var empUpdate = db.Employee.FirstOrDefault(x => x.Id == dto.Id);
                    empUpdate.LinkedinURL = dto.ImageURL;
                    empUpdate.Modified = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);

                }
            }
        }

        public void UpdateTechnologies(EmployeeTechnologiesDto dto)
        {
            using (var db = new EmpContext())
            {
                try
                {
                    var empUpdate = db.Employee.FirstOrDefault(x => x.Id == dto.Id);
                    empUpdate.EmployeeTechnologies = dto.Technologies.Select(x => new EmployeeTechnology() { Id = Guid.NewGuid(), EmpId = empUpdate.Id, Name = x }).ToList();
                    empUpdate.Modified = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        public List<EmployeeDto> GetAllEmployee()
        {
            using (var db = new EmpContext())
            {
                return (from Employee emp in db.Employee
                        select new EmployeeDto()
                        {
                            Age = emp.Age,
                            Technologies = emp.EmployeeTechnologies.Select(x => x.Name).ToList(),
                            LinkedinURL = emp.LinkedinURL,
                            ImageURL = emp.ImageURL,
                            City = emp.City,
                            DateOfBrith = emp.DateOfBrith,
                            Id = emp.Id,
                            Gender = emp.Gender,
                            Name = emp.Name,
                            Email = emp.Email
                        }).ToList();
            }
        }

        public EmployeeDto GetEmployeeById(Guid id)
        {
            using (var db = new EmpContext())
            {
                return (from Employee emp in db.Employee
                        where emp.Id.Equals(id)
                        select new EmployeeDto()
                        {
                            Age = emp.Age,
                            Technologies = emp.EmployeeTechnologies.Select(x => x.Name).ToList(),
                            LinkedinURL = emp.LinkedinURL,
                            ImageURL = emp.ImageURL,
                            City = emp.City,
                            DateOfBrith = emp.DateOfBrith,
                            Id = emp.Id,
                            Gender = emp.Gender,
                            Name = emp.Name,
                            Email = emp.Email
                        }).FirstOrDefault();
            }
        }

        public EmployeeProfileDto GetEmployeeProfile(Guid id)
        {
            using (var db = new EmpContext())
            {

                IEnumerable<Employee> employees = db.Employee;
                IEnumerable<EmployeeTechnology> employeeTechnology = db.EmployeeTechnology;
                IEnumerable<EmployeeGroup> employeeGroup = db.EmployeeGroup;
                IEnumerable<EmpGroupList> empGroupList = db.EmpGroupList;
                EmployeeProfileDto profil = (from Employee emp in employees
                                             where emp.Id.Equals(id)
                                             select new EmployeeProfileDto()
                                             {
                                                 Technologies = employeeTechnology.Where(x => x.EmpId == id).Select(x => x.Name).ToList(),
                                                 LinkedinURL = emp.LinkedinURL,
                                                 ImageURL = emp.ImageURL,
                                                 City = emp.City,
                                                 DateOfBrith = emp.DateOfBrith,
                                                 Id = emp.Id,
                                                 Gender = emp.Gender,
                                                 Name = emp.Name,
                                                 Email = emp.Email
                                             }).FirstOrDefault();

                profil.Subscription = (from EmployeeGroup empGroup in employeeGroup
                                       where !empGroup.AdminId.Equals(id) && !empGroupList.Any(x => x.GroupId == empGroup.Id && x.EmpId == id)
                                       select new EmployeeGroupProfileDto()
                                       {
                                           Id = empGroup.Id,
                                           Description = empGroup.Description,
                                           IconImg = empGroup.IconImg,
                                           Name = empGroup.Name
                                       }).ToList();

                profil.Invited = (from EmployeeGroup empGroup in employeeGroup
                                  where !empGroup.AdminId.Equals(id) && empGroupList.Any(a => a.GroupId == empGroup.Id && a.EmpId == id && a.InviteType == (int)InviteType.Invite)
                                  select new EmployeeGroupProfileDto()
                                  {
                                      Id = empGroup.Id,
                                      Description = empGroup.Description,
                                      IconImg = empGroup.IconImg,
                                      Name = empGroup.Name
                                  }).ToList();

                profil.Accepted = (from EmployeeGroup empGroup in employeeGroup
                                   where !empGroup.AdminId.Equals(id) && empGroupList.Any(a => a.GroupId == empGroup.Id && a.EmpId == id && a.InviteType == (int)InviteType.Accepted)
                                   select new EmployeeGroupProfileDto()
                                   {
                                       Id = empGroup.Id,
                                       Description = empGroup.Description,
                                       IconImg = empGroup.IconImg,
                                       Name = empGroup.Name
                                   }).ToList();

                profil.Pending = (from EmployeeGroup empGroup in employeeGroup
                                  where !empGroup.AdminId.Equals(id) && empGroupList.Any(a => a.GroupId == empGroup.Id && a.EmpId == id && a.InviteType == (int)InviteType.Pending)
                                  select new EmployeeGroupProfileDto()
                                  {
                                      Id = empGroup.Id,
                                      Description = empGroup.Description,
                                      IconImg = empGroup.IconImg,
                                      Name = empGroup.Name
                                  }).ToList();

                profil.Rejected = (from EmployeeGroup empGroup in employeeGroup
                                   where !empGroup.AdminId.Equals(id) && empGroupList.Any(a => a.GroupId == empGroup.Id && a.EmpId == id && a.InviteType == (int)InviteType.Rejected)
                                   select new EmployeeGroupProfileDto()
                                   {
                                       Id = empGroup.Id,
                                       Description = empGroup.Description,
                                       IconImg = empGroup.IconImg,
                                       Name = empGroup.Name
                                   }).ToList();

                profil.Self = (from EmployeeGroup empGroup in employeeGroup
                               where empGroup.AdminId.Equals(id)
                               select new EmployeeGroupProfileDto()
                               {
                                   Id = empGroup.Id,
                                   Description = empGroup.Description,
                                   IconImg = empGroup.IconImg,
                                   Name = empGroup.Name
                               }).ToList();
                profil.ForApproval = (from EmployeeGroup empGroup in employeeGroup
                                      where empGroup.AdminId.Equals(id) && empGroupList.Any(x => x.GroupId == empGroup.Id && x.InviteType == (int)InviteType.Pending && x.EmpId != id)
                                      select new EmployeeGroupForApprovalListDto()
                                      {
                                          Name = empGroup.Name,
                                          Id = empGroup.Id,
                                          Description = empGroup.Description,
                                          IconImg = empGroup.IconImg,

                                          Employees = (from Employee emp in employees
                                                       where empGroupList.Any(x => x.GroupId == empGroup.Id && x.InviteType == (int)InviteType.Pending && x.EmpId.Equals(emp.Id)) && emp.Id != id
                                                       select new EmployeeGroupForApprovalEmpListDto()
                                                       {
                                                           Technologies = employeeTechnology.Where(x => x.EmpId == emp.Id).Select(x => x.Name).ToList(),
                                                           ImageURL = emp.ImageURL,
                                                           Id = emp.Id,
                                                           Name = emp.Name
                                                       }).ToList()
                                      }).ToList();

                profil.Group = profil.Accepted.Count() + profil.Self.Count();
                return profil;
            }
        }

        public List<EmployeeDto> GetEmployeeByName(string name)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<Employee> employees = db.Employee;
                return (from Employee emp in employees
                        where emp.Name.Contains(name)
                        select new EmployeeDto()
                        {
                            Age = emp.Age,
                            Technologies = emp.EmployeeTechnologies.Select(x => x.Name).ToList(),
                            LinkedinURL = emp.LinkedinURL,
                            ImageURL = emp.ImageURL,
                            City = emp.City,
                            DateOfBrith = emp.DateOfBrith,
                            Id = emp.Id,
                            Gender = emp.Gender,
                            Name = emp.Name,
                            Email = emp.Email
                        }).ToList();
            }
        }

        public EmployeeDto GetEmployeeByEmail(string email)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<Employee> employees = db.Employee;
                return (from Employee emp in employees
                        where emp.Email.ToLower().Equals(email.ToLower())
                        select new EmployeeDto()
                        {
                            Age = emp.Age,
                            Technologies = emp.EmployeeTechnologies.Select(x => x.Name).ToList(),
                            LinkedinURL = emp.LinkedinURL,
                            ImageURL = emp.ImageURL,
                            City = emp.City,
                            DateOfBrith = emp.DateOfBrith,
                            Id = emp.Id,
                            Gender = emp.Gender,
                            Name = emp.Name,
                            Email = emp.Email,
                            Password = emp.Password
                        }).FirstOrDefault();
            }
        }

        public List<string> GetempGroupNamesById(Guid id)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<EmployeeGroup> employees = db.EmployeeGroup;
                return (from EmployeeGroup emp in employees
                        where emp.Employee.Any(a => a.Id == id)
                        select emp.Name).ToList();
            }
        }

        public void DeleteEmployeeById(Guid id)
        {
            using (var db = new EmpContext())
            {
                db.EmployeeTechnology.RemoveRange(db.EmployeeTechnology.Where(x => x.EmpId == id).ToList());
                db.Employee.Remove(db.Employee.FirstOrDefault(x => x.Id == id));
                db.SaveChanges();
            }
        }

        public EmployeeGridlDto GetAllEmployee(int pageNo, int pageSize)
        {
            using (var db = new EmpContext())
            {

                IEnumerable<Employee> employees = db.Employee; ;
                var empQ = from Employee emp in employees select emp;
                int totalRecord = empQ.Count();

                var allemp = empQ
                    .OrderBy(a => a.Name)
                    .Skip((pageNo - 1) * pageNo)
                    .Take(pageSize)
                    .Select(emp => new EmployeeDto()
                    {
                        Age = emp.Age,
                        Technologies = emp.EmployeeTechnologies.Select(x => x.Name).ToList(),
                        LinkedinURL = emp.LinkedinURL,
                        ImageURL = emp.ImageURL,
                        City = emp.City,
                        DateOfBrith = emp.DateOfBrith,
                        Id = emp.Id,
                        Gender = emp.Gender,
                        Name = emp.Name,
                        Email = emp.Email
                    }).ToList();


                return new EmployeeGridlDto()
                {
                    List = allemp,
                    TotalItems = totalRecord,
                    PageNos = PageList(totalRecord, pageSize, pageNo)
                };
            }
        }

        private List<PageIng> PageList(int totalItem, int pageSize, int currentPage)
        {
            List<PageIng> pageList = new List<PageIng>();
            int totalPage = (totalItem / pageSize) + (totalItem % pageSize > 0 ? 1 : 0);
            for (int i = 1; i <= totalPage; i++)
            {
                pageList.Add(new PageIng { PageNo = i, IsCurrentPage = currentPage == i });
            }
            return pageList;

        }

    }

}
