using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoObstacles
    {
        private dbal _myDbal;
        private string _tableDB = "Obstacles";

        public daoObstacles(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public List<Obstacle> selectByVille(Ville ville)
        {
            List<Obstacle> obstacles = new List<Obstacle>();

            DataTable table = _myDbal.Select(_tableDB + " where idVille=" + ville.Id);

            foreach(DataRow line in table.Rows)
            {
                Obstacle obstacle = new Obstacle();
                obstacle.Id = Convert.ToInt32(line["id"]);
                obstacle.Nom = line["nomObstacle"].ToString();
                obstacle.Description = line["description"].ToString();
                obstacle.Prix = Convert.ToDouble(line["prixActuel"]);
                obstacle.Photo = line["photo"].ToString();
                obstacle.Ville = ville;
                obstacles.Add(obstacle);
            }

            return obstacles;
        }
    }
}
