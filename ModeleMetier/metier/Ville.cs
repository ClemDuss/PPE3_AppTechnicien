using System;
using System.Collections.Generic;
using System.Text;

namespace ModeleMetier.metier
{
    public class Ville
    {
        private int _id;
        private string _nom;

        public Ville()
        {
        }

        public Ville(int unId, string unNomVille)
        {
            Id = unId;
            Nom = unNomVille;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nom { get => _nom; set => _nom = value; }

        public override string ToString()
        {
            return Nom;
        }
    }
}
