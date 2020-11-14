using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        static private List<string> Events = new List<string>();

        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.events = Events;

            return View();
        }

        //GET: /<controller>/add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //GET: /<controller>/add
        [HttpPost]
        [Route("/Events/Add")]
        public IActionResult NewEvent(string name)
        {
            Events.Add(name);

            return Redirect("/Events");
        }
    }
}
