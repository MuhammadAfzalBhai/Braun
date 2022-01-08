using BAL.Repository;
using Braunability_ViewModal.Model;
using BraunApp_ViewModel.Model;
using DAL.DBEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Braunability_ViewModal.Model.BraunVM_Request;
using static Braunability_ViewModal.Model.BraunVM_Response;

namespace Bruneability_API.Controllers
{
    public class ManagerController : ApiController
    {
        ManagerRepository ManagerRepo = new ManagerRepository();

        [Route("api/Manager/GetManagerDahboardCounterValues")]
        [HttpPost]
        public GetManagerDahboardCounterValues GetManagerDahboardCounterValues(HeaderToken header)
        {
            GetManagerDahboardCounterValues response = new GetManagerDahboardCounterValues();
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (validtoken)
                {
                    response = ManagerRepo.GetManagerDashboardCounterResult(header.UserID);
                }
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = "Result Not Found.";
            }

            return response;
        }

        [Route("api/Manager/QuoteslastSixMonth")]
        [HttpPost]
        public GetDashboard QuoteslastSixMonth(HeaderToken header)
        {
            GetDashboard reponse = new GetDashboard();
            try
            {
                List<QuotesListMonths> _listofQuotesListMonths = new List<QuotesListMonths>();

                var validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (validtoken)
                {
                    _listofQuotesListMonths = ManagerRepo.GetQuoetsoflastsixmonths();
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
                        reponse.QuotesListLastSixMonth.message = "Six Month Quotes Not Found.";
                        reponse.QuotesListLastSixMonth.QuotesListLastSixMonth = null;
                    }
                }
            }
            catch (Exception ex)
            {
                reponse.status = false;
                reponse.message = "Quotes Not Found.";
                reponse.QuotesListLastSixMonth = null;
            }

            return reponse;
        }

        [Route("api/Manager/GetAllPUpApprovedQoutesDataByEmployeeId")]
        [HttpPost]
        public GetQoutesDataList GetAllPUpApprovedQoutesDataByEmployeeId(GetbyID header)
        {
            GetQoutesDataList Response = new GetQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.QoutesList = ManagerRepo.GetQuotesByEmpIdForApp(header.ID);
                    if (Response.QoutesList.Rows.Count > 0)
                    {
                        Response.status = true;
                        Response.message = "Record Found";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Record Not Found";
                    }

                }
                else
                {
                    Response.QoutesList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.QoutesList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Manager/ApproveDetails")]
        [HttpPost]
        public GetQoutesDataList ApproveDetails(GetbyID header)
        {
            GetQoutesDataList Response = new GetQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.QoutesList = ManagerRepo.ApproveDetails(header.ID);
                    if (Response.QoutesList.Rows.Count > 0)
                    {
                        Response.status = true;
                        Response.message = "Record Found";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Record Not Found";
                    }

                }
                else
                {
                    Response.QoutesList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.QoutesList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }
        [Route("api/Manager/Approval")]
        [HttpPost]
        public HeaderResponse Approval(Approvebyid qoute)
        {
            HeaderResponse Response = new HeaderResponse();


            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(qoute.token);

                if (validtoken)
                {
                    if (qoute.ID != 0)
                    {
                        DataTable userdt = ManagerRepo.ApproveQouteByID(qoute);

                        Response.status = true;
                        Response.message = "Qoute Approved Successfully.";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Qoute ID is Null.";
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

        [Route("api/Manager/GetAllRejectedData")]
        [HttpPost]
        public GetRejectedQoutesDataList GetAllRejectedData(GetbyID header)
        {
            GetRejectedQoutesDataList Response = new GetRejectedQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.RejectedQoutesList = ManagerRepo.GetRejectedQuotesByManager(header.ID);
                    if (Response.RejectedQoutesList.Rows.Count > 0)
                    {
                        Response.status = true;
                        Response.message = "Record Found";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Record Not Found";
                    }

                }
                else
                {
                    Response.RejectedQoutesList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.RejectedQoutesList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Manager/GetAllApprovedData")]
        [HttpPost]
        public GetApprovedQoutesDataList GetAllApprovedData(GetbyID header)
        {
            GetApprovedQoutesDataList Response = new GetApprovedQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.ApprovedQoutesList = ManagerRepo.GetApprovedQuotesByManager(header.ID);
                    if (Response.ApprovedQoutesList.Rows.Count > 0)
                    {
                        Response.status = true;
                        Response.message = "Record Found";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Record Not Found";
                    }

                }
                else
                {
                    Response.ApprovedQoutesList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.ApprovedQoutesList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Manager/Reject")]
        [HttpPost]
        public HeaderResponse Reject(Rejectbyid qoute)
        {
            HeaderResponse Response = new HeaderResponse();


            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(qoute.token);

                if (validtoken)
                {
                    if (qoute.ID != 0)
                    {
                        DataTable userdt = ManagerRepo.RejectQouteByID(qoute);

                        Response.status = true;
                        Response.message = "Qoute Decline Successfully.";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Qoute ID is Null.";
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

        [Route("api/Manager/UpdatePassword")]
        [HttpPost]
        public HeaderResponse UpdatePassword(ChangePassword _changePassword)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {

                bool updatecheck = ManagerRepo.UpdatePassword(_changePassword);
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

        [Route("api/Manager/GetAllQuotesforReportManager")]
        [HttpPost]
        public GetQoutesDataListManager GetAllQuotesforReportManager(HeaderToken header)
        {
            GetQoutesDataListManager obj_getquotesdatalist = new GetQoutesDataListManager();
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (validtoken)
                {
                    obj_getquotesdatalist.QoutesList = ManagerRepo.GetQoutesforReportManager(header.UserID);
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
    }
}