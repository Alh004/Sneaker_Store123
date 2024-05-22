namespace Sneaker_Store.Model
{
    public class Kurv
    {
        // instance field 
        private List<Sko> _liste;

        // property
        public List<Sko> Liste 
        { 
            get { return _liste; } 
            set { _liste = value; }
        }

        /*
         * Constructor
         */
        public Kurv()
        {
            _liste = new List<Sko>();
        }

        /*
         * Methods
         */
        public void Tilføj(Sko sko)
        {
            _liste.Add(sko);
        }

        public List<Sko> HentAlleSko()
        {
            return _liste;
        }

        public List<Sko> HentFraSko(int SkoID)
        {
            List<Sko> resultatListe = new List<Sko>();

            for (int i = 0; i < _liste.Count; i++)
            {
                if (_liste[i].SkoId == SkoID)
                {
                    resultatListe.Add(_liste[i]);
                }
            }

            return resultatListe;
        }

        public Sko Slet(Sko sko)
        {
            if (_liste.Contains(sko))
            {
                _liste.Remove(sko);
                return sko;
            }

            // not found
            return null;
        }

        // Property to calculate the total price
        public double TotalPrice 
        { 
            get 
            { 
                double totalPrice = 0;
                foreach (var sko in _liste)
                {
                    totalPrice += sko.Pris;
                }
                return totalPrice;
            } 
        }
    }
}