using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyEmployee.Models
{
    public partial class EmployeeAlert
    {
        public int AlertId { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Alert { get; set; }

        public Employee Employee { get; set; }
    }
}
