using DutchTreat.Data.Entities;
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
    }
}
