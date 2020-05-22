using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class ReservationParties
    {
        private int id, idClient, idPartie;
        private double montantRetireCredit;
        private DateTime dateReservation;

        public ReservationParties()
        {
        }

        public ReservationParties(int id, int idClient, int idPartie, double montantRetireCredit, DateTime dateReservation)
        {
            this.Id = id;
            this.IdClient = idClient;
            this.IdPartie = idPartie;
            this.MontantRetireCredit = montantRetireCredit;
            this.DateReservation = dateReservation;
        }

        public int Id { get => id; set => id = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public int IdPartie { get => idPartie; set => idPartie = value; }
        public double MontantRetireCredit { get => montantRetireCredit; set => montantRetireCredit = value; }
        public DateTime DateReservation { get => dateReservation; set => dateReservation = value; }
    }
}
