using CarModelManagementAPI.Data;
using CarModelManagementAPI.Models;
using CarModelManagementAPI.Repositories.Contract;
using System.Data.SqlClient;

namespace CarModelManagementAPI.Repositories.Implementation
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly DbHelper _dbHelper;

        public CarModelRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void AddCarModel(CarModel carModel)
        {
            SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Brand", carModel.Brand),
            new SqlParameter("@Class", carModel.Class),
            new SqlParameter("@ModelName", carModel.ModelName),
            new SqlParameter("@ModelCode", carModel.ModelCode),
            new SqlParameter("@Description", carModel.Description),
            new SqlParameter("@Features", carModel.Features),
            new SqlParameter("@Price", carModel.Price),
            new SqlParameter("@DateOfManufacturing", carModel.DateOfManufacturing),
            new SqlParameter("@Active", carModel.Active),
            new SqlParameter("@SortOrder", carModel.SortOrder),
            new SqlParameter("@ImagePath", carModel.ImageName)
                };

            _dbHelper.ExecuteStoredProcedure("InsertCarModel", parameters);

        }

        public async Task<List<CarModel>> GetCarModels()
        {
            List<CarModel> carModels = new List<CarModel>();

            using (var reader = _dbHelper.ExecuteReader("GetCarModels"))
            {
                while (reader.Read())
                {
                    CarModel carModel = new CarModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Brand = reader.GetString(reader.GetOrdinal("Brand")),
                        Class = reader.GetString(reader.GetOrdinal("Class")),
                        ModelName = reader.GetString(reader.GetOrdinal("ModelName")),
                        ModelCode = reader.GetString(reader.GetOrdinal("ModelCode")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Features = reader.GetString(reader.GetOrdinal("Features")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        DateOfManufacturing = reader.GetDateTime(reader.GetOrdinal("DateOfManufacturing")),
                        Active = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        SortOrder = reader.GetInt32(reader.GetOrdinal("SortOrder")),
                        ImageName = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))
                    };

                    carModels.Add(carModel);
                }
            }

            return carModels;
        }
    }
}
