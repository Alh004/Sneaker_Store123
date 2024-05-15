using Sneaker_Store.Model;

namespace Sneaker_Store.Services

{
    public class KvitteringRepository : IKvitteringRepository
    {
        private List<Kvittering> _kvitteringer;

        public KvitteringRepository()
        {
            _kvitteringer = new List<Kvittering>();
        }

        public void OpretKvittering(Kvittering kvittering)
        {
            _kvitteringer.Add(kvittering);
        }

        public Kvittering HentKvittering(int id)
        {
            return _kvitteringer.Find(k => k.Id == id);
        }

        public IEnumerable<Kvittering> HentAlleKvitteringer()
        {
            return _kvitteringer;
        }
    }
}