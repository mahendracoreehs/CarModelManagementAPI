using CarModelManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarModelManagementAPI.Services.Contract
{
    public interface ICarModelServices
    {
        Task<List<CarModelRespDto>> GetCarModelsList();
        void AddCarModel(CarModel carModel);
    }
}
