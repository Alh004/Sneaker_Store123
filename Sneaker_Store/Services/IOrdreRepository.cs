using Sneaker_Store.Model;

namespace Sneaker_Store.Services

{
    public interface IOrdreRepository
    {
        // Metoder til at arbejde med ordrer

        // Tilføj en ny ordre
        void TilføjOrdre(Ordre ordre);

        // Find en ordre baseret på ID
        Ordre FindOrdre(int ordreId);

        // Hent alle ordrer
        IEnumerable<Ordre> HentAlleOrdrer();
        
        // Slet en ordre
        void SletOrdre(int ordreId);
    }
}
