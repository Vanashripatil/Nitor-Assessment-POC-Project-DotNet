using Organization_Assessment_Project_NEW.Data;
using Organization_Assessment_Project_NEW.Models;
using Organization_Assessment_Project_NEW.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Repository
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly ApplicationDbContext _db;

        public TimesheetRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Timesheet entity)
        {
            _db.timesheets.Add(entity);
            return Save();
        }

        public bool Delete(Timesheet entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<Timesheet> FindAll()
        {
            var Leaves = _db.timesheets.Include(x => x.ApprovedBy).Include(x => x.TimesheetId).ToList();
            return Leaves;
        }

        public Timesheet FindById(int id)
        {
            var timesheet = _db.timesheets.Find(id);
            return timesheet;
        }

        public ICollection<LeaveRequest> GetTimesheetByEmployee(string employeeid)
        {
            var emp = _db.Employees.FindAsync(employeeid);
            //var lRs = FindAll().Where(q => q.EmployeeId == employeeid).ToList();
            //return lRs;
            throw new NotImplementedException();
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

        public bool Update(Timesheet entity)
        {
            _db.timesheets.Update(entity);
            return Save();
        }
    }
}
