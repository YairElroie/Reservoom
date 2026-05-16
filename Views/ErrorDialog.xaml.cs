using System.Windows;

namespace Reservoom.Views
{
    public partial class ErrorDialog : Window
    {
        public ErrorDialog(string message = "An error occurred. Please try again.")
        {
            InitializeComponent();
            MessageText.Text = message;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
