using System.Linq;
using System.Web.Mvc;
using Braunability_ViewModal.Model;
using BraunApp_ViewModel.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Braunability_ViewModal.Model.BraunVM_Request;
using static Braunability_ViewModal.Model.BraunVM_Response;
using System;
using System.Web.Security;
using System.Web.UI;

namespace Bruneability_Portal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            BraunSession _checksession =(BraunSession)Session["BraunSession"];
            if (_checksession!=null)
            {
                if (_checksession.SessionUser.RoleID == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Employee");
                }
            }

            else
            {
                return View();
            }    
           
             //Login Form View
        }

        //[HttpPost]
        //public async Task<ActionResult> Index(LoginRequest user)
        //{
        //    string landingController = string.Empty;
        //    string landingtAction = string.Empty;
        //    if (ModelState.IsValid)
        //    {

        //        //Login Credentials
        //        //user.Password = vt_Common.Encrypt(user.Password);
        //        user.Isportal = true;
        //        //AuthenticateUser Api Call
        //        string url = utilityRespository.getBaseUrl() + "login/AuthenticateUser";
        //        Task<string> strResponse = HttpApi.AsyncCreateRequest(url, user);
        //        AuthenticationResponseModel model = JsonConvert.DeserializeObject<AuthenticationResponseModel>(await strResponse);

        //        if (model != null && model.status != false)
        //        {
        //            BraunSession sess = new BraunSession();
        //            sess.SessionUser = model.user;
        //            sess.pagelist = model.UserPermission;
        //            sess.accesstoken = model.accesstoken;
        //            Session.Add("BraunSession", sess);
        //            string PageURL = model.UserPermission.OrderBy(x => x.Order).Where(x => x.Order != null && x.Role == model.user.RoleID).Select(x => x.PageURL).FirstOrDefault();
        //            landingController = PageURL.Split('/')[0];
        //            landingtAction = PageURL.Split('/')[1];
        //        }
        //        else
        //        {
        //            landingController = "Login";
        //            landingtAction = "Index";
        //        }

        //    }
        //    else
        //    {
        //        landingController = "Login";
        //        landingtAction = "Index";
        //    }
        //    return RedirectToAction(landingtAction, landingController);
        //}

        [HttpPost]
        public ActionResult Index(LoginRequest user)
        {
            string landingController = string.Empty;
            string landingtAction = string.Empty;
            if (ModelState.IsValid)
            {

                //Login Credentials
                //user.Password = vt_Common.Encrypt(user.Password);
                user.Isportal = true;
                //AuthenticateUser Api Call
                string url = utilityRespository.getBaseUrl() + "Login/LoginUser";
                string strResponse = HttpApi.CreateRequest(url, user);
                AuthenticationResponseModel model = JsonConvert.DeserializeObject<AuthenticationResponseModel>(strResponse);

                if (model != null && model.status != false)
                {
                    BraunSession sess = new BraunSession();
                    sess.SessionUser = model.User;
                    sess.pagelist = model.UserPermission;
                    sess.accesstoken = model.accesstoken;
                    Session.Add("BraunSession", sess);
                    string PageURL = model.UserPermission.OrderBy(x => x.Order).Where(x => x.Order != null && x.Role == model.User.RoleID).Select(x => x.PageURL).FirstOrDefault();
                    landingController = PageURL.Split('/')[0];
                    landingtAction = PageURL.Split('/')[1];

                    return RedirectToAction(landingtAction, landingController);
                }
                else
                {
                    user.Status = model.status;
                    user.Message = model.message;
                    return View(user);
                    //landingController = "Login";
                    //landingtAction = "Index";

                    //return RedirectToAction(landingtAction, landingController);
                }

            }
            else
            {
                //landingController = "Login";
                //landingtAction = "Index";
                user.Status = false;
                user.Message = "Please fill the mandatory fields.";
                return View(user);
            }

        }


        public ActionResult SignUpEmployee()
        {
            return View(); //SignUp Form View
        }

        [HttpPost]
        public ActionResult SignUpEmployee(EmployeeForm Emp)
        {
            string url = string.Empty;
            string strResponse = string.Empty;
            string landingController = string.Empty;
            string landingtAction = string.Empty;
            dynamic responseJson = string.Empty;
            if (Emp.JoiningDate!= "Invalid date")
            {
                if (ModelState.IsValid)
                {
                    url = utilityRespository.getBaseUrl() + "Login/ValidateUserEmail";
                    strResponse = HttpApi.CreateRequest(url, Emp);
                    HeaderResponse Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);


                    if (Response.status == true)
                    {
                        if (Emp.ID == 0)
                        {
                            Emp.IsApproved = false;
                            Emp.IsActive = false;
                            url = utilityRespository.getBaseUrl() + "Admin/AddUpdateEmployee";
                            strResponse = HttpApi.CreateRequest(url, Emp);
                            HeaderResponse Data = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                            Emp.Status = Data.status;
                            Emp.Message = Data.message;
                            return View(Emp);
                        }
                        else
                        {
                            Emp.Status = false;
                            Emp.Message = "ID is not Null while SignUp.";
                            return View(Emp);
                        }
                    }
                    else
                    {
                        Emp.Status = Response.status;
                        Emp.Message = Response.message;
                        return View(Emp);
                    }
                }
                else
                {
                    Emp.Status = false;
                    Emp.Message = "Please fill the mandatory fields.";
                    return View(Emp);
                }
            }
            else
            {
                Emp.Status = false;
                Emp.Message = "Joining date is mandatory.";
                return View(Emp);
            }    
           
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Add("BraunSession", null);
            return View("Index");
        }
        [HttpPost]
        public ActionResult IsEmailExist(string emailEnter)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
              
                string url = string.Empty;
                string strResponse = string.Empty;
                url = utilityRespository.getBaseUrl() + "Login/IsEmailExist?email="+ emailEnter;
                strResponse = HttpApi.CreateRequest(url);
                Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);
                return Json(new { status = Response.status, message = Response.message }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Email Not Exist" }, JsonRequestBehavior.AllowGet);
                
            }
            
        }

    }
}