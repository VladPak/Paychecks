using System;
using System.Collections.Generic;
using System.Text;

namespace Paychecks.Services
{
    public interface INationalInsuranceContributionService
    {
        decimal NIContribution(decimal totalAmount);
    }
}
