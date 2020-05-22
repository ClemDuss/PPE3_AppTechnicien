using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTestsCouches.metier
{
    public class ContenusPostes
    {
        private int id, idClient, idPartie, noteEtoile;
        private string typeContenu, photo, video, commentaire;
        private DateTime datePublication;

        public ContenusPostes()
        {
        }

        public ContenusPostes(int id, int idClient, int idPartie, int noteEtoile, string typeContenu, string photo, string video, string commentaire, DateTime datePublication)
        {
            Id = id;
            IdClient = idClient;
            IdPartie = idPartie;
            NoteEtoile = noteEtoile;
            TypeContenu = typeContenu;
            Photo = photo;
            Video = video;
            Commentaire = commentaire;
            DatePublication = datePublication;
        }

        public int Id { get => id; set => id = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public int IdPartie { get => idPartie; set => idPartie = value; }
        public int NoteEtoile { get => noteEtoile; set => noteEtoile = value; }
        public string TypeContenu { get => typeContenu; set => typeContenu = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Video { get => video; set => video = value; }
        public string Commentaire { get => commentaire; set => commentaire = value; }
        public DateTime DatePublication { get => datePublication; set => datePublication = value; }
    }
}
