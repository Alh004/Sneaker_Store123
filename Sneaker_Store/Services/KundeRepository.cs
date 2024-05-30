using Sneaker_Store.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public class KundeRepository : IKundeRepository
    {
        // Forbindelsesstreng til databasen
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // Egenskab til at holde den loggede ind kunde
        public Kunde? KundeLoggedIn { get; set; }

        // Metode til at tilføje en ny kunde
        public void AddKunde(Kunde kunde)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "INSERT INTO Kunder (Fornavn, Efternavn, Email, Adgangskode, Postnr, Adresse, Admin) VALUES (@Fornavn, @Efternavn, @Email, @Adgangskode, @Postnr, @Adresse, @Admin)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Tilføjer parametre til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@Fornavn", kunde.Navn);
                    cmd.Parameters.AddWithValue("@Efternavn", kunde.Efternavn);
                    cmd.Parameters.AddWithValue("@Email", kunde.Email);
                    cmd.Parameters.AddWithValue("@Adgangskode", kunde.Kode);
                    cmd.Parameters.AddWithValue("@Postnr", kunde.Postnr);
                    cmd.Parameters.AddWithValue("@Adresse", kunde.Adresse);
                    cmd.Parameters.AddWithValue("@Admin", kunde.Admin);
                    // Udfører kommandoen
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Metode til at hente en kunde efter ID
        public Kunde GetById(int kundeId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Tilføjer parameter til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Returnerer kundeobjekt hvis fundet
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
            // Smider en undtagelse hvis kunden ikke findes
            throw new KeyNotFoundException("Kunde not found");
        }

        // Metode til at hente alle kunder
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
                        // Tilføjer hver fundet kunde til listen
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

        // Metode til at hente en kunde efter email
        public Kunde GetByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Tilføjer parameter til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Returnerer kundeobjekt hvis fundet
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
            // Smider en undtagelse hvis kunden ikke findes
            throw new KeyNotFoundException("Kunde not found");
        }

        // Metode til at tjekke om en kunde eksisterer med den angivne email og kode
        public LoginResult? CheckKunde(string email, string kode)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE Email = @Email AND Adgangskode = @Adgangskode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Tilføjer parametre til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Adgangskode", kode);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Sætter den loggede ind kunde og returnerer loginresultat
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

        // Metode til at fjerne en kunde efter deres ID
        public void RemoveKunde(int kundeId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "DELETE FROM Kunder WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Tilføjer parameter til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);
                    // Udfører kommandoen
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Metode til at opdatere en eksisterende kunde
        public Kunde UpdateKunde(int kundeId, Kunde kunde)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var query = "UPDATE Kunder SET Fornavn = @Fornavn, Efternavn = @Efternavn, Email = @Email, Adgangskode = @Adgangskode, Postnr = @Postnr, Adresse = @Adresse, Admin = @Admin WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Tilføjer parametre til SQL-kommandoen
                    cmd.Parameters.AddWithValue("@Fornavn", kunde.Navn);
                    cmd.Parameters.AddWithValue("@Efternavn", kunde.Efternavn);
                    cmd.Parameters.AddWithValue("@Email", kunde.Email);
                    cmd.Parameters.AddWithValue("@Adgangskode", kunde.Kode);
                    cmd.Parameters.AddWithValue("@Postnr", kunde.Postnr);
                    cmd.Parameters.AddWithValue("@Adresse", kunde.Adresse);
                    cmd.Parameters.AddWithValue("@Admin", kunde.Admin);
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);
                    // Udfører kommandoen
                    cmd.ExecuteNonQuery();
                }
            }
            // Returnerer opdateret kunde
            return GetById(kundeId);
        }

        // Metode til at logge den aktuelle kunde ud
        public void LogoutKunde()
        {
            KundeLoggedIn = null;
        }

        // Metode til at fjerne en kunde og returnere det fjernede kundeobjekt
        public Kunde Remove(int kundeId)
        {
            var kunde = GetById(kundeId);
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

        // Metode til at hente alle kunder sorteret efter navn i omvendt rækkefølge
        public List<Kunde> GetAllKunderSortedByNavnReversed()
        {
            var kunder = GetAll();
            kunder.Sort((x, y) => string.Compare(y.Navn, x.Navn, StringComparison.Ordinal));
            return kunder;
        }
    }
}
