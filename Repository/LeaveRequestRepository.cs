using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Organization_Assessment_Project_NEW.Data;
using Organization_Assessment_Project_NEW.Models;
using Organization_Assessment_Project_NEW.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace Organization_Assessment_Project_NEW.Repository
{
    public class LeaveRequestRepository :  ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<IdentityUser> _userManager;

        public LeaveRequestRepository(ApplicationDbContext db,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public bool Create(LeaveRequest entity)
        {
            _db.leaveRequests.Add(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<LeaveRequest> FindAll()
        {
            var Leaves = _db.leaveRequests.Include(x => x.ApprovedBy).Include(x => x.LeaveType).ToList();
            return Leaves;
        }

        public LeaveRequest FindById(int Empid)
        {
            var Leaves = _db.leaveRequests.Include(x => x.ApprovedBy).Include(x => x.LeaveType).FirstOrDefault(x => x.Id == Empid);
            return Leaves;
        }

        public LeaveRequest FindById(string employeeId)
        {
            var IRs = _db.leaveRequests.Include(x => x.ApprovedBy).Include(x => x.LeaveType).FirstOrDefault(q => q.EmployeeId == employeeId);
          
            return IRs;
        }

        public ICollection<LeaveRequest> GetLRByEmployee(string employeeid)
        {
            var lRs = FindAll().Where(q => q.EmployeeId == employeeid).ToList();
            return lRs;
        }

        public bool isExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveRequest entity)
        {
            _db.leaveRequests.Update(entity);
            return Save();
        }
    }
}
