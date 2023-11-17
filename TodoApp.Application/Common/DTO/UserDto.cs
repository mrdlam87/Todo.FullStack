namespace TodoApp.Application.Common.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual IEnumerable<TodoDto>? Todos { get; set; }
    }
}
