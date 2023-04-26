using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCFirstProject.Data;
using MVCFirstProject.Models;
using MVCFirstProject.Models.Domain;

namespace MVCFirstProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MyDbContext db;

        public EmployeesController(MyDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel empReq)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = empReq.Name,
                Email = empReq.Email,
                Phone = empReq.Phone,
                DateOfBirth = empReq.DateOfBirth,
                Department = empReq.Department

            };
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
          var employees =  await db.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewmodel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };
                return await Task.Run(()=>View("View",viewmodel));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await db.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name=model.Name;
                employee.Email=model.Email;
                employee.Phone=model.Phone;
                employee.DateOfBirth=model.DateOfBirth;
                employee.Department=model.Department;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await db.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                 await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
