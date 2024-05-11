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
        get { return _navn; }
        set { _navn = value; }
    }

    public string Efternavn
    {
        get { return _efternavn; }
        set { _efternavn = value; }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Adresse
    {
        get { return _adresse; }
        set { _adresse = value; }
    }

    public string By
    {
        get { return _by; }
        set { _by = value; }
    }

    public int Postnr
    {
        get { return _postnr; }
        set { _postnr = value; }
    }

    public string Kode
    {
        get { return _kode; }
        set { _kode = value; }
    }

    public bool Admin
    {
        get { return _admin; }
        set { _admin = value; }
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