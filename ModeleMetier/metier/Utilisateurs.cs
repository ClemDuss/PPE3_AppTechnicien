using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleMetier.metier
{
    public class Utilisateur
    {
        private int _id;
        private string _mail;
        private string _nom;
        private string _prenom;
        private string _photo;
        private string _adresse1, _adresse2;
        private string _ville;
        private string _codePostal;
        private DateTime _dateNaissance;
        private string _motDePasse;
        private string _identifiant;
        private string _tel;
        private EtatCompte _etatCompte;
        private bool _personnel;

        public Utilisateur()
        {
        }

        public Utilisateur(int id, string nom, string prenom, string photo, string adresse1, string adresse2, string ville, string codePostal, DateTime dateNaissance, string motDePasse, string mail, string tel, EtatCompte unEtatCompte)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Photo = photo;
            Adresse1 = adresse1;
            Adresse2 = adresse2;
            Ville = ville;
            CodePostal = codePostal;
            DateNaissance = dateNaissance;
            MotDePasse = motDePasse;
            Mail = mail;
            Tel = tel;
            EtatCompte = unEtatCompte;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        public string Photo { get => _photo; set => _photo = value; }
        public string Adresse1 { get => _adresse1; set => _adresse1 = value; }
        public string Adresse2 { get => _adresse2; set => _adresse2 = value; }
        public string Ville { get => _ville; set =>_ville = value; }
        public string CodePostal { get => _codePostal; set => _codePostal = value; }
        public string MotDePasse { get => _motDePasse; set => _motDePasse = value; }
        public string Mail { get => _mail; set => _mail = value; }
        public string Tel { get => _tel; set => _tel = value; }
        public DateTime DateNaissance { get => _dateNaissance; set => _dateNaissance = value; }
        public EtatCompte EtatCompte { get => _etatCompte; set => _etatCompte = value; }
        public string Identifiant { get => _identifiant; set => _identifiant = value; }
        public bool Personnel { get => _personnel; set => _personnel = value; }

        public override string ToString()
        {
            return Prenom + " " + Nom;
        }
    }
}
