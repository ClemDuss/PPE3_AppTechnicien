using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class Obstacles
    {
        private int id, idVille;
        private string nomObstacle, description, photo;
        private double prixActuel;

        public Obstacles()
        {
        }

        public Obstacles(int id, int idVille, string nomObstacle, string description, string photo, double prixActuel)
        {
            Id = id;
            IdVille = idVille;
            NomObstacle = nomObstacle;
            Description = description;
            Photo = photo;
            PrixActuel = prixActuel;
        }

        public int Id { get => id; set => id = value; }
        public int IdVille { get => idVille; set => idVille = value; }
        public string NomObstacle { get => nomObstacle; set => nomObstacle = value; }
        public string Description { get => description; set => description = value; }
        public string Photo { get => photo; set => photo = value; }
        public double PrixActuel { get => prixActuel; set => prixActuel = value; }
    }
}
