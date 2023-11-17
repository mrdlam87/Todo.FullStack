using System.ComponentModel.DataAnnotations;
using TodoApp.Domain.Entities.Base;

namespace TodoApp.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public virtual IEnumerable<Todo>? Todos { get; set; }
    }
}
