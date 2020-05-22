using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoTransaction
    {
        private dbal _myDbal;
        private string _tableDB = "transactionsCredits";
        private daoUtilisateurs _daoClient;
        private daoMoyenPaiement _daoMoyenPaiement;

        public daoTransaction(dbal theDbal)
        {
            _myDbal = theDbal;
            _daoClient = new daoUtilisateurs(_myDbal);
            _daoMoyenPaiement = new daoMoyenPaiement();
        }

        public List<Transaction> selectAll(List<Utilisateur> lesClients, List<MoyenPaiement> lesMoyensPaiements)
        {
            List<Transaction> lesTransactions = new List<Transaction>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                Transaction uneTransaction = new Transaction();
                uneTransaction.Id = Convert.ToInt32(line["id"]);
                if (lesClients != null)
                {
                    foreach (Utilisateur c in lesClients)
                    {
                        if (c.Id == Convert.ToInt32(line["idClient"]))
                            uneTransaction.Client = c;
                    }
                }
                else
                {
                    uneTransaction.Client = _daoClient.SelectById(Convert.ToInt32(line["idClient"]));
                }
                uneTransaction.Montant = Convert.ToDouble(line["montantTransaction"]);
                if (lesMoyensPaiements != null)
                {
                    foreach (MoyenPaiement m in lesMoyensPaiements)
                    {
                        if (m.Id == Convert.ToInt32(line["idMoyenPaiement"]))
                            uneTransaction.MoyenPaiement = m;
                    }
                }
                else
                {
                    uneTransaction.MoyenPaiement = _daoMoyenPaiement.SelectById(Convert.ToInt32(line["idMoyenPaiement"]));
                }
                uneTransaction.DateTransaction = Convert.ToDateTime(line["dateTransaction"]);

                lesTransactions.Add(uneTransaction);
            }

            return lesTransactions;
        }
    }
}
