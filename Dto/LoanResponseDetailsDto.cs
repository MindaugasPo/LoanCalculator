
namespace LoanCalculator.Dto
{
    public class LoanResponseDetailsDto
    {
        public double MonthlyPayment { get; set; }
        public double AdministrationFee { get; set; }
        public double TotalInterestPaid { get; set; }
        public double APR { get; set; }
    }
}
