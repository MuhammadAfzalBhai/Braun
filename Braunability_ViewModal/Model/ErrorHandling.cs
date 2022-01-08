using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Services.Protocols;

public class ErrorHandling
{
    public static void WriteError(string errorMessage)
    {
        string path = "";
        try
        {
            path = "~/Logs/" + DateTime.Now.ToString("MMM dd yyyy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine("\r\nLog Entry : ");
                w.WriteLine(System.Web.HttpContext.Current.Server.MapPath(path));
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                              ". Error Message:" + errorMessage;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static void WriteSmsLog(string smsLog)
    {
        string path = "";
        try
        {
            path = "~/SmsLogs/" + DateTime.Now.ToString("MMM dd yyyy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine("\r\nLog Entry : ");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Sms Log: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                              "Description :" + smsLog;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static void TryCatchException(Exception ex)
    {
        StackFrame sf = new StackFrame(true);
        string methodName = sf.GetMethod().ToString();
        int lineNumber = sf.GetFileLineNumber();
        HttpContext ctx = HttpContext.Current;
        Exception exception = ctx.Server.GetLastError();
        string errorInfo =
        Environment.NewLine + "  [Offending URL]: " + ctx.Request.Url.ToString() +
        Environment.NewLine + "  [Source]: " + ex.Source +
        Environment.NewLine + "  [Message]: " + ex.Message +
        Environment.NewLine + "  [Method]: " + methodName.ToString() +
        Environment.NewLine + "  [LineNumber]: " + lineNumber.ToString() +
        Environment.NewLine + "  [Stack trace]: " + ex.StackTrace;
        ErrorHandling.WriteError(errorInfo);
        HttpContext.Current.Server.ClearError();
    }

    public static void TryCatchExceptionNADA(Exception ex)
    {
        StackFrame sf = new StackFrame(true);
        string methodName = sf.GetMethod().ToString();
        int lineNumber = sf.GetFileLineNumber();
        HttpContext ctx = HttpContext.Current;
        Exception exception = ctx.Server.GetLastError();
        string errorInfo =
        Environment.NewLine + "  [Error]: " + ctx.Request.Url.ToString() +
        Environment.NewLine + "  [Errornumber]: " + ex.Source +
        Environment.NewLine + "  [ErrorMessage]: " + ex.Message +
        Environment.NewLine + "  [ErrorSource]: " + methodName.ToString(); 
        ErrorHandling.WriteError(errorInfo);
        HttpContext.Current.Server.ClearError();
    }
}