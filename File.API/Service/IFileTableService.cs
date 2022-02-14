using File.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File.API.Service
{
    public interface IFileTableService
    {
        void AddUpdateFile(FileTableDto dto);

        FileTableDto GetFileByKey(string key);

        List<FileTableDto> GetAllFile();

        void RemoveFile(string key);

        List<string> GetAllKey();
    }
}
