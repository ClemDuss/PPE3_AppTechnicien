using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using viewModel;
using ModeleMetier.model;
using ModeleMetier.metier;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

namespace viewModel
{
    public class viewModel : viewModelBase
    {
        private viewLogin _viewLogin;

        private daoUtilisateurs _daoUtilisateurs;
        private daoInfosSupPersonnel _daoInfosSupPersonnel;
        private daoRole _daoRole;
        private daoTheme _daoTheme;
        private daoVille _daoVille;
        private daoSalles _daoSalles;
        private daoTransaction _daoTransaction;
        private daoReservation _daoReservation;
        private daoObstacles _daoObstacles;

        private Utilisateur _client;
        private Utilisateur _user;
        private Role _role;
        private Theme _theme;
        private Ville _ville;

        private List<Utilisateur> _clients;
        private List<Role> _roles;
        private List<Theme> _themes;
        private List<Ville> _villes;
        private List<Partie> _parties;

        private Window _form;

        private int _nbSalles;

        public viewModel(daoUtilisateurs theDaoUtilisateurs, daoInfosSupPersonnel theDaoInfosSupPersonnel, daoRole theDaoRole, daoTheme theDaoTheme, daoVille theDaoVille, daoSalles theDaoSalles, daoReservation theDaoReservation, daoObstacles theDaoObstacles, Window frm)
        {
            _daoUtilisateurs = theDaoUtilisateurs;
            _daoInfosSupPersonnel = theDaoInfosSupPersonnel;
            _daoRole = theDaoRole;
            _daoTheme = theDaoTheme;
            _daoVille = theDaoVille;
            _daoSalles = theDaoSalles;
            _daoReservation = theDaoReservation;
            _daoObstacles = theDaoObstacles;

            Villes = _daoVille.SelectAll();
            Roles = _daoRole.SelectAll();
            Clients = _daoUtilisateurs.SelectAll();

            _viewLogin = new viewLogin(theDaoUtilisateurs, theDaoInfosSupPersonnel, Villes, Roles);

            Form = frm;
            Form.Title = "Escape Game - Technicien";
            Form.Icon = new BitmapImage(new Uri("pack://application:,,,/images/mtn_A.ico"));

            WindowState = WindowState.Normal;

            VisibilityLogin = Visibility.Visible;
            VisibilityHome = Visibility.Hidden;
            HideAll();

            Pseudo = "";
            ErrorPseudo = new ContentControl();

            User = new Utilisateur();
            UserCity = new Ville();
        }

        public viewLogin ViewLogin
        {
            get
            {
                return _viewLogin;
            }
        }

        public Utilisateur User { get => _user; set => _user = value; }
        public List<Utilisateur> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public List<Partie> Parties { get => _parties; set => _parties = value; }
        public List<Role> Roles { get => _roles; set => _roles = value; }
        public List<Theme> Themes
        {
            get
            {
                return _themes;
            }
            set
            {
                _themes = value;
                OnPropertyChanged("Themes");
            }
        }
        public List<Ville> Villes { get => _villes; set => _villes = value; }
        public Window Form
        {
            get
            {
                return _form;
            }
            set
            {
                _form = value;
                OnPropertyChanged("Form");
            }
        }


        public void HideAll()
        {
            VisibilityUser = Visibility.Hidden;
            VisibilityChangeParams = Hidden();
            VisibilityPlanning1 = Visibility.Hidden;
            VisibilityPlanning2 = Visibility.Hidden;
            VisibilityResa = Visibility.Hidden;
            VisibilityResaState1 = Visibility.Hidden;
            VisibilityResaState2 = Visibility.Hidden;
            VisibilityResaState3 = Visibility.Hidden;
            VisibilityResaState4 = Visibility.Hidden;
        }

        public Visibility Hidden()
        {
            return Visibility.Hidden;
        }

        public Visibility Visible()
        {
            return Visibility.Visible;
        }






