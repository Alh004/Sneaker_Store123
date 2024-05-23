using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;

namespace Sneaker_Store.Services;

public class DB_Ordre : IOrdreRepository
{
    private const string ConnectionString =
              "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public Ordre FindOrdre(int ordreId)
    {
        throw new NotImplementedException();
    }

    public List<Ordre> GetAll()
    {
        List<Ordre> ordrer = new List<Ordre>();

        SqlConnection connection = new SqlConnection(ConnectionString);

        connection.Open();

        String sql = "select * from Ordre";
        SqlCommand cmd = new SqlCommand(sql, connection);

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Ordre ordre = ReadOrdre(reader);
            ordrer.Add(ordre);
        }

        connection.Close();

        return ordrer;

    }

    public IEnumerable<Ordre> HentAlleOrdrer()
    {
        throw new NotImplementedException();
    }

    public void SletOrdre(int ordreId)
    {
        throw new NotImplementedException();
    }

    public void TilføjOrdre(Ordre ordre)
    {
        throw new NotImplementedException();
    }

    private Ordre ReadOrdre(SqlDataReader reader)
    {
        Ordre ordre = new Ordre();

        ordre.OrdreId = reader.GetInt32(0);
        ordre.KundeId = reader.GetInt32(1);
        ordre.SkoId = reader.GetInt32(2);
        ordre.Antal = reader.GetInt32(3);
        ordre.TotalPris = reader.GetDouble(4);
        return ordre;
    }
}