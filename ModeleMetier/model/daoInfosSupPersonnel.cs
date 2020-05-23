using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoInfosSupPersonnel
    {
        private dbal _myDbal;
        private string _tableDB = "InfosSupPersonnel";
        private daoVille _daoVille;
        private daoRole _daoRole;

        public daoInfosSupPersonnel(dbal theDbal)
        {
            _myDbal = theDbal;
            _daoVille = new daoVille(theDbal);
            _daoRole = new daoRole(theDbal);
        }

        public InfosSupPersonnel SelectByUser(Utilisateur theUser)
        {
            InfosSupPersonnel theISP = new InfosSupPersonnel();

            DataTable table = _myDbal.Select(_tableDB + " where idUtilisateur='" + theUser.Id + "'");
            DataRow line = table.Rows[0];

            theISP.Id = Convert.ToInt32(line["id"]);
            theISP.Utilisateur = theUser;
            theISP.Role = _daoRole.SelectById(line["idRole"].ToString());
            theISP.Ville = _daoVille.SelectById(Convert.ToInt32(line["idVille"]));

            return theISP;
        }
    }
}
