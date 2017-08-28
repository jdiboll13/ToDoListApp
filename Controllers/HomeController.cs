using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly todolistContext _context;

        public HomeController(todolistContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ToDo.ToList());
        }
        [HttpPost]
        public IActionResult Index(string ItemName)
        {
            var CurrentToDo = new ToDoListModel
            {
                TaskName = ItemName
            };

            _context.Add(CurrentToDo);
            _context.SaveChanges();

            return View(_context.ToDo.ToList());
        }
        [HttpPost]
        public IActionResult Complete(int id) 
        {
            var done = _context.ToDo.SingleOrDefault(i => i.ID == id);
            done.IsComplete();
            _context.SaveChanges();

            return Redirect("Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