        #region LOGIN
        private Visibility _visibilityLogin;
        private string _pseudo;
        private ICommand _loginCommand;
        private ContentControl _errorPseudo;

        public ICommand LoginCommand
        {
            get
            {
                if (this._loginCommand == null)
                    this._loginCommand = new RelayCommand(() => this.loginUser(), () => true);

                return this._loginCommand;
            }
        }

        public void loginUser()
        {
            if (Pseudo.Length > 0)
            {
                if (_daoPersonnel.PseudoExists(Pseudo))
                {
                    VisibilityLogin = Visibility.Hidden;
                    VisibilityHome = Visibility.Visible;

                    User = _daoPersonnel.SelectByPseudo(Pseudo, Roles, Villes);
                    UserCity = User.Ville;

                    Form.Title += " | " + User.ToString();

                    UserPseudo = User.Identifiant;
                    UserNom = User.Nom;
                    UserPrenom = User.Prenom;
                    UserEmail = User.Mail;
                    UserTel = User.Tel.ToString();
                    UserDDN = User.DateNaissance.ToString("dd-MM-yyyy");

                    //WindowState = WindowState.Maximized;
                    Form.WindowState = WindowState.Maximized;
                    Form.Icon = new BitmapImage(new Uri("pack://application:,,,/images/mtn_A_red.ico"));

                    NbSalles = _daoSalles.Count(User.Ville);

                    if (NbSalles == 1)
                    {
                        VisibilityPlanning1 = Visibility.Visible;
                    }
                    else if (NbSalles == 2)
                    {
                        VisibilityPlanning2 = Visibility.Visible;
                    }
                    else if (NbSalles == 4)
                    {
                        VisibilityPlanning4 = Visibility.Visible;
                    }

                    Obstacles = _daoObstacles.selectByVille(User.Ville);
                }
                else
                {
                    ErrorPseudo.Content = "Pseudo introuvable";
                }
            }
            else
            {
                ErrorPseudo.Content = "Veuillez saisir votre pseudo";
            }
        }

        public int NbSalles
        {
            get
            {
                return _nbSalles;
            }
            set
            {
                _nbSalles = value;
            }
        }

        public ContentControl ErrorPseudo
        {
            get
            {
                return _errorPseudo;
            }
            set
            {
                _errorPseudo = value;
                OnPropertyChanged("ErrorPseudo");
            }
        }
        public Visibility VisibilityLogin
        {
            get
            {
                return _visibilityLogin;
            }
            set
            {
                _visibilityLogin = value;
                OnPropertyChanged("VisibilityLogin");
            }
        }

        public string Pseudo
        {
            get
            {
                return _pseudo;
            }
            set
            {
                _pseudo = value;
                OnPropertyChanged("Pseudo");
            }
        }
        #endregion













        #region HOME
        private Visibility _visibilityHome;
        private Ville _userCity;
        private ICommand _clickProfile, _clickPlanning;
        private WindowState _windowState;


        public WindowState WindowState
        {
            get
            {
                return _windowState;
            }
            set
            {
                _windowState = value;
                OnPropertyChanged("WindowState");
            }
        }

        public ICommand ClickProfile
        {
            get
            {
                if (this._clickProfile == null)
                    this._clickProfile = new RelayCommand(() => this.clickProfile(), () => true);

                return this._clickProfile;
            }
        }

        public void clickProfile()
        {
            VisibilityPlanning1 = Visibility.Hidden;
            VisibilityPlanning2 = Visibility.Hidden;
            VisibilityPlanning4 = Visibility.Hidden;
            VisibilityUser = Visibility.Visible;
        }


        public ICommand ClickPlanning
        {
            get
            {
                if (this._clickPlanning == null)
                    this._clickPlanning = new RelayCommand(() => this.clickPlanning(), () => true);

                return this._clickPlanning;
            }
        }

