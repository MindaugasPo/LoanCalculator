using System;
using LoanCalculator.Dto;
using LoanCalculator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class LoanServiceTests
    {
        [TestMethod]
        public void CalculateMonthlyPayment_CalculatesCorrectValue()
        {
            var service = GetService();
            var loanRequest = new LoanRequestDetailsDto()
            {
                LoanAmount = 500000,
                LoanDuration = 10,
                AdministrationAmount = 10000,
                AdministrationFixed = 1,
                InterestRate = 5
            };
            var result = service.CalculateMonthlyPayment(loanRequest);
            Assert.AreEqual(5303.28, Math.Round(result, 2));
        }
        [TestMethod]
        public void CalculateAdministrationFee_CalculatesCorrectValue()
        {
            var service = GetService();
            var loanRequest = new LoanRequestDetailsDto()
            {
                LoanAmount = 500000,
                LoanDuration = 10,
                AdministrationAmount = 10000,
                AdministrationFixed = 1,
                InterestRate = 5
            };
            var result = service.CalculateAdministrationFee(loanRequest);
            Assert.AreEqual(5000, Math.Round(result, 2));
        }
        [TestMethod]
        public void CalculateAPR_CalculatesCorrectValue()
        {
            var service = GetService();
            var loanRequest = new LoanRequestDetailsDto()
            {
                LoanAmount = 2000,
                LoanDuration = 2,
                AdministrationAmount = 200,
                AdministrationFixed = 100,
                InterestRate = 5
            };
            var result = service.CalculateAPR(loanRequest);
            Assert.AreEqual(10, Math.Round(result, 2));
        }
        [TestMethod]
        public void CalculateTotalInterestPaid_CalculatesCorrectValue()
        {
            var service = GetService();
            var result = service.CalculateTotalInterestPaid(5303.2757619537761, 10, 500000);
            Assert.AreEqual(136393.09, Math.Round(result, 2));
        }

        private ILoanService GetService()
        {
            return new LoanService();
        }
    }
}
