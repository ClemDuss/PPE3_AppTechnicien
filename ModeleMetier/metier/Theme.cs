using System;
using System.Collections.Generic;
using System.Text;

namespace ModeleMetier.metier
{
    public class Theme
    {
        private int _id;
        private string _nom, _description;
        private double _prix;
        private DateTime _dateCreation;

        public Theme()
        {
        }

        public Theme(int unId, string unNomTheme, double unPrixTheme, string uneDescription, DateTime uneDateCreation)
        {
            Id = unId;
            Nom = unNomTheme;
            Prix = unPrixTheme;
            Description = uneDescription;
            DateCreation = uneDateCreation;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Description { get => _description; set => _description = value; }
        public double Prix { get => _prix; set => _prix = value; }
        public DateTime DateCreation { get => _dateCreation; set => _dateCreation = value; }
    }
}
