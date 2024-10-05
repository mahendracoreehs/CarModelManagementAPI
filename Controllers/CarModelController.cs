using CarModelManagementAPI.Models;
using CarModelManagementAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CarModelManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly ICarModelServices _carModelService;
        public CarModelController(ICarModelServices carModelService)
        {
            _carModelService = carModelService;
        }

        [HttpGet("GetCarModels")]
        public async Task<ActionResult<IEnumerable<CarModelRespDto>>> GetCarModels()
        {
            var carModels = await _carModelService.GetCarModelsList();
            return Ok(carModels);
        }

        [HttpPost("AddCarModel")]
        public async Task<IActionResult> AddCarModel([FromForm] CarModelRequestDto model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot/images", model.Image.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                var carModel = new CarModel
                {
                    Brand = model.Brand,
                    Class = model.Class,
                    ModelName = model.ModelName,
                    ModelCode = model.ModelCode,
                    Description = model.Description,
                    Features = model.Features,
                    Price = model.Price,
                    DateOfManufacturing = model.DateOfManufacturing,
                    Active = model.Active,
                    SortOrder = model.SortOrder,
                    ImageName = model.Image.FileName
                };

                _carModelService.AddCarModel(carModel);

                return Ok(new { message = "Car model added successfully!" });
            }

            return BadRequest("Image is required");
        }


    }
}
