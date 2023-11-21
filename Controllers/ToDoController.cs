using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoServices _todoService;

        public ToDoController(IToDoServices toDoServices)
        {
            _todoService = toDoServices;
        }

        public async Task<IActionResult> Index()
        {
            var _todo = await _todoService.GetToDos();

            return _todo != null ?
                        View(_todo) :
                        Problem("No Todos Found.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todo = await _todoService.DisplayToDo(id.Value);
            return View(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Details")] CreateTodoViewModel createTodoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddToDo(createTodoViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(createTodoViewModel);
        }
        public async Task<IActionResult> Complete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _todoService.CompleteToDo(id.Value);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                NotFound();
            }
            var todo = await _todoService.DisplayEdit(id.Value);
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Details")] EditToDoViewModel editToDoViewModel)
        {

            if (ModelState.IsValid)
            {
                await _todoService.EditToDo(editToDoViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(editToDoViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                NotFound();
            }
            var todo = await _todoService.DisplayDelete(id.Value);
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _todoService.DeleteToDo(id);
            return RedirectToAction(nameof(Index));
        }

    }
}


