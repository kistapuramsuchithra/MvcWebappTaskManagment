using Microsoft.AspNetCore.Mvc;
using MVCTaskManagmentApp.Data;
using MVCTaskManagmentApp.Models;
using System;

namespace MVCTaskManagmentApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Displays all tasks
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        // GET: Displays the "Create Task" form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Saves the new task to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }
        // GET: Tasks/Edit/5
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        // GET: Tasks/Details/5
        public IActionResult Details(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        // GET: Tasks/Delete/5
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
