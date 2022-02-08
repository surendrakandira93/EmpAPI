using EMP.Dto;
using EMP.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace EMP.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;
        public EmployeeController(IEmployeeService _service)
        {
            this.service = _service;
        }

        [HttpGet]
        public ResponseDto<EmployeeGridlDto> GetPage(int pageNo, int pageSize)
        {
            var employees = service.GetAllEmployee(pageNo, pageSize);
            return new ResponseDto<EmployeeGridlDto>() { Result = employees, IsSuccess = true };
        }


        [HttpGet]
        public ResponseDto<List<EmployeeDto>> GetAll()
        {
            var employees = service.GetAllEmployee();
            return new ResponseDto<List<EmployeeDto>>() { Result = employees, IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<EmployeeDto> GetById(Guid id)
        {
            var employee = service.GetEmployeeById(id);
            return new ResponseDto<EmployeeDto>() { Result = employee, IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<EmployeeProfileDto> GetEmployeeProfile(Guid id)
        {
            var employee = service.GetEmployeeProfile(id);
            return new ResponseDto<EmployeeProfileDto>() { Result = employee, IsSuccess = true };
        }

        [HttpGet("{name}")]
        public ResponseDto<List<EmployeeDto>> GetByName(string name)
        {
            var employee = service.GetEmployeeByName(name);
            return new ResponseDto<List<EmployeeDto>>() { Result = employee, IsSuccess = true };
        }


        [HttpGet("{email}")]
        public ResponseDto<EmployeeDto> GetByEmail(string email)
        {
            var employee = service.GetEmployeeByEmail(email);
            return new ResponseDto<EmployeeDto>() { Result = employee, IsSuccess = true };
        }


        [HttpGet("{id}")]
        public ResponseDto<List<string>> GetTechnologiesById(Guid id)
        {
            var employee = service.GetEmployeeById(id);
            return new ResponseDto<List<string>>() { Result = employee.Technologies, IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<List<string>> GetempGroupNamesById(Guid id)
        {
            var groupNames = service.GetempGroupNamesById(id);
            return new ResponseDto<List<string>>() { Result = groupNames, IsSuccess = true };
        }

        [HttpPost]
        public ResponseDto<string> Create(EmployeeAddDto employee)
        {
            if (employee == null)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            if (!ModelState.IsValid)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            EmployeeDto response = new EmployeeDto()
            {
                City = employee.City,
                DateOfBrith = employee.DateOfBrith,
                Gender = employee.Gender,
                ImageURL = employee.ImageURL,
                LinkedinURL = employee.LinkedinURL,
                Name = employee.Name,
                Technologies = employee.Technologies,
                Email = employee.Email,
                Password = employee.Password
            };

            response.Age = employee.DateOfBrith.HasValue ? CalculateAge(employee.DateOfBrith.Value) : 0;

            service.AddEmployee(response);

            return new ResponseDto<string>() { Message = "Employee created", IsSuccess = true };
        }

        [HttpPut]
        public ResponseDto<string> Update(EmployeeAddDto employee)
        {
            if (employee == null)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            if (!ModelState.IsValid)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }
            if (employee.Technologies == null || !employee.Technologies.Any())
            {
                return new ResponseDto<string>() { Message = "Technologies required", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            EmployeeDto response = new EmployeeDto()
            {
                City = employee.City,
                DateOfBrith = employee.DateOfBrith,
                Gender = employee.Gender,
                ImageURL = employee.ImageURL,
                LinkedinURL = employee.LinkedinURL,
                Name = employee.Name,
                Technologies = employee.Technologies,
                Id = employee.Id
            };

            response.Age = employee.DateOfBrith.HasValue ? CalculateAge(employee.DateOfBrith.Value) : 0;

            service.UpdateEmployee(response);
            return new ResponseDto<string>() { Message = "Employee updated", IsSuccess = true };
        }

        [HttpPut]
        public ResponseDto<string> UpdateProfileImage(EmployeeProfileImageDto employee)
        {
            if (employee == null)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            if (!ModelState.IsValid)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }          

            service.UpdateProfileImage(employee);
            return new ResponseDto<string>() { Message = "Profile Image updated", IsSuccess = true };
        }

        [HttpPut]
        public ResponseDto<string> UpdateLinkedinURL(EmployeeProfileImageDto employee)
        {
            service.UpdateLinkedinURL(employee);
            return new ResponseDto<string>() { Message = "Profile Linkedin URL", IsSuccess = true };
        }

        [HttpPut]
        public ResponseDto<string> UpdateTechnologies(EmployeeTechnologiesDto employee)
        {
            if (employee == null)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            if (!ModelState.IsValid)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            if (employee.Technologies == null || !employee.Technologies.Any())
            {
                return new ResponseDto<string>() { Message = "Technologies required", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            service.UpdateTechnologies(employee);
            return new ResponseDto<string>() { Message = "Technologies updated", IsSuccess = true };
        }

        [HttpDelete("{id}")]
        public ResponseDto<string> Delete(Guid id)
        {
            service.DeleteEmployeeById(id);
            return new ResponseDto<string>() { Message = "Employee deleted", IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<string> ChangePassword(Guid id,string password)
        {
            service.ChangePassword(id, password);
            return new ResponseDto<string>() { Message = "Password Changed", IsSuccess = true };
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
