using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;
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

        private const String insertSql = "insert into Kunder values(@navn,@efternavn,@email,@kode,@postnr,@addrese,@admin)";
        public Kunde Add(Kunde newKunde)
        {
            SqlConnection connection = new SqlConnection(DB_Kunde.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(insertSql, connection);
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
            connection.Close();
            return newKunde;
        }


        public void AddKunde(Kunde kunde)
        {
            Add(kunde);
        }

        public bool CheckKunde(string email, string password)
        {
            bool isKundeValid = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM Kunder WHERE Email = @Email AND Adgangskode = @kode";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@kode", password);
                int count = (int)cmd.ExecuteScalar();

                isKundeValid = count > 0;
            }

            return isKundeValid;
        }

        public List<Kunde> GetAll()
            {
                List<Kunde> kunder = new List<Kunde>();

                SqlConnection connection = new SqlConnection(ConnectionString);

                connection.Open();

                String sql = "select * from Kunder";
                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataReader reader = cmd.ExecuteReader(); 
                while (reader.Read()) 
                {
                    Kunde kunde = ReadKunde(reader);
                    kunder.Add(kunde);
                }

                connection.Close();

                return kunder;

            }

        public void LogoutKunde()
        {
            throw new NotImplementedException();
        }

        /*
         * DELETE
         */
        private const String deleteByIdlSql = "Delete from Kunder where KundeId = @KundeId";
        public Kunde Remove(int kundeid)
        {
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



        private const String selectByIdlSql = "select * from Kunder where KundeId = @KundeId";
        public Kunde GetById(int kundeid)
        {
            SqlConnection connection = new SqlConnection(DB_Kunde.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(selectByIdlSql, connection);
            cmd.Parameters.AddWithValue("@KundeId", kundeid);

            SqlDataReader reader = cmd.ExecuteReader();

            Kunde kunde = null;
            if (reader.Read())
            {
                kunde = ReadKunde(reader);
            }
            else
            {
                // no row i.e. not found
                throw new KeyNotFoundException();
            }

            connection.Close();
            return kunde;
        }

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

        private const String selectAllSqlSortedByNavn = "select * from Kunder order by Fornavn DESC";
        public List<Kunde> GetAllKunderSortedByNavnReversed()
        {
            return GetAllWithParameterSQL(selectAllSqlSortedByNavn);
        }

        private List<Kunde> GetAllWithParameterSQL(string sql)
        {
            List<Kunde> kunder = new List<Kunde>();

            SqlConnection connection = new SqlConnection(DB_Kunde.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(sql, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Kunde kunde = ReadKunde(reader);
                kunder.Add(kunde);
            }

            connection.Close();
            return kunder;
        }

        public Kunde GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
