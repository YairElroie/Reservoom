using System.Windows;

namespace Reservoom.Commands
{
    public class ShowInfoCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            MessageBox.Show(
                "Reservoom - Hotel Reservation App\n\n" +
                "• Fill in Username, Floor No., and Room No.\n" +
                "• Select a Start Date and End Date.\n" +
                "• Click Submit to confirm the reservation.\n" +
                "• Click Cancel to go back to the reservations list.",
                "Help",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
