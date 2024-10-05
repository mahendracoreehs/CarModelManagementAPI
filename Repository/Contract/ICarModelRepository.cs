using CarModelManagementAPI.Models;

namespace CarModelManagementAPI.Repositories.Contract
{
    public interface ICarModelRepository
    {
        Task<List<CarModel>> GetCarModels();

        void AddCarModel(CarModel carModel);
    }
}
