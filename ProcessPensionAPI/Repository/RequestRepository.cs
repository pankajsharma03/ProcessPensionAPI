
using Newtonsoft.Json;
using ProcessPensionAPI;
using ProcessPensionAPI.Model;
using ProcessPensionAPI.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ProcessPensionAPI.Repository
{
    public class RequestRepository : IRequestRepository
    {

        public PensionDetail ProcessPension(ProcessPensionInput input, string token)
        {
            try
            {
                if(token == null)
                {
                    return null;
                }
                Client obj = new Client();
                HttpClient client = obj.PensionerDetailsAPI();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                HttpResponseMessage response = new HttpResponseMessage();
                StringContent content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
                response = client.GetAsync("/api/PensionerDetail/getPensionerDetails?aadharnumber=" + input.AadhaarNumber.ToString()).Result;
                var responseData = JsonConvert.DeserializeObject<PensionerDetail>(response.Content.ReadAsStringAsync().Result);
                PensionDetail detail = CalculatePension(responseData);
                string filePath = @"D:\anagular\Pankaj Project\Project_Pension-master\Project_Pension-master\ProcessPensionAPI\ProcessPensionAPI\PensionData.csv";
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string[] values = { input.AadhaarNumber.ToString(), detail.PensionAmount.ToString(), detail.BankServiceCharge.ToString() };
                    string line = String.Join(",", values);
                    sw.WriteLine(line);
                }

                return detail;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private PensionDetail CalculatePension(PensionerDetail detail)
        {
            
            PensionDetail pensionDetail = new PensionDetail();
            if(detail != null)
            {
                try
                {
                    pensionDetail.PensionAmount = CalculatePensionAmount(detail.PensionClassification, detail.SalaryEarned, detail.Allowances);
                    pensionDetail.BankServiceCharge = CalculateBankCharges(detail.bankDetail.BankType);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return pensionDetail;
        }

        private double CalculatePensionAmount(string pensionType, double lastSalary, double allowances)
        {
            var kvm = getPensionAmountDictionary();
            double pensionAmount = 0;
            foreach (var item in kvm)
            {
                if (item.Key == pensionType)
                {
                    pensionAmount += item.Value * (lastSalary + allowances);
                    break;
                }
            }

            return pensionAmount;
        }
        private double CalculateBankCharges(string bankType)
        {
            var kvm = getBankChargeDictionary();
            double bankCharges = 0;
            foreach (var item in kvm)
            {
                if (item.Key == bankType)
                {
                    bankCharges += item.Value;
                    break;
                }
            }

            return bankCharges;
        }

        private IDictionary<string, double> getPensionAmountDictionary()
        {
            IDictionary<string, double> keyValuePairs = new Dictionary<string, double>();
            keyValuePairs.Add("Self", 0.8);
            keyValuePairs.Add("Family", 0.5);
            return keyValuePairs;
        }
        private IDictionary<string, double> getBankChargeDictionary()
        {
            IDictionary<string, double> keyValuePairs = new Dictionary<string, double>();
            keyValuePairs.Add("Public", 500);
            keyValuePairs.Add("Private", 550);
            return keyValuePairs;
        }
    }
}
