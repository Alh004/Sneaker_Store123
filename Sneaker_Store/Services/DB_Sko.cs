using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;

namespace Sneaker_Store.Services
{
    public class DB_Sko : ISkoRepository
    {
        private const string ConnectionString = "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public Sko Add(Sko newSko)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO skoer (Maerke, Model, STORRELSE, Pris, ImageUrl) OUTPUT INSERTED.SkoID VALUES (@Maerke, @Model, @STORRELSE, @Pris, @ImageUrl)", conn))
                {
                    cmd.Parameters.AddWithValue("@Maerke", newSko.Maerke);
                    cmd.Parameters.AddWithValue("@Model", newSko.Model);
                    cmd.Parameters.AddWithValue("@STORRELSE", newSko.Str);
                    cmd.Parameters.AddWithValue("@Pris", newSko.Pris);
                    cmd.Parameters.AddWithValue("@ImageUrl", newSko.ImageUrl);

                    newSko.SkoId = (int)cmd.ExecuteScalar();
                }
            }
            return newSko;
        }

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
                // Log the exception (here just writing to console, use a logging framework in production)
                Console.WriteLine(ex.Message);
                throw;
            }

            return skos;
        }

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
                        cmd.Parameters.AddWithValue("@SkoID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                sko = ReadSko(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (here just writing to console, use a logging framework in production)
                Console.WriteLine(ex.Message);
                throw;
            }

            return sko;
        }

        public Sko ReadSko(SqlDataReader reader)
        {
            try
            {
                return new Sko
                {
                    SkoId = reader.GetInt32(0),
                    Maerke = reader.GetString(1),
                    Model = reader.GetString(2),
                    Str = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                    Pris = reader.GetDouble(4),  // Use GetDouble for Pris
                    ImageUrl = reader.GetString(5)
                };
            }
            catch (Exception ex)
            {
                // Log the exception (here just writing to console, use a logging framework in production)
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
