using Rendelesek.Models;
using System.Text;

namespace Rendelesek
{
    public class Program
    {
        private static List<Product> products = new();
        private static List<Order> orders = new();

        public static void Main(string[] args)
        {
            ReadFile("raktar.csv");
            ReadFile("rendeles.csv", false);

            ProcessOrders();
            WriteMails();
        }

        private static void WriteMails()
        {
            StreamWriter sw = new StreamWriter(
                new FileStream("levelek.csv", FileMode.OpenOrCreate),
                Encoding.UTF8
            );

            foreach (Order order in orders)
            {
                int sum = 0;
                foreach (OrderedProduct orderedProduct in order.OrderedProducts) 
                {
                    Product? product = FindProductBy(orderedProduct.ProductCode);

                    if (product != null)
                        sum += product.Price;
                }


                if (!order.IsWaiting)
                    sw.WriteLine($"{ order.Email };A rendelését két napon belül szállítjuk. A rendelés értéke: { sum } Ft");
                else
                    sw.WriteLine($"{ order.Email };A rendelése függő állapotba került. Hamarosan értesítjük a szállítás időpontjáról.");
            }

            sw.Close();
        }

        private static void ProcessOrders()
        {
            foreach (Order order in orders)
            {
                if (IsAllProductsAvailable(order))
                {
                    foreach (OrderedProduct op in order.OrderedProducts)
                    {
                        Product? product = FindProductBy(op.ProductCode);

                        if (product != null)
                            product.Quantity -= op.OrderedQuantity;
                    }
                }
                else
                {
                    order.IsWaiting = true;
                }
            }
        }

        private static Product? FindProductBy(string code)
        {
            int i = 0;
            while (i < products.Count && products[i].ProductCode != code)
                i++;

            if (i == products.Count)
                return null;

            return products[i];
        }

        private static bool IsAllProductsAvailable(Order order)
        {
            foreach (OrderedProduct orderedProduct in order.OrderedProducts)
            {
                Product? product = FindProductBy(orderedProduct.ProductCode);

                if (product == null || orderedProduct.OrderedQuantity > product.Quantity)
                    return false;
            }

            return true;
        }

        private static void ReadFile(string fileName, bool readingProducts = true)
        {
            StreamReader reader = new StreamReader(
                new FileStream(fileName, FileMode.Open),
                Encoding.UTF8
            );

            if (readingProducts) 
                ReadProducts(reader);
            else 
                ReadOrders(reader);

            reader.Close();
        }

        private static void ReadOrders(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] args = line.Trim().Split(';');

                if (line[0] == 'M')
                {
                    orders.Add(new Order(args));
                }
                else
                {
                    Order lastOrder = orders.Last(); // orders[ orders.Count - 1 ]
                    lastOrder.OrderedProducts.Add(new OrderedProduct(args));
                }
            }
        }

        private static void ReadProducts(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                products.Add(new Product(line.Trim().Split(';')));
            }
        }
    }
}