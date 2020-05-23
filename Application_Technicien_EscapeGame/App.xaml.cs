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
        private daoEtatCompte _daoEtatCompte;
        private daoInfosSupPersonnel _daoInfosSupPersonnel;
        private daoObstacles _daoObstacles;
        private daoPartie _daoParties;
        private daoReservation _daoReservations;
        private daoRole _daoRoles;
        private daoSalles _daoSalles;
        private daoTheme _daoThemes;
        private daoTransaction _daoTransactions;
        private daoUtilisateurs _daoUtilisateurs;
        private daoVille _daoVilles;

        private dbal _dbal;

        public void App_Startup(object sender, StartupEventArgs e)
        {
            _dbal = new dbal();

            _daoEtatCompte = new daoEtatCompte(_dbal);
            _daoInfosSupPersonnel = new daoInfosSupPersonnel(_dbal);
            _daoObstacles = new daoObstacles(_dbal);
            _daoParties = new daoPartie(_dbal);
            _daoReservations = new daoReservation(_dbal);
            _daoRoles = new daoRole(_dbal);
            _daoSalles = new daoSalles(_dbal);
            _daoThemes = new daoTheme(_dbal);
            _daoTransactions = new daoTransaction(_dbal);
            _daoUtilisateurs = new daoUtilisateurs(_dbal);
            _daoVilles = new daoVille(_dbal);

            MainWindow wnd = new MainWindow(_daoEtatCompte, _daoInfosSupPersonnel, _daoObstacles, _daoParties, _daoReservations, _daoRoles, _daoSalles, _daoThemes, _daoTransactions, _daoUtilisateurs, _daoVilles);
            wnd.Show();
        }
    }
}
