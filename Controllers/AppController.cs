using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;
       
        public AppController(IMailService mailService,IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }
        public IActionResult Index()
        {
           
            return View();
        }
       [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            
            return View();
        }

        [HttpPost("contact")]
        public IActionResult contact(ContactViewModel model)
        {
            ViewBag.Title = "Contact Us";
            if(ModelState.IsValid)
            {
                _mailService.SendMessage("sreekanthreddy2304@gmail.com", model.Subject, $"From:{model.Name} - {model.Email},Message:{model.Message}");
                ViewBag.UserMessage = "Sent!";
                ModelState.Clear();
            }
            
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        public IActionResult Shop()
        {
            var result = _repository.GetAllProducts();
            return View(result);
        }
    }
}