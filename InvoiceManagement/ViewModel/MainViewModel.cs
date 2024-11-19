using System;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Windows;

namespace InvoiceManagement.ViewModels
{
    public class MainViewModel 
    {
        public ObservableCollection<string> InvoiceList { get; set; } = new ObservableCollection<string>();

        // Lädt alle Rechnungen und gibt Fehlermeldungen aus, wenn Fehler auftreten
        public void LoadInvoices()
        {
            string folderPath = GetInvoicesFolderPath();

            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("Der Rechnungsordner konnte nicht gefunden werden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var invoiceFiles = Directory.GetFiles(folderPath, "Invoice_*.txt");

                InvoiceList.Clear();

                foreach (var file in invoiceFiles)
                {
                    var content = File.ReadAllText(file);
                    InvoiceList.Add(content);
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Fehler beim Laden der Rechnungen: {ioEx.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unbekannter Fehler: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
