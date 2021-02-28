using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;");
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (name, price, categoryID) VALUES (@productName, @productPrice, @productCategoryID);",
            new { productName = name, productPrice = price, productCategoryID = categoryID }); //new { productPrice = price }, new { productCategoryID = categoryID });
        }
        public void UpdateProduct(string original, string name, double price, int categoryID)
        {
            var listID = _connection.Query<Product>("SELECT ProductID FROM products WHERE name = '" + original + "';").ToList();

            var objectID = listID[0];

            if (listID.Count > 1)
            {
                Console.WriteLine("There were multiple entries with that name, which would you like to delete? (input one of these numbers)");
                foreach (var item in listID)
                {
                    Console.WriteLine(item.ProductID);

                }
                var ID = Console.ReadLine();
                listID = _connection.Query<Product>("SELECT ProductID FROM products WHERE ProductID = '" + ID + "';").ToList();
                objectID = listID[0];
            }

            _connection.Execute("UPDATE products SET name = '"+ name +"', price = @productPrice, categoryID = @productCategoryID WHERE productID = @originalProductID;",
            new { originalProductID = objectID.ProductID, productName = name, productPrice = price, productCategoryID = categoryID }); //new { productPrice = price }, new { productCategoryID = categoryID });
        }

        public void DeleteProduct(string name)
        {
            //Console.WriteLine("what is the name of the product you would like to delete?");
            //string  productName = Console.ReadLine();
            var listID = _connection.Query<Product>("SELECT ProductID FROM products WHERE name = '" + name + "';").ToList();

            var objectID = listID[0];

            if (listID.Count > 1)
            {
                Console.WriteLine("There were multiple entries with that name, which would you like to delete? (input one of these numbers)");
                foreach (var item in listID)
                {
                    Console.WriteLine(item.ProductID);
                    
                }
                var ID = Console.ReadLine();
                listID = _connection.Query<Product>("SELECT ProductID FROM products WHERE ProductID = '" + ID + "';").ToList();
                objectID = listID[0];
            }
            _connection.Execute("DELETE FROM products WHERE productID = @productID;",
            new { productID = objectID.ProductID }); //new { productPrice = price }, new { productCategoryID = categoryID });
        }
    }
}
