using AutoMapper;
using TodoApp.Application.Common.DTO;
using TodoApp.Application.Common.Exceptions;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Application.Services.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            IEnumerable<User> users = await _unitOfWork.User.GetAll();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUser(int id)
        {
            User user = await _unitOfWork.User.GetById(id);

            if (user is null)
            {
                throw ServiceException.NotFound(nameof(user));
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUser(UserDto userDTO)
        {
            User userData = _mapper.Map<User>(userDTO);

            _validationService.ValidateAndThrow(userData);

            User newUser = await _unitOfWork.User.Add(userData);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserDto>(newUser);
        }

        public async Task<UserDto> UpdateUser(int id, UserDto userDTO)
        {
            User userData = _mapper.Map<User>(userDTO);

            _validationService.ValidateAndThrow(userData);

            bool userExist = await _unitOfWork.User.Any(u => u.Id == id);

            if (!userExist)
            {
                throw ServiceException.NotFound("user");
            }

            userData.Id = id;

            User updatedUser = _unitOfWork.User.Update(userData);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task DeleteUser(int id)
        {
            User user = await _unitOfWork.User.GetById(id);

            if (user is null)
            {
                throw ServiceException.NotFound(nameof(user));
            }

            _unitOfWork.User.Remove(user);

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TodoDto>> GetTodosByUser(int userId)
        {
            IEnumerable<Todo> todos = await _unitOfWork.Todo.GetAll(t => t.UserId == userId);

            return _mapper.Map<IEnumerable<TodoDto>>(todos);
        }

        public async Task<TodoDto> GetSingleTodoByUser(int userId, int todoId)
        {
            Todo todo = await _unitOfWork.Todo.Get(t => t.Id == todoId && t.UserId == userId);

            if (todo is null)
            {
                throw ServiceException.NotFound(nameof(todo));
            }

            return _mapper.Map<TodoDto>(todo);
        }

        public async Task<TodoDto> CreateUserTodo(int userId, TodoDto todoDto)
        {
            Todo todoData = _mapper.Map<Todo>(todoDto);

            _validationService.ValidateAndThrow(todoData);

            todoData.UserId = userId;

            Todo newTodo = await _unitOfWork.Todo.Add(todoData);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<TodoDto>(newTodo);
        }


        public async Task<TodoDto> UpdateUserTodo(int userId, int todoId, TodoDto todoDto)
        {
            Todo todoData = _mapper.Map<Todo>(todoDto);

            _validationService.ValidateAndThrow(todoData);

            bool todoExist = await _unitOfWork.Todo.Any(t => t.Id == todoId && t.UserId == userId);

            if (!todoExist)
            {
                throw ServiceException.NotFound("todo");
            }

            todoData.Id = todoId;

            todoData.UserId = userId;

            Todo updatedTodo = _unitOfWork.Todo.Update(todoData);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<TodoDto>(updatedTodo);
        }

        public async Task DeleteUserTodo(int userId, int todoId)
        {
            Todo todo = await _unitOfWork.Todo.Get(t => t.Id == todoId && t.UserId == userId);

            if (todo is null)
            {
                throw ServiceException.NotFound(nameof(todo));
            }

            _unitOfWork.Todo.Remove(todo);

            await _unitOfWork.SaveAsync();
        }
    }
}
