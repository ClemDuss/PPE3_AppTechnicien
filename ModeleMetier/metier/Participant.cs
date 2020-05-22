using System;
using System.Collections.Generic;
using System.Text;

namespace ModeleMetier.metier
{
    public class Participant
    {
        private int _id;
        private Partie _partie;
        private Utilisateur _client;

        public Participant()
        {
        }

        public Participant(int id, Utilisateur client, Partie partie)
        {
            Id = id;
            Client = client;
            Partie = partie;
        }

        public int Id { get => _id; set => _id = value; }
        public Utilisateur Client { get => _client; set => _client = value; }
        internal Partie Partie { get => _partie; set => _partie = value; }
    }
}
