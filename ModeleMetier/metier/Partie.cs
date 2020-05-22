using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleMetier.metier
{
    public class Partie
    {
        private int _id;
        private Utilisateur _client;
        private DateTime _heureEtJourPartie;
        private Salle _salle;

        public Partie()
        {
        }

        public Partie(int id, Utilisateur client, DateTime heureEtJourPartie, Salle uneSalle)
        {
            Id = id;
            Client = client;
            HeureEtJourPartie = heureEtJourPartie;
            Salle = uneSalle;
        }

        public Utilisateur Client { get => _client; set => _client = value; }
        public int Id { get => _id; set => _id = value; }
        public DateTime HeureEtJourPartie { get => _heureEtJourPartie; set => _heureEtJourPartie = value; }
        public Salle Salle { get => _salle; set => _salle = value; }
    }
}
