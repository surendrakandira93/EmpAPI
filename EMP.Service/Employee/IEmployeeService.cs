using EMP.Dto;
using System;
using System.Collections.Generic;

namespace EMP.Service
{
    public interface IEmployeeService
    {
        void AddEmployee(EmployeeDto dto);

        void UpdateEmployee(EmployeeDto dto);

        List<EmployeeDto> GetAllEmployee();

        EmployeeDto GetEmployeeById(Guid id);

        List<EmployeeDto> GetEmployeeByName(string name);

        EmployeeDto GetEmployeeByEmail(string email);

        void DeleteEmployeeById(Guid id);

        List<string> GetempGroupNamesById(Guid id);

        EmployeeGridlDto GetAllEmployee(int pageNo, int pageSize);

        void UpdateProfileImage(EmployeeProfileImageDto dto);

        void UpdateTechnologies(EmployeeTechnologiesDto dto);

        void UpdateLinkedinURL(EmployeeProfileImageDto dto);

        EmployeeProfileDto GetEmployeeProfile(Guid id);

        void ChangePassword(Guid id, string password);

    }
}
