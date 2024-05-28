using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;

namespace Sneaker_Store.Services
{
    public class SkoRepository : ISkoRepository
    {
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

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

            return skos;
        }

        public Sko GetById(int skoid)
        {
            Sko sko = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT SkoID, Maerke, Model, STORRELSE, Pris, ImageUrl FROM skoer WHERE SkoID = @SkoID", conn))
                {
                    cmd.Parameters.AddWithValue("@SkoID", skoid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sko = ReadSko(reader);
                        }
                    }
                }
            }

            return sko;
        }

        public Sko ReadSko(SqlDataReader reader)
        {
            return new Sko
            {
                SkoId = reader.GetInt32(reader.GetOrdinal("SkoID")),
                Maerke = reader.GetString(reader.GetOrdinal("Maerke")),
                Model = reader.GetString(reader.GetOrdinal("Model")),
                Str = reader.IsDBNull(reader.GetOrdinal("STORRELSE")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("STORRELSE")),
                Pris = reader.GetDecimal(reader.GetOrdinal("Pris")),
                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
            };
        }
    }
}