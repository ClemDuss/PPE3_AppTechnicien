using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class Roles
    {
        private string id, libelle, description;

        public Roles()
        {
        }

        public Roles(string unId, string unLibelle, string uneDescription)
        {
            Id = unId;
            Libelle = unLibelle;
            Description = uneDescription;
        }

        public string Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public string Description { get => description; set => description = value; }
    }
}
