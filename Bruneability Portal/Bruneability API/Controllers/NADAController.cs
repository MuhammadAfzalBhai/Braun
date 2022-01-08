using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bruneability_API.Controllers
{
    public class NADAController : Controller
    {
        // GET: NADA
        public ActionResult Index()
        {
            return View();
        }

        [Route("api/Admin/GetEmployeeByID")]
        [HttpPost]
        private string getToken(string userName, string password)
        {
            string token = "";
            try
            {
                //using (sref_SecureLogin.SecureLoginSoapClient sLogin = new sref_SecureLogin.SecureLoginSoapClient())
                //{
                //    // assumes C# project has added a Service Reference named sref_SecureLogin to
                //    //SecureLogin service at destination URL using local copy of wsdl or(url) ? wsdl

                //    sref_SecureLogin.GetTokenRequest req = new sref_SecureLogin.GetTokenRequest();
                //    req.Username = userName;
                //    req.Password = password;
                //    token = sLogin.getToken(req);
                //}
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error with getToken() call :\n" + ex.Message, "Service error",
               //MessageBoxButtons.OK, MessageBoxIcon.Error);
                token = "";
            }
            return token;
        }
    }
}