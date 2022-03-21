namespace Rendelesek.Models
{
    public class Product
    {
        private string productCode;
        private string productName;
        private int price;
        private int quantity;

        public Product(string[] args)
        {
            productCode = args[0];
            productName = args[1];
            price = int.Parse(args[2]);
            quantity = int.Parse(args[3]);
        }

        public string ProductCode 
        { 
            get => productCode; 
            set => productCode = value; 
        }

        public int Quantity 
        { 
            get => quantity; 
            set => quantity = value; 
        }

        public int Price 
        {
            get => price; 
            set => price = value; 
        }
    }
}
