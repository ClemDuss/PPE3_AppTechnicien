﻿using System;
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
        private daoUtilisateurs _daoUtilisateurs;

        public daoTransaction(dbal theDbal)
        {
            _myDbal = theDbal;
            _daoUtilisateurs = new daoUtilisateurs(_myDbal);
        }

        public List<Transaction> selectAll(List<Utilisateur> lesClients)
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
                        if (c.Id == Convert.ToInt32(line["idUtilisateur"]))
                            uneTransaction.User = c;
                    }
                }
                else
                {
                    uneTransaction.User = _daoUtilisateurs.SelectById(Convert.ToInt32(line["idUtilisateur"]));
                }
                uneTransaction.Montant = Convert.ToDouble(line["montantTransaction"]);
                uneTransaction.IdMoyenPaiement = Convert.ToInt32(line["idMoyenPaiement"]);
                uneTransaction.Date = Convert.ToDateTime(line["dateTransaction"]);

                lesTransactions.Add(uneTransaction);
            }

            return lesTransactions;
        }
    }
}
