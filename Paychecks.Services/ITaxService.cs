using System;
using System.Collections.Generic;
using System.Text;

namespace Paychecks.Services
{
    public interface ITaxService
    {
        decimal TaxAmount(decimal totalAmount);
    }
}
