using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Organization_Assessment_Project_NEW.Data;
using Organization_Assessment_Project_NEW.Models;
using Organization_Assessment_Project_NEW.Services;

namespace Organization_Assessment_Project_NEW.Controllers
{
    public class TimesheetsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITimesheetRepository _timesheet;
        private readonly ApplicationDbContext _context;

        public TimesheetsController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IMapper mapper,
            ITimesheetRepository timesheet)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _timesheet = timesheet;
        }

        // GET: Timesheets
        public async Task<IActionResult> Index()
        {

            var Timesheet = _timesheet.FindAll();
            var timesheetModelModel = _mapper.Map<List<Timesheet>>(Timesheet);
            var Employee = _context.Employees.ToList();
            var employee = _userManager.GetUserAsync(User).Result;
            var emp = _context.Employees.Where(x => x.Email == employee.Email).FirstOrDefault();

            var model = new EmployeeTimesheetRequest
            {
                Timesheets = timesheetModelModel
            };
            return View(model);
        }

        // GET: Timesheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheet = await _context.timesheets
                .Include(t => t.employee)
                .FirstOrDefaultAsync(m => m.TimesheetId == id);
            if (timesheet == null)
            {
                return NotFound();
            }

            return View(timesheet);
        }

        // GET: Timesheets/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Timesheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimesheetId,TimesheetComment,DayOfWeek,hour,OvertimeHour,EmployeeId,TotalHours,DateRequested,Approved,Cancelled,ApprovedById")] Timesheet timesheet)
        {
            var DateRequested = DateTime.Now;
            var employee = _userManager.GetUserAsync(User).Result;
            var emp = _context.Employees.Where(x => x.Email == employee.Email).FirstOrDefault();

            var TotalHour = timesheet.hour + timesheet.OvertimeHour;

            //var employeeE = _context.Employees.FindAsync(emp.EmployeeId);


            var timeRequestModel = new Timesheet
            {
                TimesheetComment = timesheet.TimesheetComment,
                DayOfWeek = timesheet.DayOfWeek,
                hour = timesheet.hour,
                OvertimeHour = timesheet.OvertimeHour,
                employee = emp,
                EmployeeId = emp.EmployeeId,
                TotalHours = TotalHour,
                DateRequested = DateRequested,

                Approved = null

            };

            var timeshhet = _mapper.Map<Timesheet>(timeRequestModel);
            var isSuccess = _timesheet.Create(timeshhet);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong with submitting your record");
                return View(timeshhet);
            }

            return RedirectToAction("Index");




            if (ModelState.IsValid)
            {
                _context.Add(timesheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", timesheet.EmployeeId);
            return View(timesheet);
        }

        // GET: Timesheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheet = await _context.timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", timesheet.EmployeeId);
            return View(timesheet);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimesheetId,TimesheetComment,DayOfWeek,hour,OvertimeHour,EmployeeId,TotalHours,DateRequested,Approved,Cancelled,ApprovedById")] Timesheet timesheet)
        {
            if (id != timesheet.TimesheetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timesheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimesheetExists(timesheet.TimesheetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", timesheet.EmployeeId);
            return View(timesheet);
        }

        // GET: Timesheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timesheet = await _context.timesheets
                .Include(t => t.employee)
                .FirstOrDefaultAsync(m => m.TimesheetId == id);
            if (timesheet == null)
            {
                return NotFound();
            }

            return View(timesheet);
        }

        // POST: Timesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timesheet = await _context.timesheets.FindAsync(id);
            _context.timesheets.Remove(timesheet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimesheetExists(int id)
        {
            return _context.timesheets.Any(e => e.TimesheetId == id);
        }

        public ActionResult MyTimesheets()
        {
            try
            {
                var employee = _userManager.GetUserAsync(User).Result;
                var employeeId = employee.Id;
                var emailId = _userManager.GetEmailAsync(employee).Result;

                var emp = _context.Employees.Where(x => x.Email == emailId).FirstOrDefault();
                //var emp = _context.timesheets.Where(x => x.employee. == employeeId).FirstOrDefault();

                var time =  _context.timesheets.Where(x => x.employee.EmployeeId == emp.EmployeeId).FirstOrDefault();

                var Timesheets = _timesheet.FindById(time.TimesheetId);

                //var employeeRequestsModel = _mapper.Map<List<Timesheet>>(Timesheet);

                var DateRequested = DateTime.Now;
               // var employee = _userManager.GetUserAsync(User).Result;
                //var emp = _context.Employees.Where(x => x.Email == employee.Email).FirstOrDefault();

                var TotalHour = time.hour + time.OvertimeHour;

                var model = new Timesheet
                {

                    TimesheetComment = time.TimesheetComment,
                    DayOfWeek = time.DayOfWeek,
                    hour = time.hour,
                    OvertimeHour = time.OvertimeHour,
                    employee = emp,
                    EmployeeId = emp.EmployeeId,
                    TotalHours = TotalHour,
                    DateRequested = DateRequested,

                    Approved = null
                };
                 var timeshhet = _mapper.Map<List<Timesheet>>(model);

                return View(timeshhet);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }

           
        }
    }
}
