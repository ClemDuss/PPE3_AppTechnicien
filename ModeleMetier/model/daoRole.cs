using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ModeleMetier.metier;

namespace ModeleMetier.model
{
    public class daoRole
    {
        private dbal _myDbal;
        private string _tableDB = "Roles";

        public daoRole(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public List<Role> SelectAll()
        {
            List<Role> lesRoles = new List<Role>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                Role unRole = new Role();
                unRole.Id = line["id"].ToString();
                unRole.Libelle = line["libelle"].ToString();
                unRole.Description = line["description"].ToString();
                lesRoles.Add(unRole);
            }

            return lesRoles;
        }

        public Role SelectById(string id)
        {
            Role unRole = new Role();
            DataTable table = _myDbal.Select(_tableDB + " where id='" + id + "'");
            DataRow line = table.Rows[0];

            unRole.Id = line["id"].ToString();
            unRole.Libelle = line["libelle"].ToString();
            unRole.Description = line["description"].ToString();

            return unRole;
        }
    }
}
