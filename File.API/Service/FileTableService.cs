using File.API.Data;
using File.API.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace File.API.Service
{
    public class FileTableService : IFileTableService
    {
        public void AddUpdateFile(FileTableDto dto)
        {
            using (var db = new FileContext())
            {
                var fileDB = db.FileTable.Where(x => x.Key == dto.Key).FirstOrDefault();
                if (fileDB != null)
                {
                    fileDB.Value = dto.Value;
                }
                else
                {
                    FileTable employee = new FileTable();
                    employee.Key = dto.Key;
                    employee.Value = dto.Value;
                    employee.Id = Guid.NewGuid();
                    db.Add(employee);
                }
                db.SaveChanges();


            }
        }

        public FileTableDto GetFileByKey(string key)
        {
            using (var db = new FileContext())
            {
                return db.FileTable.Where(x => x.Key == key).Select(x => new FileTableDto()
                {
                    Key = x.Key,
                    Value = x.Value
                }).FirstOrDefault();

            }
        }

        public List<FileTableDto> GetAllFile()
        {
            using (var db = new FileContext())
            {
                return db.FileTable.Select(x => new FileTableDto()
                {
                    Key = x.Key,
                    Value = x.Value
                }).ToList();

            }
        }

        public void RemoveFile(string key)
        {
            using (var db = new FileContext())
            {
                var fileDb= db.FileTable.Where(x => x.Key == key).FirstOrDefault();
                db.FileTable.Remove(fileDb);
                db.SaveChanges();
            }
        }
    }
}
