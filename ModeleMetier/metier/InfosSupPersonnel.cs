using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleMetier.metier
{
    class InfosSupPersonnel
    {
        private int _id;
        private Utilisateur _client;
        private Role _role;
        private Ville _ville;

        public InfosSupPersonnel()
        {

        }

        public InfosSupPersonnel(int id, Utilisateur client, Role role, Ville ville)
        {
            _id = id;
            _client = client;
            _role = role;
            _ville = ville;
        }

        public int Id { get => _id; set => _id = value; }
        public Utilisateur Client { get => _client; set => _client = value; }
        public Role Role { get => _role; set => _role = value; }
        public Ville Ville { get => _ville; set => _ville = value; }
    }
}
