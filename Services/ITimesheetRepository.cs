using Organization_Assessment_Project_NEW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Services
{
    public interface ITimesheetRepository : IServiceBase<Timesheet>
    {
        ICollection<LeaveRequest> GetTimesheetByEmployee(string employeeid);
    }
}
