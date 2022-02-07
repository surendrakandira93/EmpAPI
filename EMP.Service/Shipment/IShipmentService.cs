using EMP.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Service
{
    public interface IShipmentService
    {
        void Add(ShipmentAddDto dto);

        void Update(ShipmentAddDto dto);

        List<ShipmentDto> GetAll();

        ShipmentDto GetById(Guid id);

        void Delete(Guid id);

        void UPDateToLive(Guid id);

        void UPDateToNonLive(Guid id);
    }
}
