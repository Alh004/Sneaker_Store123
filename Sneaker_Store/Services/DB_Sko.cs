using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;

namespace Sneaker_Store.Services;

public class DB_Sko:ISkoRepository
{
    private const string ConnectionString =
        "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public Kunde? KundeLoggedIn => throw new NotImplementedException();
    
    private const string SelectAllSql = "SELECT * FROM Sko";

    public Sko Add(Sko newSko)
    {
        throw new NotImplementedException();
    }

    public List<Sko> GetAll()
    {
        List<Sko> skoList = new List<Sko>();

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(SelectAllSql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
            }
        }

        return skoList;
    }

    public Sko GetById(int skoid)
    {
        throw new NotImplementedException();
    }

    public Sko Delete(int skoid)
    {
        throw new NotImplementedException(); 
    }

    public Sko Update(int skoid, Sko updatedSko)
    {
        throw new NotImplementedException(); 
    }

    public Sko ReadSko(SqlDataReader reader)
    {
        Sko sko = new Sko();
        
        sko.SkoId = reader.GetInt32(0);
        sko.Maerke = reader.GetString(1);
        sko.Model = reader.GetString(2);
        sko.Str = reader.GetInt32(3);
        sko.Pris = reader.GetDouble(4);

        return sko;
    }
            
        
}
