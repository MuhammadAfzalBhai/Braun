using BAL.Repository;
using Braunability_ViewModal.Model;
using BraunApp_ViewModel.Model;
using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Braunability_ViewModal.Model.BraunVM_Request;
using static Braunability_ViewModal.Model.BraunVM_Response;

namespace Bruneability_API.Controllers
{
    public class AdminController : ApiController
    {
        AdminRepository AdminRepo;
        public AdminController()
        {
            AdminRepo = new AdminRepository(new vt_BraunAppEntities());
        }
        [Route("api/Admin/GetDahboardCounterValues")]        [HttpPost]        public GetDahboardCounterValues GetTopFiveMake(HeaderToken header)        {            GetDahboardCounterValues response = new GetDahboardCounterValues();            try            {                var validtoken = utilityRespository.ValidationAccesstoken(header.token);                if (validtoken)                {                    response = AdminRepo.GetDashboardCounterResult();                }            }            catch (Exception ex)            {                response.status = false;                response.message = "Result Not Found.";            }            return response;        }

        #region Dashboard Graph

        [Route("api/Admin/QuoteslastSixMonth")]
        [HttpPost]
        public GetDashboard QuoteslastSixMonth(HeaderToken header)
        {
            GetDashboard reponse = new GetDashboard();
            try
            {
                List<QuotesListMonths> _listofQuotesListMonths = new List<QuotesListMonths>();
                List<TopfiveMake> _ListTopfiveMake = new List<TopfiveMake>();
                List<TopfiveModel> _ListTopfiveModel = new List<TopfiveModel>();
                List<TopfiveConversions> _ListTopfiveConversions = new List<TopfiveConversions>();
                  
               var validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (validtoken)
                {
                    _listofQuotesListMonths = AdminRepo.GetQuoetsoflastsixmonths();
                    _ListTopfiveMake = AdminRepo.GetListTopfiveMake();
                    _ListTopfiveModel = AdminRepo.GetListTopfiveModel();
                    _ListTopfiveConversions = AdminRepo.GetListTopfiveConversions();
                    if (_listofQuotesListMonths.Count > 0)
                    {
                        reponse.status = true;
                        reponse.message = "Quotes Found.";
                        reponse.QuotesListLastSixMonth = new listofQuotesListMonths(); 
                        reponse.QuotesListLastSixMonth.status = true;
                        reponse.QuotesListLastSixMonth.message = "Six Month Quotes Found.";
                        reponse.QuotesListLastSixMonth.QuotesListLastSixMonth = _listofQuotesListMonths;

                    }
                    else
                    {
                        reponse.QuotesListLastSixMonth = new listofQuotesListMonths();
                        reponse.QuotesListLastSixMonth.status = false;
                        reponse.QuotesListLastSixMonth.message = "Six Month Quotes Not  Found.";
                        reponse.QuotesListLastSixMonth.QuotesListLastSixMonth = null;

                        
                    }

                    if (_ListTopfiveMake.Count > 0)
                    {
                        reponse.status = true;
                        reponse.message = "Quotes Found.";
                        reponse.ListofTopfiveMake = new ListTopfiveMake();
                        reponse.ListofTopfiveMake.status = true;
                        reponse.ListofTopfiveMake.message = "Top Five Make Found.";
                        reponse.ListofTopfiveMake.ListofTopfiveMake = _ListTopfiveMake;

                    }
                    else
                    {
                        reponse.ListofTopfiveMake = new ListTopfiveMake();
                        reponse.ListofTopfiveMake.status = false;
                        reponse.ListofTopfiveMake.message = "Top Five Make not Found.";
                        reponse.ListofTopfiveMake.ListofTopfiveMake = null;


                    }

                    if (_ListTopfiveModel.Count > 0)
                    {
                        reponse.status = true;
                        reponse.message = "Quotes Found.";
                        reponse.ListofTopfiveModel = new ListTopfiveModel();
                        reponse.ListofTopfiveModel.status = true;
                        reponse.ListofTopfiveModel.message = "Top Five Model Found.";
                        reponse.ListofTopfiveModel.ListofTopfiveModel = _ListTopfiveModel;

                    }
                    else
                    {
                        reponse.ListofTopfiveModel = new ListTopfiveModel();
                        reponse.ListofTopfiveModel.status = false;
                        reponse.ListofTopfiveModel.message = "Top Five Mdoel not Found.";
                        reponse.ListofTopfiveModel.ListofTopfiveModel = null;


                    }

                    if (_ListTopfiveConversions.Count > 0)
                    {
                        reponse.status = true;
                        reponse.message = "Quotes Found.";
                        reponse.ListofTopfiveConversions = new ListTopfiveConversions();
                        reponse.ListofTopfiveConversions.status = true;
                        reponse.ListofTopfiveConversions.message = "Top Five Model Found.";
                        reponse.ListofTopfiveConversions.ListofTopfiveConversions = _ListTopfiveConversions;

                    }
                    else
                    {
                        reponse.ListofTopfiveConversions = new ListTopfiveConversions();
                        reponse.ListofTopfiveConversions.status = false;
                        reponse.ListofTopfiveConversions.message = "Top Five Mdoel not Found.";
                        reponse.ListofTopfiveConversions.ListofTopfiveConversions = null;


                    }




                }
            }
            catch (Exception ex)
            {
                reponse.status = false;
                reponse.message = "Quotes Not Found.";
                reponse.QuotesListLastSixMonth = null;
                reponse.ListofTopfiveMake = null;
                reponse.ListofTopfiveModel = null;
                reponse.ListofTopfiveConversions = null;

            }

            return reponse;
        }
        

