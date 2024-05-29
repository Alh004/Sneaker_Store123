using Sneaker_Store.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public class KundeRepository : IKundeRepository
    {
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public Kunde? KundeLoggedIn { get; private set; }

        public void AddKunde(Kunde kunde)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "INSERT INTO Kunder (Fornavn, Efternavn, Email, Adgangskode, Postnr, Adresse, Admin) VALUES (@Fornavn, @Efternavn, @Email, @Adgangskode, @Postnr, @Adresse, @Admin)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Fornavn", kunde.Navn);
                    cmd.Parameters.AddWithValue("@Efternavn", kunde.Efternavn);
                    cmd.Parameters.AddWithValue("@Email", kunde.Email);
                    cmd.Parameters.AddWithValue("@Adgangskode", kunde.Kode);
                    cmd.Parameters.AddWithValue("@Postnr", kunde.Postnr);
                    cmd.Parameters.AddWithValue("@Adresse", kunde.Adresse);
                    cmd.Parameters.AddWithValue("@Admin", kunde.Admin);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Kunde GetById(int kundeId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Kunde
                            {
                                KundeId = reader.GetInt32(reader.GetOrdinal("KundeId")),
                                Navn = reader.GetString(reader.GetOrdinal("Fornavn")),
                                Efternavn = reader.GetString(reader.GetOrdinal("Efternavn")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Adresse = reader.GetString(reader.GetOrdinal("Adresse")),
                                Postnr = reader.GetInt32(reader.GetOrdinal("Postnr")),
                                Kode = reader.GetString(reader.GetOrdinal("Adgangskode")),
                                Admin = reader.GetBoolean(reader.GetOrdinal("Admin"))
                            };
                        }
                    }
                }
            }
            throw new KeyNotFoundException("Kunde not found");
        }

        public List<Kunde> GetAll()
        {
            var kunder = new List<Kunde>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kunder.Add(new Kunde
                            {
                                KundeId = reader.GetInt32(reader.GetOrdinal("KundeId")),
                                Navn = reader.GetString(reader.GetOrdinal("Fornavn")),
                                Efternavn = reader.GetString(reader.GetOrdinal("Efternavn")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Adresse = reader.GetString(reader.GetOrdinal("Adresse")),
                                Postnr = reader.GetInt32(reader.GetOrdinal("Postnr")),
                                Kode = reader.GetString(reader.GetOrdinal("Adgangskode")),
                                Admin = reader.GetBoolean(reader.GetOrdinal("Admin"))
                            });
                        }
                    }
                }
            }
            return kunder;
        }

        public Kunde GetByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Kunde
                            {
                                KundeId = reader.GetInt32(reader.GetOrdinal("KundeId")),
                                Navn = reader.GetString(reader.GetOrdinal("Fornavn")),
                                Efternavn = reader.GetString(reader.GetOrdinal("Efternavn")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Adresse = reader.GetString(reader.GetOrdinal("Adresse")),
                                Postnr = reader.GetInt32(reader.GetOrdinal("Postnr")),
                                Kode = reader.GetString(reader.GetOrdinal("Adgangskode")),
                                Admin = reader.GetBoolean(reader.GetOrdinal("Admin"))
                            };
                        }
                    }
                }
            }
            throw new KeyNotFoundException("Kunde not found");
        }

        public LoginResult? CheckKunde(string email, string kode)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE Email = @Email AND Adgangskode = @Adgangskode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Adgangskode", kode);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            KundeLoggedIn = new Kunde
                            {
                                KundeId = reader.GetInt32(reader.GetOrdinal("KundeId")),
                                Navn = reader.GetString(reader.GetOrdinal("Fornavn")),
                                Efternavn = reader.GetString(reader.GetOrdinal("Efternavn")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Adresse = reader.GetString(reader.GetOrdinal("Adresse")),
                                Postnr = reader.GetInt32(reader.GetOrdinal("Postnr")),
                                Kode = reader.GetString(reader.GetOrdinal("Adgangskode")),
                                Admin = reader.GetBoolean(reader.GetOrdinal("Admin"))
                            };
                            return new LoginResult
                            {
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("Admin"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void RemoveKunde(int kundeId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "DELETE FROM Kunder WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Kunde UpdateKunde(int kundeId, Kunde kunde)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "UPDATE Kunder SET Fornavn = @Fornavn, Efternavn = @Efternavn, Email = @Email, Adgangskode = @Adgangskode, Postnr = @Postnr, Adresse = @Adresse, Admin = @Admin WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Fornavn", kunde.Navn);
                    cmd.Parameters.AddWithValue("@Efternavn", kunde.Efternavn);
                    cmd.Parameters.AddWithValue("@Email", kunde.Email);
                    cmd.Parameters.AddWithValue("@Adgangskode", kunde.Kode);
                    cmd.Parameters.AddWithValue("@Postnr", kunde.Postnr);
                    cmd.Parameters.AddWithValue("@Adresse", kunde.Adresse);
                    cmd.Parameters.AddWithValue("@Admin", kunde.Admin);
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);
                    cmd.ExecuteNonQuery();
                }
            }
            return kunde;
        }

        public void LogoutKunde()
        {
            KundeLoggedIn = null;
        }

        public Kunde Remove(int kundeId)
        {
            var kunde = GetById(kundeId);
            RemoveKunde(kundeId);
            return kunde;
        }

        public Kunde Update(int nytKundeId, Kunde kunde)
        {
            if (nytKundeId != kunde.KundeId)
            {
                throw new ArgumentException("Kan ikke opdatere KundeId, de er forskellige.");
            }

            return UpdateKunde(nytKundeId, kunde);
        }

        public List<Kunde> GetAllKunderSortedByNavnReversed()
        {
            var kunder = GetAll();
            kunder.Sort((x, y) => string.Compare(y.Navn, x.Navn, StringComparison.Ordinal));
            return kunder;
        }
    }
}
