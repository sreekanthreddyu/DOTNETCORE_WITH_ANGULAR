using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;
     

        public DutchSeeder(DutchContext ctx, IWebHostEnvironment hosting,UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();
            StoreUser user = await _userManager.FindByEmailAsync("sreekanth@gmail.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Sreekanth",
                    LastName = "Ugrareddy",
                    Email = "sreekanth@gmail.com",
                    UserName = "sreekanth@gmail.com",                   
                };
                String password = "Abcd#342abcd";

                 var result =   _userManager.CreateAsync(user,password).Result;
                
             //  await _ctx.SaveChangesAsync();



                if (result != IdentityResult.Success)             {
                    throw new InvalidOperationException("Could not create the new user!");
                }
            }
            

            if (!_ctx.products.Any())
            {
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.products.AddRange(products);
                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if(order !=null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product=products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price
                        }
                    };
                }
                



            }
            _ctx.SaveChanges();
        }
    }
}
