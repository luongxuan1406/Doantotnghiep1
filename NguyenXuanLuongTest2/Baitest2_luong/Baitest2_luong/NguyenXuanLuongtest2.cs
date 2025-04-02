using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace OrderMerger
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Address { get; set; }
        public List<string> Products { get; set; }

        public Order(string orderId, string address, List<string> products)
        {
            OrderId = orderId;
            Address = address;
            Products = products;
        }

        public override string ToString()
        {
            return $"Order(IDs: {OrderId}, Address: {Address}, Products: [{string.Join(", ", Products)}])";
        }
    }

    class Program
    {
        static List<Order> MergeOrdersDict(List<Order> orders)
        {
            var grouped = new Dictionary<string, (List<string> ids, List<string> prods)>();

            foreach (var order in orders)
            {
                if (grouped.TryGetValue(order.Address, out var value))
                {
                    value.ids.Add(order.OrderId);
                    value.prods.AddRange(order.Products);
                }
                else
                {
                    grouped[order.Address] = (new List<string> { order.OrderId },
                                           new List<string>(order.Products));
                }
            }

            var result = new List<Order>();
            foreach (var pair in grouped)
            {
                result.Add(new Order(
                    string.Join(",", pair.Value.ids),
                    pair.Key,
                    pair.Value.prods
                ));
            }
            return result;
        }

        static List<Order> MergeOrdersLinq(List<Order> orders)
        {
            return orders
                .GroupBy(o => o.Address)
                .Select(g => new Order(
                    string.Join(",", g.Select(o => o.OrderId)),
                    g.Key,
                    g.SelectMany(o => o.Products).ToList()
                ))
                .ToList();
        }

        static void RunTests()
        {
            var test1 = new List<Order>
            {
                new Order("001", "123 Hanoi", new List<string> { "Book", "Pen" }),
                new Order("002", "123 Hanoi", new List<string> { "Notebook" }),
                new Order("003", "123 Hanoi", new List<string> { "Pencil" })
            };

            var test2 = new List<Order>
            {
                new Order("001", "123 Hanoi", new List<string> { "Book" }),
                new Order("002", "456 HCMC", new List<string> { "Pen" }),
                new Order("003", "789 Danang", new List<string> { "Pencil" })
            };

            var test3 = new List<Order>
            {
                new Order("001", "123 Hanoi", new List<string> { "Book" }),
                new Order("002", "456 HCMC", new List<string> { "Pen" }),
                new Order("003", "123 Hanoi", new List<string> { "Pencil" }),
                new Order("004", "789 Danang", new List<string> { "Notebook" })
            };

            var tests = new[] { ("Same", test1), ("Diff", test2), ("Mix", test3) };

            Console.WriteLine("Dictionary Way:");
            foreach (var (name, test) in tests)
            {
                var sw = Stopwatch.StartNew();
                var result = MergeOrdersDict(test);
                Console.WriteLine($"\n{name}:");
                result.ForEach(r => Console.WriteLine(r));
                Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");
            }

            Console.WriteLine("\nLINQ Way:");
            foreach (var (name, test) in tests)
            {
                var sw = Stopwatch.StartNew();
                var result = MergeOrdersLinq(test);
                Console.WriteLine($"\n{name}:");
                result.ForEach(r => Console.WriteLine(r));
                Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");
            }
        }

        static void Main(string[] args)
        {
            RunTests();
        }
    }
}