using EMP.Dto;
using System;
using System.Collections.Generic;

namespace EMP.Service
{
    public interface IEmployeeGroupService
    {
        void AddEmployeeGroup(EmployeeGroupAddDto dto);

        void UpdateEmployeeGroup(EmployeeGroupAddDto dto);

        List<EmployeeGroupListDto> GetAllEmployeeGroup();

        EmployeeGroupListDto GetEmployeeGroupeById(Guid id);

        EmployeeGroupListDto GetEmployeeGroupeByName(string name);

        void DeleteEmployeeGroupById(Guid id);

        EmployeeGroupGridlDto GetAllEmployeeGroup(Guid id, int pageNo, int pageSize);

        List<SelectedListItemDto> GetEmployeeListByGroupId(Guid id);

        void UpdateEmployeeInviteType(Guid groupId, Guid empId, int inviteType);

        void AddSubscribeEmployeeGroup(Guid groupId, Guid empId);

        List<EmployeeGroupListDto> GetAllGroupForSubscription(Guid empId);

        List<EmployeeGroupForApprovalListDto> GetAllGroupForApproval(Guid empId);

        void UpdateSubscribeEmployeeGroup(Guid groupId, Guid empId, int inviteType);

        EmployeeGroupForApprovalListDto GetGroupForView(Guid id);
    }
}
