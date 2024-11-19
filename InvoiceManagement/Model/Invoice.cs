namespace Invoice_Management.Model
{
    public class Invoice
    {
        private string _companyName;
        private string _customerName;
        private string _customerNumber;
        private string _itemDescription;
        private int _quantity;
        private decimal _pricePerUnit;

       
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
            }
        }

        public string CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value;
            }
        }

        public string CustomerNumber
        {
            get => _customerNumber;
            set
            {
                _customerNumber = value; 
            }
        }

        public string ItemDescription
        {
            get => _itemDescription;
            set
            {
                _itemDescription = value; 
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value; 
            }
        }

        public decimal PricePerUnit
        {
            get => _pricePerUnit;
            set
            {
                _pricePerUnit = value; 
            }
        }

        public decimal TotalPrice => Quantity * PricePerUnit * 1.20m;
    }
}
