using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using viewModel;
using System.Windows;
using ModeleMetier.model;
using ModeleMetier.metier;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace viewModel
{
    public class viewLogin : viewModelBase
    {
        private daoUtilisateurs _daoUtilisateurs;
        private daoInfosSupPersonnel _daoInfosSupPersonnel;
        private Utilisateur _user;

        public bool isUserConnected;

        private string _pseudo;

        private ICommand loginCommand;

        private Visibility _loginViewVisibility;

        public viewLogin(daoUtilisateurs theDaoUtilisateurs, daoInfosSupPersonnel theDaoInfosSupPersonnel, List<Ville> lesVilles, List<Role> lesRoles)
        {
            _daoUtilisateurs = theDaoUtilisateurs;
            _daoInfosSupPersonnel = theDaoInfosSupPersonnel;
            IsUserConnected = false;
        }

        public viewLogin TheView
        {
            get
            {
                return this;
            }
        }

        public bool IsUserConnected
        {
            get
            {
                return isUserConnected;
            }
            set
            {
                isUserConnected = value;
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (this.loginCommand == null)
                    this.loginCommand = new RelayCommand(() => this.loginUser(), () => true);

                return this.loginCommand;
            }
        }

        public void loginUser()
        {
            if (Pseudo.Length > 0)
            {

            }
        }

        public Visibility VisibilityLogin
        {
            get
            {
                return _loginViewVisibility;
            }
            set
            {
                _loginViewVisibility = value;
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
    }
}
