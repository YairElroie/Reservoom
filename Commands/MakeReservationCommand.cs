using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Reservoom.Commands
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly Hotel _hotel;
        //private readonly Services.NavigationService _resrevationViewNavigationService;
        private readonly Services.NavigationService<ReservationListingViewModel> _resrevationViewNavigationService;
        private readonly HotelStore _hotelStore;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, Services.NavigationService<ReservationListingViewModel> reservationViewNavigationService, HotelStore hotelStore)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotelStore = hotelStore;
            _resrevationViewNavigationService = reservationViewNavigationService;
            _makeReservationViewModel.PropertyChanged += OnViewModelProperrtyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
                   _makeReservationViewModel.StartDate < _makeReservationViewModel.EndDate;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomId(_makeReservationViewModel.RoomNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username,
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate);

            try
            {
                await _hotelStore.MakeReservation(reservation);
                //await _hotel.MakeReservation(reservation);
                new Reservoom.Views.SuccessDialog().ShowDialog();

                //_resrevationViewNavigationService.Navigate();

            }
            catch (ReservationConflictException)
            {
                new Reservoom.Views.ErrorDialog("This room is already taken for the selected dates.").ShowDialog();
            }
            catch (Exception)
            {
                new Reservoom.Views.ErrorDialog("Failed to make reservation. Please try again.").ShowDialog();
            }
        }

        private void OnViewModelProperrtyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
                e.PropertyName == nameof(MakeReservationViewModel.StartDate) ||
                e.PropertyName == nameof(MakeReservationViewModel.EndDate))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
