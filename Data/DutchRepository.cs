using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx,ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Order> GetAllOrders(bool includeitems)
        {
            try
            {
                if(includeitems)
                {
                    return _ctx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .ToList();
                }
                else
                {
                    return _ctx.Orders
                    .ToList();
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occured:{ex}");
                return null;
            }

        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Get all products was called");
                var result = _ctx.products
                          .OrderBy(p => p.Category)
                          .ToList();
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error has occured:{ex}");
                return null;
            }
           
        }

        public Order GetOrderById(string username,int id)
        {
            try
            {
                return _ctx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .Where(i=>i.Id==id&&i.User.UserName==username)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occured:{ex}");
                return null;
            }

        }
        public void AddEntity(Object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                var result = _ctx.products
                    .Where(p => p.Category == category)
                    .ToList();
                return result;
            }
           
             catch (Exception ex)
            {
                _logger.LogError($"An error has occured:{ex}");
                return null;
            }

        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeitems)
        {
            try
            {
                if (includeitems)
                {
                    return _ctx.Orders
                        .Where(o=>o.User.UserName==username)
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .ToList();
                }
                else
                {
                    return _ctx.Orders
                        .Where(o => o.User.UserName == username)
                    .ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occured:{ex}");
                return null;
            }
        }
    }
}
