using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Extensions.Logging;

namespace APIEvent.Infra.Data.Repositories
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EventReservationRepository> _logger;


        public EventReservationRepository(IConfiguration configuration, ILogger<EventReservationRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<EventReservation>> GetReservationsAsync()
        {
            try
            {
                var query = "SELECT * FROM EventReservation";

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return (await conn.QueryAsync<EventReservation>(query)).ToList();

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<EventReservation>> GetReservationByTitleAndPersonNameAsync(string personName, string title)
        {
            try
            {
                var query = @"SELECT * FROM EventReservation AS er 
                        INNER JOIN CityEvent AS ce
                        ON ce.idEvent = er.IdEvent
                        WHERE er.PersonName = @personName
                        AND ce.Title LIKE CONCAT('%',@title,'%')";

                var parameters = new DynamicParameters();
                parameters.Add("personName", personName);
                parameters.Add("title", title);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return (await conn.QueryAsync<EventReservation>(query, parameters)).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }


        public async Task<bool> InsertReservationAsync(EventReservation eventReservation)
        {
            try
            {
                var query = "INSERT INTO EventReservation VALUES (@idEvent, @personName, @quantity)";

                var parameters = new DynamicParameters();
                parameters.Add("idEvent", eventReservation.IdEvent);
                parameters.Add("personName", eventReservation.PersonName);
                parameters.Add("quantity", eventReservation.Quantity);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> UpdateReservationQuantityAsync(long idReservation, int quantity)
        {
            try
            {
                var query = "UPDATE EventReservation SET quantity = @quantity WHERE idReservation = @idReservation";

                var parameters = new DynamicParameters();
                parameters.Add("idReservation", idReservation);
                parameters.Add("quantity", quantity);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteReservationAsync(long idReservation)
        {
            try
            {
                var query = "DELETE FROM EventReservation WHERE idReservation = @idReservation";

                var parameters = new DynamicParameters();
                parameters.Add("idReservation", idReservation);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return await conn.ExecuteAsync(query, parameters) == 1;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Tipo da exceção {ex.GetType().Name}, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

    }
}
