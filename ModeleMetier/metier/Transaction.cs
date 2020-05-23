using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleMetier.metier
{
    public class Transaction
    {
        private int _id;
        private Utilisateur _user;
        private double _montant;
        private int _idMoyenPaiement;
        private DateTime _date;

        public Transaction()
        {

        }

        public Transaction(int id, Utilisateur user, double montant, int idMoyenPaiement, DateTime date)
        {
            _id = id;
            _user = user;
            _montant = montant;
            _idMoyenPaiement = idMoyenPaiement;
            _date = date;
        }

        public int Id { get => _id; set => _id = value; }
        public Utilisateur User { get => _user; set => _user = value; }
        public double Montant { get => _montant; set => _montant = value; }
        public int IdMoyenPaiement { get => _idMoyenPaiement; set => _idMoyenPaiement = value; }
        public DateTime Date { get => _date; set => _date = value; }
    }
}
