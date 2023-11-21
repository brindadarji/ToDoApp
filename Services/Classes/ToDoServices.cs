using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Classes
{
    public class ToDoServices : IToDoServices
    {
        private readonly TodoDBContext _context;

        public ToDoServices(TodoDBContext context)
        {
            _context = context;
        }

        public async Task<ToDoViewModel> DisplayToDo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                return new ToDoViewModel { Id = todo.Id, Details = todo.Details };
            }

            return new ToDoViewModel();
        }

        public async Task<CreateTodoViewModel> AddToDo(CreateTodoViewModel createToDoViewModel)
        {
            var todo = new Todo { CreatedAt = DateTime.UtcNow, Details = createToDoViewModel.Details };
            _context.Add(todo);
            await _context.SaveChangesAsync();

            return createToDoViewModel;
        }

        public async Task<EditToDoViewModel> EditToDo(EditToDoViewModel editToDoViewModel)
        {
            var todo = await _context.Todos.FindAsync(editToDoViewModel.Id);
            if(todo == null)
            {
                return new EditToDoViewModel();
            }

            todo.Details = editToDoViewModel.Details;

            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return editToDoViewModel;
        }

        public async Task<EditToDoViewModel> DisplayEdit(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                return new EditToDoViewModel { Id = todo.Id, Details = todo.Details };
            }

            return new EditToDoViewModel();
        }

        public async Task<List<Todo>> GetToDos()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<DeleteToDoViewModel> DisplayDelete(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo != null)
            {
                return new DeleteToDoViewModel { Id = todo.Id, Details = todo.Details, CreatedAt = todo.CreatedAt };
            }

            return new DeleteToDoViewModel();
        }

       public async Task<DeleteToDoViewModel> DeleteToDo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return new DeleteToDoViewModel { Id = todo.Id, Details = todo.Details, CreatedAt = todo.CreatedAt };
        }

        public async Task CompleteToDo(int id)
        {
           
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return;
            }
            todo.Completed = true;
            _context.Update(todo);
            await _context.SaveChangesAsync();
        }
    }
}
