using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoPartie
    {
        private dbal _myDbal;
        private string _tableDB = "parties";
        private daoUtilisateurs _daoClient;
        private daoSalles _daoSalle;

        public daoPartie(dbal theDbal)
        {
            _myDbal = theDbal;
            _daoClient = new daoUtilisateurs(_myDbal);
            _daoSalle = new daoSalles(_myDbal);
        }

        public List<Partie> selectAll(List<Utilisateur> lesClients, List<Salle> lesSalles)
        {
            List<Partie> lesParties = new List<Partie>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                Partie unePartie = new Partie();
                unePartie.Id = Convert.ToInt32(line["id"]);
                if (lesClients != null)
                {
                    foreach (Utilisateur c in lesClients)
                    {
                        if (c.Id == Convert.ToInt32(line["idClient"]))
                            unePartie.Client = c;
                    }
                }
                else
                {
                    unePartie.Client = _daoClient.SelectById(Convert.ToInt32(line["idClient"]));
                }
                unePartie.HeureEtJourPartie = Convert.ToDateTime(line["heureEtJourPartie"]);
                if(lesSalles != null)
                {
                    foreach(Salle s in lesSalles)
                    {
                        if (s.Id == Convert.ToInt32(line["idSalle"]))
                            unePartie.Salle = s;
                    }
                }
                else
                {
                    unePartie.Salle = _daoSalle.selectById(Convert.ToInt32(line["idSalle"]));
                }
            }

            return lesParties;
        }
    }
}
