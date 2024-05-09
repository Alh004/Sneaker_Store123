namespace Sneaker_Store.Model;

public class Kvittering
{
    private int _ID;
    private int _KvitteringID;
    private int _KundeID;
    private int _Antal;
    private double _TotalPris;
    private string _Beskrivelse;
    private DateTime _Koebsdato;

    /*
     * Properties
     */
    public int Id
    {
        get => _ID;
        set => _ID = value;
    }

    public int KvitteringId
    {
        get => _KvitteringID;
        set => _KvitteringID = value;
    }

    public int KundeId
    {
        get => _KundeID;
        set => _KundeID = value;
    }

    public int Antal
    {
        get => _Antal;
        set => _Antal = value;
    }

    public double TotalPris
    {
        get => _TotalPris;
        set => _TotalPris = value;
    }

    public string Beskrivelse
    {
        get => _Beskrivelse;
        set => _Beskrivelse = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime Koebsdato
    {
        get => _Koebsdato;
        set => _Koebsdato = value;
    }

    /*
     * Constructor
     */
    public Kvittering()
    {
        _ID = 0;
        _KvitteringID = 0;
        _Antal = 0;
        _KundeID = 0;
        _TotalPris = 0; 
        _Beskrivelse = "";
        _Koebsdato = DateTime.Now;

    }
    
    public Kvittering(int id, int kvitteringId, int kundeId, int antal, double totalPris, string beskrivelse, DateTime koebsdato)
    {
        _ID = id;
        _KvitteringID = kvitteringId;
        _KundeID = kundeId;
        _Antal = antal;
        _TotalPris = totalPris;
        _Beskrivelse = beskrivelse;
        _Koebsdato = koebsdato;
    }

    public override string ToString()
    {
        return $"{nameof(_ID)}: {_ID}, {nameof(_KvitteringID)}: {_KvitteringID}, {nameof(_KundeID)}: {_KundeID}, {nameof(_Antal)}: {_Antal}, {nameof(_TotalPris)}: {_TotalPris}, {nameof(_Beskrivelse)}: {_Beskrivelse}, {nameof(_Koebsdato)}: {_Koebsdato}";
    }
}