using Sneaker_Store.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Sneaker_Store.Services
{
    public class DB_Kunde : IKundeRepository
    {
        private const string ConnectionString = "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public Kunde? KundeLoggedIn { get; private set; }

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
                    Console.WriteLine($"SQL Exception: {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
                }
            }
        }

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

        public Kunde Remove(int kundeId)
        {
            Kunde kunde = GetById(kundeId);
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

        public void LogoutKunde()
        {
            KundeLoggedIn = null;
        }

        public List<Kunde> GetAllKunderSortedByNavnReversed()
        {
            var kunder = GetAll();
            return kunder.OrderByDescending(k => k.Navn).ToList();
        }

        public List<Kunde> Search(int number, string name, string phone)
        {
            throw new NotImplementedException();
        }

        public List<Kunde> SortNumber()
        {
            throw new NotImplementedException();
        }

        public List<Kunde> SortName()
        {
            throw new NotImplementedException();
        }
    }
}
