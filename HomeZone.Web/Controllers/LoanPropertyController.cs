using HomeZone.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HomeZone.Web.Controllers
{
    public class LoanPropertyController : Controller
    {
        private readonly ILoanPropertyService loanService;

        public LoanPropertyController(ILoanPropertyService loanService)
        {
            this.loanService = loanService;
        }
    }
}
