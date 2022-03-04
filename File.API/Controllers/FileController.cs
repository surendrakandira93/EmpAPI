using File.API.Dto;
using File.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace File.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileTableService service;
        private readonly IConfiguration Configuration;
        public FileController(IFileTableService _service, IConfiguration _configuration)
        {
            this.service = _service;
            this.Configuration = _configuration;
        }

        [HttpGet("{key}")]
        public ResponseDto<FileTableDto> GetByKey(string key)
        {
            var fileVal = service.GetFileByKey(key);
            return new ResponseDto<FileTableDto>() { Result = fileVal, IsSuccess = true };
        }

        [HttpGet]
        public ResponseDto<List<string>> GetAllKey()
        {
            var fileVal = service.GetAllKey();
            return new ResponseDto<List<string>>() { Result = fileVal, IsSuccess = true };
        }

        [HttpGet]
        public ResponseDto<List<string>> CreateFreshDBFile()
        {
            string sourceFile = $"{this.Configuration.GetSection("DatabasePath").Value}\\FileStore-templete.db";
            string destinationFile = $"{this.Configuration.GetSection("DatabasePath").Value}\\FileStore.db";
            try
            {
                (new FileInfo(destinationFile)).Delete();
                (new FileInfo(sourceFile)).CopyTo(destinationFile, true);
                return new ResponseDto<List<string>>() { Message = "Created new db", IsSuccess = true };
            }
            catch
            {
                return new ResponseDto<List<string>>() { Message = "Not created new db", IsSuccess = true };
            }

        }


        [HttpGet]
        public ResponseDto<List<FileTableDto>> GetAll()
        {
            var fileVal = service.GetAllFile();
            return new ResponseDto<List<FileTableDto>>() { Result = fileVal, IsSuccess = true };
        }

        [HttpPost]

        public ResponseDto<string> AddUpdate(FileTableDto dto)
        {
            if (dto == null)
            {
                return new ResponseDto<string>() { Message = "Required filds missing", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            if (string.IsNullOrEmpty(dto.Key) || string.IsNullOrEmpty(dto.Value))
            {
                return new ResponseDto<string>() { Message = "Key and Value is required", IsSuccess = false, Code = HttpStatusCode.BadRequest };
            }

            service.AddUpdateFile(dto);

            return new ResponseDto<string>() { Message = "File created", IsSuccess = true };
        }


        [HttpDelete("{key}")]
        public ResponseDto<string> RemoveByKey(string key)
        {
            service.RemoveFile(key);
            return new ResponseDto<string>() { Message = "File Removed", IsSuccess = true };
        }
    }
}
