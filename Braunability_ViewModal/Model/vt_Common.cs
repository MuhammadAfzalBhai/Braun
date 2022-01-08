using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BraunApp_ViewModel.Model.HttpApi;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using System.Web;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net.Security;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace BraunApp_ViewModel.Model
{
    public class vt_Common
    {

      


        #region Security

        public static string DecryptPassword(string Password)
        {
            return Decrypt(Password);
        }

        public static string EncryptPassword(string Password)
        {
            return Encrypt(Password);
        }
        public enum Provider
        {
            linkedin,
            Facebook,
            Portal
        }

        public static string Encrypt(string originalString)
        {
            return Encrypt(originalString, getKey);
        }

        public static byte[] getKey
        {
            get
            {
                return ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["EncryptKey"].ToString());
            }
        }
        public static string Encrypt(string originalString, byte[] bytes)
        {
            try
            {
                if (String.IsNullOrEmpty(originalString))
                {
                    throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
                }

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);

                StreamWriter writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();


                //return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                var Converting = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                Converting = Regex.Replace(Converting, "/", "-");
                Converting = Regex.Replace(Converting, "[+]", "_");
                return Converting;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Decrypt(string originalString)
        {
            return Decrypt(originalString, getKey);
        }

        public static string Decrypt(string cryptedString, byte[] bytes)
        {
            try
            {
                if (String.IsNullOrEmpty(cryptedString))
                {
                    throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
                }
                cryptedString = Regex.Replace(cryptedString, "-", "/");
                cryptedString = Regex.Replace(cryptedString, "[ ]", "+");
                cryptedString = Regex.Replace(cryptedString, "_", "+");

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);

                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return "";
            }
        }




        #endregion

        //#region Custome

        //public static int ToInt(object value)
        //{
        //    int parseVal;
        //    return ((value == null) || (value == DBNull.Value)) ? 0 : int.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        //}
        //public static DateTime GetCurrentDateTime()
        //{
        //    string TimeZoneID = "AUS Eastern Standard Time";
        //    try
        //    {
        //        if (ConfigurationManager.AppSettings["isLive"].ToString().Contains("0"))
        //        {
        //            TimeZoneID = "Pakistan Standard Time";
        //        }
        //        else
        //        {
        //            TimeZoneID = "AUS Eastern Standard Time";
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        TimeZoneID = "AUS Eastern Standard Time";
        //    }

        //    DateTime dt = DateTime.Now;
        //    DateTime currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneID));

        //    return currentTime;
        //}

        public static DateTime ConvertDateString(string Date)
        {
            string[] formats = {
                 "d/M/yyyy h:mm:ss tt",
                         "d/M/yyyy h:mm tt",
                         "dd/MM/yyyy hh:mm:ss",
                         "dd/MM/yyyy hh:mm:ss tt",
                          "dd/MM/yyyy HH:mm:ss tt",
                           "dd/MM/yyyy HH:mm:ss",
                         "d/M/yyyy h:mm:ss",
                         "d/M/yyyy hh:mm tt",
                         "d/M/yyyy hh tt",
                         "d/M/yyyy h:mm",
                         "d/M/yyyy h:mm",
                         "dd/MM/yyyy hh:mm",
                         "dd/MM/yyyy hh:mm:ss",
                         "dd/M/yyyy hh:mm",
                         "d/MM/yyyy HH:mm:ss.ffffff",
                          "d/MM/yyyy",
                          "dd/MM/yyyy",
                "M/d/yyyy h:mm:ss tt",
                           "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss",
                          "MM/dd/yyyy hh:mm:ss tt",
                          "MM/dd/yyyy HH:mm:ss tt",
                           "MM/dd/yyyy HH:mm:ss",
                          "M/d/yyyy h:mm:ss",
                         "M/d/yyyy hh:mm tt",
                        "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm",
                         "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm",
                         "MM/dd/yyyy hh:mm:ss",
                         "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff",
                           "MM/d/yyyy"
                        };


            return DateTime.ParseExact(Date, formats, new CultureInfo("en-US"), DateTimeStyles.None);
        }

        //public static TimeSpan ConvertTimeString(double hour, double minute, double second)
        //{
        //    string strHour = "00";
        //    string strMinute = "00";
        //    string strSecond = "00";

        //    if (hour < 10)
        //        strHour = "0" + hour.ToString();
        //    else
        //        strHour = hour.ToString();

        //    if (minute < 10)
        //        strMinute = "0" + minute.ToString();
        //    else
        //        strMinute = minute.ToString();

        //    if (second < 10)
        //        strSecond = "0" + second.ToString();
        //    else
        //        strSecond = second.ToString();

        //    string strTotalTime = strHour + ":" + strMinute + ":" + strSecond;



        //    return ConvertStringTime(strTotalTime);
        //}

        //public static TimeSpan ConvertStringTime(string Time)
        //{
        //    DateTime dt_TotalTime;
        //    DateTime.TryParseExact(Time, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt_TotalTime);
        //    return dt_TotalTime.TimeOfDay;
        //}
        //public static int CalculateAge(DateTime dOB)
        //{
        //    int age = 0;
        //    age = vt_Common.GetCurrentDateTime().Year - dOB.Year;
        //    if (vt_Common.GetCurrentDateTime().DayOfYear < dOB.DayOfYear)
        //        age = age - 1;

        //    return age;
        //}


        //#endregion



        //#region Link_Security
        //public static void UpdateGuid(string Guid)
        //{
        //    Bal_Layer bal = new Bal_Layer();
        //    bal.updateLinkSecurity(Guid);
        //}

        //public static void InsertGuid(string Guid)
        //{
        //    Bal_Layer bal = new Bal_Layer();
        //    bal.insertLinkSecurity(Guid);
        //}

        //public static DataTable GetGuid(string Guid)
        //{
        //    Bal_Layer bal = new Bal_Layer();
        //    return bal.getLinkGuid(Guid);
        //}

        //#endregion


        //#region ExceptionLog

        //public static void Log(int Userid, string ViewName, string MethodName, string Message, bool isException)
        //{
        //    ExceptionLog(Userid, ViewName, MethodName, Message, isException, vt_Common.GetCurrentDateTime());
        //}


        //public static void ExceptionLog(int Userid, string ViewName, string MethodName, string Message, bool isException, DateTime CreatedDate)
        //{
        //    Message = "isLiveException: " + ConfigurationManager.AppSettings["isLive"].ToString() + " " + Environment.NewLine + " " + Message;
        //    vt_Common.DbLog(Userid, ViewName, MethodName, Message, isException, CreatedDate);
        //    vt_Common.EmailLog(Userid, ViewName, MethodName, Message, isException, CreatedDate);
        //    vt_Common.SmsLog(Userid, ViewName, MethodName, Message, isException, CreatedDate);
        //    vt_Common.TxtFileLog(Userid, ViewName, MethodName, Message, isException, CreatedDate);
        //}

        //public static void DbLog(int Userid, string ViewName, string MethodName, string Message, bool isException, DateTime CreatedDate)
        //{
        //    //Bal_Layer bal = new Bal_Layer();
        //    vt_Log log = new vt_Log();

        //    log.UserId = Userid;
        //    log.ViewName = ViewName;
        //    log.MethodName = MethodName;
        //    log.Message = Message;
        //    log.isException = true;
        //    log.CreatedDate = CreatedDate;

        //    //bal.InsertDBLog(log);
        //}

        ////To Create the email body
        //public static void EmailLog(int Userid, string ViewName, string MethodName, string Message, bool isException, DateTime CreatedDate)
        //{
        //    string html = @"<html>
        //            <head>
        //                <title></title>

        //                <style>
        //                    table {
        //                        border-collapse: collapse;
        //                        border: 0;
        //                    }
        //                </style>
        //            </head>
        //            <body>
        //                <table style='width: 600px; margin: 0 auto; padding: 15px;'>
        //                    <tbody>
        //                        <tr>
        //                            <td>
        //                                <table style='width: 100%;'>
        //                                    <tr>
        //                                        <td align='center' style='background-color: #f2f5f9; padding: 15px;'>
        //                                            <img src='https://app.superdrive.com.au/Content/SDImage/logo.png' width='200px'>
        //                                            <h1 style='font-weight: 500;'>Super Drive Exception<br/>

        //                                   <span style='width: 100px; height: 2px; background-color: #333; display: inline-block; margin-top: 4px;'></span>

        //                                            </h1>
        //                                         </td>
        //            </tr>
        //            <td style='background-color: #f2f5f9; padding: 15px;'>
        //                                            <span style='text-align:left'>" + "<b>Super Drive Exception Log:</b><br>" + @"</span>
        //                                        <p style='font-size: 17px; line-height: 30px; text-align: left; color: #0949f6; padding: 10px 40px;'>
        //                                        UserID: " + Userid + @" <br>
        //                                        View: " + ViewName + @" <br>
        //                                        Method: " + MethodName + @" <br>
        //                                        Message: " + Message + @" <br>
        //                                        CreatedDate: " + CreatedDate + @" <br>

        //                                            </p>
        //                                    <br>
        //                                <span style='text-align:left'>
        //                            Regards, <br>
        //                                The SuperDrive Crew <br>
        //                                “Super Crew! Assemble!”<br>
        //                                <img src='https://app.superdrive.com.au/Content/SDImage/signature.jpg' width='150px' height='50px'>
        //                                </span>



        //                                        </td>
        //                                    </tr>

        //                                </table>
        //                                <table width='100 % '>
        //                                    <tr>
        //                                        <td style='height: 20px;'></td>
        //                                    </tr>

        //                                </table>
        //                            </td>
        //                        </tr>
        //                    </tbody>
        //                </table> 
        //            </body>
        //            </html>";

        //    //Sending Error Email
        //    SendLogEmail(html);

        //}

        ////To create the sms body
        //public static void SmsLog(int Userid, string ViewName, string MethodName, string Message, bool isException, DateTime CreatedDate)
        //{
        //    string smsbody = "SuperDrive Exception Log: UserID: " + Userid + ", View: " + ViewName + ", Method: " + MethodName + ", Message: " + Message + ", isException: " + isException + ", CreatedDate: " + CreatedDate + ".";

        //    SendLogSMS(smsbody);
        //}

        ////To create the txt body
        //public static void TxtFileLog(int Userid, string ViewName, string MethodName, string Message, bool isException, DateTime CreatedDate)
        //{
        //    string txtbody = "SuperDrive Exception Log: UserID: " + Userid + " View: " + ViewName + " Method: " + MethodName + " Message: " + Message + " isException: " + isException + " CreatedDate: " + CreatedDate + ".";

        //    ErrorHandling.WriteError(txtbody);
        //}

        ////public static DataTable GetAllEmailandPhone()
        ////{
        ////    //Bal_Layer bal = new Bal_Layer();
        ////    //return bal.getEmailTable();
        ////}

        //public static string SendLogEmail(string body)
        //{
        //    try
        //    {

        //        //Get All records of Emails from db using sp in DataTable
        //        //Loop thorugh al record and concatenate record with ',' like email1,email2,email3.....

        //        string cc = "";
        //        DataTable dt = GetAllEmailandPhone();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            cc += row[1] + ",";
        //        }

        //        cc = cc.TrimEnd(',');

        //        //MailTo = ConfigurationSettings.AppSettings["MailTo"].ToString();

        //        string to = "", bcc = "", subject = "", ReplyTo = "", attachment = "";

        //        to = ConfigurationSettings.AppSettings["MailTo"].ToString();

        //        //string[] ToEmails = to.Split(',');
        //        using (MailMessage message = new MailMessage())
        //        {
        //            string[] Add = to.ToString().Split(',');
        //            message.From = new MailAddress(ConfigurationSettings.AppSettings["MailFrom"].ToString(), ConfigurationSettings.AppSettings["MailFromName"].ToString());
        //            if (Add.Length > 1)
        //            {
        //                for (int a = 0; a < Add.Length; a++)
        //                {
        //                    message.To.Add(new MailAddress(Add[a].ToString()));
        //                }
        //            }
        //            else { message.To.Add(new MailAddress(to)); }
        //            if (cc != null && cc != "")
        //            {
        //                if (cc.ToString().Contains(","))
        //                {
        //                    string[] Add_cc = cc.ToString().Split(',');
        //                    if (Add_cc.Length > 1)
        //                    {
        //                        for (int a = 0; a < Add_cc.Length; a++)
        //                        {
        //                            message.CC.Add(new MailAddress(Add_cc[a]));
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    message.CC.Add(new MailAddress(cc));
        //                }
        //            }
        //            if (bcc != null && bcc != "")
        //            {
        //                message.Bcc.Add(new MailAddress(bcc));
        //            }
        //            if (ReplyTo != null && ReplyTo != "")
        //            {
        //                message.ReplyToList.Add(new MailAddress(ReplyTo));
        //            }
        //            message.IsBodyHtml = true;


        //            message.Subject = "Superdrive Exception";
        //            message.Body = body;



        //            using (SmtpClient smtp = new SmtpClient())
        //            {
        //                smtp.UseDefaultCredentials = true;
        //                smtp.Host = ConfigurationSettings.AppSettings["SmtpServer"].ToString();


        //                if (bool.Parse(ConfigurationSettings.AppSettings["UseCredentials"]))
        //                {
        //                    string EmailPass = (ConfigurationSettings.AppSettings["Password"].ToString());
        //                    smtp.Credentials = new NetworkCredential(ConfigurationSettings.AppSettings["UserEmail"].ToString(), EmailPass);

        //                }
        //                else
        //                {
        //                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                    smtp.UseDefaultCredentials = false;

        //                }
        //                smtp.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["Port"].ToString());
        //                smtp.EnableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["isSSL"].ToString());


        //                try
        //                {
        //                    smtp.Send(message);
        //                }
        //                catch (Exception ex)
        //                {
        //                    return "Exception: " + ex.Message.ToString();
        //                }
        //            }
        //            //if (type == "Forgot Password")
        //            //    return "Successfully email send for reset pass";
        //            //if (type == "Set Password")
        //            //    return "Successfully email send for set pass";
        //            //else
        //            return "email sent successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}

        //public static void SendLogSMS(string Body)
        //{
        //    //Get All records of PhoneNumber from db using sp in DataTable
        //    //Loop thorugh al record and concatenate record with ',' like email1,email2,email3.....

        //    string phoneNumber = "";

        //    DataTable dt = GetAllEmailandPhone();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        phoneNumber += row[2] + ",";
        //    }

        //    phoneNumber = phoneNumber.TrimEnd(',');

        //    string ACCOUNT_SID = ConfigurationManager.AppSettings["ACCOUNT_SID"].ToString(); //"AC51942f59dfc8549581649307c913a91d";
        //    string AUTH_TOKEN = ConfigurationManager.AppSettings["AUTH_TOKEN"].ToString(); //"6205b8a5743322e25b3207606fee7457";
        //    TwilioClient.Init(ACCOUNT_SID, AUTH_TOKEN);


        //    string[] splitNo = phoneNumber.Split(',');
        //    if (splitNo.Length > 1)
        //    {
        //        for (int i = 0; i < splitNo.Length; i++)
        //        {
        //            var to = new PhoneNumber(splitNo[i]);
        //            try
        //            {

        //                var message = MessageResource.Create(
        //                    to,
        //                    ////from: new PhoneNumber("+13025261595"),

        //                    from: new PhoneNumber(ConfigurationManager.AppSettings["SMSFrom"].ToString()),
        //                    // statusCallback: new Uri(callbackurl),
        //                    body: Body);
        //            }
        //            catch (Exception ex)
        //            {
        //                ErrorHandling.WriteError(ex.ToString());
        //            }
        //        }

        //    }
        //    else
        //    {

        //    }


        //}
        //#endregion

    }
}
