using APIEvent.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using APIEvent.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace APIEvent.Infra.Data.Repositories
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CityEventRepository> _logger;

        public CityEventRepository(IConfiguration configuration, ILogger<CityEventRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<CityEvent>> GetEventsAsync()
        {
            try
            {
                var query = "SELECT * FROM CityEvent";

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return (await conn.QueryAsync<CityEvent>(query)).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<CityEvent>> GetEventByTitleAsync(string title)
        {
            try
            {
                var query = "SELECT * FROM CityEvent WHERE title LIKE CONCAT('%',@title,'%')";

                var parameters = new DynamicParameters();
                parameters.Add("title", title);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<CityEvent>> GetEventByLocalDateAsync(string local, DateTime dateHourEvent)
        {
            try
            {
                var query = "SELECT * FROM CityEvent WHERE local = @local AND CAST(dateHourEvent as DATE) = CAST(@dateHourEvent as DATE)";

                var parameters = new DynamicParameters();
                parameters.Add("local", local);
                parameters.Add("dateHourEvent", dateHourEvent);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<CityEvent>> GetEventByPriceRangeDateAsync(decimal min, decimal max, DateTime dateHourEvent)
        {
            try
            {
                var query = "SELECT * FROM CityEvent WHERE (price BETWEEN @min AND @max) AND (CAST(dateHourEvent as DATE) = CAST(@dateHourEvent as DATE))";

                var parameters = new DynamicParameters();
                parameters.Add("min", min);
                parameters.Add("max", max);
                parameters.Add("dateHourEvent", dateHourEvent);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }


        public async Task<bool> InsertEventAsync(CityEvent cityEvent)
        {
            try
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

                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> UpdateEventAsync(long idEvent, CityEvent cityEvent)
        {
            try
            {
                var query = "UPDATE CityEvent SET title = @title, description = @description, dateHourEvent = @dateHourEvent, local = @local, address = @address, price = @price, @status = status WHERE idEvent = @idEvent";

                var parameters = new DynamicParameters(cityEvent);
                cityEvent.IdEvent = idEvent;

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteEventAsync(long idEvent)
        {
            try
            {
                var query = "DELETE FROM CityEvent WHERE idEvent = @idEvent";

                var parameters = new DynamicParameters();
                parameters.Add("idEvent", idEvent);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> UpdateEventStatusAsync(long idEvent)
        {
            try
            {
                var query = "UPDATE CityEvent SET status = 0 WHERE idEvent = @idEvent";

                var parameters = new DynamicParameters();
                parameters.Add("idEvent", idEvent);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> CheckReservation(long idEvent)
        {
            try
            {
                var query = "SELECT * FROM EventReservation WHERE idEvent = @idEvent";

                var parameters = new DynamicParameters();
                parameters.Add("idEvent", idEvent);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return (await conn.QueryAsync<CityEvent>(query, parameters)).ToList().Count > 0;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> CheckStatus(long idEvent)
        {
            try
            {
                var query = "SELECT * FROM CityEvent WHERE idEvent = @idEvent";

                var parameters = new DynamicParameters();
                parameters.Add("idEvent", idEvent);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return (await conn.QueryFirstOrDefaultAsync<CityEvent>(query, parameters)).Status;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }
    }
}
