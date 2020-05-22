using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ModeleMetier.model;
using ModeleMetier.metier;

namespace Application_Technicien_EscapeGame
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private daoUtilisateurs theDaoClient;
        private daoPersonnel theDaoPersonnel;
        private daoRole theDaoRole;
        private daoTheme theDaoTheme;
        private daoVille theDaoVille;
        private daoSalles theDaoSalles;
        private daoTransaction theDaoTransaction;
        private daoMoyenPaiement theDaoMoyenPaiement;
        private daoReservation theDaoReservation;
        private daoObstacles theDaoObstacles;

        public void App_Startup(object sender, StartupEventArgs e)
        {
            theDaoVille = new daoVille();
            theDaoRole = new daoRole();
            theDaoPersonnel = new daoPersonnel();
            theDaoTheme = new daoTheme();
            theDaoMoyenPaiement = new daoMoyenPaiement();
            theDaoSalles = new daoSalles();
            theDaoTransaction = new daoTransaction();
            theDaoReservation = new daoReservation();
            theDaoClient = new daoUtilisateurs();
            theDaoObstacles = new daoObstacles();

            MainWindow wnd = new MainWindow(theDaoClient, theDaoPersonnel, theDaoRole, theDaoTheme, theDaoVille, theDaoSalles, theDaoMoyenPaiement, theDaoTransaction, theDaoReservation, theDaoObstacles);
            wnd.Show();
        }
    }
}
