using System;
using System.Collections.Generic;
using System.Text;

namespace ModeleMetier.metier
{
    public class Obstacle
    {
        private int _id;
        private string _nomObstacle, _description, _photo;
        private double _prixActuel;
        private Ville _ville;

        public Obstacle()
        {
        }

        public Obstacle(int id, string nomObstacle, string description, string photo, double prixActuel, Ville ville)
        {
            Id = id;
            Ville = ville;
            Nom = nomObstacle;
            Description = description;
            Photo = photo;
            Prix = prixActuel;
        }

        public int Id { get => _id; set => _id = value; }
        public Ville Ville { get => _ville; set => _ville = value; }
        public string Nom { get => _nomObstacle; set => _nomObstacle = value; }
        public string Description { get => _description; set => _description = value; }
        public string Photo { get => _photo; set => _photo = value; }
        public double Prix { get => _prixActuel; set => _prixActuel = value; }

        public override string ToString()
        {
            return Nom + " | " + Prix + " €";
        }
    }
}
