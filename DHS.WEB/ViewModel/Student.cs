
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DHS.WEB.ViewModel
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 4, ErrorMessage = "you must specify Name between 4 and 20 char")]
        [Required]
        public String Name { get; set; }
        [Required]
        public String Address { get; set; }
    }
}
