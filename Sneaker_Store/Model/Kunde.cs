namespace Sneaker_Store.Model;

public class Kunde
{
    //instans felt
    private string _navn;
    private string _efternavn;
    private string _email;
    private string _adresse;
    private string _by;
    private int _postnr;
    private string _kode;
    private bool _admin;
   
    
    // properties
    public string Navn
    {
        get => _navn;
        set => _navn = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Efternavn
    {
        get => _efternavn;
        set => _efternavn = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Email
    {
        get => _email;
        set => _email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Adresse
    {
        get => _adresse;
        set => _adresse = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string By
    {
        get => _by;
        set => _by = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Postnr
    {
        get => _postnr;
        set => _postnr = value;
    }

    public string Kode
    {
        get => _kode;
        set => _kode = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool Admin
    {
        get => _admin;
        set => _admin = value;
    }

    public Kunde()
    {
        _navn = "";
        _efternavn = "";
        _email = "";
        _adresse = "";
        _by = "";
        _postnr = 0;
        _kode = "";
        _admin = false;
    }

    public Kunde(string navn, string efternavn, string email, string adresse, string by, int postnr, string kode, bool admin)
    {
        _navn = navn;
        _efternavn = efternavn;
        _email = email;
        _adresse = adresse;
        _by = by;
        _postnr = postnr;
        _kode = kode;
        _admin = admin;
    }

    public override string ToString()
    {
        return $"{nameof(_navn)}: {_navn}, {nameof(_efternavn)}: {_efternavn}, {nameof(_email)}: {_email}, {nameof(_adresse)}: {_adresse}, {nameof(_by)}: {_by}, {nameof(_postnr)}: {_postnr}, {nameof(_kode)}: {_kode}, {nameof(_admin)}: {_admin}";
    }
}