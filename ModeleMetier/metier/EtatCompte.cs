using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleMetier.metier
{
    public class EtatCompte
    {
        private int _id;
        private string _libelle, _description;

        public EtatCompte()
        {

        }

        public EtatCompte(int unId, string unLibelle, string uneDescription)
        {
            Id = unId;
            Libelle = unLibelle;
            Description = uneDescription;
        }

        public int Id { get => _id; set => _id = value; }
        public string Libelle { get => _libelle; set => _libelle = value; }
        public string Description { get => _description; set => _description = value; }
    }
}
