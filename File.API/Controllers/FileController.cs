using File.API.Dto;
using File.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace File.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileTableService service;
        public FileController(IFileTableService _service)
        {
            this.service = _service;
        }

        [HttpGet("{key}")]
        public ResponseDto<FileTableDto> GetByKey(string key)
        {
            var fileVal = service.GetFileByKey(key);
            return new ResponseDto<FileTableDto>() { Result = fileVal, IsSuccess = true };
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
