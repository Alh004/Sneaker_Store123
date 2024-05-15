using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;

namespace Sneaker_Store.Services
{
    public class DB_Kunde:IKundeRepository
    {
        private const string ConnectionString =
            "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public Kunde? KundeLoggedIn => throw new NotImplementedException();

        public void AddKunde(Kunde kunde)
        {
            throw new NotImplementedException();
        }

        public bool CheckKunde(string email, string password)
        {
            throw new NotImplementedException();
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
            kunde.Adresse = reader.GetString(4);
            kunde.By = reader.GetString(5);
            kunde.Postnr = reader.GetInt32(6);

            return kunde;
        }
    }
}
