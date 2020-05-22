using System;
using System.Collections.Generic;
using System.Text;

namespace ModeleMetier.metier
{
    public class ObstaclesPartie
    {
        private int _id, _positionObstacle;
        private Obstacle _obstacle;
        private Partie _partie;
        private double prixObstacle;

        public ObstaclesPartie()
        {
        }

        public ObstaclesPartie(int id, Obstacle obstacle, int positionObstacle, Partie partie, double prixObstacle)
        {
            Id = id;
            Obstacle = obstacle;
            PositionObstacle = positionObstacle;
            Partie = partie;
            PrixObstacle = prixObstacle;
        }

        public int Id { get => _id; set => _id = value; }
        public Obstacle Obstacle { get => _obstacle; set => _obstacle = value; }
        public int PositionObstacle { get => _positionObstacle; set => _positionObstacle = value; }
        public Partie Partie { get => _partie; set => _partie = value; }
        public double PrixObstacle { get => prixObstacle; set => prixObstacle = value; }
    }
}
