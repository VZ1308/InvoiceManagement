using InvoiceManagement.ViewModels;
using System.Windows;

namespace InvoiceManagement
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;
        private AddInvoiceViewModel _addInvoiceViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel(); // stellt Logik des Hauptfensters bereit
            DataContext = _mainViewModel; // Setze den DataContext auf das ViewModel
            _addInvoiceViewModel = new AddInvoiceViewModel();
        }

        private void AddInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            var addInvoiceWindow = new AddInvoiceWindow(_addInvoiceViewModel);
            addInvoiceWindow.ShowDialog(); // Öffne das Fenster als modales Fenster
        }

        private void ShowAllInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.LoadInvoices(); // Lade die Rechnungen
            Listbox.ItemsSource = _mainViewModel.InvoiceList; //weist die ListBox-Eigenschaft ItemsSource die InvoiceList des MainViewModel zu
        }

        private void ShowMonthlyReportButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Monatsbericht wird angezeigt");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();  
        }
    }
}

