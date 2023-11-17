using AutoMapper;
using TodoApp.Application.Common.DTO;
using TodoApp.Application.Common.Exceptions;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Application.Services.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services.Implementation
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public TodoService(IUnitOfWork unitOfWork, IMapper mapper, IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<TodoDto>> GetAllTodos()
        {
            IEnumerable<Todo> todos = await _unitOfWork.Todo.GetAll();

            return _mapper.Map<IEnumerable<TodoDto>>(todos);
        }

        public async Task<TodoDto> GetTodo(int id)
        {
            Todo todo = await _unitOfWork.Todo.GetById(id);

            if (todo is null)
            {
                throw ServiceException.NotFound(nameof(todo));
            }

            return _mapper.Map<TodoDto>(todo);
        }

        public async Task<TodoDto> CreateTodo(TodoDto todoDto)
        {
            Todo todoData = _mapper.Map<Todo>(todoDto);

            _validationService.ValidateAndThrow(todoData);

            Todo newTodo = await _unitOfWork.Todo.Add(todoData);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<TodoDto>(newTodo);
        }

        public async Task<TodoDto> UpdateTodo(int id, TodoDto todoDto)
        {
            Todo todoData = _mapper.Map<Todo>(todoDto);

            _validationService.ValidateAndThrow(todoData);

            bool todoExist = await _unitOfWork.Todo.Any(t => t.Id == id);

            if (!todoExist)
            {
                throw ServiceException.NotFound("todo");
            }

            todoData.Id = id;

            Todo updatedTodo = _unitOfWork.Todo.Update(todoData);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<TodoDto>(updatedTodo);
        }

        public Task DeleteTodo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
