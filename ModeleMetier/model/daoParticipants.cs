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
    public class daoParticipants
    {
        private dbal _myDbal;
        private string _tableDB = "participants";

        public daoParticipants(dbal theDbal)
        {
            _myDbal = theDbal;
        }

        public void Insert(ObservableCollection<Utilisateur> participants, Partie partie)
        {
            foreach (Utilisateur p in participants)
            {
                _myDbal.Insert(_tableDB + "(idUtilisateur, idPartie) values (" + p.Id + ", " + partie.Id + ");");
            }
        }
    }
}
