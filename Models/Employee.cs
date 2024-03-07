using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfJoined { get; set; }
        public string Email { get; set; }
        public string ManagerName { get; set; }
        public string EmployeeActive { get; set; }
        public string TimeType { get; set; }

        public string UserRole { get; set; }
        
    }
}
