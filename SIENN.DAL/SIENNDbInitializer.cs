using SIENN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIENN.DAL
{
    public static class SIENNDbInitializer
    {
        public static void Initialize(SIENNDbContext context)
        {
            context.Database.EnsureCreated();

            // Look is there any project.
            if (context.Products.Any())
            {
                return;
            }

            var types = new Models.Type[]
            {
                new Models.Type{Description="Uredjaj" },
                new Models.Type{Description="Lopta" },
                new Models.Type{Description="Casa" }
            };
            foreach (Models.Type t in types)
            {
                context.Types.Add(t);
            }
            context.SaveChanges();



            Unit unit1 = new Unit { Description = "Euro" };
            Unit unit2 = new Unit { Description = "Dolar" };
            Unit unit3 = new Unit { Description = "Funta" };
            var units = new Unit[]
            {
                unit1,unit2,unit3
            };
            foreach (Unit u in units)
            {
                context.Units.Add(u);
            }
            context.SaveChanges();

            Category category1 = new Category { Description = "Tehnologija"};
            Category category2 = new Category { Description = "Sportski rekvizit"};
            Category category3 = new Category { Description = "Escajg"};
            Category category4 = new Category { Description = "Grderoba" };
            var categories = new Category[]
            {
                category1, category2, category3
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            List<ProductCategory> pc1 = new List<ProductCategory>();
            pc1.Add(new ProductCategory { Category = category4 });
            Product product1 = new Product { Description = "Muzicka linija", Price = 200, IsAvailable = true, DelivaryDate = new DateTime(2018, 02, 16), Unit = unit1, UnitID = 1};
            Product product2 = new Product { Description = "Kosarkaska lopta", Price = 100, IsAvailable = true, DelivaryDate = new DateTime(2018, 04, 09), Unit = unit2, UnitID = 2 };
            Product product3 = new Product { Description = "Solja", Price = 5, IsAvailable = true, DelivaryDate = new DateTime(2018, 02, 16), Unit = unit3, UnitID = 3 };
            Product product4 = new Product { Description = "Duks", Price = 10, IsAvailable = true, DelivaryDate = new DateTime(2018, 03, 28), Unit = unit1, UnitID = 1, ProductCategories = pc1 };

            var products = new Product[]
            {
                
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();

            var productCategories = new ProductCategory[]
            {
                new ProductCategory{Product=product1,ProductId=product1.Id,Category=category1,CategoryId=category1.Id},
                new ProductCategory{Product=product2,ProductId=product2.Id,Category=category2,CategoryId=category2.Id},
                new ProductCategory{Product=product3,ProductId=product3.Id,Category=category3,CategoryId=category3.Id}

                
            };
            foreach (ProductCategory pc in productCategories)
            {
                context.ProductCategories.Add(pc);
            }
            context.SaveChanges();            
        }
    }
}
