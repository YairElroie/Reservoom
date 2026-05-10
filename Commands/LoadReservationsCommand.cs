using Reservoom.Models;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly ReservationListingViewModel _viewModel;
        //private ReservationListingViewModel reservationListingViewModel;

        public LoadReservationsCommand(ReservationListingViewModel reservationListingViewModel, HotelStore hotelStore)
        {
            _viewModel = reservationListingViewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;
            try
            {

                await _hotelStore.Load();

                _viewModel.UpdateReservations(_hotelStore.Reservations);

                //IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();
                //_viewModel.UpdateReservations(reservations);

            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to load reservations";    
                //MessageBox.Show("Failed to make reservation", "Error", MessageBoxButton.OK);
            }   
            _viewModel.IsLoading = false;
        }
    }
}
