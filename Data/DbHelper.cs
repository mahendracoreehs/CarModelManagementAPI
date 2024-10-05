using System.Data;
using System.Data.SqlClient;

namespace CarModelManagementAPI.Data
{
    public class DbHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public DataSet ExecuteStoredProcedure(string procName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        public SqlDataReader ExecuteReader(string procName, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(procName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open(); 

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }

}
