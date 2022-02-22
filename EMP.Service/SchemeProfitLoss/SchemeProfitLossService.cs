using EMP.Data;
using EMP.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Service
{
    public class SchemeProfitLossService : ISchemeProfitLossService
    {
        public void AddRange(List<SchemeProfitLossDto> dto)
        {
            using (var db = new EmpContext())
            {

                db.AddRange(dto.Select(x => new SchemeProfitLoss()
                {
                    Id = Guid.NewGuid(),
                    Expense = x.Expense,
                    Date = x.Date,
                    IsHoliday = x.IsHoliday,
                    IsNoTradeDay = x.IsNoTradeDay,
                    Comments = x.Comments,
                    Created = DateTime.Now,
                    GroupId = x.GroupId,
                    KeyWord = x.KeyWord,
                    Modified = DateTime.Now,
                    ProfitLoss = x.ProfitLoss
                }));
                db.SaveChanges();

            }
        }

        public void Add(SchemeProfitLossDto dto)
        {
            using (var db = new EmpContext())
            {

                SchemeProfitLoss schemeProft = new SchemeProfitLoss();
                schemeProft.Id = Guid.NewGuid();
                schemeProft.Comments = dto.Comments;
                schemeProft.Date = dto.Date;
                schemeProft.Expense = dto.Expense;
                schemeProft.IsHoliday = dto.IsHoliday;
                schemeProft.IsNoTradeDay = dto.IsNoTradeDay;
                schemeProft.KeyWord = dto.KeyWord;
                schemeProft.ProfitLoss = dto.ProfitLoss;
                schemeProft.GroupId = dto.GroupId;
                schemeProft.Created = DateTime.Now;
                schemeProft.Modified = DateTime.Now;
                db.Add(schemeProft);
                db.SaveChanges();

            }
        }

        public void Update(SchemeProfitLossDto dto)
        {
            using (var db = new EmpContext())
            {
                var schemeProft = db.SchemeProfitLoss.FirstOrDefault(x => x.Id == dto.Id);
                schemeProft.Comments = dto.Comments;
                schemeProft.Date = dto.Date;
                schemeProft.Expense = dto.Expense;
                schemeProft.IsHoliday = dto.IsHoliday;
                schemeProft.IsNoTradeDay = dto.IsNoTradeDay;
                schemeProft.KeyWord = dto.KeyWord;
                schemeProft.ProfitLoss = dto.ProfitLoss;
                schemeProft.Modified = DateTime.Now;
                db.SaveChanges();

            }
        }

        public List<SchemeProfitLossDto> GetAll(Guid groupId)
        {
            using (var db = new EmpContext())
            {
                return db.SchemeProfitLoss.Where(x => x.GroupId == groupId).OrderByDescending(o => o.Date).Select(x => new SchemeProfitLossDto()
                {
                    Comments = x.Comments,
                    GroupId = x.GroupId,
                    ProfitLoss = x.ProfitLoss,
                    KeyWord = x.KeyWord,
                    IsNoTradeDay = x.IsNoTradeDay,
                    IsHoliday = x.IsHoliday,
                    Date = x.Date,
                    Expense = x.Expense,
                    Id = x.Id
                }).ToList();
            }
        }

        public SchemeProfitLossDto GetById(Guid id)
        {
            using (var db = new EmpContext())
            {
                return db.SchemeProfitLoss.Where(x => x.Id == id).Select(x => new SchemeProfitLossDto()
                {
                    Comments = x.Comments,
                    GroupId = x.GroupId,
                    ProfitLoss = x.ProfitLoss,
                    KeyWord = x.KeyWord,
                    IsNoTradeDay = x.IsNoTradeDay,
                    IsHoliday = x.IsHoliday,
                    Date = x.Date,
                    Expense = x.Expense,
                    Id = x.Id

                }).FirstOrDefault();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = new EmpContext())
            {


                var shipment = db.SchemeProfitLoss.FirstOrDefault(x => x.Id == id);
                db.SchemeProfitLoss.Remove(shipment);
                db.SaveChanges();

            }
        }

        public bool IsExist(Guid? id, Guid groupId, DateTime date)
        {
            using (var db = new EmpContext())
            {
                return db.SchemeProfitLoss.Any(x => x.Id != id && x.GroupId == groupId && x.Date.Date == date);

            }
        }

        public List<string> GetKeywords()
        {
            List<string> keyResponse = new List<string>();
            using (var db = new EmpContext())
            {
                List<string> keywords = db.SchemeProfitLoss.Where(w => !string.IsNullOrEmpty(w.KeyWord)).Select(x => x.KeyWord).ToList();
                foreach (var item in keywords)
                {
                    keyResponse.AddRange(item.Split(","));
                }

            }
            return keyResponse.Distinct().ToList();
        }

        public List<GroupChartDto> GetChartData(Guid groupId, int typeId)
        {
            using (var db = new EmpContext())
            {
                var response = db.SchemeProfitLoss.Where(x => x.GroupId == groupId).OrderBy(o => o.Date).Select(x => new GroupChartDto()
                {
                    DailyPnL = x.ProfitLoss,
                    Date = x.Date,
                    AggregateSum = x.Expense
                }).ToList();

                if (typeId == 2)
                {
                    response = response.GroupBy(x => GetWeekEndDate(x.Date)).Select(x => new GroupChartDto
                    {
                        Date = x.Key,
                        Day = x.Key.ToString("dd MMM"),
                        DailyPnL = x.Sum(s => s.DailyPnL),
                        AggregateSum = x.Sum(s => s.AggregateSum)
                    }).ToList();
                }
                else if (typeId == 3)
                {
                    response = response.GroupBy(x => GetMonthEndDate(x.Date)).Select(x => new GroupChartDto
                    {
                        Date = x.Key,
                        Day = x.Key.ToString("MMM yyyy"),
                        DailyPnL = x.Sum(s => s.DailyPnL),
                        AggregateSum = x.Sum(s => s.AggregateSum)
                    }).ToList();
                }

                return response;


            }
        }

        public List<GroupChartDto> GetProfitLossChartData(Guid groupId, int typeId)
        {
            using (var db = new EmpContext())
            {
                var response = db.SchemeProfitLoss.Where(x => x.GroupId == groupId).OrderBy(o => o.Date).Select(x => new GroupChartDto()
                {
                    Date = x.Date,
                    Day = x.Date.ToString("MMM dd,yyyy"),
                    DailyPnL = x.ProfitLoss
                }).ToList();

                if (typeId == 2)
                {
                    response = response.GroupBy(x => GetWeekEndDate(x.Date)).Select(x => new GroupChartDto
                    {
                        Date = x.Key,
                        Day = x.Key.ToString("dd MMM"),
                        DailyPnL = x.Sum(s => s.DailyPnL)
                    }).ToList();
                }
                else if (typeId == 3)
                {
                    response = response.GroupBy(x => GetMonthEndDate(x.Date)).Select(x => new GroupChartDto
                    {
                        Date = x.Key,
                        Day = x.Key.ToString("MMM yyyy"),
                        DailyPnL = x.Sum(s => s.DailyPnL)
                    }).ToList();
                }

                return response;


            }
        }

        public List<GroupMontlyBreakupDto> GetMonthlyBreaupData(Guid groupId)
        {
            List<GroupMontlyBreakupDto> result = new List<GroupMontlyBreakupDto>();
            using (var db = new EmpContext())
            {
                var response = db.SchemeProfitLoss.Where(x => x.GroupId == groupId).OrderBy(o => o.Date).Select(x => new GroupChartDto()
                {
                    DailyPnL = x.ProfitLoss,
                    Date = x.Date,
                    AggregateSum = x.Expense
                }).ToList();

                response = response.GroupBy(x => GetMonthEndDate(x.Date)).Select(x => new GroupChartDto
                {
                    Date = x.Key,
                    Day = x.Key.ToString("MMM yyyy"),
                    DailyPnL = x.Sum(s => s.DailyPnL),
                    AggregateSum = x.Sum(s => s.AggregateSum)
                }).ToList();

                int miniYear = response.Min(m => m.Date.Year), maxYear = response.Max(m => m.Date.Year);
                for (int i = miniYear; i <= maxYear; i++)
                {
                    GroupMontlyBreakupDto dto = new GroupMontlyBreakupDto()
                    {
                        Year = i
                    };
                    for (int j = 1; j <= 12; j++)
                    {
                        var record = response.Where(x => x.Date.Month == j && x.Date.Year == i).FirstOrDefault();
                        dto.Monthly.Add(record != null ? record : new GroupChartDto()
                        {
                            AggregateSum = 0,
                            DailyPnL = 0,
                            Date = new DateTime(i, j, 1),
                            Day = ""
                        });
                    }

                    dto.Total = dto.Monthly.Sum(s => s.DailyPnL);
                    result.Add(dto);
                }


                return result;


            }
        }

        private DateTime GetWeekEndDate(DateTime time)
        {
            var datetime = time.AddDays((7 - (time.DayOfWeek == DayOfWeek.Sunday ? 0 : (int)time.DayOfWeek)));
            return datetime;
        }

        private DateTime GetMonthEndDate(DateTime time)
        {
            var datetime = ((new DateTime(time.Year, time.Month, 01)).AddMonths(1)).AddDays(-1);
            return datetime;
        }
    }
}