        #endregion

        #region Report's
        [Route("api/Admin/GetAllQuotesforReport")]
        [HttpPost]
        public GetQoutesDataList GetAllQuotesforReport(HeaderToken header)
        {
            GetQoutesDataList obj_getquotesdatalist = new GetQoutesDataList();
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (validtoken)
                {
                    obj_getquotesdatalist.QoutesList = AdminRepo.GetAllQoutesforReport();
                    obj_getquotesdatalist.status = true;
                    obj_getquotesdatalist.message = "Record Found.";

                }
                else
                {
                    obj_getquotesdatalist.status = false;
                    obj_getquotesdatalist.message = "Invalid Token.";
                    obj_getquotesdatalist.QoutesList = null;


                }
            }
            catch (Exception)
            {
                obj_getquotesdatalist.status = false;
                obj_getquotesdatalist.message = "Record Not Found.";
                obj_getquotesdatalist.QoutesList = null;

            }

            return obj_getquotesdatalist;
        }

        [Route("api/Admin/GetQuotesBetweenTwoDates")]
        [HttpPost]
        public GetQoutesDataList GetQuotesBetweenTwoDates(Betweentwodates header)
        {
            GetQoutesDataList obj_getquotesdatalist = new GetQoutesDataList();
            try
            {
                var _end = header.end;
                var _start = header.start;
                if (_end.Contains('?'))
                {
                    _end = _end.Replace("?", "");

                }
                if (_start.Contains('?'))
                {
                    _start = _start.Replace("?", "");
                }
               
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (validtoken)
                {
                   // var a= header.start.Trim(new Char[] { '?','?','?','?','?'});

                    obj_getquotesdatalist.QoutesList = AdminRepo.GetQoutesBetweentwoDates(_start, _end);
                    obj_getquotesdatalist.status = true;
                    obj_getquotesdatalist.message = "Record Found.";

                }
                else
                {
                    obj_getquotesdatalist.status = false;
                    obj_getquotesdatalist.message = "Invalid Token.";
                    obj_getquotesdatalist.QoutesList = null;


                }
            }
            catch (Exception)
            {
                obj_getquotesdatalist.status = false;
                obj_getquotesdatalist.message = "Record Not Found.";
                obj_getquotesdatalist.QoutesList = null;

            }

            return obj_getquotesdatalist;
        }
        #endregion

        #region User Management

