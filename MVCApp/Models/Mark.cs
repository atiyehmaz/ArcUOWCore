using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp
{
    public class Mark
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public float Grade { get; set; }

        public MarkType MarkType { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual  Student Student{ get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

    }

    public enum MarkType
    {
        Exam = 0,
        Test = 1,
        Quiz = 2,
    }
}
