using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoEtatCompte
    {
        private dbal _myDbal;
        private string _tableDB = "etatsComptes";

        public daoEtatCompte(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public EtatCompte SelectById(int id)
        {
            EtatCompte theEtatCompte = new EtatCompte();
            DataTable table = _myDbal.Select(_tableDB + " where id=" + id);
            DataRow line = table.Rows[0];

            theEtatCompte.Id = Convert.ToInt32(line["id"]);
            theEtatCompte.Libelle = line["libelle"].ToString();
            theEtatCompte.Description = line["description"].ToString();

            return theEtatCompte;
        }
    }
}
