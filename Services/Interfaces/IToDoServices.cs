using ToDoApp.Models;

namespace ToDoApp.Services.Interfaces
{
    public interface IToDoServices
    {
        Task<List<Todo>> GetToDos();

        Task<ToDoViewModel> DisplayToDo(int id);

        Task<CreateTodoViewModel> AddToDo(CreateTodoViewModel createToDoViewModel);

        Task<EditToDoViewModel> DisplayEdit(int id);

        Task<EditToDoViewModel> EditToDo(EditToDoViewModel editToDoViewModel);

        Task<DeleteToDoViewModel> DisplayDelete(int id);

        Task<DeleteToDoViewModel> DeleteToDo(int id);

        Task CompleteToDo(int id);

    }
}
