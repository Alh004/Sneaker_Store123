using Microsoft.Data.SqlClient; // Tilføjelse af namespace for SQL-forbindelser
using Sneaker_Store.Model; // Tilføjelse af namespace for modelklasser
using System.Collections.Generic; // Tilføjelse af namespace for generiske lister

namespace Sneaker_Store.Services
{
    // Implementering af IOrderRepository interfacet
    public class DB_Ordre : IOrderRepository
    {
        // Forbindelsesstreng til databasen
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // Metode til at tilføje en ordre
        public void AddOrdre(Ordre ordre)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Ordre (KundeID, SkoID) OUTPUT INSERTED.OrdreID VALUES (@KundeID, @SkoID)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Tilføjelse af parametre til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@KundeID", ordre.KundeId);
                    cmd.Parameters.AddWithValue("@SkoID", ordre.SkoId);
                    // Udfører kommandoen og sætter det genererede OrdreID til ordren
                    ordre.OrdreId = (int)cmd.ExecuteScalar();
                }
            }
        }

        // Metode til at hente en ordre efter ID
        public Ordre GetById(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Ordre WHERE OrdreID = @OrdreID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Tilføjelse af parameter til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@OrdreID", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Returnerer en ordre læst fra datareader
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

        // Metode til at hente ordrer efter kunde-ID
        public List<Ordre> GetOrdersByCustomerId(int customerId)
        {
            List<Ordre> ordrer = new List<Ordre>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Ordre WHERE KundeID = @KundeID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Tilføjelse af parameter til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@KundeID", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Tilføjer hver fundet ordre til listen
                            Ordre ordre = ReadOrdre(reader);
                            ordrer.Add(ordre);
                        }
                    }
                }
            }

            return ordrer;
        }

        // Metode til at hente alle ordrer
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
                            // Tilføjer hver fundet ordre til listen
                            Ordre ordre = ReadOrdre(reader);
                            ordrer.Add(ordre);
                        }
                    }
                }
            }

            return ordrer;
        }

        // Metode til at oprette en ny ordre
        public int CreateOrder(int kundeId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Ordre (KundeID) OUTPUT INSERTED.OrdreID VALUES (@KundeID)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Tilføjelse af parameter til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@KundeID", kundeId);
                    // Returnerer det genererede OrdreID
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Metode til at tilføje sko til en ordre
        public void AddSkoToOrder(int orderId, int skoId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Ordre (OrdreID, SkoID) VALUES (@OrdreID, @SkoID)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Tilføjelse af parametre til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@OrdreID", orderId);
                    cmd.Parameters.AddWithValue("@SkoID", skoId);
                    // Udfører kommandoen
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Metode til at hente sko i en ordre
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
                    // Tilføjelse af parameter til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@OrdreID", orderId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Tilføjer hver fundet sko til listen
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

        // Privat metode til at læse en ordre fra SqlDataReader
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
