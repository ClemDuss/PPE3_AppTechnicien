using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ModeleMetier.metier;

namespace ModeleMetier.model
{
    public class daoTheme
    {
        private dbal _myDbal;
        private string _tableDB = "Themes";

        public daoTheme(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public List<Theme> SelectAll()
        {
            List<Theme> lesThemes = new List<Theme>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                Theme unTheme = new Theme();
                unTheme.Id = Convert.ToInt32(line["id"]);
                unTheme.Nom = line["nomTheme"].ToString();
                unTheme.Prix = Convert.ToDouble(line["prixTheme"]);
                unTheme.Description = line["description"].ToString();
                unTheme.DateCreation = Convert.ToDateTime(line["dateCreation"]).Date;
                lesThemes.Add(unTheme);
            }

            return lesThemes;
        }

        public Theme SelectById(int id)
        {
            DataTable table = _myDbal.Select(_tableDB + " where id=" + id);
            Theme unTheme = new Theme();
            DataRow line = table.Rows[0];

            unTheme.Id = Convert.ToInt32(line["id"]);
            unTheme.Nom = line["nomTheme"].ToString();
            unTheme.Prix = Convert.ToDouble(line["prixTheme"]);
            unTheme.Description = line["description"].ToString();
            unTheme.DateCreation = Convert.ToDateTime(line["dateCreation"]).Date;

            return unTheme;
        }
    }
}
