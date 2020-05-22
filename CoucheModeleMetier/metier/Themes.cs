using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class Themes
    {
        private int id;
        private string nomTheme, description;
        private double prixTheme;
        private DateTime dateCreation;

        public Themes()
        {
        }

        public Themes(int unId, string unNomTheme, double unPrixTheme, string uneDescription, DateTime uneDateCreation)
        {
            Id = unId;
            NomTheme = unNomTheme;
            PrixTheme = unPrixTheme;
            Description = uneDescription;
            DateCreation = uneDateCreation;
        }

        public int Id { get => id; set => id = value; }
        public string NomTheme { get => nomTheme; set => nomTheme = value; }
        public string Description { get => description; set => description = value; }
        public double PrixTheme { get => prixTheme; set => prixTheme = value; }
        public DateTime DateCreation { get => dateCreation; set => dateCreation = value; }
    }
}
