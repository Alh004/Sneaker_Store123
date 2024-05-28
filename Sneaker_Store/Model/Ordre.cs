namespace Sneaker_Store.Model
{
    public class Ordre
    {
        private int _OrdreID;
        private int _KundeID;
        private int _SkoID;
        private int _Antal;
        private decimal _TotalPris;

        /*
         * Properties
         */
        public int OrdreId
        {
            get { return _OrdreID; }
            set { _OrdreID = value; }
        }

        public int KundeId
        {
            get { return _KundeID; }
            set { _KundeID = value; }
        }

        public int SkoId
        {
            get { return _SkoID; }
            set { _SkoID = value; }
        }

        public int Antal
        {
            get { return _Antal; }
            set { _Antal = value; }
        }

        public decimal TotalPris
        {
            get { return _TotalPris; }
            set { _TotalPris = value; }
        }

        /*
         * Constructor
         */

        public Ordre(int ordreId, int kundeId, int skoId, int antal, decimal totalPris)
        {
            _OrdreID = ordreId;
            _KundeID = kundeId;
            _SkoID = skoId;
            _Antal = antal;
            _TotalPris = totalPris;
        }

        public Ordre()
        {
            _OrdreID = 0;
            _KundeID = 0;
            _SkoID = 0;
            _Antal = 0;
            _TotalPris = 0;
        }

        public override string ToString()
        {
            return $"{nameof(_OrdreID)}: {_OrdreID}, {nameof(_KundeID)}: {_KundeID}, {nameof(_SkoID)}: {_SkoID}, {nameof(_Antal)}: {_Antal}, {nameof(_TotalPris)}: {_TotalPris}";
        }
    }
}