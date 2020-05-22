using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoPersonnel
    {
        private dbal _myDbal;
        private string _tableDB = "Personnel";
        private daoVille _daoVille;
        private daoRole _daoRole;

        public daoPersonnel(dbal theDbal)
        {
            _myDbal = theDbal;
            _daoVille = new daoVille();
            _daoRole = new daoRole();
            
        }

        public List<Personnel> SelectAll(List<Role> lesRoles = null, List<Ville> lesVilles = null)
        {
            List<Personnel> lesPersonnels = new List<Personnel>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                Personnel unPersonnel = new Personnel();
                unPersonnel.Id = Convert.ToInt32(line["id"]);
                unPersonnel.Nom = line["nom"].ToString();
                unPersonnel.Prenom = line["prenom"].ToString();
                unPersonnel.Identifiant = line["identifiant"].ToString();
                if (lesRoles != null)
                {
                    foreach (Role r in lesRoles)
                    {
                        if (r.Id == line["idRole"].ToString())
                            unPersonnel.Role = r;
                    }
                }
                else
                {
                    unPersonnel.Role = _daoRole.SelectById(line["idRole"].ToString());
                }
                unPersonnel.Mail = line["mail"].ToString();
                unPersonnel.MotDePasse = line["motDePasse"].ToString();
                unPersonnel.DateNaissance = Convert.ToDateTime(line["dateNaissance"]).Date;
                unPersonnel.Tel = Convert.ToString(line["tel"]);
                if (lesVilles != null)
                {
                    foreach (Ville v in lesVilles)
                    {
                        if (v.Id == Convert.ToInt32(line["idVille"]))
                            unPersonnel.Ville = v;
                    }
                }
                else
                {
                    unPersonnel.Ville = _daoVille.SelectById(Convert.ToInt32(line["idVille"]));
                }

                lesPersonnels.Add(unPersonnel);
            }

            return lesPersonnels;
        }

        public Personnel SelectById(int id, List<Role> lesRoles = null, List<Ville> lesVilles = null)
        {
            Personnel unPersonnel = new Personnel();
            DataTable table = _myDbal.Select(_tableDB + " where id=" + id);
            DataRow line = table.Rows[0];

            unPersonnel.Id = Convert.ToInt32(line["id"]);
            unPersonnel.Nom = line["nom"].ToString();
            unPersonnel.Prenom = line["prenom"].ToString();
            unPersonnel.Identifiant = line["identifiant"].ToString();
            if (lesRoles != null)
            {
                foreach (Role r in lesRoles)
                {
                    if (r.Id == line["idRole"].ToString())
                        unPersonnel.Role = r;
                }
            }
            else
            {
                unPersonnel.Role = _daoRole.SelectById(line["idRole"].ToString());
            }
            unPersonnel.Mail = line["mail"].ToString();
            unPersonnel.MotDePasse = line["motDePasse"].ToString();
            unPersonnel.DateNaissance = Convert.ToDateTime(line["dateNaissance"]).Date;
            unPersonnel.Tel = Convert.ToString(line["tel"]);
            if (lesVilles != null)
            {
                foreach (Ville v in lesVilles)
                {
                    if (v.Id == Convert.ToInt32(line["idVille"]))
                        unPersonnel.Ville = v;
                }
            }
            else
            {
                unPersonnel.Ville = _daoVille.SelectById(Convert.ToInt32(line["idVille"]));
            }

            return unPersonnel;
        }

        public Personnel SelectByPseudo(string pseudo, List<Role> lesRoles = null, List<Ville> lesVilles = null)
        {
            Personnel unPersonnel = new Personnel();
            DataTable table = _myDbal.Select(_tableDB + " where identifiant='" + pseudo + "'");
            DataRow line = table.Rows[0];

            unPersonnel.Id = Convert.ToInt32(line["id"]);
            unPersonnel.Nom = line["nom"].ToString();
            unPersonnel.Prenom = line["prenom"].ToString();
            unPersonnel.Identifiant = line["identifiant"].ToString();
            if (lesRoles != null)
            {
                foreach (Role r in lesRoles)
                {
                    if (r.Id == line["idRole"].ToString())
                        unPersonnel.Role = r;
                }
            }
            else
            {
                unPersonnel.Role = _daoRole.SelectById(line["idRole"].ToString());
            }
            unPersonnel.Mail = line["mail"].ToString();
            unPersonnel.MotDePasse = line["motDePasse"].ToString();
            unPersonnel.DateNaissance = Convert.ToDateTime(line["dateNaissance"]).Date;
            unPersonnel.Tel = Convert.ToString(line["tel"]);
            if (lesVilles != null)
            {
                foreach (Ville v in lesVilles)
                {
                    if (v.Id == Convert.ToInt32(line["idVille"]))
                        unPersonnel.Ville = v;
                }
            }
            else
            {
                unPersonnel.Ville = _daoVille.SelectById(Convert.ToInt32(line["idVille"]));
            }

            return unPersonnel;
        }

        public bool PseudoExists(string pseudo)
        {
            List<string> lesPseudos = new List<string>();

            DataTable table = _myDbal.Select(_tableDB);
            
            foreach(DataRow line in table.Rows)
            {
                lesPseudos.Add(line["identifiant"].ToString());
            }

            if (lesPseudos.Contains(pseudo))
                return true;
            else
                return false;
        }

        public bool IsATech(Personnel user)
        {
            DataTable table = _myDbal.Select(_tableDB + " where id=" + user.Id);
            DataRow line = table.Rows[0];

            if (line["idRole"].ToString() == "TECH")
                return true;
            else
                return false;
        }

        public void changePassword(Personnel user, string newPassword)
        {
            _myDbal.Update(_tableDB + " motDePasse = '" + newPassword + "' where id =" + user.Id);
        }

        public void updateUser(Personnel user, string pseudo, string email, string tel, string date)
        {
            _myDbal.Update(_tableDB + " set identifiant='" + pseudo + "', mail='" + email + "', tel='" + tel + "', dateNaissance='" + date + "' where id=" + user.Id);
        }
    }
}
