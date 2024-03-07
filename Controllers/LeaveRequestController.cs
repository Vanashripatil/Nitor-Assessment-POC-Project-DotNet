using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Organization_Assessment_Project_NEW.Data;
using Organization_Assessment_Project_NEW.Models;
using Organization_Assessment_Project_NEW.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization_Assessment_Project_NEW.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequest;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveType;
        private readonly ApplicationDbContext _context;

        public LeaveRequestController(
            ILeaveRequestRepository leaveRequest,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            ILeaveTypeRepository leaveType,
            ApplicationDbContext context
        )
        {
            _leaveRequest = leaveRequest;
            _mapper = mapper;
            _userManager = userManager;
            _leaveType = leaveType;
            _context = context;
        }
        public ActionResult Index()
        {
            var leaveRequests = _leaveRequest.FindAll();
            var leaveRequestsModel = _mapper.Map<List<LeaveRequest>>(leaveRequests);
            var Employee = _context.Employees.ToList();
            var employee = _userManager.GetUserAsync(User).Result;
            var emp = _context.Employees.Where(x => x.Email == employee.Email).FirstOrDefault();

            var model = new EmployeeLeaveRequest
            {
                LeaveRequests = leaveRequestsModel
            };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var lRequest = _leaveRequest.FindById(id);
            var model = _mapper.Map<LeaveRequest>(lRequest);
            return View(model);
        }

        public ActionResult Create()
        {
            var leaveTypes = _leaveType.FindAll();
            ViewData["leavetype"] = _leaveType.FindAll().ToList();
            //var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
            //{
            //    Text = q.Name,
            //    Value = q.Id.ToString()
            //});
            //var model = new CreateLeaveRequest
            //{
            //    LeaveTypes = (LeaveType)leaveTypeItems
            //};
            // return View(leaveTypes);
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateLeaveRequest CLRequest)
        {
            var startDate = Convert.ToDateTime(CLRequest.StartDate);
            var endDate = Convert.ToDateTime(CLRequest.EndDate);
            var leaveTypes = _leaveType.FindAll();
            var employee = _userManager.GetUserAsync(User).Result;
            var emp = _context.Employees.Where(x => x.Email == employee.Email).FirstOrDefault();
            //var employeeE = _context.Employees.FindAsync(emp.EmployeeId);


            var leaveRequestModel = new LeaveRequest
            {
                Employee = emp,
                EmployeeId = Convert.ToString(employee.Id),
                StartDate = startDate,
                EndDate = endDate,
                Approved = null,
                DateRequested = DateTime.Now,
                LeaveTypeId = CLRequest.LeaveTypeId,
                LeaveComments = CLRequest.Levae_Request_Comments
            };

            var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
            var isSuccess = _leaveRequest.Create(leaveRequest);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong with submitting your record");
                return View(CLRequest);
            }

            return RedirectToAction("MyLeave");
        }

        public ActionResult MyLeave()
        {
            var employee = _userManager.GetUserAsync(User).Result;
            var employeeId = employee.Id;

            var employeeRequests = _leaveRequest.GetLRByEmployee(employeeId);

            var employeeRequestsModel = _mapper.Map<List<LeaveRequest>>(employeeRequests);


            var model = new EmployeeLeaveRequest
            {
                LeaveRequests = employeeRequestsModel
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ApproveR(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;

                var emp = _context.leaveRequests.Where(x => x.Employee.EmployeeId == id).FirstOrDefault();
                //string Id = Convert.ToInt32(emp.EmployeeId);
                var leaveRequest = _leaveRequest.FindById(emp.EmployeeId);

                var employeeid = leaveRequest.EmployeeId;
                var leaveTypeid = leaveRequest.LeaveTypeId;

                leaveRequest.Approved = true;
                leaveRequest.ApprovedById = user.Id;

                _leaveRequest.Update(leaveRequest);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));

        }

        public ActionResult RejectR(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var leaveRequest = _leaveRequest.FindById(id);
                
                leaveRequest.ApprovedById = user.Id;

                leaveRequest.Approved = false;
                _leaveRequest.Update(leaveRequest);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
