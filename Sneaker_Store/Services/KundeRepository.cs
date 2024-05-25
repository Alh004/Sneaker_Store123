using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;

namespace Sneaker_Store.Services
{
    public class KundeRepository : IKundeRepository
    {
        private List<Kunde> _kunder = new List<Kunde>();
        private const string ConnectionString = "Data Source=mssql13.unoeuro.com;Initial Catalog=sirat_dk_db_thread;User ID=sirat_dk;Password=m5k6BgDhAzxbprH49cyE;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
namespace Sneaker_Store.Services;

public class KundeRepository : IKundeRepository
{
    private List<Kunde> _kunder = new List<Kunde>();

    // konstruktør

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
                    // Handle the SQL exception
                    Console.WriteLine($"SQL Exception: {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions
                    Console.WriteLine($"Exception: {ex.Message}");
                    return null;
                }
            }
        }

        public Kunde GetById(int Kundeid)
        {
            Kunde? kunde = _kunder.Find(k => k.KundeId == Kundeid);
            if (kunde is null)
            {
                throw new KeyNotFoundException();
            }
            return kunde;
        }

        public List<Kunde> GetAll()
        {
            return new List<Kunde>(_kunder);
        }

        public void AddKunde(Kunde kunde)
        {
            if (kunde == null)
            {
                throw new ArgumentNullException(nameof(kunde));
            }
            _kunder.Add(kunde);
        }

        public void RemoveKunde(Kunde kunde)
        {
            _kunder.Remove(kunde);
        }
    public void Remove(Kunde kunde)
    {
        _kunder.Remove(kunde);
    }

        public Kunde Opdater(Kunde kunde)
        {
            Kunde editKunde = GetById(kunde.KundeId);
            if (editKunde != null)
            {
                editKunde.Navn = kunde.Navn;
                editKunde.Efternavn = kunde.Efternavn;
                editKunde.Email = kunde.Email;
                editKunde.Kode = kunde.Kode;
                editKunde.Postnr = kunde.Postnr;
                editKunde.Adresse = kunde.Adresse;
                editKunde.Admin = kunde.Admin; // Update the Admin property
            }
            return editKunde;
        }
    
    
public bool CheckKunde(string email, string kode)
    {
        Kunde? foundKunde = _kunder.Find(u => u.Email == email && u.Kode == kode);

        if (foundKunde != null)
        {
            KundeLoggedIn = foundKunde;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Kunde Update(int kundeid, Kunde updatedKunde)
    {
        if (kundeid != updatedKunde.KundeId)
        {
            throw new ArgumentException("kan ikke opdatere kundeid og obj.KundeId er forskellige");
        }

        Kunde updateThisKunde = GetById(kundeid);

        updateThisKunde.Navn = updatedKunde.Navn;
        updateThisKunde.Efternavn = updatedKunde.Efternavn;
        updateThisKunde.Email = updatedKunde.Email;
        updateThisKunde.Adresse = updatedKunde.Adresse;
        updateThisKunde.Postnr = updatedKunde.Postnr;
        updateThisKunde.Kode = updatedKunde.Kode;

        return updateThisKunde;
    }

    public Kunde GetByEmail(string email)
    {
        return _kunder.FirstOrDefault(k => k.Email == email);
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

        public void LogoutKunde()
        {
            KundeLoggedIn = null;
        }
    }

    public Kunde Update(Kunde kunde)
    {
        throw new NotImplementedException();
    }

    public Kunde Remove(int kundeid)
    {
        Kunde deleteKunde = GetById(kundeid);

        _kunder.Remove(deleteKunde);
        return deleteKunde;
    }
}