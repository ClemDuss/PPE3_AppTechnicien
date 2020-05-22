using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTestsCouches.metier
{
    class Clients
    {
        private int id;
        private string nom;
        private string prenom;
        private string photo;
        private string adresse;
        private string ville;
        private int codePostal;
        private string dateNaissance;
        private string motDePasse;
        private string mail;
        private int tel;

        public Clients()
        {
        }

        public Clients(int id, string nom, string prenom, string photo, string adresse, string ville, int codePostal, string dateNaissance, string motDePasse, string mail, int tel)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Photo = photo;
            Adresse = adresse;
            Ville = ville;
            CodePostal = codePostal;
            DateNaissance = dateNaissance;
            MotDePasse = motDePasse;
            Mail = mail;
            Tel = tel;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Ville { get => ville; set => ville = value; }
        public int CodePostal { get => codePostal; set => codePostal = value; }
        public string DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string MotDePasse { get => motDePasse; set => motDePasse = value; }
        public string Mail { get => mail; set => mail = value; }
        public int Tel { get => tel; set => tel = value; }
    }
}
