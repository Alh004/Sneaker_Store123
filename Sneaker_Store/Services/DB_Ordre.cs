using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public class DB_Ordre : IOrderRepository
    {
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IKundeRepository _kundeRepository;
        private readonly ISkoRepository _skoRepository;

        public DB_Ordre(IHttpContextAccessor httpContextAccessor, IKundeRepository kundeRepository, ISkoRepository skoRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _kundeRepository = kundeRepository;
            _skoRepository = skoRepository;
        }
        private int GetKundeIdFromSession()
        {
            var email = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }
            var kunde = _kundeRepository.GetByEmail(email);
            return kunde?.KundeId ?? throw new Exception("Customer not found.");
        }

        public Sko GetBySkoIdSko(int id)
        {
            return _skoRepository.GetById(id);
        }

        public void AddOrdre(Ordre ordre)
        {
            ordre.KundeId = GetKundeIdFromSession();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Ordre (KundeID, SkoID, Antal, TotalPris) OUTPUT INSERTED.OrdreID VALUES (@KundeID, @SkoID, @Antal, @TotalPris)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@KundeID", ordre.KundeId);
                    cmd.Parameters.AddWithValue("@SkoID", ordre.SkoId);
                    cmd.Parameters.AddWithValue("@Antal", ordre.Antal);
                    cmd.Parameters.AddWithValue("@TotalPris", ordre.TotalPris);
                    ordre.OrdreId = (int)cmd.ExecuteScalar();
                }
            }
        }
        public int CreateOrder()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Orders DEFAULT VALUES; SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // ExecuteScalar bruges til at få den genererede ordre-ID tilbage
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void AddSkoToOrder(int orderId, int skoId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO OrderDetails (OrderId, SkoId) VALUES (@OrderId, @SkoId)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@SkoId", skoId);
                    cmd.ExecuteNonQuery(); // Udfører den ikke-returnerende forespørgsel
                }
            }
        }

        public Ordre GetById(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Ordre WHERE OrdreID = @OrdreID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@OrdreID", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadOrdre(reader);
                        }
                        else
                        {
                            throw new KeyNotFoundException("Order not found.");
                        }
                    }
                }
            }
        }

        public List<Ordre> GetOrdersByCustomerId(int customerId)
        {
            List<Ordre> ordrer = new List<Ordre>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Ordre WHERE KundeID = @KundeID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@KundeID", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ordre ordre = ReadOrdre(reader);
                            ordrer.Add(ordre);
                        }
                    }
                }
            }

            return ordrer;
        }

        public List<Ordre> GetAllOrders()
        {
            List<Ordre> ordrer = new List<Ordre>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Ordre";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ordre ordre = ReadOrdre(reader);
                            ordrer.Add(ordre);
                        }
                    }
                }
            }

            return ordrer;
        }


        private Ordre ReadOrdre(SqlDataReader reader)
        {
            return new Ordre
            {
                OrdreId = reader.GetInt32(reader.GetOrdinal("OrdreID")),
                KundeId = reader.GetInt32(reader.GetOrdinal("KundeID")),
                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID")),
                Antal = reader.GetInt32(reader.GetOrdinal("Antal")),
                TotalPris = reader.GetDecimal(reader.GetOrdinal("TotalPris"))
            };
        }
    }
}