        public void clickPlanning()
        {
            VisibilityUser = Visibility.Hidden;
            VisibilityPlanning1 = Visibility.Hidden;
            VisibilityPlanning2 = Visibility.Hidden;
            VisibilityPlanning4 = Visibility.Hidden;

            resetResa();

            if (NbSalles == 1)
            {
                VisibilityPlanning1 = Visibility.Visible;
            }
            else if (NbSalles == 2)
            {
                VisibilityPlanning2 = Visibility.Visible;
            }
            else if (NbSalles == 4)
            {
                VisibilityPlanning4 = Visibility.Visible;
            }
        }
        public Visibility VisibilityHome
        {
            get
            {
                return _visibilityHome;
            }
            set
            {
                _visibilityHome = value;
                OnPropertyChanged("VisibilityHome");
            }
        }

        public Ville UserCity
        {
            get
            {
                return _userCity;
            }
            set
            {
                _userCity = value;
                OnPropertyChanged("UserCity");
            }
        }











        #region PLANNING
        private Visibility _visibilityPlanning2, _visibilityPlanning1, _visibilityPlanning4, _visibilityPlanning;
        private ICommand _openResa;
        private DateTime _selectedDate = DateTime.Now;


        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }


        public ICommand OpenResa
        {
            get
            {
                if (this._openResa == null)
                    this._openResa = new RelayCommand(() => this.openResa(), () => true);

                return this._openResa;
            }
        }

        public void openResa()
        {
            VisibilityPlanning1 = Visibility.Hidden;
            VisibilityPlanning2 = Visibility.Hidden;
            VisibilityPlanning4 = Visibility.Hidden;
            VisibilityResa = Visibility.Visible;
            VisibilityResaState1 = Visibility.Visible;
        }

        public void dispPlanning()
        {
            NbSalles = _daoSalles.Count(User.Ville);

            if (NbSalles == 1)
            {
                VisibilityPlanning1 = Visibility.Visible;
            }
            else if (NbSalles == 2)
            {
                VisibilityPlanning2 = Visibility.Visible;
            }
            else if (NbSalles == 4)
            {
                VisibilityPlanning4 = Visibility.Visible;
            }
        }

        public Visibility VisibilityPlanning2
        {
            get
            {
                return _visibilityPlanning2;
            }
            set
            {
                _visibilityPlanning2 = value;
                OnPropertyChanged("VisibilityPlanning2");
            }
        }

        public Visibility VisibilityPlanning1
        {
            get
            {
                return _visibilityPlanning1;
            }
            set
            {
                _visibilityPlanning1 = value;
                OnPropertyChanged("VisibilityPlanning1");
            }
        }

        public Visibility VisibilityPlanning4
        {
            get
            {
                return _visibilityPlanning4;
            }
            set
            {
                _visibilityPlanning4 = value;
                OnPropertyChanged("VisibilityPlanning4");
            }
        }
        #endregion










        #region RESA
        private Visibility _visibilityResa, _visibilityResaState1, _visibilityResaState2, _visibilityResaState3, _visibilityResaState4;
        private ICommand _nextResa, _cancelResa, _prevResa, _validResa;
        private List<CheckBox> _chkObstacles;
        private string _selectedClientIndex, _selectedHorraireIndex, _selectedNbJoueurIndex;

        private Utilisateur _selectedClient;

        private double _creditClient;

        private string _selectedHoraire;
        private List<string> _horairesDispos;

        private List<Obstacle> _obstacles = new List<Obstacle>();
        private List<Obstacle> _selectedObstacles = new List<Obstacle>();

        private List<Utilisateur> _selectedParticipants = new List<Utilisateur>();

        private int _nbJoueurs, _nbObstacles;
        private double _prixParticipants, _prixObstacles, _prixTotal;


        public int NbJoueurs
        {
            get
            {
                return _nbJoueurs;
            }
            set
            {
                _nbJoueurs = value;
                OnPropertyChanged("NbJoueurs");
            }
        }

