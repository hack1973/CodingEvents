using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers
{
    public class EventCategoryController : Controller
    {
        private EventDbContext context;

        public EventCategoryController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<EventCategory> categories = context.Categories.ToList();

            return View(categories);
        }
    }
}
