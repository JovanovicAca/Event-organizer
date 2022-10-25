using HCI_Projekat.Core;
using System;

namespace HCI_Projekat.MVVM.ViewModel
{
    class UserViewModel : ObservableObject
    {
        public RelayCommand UserKrProslavuCommand { get; set; }
        public RelayCommand UserTrenutneProslaveCommand { get; set; }
        public RelayCommand UserGotoveProslaveCommand { get; set; }
        public RelayCommand UserPersonalInfoCommand { get; set; }

        public UserKrProslavuViewModel UserKrProslavuVM { get; set; }
        public UserTrenutneProslaveViewModel UserTrenutneProslaveVM { get; set; }
        public UserGotoveProslaveViewModel UserGotoveProslaveVM { get; set; }
        public UserPersonalInfoViewModel UserPersonalInfoVM { get; set; }


        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public UserViewModel()
        {
            UserKrProslavuVM = new UserKrProslavuViewModel();
            UserTrenutneProslaveVM = new UserTrenutneProslaveViewModel();
            UserGotoveProslaveVM = new UserGotoveProslaveViewModel();
            UserPersonalInfoVM = new UserPersonalInfoViewModel();
            CurrentView = UserKrProslavuVM;

            UserKrProslavuCommand = new RelayCommand(o => {
                CurrentView = UserKrProslavuVM;
            });
            UserTrenutneProslaveCommand = new RelayCommand(o => {
                CurrentView = UserTrenutneProslaveVM;
            });
            UserGotoveProslaveCommand = new RelayCommand(o => {
                CurrentView = UserGotoveProslaveVM;
            });
            UserPersonalInfoCommand = new RelayCommand(o => {
                CurrentView = UserPersonalInfoVM;
            });
        }
    }
}
