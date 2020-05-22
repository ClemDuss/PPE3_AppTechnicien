using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTestsCouches.metier
{
    class Parties
    {
        private int id;
        private int idClientAcheteur;
        private DateTime heureEtJourPartie;
        private int idSalle;

        public Parties()
        {
        }

        public Parties(int id, int idClientAcheteur, DateTime heureEtJourPartie, int idSalle)
        {
            Id = id;
            IdClientAcheteur = idClientAcheteur;
            HeureEtJourPartie = heureEtJourPartie;
            IdSalle = idSalle;
        }

        protected int Id { get => id; set => id = value; }
        protected int IdClientAcheteur { get => idClientAcheteur; set => idClientAcheteur = value; }
        protected DateTime HeureEtJourPartie { get => heureEtJourPartie; set => heureEtJourPartie = value; }
        protected int IdSalle { get => idSalle; set => idSalle = value; }
    }
}
