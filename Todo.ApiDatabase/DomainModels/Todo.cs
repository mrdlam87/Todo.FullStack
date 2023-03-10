using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.ApiDatabase.DomainModels
{
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Complete { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
