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
using System.Web.Helpers;

namespace Bruneability_API.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeRepository EmpRepo;


        public EmployeeController()
        {
            EmpRepo = new EmployeeRepository(new vt_BraunAppEntities());
        }

        [Route("api/Employee/SearchQuotesNew")]
        [HttpPost]
        public QoutesSearchData SearchQuotesNew(QuoteSearchHeader header)
        {
            QoutesSearchData result = new QoutesSearchData();
            try
            {
                var Validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (Validtoken)
                {
                    result.qouteData = EmpRepo.getSearchQuotesNew(header.Name, header.Model, header.Make, header.Year, header.EmpId);


                    if (result.qouteData.Rows.Count > 0)
                    {
                        result.status = true;
                        result.message = "Quotes Found.";
                    }
                    else
                    {
                        result.status = false;
                        result.message = "Quotes not found.";
                    }


                }

                else
                {
                    result.status = false;
                    result.message = "Invalid Token.";
                    result.qouteData = null;
                }
            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = "Quotes not Found.";
                result.qouteData = null;
                throw;
            }

            return result;

        }

        [Route("api/Employee/GetAllRejectedData")]
        [HttpPost]
        public GetRejectedQoutesDataList GetAllRejectedData(GetbyID header)
        {
            GetRejectedQoutesDataList Response = new GetRejectedQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.RejectedQoutesList = EmpRepo.GetRejectedQuotesByEmpId(header.ID);
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

        [Route("api/Employee/GetAllApprovedData")]
        [HttpPost]
        public GetApprovedQoutesDataList GetAllApprovedData(GetbyID header)
        {
            GetApprovedQoutesDataList Response = new GetApprovedQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.ApprovedQoutesList = EmpRepo.GetApprovedQuotesByEmp(header.ID);
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
        [Route("api/Employee/GetManagerDahboardCounterValues")]
        [HttpPost]
        public GetManagerDahboardCounterValues GetManagerDahboardCounterValues(HeaderToken header)
        {
            GetManagerDahboardCounterValues response = new GetManagerDahboardCounterValues();
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (validtoken)
                {
                    response = EmpRepo.GetManagerDashboardCounterResult();
                }
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = "Result Not Found.";
            }

            return response;
        }

        [Route("api/Employee/SearchQuotesDraft")]
        [HttpPost]
        public QoutesSearchData SearchQuotesDraft(QuoteSearchHeader header)
        {
            QoutesSearchData result = new QoutesSearchData();
            try
            {
                var Validtoken = utilityRespository.ValidationAccesstoken(header.token);
                if (Validtoken)
                {
                    result.qouteData = EmpRepo.getSearchQuotesDraft(header.Name, header.Model, header.Make, header.Year, header.EmpId);


                    if (result.qouteData.Rows.Count > 0)
                    {
                        result.status = true;
                        result.message = "Quotes Found.";

                    }
                    else
                    {
                        result.status = false;
                        result.message = "Quotes not found.";
                    }
                }

                else
                {
                    result.status = false;
                    result.message = "Invalid Token.";
                    result.qouteData = null;
                }
            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = "Quotes not Found.";
                result.qouteData = null;
                throw;
            }

            return result;

        }


        [Route("api/Employee/GetAllStateCode")]
        [HttpPost]
        public StateCode GetAllStateCode(HeaderToken header)
        {
            StateCode Response = new StateCode();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    List<statecodeprop> listofstates = new List<statecodeprop>()
                    {
                      new  statecodeprop { Descr = "Alabama, US", Code = "AL" },
                       new statecodeprop { Descr = "Alaska, US", Code = "AK" },
                       new statecodeprop { Descr = "Arizona, US", Code = "AZ" },
                       new statecodeprop { Descr = "Arkansas, US", Code = "AR" },
                       new statecodeprop { Descr = "California, US", Code = "CA" },
                       new statecodeprop { Descr = "Colorado, US", Code = "CO" },
                        new statecodeprop { Descr = "Connecticut, US", Code = "CT" },
                       new statecodeprop { Descr = "Delaware, US", Code = "DE" },
                       new statecodeprop { Descr = "District, US", Code = "DC" },
                       new statecodeprop { Descr = "Florida, US", Code = "FL" },
                       new statecodeprop { Descr = "Georgia, US", Code = "GA" },
                       new statecodeprop { Descr = "Hawaii, US", Code = "HI" },

                        new statecodeprop { Descr = "Idaho, US", Code = "ID" },
                       new statecodeprop { Descr = "Illinois, US", Code = "IL" },
                       new statecodeprop { Descr = "Indiana, US", Code = "IN" },
                       new statecodeprop { Descr = "Iowa, US", Code = "IA" },
                       new statecodeprop { Descr = "Kansas, US", Code = "KS" },
                       new statecodeprop { Descr = "Kentucky, US", Code = "KY" },

                       new statecodeprop { Descr = "Louisiana, US", Code = "LA" },
                       new statecodeprop { Descr = "Maine, US", Code = "ME" },
                       new statecodeprop { Descr = "Maryland, US", Code = "MD" },
                       new statecodeprop { Descr = "Massachusetts, US", Code = "MA" },
                       new statecodeprop { Descr = "Michigan, US", Code = "MI" },
                       new statecodeprop { Descr = "Minnesota, US", Code = "MN" },

                       new statecodeprop { Descr = "Mississippi, US", Code = "MS" },
                       new statecodeprop { Descr = "Missouri, US", Code = "MO" },
                       new statecodeprop { Descr = "Montana, US", Code = "MT" },
                       new statecodeprop { Descr = "Nebraska, US", Code = "NE" },
                       new statecodeprop { Descr = "Nevada, US", Code = "NV" },
                       new statecodeprop { Descr = "New Hampshire, US", Code = "NH" },

                       new statecodeprop { Descr = "New Jersey, US", Code = "NJ" },
                       new statecodeprop { Descr = "New Mexico, US", Code = "NM" },
                       new statecodeprop { Descr = "New York, US", Code = "NY" },
                       new statecodeprop { Descr = "North Carolina, US", Code = "NC" },
                       new statecodeprop { Descr = "North Dakota, US", Code = "ND" },
                       new statecodeprop { Descr = "Ohio, US", Code = "OH" },

                       new statecodeprop { Descr = "Oklahoma, US", Code = "OK" },
                       new statecodeprop { Descr = "Oregon, US", Code = "OR" },
                       new statecodeprop { Descr = "Pennsylvania, US", Code = "PA" },
                       new statecodeprop { Descr = "Rhode Island, US", Code = "RI" },
                       new statecodeprop { Descr = "South Carolina, US", Code = "SC" },
                       new statecodeprop { Descr = "South Dakota, US", Code = "SD" },

                       new statecodeprop { Descr = "Tennessee, US", Code = "TN" },
                       new statecodeprop { Descr = "Texas, US", Code = "TX" },
                       new statecodeprop { Descr = "Utah, US", Code = "UT" },
                       new statecodeprop { Descr = "Vermont, US", Code = "VT" },
                       new statecodeprop { Descr = "Virginia, US", Code = "VA" },
                       new statecodeprop { Descr = "Washington, US", Code = "WA" },

                      new statecodeprop { Descr = "West Virginia, US", Code = "WV" },
                       new statecodeprop { Descr = "Wisconsin, US", Code = "WI" },
                       new statecodeprop { Descr = "Wyoming, US", Code = "WY" },
                       new statecodeprop { Descr = "Vermont, US", Code = "VT" },
                       new statecodeprop { Descr = "Virginia, US", Code = "VA" },
                       new statecodeprop { Descr = "Washington, US", Code = "WA" }
                    };



                    Response.Statecodelist = listofstates;
                    Response.status = true;
                    Response.message = "Record Found";
                }
                else
                {
                    Response.Statecodelist = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.Statecodelist = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }


        [Route("api/Employee/GetAllConversions")]
        [HttpPost]
        public GetConversionDataList GetAllConversions(HeaderToken header)
        {
            GetConversionDataList Response = new GetConversionDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.ConversionsList = EmpRepo.GetAllConversions();
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


        [Route("api/Employee/GetSelectedMarkup")]
        [HttpPost]
        public GetMarkUpValueswithId GetSelectedMarkup(HeaderToken header)
        {
            GetMarkUpValueswithId obj = new GetMarkUpValueswithId();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    using (vt_BraunAppEntities db = new vt_BraunAppEntities())
                    {

                        var queryforgetselectedMarkup = db.vt_MarkUp.Where(x => x.Selected == true).FirstOrDefault();

                        obj.Id = queryforgetselectedMarkup.ID;
                        obj.MarkUpPer = Convert.ToDouble(queryforgetselectedMarkup.MarkUpPercent);
                        obj.MarkUpFixed = Convert.ToDouble(queryforgetselectedMarkup.MarkUpFixed);

                        obj.status = true;
                        obj.message = "Record Found";

                    }

                }
                else
                {
                    obj.Id = 0;
                    obj.MarkUpPer = 0;
                    obj.MarkUpFixed = 0;
                    obj.status = false;
                    obj.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                obj.Id = 0;
                obj.MarkUpPer = 0;
                obj.MarkUpFixed = 0;
                obj.status = false;
                obj.message = "Record Not Found";
            }

            return obj;
        }
        [Route("api/Employee/GetAllMakes")]
        [HttpPost]
        public GetMakeList GetAllMakes()
        {
            GetMakeList Response = new GetMakeList();

            try
            {


                // Response.ConversionsList = EmpRepo.GetAllConversions();
                Response.MakesList = EmpRepo.GetMakesForDropdown();
                Response.TierList = EmpRepo.GetTiersForDropdown();
                if (Response.MakesList.Rows.Count > 0)
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
            catch (Exception ex)
            {
                Response.MakesList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }
        [Route("api/Employee/GetAllConversionsByMakeId")]
        [HttpPost]
        public GetConversionDataList GetAllConversionsByMakeId(GetbyID header)
        {
            GetConversionDataList Response = new GetConversionDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {

                    // Response.ConversionsList = EmpRepo.GetAllConversions();
                    Response.ConversionsList = EmpRepo.GetAllConversionsbymakeId(header.ID, header.TierId);
                    if (Response.ConversionsList.Rows.Count > 0)
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


        [Route("api/Employee/CalDepreciation")]
        [HttpPost]
        public GetDepriciationresult CalDepreciation(GetbyDepreciation header)
        {

            vt_BraunAppEntities db = new vt_BraunAppEntities();
            GetDepriciationresult Response = new GetDepriciationresult();
            double result = 0;
            double calculatedamount = header.conversionamount;
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    int finalmonth = header.Month + 4;
                    var listofabovemonth = db.vt_Depreciation.Where(x => x.AgeInMonths <= finalmonth && x.DeletedAt == null).ToList();


                    foreach (var item in listofabovemonth)
                    {
                        result = Convert.ToDouble((calculatedamount * Convert.ToDouble(item.DepreciationPercent)) / 100);
                        calculatedamount = calculatedamount - result;

                        //calculatedamount = calculatedamount + header.Amount;
                    }

                    int TierTotalAmt = header.Amount;
                    calculatedamount = calculatedamount - TierTotalAmt;
                    // Response.ConversionsList = EmpRepo.GetAllConversions();
                    //  Response.Depreciationresult = EmpRepo.GetAllConversionsbymakeId(header.ID);
                    Response.Depreciationresult = calculatedamount;
                    Response.status = true;
                    Response.message = "Record Found";
                }
                else
                {
                    Response.Depreciationresult = 0;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.Depreciationresult = 0;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Employee/GetAllQouteConversionDetails")]
        [HttpPost]
        public GetQouteConversionDetailList GetAllQouteConversionDetails(ConversionArray array)
        {
            GetQouteConversionDetailList Response = new GetQouteConversionDetailList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(array.token);

                if (validtoken)
                {
                    Response.ConversionList = EmpRepo.GetAllConversionsbyArray(array.conversionArray);
                    Response.DeductionsList = EmpRepo.GetAllDeductions();
                    Response.Amount = EmpRepo.GetTierAmountById(Convert.ToInt32(Response.ConversionList[0].TierID));
                    Response.status = true;
                    Response.message = "Record Found";
                }
                else
                {
                    Response.ConversionList = null;
                    Response.DeductionsList = null;
                    Response.status = false;
                    Response.message = "Invalid Token";
                }



            }
            catch (Exception ex)
            {
                Response.ConversionList = null;
                Response.DeductionsList = null;
                Response.status = false;
                Response.message = "Record Not Found";
            }

            return Response;
        }

        [Route("api/Employee/SaveAttachments")]
        [HttpPost]
        public HeaderResponse SaveQoute(Attachments header)
        {
            HeaderResponse Response = new HeaderResponse();

            if (header.QuoteID == 0)
            {
                string ID = EmpRepo.GetLastQouteID();
                header.QuoteID = Convert.ToInt32(ID);
            }

            EmpRepo.Insert(header);
            Response.status = true;
            Response.message = "Qoute Saved Successfully.";
            return Response;
        }

        [Route("api/Employee/SaveQoute")]
        [HttpPost]
        public HeaderResponse SaveQoute(QouteRequest header)
        {
            //var serializeOBJ = JsonConvert.SerializeObject(header);
            //ErrorHandling.WriteError(serializeOBJ);

            HeaderResponse Response = new HeaderResponse();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    if (header.DeductionAmount != null)
                    {

                        DataTable dtQoute = EmpRepo.AddNewQoute(header);
                        string ID = EmpRepo.GetLastQouteID();
                        int QouteID = Convert.ToInt32(ID);


                        if (header.ConversionID != null)
                        {
                            if (header.ConversionID[0].Contains(','))
                            {
                                var ios_ConversionID = header.ConversionID[0].Split(',');
                                var ios_ConversionAmount = header.ConversionAmount[0].Split(',');
                                for (int i = 0; i < ios_ConversionID.Length; i++)
                                {
                                    DataTable dtConversion = EmpRepo.AddNewQouteConversion(QouteID, Convert.ToInt32(ios_ConversionID[i]), ios_ConversionAmount[i]);

                                }

                            }

                            else
                            {
                                for (int i = 0; i < header.ConversionID.Length; i++)
                                {
                                    DataTable dtConversion = EmpRepo.AddNewQouteConversion(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i]);

                                }
                            }

                        }
                        else
                        {
                            //DataTable dtConversion = EmpRepo.AddNewQouteConversion(QouteID, 0, null);
                        }


                        if (header.DeductionAmount.Length > 0)
                        {
                            var ios_ConversionID = header.ConversionID[0].Split(',');
                            var ios_ConversionAmount = header.ConversionAmount[0].Split(',');
                            var ios_DeductionID = header.DeductionID[0].Split(',');
                            var ios_DeductionAmount = header.DeductionAmount[0].Split(',');
                            var ios_DepericiationAmount = header.DepericiationAmount[0].Split(',');
                            int lengthofDeduction = ios_DeductionID.Length;
                            for (int i = 0; i < header.DeductionID.Length; i++)
                            {
                                var ddid = header.DeductionID[i];
                                var ddAmount = header.DeductionAmount[i];
                                DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(ios_ConversionID[0]), ios_ConversionAmount[0], Convert.ToInt32(ddid), ddAmount, ios_DepericiationAmount[0]);
                            }
                        }
                        Response.status = true;
                        Response.message = "Qoute Saved Successfully.";
                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Please add conversion to continue procedure";
                    }








                    //if (header.DeductionID != null)
                    //{
                    //    if (header.DeductionID[0].Contains(','))
                    //    {
                    //        var ios_ConversionID = header.ConversionID[0].Split(',');
                    //        var ios_ConversionAmount = header.ConversionAmount[0].Split(',');
                    //        var ios_DeductionID = header.DeductionID[0].Split(',');
                    //        var ios_DeductionAmount  = header.DeductionAmount[0].Split(',');
                    //        var ios_DepericiationAmount = header.DepericiationAmount[0].Split(',');
                    //        int lengthofDeduction = ios_DeductionID.Length;

                    //        for (int i = 0; i < header.DepericiationAmount.Length; i++)
                    //        {

                    //            if (lengthofDeduction > i)
                    //            {
                    //                DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(ios_ConversionID[i]), ios_ConversionAmount[i], Convert.ToInt32(ios_DeductionID[i]), ios_DeductionAmount[i], ios_DepericiationAmount[i]);
                    //            }
                    //            else
                    //            {
                    //                DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(ios_ConversionID[i]), ios_ConversionAmount[i], 0, null, ios_DepericiationAmount[i]);
                    //            }

                    //        }

                    //    }
                    //    else {

                    //        int lengthofDeduction = header.DeductionID.Length;
                    //        for (int i = 0; i < header.DepericiationAmount.Length; i++)
                    //        {

                    //            if (lengthofDeduction > i)
                    //            {
                    //                DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i], Convert.ToInt32(header.DeductionID[i]), header.DeductionAmount[i], header.DepericiationAmount[i]);
                    //            }
                    //            else
                    //            {
                    //                DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i], 0, null, header.DepericiationAmount[i]);
                    //            }

                    //        }
                    //    }


                    //}

                    //else
                    //{
                    //    if (header.DepericiationAmount != null)
                    //    {
                    //        if (header.DepericiationAmount[0].Contains(','))
                    //        {
                    //            var ios_ConversionID = header.ConversionID[0].Split(',');
                    //            var ios_ConversionAmount = header.ConversionAmount[0].Split(',');
                    //            var ios_DeductionID = header.DeductionID[0].Split(',');
                    //            var ios_DeductionAmount = header.DeductionAmount[0].Split(',');
                    //            var ios_DepericiationAmount = header.DepericiationAmount[0].Split(',');

                    //            for (int i = 0; i < ios_DepericiationAmount.Length; i++)
                    //            {
                    //                DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(ios_ConversionID[i]), ios_ConversionAmount[i], 0, null, ios_DepericiationAmount[i]);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            for (int i = 0; i < header.DepericiationAmount.Length; i++)
                    //            {
                    //                DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i], 0, null, header.DepericiationAmount[i]);
                    //            }
                    //        }

                    //    }
                    //    else
                    //    {

                    //    }
                    //}



                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid Token";
                }
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Employee/UpdateQoute")]
        [HttpPost]
        public HeaderResponse UpdateQoute(QouteRequest header)
        {
            HeaderResponse Response = new HeaderResponse();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    if (header.QouteID > 0)
                    {
                        DataTable dtQoute = EmpRepo.UpdateQoute(header);
                        int QouteID = header.QouteID;

                        dtQoute = EmpRepo.DeleteQouteConversions(QouteID); //Delete all qoute conversions
                        dtQoute = EmpRepo.DeleteQouteDeductions(QouteID); //Delete all qoute deductions

                        //if (header.ConversionID != null)
                        //{
                        //    for (int i = 0; i < header.ConversionID.Length; i++)
                        //    {
                        //        DataTable dtConversion = EmpRepo.AddNewQouteConversion(QouteID, header.ConversionID[i], header.ConversionAmount[i]);//Insert updated qoute conversions
                        //        DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, header.ConversionID[i], header.ConversionAmount[i], header.DeductionID[i], header.DeductionAmount[i], header.DepericiationAmount[i]);//Insert updated qoute deductions
                        //    }
                        //}
                        //else
                        //{
                        //    DataTable dtConversion = EmpRepo.AddNewQouteConversion(QouteID, 0, null);
                        //    DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, 0, null, 0, null,null);
                        //}
                        ////////////
                        if (header.ConversionID != null)
                        {
                            if (header.ConversionID[0].Contains(','))
                            {
                                var ios_ConversionID = header.ConversionID[0].Split(',');
                                var ios_ConversionAmount = header.ConversionAmount[0].Split(',');
                                for (int i = 0; i < ios_ConversionID.Length; i++)
                                {
                                    DataTable dtConversion = EmpRepo.AddNewQouteConversion(QouteID, Convert.ToInt32(ios_ConversionID[i]), ios_ConversionAmount[i]);

                                }

                            }
                            else
                            {
                                for (int i = 0; i < header.ConversionID.Length; i++)
                                {
                                    DataTable dtConversion = EmpRepo.AddNewQouteConversion(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i]);

                                }
                            }

                        }
                        else
                        {
                            //DataTable dtConversion = EmpRepo.AddNewQouteConversion(QouteID, 0, null);
                        }

                        if (header.DeductionID != null && header.DeductionID[0] != null)
                        {

                            if (header.DeductionID[0].Contains(','))
                            {
                                var ios_ConversionID = header.ConversionID[0].Split(',');
                                var ios_ConversionAmount = header.ConversionAmount[0].Split(',');
                                var ios_DeductionID = header.DeductionID[0].Split(',');
                                var ios_DeductionAmount = header.DeductionAmount[0].Split(',');
                                var ios_DepericiationAmount = header.DepericiationAmount[0].Split(',');
                                int lengthofDeduction = ios_DeductionID.Length;

                                for (int i = 0; i < header.DepericiationAmount.Length; i++)
                                {

                                    if (lengthofDeduction > i)
                                    {
                                        DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(ios_ConversionID[i]), ios_ConversionAmount[i], Convert.ToInt32(ios_DeductionID[i]), ios_DeductionAmount[i], ios_DepericiationAmount[i]);
                                    }
                                    else
                                    {
                                        DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(ios_ConversionID[i]), ios_ConversionAmount[i], 0, null, ios_DepericiationAmount[i]);
                                    }

                                }

                            }
                            else
                            {
                                int lengthofDeduction = header.DeductionID.Length;
                                //for (int i = 0; i < header.DepericiationAmount.Length; i++)
                                for (int i = 0; i < lengthofDeduction; i++)
                                {
                                    if (lengthofDeduction > i)
                                    {
                                        DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(header.ConversionID[0]), header.ConversionAmount[0], Convert.ToInt32(header.DeductionID[i]), header.DeductionAmount[i], header.DepericiationAmount[0]);
                                    }
                                    else
                                    {
                                        DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i], 0, null, header.DepericiationAmount[i]);
                                    }

                                }
                            }

                        }

                        else
                        {
                            if (header.DepericiationAmount != null)
                            {
                                if (header.DepericiationAmount[0].Contains(','))
                                {
                                    var ios_ConversionID = header.ConversionID[0].Split(',');
                                    var ios_ConversionAmount = header.ConversionAmount[0].Split(',');
                                    var ios_DeductionID = header.DeductionID[0].Split(',');
                                    var ios_DeductionAmount = header.DeductionAmount[0].Split(',');
                                    var ios_DepericiationAmount = header.DepericiationAmount[0].Split(',');

                                    for (int i = 0; i < ios_DepericiationAmount.Length; i++)
                                    {
                                        DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(ios_ConversionID[i]), ios_ConversionAmount[i], 0, null, ios_DepericiationAmount[i]);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < header.DepericiationAmount.Length; i++)
                                    {
                                        DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i], 0, null, header.DepericiationAmount[i]);
                                    }
                                }

                            }
                            else
                            {

                            }
                        }
                        ///////////



                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Qoute ID is null.";
                    }

                    Response.status = true;
                    Response.message = "Qoute Updated Successfully.";

                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid Token";
                }
            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
            }

            return Response;
        }

        [Route("api/Employee/GetQouteByID")]
        [HttpPost]
        public QouteDetailsData GetQouteByID(GetbyID qoute)
        {
            QouteDetailsData Response = new QouteDetailsData();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(qoute.token);

                if (validtoken)
                {
                    QouteDetails QouteForm = new QouteDetails();

                    if (qoute.ID != 0)
                    {
                        DataTable qoutedt = EmpRepo.GetQouteData(qoute.ID);

                        if (qoutedt != null)
                        {
                            foreach (DataRow item in qoutedt.Rows)
                            {
                                QouteForm = new QouteDetails();
                                QouteForm.ID = Convert.ToInt32(item["ID"]);
                                QouteForm.CustomerName = !string.IsNullOrEmpty(item["CustomerName"].ToString()) ? item["CustomerName"].ToString() : string.Empty;
                                QouteForm.EmailAddress = !string.IsNullOrEmpty(item["Email"].ToString()) ? item["Email"].ToString() : string.Empty;
                                QouteForm.PhoneNumber = !string.IsNullOrEmpty(item["Phone"].ToString()) ? item["Phone"].ToString() : string.Empty;
                                QouteForm.AddressLine1 = !string.IsNullOrEmpty(item["AddressLine1"].ToString()) ? item["AddressLine1"].ToString() : string.Empty;
                                QouteForm.AddressLine2 = !string.IsNullOrEmpty(item["AddressLine2"].ToString()) ? item["AddressLine2"].ToString() : string.Empty;
                                QouteForm.SearchType = !string.IsNullOrEmpty(item["SearchType"].ToString()) ? item["SearchType"].ToString() : string.Empty;
                                QouteForm.SearchTypeID = !string.IsNullOrEmpty(item["SearchTypeID"].ToString()) ? item["SearchTypeID"].ToString() : string.Empty;
                                QouteForm.Make = !string.IsNullOrEmpty(item["Make"].ToString()) ? item["Make"].ToString() : string.Empty;
                                QouteForm.MakeID = Convert.ToInt32(item["MakeID"]);
                                QouteForm.Model = !string.IsNullOrEmpty(item["Model"].ToString()) ? item["Model"].ToString() : string.Empty;
                                QouteForm.ModelID = Convert.ToInt32(item["ModelID"]);
                                QouteForm.Year = Convert.ToInt32(item["Year"]);
                                QouteForm.YearID = Convert.ToInt32(item["YearID"]);
                                QouteForm.Trim = !string.IsNullOrEmpty(item["Trim"].ToString()) ? item["Trim"].ToString() : string.Empty;
                                QouteForm.TrimID = Convert.ToInt32(item["TrimID"]);
                                QouteForm.Millage = !string.IsNullOrEmpty(item["CurrentMilage"].ToString()) ? item["CurrentMilage"].ToString() : string.Empty;
                                QouteForm.StateCode = !string.IsNullOrEmpty(item["StateCode"].ToString()) ? item["StateCode"].ToString() : string.Empty;
                                QouteForm.CleanTrade = !string.IsNullOrEmpty(item["CleanTrade"].ToString()) ? item["CleanTrade"].ToString() : string.Empty;
                                QouteForm.AverageTrade = !string.IsNullOrEmpty(item["AverageTrade"].ToString()) ? item["AverageTrade"].ToString() : string.Empty;
                                QouteForm.RoughTrade = !string.IsNullOrEmpty(item["RoughTrade"].ToString()) ? item["RoughTrade"].ToString() : string.Empty;
                                QouteForm.RetailTrade = !string.IsNullOrEmpty(item["RetailTrade"].ToString()) ? item["RetailTrade"].ToString() : string.Empty;
                                QouteForm.MarkUpPercent = !string.IsNullOrEmpty(item["MarkUpPercent"].ToString()) ? item["MarkUpPercent"].ToString() : string.Empty;
                                QouteForm.Status = !string.IsNullOrEmpty(item["Status"].ToString()) ? item["Status"].ToString() : string.Empty;
                                QouteForm.EmployeeID = Convert.ToInt32(item["EmployeeID"]);
                                QouteForm.TotalConversionDeduction = !string.IsNullOrEmpty(item["TotalConversionDeduction"].ToString()) ? item["TotalConversionDeduction"].ToString() : string.Empty;
                                QouteForm.Created = Convert.ToDateTime(item["Created"].ToString());
                                QouteForm.VinNumber = !string.IsNullOrEmpty(item["VinNumber"].ToString()) ? item["VinNumber"].ToString() : string.Empty;
                                QouteForm.RegionVal = !string.IsNullOrEmpty(item["RegionVal"].ToString()) ? item["RegionVal"].ToString() : string.Empty;
                                QouteForm.RegionText = !string.IsNullOrEmpty(item["RegionText"].ToString()) ? item["RegionText"].ToString() : string.Empty;
                                QouteForm.IsClean = Convert.ToBoolean(item["IsClean"]);
                                QouteForm.IsAverage = Convert.ToBoolean(item["IsAverage"]);
                                QouteForm.IsRough = Convert.ToBoolean(item["IsRough"]);
                                QouteForm.SuggestedPrice = Convert.ToDecimal(item["SuggestedPrice"]);
                                QouteForm.Comment = Convert.ToString(item["Comment"]);
                                //QouteForm.Deleted = Convert.ToDateTime(item["Deleted"].ToString());
                            }

                            Response.qouteData = QouteForm;
                            Response.status = true;
                            Response.message = "Qoute Found.";

                        }
                        else
                        {
                            Response.qouteData = null;
                            Response.status = false;
                            Response.message = "Qoute Not Found.";

                        }


                    }
                    else
                    {
                        Response.status = false;
                        Response.message = "Qoute ID is null.";
                        Response.qouteData = null;
                    }
                }
                else
                {
                    Response.status = false;
                    Response.message = "Invalid token.";
                    Response.qouteData = null;
                }

            }
            catch (Exception ex)
            {
                Response.status = false;
                Response.message = ex.Message;
                Response.qouteData = null;
            }

            return Response;
        }

        [Route("api/Employee/GetAllConversionsByQouteID")]
        [HttpPost]
        public GetConversionDataList GetAllConversionsByQouteID(GetbyID header)
        {
            GetConversionDataList Response = new GetConversionDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.ConversionsList = EmpRepo.GetAllConversionsbyQouteID(header.ID);
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

        [Route("api/Employee/GetAllDeductionsByQouteID")]
        [HttpPost]
        public GetDeductionDataList GetAllDeductionsByQouteID(GetbyID header)
        {
            GetDeductionDataList Response = new GetDeductionDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    //Response.DeductionList = EmpRepo.GetAllDeductionsbyQouteID(header.ID);
                    Response.DeductionList = EmpRepo.GetAllDeduction();
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


        [Route("api/Employee/GetDeductionsByQouteID")]
        [HttpPost]
        public GetDeductionDataList GetDeductionsByQouteID(GetbyID header)
        {
            GetDeductionDataList Response = new GetDeductionDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    //Response.DeductionList = EmpRepo.GetAllDeductionsbyQouteID(header.ID);
                    Response.DeductionList = EmpRepo.GetAllDeductionByQuoteId(header.ID);
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


        [Route("api/Employee/GetAllQoutesData")]
        [HttpPost]
        public GetQoutesDataList GetAllQoutesData(HeaderToken header)
        {
            GetQoutesDataList Response = new GetQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.QoutesList = EmpRepo.GetAllQoutes();
                    Response.status = true;
                    Response.message = "Record Found";
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


        [Route("api/Employee/GetAllQuotesDataByEmployeeId")]
        [HttpPost]
        public GetQoutesDataList GetAllQuotesDataByEmployeeId(GetbyID header)
        {
            GetQoutesDataList Response = new GetQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.QoutesList = EmpRepo.GetQuotesByEmpId(header.ID);
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


        //[Route("api/Employee/GetAllPUpApprovedQoutesDataByEmployeeId")]
        //[HttpPost]
        //public GetQoutesDataList GetAllPUpApprovedQoutesDataByEmployeeId(GetbyID header)
        //{
        //    GetQoutesDataList Response = new GetQoutesDataList();

        //    try
        //    {
        //        var validtoken = utilityRespository.ValidationAccesstoken(header.token);

        //        if (validtoken)
        //        {
        //            Response.QoutesList = EmpRepo.GetQuotesByEmpIdForApp(header.ID);
        //            if (Response.QoutesList.Rows.Count > 0)
        //            {
        //                Response.status = true;
        //                Response.message = "Record Found";
        //            }
        //            else
        //            {
        //                Response.status = false;
        //                Response.message = "Record Not Found";
        //            }

        //        }
        //        else
        //        {
        //            Response.QoutesList = null;
        //            Response.status = false;
        //            Response.message = "Invalid Token";
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        Response.QoutesList = null;
        //        Response.status = false;
        //        Response.message = "Record Not Found";
        //    }

        //    return Response;
        //}

        //[Route("api/Employee/ApproveDetails")]
        //[HttpPost]
        //public GetQoutesDataList ApproveDetails(GetbyID header)
        //{
        //    GetQoutesDataList Response = new GetQoutesDataList();

        //    try
        //    {
        //        var validtoken = utilityRespository.ValidationAccesstoken(header.token);

        //        if (validtoken)
        //        {
        //            Response.QoutesList = EmpRepo.ApproveDetails(header.ID);
        //            if (Response.QoutesList.Rows.Count > 0)
        //            {
        //                Response.status = true;
        //                Response.message = "Record Found";
        //            }
        //            else
        //            {
        //                Response.status = false;
        //                Response.message = "Record Not Found";
        //            }

        //        }
        //        else
        //        {
        //            Response.QoutesList = null;
        //            Response.status = false;
        //            Response.message = "Invalid Token";
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        Response.QoutesList = null;
        //        Response.status = false;
        //        Response.message = "Record Not Found";
        //    }

        //    return Response;
        //}

        [Route("api/Employee/GetDraftQoutesData")]
        [HttpPost]
        public GetQoutesDataList GetDraftQoutesData(HeaderToken header)
        {
            GetQoutesDataList Response = new GetQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.QoutesList = EmpRepo.GetDraftQoutes();
                    Response.status = true;
                    Response.message = "Record Found";
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


        [Route("api/Employee/GetAllDraftQuotesDataByEmployeeId")]
        [HttpPost]
        public GetQoutesDataList GetAllDraftQuotesDataByEmployeeId(GetbyID header)
        {
            GetQoutesDataList Response = new GetQoutesDataList();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    Response.QoutesList = EmpRepo.GetDraftQuotesByEmpId(header.ID);
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

        [Route("api/Employee/GetMarkUpPercent")]
        [HttpPost]
        public GetMarkUpData GetMarkUpPercent(HeaderToken header)
        {
            GetMarkUpData Response = new GetMarkUpData();

            try
            {

                Response.MarkupData = EmpRepo.GetMarkupPercent();
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
        [Route("api/Employee/GetAttachments")]
        [HttpPost]
        public DataTable GetAttachments(GetAttachmentsbyID qoute)
        {
            DataTable userdt = EmpRepo.GetAttachments(qoute);
            return userdt;
        }

        [Route("api/Employee/DeleteQouteByID")]
        [HttpPost]
        public HeaderResponse DeleteConversionByID(GetbyID qoute)
        {
            HeaderResponse Response = new HeaderResponse();

            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(qoute.token);

                if (validtoken)
                {
                    if (qoute.ID != 0)
                    {
                        DataTable userdt = EmpRepo.DeleteQouteByID(qoute.ID);

                        Response.status = true;
                        Response.message = "Qoute Deleted Successfully.";
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


        //[Route("api/Employee/Approval")]
        //[HttpPost]
        //public HeaderResponse Approval(Approvebyid qoute)
        //{
        //    HeaderResponse Response = new HeaderResponse();


        //    try
        //    {
        //        var validtoken = utilityRespository.ValidationAccesstoken(qoute.token);

        //        if (validtoken)
        //        {
        //            if (qoute.ID != 0)
        //            {
        //                DataTable userdt = EmpRepo.ApproveQouteByID(qoute);

        //                Response.status = true;
        //                Response.message = "Qoute Approved Successfully.";
        //            }
        //            else
        //            {
        //                Response.status = false;
        //                Response.message = "Qoute ID is Null.";
        //            }
        //        }
        //        else
        //        {
        //            Response.status = false;
        //            Response.message = "Invalid token.";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.status = false;
        //        Response.message = ex.Message;
        //    }

        //    return Response;
        //}

        //[Route("api/Employee/Reject")]
        //[HttpPost]
        //public HeaderResponse Reject(Rejectbyid qoute)
        //{
        //    HeaderResponse Response = new HeaderResponse();


        //    try
        //    {
        //        var validtoken = utilityRespository.ValidationAccesstoken(qoute.token);

        //        if (validtoken)
        //        {
        //            if (qoute.ID != 0)
        //            {
        //                DataTable userdt = EmpRepo.RejectQouteByID(qoute);

        //                Response.status = true;
        //                Response.message = "Qoute Decline Successfully.";
        //            }
        //            else
        //            {
        //                Response.status = false;
        //                Response.message = "Qoute ID is Null.";
        //            }
        //        }
        //        else
        //        {
        //            Response.status = false;
        //            Response.message = "Invalid token.";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.status = false;
        //        Response.message = ex.Message;
        //    }

        //    return Response;
        //}

        [Route("api/Employee/UpdatePassword")]
        [HttpPost]
        public HeaderResponse UpdatePassword(ChangePassword _changePassword)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {

                bool updatecheck = EmpRepo.UpdatePassword(_changePassword);
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
        [Route("api/Admin/GetYear")]
        [HttpPost]
        public object GetYear(HeaderTokenForSearch header)
        {

            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();

            var Validtoken = utilityRespository.ValidationAccesstoken(header.token);
            if (Validtoken)
            {

                List<object> result = new List<object>();

                url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/years?period=" + header.period;
                try
                {
                    strResponse = BraunApp_ViewModel.Model.HttpApi.WebRequestNada(url);
                    NadaRestYearResponse data = JsonConvert.DeserializeObject<NadaRestYearResponse>(strResponse);
                    var i = 1;
                    foreach (var item in data.result)
                    {
                        var obj = new { Code = item.modelyear, Descr = item.modelyear };
                        result.Add(obj);
                        i++;
                    }
                    objtoSend.status = true;
                    objtoSend.msg = "Successfull";
                    objtoSend.data = result;
                    //return Json(objtoSend, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    objtoSend.status = false;
                    objtoSend.msg = ex.Message;
                    //return Json(objtoSend, JsonRequestBehavior.AllowGet);
                }

            }

            else
            {
                objtoSend.status = false;
                objtoSend.msg = "Invalid Token.";
                objtoSend.data = null;
            }



            return objtoSend;
        }


        [Route("api/Admin/GetMakes")]
        [HttpGet]
        public object GetMakes(string period, string modelyear, string token)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            var Validtoken = utilityRespository.ValidationAccesstoken(token);
            if (Validtoken)
            {
                List<object> result = new List<object>();

                url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/makes?period=" + period + "&modelyear=" + modelyear;
                //strResponse = HttpApi.WebRequestNada("https://cloud.jdpower.ai/data-api/UAT/valuationservices/valuation/makes?period=0&modelyear=2019");
                try
                {
                    strResponse = BraunApp_ViewModel.Model.HttpApi.WebRequestNada(url);
                    NadaRestMakesResponse data = JsonConvert.DeserializeObject<NadaRestMakesResponse>(strResponse);
                    var i = 1;
                    foreach (var item in data.result)
                    {
                        var obj = new { Code = item.make, Descr = item.make };
                        result.Add(obj);
                        i++;
                    }
                    objtoSend.status = true;
                    objtoSend.msg = "Successfull";
                    objtoSend.data = result;
                }
                catch (Exception ex)
                {
                    objtoSend.status = false;
                    objtoSend.msg = ex.Message;
                }
            }
            else
            {
                objtoSend.status = false;
                objtoSend.msg = "Invalid Token.";
                objtoSend.data = null;
            }
            return objtoSend;
        }


        [Route("api/Admin/GetModels")]
        [HttpGet]
        public object GetModels(string period, string modelyear, string make, string token)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            List<object> result = new List<object>();
            var Validtoken = utilityRespository.ValidationAccesstoken(token);
            if (Validtoken)
            {


                url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/models?period=" + period + "&modelyear=" + modelyear + "&make=" + make;
                try
                {
                    strResponse = HttpApi.WebRequestNada(url);
                    NadaRestModelsResponse data = JsonConvert.DeserializeObject<NadaRestModelsResponse>(strResponse);
                    var i = 1;
                    foreach (var item in data.result)
                    {
                        var obj = new { Code = item.model, Descr = item.model };
                        result.Add(obj);
                        i++;
                    }
                    objtoSend.status = true;
                    objtoSend.msg = "Successfull";
                    objtoSend.data = result;


                }
                catch (Exception ex)
                {
                    objtoSend.status = false;
                    objtoSend.msg = ex.Message;

                }
            }
            else
            {
                objtoSend.status = false;
                objtoSend.msg = "Invalid Token.";
                objtoSend.data = null;
            }
            return objtoSend;
        }
        [Route("api/Admin/GetTrims")]
        [HttpGet]
        public object GetTrims(string period, string modelyear, string make, string model, string token)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            List<object> result = new List<object>();
            var Validtoken = utilityRespository.ValidationAccesstoken(token);
            if (Validtoken)
            {
                url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/bodies?period=" + period + "&modelyear=" + modelyear + "&make=" + make + "&model=" + model;
                try
                {
                    strResponse = HttpApi.WebRequestNada(url);
                    NadaRestTrimsResponse data = JsonConvert.DeserializeObject<NadaRestTrimsResponse>(strResponse);
                    var i = 1;
                    foreach (var item in data.result)
                    {
                        var obj = new { Code = item.ucgvehicleid, Descr = item.body };
                        result.Add(obj);
                        i++;
                    }
                    objtoSend.status = true;
                    objtoSend.msg = "Successfull";
                    objtoSend.data = result;


                }
                catch (Exception ex)
                {
                    objtoSend.status = false;
                    objtoSend.msg = ex.Message;

                }
            }
            else
            {
                objtoSend.status = false;
                objtoSend.msg = "Invalid Token.";
                objtoSend.data = null;
            }
            return objtoSend;
        }

        [Route("api/Admin/GetValues")]
        [HttpGet]
        public object GetValues(string period, string ucgvehicleid, string mileage, string statecode, string token)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            List<object> result = new List<object>();
            TradeInValues carValues = new TradeInValues();
            var Validtoken = utilityRespository.ValidationAccesstoken(token);
            if (Validtoken)
            {
                url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/regionIdByStateCode?statecode=" + statecode;
                try
                {
                    strResponse = HttpApi.WebRequestNada(url);
                    NadaRestRegionByStateCodeResponse data = JsonConvert.DeserializeObject<NadaRestRegionByStateCodeResponse>(strResponse);
                    var regionid = data.result[0].regionid;


                    url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
                    strResponse = HttpApi.CreateRequest(url);
                    GetMarkUpDataPercent markupPercentData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

                    url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
                    strResponse = HttpApi.CreateRequest(url);
                    GetMarkUpDataFixed markupFixedData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

                    var markup_percent = Convert.ToDecimal(markupPercentData.MarkupData.MarkUpPercent);
                    var markup_fixed = Convert.ToDecimal(markupFixedData.MarkupData.MarkUpFixed);




                    url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/vehicleAndValueByVehicleId?period=" + period + "&ucgvehicleid=" + ucgvehicleid + "&region=" + regionid + "&mileage=" + mileage;
                    strResponse = HttpApi.WebRequestNada(url);
                    NadaRestVehicleAndValuesByVehicleIdResponse dataNew = JsonConvert.DeserializeObject<NadaRestVehicleAndValuesByVehicleIdResponse>(strResponse);

                    if (dataNew.result.Count != 0)
                    {
                        var carValuess = dataNew.result[0];
                        if (carValuess != null)
                        {
                            carValues.MakeId = 1;
                            carValues.MakeName = carValuess.make;
                            carValues.Year = carValuess.modelyear;
                            carValues.YearId = carValuess.modelyear;
                            carValues.Model = carValuess.model;
                            carValues.ModelId = 1;
                            carValues.Trim = carValuess.trim;
                            carValues.TrimId = carValuess.ucgvehicleid;
                            carValues.CleanTradeIn = carValuess.adjustedcleantrade;
                            carValues.AverageTradeIn = carValuess.adjustedaveragetrade;
                            carValues.RoughTradeIn = carValuess.adjustedroughtrade;
                            carValues.RetailTradeIn = carValuess.adjustedcleanretail;
                            carValues.MarkUpPercent = markup_percent;
                            carValues.MarkUpFixed = markup_fixed;
                            carValues.Status = true;
                            carValues.Message = "Successfully get TradeIn values";
                        }
                        else
                        {
                            carValues.Status = false;
                            carValues.Message = "Failed to get TradeIn values";
                        }
                    }
                    else
                    {
                        carValues.Status = false;
                        carValues.Message = "TradeIn values Not Found";
                    }



                    //return PartialView("_TradeInValues", carValues);


                    //var i = 1;
                    //foreach (var item in data.result)
                    //{
                    //    var obj = new { Code = item.regionid, Descr = item.region };
                    //    result.Add(obj);
                    //    i++;
                    //}
                    //objtoSend.status = true;
                    //objtoSend.msg = "Successfull";
                    //objtoSend.data = result;
                    //return Json(objtoSend, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    carValues.Status = false;
                    carValues.Message = ex.Message;
                    //return PartialView("_TradeInValues", carValues);
                }
            }
            else
            {
                objtoSend.status = false;
                objtoSend.msg = "Invalid Token.";
                objtoSend.data = null;
            }
            return carValues;
        }

        [Route("api/Admin/GetValuesByVIN")]
        [HttpGet]
        public object GetPricesByVIN(string Token, string VIN, int RegionID, int Mileage)
        {


            TradeInValues carValues = new TradeInValues();
            string url = "";
            string strResponse = "";
            var Validtoken = utilityRespository.ValidationAccesstoken(Token);
            if (Validtoken)
            {
                try
                {
                    url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
                    strResponse = HttpApi.CreateRequest(url);
                    GetMarkUpDataPercent markupPercentData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

                    url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
                    strResponse = HttpApi.CreateRequest(url);
                    GetMarkUpDataFixed markupFixedData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

                    var markup_percent = Convert.ToDecimal(markupPercentData.MarkupData.MarkUpPercent);
                    var markup_fixed = Convert.ToDecimal(markupFixedData.MarkupData.MarkUpFixed);

                    url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/buildVehicleAndValuesByVin?period=" + 0 + "&vin=" + VIN + "&region=" + RegionID + "&mileage=" + Mileage;
                    strResponse = HttpApi.WebRequestNada(url);
                    SearchByVin dataNew = JsonConvert.DeserializeObject<SearchByVin>(strResponse);

                    //var carValue = Vechile.getDefaultVehicleAndValueByVin(uid);
                    var carValue = dataNew.result[0];
                    if (carValue != null)
                    {
                        carValues.MakeId = 1;
                        carValues.MakeName = carValue.make;
                        carValues.Year = carValue.modelyear;
                        carValues.YearId = carValue.modelyear;
                        carValues.Model = carValue.model;
                        carValues.ModelId = 1;
                        carValues.Trim = carValue.trim;
                        carValues.TrimId = carValue.ucgvehicleid;
                        carValues.CleanTradeIn = carValue.adjustedcleantrade;
                        carValues.AverageTradeIn = carValue.adjustedaveragetrade;
                        carValues.RoughTradeIn = carValue.adjustedroughtrade;
                        carValues.RetailTradeIn = carValue.adjustedcleanretail;

                        carValues.MarkUpPercent = markup_percent;
                        carValues.MarkUpFixed = markup_fixed;
                        carValues.Status = true;
                        carValues.Message = "Successfully get TradeIn values";
                    }
                    else
                    {
                        carValues.Status = false;
                        carValues.Message = "Failed to get TradeIn values";
                    }

                    return carValues;
                }
                catch (Exception ex)
                {
                    ErrorHandling.TryCatchExceptionNADA(ex);
                    carValues.Status = false;
                    carValues.Message = "No vehicle found.";

                    return carValues;

                }
            }
            else
            {
                carValues.Status = false;
                carValues.Message = "Invalid Token.";
            }
            return carValues;
        }
        [Route("api/Admin/Regions")]
        [HttpGet]
        public object GetRegions(string token)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            List<object> result = new List<object>();
            var Validtoken = utilityRespository.ValidationAccesstoken(token);
            if (Validtoken)
            {
                url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/regions";
                try
                {
                    strResponse = HttpApi.WebRequestNada(url);
                    GetRegions data = JsonConvert.DeserializeObject<GetRegions>(strResponse);
                    var i = 1;
                    foreach (var item in data.result)
                    {
                        var obj = new { Code = item.regionid, Descr = item.description };
                        result.Add(obj);
                        i++;
                    }
                    objtoSend.status = true;
                    objtoSend.msg = "Successfull";
                    objtoSend.data = result;


                }
                catch (Exception ex)
                {
                    objtoSend.status = false;
                    objtoSend.msg = ex.Message;

                }
            }
            else
            {
                objtoSend.status = false;
                objtoSend.msg = "Invalid Token.";
                objtoSend.data = null;
            }
            return objtoSend;
        }

        [Route("api/Admin/ValidateVin")]
        [HttpGet]
        public object ValidateVin(string token, string Vin)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            List<object> result = new List<object>();
            var Validtoken = utilityRespository.ValidationAccesstoken(token);
            if (Validtoken)
            {
                url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/vehiclesByVin?period=0&vin=" + Vin;
                try
                {
                    strResponse = HttpApi.WebRequestNada(url);
                    ValidateVin data = JsonConvert.DeserializeObject<ValidateVin>(strResponse);
                    if (data.result.Count > 0)
                    {
                        objtoSend.status = true;
                        objtoSend.msg = "valid Vin.";
                        objtoSend.data = data.result;
                    }
                    else
                    {
                        objtoSend.status = false;
                        objtoSend.msg = "Invalid Vin.";
                        objtoSend.data = data.result;
                    }


                }
                catch (Exception ex)
                {
                    objtoSend.status = false;
                    objtoSend.msg = ex.Message;

                }
            }
            else
            {
                objtoSend.status = false;
                objtoSend.msg = "Invalid Token.";
                objtoSend.data = null;
            }
            return objtoSend;
        }

        [Route("api/Employee/SendEmail")]
        [HttpGet]
        public HeaderResponse SendEmail(string ManagerID, string Subject, string Description, string token)
        {
            var Validtoken = utilityRespository.ValidationAccesstoken(token);

            int ID = Convert.ToInt32(ManagerID);
            HeaderResponse Response = new HeaderResponse();
            //if (ModelState.IsValid)
            //{
            vt_BraunAppEntities db = new vt_BraunAppEntities();
            if (Validtoken)
            {



                var User = db.vt_UserProfile.Where(x => x.ID == ID).FirstOrDefault();
                if (User != null)
                {
                    string EMailBody = @"<!doctype html>
<html>
  <head>
    <meta name='viewport' content='width = device-width'>
        <meta http-equiv = 'Content-Type' content = 'text/html; charset=UTF-8'>
         
             <title> Simple Transactional Email</title>
            
                <style>
            

                .wrapper{
                        background-image: url(http://braunability.clientdemos.me/assets/images/nav.png);
                        background-size: contain;
                        background-repeat: no-repeat;
                    }
    .footer{
                        background-image: linear-gradient(to right, #3badcb , #233b7d);
    color: white;
                    }
    .btn-primary table td:hover {
                        background-color: #ffffff !important;
}
                    @media only screen and (max-width: 620px) {
                        table[class=body] h1 {
        font-size: 28px !important;
        margin-bottom: 10px !important;
      }
    table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
        font-size: 16px !important;
      }
table[class=body] .wrapper,
            table[class=body] .article {
        padding: 10px !important;
      }
      table[class=body] .content {
        padding: 0 !important;
      }
      table[class=body] .container {
        padding: 0 !important;
        width: 100% !important;
      }
      table[class=body] .main {
        border-left-width: 0 !important;
        border-radius: 0 !important;
        border-right-width: 0 !important;
      }
      table[class=body] .btn table
{
    width: 100% !important;
}
table[class=body] .btn a
{
    width: 100% !important;
}
table[class=body] .img-responsive {
        height: auto !important;
        max-width: 100% !important;
        width: auto !important;
      }
    }

    
    @media all
{
      .ExternalClass
    {
    width: 100 %;
    }
      .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div
    {
        line-height: 100 %;
    }
      .apple-link a
    {
    color: inherit !important;
        font-family: inherit !important;
        font-size: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
        text-decoration: none !important;
    }
      .btn-primary table td:hover
    {
        background-color: #34495e !important;
      }
      .btn-primary a:hover
    {
        background-color: #34495e !important;
        border-color: #34495e !important;
      }
}
    </style>
  </head>
  <body class='' style='background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;'>
    <table border = '0' cellpadding='0' cellspacing='0' class='body' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background-color: #f6f6f6;'>
     
      <tr>
        <td style = 'font-family: sans-serif; font-size: 14px; vertical-align: top;'> &nbsp;</td>
        <td class='container' style='font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; Margin: 0 auto; max-width: 580px; padding: 10px; max-width: 580px;'>
          <div class='content' style='box-sizing: border-box; display: block; Margin: 0 auto; max-width: 580px; padding: 10px;'>
           

            <!-- START CENTERED WHITE CONTAINER -->
            <span class='preheader' style='color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;'></span>
            <table class='main' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background: #ffffff; border-radius: 3px;'>

              <!-- START MAIN CONTENT AREA -->
              <tr>
                <td class='wrapper' style='font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;background-image: url(http://braunability.clientdemos.me/assets/images/nav.png);background-size: contain;background-repeat: no-repeat;'>
                  <table border = '0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;'>
                    <tr>
                      <td>
                        <img src = 'http://braunability.clientdemos.me/assets/images/login-logo.png' alt='' style='max-width:200px;margin-left: 30px;margin-top: 15px;'>
                      </td>
                    </tr>
                    <tr style = 'height:60px;'></ tr>
                    <tr>
                      <td style='font-family: sans-serif; font-size: 14px; vertical-align: top;'>
                        <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;font-weight: bold;font-size: 18px;'> Hi " + User.UserName + @",</p>
                        <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>" + Description + @"</p>
                        
                        <table border = '0' cellpadding= '0' cellspacing= '0' class='btn btn-primary' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; box-sizing: border-box;'>
                          <tbody>
                            <tr>
                              <td align = 'left' style='font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;'>
                                <table border = '0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;'>
                                  <tbody>
                                    <tr>
                                      <td style = 'font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;background-color:#ffff!important'> <a href='http://braunability.clientdemos.me' target='_blank' style='display: inline-block; color: #ffffff;  background-image: linear-gradient(to right, #3badcb , #233b7d); border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #3498db;border-radius: 30px;'>Login</a> </td>
                                    </tr>
                                  </tbody>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'> In case of any difficulty kindly contact site admin.</p>
    <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'> Best Regards,</p>                    
    <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;font-weight: bold;font-size: 18px;'>Braunability Support Team</p>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </table>

            <!-- START FOOTER -->
            <div class='footer' style='clear: both;  text-align: center; width: 100%;background-image: linear-gradient(to right, #3badcb , #233b7d);color: white;'>
              <table border = '0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;'>
                <tr>
                  <td class='content-block' style='font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px;  text-align: center;'>
                    <span class='apple-link' style=' font-size: 12px; text-align: center;'>Braunability Inc, 631 West 11th Street-Winamac, IN 46996 </span>
                    <br>Don't reply to this email.
                  </td>
                </tr>
                <tr>
                  <td class='content-block powered-by' style='font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px;  text-align: center;'>
                    Powered by<a href='https://www.nada.com/' style=' font-size: 12px; text-align: center; text-decoration: none;color: white;'> NADA</a>.
                  </td>
                </tr>
              </table>
            </div>
            <!-- END FOOTER -->

          <!-- END CENTERED WHITE CONTAINER -->
          </div>
        </td>
        <td style = 'font-family: sans-serif; font-size: 14px; vertical-align: top;'>&nbsp;</td>
      </tr>
    </table>
  </body>
</html>
";
                    // string EMailBody = "<html></html> UserName: "+email+ "\n"+ "Password: "+ encryptPassword;
                    WebMail.SmtpServer = "smtp.gmail.com";
                    //gmail port to send emails  
                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = true;
                    //sending emails with secure protocol  
                    WebMail.EnableSsl = true;
                    //EmailId used to send emails from application  
                    WebMail.UserName = "braunabilitypvt@gmail.com";
                    WebMail.Password = "@a_12345";

                    //Sender email address.  
                    WebMail.From = "braunabilitypvt@gmail.com";

                    //Send email  
                    WebMail.Send(to: User.Email, subject: Subject, body: EMailBody, cc: null, bcc: null, isBodyHtml: true);


                    Response.status = true;
                    Response.message = "Email send successfully";

                }
                else
                {
                    Response.status = false;
                    Response.message = "Email in Null.";
                }

                //}
                //else
                //{
                //    Response.status = false;
                //    Response.message = "Invalid Parameters.";
                //}

            }
            else
            {
                Response.status = false;
                Response.message = "Invalid Token";
            }
            return Response;
        }

    }
}
