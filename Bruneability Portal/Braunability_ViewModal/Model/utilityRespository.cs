using BraunApp_ViewModel.Model;
using System;
using System.Configuration;

namespace Braunability_ViewModal.Model
{
    public class utilityRespository
    {
        public static string getBaseUrl()
        {
            string hostingEnvironment = getAppSettingKey("HostingEnvironment");
            string baseUrl = string.Empty;

            if (hostingEnvironment == "0")
            {
                baseUrl = ConfigurationManager.AppSettings["APIBaseUrlBraunLocal"].ToString();
            }
            else if(hostingEnvironment == "1")
            {
                baseUrl = ConfigurationManager.AppSettings["APIBaseUrlBraunAWS"].ToString();
            }

            else
            {
                baseUrl = ConfigurationManager.AppSettings["APIBaseUrlBraunUAT"].ToString();
            }

            return baseUrl;
        }

        public static string getAppSettingKey(string keyName)
        {
            string url = string.Empty;
            var endPoint = ConfigurationManager.AppSettings[keyName];
            if (endPoint != null)
            {
                url = endPoint.ToString();
            }
            return url;
        }

        public static string Accesstoken(int userID, int RoleID, bool Isportal)
        {
            string Plaintoken = string.Empty;
            if (Isportal)
            {
                Plaintoken = userID + "-" + RoleID + "-" + "Web-" + DateTime.Now;
                Plaintoken = Plaintoken.Replace("/", "@").Replace(" ", "+").Replace(":", "#");
                Plaintoken = vt_Common.Encrypt(Plaintoken);
            }
            else
            {
                Plaintoken = userID + "-" + RoleID + "-" + "Mobile-" + DateTime.Now;
                Plaintoken = Plaintoken.Replace("/", "@").Replace(" ", "+").Replace(":", "#");
                Plaintoken = vt_Common.Encrypt(Plaintoken);
            }
            return Plaintoken;
        }

        public static bool ValidationAccesstoken(string Token)
        {
            bool IsValid = false;
            string cipher = Token;
            cipher = vt_Common.Decrypt(cipher);
            cipher = cipher.Replace("@", "/").Replace("+", " ").Replace("#", ":");
            if (cipher.Contains("Web") || cipher.Contains("Mobile"))
            {
                if (cipher.Contains("Web"))
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = true;
                }
            }
            else
            {
                IsValid = false;
            }
            //if (Isportal)
            //{
            //    cipher = vt_Common.Decrypt(cipher);
            //    cipher = userID + "-" + RoleID + "-" + "Web-" + DateTime.Now;
            //    cipher = cipher.Replace("@", "/").Replace("+", " ").Replace("#", ":");
            //}
            //else
            //{
            //    cipher = vt_Common.Decrypt(cipher);
            //    cipher = userID + "-" + RoleID + "-" + "Mobile-" + DateTime.Now;
            //    cipher = cipher.Replace("@", "/").Replace("+", " ").Replace("#", ":");
            //}
            return IsValid;
        }
    }
}
