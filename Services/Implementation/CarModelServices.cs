using CarModelManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarModelManagementAPI.Repositories.Contract;
using CarModelManagementAPI.Services.Contract;

namespace CarModelManagementAPI.Services.Implementation
{
    public class CarModelServices : ICarModelServices
    {
        private readonly ICarModelRepository _carModelRepository;
        private readonly string _wwwrootPath; // Declare the variable for the wwwroot path
        public CarModelServices(ICarModelRepository carModelRepository)
        {
            _carModelRepository = carModelRepository;
            _wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        }

        public async Task<List<CarModelRespDto>> GetCarModelsList()
        {
            var carModels = await _carModelRepository.GetCarModels();
            var carModelDtos = new List<CarModelRespDto>(); 

            foreach (var carModel in carModels)
            {
                var dto = new CarModelRespDto
                {
                    Id = carModel.Id,
                    Brand = carModel.Brand,
                    Class = carModel.Class,
                    ModelName = carModel.ModelName,
                    ModelCode = carModel.ModelCode,
                    Description = carModel.Description,
                    Features = carModel.Features,
                    Price = carModel.Price,
                    DateOfManufacturing = carModel.DateOfManufacturing,
                    Active = carModel.Active,
                    SortOrder = carModel.SortOrder,
                    ImageBase64 = await GetImageBase64(carModel.ImageName)
                };

                carModelDtos.Add(dto); // Add the DTO to the list
            }

            return carModelDtos; // Return the list of DTOs
        }

        public void AddCarModel(CarModel carModel)
        {
            _carModelRepository.AddCarModel(carModel);
        }

        private async Task<string> GetImageBase64(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                // Construct the full path to the image
                string imagePath = Path.Combine(_wwwrootPath, imageName);

                if (File.Exists(imagePath))
                {
                    try
                    {
                        // Read the image bytes and convert to Base64
                        byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);
                        return Convert.ToBase64String(imageBytes); // Return the Base64 string
                    }
                    catch (Exception)
                    {
                        return default;
                    }
                }
            }
            return null; // Return null if imageName is null or file doesn't exist
        }

    }
}
