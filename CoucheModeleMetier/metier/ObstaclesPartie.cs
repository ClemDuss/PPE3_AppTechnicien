using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class ObstaclesPartie
    {
        private int id, idObstacle, positionObstacle, idPartie;
        private double prixObstacle;

        public ObstaclesPartie()
        {
        }

        public ObstaclesPartie(int id, int idObstacle, int positionObstacle, int idPartie, double prixObstacle)
        {
            Id = id;
            IdObstacle = idObstacle;
            PositionObstacle = positionObstacle;
            IdPartie = idPartie;
            PrixObstacle = prixObstacle;
        }

        public int Id { get => id; set => id = value; }
        public int IdObstacle { get => idObstacle; set => idObstacle = value; }
        public int PositionObstacle { get => positionObstacle; set => positionObstacle = value; }
        public int IdPartie { get => idPartie; set => idPartie = value; }
        public double PrixObstacle { get => prixObstacle; set => prixObstacle = value; }
    }
}
