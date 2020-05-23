using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoVille
    {
        private dbal _myDbal;
        private string _tableDB = "Villes";

        public daoVille(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public List<Ville> SelectAll()
        {
            List<Ville> lesVilles = new List<Ville>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                Ville uneVille = new Ville();
                uneVille.Id = Convert.ToInt32(line["id"]);
                uneVille.Nom = line["nomVille"].ToString();
                lesVilles.Add(uneVille);
            }

            return lesVilles;
        }

        public Ville SelectById(int id)
        {
            DataTable table = _myDbal.Select(_tableDB + " where id=" + id);
            Ville uneVille = new Ville();
            DataRow line = table.Rows[0];

            uneVille.Id = Convert.ToInt32(line["id"]);
            uneVille.Nom = line["nomVille"].ToString();

            return uneVille;
        }

        public Ville SelectByNom(string nom)
        {
            DataTable table = _myDbal.Select(_tableDB + "where nomVille='" + nom + "'");
            Ville uneVille = new Ville();
            DataRow line = table.Rows[0];

            uneVille.Id = Convert.ToInt32(line["id"]);
            uneVille.Nom = line["nomVille"].ToString();

            return uneVille;
        }
    }
}
