using EMP.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Service
{
    public interface ISchemeProfitLossService
    {
        void AddRange(List<SchemeProfitLossDto> dto);

        void Add(SchemeProfitLossDto dto);

        void Update(SchemeProfitLossDto dto);

        List<SchemeProfitLossDto> GetAll(Guid groupId);

        SchemeProfitLossDto GetById(Guid id);

        void Delete(Guid id);

        bool IsExist(Guid? id, Guid groupId, DateTime date);

        List<string> GetKeywords();

        List<GroupChartDto> GetChartData(Guid groupId, int typeId);

        List<GroupChartDto> GetProfitLossChartData(Guid groupId, int typeId);

        List<GroupMontlyBreakupDto> GetMonthlyBreaupData(Guid groupId);

    }
}
