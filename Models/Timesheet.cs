using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Models
{
    public class Timesheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimesheetId { get; set; }
        [Display(Name = "Comment")]
        public string TimesheetComment { get; set; }

        [Display(Name = "How many days of week worked ?")]
        public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "How many hours worked ?")]
        public int hour { get; set; }

        [Display(Name = "How many hour overtime worked ?")]
        public int OvertimeHour { get; set; }

        public Employee employee { get; set; }
        public int EmployeeId { get; set; }

        public int TotalHours { get; set; }

        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }

        [Display(Name = "Approval State")]
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        public Employee ApprovedBy { get; set; }

        [Display(Name = "Approver Name")]
        public string ApprovedById { get; set; }

    }

    public class EmployeeTimesheetRequest
    {
        public List<Timesheet> Timesheets { get; set; }
    }
}
