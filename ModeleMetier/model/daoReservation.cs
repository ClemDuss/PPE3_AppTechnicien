using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoReservation
    {
        private dbal _myDbal;
        private string _tableDB = "reservationsparties";
        private daoUtilisateurs _daoClient;
        private daoPartie _daoPartie;

        public daoReservation(dbal theDbal)
        {
            _myDbal = theDbal;
            _daoPartie = new daoPartie(_myDbal);
            _daoClient = new daoUtilisateurs(_myDbal);
        }

        public List<ReservationPartie> selectAll(List<Utilisateur> lesClients, List<Partie> lesParties)
        {
            List<ReservationPartie> lesResa = new List<ReservationPartie>();

            DataTable table = _myDbal.Select(_tableDB);

            foreach(DataRow line in table.Rows)
            {
                ReservationPartie resa = new ReservationPartie();
                resa.Id = Convert.ToInt32(line["id"]);
                if (lesClients != null)
                {
                    foreach (Utilisateur c in lesClients)
                    {
                        if (c.Id == Convert.ToInt32(line["idUtilisateur"]))
                            resa.Client = c;
                    }
                }
                else
                {
                    resa.Client = _daoClient.SelectById(Convert.ToInt32(line["idUtilisateur"]));
                }
                resa.MontantRetireCredit = Convert.ToDouble(line["montantRetireCredit"]);
                if(lesParties != null)
                {
                    foreach(Partie p in lesParties)
                    {
                        if (p.Id == Convert.ToInt32(line["idPartie"]))
                            resa.Partie = p;
                    }
                }
                resa.DateReservation = Convert.ToDateTime(line["dateReservation"]);

                lesResa.Add(resa);
            }

            return lesResa;
        }
    }
}
