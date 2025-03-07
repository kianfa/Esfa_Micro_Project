using MiniProject.Core;
using MiniProject.MVVM.ViewModel;
using ModernDesign.Core;
using System.ComponentModel;


namespace MiniProject.MVVM.ViewModel
{
    class MainViewModel : ObservebleObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }


        public HomeViewModel HomeVm { get; set; }
        public DiscoveryViewModel DiscoveryVm { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                onPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            DiscoveryVm = new DiscoveryViewModel();

            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVm;
            });
            DiscoveryViewCommand = new RelayCommand(x =>
            {
                CurrentView = DiscoveryVm;
            });

        }


        
        private string _header;
        private string _description;
        private DateTime? _selectedDate;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged(nameof(Header));
                OnPropertyChanged(nameof(IsCalendarVisible)); // Notify change in visibility
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(IsCalendarVisible)); // Notify change in visibility
            }
        }

        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        // Property to determine if the calendar should be visible
        public bool IsCalendarVisible => !string.IsNullOrEmpty(Header) && !string.IsNullOrEmpty(Description);

        public event PropertyChangedEventHandler PropertyChanged_;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged_?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
