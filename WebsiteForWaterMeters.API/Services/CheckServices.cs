using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebsiteForWaterMeters.API.EFCore;
using WebsiteForWaterMeters.API.EFCore.Tables;

namespace WebsiteForWaterMeters.API.Services
{
    public class CheckServices
    {
        ApplicationContext bd;
        WebClient wc;
        public CheckServices()
        {
            bd = new ApplicationContext();
            wc = new WebClient();
        }
        private string getValueFromJson(string valueName, string JSON, string endString)
        {
            string result = JSON.Substring(JSON.IndexOf(valueName) + valueName.Length);
            result = result.Substring(0, result.IndexOf(endString));
            return result;
        }
        public int? RegisterCheck(int id)
        {
            Person person = bd.Persons.FirstOrDefault(p=>p.Id==id);
            if (person != null)
            {
                int hash = (person.Id + person.Сounter1 * 100 /*+ DateTime.Now.ToString("Ru-ru")*/).GetHashCode();
                wc.DownloadString("https://pay.pay-ok.org/demo/?REQ={\"PAY_ID\": \""+ hash +"\",\"PAY_ACTION\": \"REG\",\"PAY_DATE\": \""+ DateTime.Now.ToString("Ru-ru") + "\",\"PAY_EMAIL\": \"\",\"PAY_LS\": \""+person.Id+"\",\"PAY_ITOG\":\""+ person.Сounter1*100 + "\",\"PAY_NAME\": \"Оплата водопотребления л/с "+person.Id+"\"}");
                string answer = wc.DownloadString("https://pay.pay-ok.org/demo/?REQ={\"PAY_ID\": \"" + hash + "\",\"PAY_ACTION\": \"GET_INFO\"}");
                Check check = bd.Checks.FirstOrDefault(c => c.Id == hash);
                if (check == null)
                {
                    check = new Check()
                    {
                        Id = hash,
                        OfdName = getValueFromJson("\"ofdName\\\":\\\"", answer, "\\\""),
                        InnOfd = getValueFromJson("\"ofdinn\\\":\\\"", answer, "\\\""),
                        InnCompany = getValueFromJson("\"companyINN\\\":\\\"", answer, "\\\""),
                        DocumentNumber = getValueFromJson("\"documentNumber\\\":", answer, ","),
                        SmenaNumber = getValueFromJson("\"shiftNumber\\\":", answer, ","),
                        DocumentIndex = getValueFromJson("\"documentIndex\\\":", answer, ","),
                        Date = getValueFromJson("\"OD_TIMESTAMP\":\"", answer, "\""),
                        FN = getValueFromJson("\"fsNumber\\\":\\\"", answer, "\\\""),
                        FP = getValueFromJson("\"fp\\\":\\\"", answer, "\\\""),
                        LS = getValueFromJson("\"lsc\":\"", answer, "\""),
                        UslugaName = getValueFromJson("s:59:\\\"", answer, "\\\"").Replace("\\",""),
                        Price = person.Сounter1,
                        PaymentStatus = false
                    };
                    bd.Checks.Add(check);
                    bd.SaveChanges();
                }
                return hash;
            }
            else return null;
        }
        public Check GetCheck(int hash)
        {
            if (hash!=0)
            {
                Check check = bd.Checks.FirstOrDefault(c=>c.Id == hash);
                if (check != null)
                {
                    if (!check.PaymentStatus)
                    {
                        string answer = wc.DownloadString("https://pay.pay-ok.org/demo/?REQ={\"PAY_ACTION\":\"GET_PAYMENT_INFO\",\"PAY_ID\":\""+check.PayId+"\"}");
                        string result = getValueFromJson("\"textstatus\":\"", answer, "\"");
                        if (result == "APPROVED")
                        {
                            check.PaymentStatus = true;
                            bd.SaveChanges();
                        }
                    }
                    return check;
                }
            }
            return null;
        }
        public string GetPaymentLink(int id)
        {
            try
            {
                Check check = bd.Checks.FirstOrDefault(c => c.LS == id.ToString());
                if (check != null)
                {
                    string answer = wc.DownloadString("https://pay.pay-ok.org/demo/?REQ={\"PAY_ACTION\":\"REG_PAYMENT\",\"PAY_ITOG\":\"" + check.Price * 100 + "\",\"PAY_NAME\":\"" + check.UslugaName + "\"}");
                    string resultLink = getValueFromJson("\"PAY_URL\":\"", answer, "\"");
                    check.PayId = long.Parse(getValueFromJson("\"PAY_ID\":\"", answer, "\""));
                    resultLink = resultLink.Replace("\\", "");
                    bd.SaveChanges();
                    return resultLink;
                }
                else return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
