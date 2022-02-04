using EMP.API.Controllers;
using EMP.Dto;
using EMP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EMP.Test
{
    public class EmployeeGroupTest
    {
        private readonly IEmployeeGroupService service;
        private readonly IEmployeeService empService;
        public EmployeeGroupTest()
        {
            this.service = new EmployeeGroupService();
            this.empService = new EmployeeService();

        }

        [Fact]
        public void AddEmployeeGroup_Success()
        {
            EmployeeGroupController empController = new EmployeeGroupController(service);
            var result = empController.Create(GetEmpGroup());
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.Equal("Employee Group created", result.Message);
            Assert.NotNull(result.Message);
            Assert.Null(result.Result);

        }


        [Fact]
        public void GetAllEmployeeGroup_Success()
        {
            var emp = GetEmpGroup();
            EmployeeGroupController empController = new EmployeeGroupController(service);
            var result = empController.GetAll();
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.NotNull(result.Result);
            Assert.Null(result.Message);
            Assert.True(result.Result.Any());
            Assert.Contains(result.Result, a => a.Name == emp.Name);

        }

        [Fact]
        public void GetByEmployeeGroupById_Success()
        {
            var emp = GetEmpGroup();
            EmployeeGroupController empController = new EmployeeGroupController(service);
            var allEmp = empController.GetAll();
            var emId = allEmp.Result.Any() ? allEmp.Result.FirstOrDefault().Id.Value : Guid.NewGuid();
            var result = empController.GetById(emId);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.NotNull(result.Result);
            Assert.Null(result.Message);
            Assert.NotNull(result.Result);
            Assert.NotNull(result.Result.Name);
        }

        [Fact]
        public void GetByEmployeeGroupByName_Success()
        {
            var emp = GetEmpGroup();
            EmployeeGroupController empController = new EmployeeGroupController(service);
            var allEmp = empController.GetAll();
            var emName = allEmp.Result.Any() ? allEmp.Result.FirstOrDefault().Name : string.Empty;
            var result = empController.GetByName(emName);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.NotNull(result.Result);
            Assert.Null(result.Message);
            Assert.NotNull(result.Result);
            Assert.NotNull(result.Result.Name);
        }


        [Fact]
        public void UpdateEmployeeGroup_Success()
        {
            EmployeeGroupController empController = new EmployeeGroupController(service);
            var emp = GetEmpGroup();
            var empU = empController.GetByName(emp.Name);
            var empGroupUpadte = empU.Result;
            empGroupUpadte.Name = $"{empGroupUpadte.Name}_{DateTime.Now.Ticks}";
            var result = empController.Update(new EmployeeGroupAddDto() { Name = empGroupUpadte.Name, Id = empGroupUpadte.Id, EmployeeIds = empGroupUpadte.Employees.Select(x => x.Id.Value).ToList() });
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.Equal("Employee Group updated", result.Message);
            Assert.NotNull(result.Message);
            Assert.Null(result.Result);

        }

        private EmployeeGroupAddDto GetEmpGroup()
        {
            var allEmp = empService.GetAllEmployee();
            return new EmployeeGroupAddDto()
            {
                Name = ".Net Core Group",
                EmployeeIds = allEmp.Select(x => x.Id.Value).ToList()
            };
        }
    }
}
