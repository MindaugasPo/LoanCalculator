using LoanCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoanCalculator.Dto;
using LoanCalculator.Services;

namespace LoanCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoanService _loanService;

        public HomeController(
            ILogger<HomeController> logger,
            ILoanService loanService
            )
        {
            _logger = logger;
            _loanService = loanService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public LoanResponseDetailsDto CalculateLoan(LoanRequestDetailsDto loanRequestDetails)
        {
            var response = new LoanResponseDetailsDto();
            response.MonthlyPayment = _loanService.CalculateMonthlyPayment(loanRequestDetails);
            response.AdministrationFee = _loanService.CalculateAdministrationFee(loanRequestDetails);
            response.TotalInterestPaid = _loanService.CalculateTotalInterestPaid(
                response.MonthlyPayment,
                loanRequestDetails.LoanDuration, 
                loanRequestDetails.LoanAmount);
            response.APR = _loanService.CalculateAPR(loanRequestDetails);

            return response;
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
