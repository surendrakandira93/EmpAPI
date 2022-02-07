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
    public class ShipmentService : IShipmentService
    {
        public ShipmentService()
        {
            using (var db = new EmpContext())
            {
                //Ensure database is created
                db.Database.EnsureCreated();
            }
        }

        public void Add(ShipmentAddDto dto)
        {
            using (var db = new EmpContext())
            {

                try
                {

                    Shipment shipment = new Shipment();
                    shipment.Id = Guid.NewGuid();
                    shipment.LoginId = dto.LoginId;
                    shipment.Broker = dto.Broker;
                    shipment.Platform = dto.Platform;
                    shipment.IsLive = true;
                    shipment.Expiry = DateTime.Now.AddMonths(1);
                    shipment.Password = dto.Password;
                    shipment.Password2 = dto.Password2;
                    shipment.EmpId = dto.EmpId;
                    shipment.APIKey = dto.APIKey;
                    shipment.Created = DateTime.Now;
                    shipment.Modified = DateTime.Now;
                    db.Add(shipment);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);

                }
            }
        }

        public void Update(ShipmentAddDto dto)
        {
            using (var db = new EmpContext())
            {

                try
                {
                    var shipment = db.Shipment.FirstOrDefault(x => x.Id == dto.Id);
                    shipment.LoginId = dto.LoginId;
                    shipment.Platform = dto.Platform;                    
                    shipment.Broker = dto.Broker;
                    shipment.Password = dto.Password;
                    shipment.Password2 = dto.Password2;
                    shipment.EmpId = dto.EmpId;
                    shipment.APIKey = dto.APIKey;
                    shipment.Modified = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);

                }
            }
        }

        public List<ShipmentDto> GetAll()
        {
            using (var db = new EmpContext())
            {

                return db.Shipment.Select(x => new ShipmentDto()
                {
                    Broker = x.Broker,
                    Expiry = x.Expiry,
                    Id = x.Id,
                    IsLive = x.IsLive,
                    LoginId = x.LoginId,
                    Password = x.Password,
                    Password2 = x.Password2,
                    Platform = x.Platform,
                    APIKey=x.APIKey,
                    EmpId=x.EmpId,
                }).ToList();


            }
        }

        public ShipmentDto GetById(Guid id)
        {
            using (var db = new EmpContext())
            {

                return db.Shipment.Where(x=>x.Id==id).Select(x => new ShipmentDto()
                {
                    Broker = x.Broker,
                    Expiry = x.Expiry,
                    Id = x.Id,
                    IsLive = x.IsLive,
                    LoginId = x.LoginId,
                    Password = x.Password,
                    Password2 = x.Password2,
                    Platform = x.Platform,
                    APIKey = x.APIKey,
                    EmpId=x.EmpId,

                }).FirstOrDefault();


            }
        }

        public void Delete(Guid id)
        {
            using (var db = new EmpContext())
            {

                
                    var shipment = db.Shipment.FirstOrDefault(x => x.Id == id);
                    db.Shipment.Remove(shipment);
                    db.SaveChanges();
                
            }
        }

        public void UPDateToLive(Guid id)
        {
            using (var db = new EmpContext())
            {
                var shipment = db.Shipment.FirstOrDefault(x => x.Id == id);
                shipment.IsLive = true;
                db.SaveChanges();

            }
        }

        public void UPDateToNonLive(Guid id)
        {
            using (var db = new EmpContext())
            {
                var shipment = db.Shipment.FirstOrDefault(x => x.Id == id);
                shipment.IsLive = false;
                db.SaveChanges();

            }
        }
    }
}
