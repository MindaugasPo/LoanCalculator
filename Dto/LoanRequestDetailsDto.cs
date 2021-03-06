
namespace LoanCalculator.Dto
{
    public class LoanRequestDetailsDto
    {
        public double AdministrationFixed { get; set; }
        public double AdministrationAmount { get; set; }
        public double InterestRate { get; set; }
        public double LoanDuration { get; set; }
        public double LoanAmount { get; set; }
    }
}
