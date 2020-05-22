using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class Participants
    {
        private int id, idClient, idParties;

        public Participants()
        {
        }

        public Participants(int id, int idClient, int idParties)
        {
            Id = id;
            IdClient = idClient;
            IdParties = idParties;
        }

        public int Id { get => id; set => id = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public int IdParties { get => idParties; set => idParties = value; }
    }
}
