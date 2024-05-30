using System; // Tilføjelse af namespace for grundlæggende systemfunktioner
using System.Collections.Generic; // Tilføjelse af namespace for generiske lister
using Microsoft.Data.SqlClient; // Tilføjelse af namespace for SQL-forbindelser
using Sneaker_Store.Model; // Tilføjelse af namespace for modelklasser

namespace Sneaker_Store.Services
{
    // Implementering af ISkoRepository interfacet
    public class DB_Sko : ISkoRepository
    {
        // Forbindelsesstreng til databasen
        private const string ConnectionString = "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // Metode til at tilføje en ny sko
        public Sko Add(Sko newSko)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO skoer (Maerke, Model, STORRELSE, Pris, ImageUrl) OUTPUT INSERTED.SkoID VALUES (@Maerke, @Model, @STORRELSE, @Pris, @ImageUrl)", conn))
                {
                    // Tilføjelse af parametre til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@Maerke", newSko.Maerke);
                    cmd.Parameters.AddWithValue("@Model", newSko.Model);
                    cmd.Parameters.AddWithValue("@STORRELSE", newSko.Str);
                    cmd.Parameters.AddWithValue("@Pris", newSko.Pris);
                    cmd.Parameters.AddWithValue("@ImageUrl", newSko.ImageUrl);

                    // Udfører kommandoen og sætter det genererede SkoID til skoen
                    newSko.SkoId = (int)cmd.ExecuteScalar();
                }
            }
            return newSko;
        }

        // Metode til at hente alle sko
        public List<Sko> GetAll()
        {
            var skos = new List<Sko>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SkoID, Maerke, Model, STORRELSE, Pris, ImageUrl FROM skoer", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Læser alle sko og tilføjer dem til listen
                            while (reader.Read())
                            {
                                var sko = ReadSko(reader);
                                skos.Add(sko);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return skos;
        }

        // Metode til at hente en sko efter ID
        public Sko GetById(int id)
        {
            Sko sko = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SkoID, Maerke, Model, STORRELSE, Pris, ImageUrl FROM skoer WHERE SkoID = @SkoID", conn))
                    {
                        // Tilføjelse af parameter til SQL-kommandoen
                        cmd.Parameters.AddWithValue("@SkoID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Læser skoen hvis fundet
                                sko = ReadSko(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return sko;
        }
        
        // Metode til at læse en sko fra SqlDataReader
        public Sko ReadSko(SqlDataReader reader)
        {
            try
            {
                return new Sko
                {
                    SkoId = reader.GetInt32(0), // Læser SkoID
                    Maerke = reader.GetString(1), // Læser Maerke
                    Model = reader.GetString(2), // Læser Model
                    Str = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3), // Læser STORRELSE, håndterer null-værdi
                    Pris = reader.GetDecimal(4), // Læser Pris
                    ImageUrl = reader.GetString(5) // Læser ImageUrl
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
