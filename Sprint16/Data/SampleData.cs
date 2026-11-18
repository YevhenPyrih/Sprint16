using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sprint16.Models;
using System;
using System.Linq;

namespace Sprint16.Data
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ShoppingContext(
                serviceProvider.GetRequiredService<DbContextOptions<ShoppingContext>>());

            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            // 1. Define independent entities
            var customers = new[]
            {
                new Customer { First_name = "John", Last_name = "Doe", Address = "456 Oak Ave", Discount = 0 },
                new Customer { First_name = "Jane", Last_name = "Smith", Address = "123 Main St", Discount = 10 },
                new Customer { First_name = "Jared", Last_name = "Stone", Address = "2109 St Veronica", Discount = 5 }
            };

            var supermarkets = new[]
            {
                new Supermarket { Name = "SuperMart Central", Address = "100 Downtown Blvd" },
                new Supermarket { Name = "FreshFoods Local", Address = "200 Suburb Ln" }
            };

            var products = new[]
            {
                new Product { Name = "Whole Milk (1L)", Price = 1.50f },
                new Product { Name = "Sourdough Bread", Price = 3.20f },
                new Product { Name = "Apples (1kg)", Price = 2.80f },
                new Product { Name = "Coffee Beans (500g)", Price = 12.50f }
            };

            // Add them to the context
            context.Customers.AddRange(customers);
            context.Supermarkets.AddRange(supermarkets);
            context.Products.AddRange(products);

            // 2. Define Orders by assigning the object references instead of integer IDs
            var orders = new[]
            {
                new Order
                {
                    Customer = customers[0],
                    Supermarket = supermarkets[0],
                    Order_date = DateTime.Now.AddDays(-2)
                },
                new Order
                {
                    Customer = customers[1],
                    Supermarket = supermarkets[1],
                    Order_date = DateTime.Now.AddHours(-5)
                }
            };
            context.Orders.AddRange(orders);

            // 3. Define OrderDetails by assigning the Order and Product object references
            var orderDetails = new[]
            {
                // Items for John's order
                new OrderDetail { Order = orders[0], Product = products[0], Quantity = 2f },
                new OrderDetail { Order = orders[0], Product = products[1], Quantity = 1f },
                
                // Items for Jane's order
                new OrderDetail { Order = orders[1], Product = products[2], Quantity = 1.5f },
                new OrderDetail { Order = orders[1], Product = products[3], Quantity = 1f }
            };
            context.OrderDetails.AddRange(orderDetails);

            // 4. Save everything at once. EF Core will automatically generate all IDs and map all shadow foreign keys.
            context.SaveChanges();
        }
    }
}