using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class TransactionsCredits
    {
        private int id, idClient;
        private double montant;
        private string moyenPaiement;
        private DateTime dateTransaction;

        public TransactionsCredits()
        {
        }

        public TransactionsCredits(int unId, int unIdClient, double unMontant, string unMoyendDePaiement, DateTime uneDateTransaction)
        {
            Id = unId;
            IdClient = unIdClient;
            Montant = unMontant;
            MoyenPaiement = unMoyendDePaiement;
            DateTransaction = uneDateTransaction;
        }

        public int Id { get => id; set => id = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public double Montant { get => montant; set => montant = value; }
        public string MoyenPaiement { get => moyenPaiement; set => moyenPaiement = value; }
        public DateTime DateTransaction { get => dateTransaction; set => dateTransaction = value; }
    }
}
