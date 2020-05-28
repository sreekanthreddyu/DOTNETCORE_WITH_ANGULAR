using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository repository,ILogger<OrdersController> logger,IMapper mapper,UserManager<StoreUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get(bool includeitems=true)
        {
            try
            {
                var username = User.Identity.Name;
                var results = _repository.GetAllOrdersByUser(username,includeitems);
                return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModel>>(results));
            }

            catch (Exception ex)
            {
                _logger.LogError($"An error has occured:{ex}");
                return BadRequest("Failed to load orders");
            }
        }
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<Order>> Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name,id);
                if (order!=null)
                {
                    return Ok(_mapper.Map<Order,OrderViewModel>(order));
                }
                else
                {
                    return NotFound("Order not Found!");
                }
                
            }

            catch (Exception ex)
            {
                _logger.LogError($"An error has occured:{ex}");
                return BadRequest("Failed to load orders");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    if(newOrder.OrderDate==DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;
                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        
                        return Created($"/api/Orders/{newOrder.Id}", _mapper.Map<Order,OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
               
            }
            catch(Exception ex)
            { 
                _logger.LogError($"An error has occured:{ex}");
                
            }
            return BadRequest("Failed to Save order");

        }
    }
}