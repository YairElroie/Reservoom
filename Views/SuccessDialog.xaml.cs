using System.Windows;

namespace Reservoom.Views
{
    public partial class SuccessDialog : Window
    {
        public SuccessDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
