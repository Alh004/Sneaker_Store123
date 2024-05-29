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
                var query = "INSERT INTO Ordre (KundeID, SkoID) OUTPUT INSERTED.OrdreID VALUES (@KundeID, @SkoID)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KundeID", ordre.KundeId);
                    cmd.Parameters.AddWithValue("@SkoID", ordre.SkoId);
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
                                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID"))
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
                                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID"))
                            });
                        }
                    }
                }
            }
            return orders;
        }

        public int CreateOrder(int kundeId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Ordre (KundeID) OUTPUT INSERTED.OrdreID VALUES (@KundeID)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
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
                                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID"))
                            };
                        }
                    }
                }
            }
            throw new KeyNotFoundException("Ordre not found");
        }

        public void AddSkoToOrder(int orderId, int skoId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Ordre (OrdreID, SkoID) VALUES (@OrdreID, @SkoID)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@OrdreID", orderId);
                    cmd.Parameters.AddWithValue("@SkoID", skoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Sko> GetSkoInOrder(int orderId)
        {
            var shoes = new List<Sko>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT s.SkoID, s.Maerke, s.Model, s.STORRELSE, s.Pris, s.ImageUrl " +
                            "FROM Ordre o " +
                            "JOIN skoer s ON o.SkoID = s.SkoID " +
                            "WHERE o.OrdreID = @OrdreID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrdreID", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shoes.Add(new Sko
                            {
                                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID")),
                                Maerke = reader.GetString(reader.GetOrdinal("Maerke")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                Str = reader.IsDBNull(reader.GetOrdinal("STORRELSE")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("STORRELSE")),
                                Pris = reader.GetDecimal(reader.GetOrdinal("Pris")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
                            });
                        }
                    }
                }
            }
            return shoes;
        }
    }
}
