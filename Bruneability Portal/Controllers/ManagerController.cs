using Braunability_ViewModal.Model;
using BraunApp_ViewModel.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Braunability_ViewModal.Model.BraunVM_Request;
using static Braunability_ViewModal.Model.BraunVM_Response;
using DAL.DBEntities;

namespace Bruneability_Portal.Controllers
{
    public class ManagerController : BaseController
    {
        BraunSession _Session;
        // GET: Manager
        public ActionResult SalesRep()
        {
            
            return View();
        }
        public ActionResult successMessage()
        {
            return View();
        }

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Approval()
        {

            return View();
        }

        public ActionResult Rejected()
        {
            return View();
        }
        public ActionResult Approved()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult NewEmployeeForm()
        {
            try
            {
                GetEmployeeForm employeesData = new GetEmployeeForm();

                employeesData.EmpData = new EmployeeForm();
                employeesData.EmpData.RoleList = RoleList();
                return PartialView("_EmployeeForm", employeesData.EmpData);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetManagerDashboardcounterResult()
        {
            GetManagerDahboardCounterValues reponse = new GetManagerDahboardCounterValues();

            string jsondata = string.Empty;

            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                request.UserID = CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Manager/GetManagerDahboardCounterValues";
                string strResponse = HttpApi.CreateRequest(url, request);
                reponse = JsonConvert.DeserializeObject<GetManagerDahboardCounterValues>(strResponse);

                jsondata = JsonConvert.SerializeObject(reponse, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                reponse.status = false;
                reponse.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(reponse, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult QuoteslastSixMonth()
        {
            GetDashboard reponse = new GetDashboard();

            string jsondata = string.Empty;

            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Manager/QuoteslastSixMonth";
                string strResponse = HttpApi.CreateRequest(url, request);
                reponse = JsonConvert.DeserializeObject<GetDashboard>(strResponse);

                jsondata = JsonConvert.SerializeObject(reponse, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                reponse.status = false;
                reponse.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(reponse, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllPUpApprovedQoutesDataByEmployeeId()
        {
            _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
            string jsondata = string.Empty;
            GetQoutesDataList ListofQuotes = new GetQoutesDataList();
            try
            {
                GetbyID request = new GetbyID();
                request.token = CurrentUser.accesstoken;
                request.ID = CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Manager/GetAllPUpApprovedQoutesDataByEmployeeId";
                string strResponse = HttpApi.CreateRequest(url, request);
                ListofQuotes = JsonConvert.DeserializeObject<GetQoutesDataList>(strResponse);
                jsondata = JsonConvert.SerializeObject(ListofQuotes, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ListofQuotes.status = false;
                ListofQuotes.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(ListofQuotes, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
                ;
            }

        }

        public ActionResult GetAllRejectedData()
        {
            _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
            string jsondata = string.Empty;
            GetRejectedQoutesDataList ListofQuotes = new GetRejectedQoutesDataList();
            try
            {
                GetbyID request = new GetbyID();
                request.token = CurrentUser.accesstoken;
                request.ID = CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Manager/GetAllRejectedData";
                string strResponse = HttpApi.CreateRequest(url, request);
                ListofQuotes = JsonConvert.DeserializeObject<GetRejectedQoutesDataList>(strResponse);
                jsondata = JsonConvert.SerializeObject(ListofQuotes, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ListofQuotes.status = false;
                ListofQuotes.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(ListofQuotes, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllApprovedData()
        {
            _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
            string jsondata = string.Empty;
            GetApprovedQoutesDataList ListofQuotes = new GetApprovedQoutesDataList();
            try
            {
                GetbyID request = new GetbyID();
                request.token = CurrentUser.accesstoken;
                request.ID = CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Manager/GetAllApprovedData";
                string strResponse = HttpApi.CreateRequest(url, request);
                ListofQuotes = JsonConvert.DeserializeObject<GetApprovedQoutesDataList>(strResponse);
                jsondata = JsonConvert.SerializeObject(ListofQuotes, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ListofQuotes.status = false;
                ListofQuotes.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(ListofQuotes, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ApproveDetails(int ID)
        {
            _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
            string jsondata = string.Empty;
            GetQoutesDataList ListofQuotes = new GetQoutesDataList();
            try
            {
                GetbyID request = new GetbyID();
                request.token = CurrentUser.accesstoken;
                request.ID = ID;
                string url = utilityRespository.getBaseUrl() + "Manager/ApproveDetails";
                string strResponse = HttpApi.CreateRequest(url, request);
                ListofQuotes = JsonConvert.DeserializeObject<GetQoutesDataList>(strResponse);
                jsondata = JsonConvert.SerializeObject(ListofQuotes, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ListofQuotes.status = false;
                ListofQuotes.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(ListofQuotes, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
                ;
            }

        }
        public ActionResult Approve(Approvebyid qoute)
        {
            try
            {
                if (qoute.ID != 0)
                {
                    Approvebyid qoutes = new Approvebyid();
                    qoutes.ID = qoute.ID;
                    qoutes.Comment = qoute.Comment;
                    qoutes.token = CurrentUser.accesstoken;
                    string url = utilityRespository.getBaseUrl() + "Manager/Approval";
                    string strResponse = HttpApi.CreateRequest(url, qoutes);
                    HeaderResponse Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);
                    vt_BraunAppEntities db = new vt_BraunAppEntities();

                    var User = db.vt_Quotes.Where(x => x.ID == qoute.ID).FirstOrDefault();
                    string UserID = Convert.ToString(User.EmployeeID);
                    string token = CurrentUser.accesstoken;
                    string Sub = "Qoute Approved";
                    string Dec = CurrentUser.SessionUser.UserName + " is approved your qoute request";
                    url = utilityRespository.getBaseUrl() + "Employee/SendEmail?ManagerID=" + UserID + "&Subject=" + Sub + "&Description=" + Dec + "&token=" + token;
                    strResponse = HttpApi.CreateRequest(url);

                    return Json(new { status = Response.status, message = Response.message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Qoute ID is Null." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Reject(Rejectbyid qoute)
        {
            try
            {
                if (qoute.ID != 0)
                {
                    Rejectbyid qoutes = new Rejectbyid();
                    qoutes.ID = qoute.ID;
                    qoutes.Comment = qoute.Comment;
                    qoutes.token = CurrentUser.accesstoken;
                    string url = utilityRespository.getBaseUrl() + "Manager/Reject";
                    string strResponse = HttpApi.CreateRequest(url, qoutes);
                    HeaderResponse Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                    vt_BraunAppEntities db = new vt_BraunAppEntities();
                    var User = db.vt_Quotes.Where(x => x.ID == qoute.ID).FirstOrDefault();
                    string UserID = Convert.ToString(User.EmployeeID);
                    string token = CurrentUser.accesstoken;
                    string Sub = "Qoute Rejected";
                    string Dec = CurrentUser.SessionUser.UserName + " is rejected your qoute request";
                    url = utilityRespository.getBaseUrl() + "Employee/SendEmail?ManagerID=" + UserID + "&Subject=" + Sub + "&Description=" + Dec + "&token=" + token;
                    strResponse = HttpApi.CreateRequest(url);

                    return Json(new { status = Response.status, message = Response.message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Qoute ID is Null." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public SelectList RoleList()
        {
            HeaderToken request = new HeaderToken();

            string url = utilityRespository.getBaseUrl() + "Admin/RoleListForManager";
            string strResponse = HttpApi.CreateRequest(url, request);

            DataTable tester = (DataTable)JsonConvert.DeserializeObject(strResponse, (typeof(DataTable)));
            DataRow newRow = tester.NewRow();
            newRow[0] = "0";
            newRow[1] = "Please Select";
            tester.Rows.InsertAt(newRow, 0);
            return new SelectList(tester.AsDataView(), "ID", "RoleName");
        }

        public ActionResult AddUpdateEmployee(EmployeeForm emp)
        {
            try
            {
                _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
                //emp.RoleId = Convert.ToInt32(_Session.SessionUser.RoleID);
                emp.UserId = Convert.ToInt32(_Session.SessionUser.ID);
                if (ModelState.IsValid)
                {
                    if (emp != null)
                    {
                        emp.IsApproved = true;
                        emp.IsActive = true;
                        emp.token = CurrentUser.accesstoken;
                        if(CurrentUser.SessionUser.RoleID == 1){
                            emp.RoleID = emp.RoleID;
                        }
                        else if(CurrentUser.SessionUser.RoleID == 3 && CurrentUser.SessionUser.ManagerStatus == "Brauneability Manager") {
                            emp.RoleID = 4;
                            emp.Managerstatus = "Braun Ability Dealers";
                        }
                        else if(CurrentUser.SessionUser.RoleID == 3 && CurrentUser.SessionUser.ManagerStatus == "Other Manager"){
                            emp.RoleID = 5;
                            emp.Managerstatus = "Other Dealers";
                        }
                        
                        string url = utilityRespository.getBaseUrl() + "Admin/AddUpdateEmployee";
                        string strResponse = HttpApi.CreateRequest(url, emp);
                        HeaderResponse empData = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                        return Json(new { status = empData.status, message = empData.message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = false, message = "Parameters are Null." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Fill the required fields." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Modelstate = ModelState, status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllEmployeeData()
        {
            _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
            string jsondata = string.Empty;
            GetEmployeeDataList employeesData = new GetEmployeeDataList();

            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                request.roleID = Convert.ToInt32(_Session.SessionUser.RoleID);
                request.UserID = CurrentUser.SessionUser.ID;
                //var token = CurrentUser.accesstoken;
                //var validtoken = utilityRespository.ValidationAccesstoken(token);

                //if (validtoken)
                //{
                string url = utilityRespository.getBaseUrl() + "Admin/GetAllEmployees";
                string strResponse = HttpApi.CreateRequest(url, request);
                employeesData = JsonConvert.DeserializeObject<GetEmployeeDataList>(strResponse);
                //GetEmployeeDataList employeesData = JsonConvert.DeserializeObject<GetEmployeeDataList>(strResponse);


                jsondata = JsonConvert.SerializeObject(employeesData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
                //return Json(new { status = employeesData.status, message = employeesData.message, EmployeeList =  employeesData.EmployeeList }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    //return Json(new { status = false, message = "Invalid token." }, JsonRequestBehavior.AllowGet);
                //    employeesData.status = false;
                //    employeesData.message = "Invalid token.";
                //    jsondata = JsonConvert.SerializeObject(employeesData, Formatting.Indented);
                //    return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
                //}


            }
            catch (Exception ex)
            {
                employeesData.status = false;
                employeesData.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(employeesData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetEmployeeByID(int ID)
        {
            GetEmployeeForm employeesData = new GetEmployeeForm();
            try
            {
                GetbyID user = new GetbyID();
                user.token = CurrentUser.accesstoken;
                user.ID = Convert.ToInt32(ID);
                string url = utilityRespository.getBaseUrl() + "Admin/GetEmployeeByID";
                string strResponse = HttpApi.CreateRequest(url, user);
                employeesData = JsonConvert.DeserializeObject<GetEmployeeForm>(strResponse);

                //employeesData.EmpData.JoiningDate = null;
                if (employeesData.EmpData == null)
                {
                    employeesData.EmpData = new EmployeeForm();
                    employeesData.EmpData.Status = employeesData.status;
                    employeesData.EmpData.Message = employeesData.message;
                }
                else
                {
                    employeesData.EmpData.Status = employeesData.status;
                    employeesData.EmpData.Message = employeesData.message;
                }
                employeesData.EmpData.RoleList = RoleList();
                return PartialView("_EmployeeForm", employeesData.EmpData);
                //var emp_model = PartialView("_EmployeeForm", employeesData.EmpData);
                //return Json(new { status = employeesData.status, message = employeesData.message, html = emp_model }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                employeesData.EmpData = new EmployeeForm();
                employeesData.EmpData.Status = false;
                employeesData.EmpData.Message = ex.Message;
                return PartialView("_EmployeeForm", employeesData.EmpData);
                //return Json(new { status = false, message = ex.Message}, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult DeleteEmployeeByID(int ID)
        {
            try
            {
                if (ID != 0)
                {
                    //vt_UserProfile user = new vt_UserProfile();
                    GetbyID user = new GetbyID();
                    user.token = CurrentUser.accesstoken;

                    user.ID = Convert.ToInt32(ID);
                    string url = utilityRespository.getBaseUrl() + "Admin/DeleteEmployeeByID";
                    string strResponse = HttpApi.CreateRequest(url, user);
                    HeaderResponse Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                    return Json(new { status = Response.status, message = Response.message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Employee ID is Null." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ChangePasswordForm()
        {
            try
            {
                return PartialView("_ChangePasswordForm", new EmployeeForm());
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChangePassword(string new_password)
        {
            try
            {
                ChangePassword _changePassword = new ChangePassword();
                _changePassword.NewPassword = new_password;
                _changePassword.EmpID = CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Manager/UpdatePassword";
                string strResponse = HttpApi.CreateRequest(url, _changePassword);
                HeaderResponse UpdatedPass = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);
                if (UpdatedPass.status)
                {
                    return RedirectToAction("successMessage", "Manager");
                }
                else
                {
                    return RedirectToAction("errorMessage", "Manager");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("errorMessage", "Manager");
            }
        }
        public ActionResult DecryptPasswordForChange(string EncryptPassword)
        {
            string currentUserPassword = string.Empty;

            try
            {
                currentUserPassword = vt_Common.DecryptPassword(EncryptPassword);

                return Json(new { currentUserPassword }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { currentUserPassword }, JsonRequestBehavior.AllowGet);
                //return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetAllQuotesReportManager()
        {
            string jsondata = string.Empty;
            GetQoutesDataListManager response = new GetQoutesDataListManager();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                request.UserID = CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Manager/GetAllQuotesforReportManager";
                string strresponse = HttpApi.CreateRequest(url, request);
                response = JsonConvert.DeserializeObject<GetQoutesDataListManager>(strresponse);
                jsondata = JsonConvert.SerializeObject(response, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.status = false;
                jsondata = JsonConvert.SerializeObject(response, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}