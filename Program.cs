using Rendelesek.Models;
using System.Diagnostics;
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
        }

        private static void ProcessOrders()
        {
            foreach (Order order in orders)
            {
                if (order.OrderedProducts.All(op => products.Find(p => p.ProductCode == op.ProductCode)?.Quantity >= op.OrderedQuantity))
                {
                    foreach (OrderedProduct op in order.OrderedProducts)
                    {
                        Product product = products.Find(p => p.ProductCode == op.ProductCode);
                        product.Quantity -= op.OrderedQuantity;
                    }
                }
                else
                {
                    order.IsWaiting = true;
                }
            }
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