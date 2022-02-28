using EMP.Dto;
using EMP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemeProfitLossController : ControllerBase
    {
        private readonly ISchemeProfitLossService service;
        public SchemeProfitLossController(ISchemeProfitLossService _service)
        {
            this.service = _service;
        }

        [HttpGet]
        [Route("GetAll")]
        public ResponseDto<List<SchemeProfitLossDto>> GetAll(Guid groupId)
        {
            var employees = service.GetAll(groupId);
            return new ResponseDto<List<SchemeProfitLossDto>>() { Result = employees, IsSuccess = true };
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public ResponseDto<SchemeProfitLossDto> GetById(Guid id)
        {
            var response = service.GetById(id);
            return new ResponseDto<SchemeProfitLossDto>() { Result = response, IsSuccess = true };
        }

        [HttpPost]
        [Route("AddRange")]
        public ResponseDto<string> AddRange(List<SchemeProfitLossDto> schemeProft)
        {
            service.AddRange(schemeProft);
            return new ResponseDto<string>() { Message = "Scheme ProfitLoss created", IsSuccess = true };
        }


        [HttpPost]
        [Route("Create")]
        public ResponseDto<string> Create(SchemeProfitLossDto schemeProft)
        {
            service.Add(schemeProft);
            return new ResponseDto<string>() { Message = "Scheme ProfitLoss created", IsSuccess = true };
        }

        [HttpPut]
        [Route("Update")]
        public ResponseDto<string> Update(SchemeProfitLossDto schemeProft)
        {
            service.Update(schemeProft);
            return new ResponseDto<string>() { Message = "Scheme ProfitLoss updated", IsSuccess = true };
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public ResponseDto<string> Delete(Guid id)
        {
            service.Delete(id);
            return new ResponseDto<string>() { Message = "Scheme ProfitLoss deleted", IsSuccess = true };
        }

        [HttpDelete]
        [Route("DeleteByGroupId/{id}")]
        public ResponseDto<string> DeleteByGroupId(Guid id)
        {
            service.DeleteByGroupId(id);
            return new ResponseDto<string>() { Message = "Scheme ProfitLoss deleted", IsSuccess = true };
        }

        [HttpGet]
        [Route("IsExist")]
        public ResponseDto<bool> IsExist(Guid? id, Guid groupId, DateTime date)
        {
            var response = service.IsExist(id, groupId, date);
            return new ResponseDto<bool>() { Result = response, IsSuccess = true };
        }

        [HttpGet]
        [Route("GetKeywords")]
        public ResponseDto<List<string>> GetKeywords()
        {
            var response = service.GetKeywords();
            return new ResponseDto<List<string>>() { Result = response, IsSuccess = true };
        }

        [HttpGet]
        [Route("Chart")]
        public ResponseDto<List<GroupChartDto>> GetChartData(Guid groupId, int typeId, DateTime? fromDate, DateTime? toDate)
        {
            var employees = service.GetChartData(groupId, typeId,fromDate,toDate);
            return new ResponseDto<List<GroupChartDto>>() { Result = employees, IsSuccess = true };
        }

        [HttpGet]
        [Route("ProfitLossChart")]
        public ResponseDto<List<GroupChartDto>> GetProfitLossChartData(Guid groupId, int typeId, DateTime? fromDate, DateTime? toDate)
        {
            var employees = service.GetProfitLossChartData(groupId, typeId,fromDate,toDate);
            return new ResponseDto<List<GroupChartDto>>() { Result = employees, IsSuccess = true };
        }

        [HttpGet]
        [Route("MonthlyBreaup")]
        public ResponseDto<List<GroupMontlyBreakupDto>> GetMonthlyBreaupData(Guid groupId, DateTime? fromDate, DateTime? toDate)
        {
            var employees = service.GetMonthlyBreaupData(groupId,fromDate,toDate);
            return new ResponseDto<List<GroupMontlyBreakupDto>>() { Result = employees, IsSuccess = true };
        }

        [HttpGet]
        [Route("Cal_Heatmap")]
        public ResponseDto<List<HeatmapResponseDto>> GetCal_HeatmapData(Guid groupId, DateTime fromDate, DateTime toDate)
        {
            var employees = service.GetCal_HeatmapData(groupId, fromDate, toDate);
            return new ResponseDto<List<HeatmapResponseDto>>() { Result = employees, IsSuccess = true };
        }

        [HttpGet]
        [Route("PLSummary")]
        public ResponseDto<SchemeProfitLossSummary> PLSummary(Guid groupId, DateTime? fromDate, DateTime? toDate)
        {
            var employees = service.PLSummary(groupId, fromDate, toDate);
            return new ResponseDto<SchemeProfitLossSummary>() { Result = employees, IsSuccess = true };
        }
    }
}
