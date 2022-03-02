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

        public void DeleteByGroupId(Guid groupId)
        {
            using (var db = new EmpContext())
            {


                var shipment = db.SchemeProfitLoss.Where(x => x.GroupId == groupId).ToList();
                db.SchemeProfitLoss.RemoveRange(shipment);
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

        public List<GroupChartDto> GetChartData(Guid groupId, int typeId, DateTime? fromDate, DateTime? toDate)
        {
            using (var db = new EmpContext())
            {
                IQueryable<SchemeProfitLoss> queryable = db.SchemeProfitLoss.Where(x => x.GroupId == groupId
                && (fromDate.HasValue && toDate.HasValue ? x.Date.Date >= fromDate.Value.Date && x.Date.Date <= toDate.Value.Date : true)
                ).OrderBy(o => o.Date);
                var response = queryable.Select(x => new GroupChartDto()
                {
                    DailyPnL = x.ProfitLoss,
                    Date = x.Date,
                    AggregateSum = queryable.Where(w => w.Date.Date <= x.Date.Date).Sum(s => s.ProfitLoss)
                }).ToList();

                if (typeId == 2)
                {
                    response = response.GroupBy(x => GetWeekEndDate(x.Date)).Select(x => new GroupChartDto
                    {
                        Date = x.Key,
                        Day = x.Key.ToString("dd MMM"),
                        DailyPnL = x.Sum(s => s.DailyPnL),
                        AggregateSum = queryable.Where(w => w.Date.Date <= x.Key.Date).Sum(s => s.ProfitLoss)
                    }).ToList();
                }
                else if (typeId == 3)
                {
                    response = response.GroupBy(x => GetMonthEndDate(x.Date)).Select(x => new GroupChartDto
                    {
                        Date = x.Key,
                        Day = x.Key.ToString("MMM yyyy"),
                        DailyPnL = x.Sum(s => s.DailyPnL),
                        AggregateSum = queryable.Where(w => w.Date.Date <= x.Key.Date).Sum(s => s.ProfitLoss)
                    }).ToList();
                }

                return response.Any() ? response.ToList() : new List<GroupChartDto>();


            }
        }

        public List<GroupChartDto> GetProfitLossChartData(Guid groupId, int typeId, DateTime? fromDate, DateTime? toDate)
        {
            using (var db = new EmpContext())
            {
                var response = db.SchemeProfitLoss.Where(x => x.GroupId == groupId
                && (fromDate.HasValue && toDate.HasValue ? x.Date.Date >= fromDate.Value.Date && x.Date.Date <= toDate.Value.Date : true)
                ).OrderBy(o => o.Date).Select(x => new GroupChartDto()
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

                return response.Any() ? response.ToList() : new List<GroupChartDto>();


            }
        }

        public List<GroupMontlyBreakupDto> GetMonthlyBreaupData(Guid groupId, DateTime? fromDate, DateTime? toDate)
        {
            List<GroupMontlyBreakupDto> result = new List<GroupMontlyBreakupDto>();
            using (var db = new EmpContext())
            {
                int miniYear = fromDate.HasValue ? fromDate.Value.Year : 0, maxYear = toDate.HasValue ? toDate.Value.Year : 0;
                var response = db.SchemeProfitLoss.Where(x => x.GroupId == groupId &&
                (fromDate.HasValue && toDate.HasValue ? x.Date.Date >= fromDate.Value.Date && x.Date.Date <= toDate.Value.Date : true)
                ).OrderBy(o => o.Date).Select(x => new GroupChartDto()
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
                if (response != null && response.Any())
                {
                    miniYear = (miniYear == 0 ? response.Min(m => m.Date.Year) : miniYear);
                    maxYear = (maxYear==0?response.Max(m => m.Date.Year): maxYear);
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
                }
                else
                {
                    for (int i = miniYear; i <= maxYear; i++)
                    {
                        GroupMontlyBreakupDto dto = new GroupMontlyBreakupDto()
                        {
                            Year = i
                        };
                        for (int j = 1; j <= 12; j++)
                        {
                            
                            dto.Monthly.Add(new GroupChartDto()
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
                }

                return result.Any() ? result.ToList() : new List<GroupMontlyBreakupDto>();


            }
        }

        public List<HeatmapResponseDto> GetCal_HeatmapData(Guid groupId, DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(10);
            using (var db = new EmpContext())
            {
                List<SchemeProfitLoss> queryable = db.SchemeProfitLoss.Where(x => x.GroupId == groupId
                && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date
                ).OrderBy(o => o.Date).ToList();

                List<HeatmapResponseDto> result = new List<HeatmapResponseDto>();
                DateTime d = fromDate.Date;
                while (d <= toDate.Date)
                {
                    if (queryable.Any(x => x.Date.Date == d))
                    {
                        result.Add(queryable.Where(x => x.Date.Date == d).Select(s => new HeatmapResponseDto()
                        {
                            Date = GetUnixTimestamp(s.Date.Date),
                            Value = s.ProfitLoss
                        }).FirstOrDefault());
                    }

                    d = d.AddDays(1);
                }

                return result.Any() ? result : new List<HeatmapResponseDto>();

            }

        }

        public SchemeProfitLossSummary PLSummary(Guid groupId, DateTime? fromDate, DateTime? toDate)
        {

            using (var db = new EmpContext())
            {
                List<SchemeProfitLoss> queryable = db.SchemeProfitLoss.Where(x => x.GroupId == groupId
                && (fromDate.HasValue && toDate.HasValue ? x.Date.Date >= fromDate.Value.Date && x.Date.Date <= toDate.Value.Date : true)
                ).OrderBy(o => o.Date).ToList();

                double pl = queryable.Sum(a => a.ProfitLoss);
                double ex = queryable.Sum(a => a.Expense);
                double netPL = pl - ex;

                return new SchemeProfitLossSummary()
                {
                    Charge = ex,
                    RealisedPL = pl,
                    NetRealisedPL = pl - ex,
                    UnRealisedPL = 0

                };

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

        private int GetUnixTimestamp(DateTime date)
        {
            int unixTimestamp = (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp;
        }
    }
}
