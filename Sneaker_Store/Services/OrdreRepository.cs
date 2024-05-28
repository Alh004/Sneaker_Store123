using Sneaker_Store.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public class OrderRepository : IOrderRepository
    {
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public void AddOrdre(Ordre ordre)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "INSERT INTO Ordre (KundeID, SkoID, Antal, TotalPris) OUTPUT INSERTED.OrdreID VALUES (@KundeID, @SkoID, @Antal, @TotalPris)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KundeID", ordre.KundeId);
                    cmd.Parameters.AddWithValue("@SkoID", ordre.SkoId);
                    cmd.Parameters.AddWithValue("@Antal", ordre.Antal);
                    cmd.Parameters.AddWithValue("@TotalPris", ordre.TotalPris);
                    ordre.OrdreId = (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<Ordre> GetOrdersByCustomerId(int customerId)
        {
            var orders = new List<Ordre>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Ordre WHERE KundeID = @KundeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KundeID", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Ordre
                            {
                                OrdreId = reader.GetInt32(reader.GetOrdinal("OrdreID")),
                                KundeId = reader.GetInt32(reader.GetOrdinal("KundeID")),
                                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID")),
                                Antal = reader.GetInt32(reader.GetOrdinal("Antal")),
                                TotalPris = reader.GetDecimal(reader.GetOrdinal("TotalPris"))
                            });
                        }
                    }
                }
            }
            return orders;
        }

        public List<Ordre> GetAllOrders()
        {
            var orders = new List<Ordre>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Ordre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Ordre
                            {
                                OrdreId = reader.GetInt32(reader.GetOrdinal("OrdreID")),
                                KundeId = reader.GetInt32(reader.GetOrdinal("KundeID")),
                                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID")),
                                Antal = reader.GetInt32(reader.GetOrdinal("Antal")),
                                TotalPris = reader.GetDecimal(reader.GetOrdinal("TotalPris"))
                            });
                        }
                    }
                }
            }
            return orders;
        }
        
        public Ordre GetById(int ordreId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Ordre WHERE OrdreID = @OrdreID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrdreID", ordreId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
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
            }
            throw new KeyNotFoundException("Ordre not found");
        }
    }
}
