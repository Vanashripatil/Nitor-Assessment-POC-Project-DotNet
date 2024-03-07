using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Models
{
    public class LeaveRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Employee Employee { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeId { get; set; }

        [Display(Name = "Start Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }

        [Display(Name = "Approval State")]
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        public Employee ApprovedBy { get; set; }

        [Display(Name = "Approver Name")]
        public string ApprovedById { get; set; }

        [Display(Name = "Employee Comments")]
        [MaxLength(300)]
        public string LeaveComments { get; set; }

        public static explicit operator LeaveRequest(Employee v)
        {
            throw new NotImplementedException();
        }
    }

    public class CreateLeaveRequest
    {
        [Key]
        public int Id { get; set; }

        public Employee Employee { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeId { get; set; }
        public LeaveType LeaveTypes { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        [Display(Name = "Employee Comments")]
        [MaxLength(300)]
        public string Levae_Request_Comments { get; set; }
        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]
        public DateTime EndDate { get; set; }


    }

    public class EmployeeLeaveRequest
    {
        public List<LeaveRequest> LeaveRequests { get; set; }
    }



}
