using Microsoft.AspNetCore.Mvc.Rendering;
using Paychecks.Entity;
using Paychecks.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paychecks.Services.Implementation
{
    public class PayComputationService : IPayComputationService
    {
        private decimal contractualEarning;
        private readonly ApplicationDbContext _context;
        private decimal overtimeHours;

        public PayComputationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
            {
                contractualEarning = hoursWorked * hourlyRate;
            }
            else
            {
                contractualEarning = contractualHours * hourlyRate;
            }
            return contractualEarning;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll() => _context.PaymentRecords.OrderBy(p => p.EmployeeId);

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select(TaxYear => new SelectListItem
            {
                Text = TaxYear.YearOfTax,
                Value = TaxYear.Id.ToString()
            });
            return allTaxYear;
        }

        public PaymentRecord GetById(int id) => _context.PaymentRecords.Where(pay => pay.Id == id).FirstOrDefault();

        public decimal NetPayment(decimal totalEarnings, decimal totalDeduction) => totalEarnings - totalDeduction;

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours) => overtimeHours * overtimeRate;

        public decimal OvertimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if (hoursWorked <= contractualHours)
            {
                overtimeHours = 0.00m;
            }
            else if (hoursWorked > contractualHours)
            {
                overtimeHours = hoursWorked - contractualHours;
            }
            return overtimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate) => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFee)
        => tax + nic + studentLoanRepayment + unionFee;

        public decimal TotalEarning(decimal overtimeEarnings, decimal contractualEarnings)
        => overtimeEarnings + contractualEarning;
    }
}
