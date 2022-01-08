using BAL.Repository;
using Braunability_ViewModal.Model;
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
                    result.qouteData = EmpRepo.getSearchQuotesNew(header.Name,header.Model,header.Make,header.Year,header.EmpId);
                   
                    
                    if (result.qouteData.Rows.Count>0)
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
                    result.qouteData = EmpRepo.getSearchQuotesDraft(header.Name, header.Model, header.Make, header.Year,header.EmpId);


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
                    using (vt_BraunAppEntities db = new vt_BraunAppEntities()) {

                        var queryforgetselectedMarkup = db.vt_MarkUp.Where(x => x.Selected == true).FirstOrDefault();

                        obj.Id = queryforgetselectedMarkup.ID;
                        obj.MarkUpPer =Convert.ToDouble(queryforgetselectedMarkup.MarkUpPercent);
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
                   Response.ConversionsList = EmpRepo.GetAllConversionsbymakeId(header.ID);
                    if (Response.ConversionsList.Rows.Count>0)
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
            double calculatedamount =header.conversionamount;
            try
            {
                var validtoken = utilityRespository.ValidationAccesstoken(header.token);

                if (validtoken)
                {
                    int finalmonth=header.Month + 4;
                    var listofabovemonth = db.vt_Depreciation.Where(x => x.AgeInMonths <= finalmonth && x.DeletedAt==null).ToList();
                    foreach (var item in listofabovemonth)
                    {
                        result =Convert.ToDouble( (calculatedamount * Convert.ToDouble(item.DepreciationPercent)) / 100);
                        calculatedamount = calculatedamount - result;

                    }
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

                    if (header.DeductionID != null)
                    {
                        if (header.DeductionID[0].Contains(','))
                        {
                            var ios_ConversionID = header.ConversionID[0].Split(',');
                            var ios_ConversionAmount = header.ConversionAmount[0].Split(',');
                            var ios_DeductionID = header.DeductionID[0].Split(',');
                            var ios_DeductionAmount  = header.DeductionAmount[0].Split(',');
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
                        else {

                            int lengthofDeduction = header.DeductionID.Length;
                            for (int i = 0; i < header.DepericiationAmount.Length; i++)
                            {

                                if (lengthofDeduction > i)
                                {
                                    DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i], Convert.ToInt32(header.DeductionID[i]), header.DeductionAmount[i], header.DepericiationAmount[i]);
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
                    
                    Response.status = true;
                    Response.message = "Qoute Saved Successfully.";

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
                            else {
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
                                for (int i = 0; i < header.DepericiationAmount.Length; i++)
                                {
                                    if (lengthofDeduction > i)
                                    {
                                        DataTable dtDeduction = EmpRepo.AddNewQouteDeduction(QouteID, Convert.ToInt32(header.ConversionID[i]), header.ConversionAmount[i], Convert.ToInt32(header.DeductionID[i]), header.DeductionAmount[i], header.DepericiationAmount[i]);
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
                                else {
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
                                QouteForm.VinNumber= !string.IsNullOrEmpty(item["VinNumber"].ToString()) ? item["VinNumber"].ToString() : string.Empty;
                                QouteForm.RegionVal = !string.IsNullOrEmpty(item["RegionVal"].ToString()) ? item["RegionVal"].ToString() : string.Empty;
                                QouteForm.RegionText = !string.IsNullOrEmpty(item["RegionText"].ToString()) ? item["RegionText"].ToString() : string.Empty;
                                QouteForm.IsClean =Convert.ToBoolean(item["IsClean"]);
                                QouteForm.IsAverage = Convert.ToBoolean(item["IsAverage"]);
                                QouteForm.IsRough = Convert.ToBoolean(item["IsRough"]);
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
                    if (Response.QoutesList.Rows.Count>0)
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
    }
}
