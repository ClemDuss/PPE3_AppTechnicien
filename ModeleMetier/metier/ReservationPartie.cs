using System;
using System.Collections.Generic;
using System.Text;

namespace ModeleMetier.metier
{
    public class ReservationPartie
    {
        private int _id;
        private Utilisateur _client;
        private Partie _partie;
        private double _montantRetireCredit;
        private DateTime _dateReservation;

        public ReservationPartie()
        {
        }

        public ReservationPartie(int id, Utilisateur client, Partie partie, double montantRetireCredit, DateTime dateReservation)
        {
            Id = id;
            Client = client;
            Partie = partie;
            MontantRetireCredit = montantRetireCredit;
            DateReservation = dateReservation;
        }

        public int Id { get => _id; set => _id = value; }
        public double MontantRetireCredit { get => _montantRetireCredit; set => _montantRetireCredit = value; }
        public DateTime DateReservation { get => _dateReservation; set => _dateReservation = value; }
        public Utilisateur Client { get => _client; set => _client = value; }
        public Partie Partie { get => _partie; set => _partie = value; }
    }
}
