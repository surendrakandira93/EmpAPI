using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Dto
{
    public class SchemeProfitLossDto
    {
        public Guid? Id { get; set; }
        public Guid GroupId { get; set; }
        public DateTime Date { get; set; }
        public string KeyWord { get; set; }
        public double ProfitLoss { get; set; }
        public double Expense { get; set; }
        public string Comments { get; set; }
        public bool IsNoTradeDay { get; set; }
        public bool IsHoliday { get; set; }
    }

    public class SchemeProfitLossSummary
    {
        public double RealisedPL { get; set; }
        public double Charge { get; set; }
        public double NetRealisedPL { get; set; }
        public double UnRealisedPL { get; set; }

    }   
}
