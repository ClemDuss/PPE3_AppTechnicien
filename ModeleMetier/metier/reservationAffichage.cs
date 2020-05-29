using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleMetier.metier
{
    public class reservationAffichage
    {
        private Utilisateur _user;
        private ReservationPartie _reservationPartie;
        private Partie _partie;

        public reservationAffichage()
        {

        }

        public reservationAffichage(Utilisateur user, ReservationPartie reservationPartie, Partie partie)
        {
            User = user;
            ReservationPartie = reservationPartie;
            Partie = partie;
        }

        public Utilisateur User { get => _user; set => _user = value; }
        public ReservationPartie ReservationPartie { get => _reservationPartie; set => _reservationPartie = value; }
        public Partie Partie { get => _partie; set => _partie = value; }
    }
}
