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
using System.Windows.Media;

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
        private daoPartie _daoParties;
        private daoEtatCompte _daoEtatComptes;
        private daoReservationAffichage _daoReservationAffichage;
        private daoParticipants _daoParticipants;
        private daoObstaclesParties _daoObstaclesParties;

        private Utilisateur _client;
        private Utilisateur _user;
        private InfosSupPersonnel _userInfosSupPersonnel;
        private Role _role;
        private Theme _theme;
        private Ville _ville;
        private Salle _selectedSalle;
        private int _selectedSalleIndex;

        private List<Utilisateur> _clients;
        private List<Role> _roles;
        private List<Theme> _themes;
        private List<Ville> _villes;
        private List<Partie> _parties;
        private List<Salle> _salles;

        private Window _form;

        private int _nbSalles;

        public viewModel(daoEtatCompte theDaoEtatCompte, daoInfosSupPersonnel theDaoInfosSupPersonnel, daoObstacles theDaoObstacles, daoPartie theDaoPartie, daoReservation theDaoReservation, daoRole theDaoRole, daoSalles theDaoSalles, daoTheme theDaoTheme, daoTransaction theDaoTransaction, daoUtilisateurs theDaoUtilisateurs, daoVille theDaoVille, daoReservationAffichage theDaoReservationAffichage, daoParticipants theDaoParticipants, daoObstaclesParties theDaoObstaclesParties, Window frm)
        {
           
            _daoUtilisateurs = theDaoUtilisateurs;
            _daoInfosSupPersonnel = theDaoInfosSupPersonnel;
            _daoRole = theDaoRole;
            _daoTheme = theDaoTheme;
            _daoVille = theDaoVille;
            _daoSalles = theDaoSalles;
            _daoReservation = theDaoReservation;
            _daoObstacles = theDaoObstacles;
            _daoEtatComptes = theDaoEtatCompte;
            _daoTransaction = theDaoTransaction;
            _daoParties = theDaoPartie;
            _daoReservationAffichage = theDaoReservationAffichage;
            _daoParticipants = theDaoParticipants;
            _daoObstaclesParties = theDaoObstaclesParties;

            Villes = _daoVille.SelectAll();
            Roles = _daoRole.SelectAll();
            Clients = _daoUtilisateurs.SelectAll();

            _viewLogin = new viewLogin(theDaoUtilisateurs, theDaoInfosSupPersonnel, Villes, Roles);

            Form = frm;
            Form.Title = "Escape Game - Technicien";
            Form.Icon = new BitmapImage(new Uri("pack://application:,,,/images/icon Escape Game cadre.ico"));

            WindowState = WindowState.Normal;

            VisibilityLogin = Visibility.Visible;
            VisibilityHome = Visibility.Hidden;
            HideAll();

            Pseudo = "";
            ErrorPseudo = new ContentControl();

            User = new Utilisateur();
            UserCity = new Ville();
        }

        public viewLogin ViewLogin { get=>_viewLogin; }

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
        public Ville Ville { get => _ville; set => _ville = value; }
        public InfosSupPersonnel UserInfosSupPersonnel { get => _userInfosSupPersonnel; set => _userInfosSupPersonnel = value; }
        public List<Salle> Salles { get => _salles; set => _salles = value; }
        public Salle SelectedSalle
        {
            get => _selectedSalle;
            set
            {
                _selectedSalle = value;
                OnPropertyChanged("SelectedSalle");
                refreshResaAffichage();
            }
        }
        public int SelectedSalleIndex { get => _selectedSalleIndex; set => _selectedSalleIndex = value; }


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
                if (_daoUtilisateurs.PseudoExists(Pseudo))
                {
                    Utilisateur tempUser = _daoUtilisateurs.SelectByPseudo(Pseudo);
                    if (tempUser.Personnel)
                    {
                        VisibilityLogin = Visibility.Hidden;
                        VisibilityHome = Visibility.Visible;

                        User = tempUser;
                        UserInfosSupPersonnel = _daoInfosSupPersonnel.SelectByUser(User);
                        UserCity = UserInfosSupPersonnel.Ville;

                        Form.Title += " | " + User.ToString();

                        UserPseudo = User.Identifiant;
                        UserNom = User.Nom;
                        UserPrenom = User.Prenom;
                        UserEmail = User.Mail;
                        UserTel = User.Tel.ToString();
                        UserDDN = User.DateNaissance.ToString("dd-MM-yyyy");

                        //WindowState = WindowState.Maximized;
                        Form.WindowState = WindowState.Maximized;
                        Form.Icon = new BitmapImage(new Uri("pack://application:,,,/images/icon Escape Game cadre.ico"));

                        Salles = _daoSalles.SelectByVille(UserCity);
                        SelectedSalleIndex = 0;
                        SelectedSalle = Salles[SelectedSalleIndex];

                        NbSalles = _daoSalles.Count(UserInfosSupPersonnel.Ville);

                        VisibilityPlanning1 = Visibility.Visible;

                        Obstacles = _daoObstacles.selectByVille(UserInfosSupPersonnel.Ville);

                        SelectedDate = DateTime.Now;
                    }
                    else
                    {
                        ErrorPseudo.Content = "Vous n'êtes pas membre du personnel";
                    }
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
        private ICommand _openResa, _upRooms, _downRooms;
        private DateTime _selectedDate = DateTime.Now;
        private reservationAffichage _resa9, _resa10, _resa11, _resa12, _resa13, _resa14, _resa15, _resa16, _resa17, _resa18, _resa19;
        private SolidColorBrush _fillRect9, _fillRect10, _fillRect11, _fillRect12, _fillRect13, _fillRect14, _fillRect15, _fillRect16, _fillRect17, _fillRect18, _fillRect19;

        public SolidColorBrush FillRect9
        {
            get => _fillRect9;
            set
            {
                _fillRect9 = value;
                OnPropertyChanged("FillRect9");
            }
        }
        public SolidColorBrush FillRect10
        {
            get => _fillRect10;
            set
            {
                _fillRect10 = value;
                OnPropertyChanged("FillRect10");
            }
        }
        public SolidColorBrush FillRect11
        {
            get => _fillRect11;
            set
            {
                _fillRect11 = value;
                OnPropertyChanged("FillRect11");
            }
        }
        public SolidColorBrush FillRect12
        {
            get => _fillRect12;
            set
            {
                _fillRect12 = value;
                OnPropertyChanged("FillRect12");
            }
        }
        public SolidColorBrush FillRect13
        {
            get => _fillRect13;
            set
            {
                _fillRect13 = value;
                OnPropertyChanged("FillRect13");
            }
        }
        public SolidColorBrush FillRect14
        {
            get => _fillRect14;
            set
            {
                _fillRect14 = value;
                OnPropertyChanged("FillRect14");
            }
        }
        public SolidColorBrush FillRect15
        {
            get => _fillRect15;
            set
            {
                _fillRect15 = value;
                OnPropertyChanged("FillRect15");
            }
        }
        public SolidColorBrush FillRect16
        {
            get => _fillRect16;
            set
            {
                _fillRect16 = value;
                OnPropertyChanged("FillRect16");
            }
        }
        public SolidColorBrush FillRect17
        {
            get => _fillRect17;
            set
            {
                _fillRect17 = value;
                OnPropertyChanged("FillRect17");
            }
        }
        public SolidColorBrush FillRect18
        {
            get => _fillRect18;
            set
            {
                _fillRect18 = value;
                OnPropertyChanged("FillRect18");
            }
        }
        public SolidColorBrush FillRect19
        {
            get => _fillRect19;
            set
            {
                _fillRect19 = value;
                OnPropertyChanged("FillRect19");
            }
        }


        public reservationAffichage Resa9
        {
            get => _resa9;
            set
            {
                _resa9 = value;
                OnPropertyChanged("Resa9");
            }
        }
        public reservationAffichage Resa10
        {
            get => _resa10;
            set
            {
                _resa10 = value;
                OnPropertyChanged("Resa10");
            }
        }
        public reservationAffichage Resa11
        {
            get => _resa11;
            set
            {
                _resa11 = value;
                OnPropertyChanged("Resa11");
            }
        }
        public reservationAffichage Resa12
        {
            get => _resa12;
            set
            {
                _resa12 = value;
                OnPropertyChanged("Resa12");
            }
        }
        public reservationAffichage Resa13
        {
            get => _resa13;
            set
            {
                _resa13 = value;
                OnPropertyChanged("Resa13");
            }
        }
        public reservationAffichage Resa14
        {
            get => _resa14;
            set
            {
                _resa14 = value;
                OnPropertyChanged("Resa14");
            }
        }
        public reservationAffichage Resa15
        {
            get => _resa15;
            set
            {
                _resa15 = value;
                OnPropertyChanged("Resa15");
            }
        }
        public reservationAffichage Resa16
        {
            get => _resa16;
            set
            {
                _resa16 = value;
                OnPropertyChanged("Resa16");
            }
        }
        public reservationAffichage Resa17
        {
            get => _resa17;
            set
            {
                _resa17 = value;
                OnPropertyChanged("Resa17");
            }
        }
        public reservationAffichage Resa18
        {
            get => _resa18;
            set
            {
                _resa18 = value;
                OnPropertyChanged("Resa18");
            }
        }
        public reservationAffichage Resa19
        {
            get => _resa19;
            set
            {
                _resa19 = value;
                OnPropertyChanged("Resa19");
            }
        }

        private void refreshResaAffichage()
        {
            Resa9 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 9, Clients);
            if (Resa9.User != null)
            {
                FillRect9 = new SolidColorBrush();
                FillRect9.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect9 = new SolidColorBrush();
                FillRect9.Color = Color.FromRgb(0, 157, 31);
            }

            Resa10 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 10, Clients);
            if(Resa10.User != null)
            {
                FillRect10 = new SolidColorBrush();
                FillRect10.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect10 = new SolidColorBrush();
                FillRect10.Color = Color.FromRgb(0, 157, 31);
            }

            Resa11 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 11, Clients);
            if (Resa11.User != null)
            {
                FillRect11 = new SolidColorBrush();
                FillRect11.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect11 = new SolidColorBrush();
                FillRect11.Color = Color.FromRgb(0, 157, 31);
            }

            Resa12 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 12, Clients);
            if (Resa12.User != null)
            {
                FillRect12 = new SolidColorBrush();
                FillRect12.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect12 = new SolidColorBrush();
                FillRect12.Color = Color.FromRgb(0, 157, 31);
            }

            Resa13 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 13, Clients);
            if (Resa13.User != null)
            {
                FillRect13 = new SolidColorBrush();
                FillRect13.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect13 = new SolidColorBrush();
                FillRect13.Color = Color.FromRgb(0, 157, 31);
            }

            Resa14 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 14, Clients);
            if (Resa14.User != null)
            {
                FillRect14 = new SolidColorBrush();
                FillRect14.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect14 = new SolidColorBrush();
                FillRect14.Color = Color.FromRgb(0, 157, 31);
            }

            Resa15 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 15, Clients);
            if (Resa15.User != null)
            {
                FillRect15 = new SolidColorBrush();
                FillRect15.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect15 = new SolidColorBrush();
                FillRect15.Color = Color.FromRgb(0, 157, 31);
            }

            Resa16 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 16, Clients);
            if (Resa16.User != null)
            {
                FillRect16 = new SolidColorBrush();
                FillRect16.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect16 = new SolidColorBrush();
                FillRect16.Color = Color.FromRgb(0, 157, 31);
            }

            Resa17 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 17, Clients);
            if (Resa17.User != null)
            {
                FillRect17 = new SolidColorBrush();
                FillRect17.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect17 = new SolidColorBrush();
                FillRect17.Color = Color.FromRgb(0, 157, 31);
            }

            Resa18 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 18, Clients);
            if (Resa18.User != null)
            {
                FillRect18 = new SolidColorBrush();
                FillRect18.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect18 = new SolidColorBrush();
                FillRect18.Color = Color.FromRgb(0, 157, 31);
            }

            Resa19 = _daoReservationAffichage.getReservationAffichage(SelectedSalle, SelectedDate, 19, Clients);
            if (Resa19.User != null)
            {
                FillRect19 = new SolidColorBrush();
                FillRect19.Color = Color.FromRgb(215, 0, 0);
            }
            else
            {
                FillRect19 = new SolidColorBrush();
                FillRect19.Color = Color.FromRgb(0, 157, 31);
            }
        }


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
                refreshResaAffichage();
                refreshHorairesDisponibles();
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

        public ICommand UpRoom
        {
            get
            {
                if (this._upRooms == null)
                    this._upRooms = new RelayCommand(() => this.upRoom(), () => true);

                return this._upRooms;
            }
        }

        public void upRoom()
        {
            if(SelectedSalleIndex == Salles.Count - 1)
            {
                SelectedSalleIndex = 0;
                SelectedSalle = Salles[SelectedSalleIndex];
            }
            else
            {
                SelectedSalleIndex++;
                SelectedSalle = Salles[SelectedSalleIndex];
            }
        }

        public ICommand DownRoom
        {
            get
            {
                if (this._downRooms == null)
                    this._downRooms = new RelayCommand(() => this.downRoom(), () => true);

                return this._downRooms;
            }
        }

        public void downRoom()
        {
            if(SelectedSalleIndex == 0)
            {
                SelectedSalleIndex = Salles.Count - 1;
                SelectedSalle = Salles[SelectedSalleIndex];
            }
            else
            {
                SelectedSalleIndex--;
                SelectedSalle = Salles[SelectedSalleIndex];
            }
        }

        public void dispPlanning()
        {
            NbSalles = _daoSalles.Count(UserInfosSupPersonnel.Ville);

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
        private ObservableCollection<Obstacle> _selectedObstacles = new ObservableCollection<Obstacle>();
        private Obstacle _selectedObstacleToAdd, _selectedObstacleToRemove;

        private bool _enableAddObstacleButton, _enableRemoveObstacleButton;
        private ICommand _addObstacleButton, _removeObstacleButton;

        private ObservableCollection<Utilisateur> _selectedParticipants = new ObservableCollection<Utilisateur>();
        private Utilisateur _selectedParticipantToAdd, _selectedParticipantToRemove;

        private bool _enableAddParticipantButton, _enableRemoveParticipantButton;
        private ICommand _addParticipantButton, _removeParticipantButton;

        private int _nbJoueurs, _nbObstacles;
        private double _prixParticipants, _prixObstacles, _prixTotal;




        public ICommand AddParticipantButton
        {
            get
            {
                if (this._addParticipantButton == null)
                    this._addParticipantButton = new RelayCommand(() => this.addParticipantButton(), () => true);

                return this._addParticipantButton;
            }
        }

        public void addParticipantButton()
        {
            if(SelectedParticipantToAdd != null)
            {
                if(SelectedParticipantToAdd != SelectedClient)
                {
                    if (!SelectedParticipants.Contains(SelectedParticipantToAdd))
                    {
                        SelectedParticipants.Add(SelectedParticipantToAdd);
                    }
                }
            }
        }


        public ICommand RemoveParticipantButton
        {
            get
            {
                if (this._removeParticipantButton == null)
                    this._removeParticipantButton = new RelayCommand(() => this.removeParticipantButton(), () => true);

                return this._removeParticipantButton;
            }
        }

        public void removeParticipantButton()
        {
            if(SelectedParticipantToRemove != null)
            {
                SelectedParticipants.Remove(SelectedParticipantToRemove);
            }
        }

        public Utilisateur SelectedParticipantToRemove
        {
            get => _selectedParticipantToRemove;
            set
            {
                _selectedParticipantToRemove = value;
                OnPropertyChanged("SelectedParticipantToRemove");
                if (SelectedParticipantToRemove != null)
                {
                    EnableRemoveParticipantButton = true;
                }
                else
                {
                    EnableRemoveParticipantButton = false;
                }
            }
        }
        public Utilisateur SelectedParticipantToAdd
        {
            get => _selectedParticipantToAdd;
            set
            {
                _selectedParticipantToAdd = value;
                OnPropertyChanged("SelectedParticipantToAdd");
                if(SelectedParticipantToAdd != null)
                {
                    EnableAddParticipantButton = true;
                }
                else
                {
                    EnableAddParticipantButton = false;
                }
            }
        }

        public bool EnableAddParticipantButton
        {
            get => _enableAddParticipantButton;
            set
            {
                _enableAddParticipantButton = value;
                OnPropertyChanged("EnableAddParticipantButton");
            }
        }
        public bool EnableRemoveParticipantButton
        {
            get => _enableRemoveParticipantButton;
            set
            {
                _enableRemoveParticipantButton = value;
                OnPropertyChanged("EnableRemoveParticipantButton");
            }
        }








        public ICommand AddObstacleButton
        {
            get
            {
                if (this._addObstacleButton == null)
                    this._addObstacleButton = new RelayCommand(() => this.addObstacleButton(), () => true);

                return this._addObstacleButton;
            }
        }

        public void addObstacleButton()
        {
            if (SelectedObstacleToAdd != null)
            {
                if (!SelectedObstacles.Contains(SelectedObstacleToAdd))
                {
                    SelectedObstacles.Add(SelectedObstacleToAdd);
                }
            }
        }


        public ICommand RemoveObstacleButton
        {
            get
            {
                if (this._removeObstacleButton == null)
                    this._removeObstacleButton = new RelayCommand(() => this.removeObstacleButton(), () => true);

                return this._removeObstacleButton;
            }
        }

        public void removeObstacleButton()
        {
            if (SelectedObstacleToRemove != null)
            {
                SelectedObstacles.Remove(SelectedObstacleToRemove);
            }
        }

        public Obstacle SelectedObstacleToRemove
        {
            get => _selectedObstacleToRemove;
            set
            {
                _selectedObstacleToRemove = value;
                OnPropertyChanged("SelectedObstacleToRemove");
                if (SelectedObstacleToRemove != null)
                {
                    EnableRemoveObstacleButton = true;
                }
                else
                {
                    EnableRemoveObstacleButton = false;
                }
            }
        }
        public Obstacle SelectedObstacleToAdd
        {
            get => _selectedObstacleToAdd;
            set
            {
                _selectedObstacleToAdd = value;
                OnPropertyChanged("SelectedObstacleToAdd");
                if (SelectedObstacleToAdd != null)
                {
                    EnableAddObstacleButton = true;
                }
                else
                {
                    EnableAddObstacleButton = false;
                }
            }
        }

        public bool EnableAddObstacleButton
        {
            get => _enableAddObstacleButton;
            set
            {
                _enableAddObstacleButton = value;
                OnPropertyChanged("EnableAddObstacleButton");
            }
        }
        public bool EnableRemoveObstacleButton
        {
            get => _enableRemoveObstacleButton;
            set
            {
                _enableRemoveObstacleButton = value;
                OnPropertyChanged("EnableRemoveObstacleButton");
            }
        }





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


        public ObservableCollection<Utilisateur> SelectedParticipants
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
                OnPropertyChanged("SelecteHoraire");
            }
        }

        public List<string> HorairesDispos
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

        

        public void refreshHorairesDisponibles()
        {
            List<string> tempList = new List<string>();

            if (Resa9.User == null)
                tempList.Add("9h");
            if (Resa10.User == null)
                tempList.Add("10h");
            if (Resa11.User == null)
                tempList.Add("11h");
            if (Resa12.User == null)
                tempList.Add("12h");
            if (Resa13.User == null)
                tempList.Add("13h");
            if (Resa14.User == null)
                tempList.Add("14h");
            if (Resa15.User == null)
                tempList.Add("15h");
            if (Resa16.User == null)
                tempList.Add("16h");
            if (Resa17.User == null)
                tempList.Add("17h");
            if (Resa18.User == null)
                tempList.Add("18h");
            if (Resa19.User == null)
                tempList.Add("19h");

            HorairesDispos = tempList;
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
                CreditsClient = _daoUtilisateurs.Credit(SelectedClient, Clients, Parties, _daoTransaction, _daoReservation).ToString();
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

                refreshTotalResa();
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

        public ObservableCollection<Obstacle> SelectedObstacles
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
                refreshTotalResa();
            }
        }

        private void refreshTotalResa()
        {
            //créer les éléments à basculer ensuite dans les accesseurs
            int nbJoueurs, nbObstacles;
            double prixParticipants, prixObstacles, prixTotal;

            nbJoueurs = SelectedParticipants.Count;
            nbObstacles = SelectedObstacles.Count;

            prixParticipants = nbJoueurs * 8;
            prixObstacles = 0;
            foreach (Obstacle o in SelectedObstacles)
            {
                prixObstacles += o.Prix;
            }
            prixTotal = prixParticipants + prixObstacles;

            NbJoueurs = nbJoueurs;
            PrixParticipants = prixParticipants;
            NbObstacles = nbObstacles;
            PrixObstacles = prixObstacles;
            PrixTotal = prixTotal;
        }




        public ICommand ValidResa
        {
            get
            {
                if (this._validResa == null)
                    this._validResa = new RelayCommand(() => this.validResa(), () => true);

                return this._validResa;
            }
        }

        private void validResa()
        {
            _daoParties.Insert(SelectedClient, SelectedDate, SelecteHoraire, SelectedSalle);
            Partie dernierePartie = _daoParties.SelectDernierePartie(Clients);
            _daoParticipants.Insert(SelectedParticipants, dernierePartie);
            _daoReservation.Insert(SelectedClient, PrixTotal, dernierePartie, DateTime.Now);
            _daoObstaclesParties.Insert(SelectedObstacles, dernierePartie);

            resetResa();
            VisibilityPlanning1 = Visibility.Visible;
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
            User = _daoUtilisateurs.SelectById(User.Id);
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
            _daoUtilisateurs.updateUser(User, TempPseudo, TempEmail, TempTel, TempDDN);
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
