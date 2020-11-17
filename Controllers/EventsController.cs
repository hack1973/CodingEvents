﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        //GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.events = EventData.GetAll();

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
        public IActionResult NewEvent(Event newEvent)
        {
            EventData.Add(newEvent);

            return Redirect("/Events");
        }

        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }

            return Redirect("/Events");
        }

        
        [HttpGet]
        [Route("/Events/Edit/{eventId}")] // /Events/Edit/eventId?
        public IActionResult Edit(int eventId)
        {
            ViewBag.eventToEdit = EventData.GetById(eventId);
            // “Edit Event NAME (id=ID)” where "NAME" and "ID" are replaced by the values for the given event
            ViewBag.title = "Edit Event " + ViewBag.eventToEdit.Name + " (id = " + ViewBag.eventToEdit.Id + ")";

            return View();
        }

        [HttpPost]
        [Route("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            Event eventToEdit = EventData.GetById(eventId);
            eventToEdit.Name = name;
            eventToEdit.Description = description;
            
            return Redirect("/Events");
        }
    }
}
