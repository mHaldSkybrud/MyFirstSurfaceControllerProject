using MyFirstSurfaceControllerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Umbraco.Web.WebApi;

namespace MyFirstSurfaceControllerProject.Controllers
{
    public class ProductsApiController : UmbracoApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 32.50m },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 4.25m},
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 150.95m }
        };

        // GET: ProductsApi
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

    }
}