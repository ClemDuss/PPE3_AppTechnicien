using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class Villes
    {
        private int id;
        private string nomVille;

        public Villes()
        {
        }

        public Villes(int unId, string unNomVille)
        {
            Id = unId;
            NomVille = unNomVille;
        }

        public int Id { get => id; set => id = value; }
        public string NomVille { get => nomVille; set => nomVille = value; }
    }
}
