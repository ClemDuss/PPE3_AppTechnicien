using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleMetier.metier
{
    public class InfosSupPersonnel
    {
        private int _id;
        private Utilisateur _utilisateur;
        private Role _role;
        private Ville _ville;

        public InfosSupPersonnel()
        {

        }

        public InfosSupPersonnel(int id, Utilisateur client, Role role, Ville ville)
        {
            _id = id;
            _utilisateur = client;
            _role = role;
            _ville = ville;
        }

        public int Id { get => _id; set => _id = value; }
        public Utilisateur Utilisateur { get => _utilisateur; set => _utilisateur = value; }
        public Role Role { get => _role; set => _role = value; }
        public Ville Ville { get => _ville; set => _ville = value; }
    }
}