        public int NbObstacles
        {
            get
            {
                return _nbObstacles;
            }
            set
            {
                _nbObstacles = value;
                OnPropertyChanged("NbObstacles");
            }
        }

        public double PrixParticipants
        {
            get
            {
                return _prixParticipants;
            }
            set
            {
                _prixParticipants = value;
                OnPropertyChanged("PrixParticipants");
            }
        }

        public double PrixObstacles
        {
            get
            {
                return _prixObstacles;
            }
            set
            {
                _prixObstacles = value;
                OnPropertyChanged("PrixObstacles");
            }
        }

        public double PrixTotal
        {
            get
            {
                return _prixTotal;
            }
            set
            {
                _prixTotal = value;
                OnPropertyChanged("PrixTotal");
            }
        }


        public List<Utilisateur> SelectedParticipants
        {
            get
            {
                return _selectedParticipants;
            }
            set
            {
                _selectedParticipants = value;
                OnPropertyChanged("SelectedParticipants");
            }
        }

        public string SelecteHoraire
        {
            get
            {
                return _selectedHoraire;
            }
            set
            {
                _selectedHoraire = value;
                OnPropertyChanged("SelectedHoraire");
            }
        }

        public List<string> HorairesDispo
        {
            get
            {
                return _horairesDispos;
            }
            set
            {
                _horairesDispos = value;
                OnPropertyChanged("HorairesDispos");
            }
        }

        





