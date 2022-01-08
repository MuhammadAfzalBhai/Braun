using BAL.Repository;
using Braunability_ViewModal.Model;
using BraunApp_ViewModel.Model;
using DAL.DBEntities;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Braunability_ViewModal.Model.BraunVM_Request;
using static Braunability_ViewModal.Model.BraunVM_Response;

namespace Bruneability_Portal.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        #region Dashboard Graph

        [HttpGet]
        public ActionResult QuoteslastSixMonth()
        {
            GetDashboard reponse = new GetDashboard();

            string jsondata = string.Empty;
           
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/QuoteslastSixMonth";
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
                //return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public ActionResult GetDashboardcounterResult()
        {
            GetDahboardCounterValues reponse = new GetDahboardCounterValues();

            string jsondata = string.Empty;

            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/GetDahboardCounterValues";
                string strResponse = HttpApi.CreateRequest(url, request);
                reponse = JsonConvert.DeserializeObject<GetDahboardCounterValues>(strResponse);

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
        #endregion

        #region User Management

        public ActionResult UserManagement()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<JsonResult> GetEmployeeData()
        //{
        //    try
        //    {
        //        string url = utilityRespository.getBaseUrl() + "Admin/GetAllEmployees";
        //        Task<string> strResponse = HttpApi.AsyncCreateRequest(url);
        //        DataTable employeesData = JsonConvert.DeserializeObject<DataTable>(await strResponse);

        //        string jsondata = string.Empty;
        //        jsondata = JsonConvert.SerializeObject(employeesData, Formatting.Indented);
        //        return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch(Exception ex)
        //    {
        //        return Json(new { status = false, message = ex.Message });
        //    }

        //}


        public ActionResult CheckRequest()
        {
            bool isexit = false;
            GetEmployeeDataList requestlist = new GetEmployeeDataList();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/GetAllEmployeesRequest";
                string strResponse = HttpApi.CreateRequest(url, request);               
                requestlist = JsonConvert.DeserializeObject<GetEmployeeDataList>(strResponse);
                if (requestlist.EmployeeList.Rows.Count>0)
                {
                    isexit = true;

                }
                return Json(new { isexit },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                isexit = true;

                return Json(new { isexit }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EmployeesRequest()
        {
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/GetAllEmployeesRequest";
                string strResponse = HttpApi.CreateRequest(url, request);
                GetEmployeeDataList requestlist = JsonConvert.DeserializeObject<GetEmployeeDataList>(strResponse);

                return PartialView("_Requests", requestlist.EmployeeList);
            }
            catch (Exception ex)
            {
                return PartialView("_Requests", new DataTable());
            }
        }

        public ActionResult ApproveEmpByID(int ID)
        {
            string jsondata = string.Empty;
            GetEmployeeDataList employeesData = new GetEmployeeDataList();
            try
            {
                GetbyID user = new GetbyID();
                user.token = CurrentUser.accesstoken;
                user.ID = Convert.ToInt32(ID);
                string url = utilityRespository.getBaseUrl() + "Admin/AcceptEmployeeByID";
                string strResponse = HttpApi.CreateRequest(url, user);
                employeesData = JsonConvert.DeserializeObject<GetEmployeeDataList>(strResponse);

                //return PartialView("_Requests", employeesData.EmployeeList);
                jsondata = JsonConvert.SerializeObject(employeesData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                employeesData.status = false;
                employeesData.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(employeesData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult RejectEmpByID(int ID)
        {
            string jsondata = string.Empty;
            GetEmployeeDataList employeesData = new GetEmployeeDataList();
            try
            {
                GetbyID user = new GetbyID();
                user.token = CurrentUser.accesstoken;
                user.ID = Convert.ToInt32(ID);
                string url = utilityRespository.getBaseUrl() + "Admin/RejectEmployeeByID";
                string strResponse = HttpApi.CreateRequest(url, user);
                employeesData = JsonConvert.DeserializeObject<GetEmployeeDataList>(strResponse);

                //return PartialView("_Requests", employeesData.EmployeeList);
                jsondata = JsonConvert.SerializeObject(employeesData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                employeesData.status = false;
                employeesData.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(employeesData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetAllEmployeeData()
        {
            string jsondata = string.Empty;
            GetEmployeeDataList employeesData = new GetEmployeeDataList();

            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                //var token = CurrentUser.accesstoken;
                //var validtoken = utilityRespository.ValidationAccesstoken(token);

                //if (validtoken)
                //{
                    string url = utilityRespository.getBaseUrl() + "Admin/GetAllEmployees";
                    string strResponse = HttpApi.CreateRequest(url,request);
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

        public ActionResult NewEmployeeForm()
        {
            try
            {
                
                return PartialView("_EmployeeForm", new EmployeeForm());
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddUpdateEmployee(EmployeeForm emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (emp != null)
                    {
                        emp.IsApproved = true;
                        emp.IsActive = true;
                        emp.token = CurrentUser.accesstoken;
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

        #endregion

        #region Conversion Details

        public ActionResult ConversionDetails()
        {
            return View();
        }

        public JsonResult AllConversionDetails()
        {
            string jsondata = string.Empty;
            GetConversionDataList conversionData = new GetConversionDataList();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/GetAllConversions";
                string strResponse = HttpApi.CreateRequest(url,request);
                conversionData = JsonConvert.DeserializeObject<GetConversionDataList>(strResponse);

                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                conversionData.status = false;
                conversionData.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
                //return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetConversionByID(int ID)
        {
            GetConversionForm conversionData = new GetConversionForm();
            try
            {
                //vt_UserProfile user = new vt_UserProfile();
                GetbyID conversion = new GetbyID();
                conversion.token = CurrentUser.accesstoken;

                conversion.ID = Convert.ToInt32(ID);
                string url = utilityRespository.getBaseUrl() + "Admin/GetConversionByID";
                string strResponse = HttpApi.CreateRequest(url, conversion);
                conversionData = JsonConvert.DeserializeObject<GetConversionForm>(strResponse);

                if (conversionData.ConversionData == null)
                {
                    conversionData.ConversionData = new ConversionDetailForm();
                    conversionData.ConversionData.Status = conversionData.status;
                    conversionData.ConversionData.Message = conversionData.message;
                }
                else
                {
                    conversionData.ConversionData.Status = conversionData.status;
                    conversionData.ConversionData.Message = conversionData.message;
                }

                //employeesData.EmpData.JoiningDate = null;

                return PartialView("_ConversionForm", conversionData.ConversionData);
            }
            catch (Exception ex)
            {
                conversionData.ConversionData = new ConversionDetailForm();
                conversionData.ConversionData.Status = false;
                conversionData.ConversionData.Message = ex.Message;

                return PartialView("_ConversionForm", conversionData.ConversionData);
                //return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult NewConversionForm()
        {
            try
            {
                return PartialView("_ConversionForm", new ConversionDetailForm());
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddUpdateConversion(ConversionDetailForm conversion)
        {
            try
            {
                
                      conversion.token = CurrentUser.accesstoken;
                        string url = utilityRespository.getBaseUrl() + "Admin/AddUpdateConversion";
                        string strResponse = HttpApi.CreateRequest(url, conversion);
                        HeaderResponse empData = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                        //conversion.Status = empData.status;
                        //conversion.Message = empData.message;

                        return Json(new { status = empData.status, message = empData.message }, JsonRequestBehavior.AllowGet);
                        //return PartialView("_ConversionForm", conversion);
                   
                
                
            }
            catch (Exception ex)
            {
                //conversion.Status = false;
                //conversion.Message = ex.Message;
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                //return PartialView("_ConversionForm", conversion);
            }
        }


        public ActionResult DeleteConversionByID(int ID)
        {
            try
            {
                if (ID != 0)
                {
                    //vt_UserProfile user = new vt_UserProfile();
                    GetbyID conversion = new GetbyID();
                    conversion.token = CurrentUser.accesstoken;
                    conversion.ID = Convert.ToInt32(ID);
                    string url = utilityRespository.getBaseUrl() + "Admin/DeleteConversionByID";
                    string strResponse = HttpApi.CreateRequest(url, conversion);
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
        #endregion

        #region Conversion Depreciation

        public ActionResult ConversionDepreciation()
        {
            return View();
        }

        public JsonResult GetAllConversionDepriciation()
        {
            string jsondata = string.Empty;
            GetDepriciationDataList depreciationData = new GetDepriciationDataList();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/GetAllConversionDepriciation";
                string strResponse = HttpApi.CreateRequest(url, request);
                depreciationData = JsonConvert.DeserializeObject<GetDepriciationDataList>(strResponse);

                jsondata = JsonConvert.SerializeObject(depreciationData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                depreciationData.status = false;
                depreciationData.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(depreciationData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
                //return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDepreciationByID(int ID)
        {
            GetDepreciationForm depreciationData = new GetDepreciationForm();
            try
            {
                //vt_Depreciation dep = new vt_Depreciation();
                GetbyID dep = new GetbyID();

                dep.token = CurrentUser.accesstoken;
                dep.ID = Convert.ToInt32(ID);
                string url = utilityRespository.getBaseUrl() + "Admin/GetDepreciationByID";
                string strResponse = HttpApi.CreateRequest(url, dep);
                depreciationData = JsonConvert.DeserializeObject<GetDepreciationForm>(strResponse);

                if (depreciationData.DepreciationData == null)
                {
                    depreciationData.DepreciationData = new DeprecaitionForm();
                    depreciationData.DepreciationData.Status = depreciationData.status;
                    depreciationData.DepreciationData.Message = depreciationData.message;
                }
                else
                {
                    depreciationData.DepreciationData.Status = depreciationData.status;
                    depreciationData.DepreciationData.Message = depreciationData.message;
                }

                //employeesData.EmpData.JoiningDate = null;
                return PartialView("_DepreciationForm", depreciationData.DepreciationData);
            }
            catch (Exception ex)
            {
                depreciationData.DepreciationData = new DeprecaitionForm();
                depreciationData.DepreciationData.Status =false;
                depreciationData.DepreciationData.Message = ex.Message;
                return PartialView("_DepreciationForm", depreciationData.DepreciationData);
                //return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult NewDeprecaitionForm()
        {
            try
            {
                return PartialView("_DepreciationForm", new DeprecaitionForm());
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddUpdateDeprecaition(DeprecaitionForm dep)
        {
            try
            {
                if (dep != null)
                {
                    dep.token = CurrentUser.accesstoken;
                    string url = utilityRespository.getBaseUrl() + "Admin/AddUpdateDepreciation";
                    string strResponse = HttpApi.CreateRequest(url, dep);
                    HeaderResponse depData = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                    return Json(new { status = depData.status, message = depData.message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Parameters are Null." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteDepreciationByID(int ID)
        {
            try
            {
                if (ID != 0)
                {
                    //vt_UserProfile user = new vt_UserProfile();
                    GetbyID depreciation = new GetbyID();
                    depreciation.token = CurrentUser.accesstoken;

                    depreciation.ID = Convert.ToInt32(ID);
                    string url = utilityRespository.getBaseUrl() + "Admin/DeleteDepreciationByID";
                    string strResponse = HttpApi.CreateRequest(url, depreciation);
                    HeaderResponse Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                    return Json(new { status = Response.status, message = Response.message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Depreciation ID is Null." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

        #region Conversion MarkUp

        public ActionResult ConversionMarkup()
        {
            return View();
        }

        public JsonResult GetMarkUpPercent()
        {
            try
            {
                string url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
                string strResponse = HttpApi.CreateRequest(url);
                GetMarkUpDataPercent markupData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

                string jsondata = string.Empty;
                jsondata = JsonConvert.SerializeObject(markupData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMarkUpFixed()
        {
            try
            {
                string url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
                string strResponse = HttpApi.CreateRequest(url);
                GetMarkUpDataFixed markupData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

                string jsondata = string.Empty;
                jsondata = JsonConvert.SerializeObject(markupData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult UpdateMarkUpPercent(MarkUpFormPercent markup)
        {
            try
            {
                MarkUpFormPercent mark = new MarkUpFormPercent();
                mark.ID = markup.ID;
                mark.MarkUpPercent = markup.MarkUpPercent.Trim().Replace("%","").Replace(" ","");
                mark.UserID = CurrentUser.SessionUser.ID;

                string url = utilityRespository.getBaseUrl() + "Admin/UpdateMarkUpPercent";
                string strResponse = HttpApi.CreateRequest(url, mark);
                HeaderResponse markupData = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                return Json(new { status = markupData.status , message = markupData.message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateMarkUpFixed(MarkUpFormFixed markup)
        {
            try
            {
                MarkUpFormFixed mark = new MarkUpFormFixed();
                mark.ID = markup.ID;
                mark.MarkUpFixed= markup.MarkUpFixed.Trim().Replace("$", "").Replace(" ", "");
                mark.UserID = CurrentUser.SessionUser.ID;

                string url = utilityRespository.getBaseUrl() + "Admin/UpdateMarkUpFixed";
                string strResponse = HttpApi.CreateRequest(url, mark);
                HeaderResponse markupData = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                return Json(new { status = markupData.status, message = markupData.message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region Conversion Deduction

        public ActionResult ConversionDeduction()
        {
            return View();
        }

        public JsonResult GetAllConversionDeduction()
        {
            string jsondata = string.Empty;
            GetDeductionDataList deductionData = new GetDeductionDataList();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/GetAllConversionDeduction";
                string strResponse = HttpApi.CreateRequest(url,request);
                deductionData = JsonConvert.DeserializeObject<GetDeductionDataList>(strResponse);

                jsondata = string.Empty;
                jsondata = JsonConvert.SerializeObject(deductionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDeductionByID(int ID)
        {
            GetDeductionForm deductionData = new GetDeductionForm();
            try
            {
                //vt_Deductions deduct = new vt_Deductions();
                GetbyID deduct = new GetbyID();

                deduct.token = CurrentUser.accesstoken;
                deduct.ID = Convert.ToInt32(ID);
                string url = utilityRespository.getBaseUrl() + "Admin/GetDeductionByID";
                string strResponse = HttpApi.CreateRequest(url, deduct);
                deductionData = JsonConvert.DeserializeObject<GetDeductionForm>(strResponse);

                if (deductionData.DeductionData == null)
                {
                    deductionData.DeductionData = new DeductionForm();
                    deductionData.DeductionData.Status = deductionData.status;
                    deductionData.DeductionData.Message = deductionData.message;
                }
                else
                {
                    deductionData.DeductionData.Status = deductionData.status;
                    deductionData.DeductionData.Message = deductionData.message;
                }

                return PartialView("_DeductionForm", deductionData.DeductionData);
            }
            catch (Exception ex)
            {
                deductionData.DeductionData = new DeductionForm();
                deductionData.DeductionData.Status = false;
                deductionData.DeductionData.Message = ex.Message;
                return PartialView("_DeductionForm", deductionData.DeductionData);
                //return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult NewDeductionForm()
        {
            try
            {
                return PartialView("_DeductionForm", new DeductionForm());
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddUpdateDeduction(DeductionForm deduct)
        {
            try
            {
                if (deduct != null)
                {
                    deduct.token = CurrentUser.accesstoken;
                    string url = utilityRespository.getBaseUrl() + "Admin/AddUpdateDeduction";
                    string strResponse = HttpApi.CreateRequest(url, deduct);
                    HeaderResponse depData = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                    return Json(new { status = depData.status, message = depData.message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Parameters are Null." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteDeductionByID(int ID)
        {
            try
            {
                if (ID != 0)
                {
                    //vt_Deductions deduct = new vt_Deductions();
                    GetbyID deduct = new GetbyID();
                    deduct.token = CurrentUser.accesstoken;
                    deduct.ID = Convert.ToInt32(ID);
                    string url = utilityRespository.getBaseUrl() + "Admin/DeleteDeductionByID";
                    string strResponse = HttpApi.CreateRequest(url, deduct);
                    HeaderResponse Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                    return Json(new { status = Response.status, message = Response.message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Deduction ID is Null." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

        #region Makes

        private NadaLogin.SecureLoginSoapClient myLogin = new NadaLogin.SecureLoginSoapClient();
        private VehicleService.VehicleSoapClient Vechile = new VehicleService.VehicleSoapClient();

        public ActionResult Makes()
        {
            return View();
        }


        private string GetLoginToken(string UserID, string Password)
        {
            NadaLogin.GetTokenRequest myRequest = new NadaLogin.GetTokenRequest();
            myRequest.Username = UserID;
            myRequest.Password = Password;

            return myLogin.getToken(myRequest);
        }

        public JsonResult GetAllMakesbyNADA(int Year)
        {
            try
            {
                string token = GetLoginToken("braun_631", "braun_631@");
                VehicleService.GetMakesRequest req = new VehicleService.GetMakesRequest();
                var make = new VehicleService.GetMakesRequest();
                make.Period = 0;
                make.Year = Year;
                make.Token = token;
                make.VehicleType = VehicleService.VehicleTypes.UsedCar;
                req.VehicleType = VehicleService.VehicleTypes.UsedCar;
                req.Year = Year;
                req.Token = token;
                var carsMake = Vechile.getMakes(req);

                var result = new
                {
                    status = true,
                    message = "Successfully get Car Make List.",
                    CarsMake = carsMake,
                    Token = token
                };


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = false,
                    message = ex.Message,
                    CarsMake = "",
                    Token = ""
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

        #region Reports

        public ActionResult Reports()
        {
            return View();
        }

        //GetAllQuotesforReport
        [HttpGet]
        public ActionResult GetAllQuotesReport()
        {
            string jsondata = string.Empty;
            GetQoutesDataList response = new GetQoutesDataList();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/GetAllQuotesforReport";
                string strresponse = HttpApi.CreateRequest(url, request);
                response = JsonConvert.DeserializeObject<GetQoutesDataList>(strresponse);
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

        [HttpPost]
        public ActionResult GetQuotesBetweenTwoDates(string From ,string To)
        {
            string jsondata = string.Empty;
            GetQoutesDataList response = new GetQoutesDataList();
            try
            {
                Betweentwodates request = new Betweentwodates();
                request.start = From;
                request.end = To;
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Admin/GetQuotesBetweenTwoDates";
                string strresponse = HttpApi.CreateRequest(url, request);
                response = JsonConvert.DeserializeObject<GetQoutesDataList>(strresponse);
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

        #endregion

        #region Upload Excel


        [HttpPost]
        public ActionResult UploadExcelConversion(FormCollection form)
        {
            try
            {
                if (Request != null)
                {
                    DataTable dt = new DataTable();
                    IWorkbook workbook = null;
                    HttpPostedFileBase files = Request.Files["excelFile"];
                    if ((files != null) && (files.ContentLength > 0) && !string.IsNullOrEmpty(files.FileName))
                    {
                        string extension = string.Empty;
                        string filename = files.FileName;
                        string[] obj = filename.Split('.');
                        filename = obj[0];
                        extension = obj[1];
                        //Stream uploadFileStream = FileUpload1.PostedFile.InputStream;
                        //HttpPostedFile file = Request.files["excelFile"];
                        MemoryStream mem = new MemoryStream();
                        mem.SetLength((int)files.ContentLength);
                        files.InputStream.Read(mem.GetBuffer(), 0, (int)files.ContentLength);
                        //using (MemoryStream file= new MemoryStream())
                        //{
                        if (extension == "xlsx")
                        {
                            workbook = new XSSFWorkbook(mem);
                        }
                        else if (extension == "xls")
                        {
                            workbook = new XSSFWorkbook(mem);
                        }
                        else
                        {
                            return Json(new { status = false, message = "This format is not supported." }, JsonRequestBehavior.AllowGet);
                        }
                        //throw new Exception("This format is not supported");
                        
                    }
                    //}
                    //IWorkbook workbook = WorkbookFactory.Create(uploadFileStream);
                    ISheet sheet = workbook.GetSheetAt(0);
                    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    int rowCount = headerRow.RowNum;

                    //for (int j = 0; j < cellCount ; j++)
                    //{
                    //    ICell cell = headerRow.GetCell(j);
                    //    dt.Columns.Add(cell.ToString(),typeof(string));


                    //}
                    dt.Columns.Add("ConversionName", typeof(string));
                    dt.Columns.Add("RetailPrice", typeof(decimal));     
                    dt.Columns.Add("DealerPrice", typeof(decimal));
                    dt.Columns.Add("VisualCode", typeof(string));
                    dt.Columns.Add("EpicorCode", typeof(string));
                    dt.Columns.Add("Make", typeof(string));
                    dt.Columns.Add("MakeID", typeof(int));
                    dt.Columns.Add("CreatedAt",typeof(DateTime));
                    dt.Columns.Add("IsActive",typeof(bool));

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dataRow = dt.NewRow();
                        if (row == null)
                        {
                            break;
                        }

                        for (int j = row.FirstCellNum; j < (row.LastCellNum + 2); j++)
                        {
                            if (row.GetCell(j) == null)
                            {
                                if (j == 1 || j == 2)
                                {
                                    dataRow[j] = 0.0;
                                }
                                if (j == 7) {
                                    dataRow[j] = DateTime.Now;
                                }
                                    
                                else
                                {
                                    if (j == 8)
                                        dataRow[j] = true;
                                    else
                                        dataRow[j] = "";
                                }
                            }
                            else
                            {
                                if (j == 1 || j==2) {
                                    dataRow[j] = Convert.ToDecimal(row.GetCell(j).ToString());
                                }
                                else if (j == 6)
                                {
                                    dataRow[j] = Convert.ToInt32(row.GetCell(j).ToString());
                                }
                                                                
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                                //dataRow[j] = row.GetCell(j).ToString();
                            }
                                
                            //if(j == 5)

                            //dataRow[j] = row.GetCell(j).ToString();
                        }

                        dt.Rows.Add(dataRow);
                        dt.CaseSensitive = false;

                    }

                    string sqlConnectionString = ConfigurationManager.ConnectionStrings["vt_BraunAppEntities"].ToString();
                    using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
                    {
                        bcp.BatchSize = 100000;
                        bcp.DestinationTableName = "vt_Conversions";   //Name of Table

                        //Mapping of table columns
                        bcp.ColumnMappings.Add("ConversionName", "ConversionName");
                        bcp.ColumnMappings.Add("RetailPrice", "RetailPrice");
                        bcp.ColumnMappings.Add("DealerPrice", "DealerPrice");
                        bcp.ColumnMappings.Add("VisualCode", "VisualCode");
                        bcp.ColumnMappings.Add("EpicorCode", "EpicorCode");
                        bcp.ColumnMappings.Add("Make", "Make");
                        bcp.ColumnMappings.Add("MakeID", "MakeID");
                        bcp.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        bcp.ColumnMappings.Add("IsActive", "IsActive");

                        bcp.BulkCopyTimeout = 0;

                        bcp.WriteToServer(dt);
                        bcp.Close();


                    }
                    return Json(new { status = true, message = "Successfully Uploaded the Conversions." }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { status = false, message = "Please upload the file." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UploadExcelDepreciation(FormCollection form)
        {
            try
            {
                if (Request != null)
                {
                    DataTable dt = new DataTable();
                    IWorkbook workbook = null;
                    HttpPostedFileBase files = Request.Files["excelFile"];
                    if ((files != null) && (files.ContentLength > 0) && !string.IsNullOrEmpty(files.FileName))
                    {
                        string extension = string.Empty;
                        string filename = files.FileName;
                        string[] obj = filename.Split('.');
                        filename = obj[0];
                        extension = obj[1];
                        //Stream uploadFileStream = FileUpload1.PostedFile.InputStream;
                        //HttpPostedFile file = Request.files["excelFile"];
                        MemoryStream mem = new MemoryStream();
                        mem.SetLength((int)files.ContentLength);
                        files.InputStream.Read(mem.GetBuffer(), 0, (int)files.ContentLength);
                        //using (MemoryStream file= new MemoryStream())
                        //{
                        if (extension == "xlsx")
                        {
                            workbook = new XSSFWorkbook(mem);
                        }
                        else if (extension == "xls")
                        {
                            workbook = new XSSFWorkbook(mem);
                        }
                        else
                        {
                            return Json(new { status = false, message = "This format is not supported." }, JsonRequestBehavior.AllowGet);
                        }
                        //throw new Exception("This format is not supported");

                    }
                    //}
                    //IWorkbook workbook = WorkbookFactory.Create(uploadFileStream);
                    ISheet sheet = workbook.GetSheetAt(0);
                    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    int rowCount = headerRow.RowNum;

                    for (int j = 0; j < cellCount; j++)
                    {
                        ICell cell = headerRow.GetCell(j);
                        dt.Columns.Add(cell.ToString());
                    }

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dataRow = dt.NewRow();
                        if (row == null)
                        {
                            break;
                        }

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) == null)
                                dataRow[j] = "";
                            else
                                dataRow[j] = row.GetCell(j).ToString();
                        }

                        dt.Rows.Add(dataRow);
                        dt.CaseSensitive = false;

                    }

                    string sqlConnectionString = ConfigurationManager.ConnectionStrings["vt_BraunAppEntities"].ToString();
                    using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
                    {
                        bcp.BatchSize = 100000;
                        bcp.DestinationTableName = "vt_Depreciation";   //Name of Table

                        //Mapping of table columns
                        bcp.ColumnMappings.Add("AgeInMonths", "AgeInMonths");
                        bcp.ColumnMappings.Add("DepreciationPercent", "DepreciationPercent");
                        bcp.ColumnMappings.Add(DateTime.Now.ToString(), "CreatedAt");
                        bcp.ColumnMappings.Add("true", "IsActive");
                        bcp.BulkCopyTimeout = 0;

                        bcp.WriteToServer(dt);
                        bcp.Close();


                    }
                    return Json(new { status = true, message = "Successfully Uploaded the Conversions." }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { status = false, message = "Please upload the file." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region Exsisting Values

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckExistingEmail(string Email)
        {
            try
            {
                string url = utilityRespository.getBaseUrl() + "Admin/CheckExistingEmail";
                string strResponse = HttpApi.CreateRequest(url, Email);
                HeaderResponse existingData = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

                return Json(existingData.status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

      
        public ActionResult successMessage()
        {
            return View();
        }

        
        public ActionResult errorMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string new_password)
        {
            try
            {
                ChangePassword _changePassword = new ChangePassword();
                _changePassword.NewPassword = new_password;
                _changePassword.EmpID = CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Admin/UpdatePassword";
                string strResponse = HttpApi.CreateRequest(url, _changePassword);
                HeaderResponse UpdatedPass = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);
                if (UpdatedPass.status)
                {
                    return RedirectToAction("successMessage", "Admin");
                }
                else
                {
                    return RedirectToAction("errorMessage", "Admin");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("errorMessage", "Admin");
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
        [HttpPost]
        public ActionResult DecryptPasswordForChange (string EncryptPassword)
        {
            string currentUserPassword = string.Empty;
            
            try
            {
                currentUserPassword= vt_Common.DecryptPassword(EncryptPassword);
                
                return Json(new { currentUserPassword }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return Json(new { currentUserPassword }, JsonRequestBehavior.AllowGet);
                //return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}