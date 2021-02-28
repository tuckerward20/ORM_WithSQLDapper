using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            ////Create a new Department

            //var repo = new DapperDepartmentRepository(conn);

            //Console.WriteLine("Type a new Department name");

            //var newDepartment = Console.ReadLine();

            //repo.InsertDepartment(newDepartment);

            //var departments = repo.GetAllDepartments();

            //foreach (var department in departments)
            //{
            //    Console.WriteLine($"{department.DepartmentID} {department.Name}");
            //}

            //Create a new product
            Console.WriteLine("Would you like to create a new product? (type 'nothing' if you have nothing to add)");
            var repo2 = new DapperProductRepository(conn);

            var products = repo2.GetAllProducts();

            var answer = Console.ReadLine();

            if (answer == "nothing")
            {
                Console.WriteLine("thanks!");
            }
            else
            {
                foreach (var item in products)
                {
                    Console.WriteLine($"{item.CategoryID} {item.Name} {item.Price} {item.ProductID} {item.OnSale} {item.StockLevel}");
                }

                Console.WriteLine("Type the new product name:");

                var newProductName = Console.ReadLine();

                Console.WriteLine("Type the new product price:");

                var newProductPrice = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Type the new product category ID:");

                var newProductCID = Convert.ToInt32(Console.ReadLine());

                repo2.CreateProduct(newProductName, newProductPrice, newProductCID);

                products = repo2.GetAllProducts();

                foreach (var item in products)
                {
                    Console.WriteLine($"{item.CategoryID} {item.Name} {item.Price} {item.ProductID} {item.OnSale} {item.StockLevel}");
                }
            }
            //Update an existing product:

            Console.WriteLine("Would you like to update an existing product? (type 'nothing' if you have nothing to update)");

            answer = Console.ReadLine();

            if (answer == "nothing")
            {
                Console.WriteLine("thanks!");
            }
            else
            {
                products = repo2.GetAllProducts();

                foreach (var item in products)
                {
                    Console.WriteLine($"{item.CategoryID} {item.Name} {item.Price} {item.ProductID} {item.OnSale} {item.StockLevel}");
                }

                Console.WriteLine("Type the name of the product you would like to update:");

                var existingProductName = Console.ReadLine();

                Console.WriteLine("Type the new product name:");

                var newProductName = Console.ReadLine();

                Console.WriteLine("Type the new product price:");

                var newProductPrice = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Type the new product category ID:");

                var newProductCID = Convert.ToInt32(Console.ReadLine());

                repo2.UpdateProduct(existingProductName, newProductName, newProductPrice, newProductCID);

                products = repo2.GetAllProducts();

                foreach (var item in products)
                {
                    Console.WriteLine($"{item.CategoryID} {item.Name} {item.Price} {item.ProductID} {item.OnSale} {item.StockLevel}");
                }
            }
            //Delete an existing product:

            Console.WriteLine("What would you like to delete? (type 'nothing' if you have nothing to delete)");

            answer = Console.ReadLine();

            if (answer == "nothing")
            {
                Console.WriteLine("thanks!");
            }
            else
            {
                repo2.DeleteProduct(answer);

                //showing the reulting database

                products = repo2.GetAllProducts();

                foreach (var item in products)
                {
                    Console.WriteLine($"{item.CategoryID} {item.Name} {item.Price} {item.ProductID} {item.OnSale} {item.StockLevel}");
                }
            }
        }
    }
}
