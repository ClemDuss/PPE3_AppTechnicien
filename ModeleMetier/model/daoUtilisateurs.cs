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
        private string _tableDB = "Utilisateurs";
        private daoTransaction _daotransaction;
        private daoReservation _daoReservation;
        private daoEtatCompte _daoEtatCompte;
        
        public daoUtilisateurs(dbal theDbal)
        {
            _myDbal = theDbal;
            _daoEtatCompte = new daoEtatCompte(theDbal);
        }

        public List<Utilisateur> SelectAll()
        {
            List<Utilisateur> allUsers = new List<Utilisateur>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                Utilisateur someUser = new Utilisateur();

                someUser = GenerateTheUser(line);

                allUsers.Add(someUser);
            }

            return allUsers;
        }

        public Utilisateur SelectById(int id)
        {
            Utilisateur someUser = new Utilisateur();
            DataTable table = _myDbal.Select(_tableDB + " where id=" + id);
            DataRow line = table.Rows[0];

            someUser = GenerateTheUser(line);

            return someUser;
        }

        /// <summary>
        /// Génère un objet Utilisateur
        /// </summary>
        /// <param name="databaseLine">Ligne de la base de donnée à convertir</param>
        /// <returns>Un utilisateur</returns>
        private Utilisateur GenerateTheUser(DataRow databaseLine)
        {
            Utilisateur someUser = new Utilisateur();

            someUser.Id = Convert.ToInt32(databaseLine["id"]);
            someUser.Mail = databaseLine["mail"].ToString();
            someUser.Nom = databaseLine["nom"].ToString();
            someUser.Prenom = databaseLine["prenom"].ToString();
            someUser.Photo = databaseLine["photo"].ToString();
            someUser.Adresse1 = databaseLine["adresse1"].ToString();
            someUser.Adresse2 = databaseLine["adresse2"].ToString();
            someUser.Ville = databaseLine["ville"].ToString();
            someUser.CodePostal = databaseLine["codePostal"].ToString();
            someUser.DateNaissance = Convert.ToDateTime(databaseLine["dateNaissance"]).Date;
            someUser.MotDePasse = databaseLine["motDePasse"].ToString();
            someUser.Identifiant = databaseLine["identifiant"].ToString();
            someUser.Tel = databaseLine["tel"].ToString();
            someUser.EtatCompte = _daoEtatCompte.SelectById(Convert.ToInt32(databaseLine["idEtatCompte"]));
            someUser.Personnel = Convert.ToBoolean(databaseLine["personnel"]);

            return someUser;
        }

        public bool PseudoExists(string pseudo)
        {
            bool pseudoExists = false;
            List<string> allPseudos = new List<string>();
            DataTable table = _myDbal.Select(_tableDB);
            foreach(DataRow line in table.Rows)
            {
                allPseudos.Add(line["identifiant"].ToString());
            }
            if (allPseudos.Contains(pseudo))
            {
                pseudoExists = true;
            }
            return pseudoExists;
        }

        public Utilisateur SelectByPseudo(string pseudo)
        {
            Utilisateur theUser = new Utilisateur();
            DataTable table = _myDbal.Select(_tableDB + " where identifiant='" + pseudo + "'");
            DataRow line = table.Rows[0];

            theUser = GenerateTheUser(line);

            return theUser;
        }

        public double Credit(Utilisateur unClient, List<Utilisateur> lesClients, List<Partie> lesParties, daoTransaction theDaoTransaction, daoReservation theDaoReservation)
        {
            _daotransaction = theDaoTransaction;
            _daoReservation = theDaoReservation;

            List<ReservationPartie> lesResa = _daoReservation.selectAll(lesClients, lesParties);
            List<Transaction> lesTrans = _daotransaction.selectAll(lesClients);

            double credit = 0;

            foreach(Transaction t in lesTrans)
            {
                if(t.User.Id == unClient.Id)
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

        public void updateUser(Utilisateur user, string newPseudo, string newEmail, string newTel, string newDDN)
        {
            _myDbal.Update(_tableDB + " set identifiant='" + newPseudo +"', mail='" + newEmail +"', tel='" + newTel +"', dateNaissance='" + newDDN +"' where id=" + user.Id);
        }

    }
}
