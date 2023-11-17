namespace TodoApp.Application.Common.DTO
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Complete { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
    }
}
