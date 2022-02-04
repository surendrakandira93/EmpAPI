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
    public class EmployeeGroupController : ControllerBase
    {
        private readonly IEmployeeGroupService service;
        public EmployeeGroupController(IEmployeeGroupService _service)
        {
            this.service = _service;
        }


        [HttpGet("{id}")]
        public ResponseDto<EmployeeGroupGridlDto> GetPage(Guid id, int pageNo, int pageSize)
        {
            var groups = service.GetAllEmployeeGroup(id,pageNo, pageSize);
            return new ResponseDto<EmployeeGroupGridlDto>() { Result = groups, IsSuccess = true };
        }


        [HttpGet]
        public ResponseDto<List<EmployeeGroupListDto>> GetAll()
        {
            var groups = service.GetAllEmployeeGroup();
            return new ResponseDto<List<EmployeeGroupListDto>>() { Result = groups, IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<EmployeeGroupListDto> GetById(Guid id)
        {
            var group = service.GetEmployeeGroupeById(id);
            return new ResponseDto<EmployeeGroupListDto>() { Result = group, IsSuccess = true };
        }

        [HttpGet("{name}")]
        public ResponseDto<EmployeeGroupListDto> GetByName(string name)
        {
            var group = service.GetEmployeeGroupeByName(name);
            return new ResponseDto<EmployeeGroupListDto>() { Result = group, IsSuccess = true };
        }

        [HttpPost]
        public ResponseDto<string> Create(EmployeeGroupAddDto employeeGroup)
        {
            if (employeeGroup == null)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            if (!ModelState.IsValid)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }
            if (employeeGroup.EmployeeIds == null || !employeeGroup.EmployeeIds.Any())
            {
                return new ResponseDto<string>() { Message = "Employee required", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            service.AddEmployeeGroup(employeeGroup);

            return new ResponseDto<string>() { Message = "Employee Group created", IsSuccess = true };
        }

        [HttpPut]
        public ResponseDto<string> Update(EmployeeGroupAddDto employeeGroup)
        {
            if (employeeGroup == null)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            if (!ModelState.IsValid)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }
            if (employeeGroup.EmployeeIds == null || !employeeGroup.EmployeeIds.Any())
            {
                return new ResponseDto<string>() { Message = "Employee required", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }


            service.UpdateEmployeeGroup(employeeGroup);

            return new ResponseDto<string>() { Message = "Employee Group updated", IsSuccess = true };
        }

        [HttpDelete("{id}")]
        public ResponseDto<string> Delete(Guid id)
        {
            service.DeleteEmployeeGroupById(id);
            return new ResponseDto<string>() { Message = "Employee Group deleted", IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<List<SelectedListItemDto>> GetEmployeeListByGroupId(Guid id)
        {
            var group = service.GetEmployeeListByGroupId(id);
            return new ResponseDto<List<SelectedListItemDto>>() { Result = group, IsSuccess = true };
        }

        [HttpGet()]
        public ResponseDto<string> UpdateEmployeeInviteType(Guid groupId, Guid empId, int inviteType)
        {
            service.UpdateEmployeeInviteType(groupId, empId, inviteType);
            return new ResponseDto<string>() { IsSuccess = true };
        }

        [HttpGet()]
        public ResponseDto<string> AddSubscribeEmployeeGroup(Guid groupId, Guid empId)
        {
            service.AddSubscribeEmployeeGroup(groupId, empId);
            return new ResponseDto<string>() { IsSuccess = true };
        }


        [HttpGet()]
        public ResponseDto<string> UpdateSubscribeEmployeeGroup(Guid groupId, Guid empId,int inviteType)
        {
            service.UpdateSubscribeEmployeeGroup(groupId, empId,inviteType);
            return new ResponseDto<string>() { IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<List<EmployeeGroupListDto>> GetAllGroupForSubscription(Guid id)
        {
            var group = service.GetAllGroupForSubscription(id);
            return new ResponseDto<List<EmployeeGroupListDto>>() { Result = group, IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<EmployeeGroupForApprovalListDto> GetGroupForView(Guid id)
        {
            var group = service.GetGroupForView(id);
            return new ResponseDto<EmployeeGroupForApprovalListDto>() { Result = group, IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<List<EmployeeGroupForApprovalListDto>> GetAllGroupForApproval(Guid id)
        {
            var group = service.GetAllGroupForApproval(id);
            return new ResponseDto<List<EmployeeGroupForApprovalListDto>>() { Result = group, IsSuccess = true };
        }
    }
}
