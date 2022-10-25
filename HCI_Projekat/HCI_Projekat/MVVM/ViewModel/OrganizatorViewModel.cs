using HCI_Projekat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.MVVM.ViewModel
{
    class OrganizatorViewModel : ObservableObject
    {
        public RelayCommand OrgKreirajSaradnikaCommand { get; set; }
        public RelayCommand OrgKreirajProslavuCommand { get; set; }
        public RelayCommand OrgTrenutneProslaveCommand { get; set; }
        public RelayCommand OrgTrenutniSaradniciCommand { get; set; }
        public RelayCommand OrgGotoveProslaveCommand { get; set; }
        public RelayCommand UserGotoveProslaveCommand { get; set; }

        public OrgKrProslavuViewModel OrgKreirajProslavuVM { get; set; }
        public UserGotoveProslaveViewModel UserGotoveProslaveVM { get; set; }
        public OrgKrSaradnikaViewModel OrgKreirajSaradnikaVM { get; set; }
        public OrgTrenutneProslaveViewModel OrgTrenutneProslaveVM { get; set; }
        public OrgTrenutniSaradniciViewModel OrgTrenutniSaradniciVM { get; set; }
        public OrgGotoveProslaveViewModel OrgGotoveProslaveVM { get; set; }

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
        public OrganizatorViewModel()
        {
            OrgKreirajProslavuVM = new OrgKrProslavuViewModel();
            OrgKreirajSaradnikaVM = new OrgKrSaradnikaViewModel();
            OrgTrenutneProslaveVM = new OrgTrenutneProslaveViewModel();
            OrgTrenutniSaradniciVM = new OrgTrenutniSaradniciViewModel();
            OrgGotoveProslaveVM = new OrgGotoveProslaveViewModel();
            CurrentView = OrgKreirajProslavuVM;

            OrgKreirajProslavuCommand = new RelayCommand(o => {
                CurrentView = OrgKreirajProslavuVM;
            });
            OrgKreirajSaradnikaCommand = new RelayCommand(o => {
                CurrentView = OrgKreirajSaradnikaVM;
            });
            OrgTrenutneProslaveCommand = new RelayCommand(o => {
                CurrentView = OrgTrenutneProslaveVM;
            });
            OrgTrenutniSaradniciCommand = new RelayCommand(o => {
                CurrentView = OrgTrenutniSaradniciVM;
            });
            OrgGotoveProslaveCommand = new RelayCommand(o => {
                CurrentView = OrgGotoveProslaveVM;
            });
        }
    }
}
