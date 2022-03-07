namespace Rendelesek.Models
{
    public class OrderedProduct
    {
        private int orderNumber;
        private string productCode;
        private int orderedQuantity;

        public OrderedProduct(string[] args)
        {
            orderNumber = int.Parse(args[1]);
            productCode = args[2];
            orderedQuantity = int.Parse(args[3]);
        }

        public int OrderedQuantity 
        { 
            get => orderedQuantity; 
            set => orderedQuantity = value; 
        }

        public string ProductCode 
        { 
            get => productCode; 
            set => productCode = value; 
        }
    }
}
