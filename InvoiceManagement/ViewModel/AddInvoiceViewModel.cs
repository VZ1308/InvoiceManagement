using System.ComponentModel;
using System.IO;
using System.Windows;
using Invoice_Management.Model;

namespace InvoiceManagement.ViewModels
{
    public class AddInvoiceViewModel : INotifyPropertyChanged
    {
        private Invoice _currentInvoice = new Invoice()
        {
            CompanyName = "WPF Bau GmbH"
        };

        public Invoice CurrentInvoice
        {
            get => _currentInvoice;
            set
            {
                _currentInvoice = value;
                OnPropertyChanged(nameof(CurrentInvoice)); // Meldet der UI, dass sich Rechnung geändert hat
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Speichern der Rechnung mit Validierung
        public bool SaveInvoice()
        {
            string validationMessage = ValidateInvoice();

            if (!string.IsNullOrEmpty(validationMessage))
            {
                // Zeigt die Fehlermeldung an, wenn die Validierung fehlschlägt
                MessageBox.Show(validationMessage, "Eingabefehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false; 
            }

            // Holt den Rechnungsordnerpfad
            string folderPath = GetInvoicesFolderPath();

            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("Der Rechnungsordner konnte nicht erstellt oder gefunden werden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; 
            }

            // Erstellen eines Dateinamens mit Datum und Zeit
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
            string fileName = $"Invoice_{timestamp}.txt";
            string filePath = Path.Combine(folderPath, fileName);

            string invoiceData = $"Firma: {CurrentInvoice.CompanyName}, Kunde: {CurrentInvoice.CustomerName}, " +
                                 $"Kundennummer: {CurrentInvoice.CustomerNumber}, Artikel: {CurrentInvoice.ItemDescription}, " +
                                 $"Anzahl: {CurrentInvoice.Quantity}, Preis pro Stück: {CurrentInvoice.PricePerUnit:C}, " +
                                 $"Gesamtpreis (inkl. MwSt): {CurrentInvoice.TotalPrice:C}";

            try
            {
                // Speichert die Datei
                File.WriteAllText(filePath, invoiceData);
                MessageBox.Show($"Rechnung erfolgreich gespeichert in:\n{filePath}", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                return true; // Speichern erfolgreich
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Fehler beim Speichern der Datei: {ioEx.Message}", "Speicherfehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; // Speichern schlägt fehl
            }
            catch (UnauthorizedAccessException uaEx)
            {
                MessageBox.Show($"Zugriffsfehler: {uaEx.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unbekannter Fehler: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; 
            }
        }

        // Validierungslogik im ViewModel
        private string ValidateInvoice()
        {
            if (string.IsNullOrWhiteSpace(CurrentInvoice.CompanyName))
                return "Der Firmenname darf nicht leer sein.";

            if (string.IsNullOrWhiteSpace(CurrentInvoice.CustomerName))
                return "Der Kundenname darf nicht leer sein.";

            if (string.IsNullOrWhiteSpace(CurrentInvoice.CustomerNumber))
                return "Die Kundennummer darf nicht leer sein.";

            if (CurrentInvoice.CustomerNumber.Length < 5)
                return "Die Kundennummer muss mindestens 5 Zeichen lang sein.";

            if (!CurrentInvoice.CustomerNumber.StartsWith("KU"))
                return "Die Kundennummer muss mit 'KU' beginnen.";

            if (string.IsNullOrWhiteSpace(CurrentInvoice.ItemDescription))
                return "Die Artikelbeschreibung darf nicht leer sein.";

            if (CurrentInvoice.Quantity <= 0 || CurrentInvoice.Quantity > 10000)
                return "Die Anzahl muss zwischen 1 und 10.000 liegen.";

            if (CurrentInvoice.PricePerUnit <= 0 || CurrentInvoice.PricePerUnit > 10000)
                return "Der Preis pro Stück muss größer als 0 und kleiner als 10.000 sein.";

            return string.Empty; // Keine Fehler
        }

        // Holt den Pfad zum Rechnungsordner
        private string GetInvoicesFolderPath()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Invoices");

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                return folderPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen des Verzeichnisses: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }
    }
}
