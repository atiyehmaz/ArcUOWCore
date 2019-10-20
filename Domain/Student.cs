using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [Required]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "NationalCode should be 10 digits")]
        public int NationalCode { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [StringLength(15, MinimumLength = 5, ErrorMessage = "Contact number should have 5 - 15 digits")]
        [Required]
        public string Mobile { get; set; }

    }
}
