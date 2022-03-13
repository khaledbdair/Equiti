using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Props

            List<Customer> lstCustomers = new List<Customer>
            {
                new Customer { Id = 1 , Name = "customer1" , Email = "customer1@customer1.com" },
                new Customer { Id = 2 , Name = "customer2" , Email = "customer2@customer2.com" },
                new Customer { Id = 3 , Name = "customer3" , Email = "customer3@customer3.com" },
            };

            List<DemosticOrder> lstDemosticOrders = new List<DemosticOrder>
            {
                new DemosticOrder { OrderDateTime = DateTime.Now, CustomerId = 1},
                new DemosticOrder { OrderDateTime = DateTime.Now, CustomerId = 2},
                new DemosticOrder { OrderDateTime = DateTime.Now, CustomerId = 1},
            };

            List<OverSeaOrder> lstOverSeaOrders = new List<OverSeaOrder>
            {
                new OverSeaOrder { OrderDateTime = DateTime.Now, CustomerId = 2},
                new OverSeaOrder { OrderDateTime = DateTime.Now, CustomerId = 3},
                new OverSeaOrder { OrderDateTime = DateTime.Now, CustomerId = 3},
            };

            #endregion

            var lst1 = GetCustomers(lstDemosticOrders);
            var lst2 = GetCustomers(lstOverSeaOrders);

            Console.WriteLine(string.Join(", ", lst1.Select(x => x.Name)));
            Console.WriteLine(string.Join(", ", lst2.Select(x => x.Name)));

            #region Methods
            IEnumerable<Customer> GetCustomers<T>(IEnumerable<T> Orders) where T : IOrder
            {
                var customers = (from c in lstCustomers
                                 join o in Orders on c.Id equals o.CustomerId
                                 where o.OrderDateTime >= DateTime.Now.AddYears(-1) &&
                                       o.OrderDateTime.Date <= DateTime.Now.Date
                                 select c).Distinct();

                return customers;
            }
            #endregion

        }
    }

    public interface IOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        DateTime OrderDateTime { get; set; }
        Customer Customer { get; set; }
        IList<OrderItem> OrderItems { get; set; }
    }

    public class DemosticOrder : IOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public Customer Customer { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }

    public class OverSeaOrder : IOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public Customer Customer { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Email { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
