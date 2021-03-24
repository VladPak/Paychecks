using Paychecks.Entity;
using Paychecks.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paychecks.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        //Create ref to DB
        private readonly ApplicationDbContext _context;
        private decimal studentLoanAmount;

        //Constructor for DB _context
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create employee
        public async Task CreateAsync(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }

        //Get employee
        public Employee GetById(int employeeId) => 
            _context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();

        //Delete employee
        public async Task Delete(int employeeId)
        {
            var employee = GetById(employeeId);
            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }

        //Update employee
        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id)
        {
            var employee = GetById(id);
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        //Get all employees
        public IEnumerable<Employee> GetAll() => _context.Employees;

        //Loan Repayment
        public decimal StudentLoanRepaymentAmount(int id, decimal totalAmount)
        {
            var employeeId = GetById(id);
            if (employeeId.StudentLoan == StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000)
            {
                studentLoanAmount = 15m;
            }
            else if (employeeId.StudentLoan == StudentLoan.Yes && totalAmount >= 2000 && totalAmount < 2250)
            {
                studentLoanAmount = 38m;
            }
            else if (employeeId.StudentLoan == StudentLoan.Yes && totalAmount >= 2500)
            {
                studentLoanAmount = 83m;
            }
            else
            {
                studentLoanAmount = 0;
            }
            return studentLoanAmount;
        }

        public decimal UnionFees(int id)
        {
            var employeeId = GetById(id);
            var fee = employeeId.UnionMember == UnionMember.Yes ? 10m : 0m;
            return fee;
        }
    }
}
