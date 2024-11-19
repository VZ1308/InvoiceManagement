using InvoiceManagement.ViewModels;
using System.Windows;

namespace InvoiceManagement
{
    public partial class AddInvoiceWindow : Window
    {
        private readonly AddInvoiceViewModel _viewModel;

        public AddInvoiceWindow(AddInvoiceViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel; 
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SaveInvoice())  
            {
                this.Close(); 
            }
        }
    }
}
