using HCI_Projekat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.MVVM.ViewModel
{
    class AdminViewModel : ObservableObject
    {
        public RelayCommand AdminGotoveProslaveCommand { get; set; }
        public RelayCommand AdminKrProslavuCommand { get; set; }
        public RelayCommand AdminKrSaradnikaCommand { get; set; }
        public RelayCommand AdminTrenutneProslaveCommand { get; set; }
        public RelayCommand AdminTrenutniSaradniciCommand { get; set; }

        public AdminTrenutneProslaveViewModel AdminTrenutneProslaveVM { get; set; }
        public AdminTrenutniSaradniciViewModel AdminTrenutniSaradniciVM { get; set; }
        public AdminKrProslavuViewModel AdminKrProslavuVM { get; set; }
        public AdminKrSaradnikaViewModel AdminKrSaradnikaVM { get; set; }
        public AdminGotoveProslaveviewModel AdminGotoveProslaveVM { get; set; }

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

        public AdminViewModel()
        {
            AdminKrProslavuVM = new AdminKrProslavuViewModel();
            AdminKrSaradnikaVM = new AdminKrSaradnikaViewModel();
            AdminTrenutneProslaveVM = new AdminTrenutneProslaveViewModel();
            AdminTrenutniSaradniciVM = new AdminTrenutniSaradniciViewModel();
            AdminGotoveProslaveVM = new AdminGotoveProslaveviewModel();
            CurrentView = AdminKrProslavuVM;

            AdminKrProslavuCommand = new RelayCommand(o => {
                CurrentView = AdminKrProslavuVM;
            });
            AdminKrSaradnikaCommand = new RelayCommand(o => {
                CurrentView = AdminKrSaradnikaVM;
            });
            AdminTrenutneProslaveCommand = new RelayCommand(o => {
                CurrentView = AdminTrenutneProslaveVM;
            });
            AdminTrenutniSaradniciCommand = new RelayCommand(o => {
                CurrentView = AdminTrenutniSaradniciVM;
            });
            AdminGotoveProslaveCommand = new RelayCommand(o => {
                CurrentView = AdminGotoveProslaveVM;
            });
        }
    }
}
