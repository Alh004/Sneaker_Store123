using Sneaker_Store.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public class DB_Kunde : IKundeRepository
    {
        // Forbindelsesstreng til databasen
        private const string ConnectionString = "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // Egenskab til at holde den loggede ind kunde
        public Kunde? KundeLoggedIn { get; private set; }

        // Metode til at tjekke om en kunde eksisterer med den angivne email og kode
        public LoginResult? CheckKunde(string email, string kode)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE Email = @Email AND Adgangskode = @kode";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@kode", kode);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                KundeLoggedIn = new Kunde
                                {
                                    KundeId = reader.GetInt32(0),
                                    Navn = reader.GetString(1),
                                    Efternavn = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Adresse = reader.GetString(4),
                                    Postnr = reader.GetInt32(5),
                                    Kode = reader.GetString(6),
                                    Admin = reader.GetBoolean(7)
                                };
                                return new LoginResult
                                {
                                    IsAdmin = KundeLoggedIn.Admin
                                };
                            }
                            else
                            {
                                KundeLoggedIn = null;
                                return null;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Log SQL undtagelse
                    Console.WriteLine($"SQL Exception: {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    // Log generel undtagelse
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
                }
            }
        }

        // Metode til at hente en kunde efter deres ID
        public Kunde GetById(int kundeId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Kunde
                            {
                                KundeId = reader.GetInt32(0),
                                Navn = reader.GetString(1),
                                Efternavn = reader.GetString(2),
                                Email = reader.GetString(3),
                                Adresse = reader.GetString(4),
                                Postnr = reader.GetInt32(5),
                                Kode = reader.GetString(6),
                                Admin = reader.GetBoolean(7)
                            };
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                }
            }
        }

        // Metode til at hente alle kunder
        public List<Kunde> GetAll()
        {
            List<Kunde> kunder = new List<Kunde>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Kunde kunde = new Kunde
                            {
                                KundeId = reader.GetInt32(0),
                                Navn = reader.GetString(1),
                                Efternavn = reader.GetString(2),
                                Email = reader.GetString(3),
                                Adresse = reader.GetString(4),
                                Postnr = reader.GetInt32(5),
                                Kode = reader.GetString(6),
                                Admin = reader.GetBoolean(7)
                            };
                            kunder.Add(kunde);
                        }
                    }
                }
            }

            return kunder;
        }

        // Metode til at hente en kunde efter deres email
        public Kunde GetByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Kunde
                            {
                                KundeId = reader.GetInt32(0),
                                Navn = reader.GetString(1),
                                Efternavn = reader.GetString(2),
                                Email = reader.GetString(3),
                                Adresse = reader.GetString(4),
                                Postnr = reader.GetInt32(5),
                                Kode = reader.GetString(6),
                                Admin = reader.GetBoolean(7)
                            };
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                }
            }
        }

        // Metode til at tilføje en ny kunde
        public void AddKunde(Kunde kunde)
        {
            if (kunde == null)
            {
                throw new ArgumentNullException(nameof(kunde));
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Kunder (Fornavn, Efternavn, Email, Adgangskode, Postnr, Adresse, Admin) VALUES (@navn, @efternavn, @email, @kode, @postnr, @addrese, @admin)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@navn", kunde.Navn);
                    cmd.Parameters.AddWithValue("@efternavn", kunde.Efternavn);
                    cmd.Parameters.AddWithValue("@email", kunde.Email);
                    cmd.Parameters.AddWithValue("@kode", kunde.Kode);
                    cmd.Parameters.AddWithValue("@postnr", kunde.Postnr);
                    cmd.Parameters.AddWithValue("@addrese", kunde.Adresse);
                    cmd.Parameters.AddWithValue("@admin", kunde.Admin);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Metode til at fjerne en kunde med deres ID
        public void RemoveKunde(int kundeId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Kunder WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Metode til at opdatere en eksisterende kunde
        public Kunde UpdateKunde(int kundeId, Kunde kunde)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "UPDATE Kunder SET Fornavn = @navn, Efternavn = @efternavn, Email = @email, Adgangskode = @kode, Postnr = @postnr, Adresse = @addrese, Admin = @admin WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@navn", kunde.Navn);
                    cmd.Parameters.AddWithValue("@efternavn", kunde.Efternavn);
                    cmd.Parameters.AddWithValue("@email", kunde.Email);
                    cmd.Parameters.AddWithValue("@kode", kunde.Kode);
                    cmd.Parameters.AddWithValue("@postnr", kunde.Postnr);
                    cmd.Parameters.AddWithValue("@addrese", kunde.Adresse);
                    cmd.Parameters.AddWithValue("@admin", kunde.Admin);
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);

                    cmd.ExecuteNonQuery();
                }
            }
            return kunde;
        }

        // Metode til at fjerne en kunde
        public Kunde Remove(int kundeId)
        {
            Kunde kunde = GetById(kundeId);
            RemoveKunde(kundeId);
            return kunde;
        }

        // Metode til at opdatere en kunde og sikre, at ID'erne matcher
        public Kunde Update(int nytKundeId, Kunde kunde)
        {
            if (nytKundeId != kunde.KundeId)
            {
                throw new ArgumentException("Kan ikke opdatere KundeId, de er forskellige.");
            }

            return UpdateKunde(nytKundeId, kunde);
        }

        // Metode til at logge den aktuelle kunde ud
        public void LogoutKunde()
        {
            KundeLoggedIn = null;
        }

        // Metode til at hente alle kunder sorteret efter navn i omvendt rækkefølge
        public List<Kunde> GetAllKunderSortedByNavnReversed()
        {
            var kunder = GetAll();
            return kunder.OrderByDescending(k => k.Navn).ToList();
        }

    }
}
