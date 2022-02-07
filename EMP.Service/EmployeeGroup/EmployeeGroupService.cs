using EMP.Data;
using EMP.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EMP.Service
{
    public class EmployeeGroupService : IEmployeeGroupService
    {
        public EmployeeGroupService()
        {
            using (var db = new EmpContext())
            {
                //Ensure database is created
                db.Database.EnsureCreated();
            }
        }
        public void AddEmployeeGroup(EmployeeGroupAddDto dto)
        {
            using (var db = new EmpContext())
            {
                try
                {
                    EmployeeGroup employeeGroup = new EmployeeGroup();
                    employeeGroup.Name = dto.Name;
                    employeeGroup.IconImg = dto.IconImg;
                    employeeGroup.Description = dto.Description;
                    employeeGroup.Id = Guid.NewGuid();
                    employeeGroup.AdminId = dto.AdminId;
                    employeeGroup.Employee = dto.EmployeeIds.Select(x => new EmpGroupList()
                    {
                        Id = Guid.NewGuid(),
                        EmpId = x,
                        InviteType = (int)InviteType.Invite,
                        GroupId = employeeGroup.Id
                    }).ToList();
                    db.EmployeeGroup.Add(employeeGroup);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        public void UpdateEmployeeGroup(EmployeeGroupAddDto dto)
        {
            using (var db = new EmpContext())
            {
                try
                {
                    IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                    var empGroupUpdate = (from EmployeeGroup empGroup in employeeGroups
                                          where empGroup.Id.Equals(dto.Id)
                                          select empGroup).FirstOrDefault();

                    empGroupUpdate.Name = dto.Name;
                    empGroupUpdate.IconImg = dto.IconImg;
                    empGroupUpdate.Description = dto.Description;
                    var otherInvit = empGroupUpdate.Employee.Where(x => x.InviteType != 1).ToList();
                    empGroupUpdate.Employee = dto.EmployeeIds.Select(x => new EmpGroupList()
                    {
                        Id = Guid.NewGuid(),
                        EmpId = x,
                        InviteType = (int)InviteType.Invite,
                        GroupId = dto.Id.Value
                    }).ToList();

                    if (otherInvit.Any())
                    {
                        foreach (var item in otherInvit)
                        {
                            empGroupUpdate.Employee.Add(item);
                        }

                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }


        public void AddSubscribeEmployeeGroup(Guid groupId, Guid empId)
        {
            using (var db = new EmpContext())
            {
                try
                {
                    ;
                    db.EmpGroupList.Add(new EmpGroupList()
                    {
                        Id = Guid.NewGuid(),
                        EmpId = empId,
                        InviteType = (int)InviteType.Pending,
                        GroupId = groupId
                    });
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        public void UpdateSubscribeEmployeeGroup(Guid groupId, Guid empId, int inviteType)
        {
            using (var db = new EmpContext())
            {
                try
                {
                    var otherInvit = db.EmpGroupList.Where(x => x.EmpId== empId && x.GroupId==groupId).FirstOrDefault();                    
                    otherInvit.InviteType = inviteType;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        public void UpdateEmployeeInviteType(Guid groupId, Guid empId, int inviteType)
        {
            using (var db = new EmpContext())
            {
                try
                {
                    var otherInvit = db.EmpGroupList.Where(x => x.EmpId == empId && x.GroupId == groupId).FirstOrDefault();
                    otherInvit.InviteType = inviteType;
                    db.SaveChanges(); 
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);

                }
            }
        }

        public List<EmployeeGroupListDto> GetAllEmployeeGroup()
        {
            using (var db = new EmpContext())
            {
                //Ensure database is created
                db.Database.EnsureCreated();
                IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                IEnumerable<Employee> employees = db.Employee;
                return (from EmployeeGroup empGroup in employeeGroups
                        select new EmployeeGroupListDto()
                        {
                            Name = empGroup.Name,
                            Id = empGroup.Id,
                            Employees = ((from Employee emp in employees
                                          where empGroup.Employee.Any(x => x.Id == emp.Id)
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
                                              Name = emp.Name
                                          }).ToList()).ToList()
                        }).ToList();
            }
        }
        public List<SelectedListItemDto> GetEmployeeListByGroupId(Guid id)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                IEnumerable<Employee> employees = db.Employee;
                List<Guid> empId = (from EmployeeGroup empGroup in employeeGroups
                                    where empGroup.Id.Equals(id)
                                    select empGroup.Employee.Where(x => x.InviteType != (int)InviteType.Invite).Select(x => x.Id).ToList()).FirstOrDefault();
                return (from Employee emp in employees
                        where !empId.Contains(emp.Id)
                        select new SelectedListItemDto()
                        {
                            Id = emp.Id,
                            Name = emp.Name
                        }).ToList();

            }
        }

        public EmployeeGroupListDto GetEmployeeGroupeById(Guid id)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                IEnumerable<Employee> employees = db.Employee;
                return (from EmployeeGroup empGroup in employeeGroups
                        where empGroup.Id.Equals(id)
                        select new EmployeeGroupListDto()
                        {
                            Name = empGroup.Name,
                            Id = empGroup.Id,
                            Description = empGroup.Description,
                            IconImg = empGroup.IconImg,
                            Employees = ((from Employee emp in employees
                                          where empGroup.Employee.Any(x => x.Id == emp.Id)
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
                                              Name = emp.Name
                                          }).ToList()).ToList()
                        }).FirstOrDefault();
            }
        }

        public EmployeeGroupListDto GetEmployeeGroupeByName(string name)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                IEnumerable<Employee> employees = db.Employee;
                return (from EmployeeGroup empGroup in employeeGroups
                        where empGroup.Name.Equals(name)
                        select new EmployeeGroupListDto()
                        {
                            Name = empGroup.Name,
                            Id = empGroup.Id,
                            Description = empGroup.Description,
                            IconImg = empGroup.IconImg,
                            Employees = ((from Employee emp in employees
                                          where empGroup.Employee.Any(x => x.Id == emp.Id)
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
                                              Name = emp.Name
                                          }).ToList()).ToList()
                        }).FirstOrDefault();
            }
        }

        public void DeleteEmployeeGroupById(Guid id)
        {
            using (var db = new EmpContext())
            {
                db.EmpGroupList.RemoveRange(db.EmpGroupList.Where(x => x.GroupId==id).ToList());
                db.EmployeeGroup.Remove(db.EmployeeGroup.FirstOrDefault(x => x.Id==id));
                db.SaveChanges();
            }
        }

        public EmployeeGroupGridlDto GetAllEmployeeGroup(Guid id, int pageNo, int pageSize)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                IEnumerable<Employee> employees = db.Employee;
                var empGroupQ = from EmployeeGroup empGroup in employeeGroups where empGroup.AdminId.Equals(id) select empGroup;
                int totalRecord = empGroupQ.Count();
                var allGroup = empGroupQ
                    .OrderBy(a => a.Name)
                    .Skip((pageNo - 1) * pageNo)
                    .Take(pageSize)
                    .Select(empGroup => new EmployeeGroupListDto()
                    {
                        Name = empGroup.Name,
                        Id = empGroup.Id,
                        Description = empGroup.Description,
                        IconImg = empGroup.IconImg,
                        Employees = (from Employee emp in employees
                                     where empGroup.Employee.Any(x => x.Id == emp.Id)
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
                                         Name = emp.Name
                                     }).ToList()
                    }).ToList();

                return new EmployeeGroupGridlDto()
                {
                    List = allGroup,
                    TotalItems = totalRecord,
                    PageNos = PageList(totalRecord, pageSize, pageNo)
                };
            }
        }

        public List<EmployeeGroupListDto> GetAllGroupForSubscription(Guid empId)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                var result = (from EmployeeGroup emp in employeeGroups
                              where !emp.AdminId.Equals(empId) && !emp.Employee.Any(x => x.Id == empId)
                              select new EmployeeGroupListDto()
                              {
                                  Name = emp.Name,
                                  Id = emp.Id,
                                  Description = emp.Description,
                                  IconImg = emp.IconImg
                              }).ToList();
                return result;

            }
        }

        public List<EmployeeGroupForApprovalListDto> GetAllGroupForApproval(Guid empId)
        {
            using (var db = new EmpContext())
            {
                IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                IEnumerable<Employee> employees = db.Employee;
                var result = (from EmployeeGroup empGroup in employeeGroups
                              where empGroup.AdminId.Equals(empId) && empGroup.Employee.Any(x => x.InviteType == (int)InviteType.Pending)
                              select new EmployeeGroupForApprovalListDto()
                              {
                                  Name = empGroup.Name,
                                  Id = empGroup.Id,
                                  Description = empGroup.Description,
                                  IconImg = empGroup.IconImg,

                                  Employees = (from Employee emp in employees
                                               where empGroup.Employee.Any(x => x.InviteType == (int)InviteType.Pending)
                                               select new EmployeeGroupForApprovalEmpListDto()
                                               {
                                                   Technologies = emp.EmployeeTechnologies.Select(x => x.Name).ToList(),
                                                   ImageURL = emp.ImageURL,
                                                   Id = emp.Id,
                                                   Name = emp.Name
                                               }).ToList()
                              }).ToList();

                return result;

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

        public EmployeeGroupForApprovalListDto GetGroupForView(Guid id)
        {
            using (var db = new EmpContext())
            {

                IEnumerable<EmployeeGroup> employeeGroups = db.EmployeeGroup;
                IEnumerable<Employee> employees = db.Employee;
                IEnumerable<EmployeeTechnology> employeeTechnology = db.EmployeeTechnology;
                IEnumerable<EmpGroupList> empGroupList = db.EmpGroupList;
                var result = (from EmployeeGroup empGroup in employeeGroups
                              where empGroup.Id.Equals(id)
                              select new EmployeeGroupForApprovalListDto()
                              {
                                  Name = empGroup.Name,
                                  Id = empGroup.Id,
                                  Description = empGroup.Description,
                                  IconImg = empGroup.IconImg,
                                  Admin = (from Employee emp in employees
                                           where empGroup.AdminId.Equals(emp.Id)
                                           select new EmployeeGroupForApprovalEmpListDto()
                                           {
                                               Technologies = employeeTechnology.Where(w=>w.EmpId==emp.Id).Select(x => x.Name).ToList(),
                                               ImageURL = emp.ImageURL,
                                               Id = emp.Id,
                                               Name = emp.Name
                                           }).FirstOrDefault(),
                                  Employees = (from Employee emp in employees
                                               where empGroupList.Any(x => x.GroupId==empGroup.Id && x.EmpId.Equals(emp.Id))
                                               select new EmployeeGroupForApprovalEmpListDto()
                                               {
                                                   Technologies = employeeTechnology.Where(w => w.EmpId == emp.Id).Select(x => x.Name).ToList(),
                                                   ImageURL = emp.ImageURL,
                                                   Id = emp.Id,
                                                   Name = emp.Name,
                                                   InviteType = empGroupList.FirstOrDefault(a =>a.GroupId==empGroup.Id && a.EmpId.Equals(emp.Id)).InviteType
                                               }).ToList()
                              }).FirstOrDefault();

                return result;

            }
        }
    }
}
