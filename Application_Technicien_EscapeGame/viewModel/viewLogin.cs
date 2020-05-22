using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using viewModel;
using ModeleMetier.model;
using ModeleMetier.metier;
using System.Windows.Input;

namespace viewModel
{
    public class viewLogin : viewModelBase
    {
        private daoUtilisateurs _daoUtilisateur;
        private daoInfosSupPersonnel _daoInfosSupPersonnel;
        private Utilisateur _user;

        private string _pseudo;

        private ICommand loginCommand;

        private string _visibility;

        public viewLogin(daoUtilisateurs theDaoUtilisateurs, daoInfosSupPersonnel theDaoInfosSupPersonnel, List<Ville> lesVilles, List<Role> lesRoles)
        {
            _daoUtilisateur = theDaoUtilisateurs;
            _daoInfosSupPersonnel = theDaoInfosSupPersonnel;
        }

        public viewLogin TheView
        {
            get
            {
                return this;
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
            if (_daoPersonnel.PseudoExists(Pseudo))
            {
                Visibility = "Hidden";
            }
        }

        public string Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;
                OnPropertyChanged("Visibility");
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
