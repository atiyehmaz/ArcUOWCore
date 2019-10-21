using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MVCApp
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name ="نام")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "کد ملی")]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "NationalCode should be 10 digits")]
        public int NationalCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [StringLength(15, MinimumLength = 5, ErrorMessage = "Contact number should have 5 - 15 digits")]
        [Required]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

    }
}
