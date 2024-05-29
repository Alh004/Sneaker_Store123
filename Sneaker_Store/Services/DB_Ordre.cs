using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public class DB_Ordre : IOrderRepository
    {
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public void AddOrdre(Ordre ordre)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Ordre (KundeID, SkoID) OUTPUT INSERTED.OrdreID VALUES (@KundeID, @SkoID)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@KundeID", ordre.KundeId);
                    cmd.Parameters.AddWithValue("@SkoID", ordre.SkoId);
                    ordre.OrdreId = (int)cmd.ExecuteScalar();
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

        public int CreateOrder(int kundeId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Ordre (KundeID) OUTPUT INSERTED.OrdreID VALUES (@KundeID)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void AddSkoToOrder(int orderId, int skoId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Ordre (OrdreID, SkoID) VALUES (@OrdreID, @SkoID)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@OrdreID", orderId);
                    cmd.Parameters.AddWithValue("@SkoID", skoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Sko> GetSkoInOrder(int orderId)
        {
            List<Sko> skos = new List<Sko>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT s.SkoID, s.Maerke, s.Model, s.STORRELSE, s.Pris, s.ImageUrl " +
                             "FROM Sko s " +
                             "INNER JOIN Ordre o ON s.SkoID = o.SkoID " +
                             "WHERE o.OrdreID = @OrdreID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@OrdreID", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sko sko = new Sko
                            {
                                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID")),
                                Maerke = reader.GetString(reader.GetOrdinal("Maerke")),
                                Model = reader.GetString(reader.GetOrdinal("Model")),
                                Str = reader.IsDBNull(reader.GetOrdinal("STORRELSE")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("STORRELSE")),
                                Pris = reader.GetDecimal(reader.GetOrdinal("Pris")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
                            };
                            skos.Add(sko);
                        }
                    }
                }
            }

            return skos;
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
