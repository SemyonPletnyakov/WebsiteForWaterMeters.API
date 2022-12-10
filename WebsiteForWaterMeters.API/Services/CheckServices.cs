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
                int hash = (person.Id + Math.Truncate(person.Сounter1) + DateTime.Now.ToString("Ru-ru")).GetHashCode();
                string answer = wc.DownloadString("https://pay.pay-ok.org/demo/?REQ={\"PAY_ID\": \""+ hash +"\",\"PAY_ACTION\": \"REG\",\"PAY_DATE\": \""+ DateTime.Now.ToString("Ru-ru") + "\",\"PAY_EMAIL\": \"\",\"PAY_LS\": \""+person.Id+"\",\"PAY_ITOG\":\""+ Math.Truncate(person.Сounter1) + "\",\"PAY_NAME\": \"Оплата водопотребления л/с "+person.Id+"\"}");
                answer = wc.DownloadString("https://pay.pay-ok.org/demo/?REQ={\"PAY_ID\": \"" + hash + "\",\"PAY_ACTION\": \"GET_INFO\"}");
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
                        Price = Math.Truncate(person.Сounter1).ToString()
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
                return check;
            }
            else return null;
        }
    }
}
