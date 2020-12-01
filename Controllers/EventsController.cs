using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            List<Event> events = context.Events
                .Include(e => e.Category)
                .ToList();

            return View(events);
        }

        //GET: /<controller>/add
        [HttpGet]
        public IActionResult Add()
        {
            List<EventCategory> categories = context.Categories.ToList();
            AddEventViewModel addEventViewModel = new AddEventViewModel(context.Categories.ToList());

            return View(addEventViewModel);
        }

        //GET: /<controller>/add
        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory theCategory = context.Categories.Find(addEventViewModel.CategoryId);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Category = theCategory
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        [HttpGet]
        public IActionResult Delete()
        {            
            List<Event> eventsToDelete = context.Events
                .Include(e => e.Category)
                .ToList();

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

        public IActionResult Detail(int id)
        {
            Event theEvent = context.Events
               .Include(e => e.Category)
               .Single(e => e.Id == id);

            List<EventTag> eventTags = context.EventTags
              .Where(et => et.EventId == id)
              .Include(et => et.Tag)
              .ToList();

            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent, eventTags);
            return View(viewModel);
        }
    }
}