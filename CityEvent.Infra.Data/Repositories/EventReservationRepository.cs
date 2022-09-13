using APIEvent.Core.Interfaces;
using APIEvent.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace APIEvent.Infra.Data.Repositories
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<EventReservation> GetReservations()
        {
            var query = "SELECT * FROM EventReservation";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query).ToList();
        }

        public List<EventReservation> GetReservationByTitleAndPersonName(string personName, string title)
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

            return conn.Query<EventReservation>(query, parameters).ToList();
        }


        public bool InsertReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES (@idEvent, @personName, @quantity)";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", eventReservation.IdEvent);
            parameters.Add("personName", eventReservation.PersonName);
            parameters.Add("quantity", eventReservation.Quantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateReservationQuantity(long idReservation, int quantity)
        {
            var query = "UPDATE EventReservation SET quantity = @quantity WHERE idReservation = @idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);
            parameters.Add("quantity", quantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteReservation(long idReservation)
        {
            var query = "DELETE FROM EventReservation WHERE idReservation = @idReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

    }
}
