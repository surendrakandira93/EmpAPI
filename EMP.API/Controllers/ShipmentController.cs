using EMP.Dto;
using EMP.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace EMP.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService service;
        public ShipmentController(IShipmentService _service)
        {
            this.service = _service;
        }

        [HttpGet]
        public ResponseDto<List<ShipmentDto>> GetAll()
        {
            var employees = service.GetAll();
            return new ResponseDto<List<ShipmentDto>>() { Result = employees, IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<ShipmentDto> GetById(Guid id)
        {
            var response = service.GetById(id);
            return new ResponseDto<ShipmentDto>() { Result = response, IsSuccess = true };
        }

        [HttpPost]
        public ResponseDto<string> Create(ShipmentAddDto shipment)
        {
            service.Add(shipment);
            return new ResponseDto<string>() { Message = "Shipment created", IsSuccess = true };
        }

        [HttpPut]
        public ResponseDto<string> Update(ShipmentAddDto shipment)
        {
            service.Update(shipment);
            return new ResponseDto<string>() { Message = "Shipment updated", IsSuccess = true };
        }

        [HttpDelete("{id}")]
        public ResponseDto<string> Delete(Guid id)
        {
            service.Delete(id);
            return new ResponseDto<string>() { Message = "Shipment deleted", IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<string> UPDateToLive(Guid id)
        {
            service.UPDateToLive(id);
            return new ResponseDto<string>() { Message = "Shipment updated", IsSuccess = true };
        }

        [HttpGet("{id}")]
        public ResponseDto<string> UPDateToNonLive(Guid id)
        {
            service.UPDateToNonLive(id);
            return new ResponseDto<string>() { Message = "Shipment updated", IsSuccess = true };
        }
    }
}
