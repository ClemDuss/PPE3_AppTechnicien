using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModeleMetier.model;

namespace Application_Technicien_EscapeGame
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(daoEtatCompte theDaoEtatCompte, daoInfosSupPersonnel theDaoInfosSupPersonnel, daoObstacles theDaoObstacles, daoPartie theDaoPartie, daoReservation theDaoReservation, daoRole theDaoRole, daoSalles theDaoSalles, daoTheme theDaoTheme, daoTransaction theDaoTransaction, daoUtilisateurs theDaoUtilisateurs, daoVille theDaoVille) 
        {
            InitializeComponent();

            viewModel.viewModel theViewModel = new viewModel.viewModel(theDaoEtatCompte, theDaoInfosSupPersonnel, theDaoObstacles, theDaoPartie, theDaoReservation, theDaoRole, theDaoSalles, theDaoTheme, theDaoTransaction, theDaoUtilisateurs, theDaoVille, frm);

            principale.DataContext = theViewModel;
            //grd_login.DataContext = theViewModel.ViewLogin;
            /*Label[] scoresLabelArr = new Label[5];
            int location = 98;
            for (int i = 0; i < scoresLabelArr.Length; i++)
            {
                scoresLabelArr[i] = new Label();
                scoresLabelArr[i].Height = 28;
                scoresLabelArr[i].Width = 100;
                scoresLabelArr[i].HorizontalAlignment = HorizontalAlignment.Center;
                scoresLabelArr[i].VerticalAlignment = VerticalAlignment.Center;
                scoresLabelArr[i].Content = "SALUT";
                //scoresLabelArr[i].Margin = new Thickness(211, location, 0, 0);
                location += 34;
                // I MISS SOMETHING HERE *** IN ORDER TO VIEW THOSE 5 LABELS I'VE CREATED ABOVE DURING THE RUN TIME - please advise :)
            }

            for (int i = 0; i < scoresLabelArr.Length; i++)
            {
                planning1.Children.Add(scoresLabelArr[i]);
            }*/





            /*int location = 98;
            for (int i = 1; i <= 5; i++)
            {
                Label label = new Label();
                label.Height = 28;
                label.Width = 100;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Content = 0;
                label.Margin = new Thickness(211, location, 0, 0);
                grd_homePage.Children.Add(label);
                location += 34;
            }*/
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            planning2.Visibility = Visibility.Hidden;
            Reservation.Visibility = Visibility.Visible;
        }
    }
}