        public Utilisateur SelectedClient
        {
            get
            {
                return _selectedClient;
            }
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
                CreditsClient = _daoClient.Credit(SelectedClient, Clients, Parties, MoyensPaiement, _daoTransaction, _daoReservation).ToString();
            }
        }

        public string CreditsClient
        {
            get
            {
                return _creditClient.ToString() + " €";
            }
            set
            {
                _creditClient = Convert.ToInt32(value);
                OnPropertyChanged("CreditsClient");
            }
        }




        public ICommand NextResa
        {
            get
            {
                if (this._nextResa == null)
                    this._nextResa = new RelayCommand(() => this.nextResa(), () => true);

                return this._nextResa;
            }
        }

        public void nextResa()
        {
            if(VisibilityResaState1 == Visibility.Visible)
            {
                VisibilityResaState1 = Hidden();
                VisibilityResaState2 = Visible();
            }
            else if(VisibilityResaState2 == Visibility.Visible)
            {
                VisibilityResaState2 = Hidden();
                VisibilityResaState3 = Visible();
            }
            else if(VisibilityResaState3 == Visibility.Visible)
            {
                VisibilityResaState3 = Hidden();
                VisibilityResaState4 = Visible();

                NbObstacles = SelectedObstacles.Count;
                NbJoueurs = SelectedParticipants.Count;
                PrixObstacles = 0;
                foreach(Obstacle o in SelectedObstacles)
                {
                    PrixObstacles += o.Prix;
                }
                PrixParticipants = 0;
                foreach(Utilisateur c in SelectedParticipants)
                {
                    PrixParticipants += 5;
                }
            }
        }

        public ICommand PrevResa
        {
            get
            {
                if (this._prevResa == null)
                    this._prevResa = new RelayCommand(() => this.prevResa(), () => true);

                return this._prevResa;
            }
        }

        public void prevResa()
        {
            if(VisibilityResaState4 == Visibility.Visible)
            {
                VisibilityResaState4 = Hidden();
                VisibilityResaState3 = Visible();
            }
            else if(VisibilityResaState3 == Visibility.Visible)
            {
                VisibilityResaState3 = Hidden();
                VisibilityResaState2 = Visible();
            }
            else if(VisibilityResaState2 == Visibility.Visible)
            {
                VisibilityResaState2 = Hidden();
                VisibilityResaState1 = Visible();
            }
        }

        public ICommand CancelResa
        {
            get
            {
                if (this._cancelResa == null)
                    this._cancelResa = new RelayCommand(() => this.cancelResa(), () => true);

                return this._cancelResa;
            }
        }

        public void cancelResa()
        {
            MessageBoxResult choice = MessageBox.Show("En annulant, toutes les données de cette réservation seront effacées !", "Attention !", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            switch (choice)
            {
                case MessageBoxResult.OK:
                    resetResa();
                    dispPlanning();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        public void resetResa()
        {
            VisibilityResaState1 = Hidden();
            VisibilityResaState2 = Hidden();
            VisibilityResaState3 = Hidden();
            VisibilityResaState4 = Hidden();
            VisibilityResa = Hidden();

            SelectedHorraireIndex = "-1";
            SlectedClientIndex = "-1";
            SlectedNbJoueurIndex = "-1";
        }


        public string SlectedClientIndex
        {
            get
            {
                return _selectedClientIndex;
            }
            set
            {
                _selectedClientIndex = value;
                OnPropertyChanged("SelectedClientIndex");
            }
        }
        public string SlectedNbJoueurIndex
        {
            get
            {
                return _selectedNbJoueurIndex;
            }
            set
            {
                _selectedNbJoueurIndex = value;
                OnPropertyChanged("SlectedNbJoueurIndex");
            }
        }
        public string SelectedHorraireIndex
        {
            get
            {
                return _selectedHorraireIndex;
            }
            set
            {
                _selectedHorraireIndex = value;
                OnPropertyChanged("SelectedHorraireIndex");
            }
        }


        public List<Obstacle> Obstacles
        {
            get
            {
                return _obstacles;
            }
            set
            {
                _obstacles = value;
                OnPropertyChanged("Obstacles");
            }
        }

        public List<Obstacle> SelectedObstacles
        {
            get
            {
                return _selectedObstacles;
            }
            set
            {
                _selectedObstacles = value;
                OnPropertyChanged("SelectedObstacles");
            }
        }

        public List<CheckBox> ChkObstacles
        {
            get
            {
                _chkObstacles = new List<CheckBox>();
                foreach(Obstacle o in Obstacles)
                {
                    CheckBox chk = new CheckBox();
                    chk.Content = o.ToString();
                    chk.Content = "Frite";
                    _chkObstacles.Add(chk);
                }
                return _chkObstacles;
            }
            set
            {
                _chkObstacles = value;
                OnPropertyChanged("ChkObstacles");
            }
        }


        public Visibility VisibilityResa
        {
            get
            {
                return _visibilityResa;
            }
            set
            {
                _visibilityResa = value;
                OnPropertyChanged("VisibilityResa");
            }
        }

        public Visibility VisibilityResaState1
        {
            get
            {
                return _visibilityResaState1;
            }
            set
            {
                _visibilityResaState1 = value;
                OnPropertyChanged("VisibilityResaState1");
            }
        }

        public Visibility VisibilityResaState2
        {
            get
            {
                return _visibilityResaState2;
            }
            set
            {
                _visibilityResaState2 = value;
                OnPropertyChanged("VisibilityResaState2");
            }
        }

        public Visibility VisibilityResaState3
        {
            get
            {
                return _visibilityResaState3;
            }
            set
            {
                _visibilityResaState3 = value;
                OnPropertyChanged("VisibilityResaState3");
            }
        }

        public Visibility VisibilityResaState4
        {
            get
            {
                return _visibilityResaState4;
            }
            set
            {
                _visibilityResaState4 = value;
                OnPropertyChanged("VisibilityResaState4");
            }
        }

        #endregion













        #region USER PROFILE
        private string _userPseudo, _userPrenom, _userNom, _userEmail, _userTel;
        private string _tempPseudo, _tempEmail, _tempTel;
        private DateTime _userDDN, _tempDDN;
        private Visibility _visibilityUser, _visibilityChangeParams;
        private ICommand _openChangeParams, _validChangeParams;



        public void FillTempUser()
        {
            TempDDN = UserDDN;
            TempEmail = UserEmail;
            TempTel = UserTel;
            TempPseudo = UserPseudo;
        }

        public void ClearTempUser()
        {
            TempDDN = new DateTime().Date.ToString("yyyy-MM-dd");
            TempEmail = "";
            TempTel = "";
            TempPseudo = "";
        }

        public void RefreshUser()
        {
            User = _daoPersonnel.SelectById(User.Id, Roles, Villes);
            UserPseudo = User.Identifiant;
            UserTel = User.Tel;
            UserEmail = User.Mail;
            ClearTempUser();
        }


        public ICommand OpenChangeParams
        {
            get
            {
                if (this._openChangeParams == null)
                    this._openChangeParams = new RelayCommand(() => this.openChangeParams(), () => true);

                return this._openChangeParams;
            }
        }

        public void openChangeParams()
        {
            FillTempUser();
            VisibilityChangeParams = Visible();
        }


        public ICommand ValidChangeParams
        {
            get
            {
                if (this._validChangeParams == null)
                    this._validChangeParams = new RelayCommand(() => this.validChangeParams(), () => true);

                return this._validChangeParams;
            }
        }

        public void validChangeParams()
        {
            _daoPersonnel.updateUser(User, TempPseudo, TempEmail, TempTel, TempDDN);
            VisibilityChangeParams = Hidden();
            RefreshUser();
        }

        public string TempPseudo
        {
            get
            {
                return _tempPseudo;
            }
            set
            {
                _tempPseudo = value;
                OnPropertyChanged("TempPseudo");
            }
        }

        public string TempEmail
        {
            get
            {
                return _tempEmail;
            }
            set
            {
                _tempEmail = value;
                OnPropertyChanged("TempEmail");
            }
        }

        public string TempTel
        {
            get
            {
                return _tempTel;
            }
            set
            {
                _tempTel = value;
                OnPropertyChanged("TempTel");
            }
        }

        public string TempDDN
        {
            get
            {
                return _tempDDN.ToString("yyyy-MM-dd");
            }
            set
            {
                _tempDDN = DateTime.Parse(value).Date;
                OnPropertyChanged("TempDDN");
            }
        }


        public Visibility VisibilityUser
        {
            get
            {
                return _visibilityUser;
            }
            set
            {
                _visibilityUser = value;
                OnPropertyChanged("VisibilityUser");
            }
        }

        public Visibility VisibilityChangeParams
        {
            get
            {
                return _visibilityChangeParams;
            }
            set
            {
                _visibilityChangeParams = value;
                OnPropertyChanged("VisibilityChangeParams");
            }
        }

        public string UserPseudo
        {
            get
            {
                return _userPseudo;
            }
            set
            {
                _userPseudo = value;
                OnPropertyChanged("UserPseudo");
            }
        }
        public string UserPrenom
        {
            get
            {
                return _userPrenom;
            }
            set
            {
                _userPrenom = value;
                OnPropertyChanged("UserPrenom");
            }
        }
        public string UserNom
        {
            get
            {
                return _userNom;
            }
            set
            {
                _userNom = value;
                OnPropertyChanged("UserNom");
            }
        }
        public string UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                _userEmail = value;
                OnPropertyChanged("UserEmail");
            }
        }
        public string UserTel
        {
            get
            {
                return _userTel;
            }
            set
            {
                _userTel = value;
                OnPropertyChanged("UserTel");
            }
        }
        public string UserDDN
        {
            get
            {
                return _userDDN.ToString("dd-MM-yyyy");
            }
            set
            {
                _userDDN = DateTime.Parse(value).Date;
                OnPropertyChanged("UserDDN");
            }
        }
        #endregion
        #endregion
    }
}
