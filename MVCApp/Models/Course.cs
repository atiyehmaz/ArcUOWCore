using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MVCApp
{
    public class Course
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "عنوان درس")]
        public string Name { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
