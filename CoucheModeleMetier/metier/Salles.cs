using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class Salles
    {
        private int id, idVille, idThemeActuel;
        private string nom;
        private DateTime heureOuverture, heureFermeture;

        public Salles()
        {
        }

        public Salles(int unId, string unNom, int unIdVille, int unIdThemeActuel, DateTime uneHeureOuverture, DateTime uneHeureFermeture)
        {
            Id = unId;
            Nom = unNom;
            IdVille = unIdVille;
            IdThemeActuel = unIdThemeActuel;
            HeureOuverture = uneHeureOuverture;
            HeureFermeture = uneHeureFermeture;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public int IdVille { get => idVille; set => idVille = value; }
        public int IdThemeActuel { get => idThemeActuel; set => idThemeActuel = value; }
        public DateTime HeureOuverture { get => heureOuverture; set => heureOuverture = value; }
        public DateTime HeureFermeture { get => heureFermeture; set => heureFermeture = value; }
    }
}
