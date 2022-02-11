using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace File.API.Dto
{
    public class ResponseDto<T>
    {
        public ResponseDto()
        {
            Code = HttpStatusCode.OK;
        }
        public T Result { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}
