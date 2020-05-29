using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;
using System.Collections.ObjectModel;

namespace ModeleMetier.model
{
    public class daoObstaclesParties
    {
        dbal _myDbal;
        private string _tableDB = "ObstaclesPartie";

        public daoObstaclesParties(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public void Insert(ObservableCollection<Obstacle> obstacles, Partie partie)
        {
            int positionObstacle = 1;
            foreach (Obstacle o in obstacles)
            {
                _myDbal.Insert(_tableDB + "(idObstacle, positionObstacle, idPartie, prixObstacle) values (" + o.Id +", " + positionObstacle + ", " + partie.Id + ", " + o.Prix + ");");
                positionObstacle++;
            }
        }
    }
}
