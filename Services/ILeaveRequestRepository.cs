using Organization_Assessment_Project_NEW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Services
{
    public interface ILeaveRequestRepository : IServiceBase<LeaveRequest>
    {
        ICollection<LeaveRequest> GetLRByEmployee(string employeeid);
        LeaveRequest FindById(string employeeId);
    }
}
