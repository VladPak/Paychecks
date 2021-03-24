using Microsoft.AspNetCore.Mvc.Rendering;
using Paychecks.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Paychecks.Persistence
{
    public interface IPayComputationService
    {
        Task CreateAsync(PaymentRecord paymentRecord);

        PaymentRecord GetById(int id);
        IEnumerable<PaymentRecord> GetAll();
        IEnumerable<SelectListItem> GetAllTaxYear();

        decimal OvertimeHours(decimal hoursWorked, decimal contractualHours);
        decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate);

        decimal OvertimeRate(decimal hourlyRate);
        decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours);
        decimal TotalEarning(decimal overtimeEarnings, decimal contractualEarnings);

        decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFee);
        decimal NetPayment(decimal totalEarnings, decimal totalDeduction);
    }
}
