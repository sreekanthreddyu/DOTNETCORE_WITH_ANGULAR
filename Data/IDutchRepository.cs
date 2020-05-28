using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders(bool includeitems);
        Order GetOrderById(string username,int id);
        void AddEntity(Object model);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeitems);
    }
}