
using Organization_Assessment_Project_NEW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Services
{
    public interface ILeaveTypeRepository : IServiceBase<LeaveType>
    {
        ICollection<LeaveType> GetEmployeeByLeaveType(int id);
    }
}
