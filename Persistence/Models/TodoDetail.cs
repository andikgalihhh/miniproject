using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models
{
    [Table("TodoDetail")]
    public class TodoDetail
    {
        [Key]
        [Required]
        public Guid TodoDetailId { get; set; }

        [Required]
        [StringLength(100)]
        public string Activity { get; set; }

        [Required]
        [StringLength(50)]
        public CategoryEnum Category { get; set; }

        [StringLength(500)]
        public string DetailNote { get; set; }

        // Foreign key
        [Required]
        public Guid TodoId { get; set; }

        // Navigation property
        [ForeignKey("TodoId")]
        public Todo Todo { get; set; }
    }

    public enum CategoryEnum
    {
        Task,
        DailyActivity
    }
}
