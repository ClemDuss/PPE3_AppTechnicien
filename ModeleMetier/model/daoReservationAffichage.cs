using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeleMetier.metier;
using System.Data;

namespace ModeleMetier.model
{
    public class daoReservationAffichage
    {
        private dbal _myDbal;

        public daoReservationAffichage(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public reservationAffichage getReservationAffichage(Salle uneSalle, DateTime uneDate, int uneHeureDebutCreneau, List<Utilisateur> lesClients = null)
        {
            reservationAffichage theResaAffichage = new reservationAffichage();
            DataTable table = _myDbal.SelectCustom("select U.id as 'idUser', U.*, P.id as 'idDeLaPartie', P.*, RP.id as 'idResa', RP.* "
                                            +"from Utilisateurs U "
                                            +"join reservationsParties RP on RP.idUtilisateur = U.id "
                                            +"join parties P on P.id = RP.idPartie "
                                            +"where P.idSalle = " + uneSalle.Id
                                            +"  and hour(P.heureEtJourPartie) = " + uneHeureDebutCreneau
                                            +"  and date(P.heureEtJourPartie) = '" + uneDate.Year + "-" + uneDate.Month + "-" + uneDate.Day + "'");

            if(table.Rows.Count > 0)
            {
                DataRow line = table.Rows[0];

                ReservationPartie theResa = new ReservationPartie();
                theResa.Id = Convert.ToInt32(line["idResa"]);
                if (lesClients != null)
                {
                    foreach (Utilisateur c in lesClients)
                    {
                        if (c.Id == Convert.ToInt32(line["idUtilisateur"]))
                            theResa.Client = c;
                    }
                }
                else
                {
                    theResa.Client = new daoUtilisateurs(_myDbal).SelectById(Convert.ToInt32(line["idUser"]));
                }
                theResa.MontantRetireCredit = Convert.ToDouble(line["montantRetireCredit"]);
                theResa.Partie = new daoPartie(_myDbal).SelectById(Convert.ToInt32(line["idPartie"]), lesClients);
                theResa.DateReservation = Convert.ToDateTime(line["dateReservation"]);

                Utilisateur theUser = new Utilisateur();
                theUser = theResa.Client;

                Partie thePartie = new Partie();
                thePartie = theResa.Partie;

                theResaAffichage = new reservationAffichage(theUser, theResa, thePartie);
            }
            

            return theResaAffichage;
        }
    }
}
