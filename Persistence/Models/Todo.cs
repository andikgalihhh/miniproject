using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models
{
    [Table("Todo")]
    public class Todo
    {
        [Key]
        [Required]
        public Guid TodoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Day { get; set; }

        [Required]
        public DateTime TodayDate { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public int DetailCount { get; set; }

        // Navigation property
        public ICollection<TodoDetail> TodoDetails { get; set; }
    }
}
