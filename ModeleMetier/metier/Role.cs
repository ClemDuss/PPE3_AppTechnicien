using System;
using System.Collections.Generic;
using System.Text;

namespace ModeleMetier.metier
{
    public class Role
    {
        private string _id, _libelle, _description;

        public Role()
        {
        }

        public Role(string unId, string unLibelle, string uneDescription)
        {
            Id = unId;
            Libelle = unLibelle;
            Description = uneDescription;
        }

        public string Id { get => _id; set => _id = value; }
        public string Libelle { get => _libelle; set => _libelle = value; }
        public string Description { get => _description; set => _description = value; }
    }
}
