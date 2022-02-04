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

    public class EmployeeTest
    {
        private readonly IEmployeeService service;
        public EmployeeTest()
        {
            this.service = new EmployeeService(); ;

        }

        [Fact]
        public void AddEmployee_Success()
        {
            EmployeeController empController = new EmployeeController(service);
            var result = empController.Create(GetEmp());
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.Equal("Employee created", result.Message);
            Assert.NotNull(result.Message);
            Assert.Null(result.Result);

        }
           

        [Fact]
        public void GetAllEmployee_Success()
        {
            var emp = GetEmp();
            EmployeeController empController = new EmployeeController(service);
            var result = empController.GetAll();
            Assert.Equal(HttpStatusCode.OK, result.Code);            
            Assert.NotNull(result.Result);
            Assert.Null(result.Message);
            Assert.True(result.Result.Any());
            Assert.Contains(result.Result, a =>a.Name== emp.Name);

        }

        [Fact]
        public void GetByEmployeeById_Success()
        {
            var emp = GetEmp();
            EmployeeController empController = new EmployeeController(service);
            var allEmp = empController.GetAll();
            var emId = allEmp.Result.Any()? allEmp.Result.FirstOrDefault().Id:Guid.NewGuid();
            var result = empController.GetById(emId.Value);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.NotNull(result.Result);
            Assert.Null(result.Message);
            Assert.NotNull(result.Result);
            Assert.NotNull(result.Result.Name);
        }

        [Fact]
        public void GetByEmployeeByName_Success()
        {
            var emp = GetEmp();
            EmployeeController empController = new EmployeeController(service);
            var allEmp = empController.GetAll();
            var emName = allEmp.Result.Any() ? allEmp.Result.FirstOrDefault().Name : string.Empty;
            var result = empController.GetByName(emName);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.NotNull(result.Result);
            Assert.Null(result.Message);
            Assert.NotNull(result.Result);
            //Assert.NotNull(result.Result.Name);
        }

        [Fact]
        public void GetTechnologiesById_Success()
        {
            var emp = GetEmp();
            EmployeeController empController = new EmployeeController(service);
            var allEmp = empController.GetAll();
            var emId = allEmp.Result.Any() ? allEmp.Result.FirstOrDefault().Id : Guid.NewGuid();
            var result = empController.GetTechnologiesById(emId.Value);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.NotNull(result.Result);
            Assert.Null(result.Message);
            Assert.NotNull(result.Result);
            Assert.True(result.Result.Any());
        }

        //[Fact]
        //public void UpdateEmployee_Success()
        //{
        //    EmployeeController empController = new EmployeeController(service);
        //    var emp = GetEmp();
        //    var empU = empController.GetByName(emp.Name);
        //    var empUpadte = empU.Result;
        //    empUpadte.Name = $"{empUpadte.Name}_{DateTime.Now.Ticks}";
        //    var result = empController.Update(empUpadte);
        //    Assert.Equal(HttpStatusCode.OK, result.Code);
        //    Assert.Equal("Employee updated", result.Message);
        //    Assert.NotNull(result.Message);
        //    Assert.Null(result.Result);

        //}

        private EmployeeAddDto GetEmp()
        {
            return new EmployeeAddDto()
            {
                Name = "Surendra Kandira",
                DateOfBrith = new DateTime(1993, 05, 02),
                City = "Jaipur",
                Gender = "Male",
                Technologies = new List<string> { "C#", ".Net Core", ".Net MVC" }
            };
        }
    }
}
