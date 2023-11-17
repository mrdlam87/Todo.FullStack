using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApp.Domain.Entities.Base;

namespace TodoApp.Domain.Entities
{
    public class Todo : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Complete { get; set; }
        public DateTime? DateCompleted { get; set; }

        [ForeignKey(nameof(Todo))]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
