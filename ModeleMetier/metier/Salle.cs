using System;
using System.Collections.Generic;
using System.Text;

namespace ModeleMetier.metier
{
    public class Salle
    {
        private int _id;
        private Ville _ville;
        private Theme _theme;
        private string _nom;
        private DateTime _heureOuverture, _heureFermeture;

        public Salle()
        {
        }

        public Salle(int unId, string unNom, Ville uneVille, Theme unTheme, DateTime uneHeureOuverture, DateTime uneHeureFermeture)
        {
            Id = unId;
            Nom = unNom;
            Ville = uneVille;
            Theme = unTheme;
            HeureOuverture = uneHeureOuverture;
            HeureFermeture = uneHeureFermeture;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public Ville Ville { get => _ville; set => _ville = value; }
        public Theme Theme { get => _theme; set => _theme = value; }
        public DateTime HeureOuverture { get => _heureOuverture; set => _heureOuverture = value; }
        public DateTime HeureFermeture { get => _heureFermeture; set => _heureFermeture = value; }
    }
}
