using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoSalles
    {
        private dbal _myDbal;
        private string _tableDB = "Salles";
        private daoVille _daoVille;
        private daoTheme _daoTheme;

        public daoSalles(dbal theDbal)
        {
            _myDbal = theDbal;
            _daoVille = new daoVille(_myDbal);
            _daoTheme = new daoTheme(_myDbal);
        }

        public Salle selectById(int id, List<Ville> lesVilles = null, List<Theme> lesThemes = null)
        {
            Salle uneSalle = new Salle();

            DataTable table = _myDbal.Select(_tableDB + " where id=" + id);

            DataRow line = table.Rows[0];

            uneSalle.Id = Convert.ToInt32(line["id"]);
            if (lesVilles != null)
            {
                foreach (Ville v in lesVilles)
                {
                    if (v.Id == Convert.ToInt32(line["idVille"]))
                        uneSalle.Ville = v;
                }
            }
            else
            {
                uneSalle.Ville = _daoVille.SelectById(Convert.ToInt32(line["idVille"]));
            }
            if (lesThemes != null)
            {
                foreach (Theme t in lesThemes)
                {
                    if (t.Id == Convert.ToInt32(line["idThemeActuel"]))
                        uneSalle.Theme = t;
                }
            }
            else
            {
                uneSalle.Theme = _daoTheme.SelectById(Convert.ToInt32(line["idThemeActuel"]));
            }
            uneSalle.Nom = line["nom"].ToString();
            uneSalle.HeureOuverture = Convert.ToDateTime(line["heureOuverture"]);
            uneSalle.HeureFermeture = Convert.ToDateTime(line["heureFermeture"]);

            return uneSalle;
        }

        public List<Salle> SelectByVille(Ville theVille, List<Theme> lesThemes = null)
        {
            List<Salle> allRooms = new List<Salle>();
            DataTable table = _myDbal.Select(_tableDB + " where idVille=" + theVille.Id);
            foreach(DataRow line in table.Rows)
            {
                if(Convert.ToInt32(line["idVille"]) == theVille.Id)
                {
                    Salle uneSalle = new Salle();
                    uneSalle.Id = Convert.ToInt32(line["id"]);
                    uneSalle.Ville = theVille;
                    if(lesThemes != null)
                    {
                        foreach (Theme t in lesThemes)
                        {
                            if (t.Id == Convert.ToInt32(line["idThemeActuel"]))
                                uneSalle.Theme = t;
                        }
                    }
                    else
                    {
                        uneSalle.Theme = _daoTheme.SelectById(Convert.ToInt32(line["idThemeActuel"]));
                    }
                    uneSalle.Nom = line["nom"].ToString();
                    uneSalle.HeureOuverture = Convert.ToDateTime(line["heureOuverture"]);
                    uneSalle.HeureFermeture = Convert.ToDateTime(line["heureFermeture"]);
                    allRooms.Add(uneSalle);
                }
            }
            return allRooms;
        }

        public int Count(Ville uneVille = null)
        {
            int i = 0;
            if(uneVille == null)
            {
                DataTable table = _myDbal.Select(_tableDB);
                foreach(DataRow line in table.Rows)
                {
                    i++;
                }
                return i;
            }
            else
            {
                DataTable table = _myDbal.Select(_tableDB + " where id=" + uneVille.Id);
                foreach(DataRow line in table.Rows)
                {
                    i++;
                }
                return i;
            }
        }
    }
}
