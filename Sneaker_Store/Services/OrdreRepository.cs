using Sneaker_Store.Model;

namespace Sneaker_Store.Services

{
    public class OrdreRepository : IOrdreRepository
    {
        private List<Ordre> _ordrer;

        public OrdreRepository()
        {
            _ordrer = new List<Ordre>();
            // Her kan du initialisere repository med nogle standard ordrer, hvis nødvendigt
        }

        public void TilføjOrdre(Ordre ordre)
        {
            _ordrer.Add(ordre);
        }

        public Ordre FindOrdre(int ordreId)
        {
            return _ordrer.Find(o => o.OrdreId == ordreId);
        }

        public IEnumerable<Ordre> HentAlleOrdrer()
        {
            return _ordrer;
        }

        public void OpdaterOrdre(Ordre ordre)
        {
            // Implementer opdateringslogik her
        }

        public void SletOrdre(int ordreId)
        {
            _ordrer.RemoveAll(o => o.OrdreId == ordreId);
        }
    }
}   
