using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTestsCouches.metier
{
    class Personnel
    {
        private int id;
        private string nom;
        private string prenom;
        private string identifiant;
        private string idRole;
        private string mail;
        private string motDePasse;
        private int dateNaissance;
        private int tel;
        private int idVille;

        public Personnel()
        {
        }

        public Personnel(int id, string nom, string prenom, string identifiant, string idRole, string mail, string motDePasse, int dateNaissance, int tel, int idVille)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Identifiant = identifiant;
            IdRole = idRole;
            Mail = mail;
            MotDePasse = motDePasse;
            DateNaissance = dateNaissance;
            Tel = tel;
            IdVille = idVille;
        }

        protected int Id { get => id; set => id = value; }
        protected string Nom { get => nom; set => nom = value; }
        protected string Prenom { get => prenom; set => prenom = value; }
        protected string Identifiant { get => identifiant; set => identifiant = value; }
        protected string IdRole { get => idRole; set => idRole = value; }
        protected string Mail { get => mail; set => mail = value; }
        protected string MotDePasse { get => motDePasse; set => motDePasse = value; }
        protected int DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        protected int Tel { get => tel; set => tel = value; }
        protected int IdVille { get => idVille; set => idVille = value; }
    }
}
