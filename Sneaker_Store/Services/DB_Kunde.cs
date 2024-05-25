using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;
using System.Collections.Generic;
using System.Data;

namespace Sneaker_Store.Services
{
    public class DB_Kunde : IKundeRepository
    {
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DB_Kunde(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public Kunde LoggedInKunde => _httpContextAccessor.HttpContext?.Items["LoggedInKunde"] as Kunde;
        public Kunde? KundeLoggedIn => /* hack */ null;

        private const string InsertSql = "INSERT INTO Kunder (Fornavn, Efternavn, Email, Adgangskode, Postnr, Adresse, Admin) VALUES (@navn, @efternavn, @email, @kode, @postnr, @addrese, @admin)";

        public Kunde Add(Kunde newKunde)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(InsertSql, connection))
                {
                    cmd.Parameters.AddWithValue("@navn", newKunde.Navn);
                    cmd.Parameters.AddWithValue("@efternavn", newKunde.Efternavn);
                    cmd.Parameters.AddWithValue("@email", newKunde.Email);
                    cmd.Parameters.AddWithValue("@kode", newKunde.Kode);
                    cmd.Parameters.AddWithValue("@postnr", newKunde.Postnr);
                    cmd.Parameters.AddWithValue("@addrese", newKunde.Adresse);
                    cmd.Parameters.AddWithValue("@admin", newKunde.Admin); // Add the Admin parameter

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new ArgumentException("Kunne ikke oprette Kunde = " + newKunde);
                    }
                }
            SqlCommand cmd = new SqlCommand(InsertSql, connection);
            cmd.Parameters.AddWithValue("@navn", newKunde.Navn);
            cmd.Parameters.AddWithValue("@efternavn", newKunde.Efternavn);
            cmd.Parameters.AddWithValue("@email", newKunde.Email);
            cmd.Parameters.AddWithValue("@kode", newKunde.Kode);
            cmd.Parameters.AddWithValue("@postnr", newKunde.Postnr);
            cmd.Parameters.AddWithValue("@addrese", newKunde.Adresse);
            cmd.Parameters.AddWithValue("@admin", newKunde.Admin);
            
            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
            {
                throw new ArgumentException("Kunne ikke oprette Kunde =" + newKunde);
            }
            return newKunde;
        }

        public void AddKunde(Kunde kunde)
        {
            Add(kunde);
        }

        public LoginResult? CheckKunde(string email, string kode)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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

            return isKundeValid;
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

        public Kunde GetById(int kundeId)
        /*
         * DELETE
         */
        private const String deleteByIdlSql = "Delete from Kunder where KundeId = @KundeId";
        public Kunde Remove(int kundeid)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT KundeId, Fornavn, Efternavn, Email, Adresse, Postnr, Adgangskode, Admin FROM Kunder WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@KundeId", kundeId);
            Kunde kunde = GetById(kundeid);

            SqlConnection connection = new SqlConnection(DB_Kunde.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(deleteByIdlSql, connection);
            cmd.Parameters.AddWithValue("@KundeId", kundeid);

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
            {
                throw new ArgumentException("Kunne ikke slette Kunder med KundeId=" + kundeid);
            }
            connection.Close();

            return kunde;
        }

        private Kunde ReadKunde(SqlDataReader reader)
        {
            Kunde kunde = new Kunde();

            kunde.KundeId = reader.GetInt32(0);
            kunde.Navn = reader.GetString(1);
            kunde.Efternavn = reader.GetString(2);
            kunde.Email = reader.GetString(3);
            kunde.Adresse = reader.GetString(4);
            kunde.Postnr = reader.GetInt32(5);

            return kunde;
            
          
            
        }

        private const string selectByIdlSql = "SELECT * FROM Kunder WHERE KundeId = @KundeId";

        public Kunde GetById(int kundeid)
        {
            SqlConnection connection = new SqlConnection(DB_Kunde.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(selectByIdlSql, connection);
            cmd.Parameters.AddWithValue("@KundeId", kundeid);

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

        public void RemoveKunde(Kunde kunde)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Kunder WHERE KundeId = @KundeId";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@KundeId", kunde.KundeId);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new ArgumentException("Kunne ikke slette Kunde = " + kunde);
                    }
                }
            }
        }

        public Kunde Opdater(Kunde kunde)
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
                    cmd.Parameters.AddWithValue("@KundeId", kunde.KundeId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new ArgumentException("Kunne ikke opdatere Kunde = " + kunde);
                    }
                }
            }
            return kunde;
        }

        public List<Kunde> Search(int number, string name, string phone)
        private const String updateSql = "update Kunder set Fornavn=@Navn, Efternavn=@Efternavn, Adgangskode = @Kode, Postnr = @Postnr, Adresse = @Adresse WHERE KundeId = @kundeid";
        public Kunde Update(int kundeid, Kunde updatedKunde)
        {
            if (kundeid != updatedKunde.KundeId)
            {
                throw new ArgumentException("Kan ikke opdatere id er forskellig fra id i updatedeKunde");
            }

            SqlConnection connection = new SqlConnection(DB_Kunde.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(updateSql, connection);
            cmd.Parameters.AddWithValue("@KundeId", kundeid);
            cmd.Parameters.AddWithValue("@Navn", updatedKunde.Navn);
            cmd.Parameters.AddWithValue("@Efternavn", updatedKunde.Efternavn);
            cmd.Parameters.AddWithValue("@Email", updatedKunde.Email);
            cmd.Parameters.AddWithValue("@Kode", updatedKunde.Kode);
            cmd.Parameters.AddWithValue("@Postnr", updatedKunde.Postnr);
            cmd.Parameters.AddWithValue("@Adresse", updatedKunde.Adresse);

            int row = cmd.ExecuteNonQuery();
            Console.WriteLine("Rows affected " + row);

            connection.Close();
            return updatedKunde;
        }

        public Kunde GetByEmail(string email)
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

        public List<Kunde> Search(int number, string name, string phone)
        {
            throw new NotImplementedException();
        }

        public Kunde Update(Kunde kunde)
        {
            throw new NotImplementedException();
        }

        public void Remove(Kunde kunde)
        {
            throw new NotImplementedException();
        }
    }
}
