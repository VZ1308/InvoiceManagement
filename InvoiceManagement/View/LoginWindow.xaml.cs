using InvoiceManagement;
using System.Windows;

namespace Invoice_Management
{
    /// <summary>
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                MessageBox.Show("Bitte füllen Sie alle Felder aus.", "Fehler");
            }
            else
            {
                if (UsernameTextBox.Text == "123" && PasswordTextBox.Password == "123")
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();  
                }
                else
                {
                    MessageBox.Show("Passwort oder Username ungültig. Bitte versuchen Sie es erneut.", "Login fehlgeschlagen");
                }
            }
        }

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBoxVisible.Text = PasswordTextBox.Password;
            PasswordTextBoxVisible.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Password = PasswordTextBoxVisible.Text;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordTextBoxVisible.Visibility = Visibility.Collapsed;
        }
    }
}
