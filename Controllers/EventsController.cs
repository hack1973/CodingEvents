using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext context;

        public EventsController(EventDbContext dbContext)
        {
            context = dbContext;
        }

            //GET: /<controller>/
            [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = context.Events.ToList();

            return View(events);
        }

        //GET: /<controller>/add
        [HttpGet]
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();
            return View(addEventViewModel);
        }

        //GET: /<controller>/add
        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Type = addEventViewModel.Type
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        public IActionResult Delete()
        {
            List<Event> eventsToDelete = context.Events.ToList();

            return View(eventsToDelete);
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                Event theEvent = context.Events.Find(eventId);
                context.Events.Remove(theEvent);
            }
            context.SaveChanges();

            return Redirect("/Events");
        }

        
        [HttpGet]
        [Route("/Events/Edit/{eventId}")] // /Events/Edit/eventId?
        public IActionResult Edit(int eventId)
        {
            //EditEventViewModel editEventViewModel = new EditEventViewModel(eventId);
            //EditEventViewModel editEventViewModel = context.Events.Find(eventId);
            ViewBag.eventToEdit = context.Events.Find(eventId);
            // “Edit Event NAME (id=ID)” where "NAME" and "ID" are replaced by the values for the given event
            //ViewBag.title = "Edit Event " + ViewBag.eventToEdit.Name + " (id = " + ViewBag.eventToEdit.Id + ")";

            return View(ViewBag.eventToEdit);
        }

        [HttpPost]
        [Route("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description, string contactEmail)
        {
            Event eventToEdit = context.Events.Find(eventId);
            eventToEdit.Name = name;
            eventToEdit.Description = description;
            eventToEdit.ContactEmail = contactEmail;

            context.Events.Update(eventToEdit);
            context.SaveChanges();


            return Redirect("/Events");
        }
    }
}
