using System;
using LoanCalculator.Dto;

namespace LoanCalculator.Services
{
    public interface ILoanService
    {
        double CalculateMonthlyPayment(LoanRequestDetailsDto loanRequest);
        double CalculateAdministrationFee(LoanRequestDetailsDto loanRequest);
        double CalculateAPR(LoanRequestDetailsDto loanRequest);
        double CalculateTotalInterestPaid(double monthlyPayment, double loanDuration, double loanAmount);
    }
    public class LoanService : ILoanService
    {

        public double CalculateMonthlyPayment(LoanRequestDetailsDto loanRequest)
        {
            var months = loanRequest.LoanDuration * 12;
            var rate = (loanRequest.InterestRate / 100) / 12;

            var discountFactor = months;
            if (rate != 0)
            {
                var discountFactorNumerator = Math.Pow(1 + rate, months) - 1;
                var discountFactorDenominator = rate * Math.Pow(1 + rate, months);

                discountFactor = discountFactorNumerator / discountFactorDenominator;
            }

            return discountFactor == 0
                ? 0
                : loanRequest.LoanAmount / discountFactor;
        }

        public double CalculateAdministrationFee(LoanRequestDetailsDto loanRequest)
        {
            return Math.Min(
                loanRequest.LoanAmount * loanRequest.AdministrationFixed / 100, 
                loanRequest.AdministrationAmount);
        }

        public double CalculateAPR(LoanRequestDetailsDto loanRequest)
        {
            var totalAccruedAmount = loanRequest.LoanAmount * (1 + loanRequest.LoanDuration * loanRequest.InterestRate / 100);
            var interestAccrued = totalAccruedAmount - loanRequest.LoanAmount;
            var administrationFee = CalculateAdministrationFee(loanRequest);

            double apr = 0;
            if (loanRequest.LoanAmount != 0 || loanRequest.LoanDuration != 0)
            {
                apr = 100 * ((administrationFee + interestAccrued) / loanRequest.LoanAmount) / loanRequest.LoanDuration;
            }

            return apr;
        }

        public double CalculateTotalInterestPaid(double monthlyPayment, double loanDuration, double loanAmount)
        {
            return monthlyPayment * 12 * loanDuration - loanAmount;
        }
    }
}
