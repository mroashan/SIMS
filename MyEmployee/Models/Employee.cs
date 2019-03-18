using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyEmployee.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeAlert = new HashSet<EmployeeAlert>();
        }

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("First Name")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }

        [DisplayName("Full Name")]
        public string Fullname { get { return string.Format("{0}{1}{2}", Lastname, Lastname.Length>0?", ": "", Firstname); } }

        public ICollection<EmployeeAlert> EmployeeAlert { get; set; }
    }
}
