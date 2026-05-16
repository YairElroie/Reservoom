using System.Windows;

namespace Reservoom.Views
{
    public partial class InfoDialog : Window
    {
        public InfoDialog(string title = "Information", string message = "Here is some information for you.")
        {
            InitializeComponent();
            TitleText.Text = title;
            MessageText.Text = message;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
