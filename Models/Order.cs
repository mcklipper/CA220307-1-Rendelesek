namespace Rendelesek.Models
{
    public class Order
    {
        private DateTime date;
        private int orderNumber;
        private string email;
        private bool isWaiting;

        private List<OrderedProduct> orderedProducts;

        public Order(string[] args) 
        {
            date = DateTime.Parse(args[1]);
            orderNumber = int.Parse(args[2]);
            email = args[3];
            isWaiting = false;
            orderedProducts = new();
        }

        public List<OrderedProduct> OrderedProducts
        {
            get
            {
                return orderedProducts;
            }
        }

        public bool IsWaiting 
        { 
            get => isWaiting; 
            set => isWaiting = value; 
        }
    }
}
