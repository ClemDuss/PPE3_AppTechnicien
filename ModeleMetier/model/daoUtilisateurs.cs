using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoUtilisateurs
    {
        private dbal _myDbal;
        private string _tableDB = "Clients";
        private daoTransaction _daotransaction;
        private daoReservation _daoReservation;
        
        public daoUtilisateurs(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public List<Utilisateur> SelectAll()
        {
            List<Utilisateur> lesClients = new List<Utilisateur>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                Utilisateur unClient = new Utilisateur();

                unClient.Id = Convert.ToInt32(line["id"]);
                unClient.Nom = line["nom"].ToString();
                unClient.Prenom = line["prenom"].ToString();
                unClient.Photo = line["photo"].ToString();
                unClient.Adresse1 = line["adresse1"].ToString();
                unClient.Ville = line["ville"].ToString();
                unClient.CodePostal = line["codePostal"].ToString();
                unClient.DateNaissance = Convert.ToDateTime(line["dateNaissance"]).Date;
                unClient.MotDePasse = line["motDePasse"].ToString();
                unClient.Mail = line["mail"].ToString();
                unClient.Tel = line["tel"].ToString();


                lesClients.Add(unClient);
            }

            return lesClients;
        }

        public Utilisateur SelectById(int id)
        {
            Utilisateur unClient = new Utilisateur();
            DataTable table = _myDbal.Select(_tableDB + " where id=" + id);
            DataRow line = table.Rows[0];

            unClient.Id = Convert.ToInt32(line["id"]);
            unClient.Nom = line["nom"].ToString();
            unClient.Prenom = line["prenom"].ToString();
            unClient.Photo = line["photo"].ToString();
            unClient.Adresse1 = line["adresse1"].ToString();
            unClient.Ville = line["ville"].ToString();
            unClient.CodePostal = line["codePostal"].ToString();
            unClient.DateNaissance = Convert.ToDateTime(line["dateNaissance"]).Date;
            unClient.MotDePasse = line["motDePasse"].ToString();
            unClient.Mail = line["mail"].ToString();
            unClient.Tel = line["tel"].ToString();

            return unClient;
        }

        public double Credit(Utilisateur unClient, List<Utilisateur> lesClients, List<Partie> lesParties, List<MoyenPaiement> lesMoyPaiement, daoTransaction theDaoTransaction, daoReservation theDaoReservation)
        {
            _daotransaction = theDaoTransaction;
            _daoReservation = theDaoReservation;

            List<ReservationPartie> lesResa = _daoReservation.selectAll(lesClients, lesParties);
            List<Transaction> lesTrans = _daotransaction.selectAll(lesClients, lesMoyPaiement);

            double credit = 0;

            foreach(Transaction t in lesTrans)
            {
                if(t.Client.Id == unClient.Id)
                {
                    credit += t.Montant;
                }
            }

            foreach(ReservationPartie r in lesResa)
            {
                if(r.Client.Id == unClient.Id)
                {
                    credit -= r.MontantRetireCredit;
                }
            }

            return credit;
        }

    }
}
