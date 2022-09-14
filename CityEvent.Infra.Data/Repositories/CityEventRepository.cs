using APIEvent.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using APIEvent.Core.Interfaces;

namespace APIEvent.Infra.Data.Repositories
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> GetEvents()
        {
            var query = "SELECT * FROM CityEvent";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query).ToList();
        }

        public List<CityEvent> GetEventByTitle(string title)
        {
            var query = "SELECT * FROM CityEvent WHERE title LIKE CONCAT('%',@title,'%')";
            
            var parameters = new DynamicParameters();
            parameters.Add("title", title);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetEventByLocalDate(string local, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE local = @local AND CAST(dateHourEvent as DATE) = CAST(@dateHourEvent as DATE)";

            var parameters = new DynamicParameters();
            parameters.Add("local", local);
            parameters.Add("dateHourEvent", dateHourEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetEventByPriceRangeDate(decimal min, decimal max, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE (price BETWEEN @min AND @max) AND (CAST(dateHourEvent as DATE) = CAST(@dateHourEvent as DATE))";

            var parameters = new DynamicParameters();
            parameters.Add("min", min);
            parameters.Add("max", max);
            parameters.Add("dateHourEvent", dateHourEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }


        public bool InsertEvent(CityEvent cityEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@title, @description, @dateHourEvent, @local, @address, @price, @status)";

            var parameters = new DynamicParameters();
            parameters.Add("title", cityEvent.Title);
            parameters.Add("description", cityEvent.Description);
            parameters.Add("dateHourEvent", cityEvent.DateHourEvent);
            parameters.Add("local", cityEvent.Local);
            parameters.Add("address", cityEvent.Address);
            parameters.Add("price", cityEvent.Price);
            parameters.Add("status", cityEvent.Status);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateEvent(long idEvent, CityEvent cityEvent)
        {
            var query = "UPDATE CityEvent SET title = @title, description = @description, dateHourEvent = @dateHourEvent, local = @local, address = @address, price = @price, @status = status WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters(cityEvent);
            cityEvent.IdEvent = idEvent;

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteEvent(long idEvent)
        {
            var query = "";

            if (CheckReservation(idEvent))
            {
                query = "UPDATE CityEvent SET status = 0 WHERE idEvent = @idEvent";
            }
            else
            {
                query = "DELETE FROM CityEvent WHERE idEvent = @idEvent";
            }

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool CheckReservation(long idEvent)
        {
            var query = "SELECT * FROM EventReservation WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList().Count > 0;
        }

        public bool CheckStatus(long idEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters).Status;
        }

    }
}
