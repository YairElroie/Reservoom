using Reservoom.Commands;
using Reservoom.Models;
using Reservoom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservoom.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;

        public MakeReservationViewModel MakeReservationViewModel { get; }
        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public ICommand LoadReservationsCommand { get; }
        public ICommand MakeReservationCommand { get; }

        private readonly HotelStore _hotelStore;

        private string _errorMessage;

        public string ErrorMessage
        {
            get 
            { 
                return _errorMessage; 
            }
            set 
            { 
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }


        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isLoading;
        public bool IsLoading
        {
            get 
            { 
                return _isLoading; 
            }
            set 
            { 
                _isLoading = value; 
                OnPropertyChanged(nameof(IsLoading));
            }
        }


        public ReservationListingViewModel(HotelStore hotelStore, Services.NavigationService<MakeReservationViewModel> makeReservationNavigationService)
        //public ReservationListingViewModel(HotelStore hotelStore, MakeReservationViewModel makeReservationViewModel,Services.NavigationService makeReservationNavigationService)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            //MakeReservationViewModel = makeReservationViewModel;

            LoadReservationsCommand = new LoadReservationsCommand(this, hotelStore);
            MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(makeReservationNavigationService);

            _hotelStore = hotelStore;
            hotelStore.ReservationMade+= OnReservationMade;
            //UpdateReservations();

            //_reservations.Add(new ReservationViewModel(new Models.Reservation(
            //    new Models.RoomId(1, 3),
            //    "Yair1",
            //    DateTime.Now,
            //    DateTime.Now)));

            //_reservations.Add(new ReservationViewModel(new Models.Reservation(
            // new Models.RoomId(2, 4),
            // "Yair2",
            // DateTime.Now,
            // DateTime.Now)));

            //_reservations.Add(new ReservationViewModel(new Models.Reservation(
            // new Models.RoomId(3, 5),
            // "Yair3",
            // DateTime.Now,
            // DateTime.Now)));
        }


        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMade;

            base.Dispose();
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);

            _reservations.Add(reservationViewModel);
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore , Services.NavigationService<MakeReservationViewModel> makeReservationNavigationService)
        //public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, MakeReservationViewModel makeReservationViewModel, Services.NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);
            viewModel.LoadReservationsCommand.Execute(null);
            return viewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach (var reservation in reservations)
            {
                _reservations.Add(new ReservationViewModel(reservation));
            }
        }
    }
}
