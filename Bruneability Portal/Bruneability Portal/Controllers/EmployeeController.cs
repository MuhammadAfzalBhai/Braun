using Braunability_ViewModal.Model;
using BraunApp_ViewModel.Model;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Braunability_ViewModal.Model.BraunVM_Request;
using static Braunability_ViewModal.Model.BraunVM_Response;

namespace Bruneability_Portal.Controllers
{
    public class EmployeeController : BaseController
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        #region New Qoute Form

        public ActionResult DownloadPricingDetail(string PrintData, string printSection,string [] ConNameOrAmount,string[] Deductionsdesc)
        {
            try
            {
                LocalReport localreport = new LocalReport();
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                var data = JsonConvert.DeserializeObject<CleanPrintData>(PrintData);
                var conversionlist = ConNameOrAmount[0].Split(',');
                var deductionList = Deductionsdesc[0].Split(',');
                char[] charsToTrim = { '.', '0' };
               
                string cleanString = data.cleanVal.Trim(charsToTrim);
                string avgString = data.avgVal.Trim(charsToTrim);
                string roughString = data.roughVal.Trim(charsToTrim);
                string retailString = data.retailVal.Trim(charsToTrim);
                if (printSection == "CleanSec")
                {
                    if (conversionlist.Length > 0)
                    {
                        DataTable DataTable1 = new DataTable("DataTable1");
                        DataTable1.Columns.Add("ConversionName");
                        DataTable1.Columns.Add("Price");
                        DataTable1.Columns.Add("DeductionName");
                        

                        int j = 0;
                        int k = 0;
                        int sno = 1;
                        for (int i = 1; i < conversionlist.Length; i += 2, j += 2, sno++,k++)
                        {
                            DataTable1.Rows.Add(Convert.ToString(conversionlist[i]), "$ " + Convert.ToString(conversionlist[j]), Convert.ToString(deductionList[k]));

                        }
                       
                        //DataTable1.Columns.Add("DeductionName");                     
                       
                        

                        DataSet DataSet1 = new DataSet("DataSet1");
                        DataSet1.Tables.Add(DataTable1);

                        ReportDataSource reportDS = new ReportDataSource();
                        reportDS.Name = "DataSet1";
                        reportDS.Value = DataSet1.Tables[0];
                        localreport.DataSources.Add(reportDS);
                    }

                    reportParameters.Add(new ReportParameter("ReportParameter1", data.yearVal));
                    reportParameters.Add(new ReportParameter("ReportParameter2", data.makeVal));
                    reportParameters.Add(new ReportParameter("ReportParameter3", data.modelVal +" "+data.trimVal));
                    reportParameters.Add(new ReportParameter("ReportParameter4", cleanString));
                    reportParameters.Add(new ReportParameter("ReportParameter5", data.conversionVal));
                    reportParameters.Add(new ReportParameter("ReportParameter6", data.totalCleanVal));
                    DataTable DataTable2 = new DataTable("DataTable2");
                    DataSet DataSet2 = new DataSet("DataSet2");
                   

                    

               
                    
                    localreport.ReportPath = Server.MapPath("~/PdfPrint/ForClean.rdlc");
                    localreport.SetParameters(reportParameters);

                    string mimeType;
                    string encoding;
                    string FileNameExtention;


                    FileNameExtention = "pdf";


                    string[] streams;
                    Warning[] warnings;
                    byte[] renderbyte;
                    renderbyte = localreport.Render("pdf", "", out mimeType, out encoding, out FileNameExtention, out streams, out warnings);
                    Response.AddHeader("content-disposition", "attachment;filename=C.T_"+ data.makeVal+"_"+DateTime.Now.ToShortDateString()+"." + FileNameExtention);

                    return File(renderbyte, FileNameExtention);
                }

                else if (printSection == "AvgSec")
                {
                   
                    if (conversionlist.Length>0)
                    {
                        DataTable DataTable1 = new DataTable("DataTable1");
                        DataTable1.Columns.Add("ConversionName");
                        DataTable1.Columns.Add("Price");
                        DataTable1.Columns.Add("DeductionName");
                        int k = 0;
                        int j = 0;
                        int sno = 1;
                        for (int i = 1; i < conversionlist.Length; i += 2, j += 2, sno++, k++)
                        {
                            DataTable1.Rows.Add(Convert.ToString(conversionlist[i]), "$ " + Convert.ToString(conversionlist[j]), Convert.ToString(deductionList[k]));

                        }


                        DataSet DataSet1 = new DataSet("DataSet1");
                        DataSet1.Tables.Add(DataTable1);
                        ReportDataSource reportDS = new ReportDataSource();
                        
                        reportDS.Name = "DataSet1";
                        reportDS.Value = DataSet1.Tables[0];
                        localreport.DataSources.Add(reportDS);
                    }
                   
                   

                    reportParameters.Add(new ReportParameter("ReportParameter1", data.yearVal));
                    reportParameters.Add(new ReportParameter("ReportParameter2", data.makeVal));
                    reportParameters.Add(new ReportParameter("ReportParameter3", data.modelVal + " " + data.trimVal));
                    reportParameters.Add(new ReportParameter("ReportParameter4", avgString));
                    reportParameters.Add(new ReportParameter("ReportParameter5", data.conversionVal));
                    reportParameters.Add(new ReportParameter("ReportParameter6", data.totalAvgVal));


                 
                    localreport.ReportPath = Server.MapPath("~/PdfPrint/ForAvg.rdlc");
                    localreport.SetParameters(reportParameters);

                    string mimeType;
                    string encoding;
                    string FileNameExtention;


                    FileNameExtention = "pdf";


                    string[] streams;
                    Warning[] warnings;
                    byte[] renderbyte;
                    renderbyte = localreport.Render("pdf", "", out mimeType, out encoding, out FileNameExtention, out streams, out warnings);
                    Response.AddHeader("content-disposition", "attachment;filename=A.T_" + data.makeVal + "_" + DateTime.Now.ToShortDateString() + "." + FileNameExtention);

                    return File(renderbyte, FileNameExtention);
                }

                else if (printSection == "RoughSec")
                {
                    if (conversionlist.Length > 0)
                    {
                        DataTable DataTable1 = new DataTable("DataTable1");
                        DataTable1.Columns.Add("ConversionName");
                        DataTable1.Columns.Add("Price");
                        DataTable1.Columns.Add("DeductionName");
                        int k = 0;
                        int j = 0;
                        int sno = 1;
                        for (int i = 1; i < conversionlist.Length; i += 2, j += 2, sno++, k++)
                        {
                            DataTable1.Rows.Add(Convert.ToString(conversionlist[i]), "$ " + Convert.ToString(conversionlist[j]), Convert.ToString(deductionList[k]));

                        }
                        DataSet DataSet1 = new DataSet("DataSet1");
                        DataSet1.Tables.Add(DataTable1);

                        ReportDataSource reportDS = new ReportDataSource();
                        reportDS.Name = "DataSet1";
                        reportDS.Value = DataSet1.Tables[0];
                        localreport.DataSources.Add(reportDS);
                    }

                    reportParameters.Add(new ReportParameter("ReportParameter1", data.yearVal));
                    reportParameters.Add(new ReportParameter("ReportParameter2", data.makeVal));
                    reportParameters.Add(new ReportParameter("ReportParameter3", data.modelVal + " " + data.trimVal));
                    reportParameters.Add(new ReportParameter("ReportParameter4", roughString));
                    reportParameters.Add(new ReportParameter("ReportParameter5", data.conversionVal));
                    reportParameters.Add(new ReportParameter("ReportParameter6", data.totalRoughVal));


                 
                    localreport.ReportPath = Server.MapPath("~/PdfPrint/ForRough.rdlc");
                    localreport.SetParameters(reportParameters);

                    string mimeType;
                    string encoding;
                    string FileNameExtention;


                    FileNameExtention = "pdf";


                    string[] streams;
                    Warning[] warnings;
                    byte[] renderbyte;
                    renderbyte = localreport.Render("pdf", "", out mimeType, out encoding, out FileNameExtention, out streams, out warnings);
                    Response.AddHeader("content-disposition", "attachment;filename=R.T_" + data.makeVal + "_" + DateTime.Now.ToShortDateString() + "." + FileNameExtention);

                    return File(renderbyte, FileNameExtention);
                }

                else
                {
                    if (conversionlist.Length > 0)
                    {
                        DataTable DataTable1 = new DataTable("DataTable1");
                        DataTable1.Columns.Add("ConversionName");
                        DataTable1.Columns.Add("Price");
                        DataTable1.Columns.Add("DeductionName");
                        int k = 0;
                        int j = 0;
                        int sno = 1;
                        for (int i = 1; i < conversionlist.Length; i += 2, j += 2, sno++, k++)
                        {
                            DataTable1.Rows.Add(Convert.ToString(conversionlist[i]), "$ " + Convert.ToString(conversionlist[j]), Convert.ToString(deductionList[k]));

                        }
                        DataSet DataSet1 = new DataSet("DataSet1");
                        DataSet1.Tables.Add(DataTable1);

                        ReportDataSource reportDS = new ReportDataSource();
                        reportDS.Name = "DataSet1";
                        reportDS.Value = DataSet1.Tables[0];
                        localreport.DataSources.Add(reportDS);
                    }

                    reportParameters.Add(new ReportParameter("ReportParameter1", data.yearVal));
                    reportParameters.Add(new ReportParameter("ReportParameter2", data.makeVal));
                    reportParameters.Add(new ReportParameter("ReportParameter3", data.modelVal + " " + data.trimVal));
                    reportParameters.Add(new ReportParameter("ReportParameter4", retailString));
                    reportParameters.Add(new ReportParameter("ReportParameter5", data.conversionValwithMarkup));
                    reportParameters.Add(new ReportParameter("ReportParameter6", data.totalRetailVal));

                    Response.Write(localreport.DataSources.ToString());
                    
                    localreport.ReportPath = Server.MapPath("~/PdfPrint/ForRetail.rdlc");
                    Response.Write(localreport.ReportPath + "\n");
                    try
                    {
                        localreport.SetParameters(reportParameters);
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.InnerException);
                    }
                   
                    string mimeType;
                    string encoding;
                    string FileNameExtention;


                    FileNameExtention = "pdf";


                    string[] streams;
                    Warning[] warnings;
                    byte[] renderbyte;
                    Response.Write("Render");
                    renderbyte = localreport.Render("pdf", "", out mimeType, out encoding, out FileNameExtention, out streams, out warnings);
                    Response.Write("Render completed");
                    Response.AddHeader("content-disposition", "attachment;filename=Retail_" + data.makeVal + "_" + DateTime.Now.ToShortDateString() + "." + FileNameExtention);

                    return File(renderbyte, FileNameExtention);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);                               

            }
         

          

        }

        public ActionResult NewQoute()
        {
            EditQouteData dataobj = new EditQouteData();
            //NewQouteForm form = new NewQouteForm();
            //form.customer = new CustomerDetails();
            //form.tradein = new HiddenTradeInValues();
            return View(dataobj);
        }

        public ActionResult EditQoute(int id)
        {
            EditQouteForm Response = new EditQouteForm();
            EditQouteData qouteDataResponse = new EditQouteData();
            string partialhtml = "";
            string strResponse = "";
            string url = "";
            try
            {

                GetbyID qouteheader = new GetbyID();

                qouteheader.token = CurrentUser.accesstoken;
                qouteheader.ID = Convert.ToInt32(id);
                url = utilityRespository.getBaseUrl() + "Employee/GetQouteByID";
                strResponse = HttpApi.CreateRequest(url, qouteheader);
                QouteDetailsData qoute = JsonConvert.DeserializeObject<QouteDetailsData>(strResponse);

                if (qoute.status)
                {
                    JsonResult models =new JsonResult();
                    JsonResult trims = new JsonResult();
                    JsonResult makes = new JsonResult();
                    JsonResult years = new JsonResult();
                    JsonResult regionStateCode = new JsonResult();
                    JsonResult region = new JsonResult();

                    qouteDataResponse.qoute = JsonConvert.SerializeObject(qoute.qouteData);

                    string token = GetLoginToken("braun_631", "braun_631@"); // to get Token

                    // For Makes
                    if (qoute.qouteData.Year > 0)
                    {
                       makes = GetMakeList(qoute.qouteData.Year);
                    }
                    qouteDataResponse.makedata = JsonConvert.SerializeObject(makes.Data);

                    // For Years
                    years = GetYears(token);
                    qouteDataResponse.yearsdata = JsonConvert.SerializeObject(years.Data);

                    //For Models
                    if (qoute.qouteData.YearID > 0 && qoute.qouteData.MakeID > 0)
                    {
                        models = GetSeries_Models(token, qoute.qouteData.YearID, qoute.qouteData.MakeID);
                    }
                    qouteDataResponse.modeldata = JsonConvert.SerializeObject(models.Data);

                    //For Trims
                    if (qoute.qouteData.YearID > 0 && qoute.qouteData.MakeID > 0 && qoute.qouteData.ModelID > 0)
                    {
                        trims = GetTrims(token, qoute.qouteData.YearID, qoute.qouteData.MakeID, qoute.qouteData.ModelID);
                    }
                    qouteDataResponse.trimdata = JsonConvert.SerializeObject(trims.Data);


                    //For Region
                    if (qoute.qouteData.YearID > 0 && qoute.qouteData.MakeID > 0 && qoute.qouteData.ModelID > 0)
                    {
                        region = GetRegions(token);
                    }
                    qouteDataResponse.regiondata = JsonConvert.SerializeObject(region.Data);
                    //For StateCode

                    //if (qoute.qouteData.YearID > 0 && qoute.qouteData.MakeID > 0 && qoute.qouteData.ModelID > 0 && qoute.qouteData.TrimID > 0)
                    //{
                    //    regionStateCode = getRegionbyState(token, qoute.qouteData);
                    //}

                    //For TradeIn Values
                    TradeInValues tradein = new TradeInValues();
                    tradein.CleanTradeIn = Convert.ToDecimal(qoute.qouteData.CleanTrade);
                    tradein.AverageTradeIn = Convert.ToDecimal(qoute.qouteData.AverageTrade);
                    tradein.RoughTradeIn = Convert.ToDecimal(qoute.qouteData.RoughTrade);
                    tradein.RetailTradeIn = Convert.ToDecimal(qoute.qouteData.RetailTrade);
                    tradein.MarkUpPercent = 0;
                    qouteDataResponse.tradeindata = JsonConvert.SerializeObject(tradein);

                    //Setting Partial Value and Retrieving HTML
                    partialhtml = Set_TradeInPartial(tradein.CleanTradeIn, tradein.AverageTradeIn , tradein.RoughTradeIn);

                    //Get All Conversions by QouteID
                    GetbyID qouteconversionheader = new GetbyID();
                    qouteconversionheader.token = CurrentUser.accesstoken;
                    qouteconversionheader.ID = Convert.ToInt32(id);
                    url = utilityRespository.getBaseUrl() + "Employee/GetAllConversionsByQouteID";
                    strResponse = HttpApi.CreateRequest(url, qouteconversionheader);
                    GetConversionDataList conversionData = JsonConvert.DeserializeObject<GetConversionDataList>(strResponse);

                    //Get All Deductions by QouteID
                    GetbyID qoutedeductionheader = new GetbyID();
                    qoutedeductionheader.token = CurrentUser.accesstoken;
                    qoutedeductionheader.ID = Convert.ToInt32(id);
                    url = utilityRespository.getBaseUrl() + "Employee/GetAllDeductionsByQouteID";
                    strResponse = HttpApi.CreateRequest(url, qoutedeductionheader);
                    GetDeductionDataList deductionData = JsonConvert.DeserializeObject<GetDeductionDataList>(strResponse);

                    ////Get MarkUp by QouteID
                    //HeaderToken header = new HeaderToken();
                    //qoutedeductionheader.token = CurrentUser.accesstoken;
                    //qoutedeductionheader.ID = Convert.ToInt32(id);
                    //url = utilityRespository.getBaseUrl() + "Employee/GetAllDeductionsByQouteID";
                    //strResponse = HttpApi.CreateRequest(url, header);
                    //GetDeductionDataList markupData = JsonConvert.DeserializeObject<GetDeductionDataList>(strResponse);

                    qouteDataResponse.conversionslist = JsonConvert.SerializeObject(conversionData);
                    qouteDataResponse.deductionslist = JsonConvert.SerializeObject(deductionData);


                    //if (makes.Data != null)
                    //{
                    //    qouteDataResponse.makedata = JsonConvert.SerializeObject(makes.Data);

                    //    if (years.Data != null)
                    //    {
                    //        qouteDataResponse.yearsdata = JsonConvert.SerializeObject(years.Data);

                    //        var models = GetSeries_Models(token, qoute.qouteData.YearID, qoute.qouteData.MakeID);

                    //        if (models.Data != null)
                    //        {
                    //            qouteDataResponse.modeldata = JsonConvert.SerializeObject(models.Data);
                    //            var trims = GetTrims(token, qoute.qouteData.YearID, qoute.qouteData.MakeID, qoute.qouteData.ModelID);

                    //            if (trims.Data != null)
                    //            {
                    //                qouteDataResponse.trimdata = JsonConvert.SerializeObject(trims.Data);

                    //                if ((qoute.qouteData.Millage == null || qoute.qouteData.Millage == "") && (qoute.qouteData.StateCode == null || qoute.qouteData.StateCode == ""))
                    //                {
                    //                    qouteDataResponse.Status = false;
                    //                    qouteDataResponse.Message = "TradeIn values Record not found";
                    //                }
                    //                else
                    //                {
                    //                    //var millage = Convert.ToInt32(qoute.qouteData.Millage);
                    //                    //var tradein = GetTradeIn(token, qoute.qouteData.TrimID, millage.ToString(), qoute.qouteData.StateCode);
                    //                    //if (tradein.Data != null)
                    //                    //{
                    //                    //    qouteDataResponse.tradeindata = JsonConvert.SerializeObject(tradein.Data);
                    //                    //}
                    //                    TradeInValues tradein = new TradeInValues();
                    //                    tradein.CleanTradeIn = Convert.ToDecimal(qoute.qouteData.CleanTrade);
                    //                    tradein.AverageTradeIn = Convert.ToDecimal(qoute.qouteData.AverageTrade);
                    //                    tradein.RoughTradeIn = Convert.ToDecimal(qoute.qouteData.RoughTrade);
                    //                    tradein.RetailTradeIn = Convert.ToDecimal(qoute.qouteData.RetailTrade);
                    //                    tradein.MarkUpPercent = Convert.ToDecimal(qoute.qouteData);
                    //                    qouteDataResponse.tradeindata = tradein;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                qouteDataResponse.Status = false;
                    //                qouteDataResponse.Message = "Trims DropDown Record Not Found ";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            qouteDataResponse.Status = false;
                    //            qouteDataResponse.Message = "Models DropDown Record Not Found ";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        qouteDataResponse.Status = false;
                    //        qouteDataResponse.Message = "Years DropDown Record Not Found ";
                    //    }

                    //}
                    //else
                    //{
                    //    qouteDataResponse.Status = false;
                    //    qouteDataResponse.Message = "Makes DropDown Record Not Found ";
                    //}

                }

                EditQouteData dataobj = new EditQouteData();

                dataobj.qoute = qouteDataResponse.qoute;
                dataobj.makedata = qouteDataResponse.makedata;
                dataobj.yearsdata = qouteDataResponse.yearsdata;
                dataobj.modeldata = qouteDataResponse.modeldata;
                dataobj.trimdata = qouteDataResponse.trimdata;
                dataobj.tradeindata = qouteDataResponse.tradeindata;
                dataobj.TradeInPartial = partialhtml;
                dataobj.conversionslist = qouteDataResponse.conversionslist;
                dataobj.deductionslist = qouteDataResponse.deductionslist;
                dataobj.regiondata = qouteDataResponse.regiondata;
                //Response.data = dataobj;

                Response.data = JsonConvert.SerializeObject(dataobj);



                return View("NewQoute", dataobj);
            }
            catch (Exception ex)
            {
                qouteDataResponse.Status = false;
                qouteDataResponse.Message = ex.Message;
                return View("NewQoute", Response);
            }

        }

        public string Set_TradeInPartial(decimal CleanTrade, decimal AvgTrade, decimal RoughTrade)
        {
            string partial = @"<div class='row'>
                                 <div class='col-lg-8 m-auto'>
                                    <h4 class='main_hdng_sm'>Vehicle Result</h4>

                                    <div class='text-center'>
      <img class='img-responsive img-car mt-30 mb-20' src='/assets/images/img-car.png'>
 
     
                                    </div>

<input type='hidden' name='CleanTradeIn' id='CleanTradeIn' value='" + CleanTrade + @"' />
<input type='hidden' name='AverageTradeIn' id='AverageTradeIn' value='" + AvgTrade + @"' />
<input type='hidden' name='RoughTradeIn' id='RoughTradeIn' value='"+ RoughTrade + @"' />
<input type='hidden' name='RetailTradeIn' id='RetailTradeIn' value='' />
<input type='hidden' name='MarkUpPercent' id='MarkUpPercent' value='' />
<input type='hidden' name='MarkUpFixed' id='MarkUpFixed' value='' />

<input type='hidden' name='MakeId' id='MakeId' value='' />
<input type='hidden' name='MakeName' id='MakeName' value='' />

<input type='hidden' name='YearId' id='YearId' value='' />
<input type='hidden' name='Year' id='Year' value='' />

<input type='hidden' name='Model' id='Model' value='' />
<input type='hidden' name='ModelId' id='ModelId' value='' />

<input type='hidden' name='Trim' id='Trim' value='' />
<input type='hidden' name='TrimId' id='TrimId' value='' />

<div class='table-responsive'>
        <table class='table table_trade'>
                                        <thead>
                                            <tr>
                                                <th colspan = '2' > Trade-in</th>
                                            </tr>
                                        </thead>

                                        <tbody>
                                            <tr>
                                                <td>Clean Trade</td>
                                                <td align ='right' > $ " + CleanTrade + @"</td>
                                            </tr>
                                            <tr>
                                                <td > Average Trade</td>
                                                <td align = 'right' > $ " + AvgTrade + @" </ td >
                                            </tr>
                                            <tr>
                                                <td> Rough Trade</td>
                                                <td align='right'> $ " + RoughTrade + @"</ td >
                                            </tr>
                                        </tbody>
                                    </table>
                                    </div>
                                 </div>
                            </div>";
            return partial;
        }

        public ActionResult GetConversionModal(int? MakeId)
        {
            ViewBag.MakeId = MakeId;
            return PartialView("_AddConversion");
        }

        [HttpGet]
        public ActionResult GetDep_per_ByMonth(int month,double _conversionamount)
        {
            string jsondata = string.Empty;
            GetDepriciationresult result = new GetDepriciationresult();
            try
            {
                GetbyDepreciation request = new GetbyDepreciation();
                request.token = CurrentUser.accesstoken;
                request.Month = month;
                request.conversionamount = _conversionamount;



                string url = utilityRespository.getBaseUrl() + "Employee/CalDepreciation";
              
                string strResponse = HttpApi.CreateRequest(url, request);

                result = JsonConvert.DeserializeObject<GetDepriciationresult>(strResponse);

               // jsondata = JsonConvert.SerializeObject(result, Formatting.Indented);
                return Json(new { result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllConversions()
        {
            string jsondata = string.Empty;
            GetConversionDataList conversionData = new GetConversionDataList();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                
                //junaid zaheer change in above Line
                string url = utilityRespository.getBaseUrl() + "Employee/GetAllConversions";
                //string url = utilityRespository.getBaseUrl() + "Employee/GetAllConversionsByMakeId";
                string strResponse = HttpApi.CreateRequest(url, request);
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
            }
        }

        public ActionResult GetAllConversionsByMakeId(int? MakeId)
        {
            string jsondata = string.Empty;
            GetConversionDataList conversionData = new GetConversionDataList();
            try
            {
                //  HeaderToken request = new HeaderToken();
                GetbyID request = new GetbyID();
                request.token = CurrentUser.accesstoken;
                request.ID =Convert.ToInt32(MakeId);
                
                //string url = utilityRespository.getBaseUrl() + "Employee/GetAllConversions";
                string url = utilityRespository.getBaseUrl() + "Employee/GetAllConversionsByMakeId";
                string strResponse = HttpApi.CreateRequest(url, request);
                conversionData = JsonConvert.DeserializeObject<GetConversionDataList>(strResponse);

                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSelectedMarkup() {

            string jsondata = string.Empty;
            GetMarkUpValueswithId MarkRecord = new GetMarkUpValueswithId();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Employee/GetSelectedMarkup";
                string response = HttpApi.CreateRequest(url, request);
                MarkRecord = JsonConvert.DeserializeObject<GetMarkUpValueswithId>(response);
                return Json(new { MarkRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
               

             
            }

        }
        public ActionResult GetQouteConversionsDetails(ConversionArray array)
        {
            string jsondata = string.Empty;
            GetQouteConversionDetailList conversionData = new GetQouteConversionDetailList();
            try
            {
                array.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Employee/GetAllQouteConversionDetails";
                string strResponse = HttpApi.CreateRequest(url, array);
                conversionData = JsonConvert.DeserializeObject<GetQouteConversionDetailList>(strResponse);

                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                conversionData.status = false;
                conversionData.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult SaveQoute(QouteRequest qoute)
        {
            HeaderResponse Response = new HeaderResponse();
            string url = string.Empty;
            string strResponse = string.Empty;
            try
            {
                
                

                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                 url = utilityRespository.getBaseUrl() + "Employee/GetSelectedMarkup";
                string response = HttpApi.CreateRequest(url, request);
                GetMarkUpValueswithId MarkRecord = JsonConvert.DeserializeObject<GetMarkUpValueswithId>(response);
                qoute.MarkUpPercent = "0";
                qoute.MarkUpFixed = "0";

                 if (MarkRecord.status)
                {
                    if (MarkRecord.MarkUpFixed > 0)
                    {
                        qoute.MarkUpFixed = Convert.ToString(MarkRecord.MarkUpFixed);

                    }
                    else if (MarkRecord.MarkUpPer > 0)
                    {
                        qoute.MarkUpPercent = Convert.ToString(MarkRecord.MarkUpPer);
                    }
                    qoute.token = CurrentUser.accesstoken;
                    qoute.EmployeeID = Convert.ToInt32(CurrentUser.SessionUser.ID);
                    url = utilityRespository.getBaseUrl() + "Employee/SaveQoute";
                    strResponse = HttpApi.CreateRequest(url, qoute);
                    Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);
                }
                
                //url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
                //strResponse = HttpApi.CreateRequest(url);
                //GetMarkUpDataPercent markupPercentData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

                //url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
                //strResponse = HttpApi.CreateRequest(url);
                //GetMarkUpDataFixed markupFixedData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

                //if (markupPercentData.status && markupFixedData.status)
                //{
                    //qoute.MarkUpPercent = markupPercentData.MarkupData.MarkUpPercent;
                    //qoute.MarkUpFixed = markupFixedData.MarkupData.MarkUpFixed;
                    //qoute.token = CurrentUser.accesstoken;
                    //qoute.EmployeeID = Convert.ToInt32(CurrentUser.SessionUser.ID);
                    //url = utilityRespository.getBaseUrl() + "Employee/SaveQoute";
                    //strResponse = HttpApi.CreateRequest(url, qoute);
                    //Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);
                //}
                //qoute.CleanTrade = qoute.CleanTrade.Replace("$", "");
                //qoute.AverageTrade = qoute.AverageTrade.Replace("$", "");
                //qoute.RoughTrade = qoute.RoughTrade.Replace("$", "");
                //qoute.RetailTrade = qoute.RetailTrade.Replace("$","");
                
                return Json(new { status = Response.status, message = Response.message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateQoute(QouteRequest qoute)
        {
            HeaderResponse Response = new HeaderResponse();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url1 = utilityRespository.getBaseUrl() + "Employee/GetSelectedMarkup";
                string response = HttpApi.CreateRequest(url1, request);
                GetMarkUpValueswithId MarkRecord = JsonConvert.DeserializeObject<GetMarkUpValueswithId>(response);
                qoute.MarkUpPercent = "0";
                qoute.MarkUpFixed = "0";

                if (MarkRecord.status)
                {
                    if (MarkRecord.MarkUpFixed > 0)
                    {
                        qoute.MarkUpFixed = Convert.ToString(MarkRecord.MarkUpFixed);

                    }
                    else if (MarkRecord.MarkUpPer > 0)
                    {
                        qoute.MarkUpPercent = Convert.ToString(MarkRecord.MarkUpPer);
                    }
                    qoute.token = CurrentUser.accesstoken;
                    qoute.QouteID = Convert.ToInt32(qoute.QouteID);
                    string url = utilityRespository.getBaseUrl() + "Employee/UpdateQoute";
                    string strResponse = HttpApi.CreateRequest(url, qoute);
                    Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);
                  
                }
                return Json(new { status = Response.status, message = Response.message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteQouteByID(int ID)
        {
            try
            {
                if (ID != 0)
                {
                    //vt_Deductions deduct = new vt_Deductions();
                    GetbyID qoute = new GetbyID();
                    qoute.token = CurrentUser.accesstoken;
                    qoute.ID = Convert.ToInt32(ID);
                    string url = utilityRespository.getBaseUrl() + "Employee/DeleteQouteByID";
                    string strResponse = HttpApi.CreateRequest(url, qoute);
                    HeaderResponse Response = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);

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


        #endregion


        public ActionResult ListofDraftQuotes() {
            return View();
        }

        public ActionResult GetDraftQoutesData()
        {
            string jsondata = string.Empty;
            GetQoutesDataList conversionData = new GetQoutesDataList();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Employee/GetDraftQoutesData";
                string strResponse = HttpApi.CreateRequest(url, request);
                conversionData = JsonConvert.DeserializeObject<GetQoutesDataList>(strResponse);

                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                conversionData.status = false;
                conversionData.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListofQoute()
        {
            return View();
        }

        public ActionResult GetAllQoutesData()
        {
            string jsondata = string.Empty;
            GetQoutesDataList conversionData = new GetQoutesDataList();
            try
            {
                HeaderToken request = new HeaderToken();
                request.token = CurrentUser.accesstoken;
                string url = utilityRespository.getBaseUrl() + "Employee/GetAllQoutesData";
                string strResponse = HttpApi.CreateRequest(url, request);
                conversionData = JsonConvert.DeserializeObject<GetQoutesDataList>(strResponse);

                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                conversionData.status = false;
                conversionData.message = ex.Message;
                jsondata = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                return Json(new { jsondata }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllQoutesDataByEmployeeId()
        {
            string jsondata = string.Empty;
            GetQoutesDataList ListofQuotes = new GetQoutesDataList();
            try
            {
                GetbyID request = new GetbyID();
                request.token = CurrentUser.accesstoken;
                request.ID  =     CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Employee/GetAllQuotesDataByEmployeeId";
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

       


        public ActionResult GetAllDraftQuotesDataByEmployeeId()
        {
            string jsondata = string.Empty;
            GetQoutesDataList ListofQuotes = new GetQoutesDataList();
            try
            {
                GetbyID request = new GetbyID();
                request.token = CurrentUser.accesstoken;
                request.ID = CurrentUser.SessionUser.ID;
                string url = utilityRespository.getBaseUrl() + "Employee/GetAllDraftQuotesDataByEmployeeId";
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

        public ActionResult SearchQoute()
        {
            return View();
        }


        #region NADA

        private NadaLogin.SecureLoginSoapClient myLogin = new NadaLogin.SecureLoginSoapClient();
        private VehicleService.VehicleSoapClient Vechile = new VehicleService.VehicleSoapClient();


        #region TradeIn Values by Vehicle Details

        private string GetLoginToken(string UserID, string Password)
        {
            NadaLogin.GetTokenRequest myRequest = new NadaLogin.GetTokenRequest();
            myRequest.Username = UserID;
            myRequest.Password = Password;

            return myLogin.getToken(myRequest);
        }

        public JsonResult GetMakeList(int Year)
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

        public JsonResult GetYears(string Token)
        {
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    Token = GetLoginToken("braun_631", "braun_631@");
                }

                var year = new VehicleService.GetYearsRequest();
                year.Period = 0;
                year.VehicleType = VehicleService.VehicleTypes.UsedCar;
                year.Token = Token;

                VehicleService.Lookup_Struc[] carsYear = Vechile.getYears(year);

                var result = new
                {
                    status = true,
                    message = "Successfully get years.",
                    CarsYear = carsYear
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = false,
                    message = ex.Message,
                    CarsYear = ""
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetSeries_Models(string Token, int Year, int MakeCode)
        {
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    Token = GetLoginToken("braun_631", "braun_631@");
                }

                var series = new VehicleService.GetSeriesRequest();
                series.Period = 0;
                series.VehicleType = VehicleService.VehicleTypes.UsedCar;
                series.Token = Token;
                series.Year = Year;
                series.MakeCode = MakeCode;

                VehicleService.Lookup_Struc[] carsSeries = Vechile.getSeries(series);

                var result = new
                {
                    status = true,
                    message = "Successfully get series.",
                    CarsSeries = carsSeries
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = true,
                    message = "Successfully get series.",
                    CarsSeries = ""
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetTrims(string Token, int Year, int MakeCode, int SeriesCode)
        {
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    Token = GetLoginToken("braun_631", "braun_631@");
                }

                var body = new VehicleService.GetBodyUidsRequest();
                body.MakeCode = MakeCode;
                body.Period = 0;
                body.Year = Year;
                body.VehicleType = VehicleService.VehicleTypes.UsedCar;
                body.Token = Token;
                body.SeriesCode = SeriesCode;

                var bodyUIDs = Vechile.getBodyUids(body);

                var result = new
                {
                    status = true,
                    message = "Successfully get trims.",
                    CarsTrim = bodyUIDs
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = false,
                    message = ex.Message,
                    CarsTrim = ""
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public int getRegionbyState(string Token, string StateCode)
        {
            try
            {
                var region = new VehicleService.GetRegionByStateCodeRequest();
                region.Period = 0;
                region.VehicleType = VehicleService.VehicleTypes.UsedCar;
                region.Token = Token;
                region.StateCode = StateCode;

                var CarRegion = Vechile.getRegionByStateCode(region);
                return CarRegion;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public ActionResult GetPrices(string Token, int TrimCode, string Mileage, string StateCode)
        {
            

            TradeInValues carValues = new TradeInValues();
            string url = "";
            string strResponse = "";
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    Token = GetLoginToken("braun_631", "braun_631@");
                }

                var uid = new VehicleService.GetVehicleValuesByUidRequest();
                uid.Uid = TrimCode;
                uid.Token = Token;
                uid.Period = 0;
                uid.Mileage = Convert.ToInt32(Mileage);
                uid.VehicleType = VehicleService.VehicleTypes.UsedCar;
                uid.Region = getRegionbyState(Token, StateCode);


                //HeaderToken request = new HeaderToken();
                //request.token = CurrentUser.accesstoken;
                //string url = utilityRespository.getBaseUrl() + "Employee/GetMarkUpPercent";
                //string strResponse = HttpApi.CreateRequest(url, request);
                //GetMarkUpData markupData = JsonConvert.DeserializeObject<GetMarkUpData>(strResponse);



                url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
                strResponse = HttpApi.CreateRequest(url);
                GetMarkUpDataPercent markupPercentData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

                url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
                strResponse = HttpApi.CreateRequest(url);
                GetMarkUpDataFixed markupFixedData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

                var markup_percent = Convert.ToDecimal(markupPercentData.MarkupData.MarkUpPercent);
                var markup_fixed = Convert.ToDecimal(markupFixedData.MarkupData.MarkUpFixed);

                //var carValue = Vechile.getVehicleValueByUid(uid);

                //if (carValue != null)
                //{
                //   // carValues.MakeName = carValue.TradeIn;
                //    carValues.CleanTradeIn = carValue.TradeIn;
                //    carValues.AverageTradeIn = carValue.AvgTradeIn;
                //    carValues.RoughTradeIn = carValue.RoughTradeIn;
                //    carValues.RetailTradeIn = carValue.Retail;
                //    carValues.MarkUpPercent = markup_percent;
                //    carValues.MarkUpFixed = markup_fixed;
                //    carValues.Status = true;
                //    carValues.Message = "Successfully get TradeIn values";
                //}
                var carValuess = Vechile.getVehicleAndValueByUid(uid);
                if (carValuess != null)
                {
                    carValues.MakeId = carValuess.MakeCode;
                    carValues.MakeName = carValuess.MakeDescr;
                    carValues.Year = carValuess.VehicleYear;
                    carValues.YearId = carValuess.Uid;
                    carValues.Model = carValuess.SeriesDescr;
                    carValues.ModelId = carValuess.SeriesCode;
                    carValues.Trim = carValuess.BodyDescr;
                    carValues.TrimId = carValuess.BodyCode;
                    carValues.CleanTradeIn = carValuess.TradeInPlusVinAccMileage;
                    carValues.AverageTradeIn = carValuess.AvgTradeInPlusVinAccMileage;
                    carValues.RoughTradeIn = carValuess.RoughTradeInPlusVinAccMileage;
                    carValues.RetailTradeIn = carValuess.RetailPlusVinAccMileage;
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

                return PartialView("_TradeInValues", carValues);
            }
            catch (Exception ex)
            {
                carValues.Status = false;
                carValues.Message = ex.Message;

                return PartialView("_TradeInValues", carValues);

            }
        }

        public JsonResult GetTradeIn(string Token, int TrimCode, string Mileage, string StateCode)
        {
            if (string.IsNullOrEmpty(Token))
            {
                Token = GetLoginToken("braun_631", "braun_631@");
            }
            try
            {
                var uid = new VehicleService.GetVehicleValuesByUidRequest();
                uid.Uid = TrimCode;
                uid.Token = Token;
                uid.Period = 0;
                uid.Mileage = Convert.ToInt32(Mileage);
                uid.VehicleType = VehicleService.VehicleTypes.UsedCar;
                uid.Region = getRegionbyState(Token, StateCode);

                var carValue = Vechile.getVehicleValueByUid(uid);

                TradeInValues carValues = new TradeInValues();
                carValues.CleanTradeIn = carValue.TradeIn;
                carValues.AverageTradeIn = carValue.AvgTradeIn;
                carValues.RoughTradeIn = carValue.RoughTradeIn;


                var result = new
                {
                    CarValues = carValues,
                    Message = "Ok"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    Message = ex.Message
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region TradeIn Values by VIN

        public JsonResult ValidateVin(string Token , string Vin)
        {
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    Token = GetLoginToken("braun_631", "braun_631@");
                }

                var vin = new VehicleService.ValidateVinRequest();
                vin.Token = Token;
                vin.VIN = Vin;


                var vinValidate = Vechile.validateVin(vin);

                if (vinValidate)
                {
                    var result = new
                    {
                        status = true,
                        message = "Valid 'VIN' number .",
                        validate = vinValidate
                    };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new
                    {
                        status = false,
                        message = "Enter a Valid 'VIN' number .",
                        validate = vinValidate
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = false,
                    message = ex.Message,
                    validate = false
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetRegions(string Token)
        {
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    Token = GetLoginToken("braun_631", "braun_631@");
                }

                var region = new VehicleService.GetRegionsRequest();
                region.Token = Token;

                var CarRegion = Vechile.getRegions(region);

                var result = new
                {
                    status = true,
                    message = "Successfully get trims.",
                    Regions = CarRegion
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = false,
                    message = ex.Message,
                    Regions = ""
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetPricesByVIN(string Token, string VIN, int RegionID, int Mileage)
        {


            TradeInValues carValues = new TradeInValues();
            string url = "";
            string strResponse = "";
            try
            {
                if (string.IsNullOrEmpty(Token))
                {
                    Token = GetLoginToken("braun_631", "braun_631@");
                }

                var uid = new VehicleService.GetVehicleValuesByVinRequest();

                uid.Token = Token;
                uid.Vin = VIN;
                uid.Region = Convert.ToInt32(RegionID);
                uid.Mileage = Convert.ToInt32(Mileage);

                url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
                strResponse = HttpApi.CreateRequest(url);
                GetMarkUpDataPercent markupPercentData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

                url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
                strResponse = HttpApi.CreateRequest(url);
                GetMarkUpDataFixed markupFixedData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

                var markup_percent = Convert.ToDecimal(markupPercentData.MarkupData.MarkUpPercent);
                var markup_fixed = Convert.ToDecimal(markupFixedData.MarkupData.MarkUpFixed);
               
                               

                var carValue = Vechile.getDefaultVehicleAndValueByVin(uid);

                if (carValue != null)
                {
                    
                    carValues.MakeId = carValue.MakeCode;
                    carValues.MakeName = carValue.MakeDescr;
                    carValues.Year = carValue.VehicleYear;
                    carValues.YearId = carValue.Uid;
                    carValues.Model = carValue.SeriesDescr;
                    carValues.ModelId = carValue.SeriesCode;
                    carValues.Trim = carValue.BodyDescr;
                    carValues.TrimId = carValue.BodyCode;
                    carValues.CleanTradeIn = carValue.TradeInPlusVinAccMileage;
                    carValues.AverageTradeIn = carValue.AvgTradeInPlusVinAccMileage;
                    carValues.RoughTradeIn = carValue.RoughTradeInPlusVinAccMileage;
                    carValues.RetailTradeIn = carValue.RetailPlusVinAccMileage;
                    //carValues.CleanTradeIn = carValue.TradeIn;
                    //carValues.AverageTradeIn = carValue.AvgTradeIn;
                    //carValues.RoughTradeIn = carValue.RoughTradeIn;
                    //carValues.RetailTradeIn = carValue.Retail;
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

                return PartialView("_TradeInValues", carValues);
            }
            catch (Exception ex)
            {
                ErrorHandling.TryCatchExceptionNADA(ex);
                carValues.Status = false;
                carValues.Message = "No vehicle found.";

                return PartialView("_TradeInValues", carValues);

            }
        }

        #endregion

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
                string url = utilityRespository.getBaseUrl() + "Employee/UpdatePassword";
                string strResponse = HttpApi.CreateRequest(url, _changePassword);
                HeaderResponse UpdatedPass = JsonConvert.DeserializeObject<HeaderResponse>(strResponse);
                if (UpdatedPass.status)
                {
                    return RedirectToAction("successMessage", "Employee");
                }
                else
                {
                    return RedirectToAction("errorMessage", "Employee");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("errorMessage", "Employee");
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


    }
}