        [Route("api/Admin/GetAllEmployeesRequest")]
        [HttpPost]
        public GetEmployeeDataList GetAllEmployeesRequest(HeaderToken header)
        {
            GetEmployeeDataList Response = new GetEmployeeDataList();
            vt_UserProfile EmpForm = new vt_UserProfile();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {


                    DataTable dt = AdminRepo.GetAllEmployeesRequest();

                    if (dt != null)
                    {
                        Response.EmployeeList = dt;
                        Response.status = true;
                        Response.message = "Record Found";
                    }
                    else
                    {
                        Response.EmployeeList = null;
                        Response.status = false;
                        Response.message = "Record Not Found";
                    }
                }
                else
                {
                    Response.EmployeeList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.EmployeeList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Admin/AcceptEmployeeByID")]
        [HttpPost]
        public GetEmployeeDataList AcceptEmployeeByID(GetbyID header)
        {
            GetEmployeeDataList Response = new GetEmployeeDataList();
            vt_UserProfile EmpForm = new vt_UserProfile();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {


                    DataTable dt1 = AdminRepo.AcceptEmployeesRequest(header.ID);
                    DataTable dt2 = AdminRepo.GetAllEmployeesRequest();


                    if (dt2 != null)
                    {
                        Response.EmployeeList = dt2;
                        Response.status = true;
                        Response.message = "Record Found";
                    }
                    else
                    {
                        Response.EmployeeList = null;
                        Response.status = false;
                        Response.message = "Record Not Found";
                    }
                }
                else
                {
                    Response.EmployeeList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.EmployeeList = null;
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/RejectEmployeeByID")]
        [HttpPost]
        public GetEmployeeDataList RejectEmployeeByID(GetbyID header)
        {
            GetEmployeeDataList Response = new GetEmployeeDataList();
            vt_UserProfile EmpForm = new vt_UserProfile();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {


                    DataTable dt1 = AdminRepo.RejectEmployeesRequest(header.ID);
                    DataTable dt2 = AdminRepo.GetAllEmployeesRequest();


                    if (dt2 != null)
                    {
                        Response.EmployeeList = dt2;
                        Response.status = true;
                        Response.message = "Record Found";
                    }
                    else
                    {
                        Response.EmployeeList = null;
                        Response.status = false;
                        Response.message = "Record Not Found";
                    }
                }
                else
                {
                    Response.EmployeeList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.EmployeeList = null;
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/GetAllEmployees")]
        [HttpPost]
        public GetEmployeeDataList GetAllEmployees(HeaderToken header)
        {
            GetEmployeeDataList Response = new GetEmployeeDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    if (header.roleID == 3)
                    {
                        Response.EmployeeList = AdminRepo.GetAllSalesRep(header.UserID);
                    }
                    else
                    {
                        Response.EmployeeList = AdminRepo.GetAllEmployees();
                    }
                    Response.status = true;
                    Response.message = "Record Found";
                }
                else
                {
                    Response.EmployeeList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.EmployeeList = null;
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/GetEmployeeByID")]
        [HttpPost]
        public GetEmployeeForm GetEmployeeByID(GetbyID User)
        {
            GetEmployeeForm Response = new GetEmployeeForm();

            try
            {
                EmployeeForm EmpForm = new EmployeeForm();
                var validtoken = utilityRespository.ValidationAccesstoken(User.token);

                if (validtoken)
                {
                    if (User.ID != 0)
                    {
                        DataTable userdt = AdminRepo.GetEmployeeData(User.ID);



                        if (userdt != null)
                        {
                            foreach (DataRow item in userdt.Rows)
                            {
                                EmpForm = new EmployeeForm();
                                EmpForm.ID = Convert.ToInt32(item[0]);
                                EmpForm.City = item["City"].ToString();
                                EmpForm.RoleID = Convert.ToInt32(item["RoleID"]);
                                EmpForm.State = item["State"].ToString();
                                EmpForm.CompanyName = item["CompanyName"].ToString();
                                EmpForm.UserName = item["UserName"].ToString();
                                EmpForm.Email = item["Email"].ToString();
                                EmpForm.Contact = item["Phone"].ToString();
                                EmpForm.JoiningDate = item["JoiningDate"].ToString();
                                EmpForm.Password = vt_Common.Decrypt(item["Password"].ToString());
                                EmpForm.Managerstatus = item["ManagerStatus"].ToString();
                            }

                            Response.EmpData = EmpForm;
                            Response.status = true;
                            Response.message = "Record Found.";

                        }
                        else
                        {
                            Response.EmpData = null;
                            Response.status = false;
                            Response.message = "Record Not Found.";

                        }


                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Employee ID is null.";
                        Response.EmpData = null;
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid Token.";
                    Response.EmpData = null;
                }



            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
                Response.EmpData = null;
            }

            return Response;
        }

        [Route("api/Admin/AddNewMake")]
        [HttpPost]
        public HeaderResponse AddNewMake(AddNewMake make)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                var data = AdminRepo.CreateMake(make);
                Response.status = true;
                Response.message = "Add Make Successfully";
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/EditMakes")]
        [HttpPost]
        public HeaderResponse EditMake(AddNewMake make)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                var data = AdminRepo.EditMake(make);
                Response.status = true;
                Response.message = "Edit Make Successfully";
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }


        [Route("api/Admin/GetMakesByID")]
        [HttpPost]
        public GetMakesForm GetMakesByID(GetMAKEbyID Make)
        {
            GetMakesForm Response = new GetMakesForm();

            try
            {
                AddNewMake MakeForm = new AddNewMake();
                if (Make.Id != 0)
                {
                    DataTable makedt = AdminRepo.GetMakeDataByID(Make.Id);
                    if (makedt != null)
                    {
                        foreach (DataRow item in makedt.Rows)
                        {
                            MakeForm = new AddNewMake();
                            MakeForm.makeID = Convert.ToInt32(item[0]);
                            MakeForm.Makes = item["Makes"].ToString();
                        }

                        Response.MakeData = MakeForm;
                        Response.status = true;
                        Response.message = "Record Found.";

                    }
                    else
                    {
                        Response.MakeData = null;
                        Response.status = false;
                        Response.message = "Record Not Found.";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Make ID is null.";
                    Response.MakeData = null;
                }
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
                Response.MakeData = null;
            }

            return Response;
        }

        [Route("api/Admin/DeleteMakeByID")]
        [HttpPost]
        public HeaderResponse DeleteMakeByID(GetMAKEbyID make)
        {
            HeaderResponse Response = new HeaderResponse();

            try
            {
                if (make.Id != 0)
                {
                    DataTable makedt = AdminRepo.DeleteMakeByID(make.Id);

                    Response.status = true;
                    Response.message = "Deleted Successfully.";
                }
                else
                {
                    Response.status = false;
                    Response.message = "Makes ID is Null.";
                }
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/GetMakesList")]
        [HttpPost]
        public GetMakesDataList GetMakesList()
        {
            GetMakesDataList Response = new GetMakesDataList();

            try
            {
                DataTable dt = AdminRepo.GetMakes();
                Response.MakesList = dt;
                Response.status = true;
                Response.message = "Record Found";
            }
            catch (Exception ex)
            {
                Response.MakesList = null;
                Response.status = false;
                Response.message = ex.Message;
            }
            return Response;
        }


        [Route("api/Admin/UpdateTiersDetails")]
        [HttpPost]
        public HeaderResponse UpdateTiersDetails(TiersAmount Emp)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
               DataTable emp_dt = AdminRepo.UpdateTierList(Emp);
               Response.status = true;
               Response.message = "Update Successfull";
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/AddUpdateEmployee")]
        [HttpPost]
        public HeaderResponse AddUpdateEmployee(EmployeeForm Emp)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(Emp.token);

                //if (validtoken)
                //{
                if (ModelState.IsValid)
                {
                    if (Emp.ID == 0)
                    {
                        var data = AdminRepo.ValidateEmployee(Emp.Email);

                        if (data != null)
                        {

                            Response.status = false;
                            Response.message = "Email already exsist.";
                        }
                        else
                        {
                            DataTable emp_dt = AdminRepo.CreateEmployee(Emp);

                            Response.status = true;
                            if (Emp.IsActive==true && Emp.IsApproved==true)
                            {
                                if(Emp.RoleID == 3 )
                                {
                                    Response.message = "Your Sales Representative account has been created successfully.";
                                }
                                else
                                {
                                    Response.message = "Your employee account has been created successfully.";
                                }
                                
                            }
                            else
                            {
                                Response.message = "Your account has been created successfully."+ "\n" + "Your request has been sent for approval.";
                                
                            }
                            
                        }


                    }
                    else
                    {
                        if (validtoken)
                        {
                            var data = AdminRepo.ValidateEmployeeforedit(Emp.Email, Emp.ID);

                            if (data != null)
                            {

                                Response.status = false;
                                Response.message = "Email already exsist.";
                            }
                            else
                            {
                                DataTable emp_dt = AdminRepo.UpdateEmployee(Emp);

                                Response.status = true;
                                if (Emp.RoleID == 3)
                                {
                                    Response.message = "Your Sales Representative account has been updated successfully.";
                                }
                                else
                                {
                                    Response.message = "Your employee account has been updated successfully.";
                                }
                                   
                            }
                            
                        }
                        else
                        {
                            Response.status = false;
                            Response.message = "Invalid Token";
                        }

                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Please fill the valid fields.";

                    //var prop_contact = ModelState["Contact"];
                    //var prop_JoiningDate = ModelState["JoiningDate"];
                    //var prop_Email = ModelState["Email"];

                    //if (prop_contact == null ||
                    //    (prop_contact != null && prop_contact.Errors.Any())
                    //   )
                    //{
                    //    Response.status = false;
                    //    Response.message = "Contact Number is Invalid";
                    //}

                    //else if (prop_JoiningDate == null ||
                    //   (prop_JoiningDate != null && prop_JoiningDate.Errors.Any())
                    //  )
                    //{
                    //    Response.status = false;
                    //    Response.message = "Joining Date is Invalid";
                    //}

                    //else if (prop_Email == null ||
                    //  (prop_Email != null && prop_Email.Errors.Any())
                    // )
                    //{
                    //    Response.status = false;
                    //    Response.message = "Email is Invalid";
                    //}
                }
                //}
                //else
                //{
                //    Response.status = false;
                //    Response.message = "Invalid Token.";
                //}


            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
                if(ex.HResult == -2146232060)
                {
                    Response.message = "Please Select Role";
                }
            }

            return Response;
        }


        [Route("api/Admin/DeleteEmployeeByID")]
        [HttpPost]
        public HeaderResponse DeleteEmployeeByID(GetbyID User)
        {
            HeaderResponse Response = new HeaderResponse();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(User.token);

                if (validtoken)
                {
                    if (User.ID != 0)
                    {
                        DataTable userdt = AdminRepo.DeleteEmployeeByID(User.ID);

                        Response.status = true;
                        Response.message = "Deleted Successfully.";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Employee ID is Null.";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid Token.";
                }


            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        #endregion
        [Route("api/Admin/GetListofTiers")]
        [HttpPost]
        public object GetListofTiers()
        {
            try
            {
                DataTable Response = AdminRepo.GetTierForDropdown();
                return Response;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        [Route("api/Admin/RoleList")]
        [HttpPost]
        public object RoleList()
        {
            try
            {
                DataTable Response = AdminRepo.RoleList();
                return Response;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [Route("api/Admin/RoleListForManager")]
        [HttpPost]
        public object RoleListForManager()
        {
            try
            {
                DataTable Response = AdminRepo.RoleListForManager();
                return Response;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [Route("api/Admin/GetListofMakes")]
        [HttpPost]
        public object GetListofMakes()
        {
            try
            {
                DataTable Response = AdminRepo.GetMakesForDropdown();
                return Response;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #region Conversion Details

        [Route("api/Admin/GetAllConversions")]
        [HttpPost]
        public GetConversionDataList GetAllConversions(HeaderToken header)
        {
            GetConversionDataList Response = new GetConversionDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.ConversionsList = AdminRepo.GetAllConversions();
                    Response.status = true;
                    Response.message = "Record Found";
                }
                else
                {
                    Response.ConversionsList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.ConversionsList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Admin/GetConversionByID")]
        [HttpPost]
        public GetConversionForm GetConversionByID(GetbyID conversion)
        {
            GetConversionForm Response = new GetConversionForm();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(conversion.token);

                if (validtoken)
                {
                    ConversionDetailForm ConversionForm = new ConversionDetailForm();

                    if (conversion.ID != 0)
                    {
                        DataTable userdt = AdminRepo.GetConversionData(conversion.ID);

                        if (userdt != null)
                        {
                            foreach (DataRow item in userdt.Rows)
                            {
                                ConversionForm = new ConversionDetailForm();
                                ConversionForm.ID = Convert.ToInt32(item["ID"]);
                                ConversionForm.ConversionName = item["ConversionName"].ToString();
                                ConversionForm.Type = item["Type"].ToString();
                                ConversionForm.Year = !string.IsNullOrEmpty(item["Year"].ToString()) ? Convert.ToInt32(item["Year"]) : 0;
                                ConversionForm.Make = item["Make"].ToString();
                                ConversionForm.MakeID = Convert.ToInt32(item["MakeID"]);
                                ConversionForm.RetailPrice = item["RetailPrice"].ToString();
                                ConversionForm.DealerPrice = item["DealerPrice"].ToString();
                                ConversionForm.Description = item["Description"].ToString();
                                ConversionForm.TierID = !string.IsNullOrEmpty(item["TierID"].ToString())? Convert.ToInt32(item["TierID"]) : 0;
                            }

                            Response.ConversionData = ConversionForm;
                            Response.status = true;
                            Response.message = "Record Found.";

                        }
                        else
                        {
                            Response.ConversionData = null;
                            Response.status = false;
                            Response.message = "Record Found.";

                        }


                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Employee ID is null.";
                        Response.ConversionData = null;
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                    Response.ConversionData = null;
                }

            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
                Response.ConversionData = null;
            }

            return Response;
        }


        [Route("api/Admin/AddUpdateConversion")]
        [HttpPost]
        public HeaderResponse AddUpdateConversion(ConversionDetailForm Conversion)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(Conversion.token);
                if (validtoken)
                {
                    if (Conversion.ID == 0)
                        {
                            DataTable emp_dt = AdminRepo.CreateConversion(Conversion);

                            Response.status = true;
                            Response.message = "Conversion Created Successfully";

                        }
                        else
                        {
                            DataTable emp_dt = AdminRepo.UpdateConversion(Conversion);

                            Response.status = true;
                            Response.message = "Conversion Updated Successfully";
                        }
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Please fill the valid fields.";
                    }
               


            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }



        [Route("api/Admin/DeleteConversionByID")]
        [HttpPost]
        public HeaderResponse DeleteConversionByID(GetbyID conversion)
        {
            HeaderResponse Response = new HeaderResponse();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(conversion.token);

                if (validtoken)
                {
                    if (conversion.ID != 0)
                    {
                        DataTable userdt = AdminRepo.DeleteConversionByID(conversion.ID);

                        Response.status = true;
                        Response.message = "Conversion Deleted Successfully.";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Employee ID is Null.";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                }

            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        #endregion

        #region Conversion Depreciation

        [Route("api/Admin/GetAllConversionDepriciation")]
        [HttpPost]
        public GetDepriciationDataList GetAllConversionDepriciation(HeaderToken header)
        {
            GetDepriciationDataList Response = new GetDepriciationDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.DepriciationList = AdminRepo.GetAllDepriciations();
                    Response.status = true;
                    Response.message = "Record Found";
                }
                else
                {
                    Response.DepriciationList = null;
                    Response.status = false;
                    Response.message = "Invalid token.";
                }



            }
            catch (Exception ex)
            {
                Response.DepriciationList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Admin/GetDepreciationByID")]
        [HttpPost]
        public GetDepreciationForm GetDepreciationByID(GetbyID depreciation)
        {
            GetDepreciationForm Response = new GetDepreciationForm();

            try
            {
                DeprecaitionForm Depreciation = new DeprecaitionForm();

                var validtoken = utilityRespository.ValidationAccesstoken(depreciation.token);

                if (validtoken)
                {
                    if (depreciation.ID != 0)
                    {
                        DataTable userdt = AdminRepo.GetDepreciationData(depreciation.ID);

                        if (userdt != null)
                        {
                            foreach (DataRow item in userdt.Rows)
                            {
                                Depreciation = new DeprecaitionForm();
                                Depreciation.ID = Convert.ToInt32(item["ID"]);
                                Depreciation.DepreciationPercentage = item["DepreciationPercent"].ToString();
                                Depreciation.AgeInMonths = item["AgeInMonths"].ToString();
                            }

                            Response.DepreciationData = Depreciation;
                            Response.status = true;
                            Response.message = "Record Found.";

                        }
                        else
                        {
                            Response.DepreciationData = null;
                            Response.status = false;
                            Response.message = "Record Found.";

                        }
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Depreciation ID is null.";
                        Response.DepreciationData = null;
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                    Response.DepreciationData = null;
                }


            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
                Response.DepreciationData = null;
            }

            return Response;
        }

        [Route("api/Admin/AddUpdateDepreciation")]
        [HttpPost]
        public HeaderResponse AddUpdateDepreciation(DeprecaitionForm depreciation)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(depreciation.token);

                if (validtoken)
                {
                    if (ModelState.IsValid)
                    {

                        if (depreciation.ID == 0)
                        {
                            DataTable emp_dt = AdminRepo.CreateDepreciation(depreciation);

                            Response.status = true;
                            Response.message = "Depreciation Created Successfully";

                        }
                        else
                        {
                            DataTable emp_dt = AdminRepo.UpdateDepreciation(depreciation);

                            Response.status = true;
                            Response.message = "Depreciation Updated Successfully";
                        }
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Please fill the valid fields.";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                }


            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/DeleteDepreciationByID")]
        [HttpPost]
        public HeaderResponse DeleteDepreciationByID(GetbyID depreciation)
        {
            HeaderResponse Response = new HeaderResponse();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(depreciation.token);

                if (validtoken)
                {
                    if (depreciation.ID != 0)
                    {
                        DataTable userdt = AdminRepo.DeleteDepreciationByID(depreciation.ID);

                        Response.status = true;
                        Response.message = "Depreciation Deleted Successfully.";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Employee ID is Null.";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                }


            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }


        #endregion

        #region Conversion MarkUp

        [Route("api/Admin/GetMarkUpPercent")]
        [HttpGet]
        public GetMarkUpDataPercent GetMarkUpPercent()
        {
            GetMarkUpDataPercent Response = new GetMarkUpDataPercent();

            try
            {

                Response.MarkupData = AdminRepo.GetMarkupPercent();
                Response.status = true;
                Response.message = "Record Found";

            }
            catch (Exception ex)
            {
                Response.MarkupData = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Admin/GetTierDetails")]
        [HttpGet]
        public GetTiersDetails GetTierDetails()
        {
            GetTiersDetails Response = new GetTiersDetails();

            try
            {

                Response.MarkupData = AdminRepo.GetAlltiersDetails();
                Response.status = true;
                Response.message = "Record Found";

            }
            catch (Exception ex)
            {
                Response.MarkupData = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Admin/GetMarkUpFixed")]
        [HttpGet]
        public GetMarkUpDataFixed GetMarkUpFixed()
        {
            GetMarkUpDataFixed Response = new GetMarkUpDataFixed();

            try
            {

                Response.MarkupData = AdminRepo.GetMarkupFixed();
                Response.status = true;
                Response.message = "Record Found";

            }
            catch (Exception ex)
            {
                Response.MarkupData = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Admin/UpdateMarkUpPercent")]
        [HttpPost]
        public HeaderResponse UpdateMarkUpPercent(MarkUpFormPercent markup)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (markup.ID == 0)
                    {
                        Response.status = false;
                        Response.message = "MarkUp ID is Null.";

                    }
                    else
                    {
                        DataTable emp_dt = AdminRepo.UpdateMarkUpPercent(markup);

                        Response.status = true;
                        Response.message = "MarkUp Percent Updated Successfully";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Please fill the valid fields.";
                }
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/UpdateMarkUpFixed")]
        [HttpPost]
        public HeaderResponse UpdateMarkUpFixed(MarkUpFormFixed markup)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (markup.ID == 0)
                    {
                        Response.status = false;
                        Response.message = "MarkUp ID is Null.";

                    }
                    else
                    {
                        DataTable emp_dt = AdminRepo.UpdateMarkUpFixed(markup);

                        Response.status = true;
                        Response.message = "MarkUp Fixed Updated Successfully";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Please fill the valid fields.";
                }
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        #endregion

        #region Conversion Deduction

        [Route("api/Admin/GetAllConversionDeduction")]
        [HttpPost]
        public GetDeductionDataList GetAllConversionDeduction(HeaderToken header)
        {
            GetDeductionDataList Response = new GetDeductionDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.DeductionList = AdminRepo.GetAllDeduction();
                    Response.status = true;
                    Response.message = "Record Found";
                }
                else
                {
                    Response.DeductionList = null;
                    Response.status = false;
                    Response.message = "Invalid token.";
                }



            }
            catch (Exception ex)
            {
                Response.DeductionList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Admin/GetDeductionByID")]
        [HttpPost]
        public GetDeductionForm GetDeductionByID(GetbyID deduction)
        {
            GetDeductionForm Response = new GetDeductionForm();

            try
            {
                DeductionForm Deduction = new DeductionForm();
                var validtoken = utilityRespository.ValidationAccesstoken(deduction.token);

                if (validtoken)
                {
                    if (deduction.ID != 0)
                    {
                        DataTable userdt = AdminRepo.GetDeductionData(deduction.ID);

                        if (userdt != null)
                        {
                            foreach (DataRow item in userdt.Rows)
                            {
                                Deduction = new DeductionForm();
                                Deduction.ID = Convert.ToInt32(item["ID"]);
                                Deduction.DeductionAmount = item["Amount"].ToString();
                                Deduction.Description = item["Description"].ToString();
                            }

                            Response.DeductionData = Deduction;
                            Response.status = true;
                            Response.message = "Record Found.";

                        }
                        else
                        {
                            Response.DeductionData = null;
                            Response.status = false;
                            Response.message = "Record Found.";

                        }
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Deduction ID is null.";
                        Response.DeductionData = null;
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                    Response.DeductionData = null;
                }

            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
                Response.DeductionData = null;
            }

            return Response;
        }

        [Route("api/Admin/AddUpdateDeduction")]
        [HttpPost]
        public HeaderResponse AddUpdateDeduction(DeductionForm Deduction)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(Deduction.token);

                if (validtoken)
                {
                    if (ModelState.IsValid)
                    {


                        if (Deduction.ID == 0)
                        {
                            DataTable emp_dt = AdminRepo.CreateDeduction(Deduction);

                            Response.status = true;
                            Response.message = "Deduction Created Successfully";

                        }
                        else
                        {
                            DataTable emp_dt = AdminRepo.UpdateDeduction(Deduction);

                            Response.status = true;
                            Response.message = "Deduction Updated Successfully";
                        }
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Please fill the valid fields.";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                }


            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Admin/DeleteDeductionByID")]
        [HttpPost]
        public HeaderResponse DeleteDeductionByID(GetbyID deduct)
        {
            HeaderResponse Response = new HeaderResponse();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(deduct.token);

                if (validtoken)
                {
                    if (deduct.ID != 0)
                    {
                        DataTable userdt = AdminRepo.DeleteDeductionByID(deduct.ID);

                        Response.status = true;
                        Response.message = "Deduction Deleted Successfully.";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Deduction ID is Null.";
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                }

            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        #endregion

        [Route("api/Admin/CheckExistingCoupon")]
        [HttpPost]
        public HeaderResponse CheckExistingCoupon(string Email)
        {
            var response = AdminRepo.EmailExist(Email);

            return response;
        }


        [Route("api/Admin/UpdatePassword")]
        [HttpPost]
        public HeaderResponse UpdatePassword(ChangePassword _changePassword)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
               
                        bool updatecheck = AdminRepo.UpdatePassword(_changePassword);
                if (updatecheck)
                {
                    Response.status = true;
                    Response.message = "Password Change Successfully";
                }

                else
                {
                    Response.status = false;
                    Response.message = "Password Not Change Successfully";
                }
                        
                   
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }
    }
}
