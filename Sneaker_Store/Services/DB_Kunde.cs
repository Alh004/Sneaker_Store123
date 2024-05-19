using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;

namespace Sneaker_Store.Services
{
    public class DB_Kunde : IKundeRepository
    {
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public Kunde? KundeLoggedIn => /* hack */ null;

        private const String insertSql = "insert into Kunder values(@navn,@efternavn,@email,@kode,@by,@postnr,@addrese)";
        public Kunde Add(Kunde newKunde)
        {
            SqlConnection connection = new SqlConnection(DB_Kunde.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(insertSql, connection);
            cmd.Parameters.AddWithValue("@navn", newKunde.Navn);
            cmd.Parameters.AddWithValue("@efternavn", newKunde.Efternavn);
            cmd.Parameters.AddWithValue("@email", newKunde.Email);
            cmd.Parameters.AddWithValue("@kode", newKunde.Kode);
            cmd.Parameters.AddWithValue("@by", newKunde.By);
            cmd.Parameters.AddWithValue("@postnr", newKunde.Postnr);
            cmd.Parameters.AddWithValue("@addrese", newKunde.Adresse);
            
            
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

                String sql = "select * from Kunde";
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

        public void RemoveKunde(Kunde kunde)
        {
            throw new NotImplementedException();
        }

        private Kunde ReadKunde(SqlDataReader reader)
        {
            Kunde kunde = new Kunde();

            kunde.KundeId = reader.GetInt32(0);
            kunde.Navn = reader.GetString(1);
            kunde.Efternavn = reader.GetString(2);
            kunde.Email = reader.GetString(3);
            kunde.By = reader.GetString(5);
            kunde.Postnr = reader.GetInt32(6);

            return kunde;
            
          
            
        }
    }
}
