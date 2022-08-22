using System;

namespace ProcessPensionAPI.Model
{
    public class PensionerDetail
    {
        public int Id { get; set; }
        public string DateOfBirth { get; set; }
        public string PAN { get; set; }
        public string AadharNumber { get; set; }    
        public double SalaryEarned { get; set; }
        public double Allowances { get; set; }
        public string PensionClassification { get; set; }
        public BankDetail bankDetail { get; set; }

    }
}
