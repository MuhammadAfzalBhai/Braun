using Braunability_ViewModal.Model;
using BraunApp_ViewModel.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Braunability_ViewModal.Model.BraunVM_Request;
using static Braunability_ViewModal.Model.BraunVM_Response;

namespace Bruneability_Portal.Controllers
{
    public class QuoteNewController : BaseController
    {
        BraunSession _Session;
        // GET: QuoteNew
        public ActionResult Index()
        {
            _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
            ViewBag.RoleID = _Session.SessionUser.RoleID;
            var user = _Session.SessionUser.ID;
            //string root = @"~/TempFolder/" + user;
            string root = Server.MapPath("~/TempFolder/" + user);
            System.IO.DirectoryInfo di = new DirectoryInfo(root);
            if (Directory.Exists(root))
            {
                bool isEmpty = !Directory.EnumerateFiles(root).Any();
                if (!isEmpty)
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    Directory.Delete(root);
                }
                else
                {
                    Directory.Delete(root);
                }

            }

            Directory.CreateDirectory(Server.MapPath("~/TempFolder/" + user));

            //Task<string> strResponse;
            //string url = "";
            //var QueryString = new { period = period, modelyear = modelyear };

            //url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/makes?period=0&modelyear=2018";
            //.ConfigureAwait(false).GetAwaiter().GetResult();
            //strResponse = HttpApi.CreateNadaRequest(url);
            //var dx =  Bill().ConfigureAwait(false).GetAwaiter().GetResult();
            EditQouteData dataobj = new EditQouteData();
            return View(dataobj);
        }
        public JsonResult Upload()
        {
            try
            {
                string FileName = "";
                _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
                var user = _Session.SessionUser.ID;
                //string root = @"~/TempFolder/" + user;
                string root = Server.MapPath("~/TempFolder/" + user);
                System.IO.DirectoryInfo di = new DirectoryInfo(root);

                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(Server.MapPath("~/TempFolder/" + user));
                }

                DataTable dta = new DataTable();
                dta.Columns.Add("FileName");
                dta.Columns.Add("Path");
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        var sts = Convert.ToString(Request.Form["Text"]);
                        var file = Request.Files[i];
                        Guid guid = Guid.NewGuid();
                        var fileName = /*guid + "_" + */Path.GetFileName(file.FileName);
                        var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/TempFolder/" + user), fileName);
                        file.SaveAs(path);

                    }
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    DataRow dr = dta.NewRow();
                    dr["FileName"] = file.Name;
                    dr["Path"] = file.FullName;
                    dta.Rows.Add(dr);
                }
                FileName = JsonConvert.SerializeObject(dta);
                return Json(FileName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult AddPricing()
        {
            return PartialView("_AddPricing");
        }
        public ActionResult AddImages()
        {
            return PartialView("_AddImages");
        }
        public JsonResult GetFiles(int id)
        {
            try
            {
                _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
              
                    string FileName = "";
                    
                    var user = _Session.SessionUser.ID;
                //string root = @"~/TempFolder/" + user;
                string root = Server.MapPath("~/TempFolder/" + user);
                System.IO.DirectoryInfo di = new DirectoryInfo(root);
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/TempFolder/" + user));
                    }
                    DataTable dta = new DataTable();
                    dta.Columns.Add("FileName");
                    dta.Columns.Add("Path");
                    foreach (FileInfo file in di.GetFiles())
                    {
                        DataRow dr = dta.NewRow();
                        dr["FileName"] = file.Name;
                        dr["Path"] = file.FullName;
                        dta.Rows.Add(dr);
                    }
                    FileName = JsonConvert.SerializeObject(dta);
                    return Json(FileName, JsonRequestBehavior.AllowGet);
                    
                
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult GetFilesFromdb(int id)
        {
            try
            {
                _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
                string url = "";
                string strResponse = "";
                GetAttachmentsbyID qouteheader = new GetAttachmentsbyID();

                qouteheader.QuoteId = id;
                qouteheader.ID = _Session.SessionUser.ID;
                url = utilityRespository.getBaseUrl() + "Employee/GetAttachments";
                strResponse = HttpApi.CreateRequest(url, qouteheader);

                string files = JsonConvert.SerializeObject(strResponse);
                return Json(strResponse, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult RemoveFiles(string Path)
        {
            _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
            var user = _Session.SessionUser.ID;
            //string root = @"~/TempFolder/" + user;
            string root = Server.MapPath("~/TempFolder/" + user);
            System.IO.DirectoryInfo di = new DirectoryInfo(root);
            string files = root + "/" + Path;
            if (System.IO.File.Exists(files))
            {
                System.IO.File.Delete(files);
            }
            string FileName = "";

            DataTable dta = new DataTable();
            dta.Columns.Add("FileName");
            dta.Columns.Add("Path");
            foreach (FileInfo file in di.GetFiles())
            {
                DataRow dr = dta.NewRow();
                dr["FileName"] = file.Name;
                dr["Path"] = file.FullName;
                dta.Rows.Add(dr);
            }
            FileName = JsonConvert.SerializeObject(dta);
            return Json(FileName, JsonRequestBehavior.AllowGet);
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
<input type='hidden' name='RoughTradeIn' id='RoughTradeIn' value='" + RoughTrade + @"' />
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
                                                <td align ='right' > $ " + CleanTrade.ToString("#,##") + @"</td>
                                            </tr>
                                            <tr>
                                                <td > Average Trade</td>
                                                <td align = 'right' > $ " + AvgTrade.ToString("#,##") + @" </ td >
                                            </tr>
                                            <tr>
                                                <td> Rough Trade</td>
                                                <td align='right'> $ " + RoughTrade.ToString("#,##") + @"</ td >
                                            </tr>
                                        </tbody>
                                    </table>
                                    </div>
                                 </div>
                            </div>";
            return partial;
        }

        public ActionResult Edit(int id)
        {
            _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
            if(_Session.SessionUser.RoleID == 4 || _Session.SessionUser.RoleID == 5 || _Session.SessionUser.RoleID == 3)
            {
                ViewBag.RoleID = _Session.SessionUser.RoleID;
            }
            else
            {
                ViewBag.RoleID = 0;
            }
            
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
                    JsonResult models = new JsonResult();
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
                        //makes = GetMakeList(qoute.qouteData.Year);
                        makes = GetMakes("0", qoute.qouteData.Year.ToString(),CurrentUser.accesstoken);
                    }
                    qouteDataResponse.makedata = JsonConvert.SerializeObject(makes.Data);

                    // For Years
                    years = GetYear("0");
                    qouteDataResponse.yearsdata = JsonConvert.SerializeObject(years.Data);
                    
                    //For Models
                    if (qoute.qouteData.YearID > 0 && qoute.qouteData.MakeID > 0)
                    {
                        models = GetModels("0", qoute.qouteData.Year.ToString(), qoute.qouteData.Make.ToString(),CurrentUser.accesstoken);
                    }
                    qouteDataResponse.modeldata = JsonConvert.SerializeObject(models.Data);

                    //For Trims
                    if (qoute.qouteData.YearID > 0 && qoute.qouteData.MakeID > 0 && qoute.qouteData.ModelID > 0)
                    {
                        trims = GetTrims("0", qoute.qouteData.Year.ToString(), qoute.qouteData.Make.ToString(), qoute.qouteData.Model.ToString(), CurrentUser.accesstoken);
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
                    partialhtml = Set_TradeInPartial(tradein.CleanTradeIn, tradein.AverageTradeIn, tradein.RoughTradeIn);

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



                return View("Index", dataobj);
            }
            catch (Exception ex)
            {
                qouteDataResponse.Status = false;
                qouteDataResponse.Message = ex.Message;
                return View("Index", Response);
            }
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
                    //carValues.MakeId = carValuess.MakeCode;
                    //carValues.MakeName = carValuess.MakeDescr;
                    //carValues.Year = carValuess.VehicleYear;
                    //carValues.YearId = carValuess.Uid;
                    //carValues.Model = carValuess.SeriesDescr;
                    //carValues.ModelId = carValuess.SeriesCode;
                    //carValues.Trim = carValuess.BodyDescr;
                    //carValues.TrimId = carValuess.BodyCode;
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

        //public JsonResult ValidateVin(string Token, string Vin)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(Token))
        //        {
        //            Token = GetLoginToken("braun_631", "braun_631@");
        //        }

        //        var vin = new VehicleService.ValidateVinRequest();
        //        vin.Token = Token;
        //        vin.VIN = Vin;


        //        var vinValidate = Vechile.validateVin(vin);

        //        if (vinValidate)
        //        {
        //            var result = new
        //            {
        //                status = true,
        //                message = "Valid 'VIN' number .",
        //                validate = vinValidate
        //            };

        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            var result = new
        //            {
        //                status = false,
        //                message = "Enter a Valid 'VIN' number .",
        //                validate = vinValidate
        //            };
        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = new
        //        {
        //            status = false,
        //            message = ex.Message,
        //            validate = false
        //        };

        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }

        //}
        public JsonResult ValidateVin(string Token, string Vin)
        {
            try
            {
                //if (string.IsNullOrEmpty(Token))
                //{
                //    Token = GetLoginToken("braun_631", "braun_631@");
                //}

                //var vin = new VehicleService.ValidateVinRequest();
                //vin.Token = Token;
                //vin.VIN = Vin;
                string url;
                string strResponse;
                Token = CurrentUser.accesstoken;
                url = utilityRespository.getBaseUrl() + "Admin/ValidateVin?token=" + Token + "&Vin=" + Vin;
                strResponse = HttpApi.CreateRequest(url);
                var myDetails = JsonConvert.DeserializeObject<ValidateVinWeb>(strResponse);
                //var vinValidate = Vechile.validateVin(vin);


                if (myDetails.data.Count > 0)
                {
                    var result = new
                    {
                        status = true,
                        message = "Valid 'VIN' number .",
                        validate = true
                    };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new
                    {
                        status = false,
                        message = "Enter a Valid 'VIN' number .",
                        validate = false
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
            string url = "";
            string strResponse = "";
            try
            {



                Token = CurrentUser.accesstoken;
                url = utilityRespository.getBaseUrl() + "Admin/Regions?token=" + Token;
                //strResponse = HttpApi.WebRequestNada(url);
                strResponse = HttpApi.CreateRequest(url);
                //var myDetails = JsonConvert.DeserializeObject<TradeInValues>(strResponse);

                //if (string.IsNullOrEmpty(Token))
                //{
                //    Token = GetLoginToken("braun_631", "braun_631@");
                //}

                //var region = new VehicleService.GetRegionsRequest();
                //region.Token = Token;

                //var CarRegion = Vechile.getRegions(region);

                var result = new
                {
                    status = true,
                    message = "Successfully get trims.",
                };

                return Json(strResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = false,
                    message = ex.Message,
                    Regions = ""
                };
                return Json(strResponse, JsonRequestBehavior.AllowGet);
            }


        }
        //public JsonResult GetRegions(string Token)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(Token))
        //        {
        //            Token = GetLoginToken("braun_631", "braun_631@");
        //        }

        //        var region = new VehicleService.GetRegionsRequest();
        //        region.Token = Token;

        //        var CarRegion = Vechile.getRegions(region);

        //        var result = new
        //        {
        //            status = true,
        //            message = "Successfully get trims.",
        //            Regions = CarRegion
        //        };

        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = new
        //        {
        //            status = false,
        //            message = ex.Message,
        //            Regions = ""
        //        };

        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }

        //}
        public ActionResult GetPricesByVIN(string Token, string VIN, int RegionID, int Mileage)
        {


            TradeInValues carValues = new TradeInValues();
            string url = "";
            string strResponse = "";

            Token = CurrentUser.accesstoken;
            url = utilityRespository.getBaseUrl() + "Admin/GetValuesByVIN?token=" + Token + "&VIN=" + VIN + "&RegionID=" + RegionID + "&Mileage=" + Mileage;
            //strResponse = HttpApi.WebRequestNada(url);
            strResponse = HttpApi.CreateRequest(url);
            var myDetails = JsonConvert.DeserializeObject<TradeInValues>(strResponse);
            //dynamic json = Json(strResponse, JsonRequestBehavior.AllowGet);.
            //JsonConvert.DeserializeObject<TradeInValues>(strResponse);
            return PartialView("_TradeInValues", myDetails);
            //try
            //{
            //    if (string.IsNullOrEmpty(Token))
            //    {
            //        Token = GetLoginToken("braun_631", "braun_631@");
            //    }

            //    var uid = new VehicleService.GetVehicleValuesByVinRequest();

            //    uid.Token = Token;
            //    uid.Vin = VIN;
            //    uid.Region = Convert.ToInt32(RegionID);
            //    uid.Mileage = Convert.ToInt32(Mileage);

            //    url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
            //    strResponse = HttpApi.CreateRequest(url);
            //    GetMarkUpDataPercent markupPercentData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

            //    url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
            //    strResponse = HttpApi.CreateRequest(url);
            //    GetMarkUpDataFixed markupFixedData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

            //    var markup_percent = Convert.ToDecimal(markupPercentData.MarkupData.MarkUpPercent);
            //    var markup_fixed = Convert.ToDecimal(markupFixedData.MarkupData.MarkUpFixed);



            //    var carValue = Vechile.getDefaultVehicleAndValueByVin(uid);

            //    if (carValue != null)
            //    {

            //        carValues.MakeId = carValue.MakeCode;
            //        carValues.MakeName = carValue.MakeDescr;
            //        carValues.Year = carValue.VehicleYear;
            //        carValues.YearId = carValue.Uid;
            //        carValues.Model = carValue.SeriesDescr;
            //        carValues.ModelId = carValue.SeriesCode;
            //        carValues.Trim = carValue.BodyDescr;
            //        carValues.TrimId = carValue.BodyCode;
            //        carValues.CleanTradeIn = carValue.TradeInPlusVinAccMileage;
            //        carValues.AverageTradeIn = carValue.AvgTradeInPlusVinAccMileage;
            //        carValues.RoughTradeIn = carValue.RoughTradeInPlusVinAccMileage;
            //        carValues.RetailTradeIn = carValue.RetailPlusVinAccMileage;
            //        //carValues.CleanTradeIn = carValue.TradeIn;
            //        //carValues.AverageTradeIn = carValue.AvgTradeIn;
            //        //carValues.RoughTradeIn = carValue.RoughTradeIn;
            //        //carValues.RetailTradeIn = carValue.Retail;
            //        carValues.MarkUpPercent = markup_percent;
            //        carValues.MarkUpFixed = markup_fixed;
            //        carValues.Status = true;
            //        carValues.Message = "Successfully get TradeIn values";
            //    }
            //    else
            //    {
            //        carValues.Status = false;
            //        carValues.Message = "Failed to get TradeIn values";
            //    }

            //    return PartialView("_TradeInValues", carValues);
            //}
            //catch (Exception ex)
            //{
            //    ErrorHandling.TryCatchExceptionNADA(ex);
            //    carValues.Status = false;
            //    carValues.Message = "No vehicle found.";

            //    return PartialView("_TradeInValues", carValues);

            //}
        }
        //public ActionResult GetPricesByVIN(string Token, string VIN, int RegionID, int Mileage)
        //{


        //    TradeInValues carValues = new TradeInValues();
        //    string url = "";
        //    string strResponse = "";



        //    try
        //    {
        //        if (string.IsNullOrEmpty(Token))
        //        {
        //            Token = GetLoginToken("braun_631", "braun_631@");
        //        }

        //        var uid = new VehicleService.GetVehicleValuesByVinRequest();

        //        uid.Token = Token;
        //        uid.Vin = VIN;
        //        uid.Region = Convert.ToInt32(RegionID);
        //        uid.Mileage = Convert.ToInt32(Mileage);

        //        url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
        //        strResponse = HttpApi.CreateRequest(url);
        //        GetMarkUpDataPercent markupPercentData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

        //        url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
        //        strResponse = HttpApi.CreateRequest(url);
        //        GetMarkUpDataFixed markupFixedData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

        //        var markup_percent = Convert.ToDecimal(markupPercentData.MarkupData.MarkUpPercent);
        //        var markup_fixed = Convert.ToDecimal(markupFixedData.MarkupData.MarkUpFixed);



        //        var carValue = Vechile.getDefaultVehicleAndValueByVin(uid);

        //        if (carValue != null)
        //        {

        //            //carValues.MakeId = carValue.MakeCode;
        //            //carValues.MakeName = carValue.MakeDescr;
        //            //carValues.Year = carValue.VehicleYear;
        //            //carValues.YearId = carValue.Uid;
        //            //carValues.Model = carValue.SeriesDescr;
        //            //carValues.ModelId = carValue.SeriesCode;
        //            //carValues.Trim = carValue.BodyDescr;
        //            //carValues.TrimId = carValue.BodyCode;
        //            carValues.CleanTradeIn = carValue.TradeInPlusVinAccMileage;
        //            carValues.AverageTradeIn = carValue.AvgTradeInPlusVinAccMileage;
        //            carValues.RoughTradeIn = carValue.RoughTradeInPlusVinAccMileage;
        //            carValues.RetailTradeIn = carValue.RetailPlusVinAccMileage;
        //            //carValues.CleanTradeIn = carValue.TradeIn;
        //            //carValues.AverageTradeIn = carValue.AvgTradeIn;
        //            //carValues.RoughTradeIn = carValue.RoughTradeIn;
        //            //carValues.RetailTradeIn = carValue.Retail;
        //            carValues.MarkUpPercent = markup_percent;
        //            carValues.MarkUpFixed = markup_fixed;
        //            carValues.Status = true;
        //            carValues.Message = "Successfully get TradeIn values";
        //        }
        //        else
        //        {
        //            carValues.Status = false;
        //            carValues.Message = "Failed to get TradeIn values";
        //        }

        //        return PartialView("_TradeInValues", carValues);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandling.TryCatchExceptionNADA(ex);
        //        carValues.Status = false;
        //        carValues.Message = "No vehicle found.";

        //        return PartialView("_TradeInValues", carValues);

        //    }
        //}

        #endregion

        #endregion


        #region NadaRest


        #region TradeIn Values By Vehicle Details

        //public ActionResult WebRequest()
        //{
        //    const string WEBSERVICE_URL = "https://cloud.jdpower.ai/data-api/UAT/valuationservices/valuation/makes?period=0&modelyear=2019";
        //    try
        //    {
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //        var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
        //        if (webRequest != null)
        //        {
        //            webRequest.Method = "GET";
        //            webRequest.Timeout = 12000;
        //            webRequest.ContentType = "application/json";
        //            webRequest.Headers.Add("api-key", "7139e338-debb-473e-9f72-ca8bf2bec7ce");

        //            using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
        //            {
        //                using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
        //                {
        //                    var jsonResponse = sr.ReadToEnd();
        //                    //Console.WriteLine(String.Format("Response: {0}", jsonResponse));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine(ex.ToString());
        //    }
        //    return View();
        //}


        [HttpGet]
        public ActionResult GetValues(string period, string ucgvehicleid, string mileage, string statecode,string token)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            List<object> result = new List<object>();
            TradeInValues carValues = new TradeInValues();

            token = CurrentUser.accesstoken;
            url = utilityRespository.getBaseUrl() + "Admin/GetValues?period=" + period + "&ucgvehicleid=" + ucgvehicleid + "&statecode=" + statecode + "&mileage=" + mileage + "&token=" + token;
            //strResponse = HttpApi.WebRequestNada(url);
            strResponse = HttpApi.CreateRequest(url);
            var myDetails = JsonConvert.DeserializeObject<TradeInValues>(strResponse);
            //dynamic json = Json(strResponse, JsonRequestBehavior.AllowGet);.
            //JsonConvert.DeserializeObject<TradeInValues>(strResponse);
            return PartialView("_TradeInValues", myDetails);

            //url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/regionIdByStateCode?statecode=" + statecode;
            //try
            //{
            //    strResponse = HttpApi.WebRequestNada(url);
            //    NadaRestRegionByStateCodeResponse data = JsonConvert.DeserializeObject<NadaRestRegionByStateCodeResponse>(strResponse);
            //    var regionid = data.result[0].regionid;


            //    url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpPercent";
            //    strResponse = HttpApi.CreateRequest(url);
            //    GetMarkUpDataPercent markupPercentData = JsonConvert.DeserializeObject<GetMarkUpDataPercent>(strResponse);

            //    url = utilityRespository.getBaseUrl() + "Admin/GetMarkUpFixed";
            //    strResponse = HttpApi.CreateRequest(url);
            //    GetMarkUpDataFixed markupFixedData = JsonConvert.DeserializeObject<GetMarkUpDataFixed>(strResponse);

            //    var markup_percent = Convert.ToDecimal(markupPercentData.MarkupData.MarkUpPercent);
            //    var markup_fixed = Convert.ToDecimal(markupFixedData.MarkupData.MarkUpFixed);




            //    url = utilityRespository.getNadaRestApiBaseURL() + "/valuation/vehicleAndValueByVehicleId?period=" + period + "&ucgvehicleid=" + ucgvehicleid + "&region=" + regionid + "&mileage=" + mileage;
            //    strResponse = HttpApi.WebRequestNada(url);
            //    NadaRestVehicleAndValuesByVehicleIdResponse dataNew = JsonConvert.DeserializeObject<NadaRestVehicleAndValuesByVehicleIdResponse>(strResponse);



            //    var carValuess = dataNew.result[0];
            //    if (carValuess != null)
            //    {
            //        carValues.MakeId = 1;
            //        carValues.MakeName = carValuess.make;
            //        carValues.Year = carValuess.modelyear;
            //        carValues.YearId = carValuess.modelyear;
            //        carValues.Model = carValuess.model;
            //        carValues.ModelId = 1;
            //        carValues.Trim = carValuess.trim;
            //        carValues.TrimId = carValuess.ucgvehicleid;
            //        carValues.CleanTradeIn = carValuess.adjustedcleantrade;
            //        carValues.AverageTradeIn = carValuess.adjustedaveragetrade;
            //        carValues.RoughTradeIn = carValuess.adjustedroughtrade;
            //        carValues.RetailTradeIn = carValuess.adjustedcleanretail;
            //        carValues.MarkUpPercent = markup_percent;
            //        carValues.MarkUpFixed = markup_fixed;
            //        carValues.Status = true;
            //        carValues.Message = "Successfully get TradeIn values";
            //    }
            //    else
            //    {
            //        carValues.Status = false;
            //        carValues.Message = "Failed to get TradeIn values";
            //    }

            //    return PartialView("_TradeInValues", carValues);


            //    //var i = 1;
            //    //foreach (var item in data.result)
            //    //{
            //    //    var obj = new { Code = item.regionid, Descr = item.region };
            //    //    result.Add(obj);
            //    //    i++;
            //    //}
            //    //objtoSend.status = true;
            //    //objtoSend.msg = "Successfull";
            //    //objtoSend.data = result;
            //    //return Json(objtoSend, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    carValues.Status = false;
            //    carValues.Message = ex.Message;

            //    return PartialView("_TradeInValues", carValues);
            //}
        }




        [HttpGet]
        public JsonResult GetTrims(string period, string modelyear, string make, string model,string token)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            List<object> result = new List<object>();
            token = CurrentUser.accesstoken;
            url = utilityRespository.getBaseUrl() + "Admin/GetTrims?period=" + period + "&modelyear=" + modelyear + "&make=" + make + "&model=" + model+ "&token=" + token;
            strResponse = HttpApi.CreateRequest(url);
            //var myDetails = JsonConvert.DeserializeObject<ResponseAPI>(strResponse);
            return Json(strResponse, JsonRequestBehavior.AllowGet);

            //try
            //{
            //    strResponse = HttpApi.WebRequestNada(url);
            //    NadaRestTrimsResponse data = JsonConvert.DeserializeObject<NadaRestTrimsResponse>(strResponse);
            //    var i = 1;
            //    foreach (var item in data.result)
            //    {
            //        var obj = new { Code = item.ucgvehicleid, Descr = item.body };
            //        result.Add(obj);
            //        i++;
            //    }
            //    objtoSend.status = true;
            //    objtoSend.msg = "Successfull";
            //    objtoSend.data = result;
            //    return Json(objtoSend, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    objtoSend.status = false;
            //    objtoSend.msg = ex.Message;
            //    return Json(objtoSend, JsonRequestBehavior.AllowGet);
            //}
        }

        

        [HttpGet]
        public JsonResult GetModels(string period, string modelyear, string make,string token)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            List<object> result = new List<object>();
            token = CurrentUser.accesstoken;

            url = utilityRespository.getBaseUrl() + "/Admin/GetModels?period=" + period + "&modelyear=" + modelyear + "&make=" + make +"&token=" + token;
            strResponse = HttpApi.CreateRequest(url);
            //var myDetails = JsonConvert.DeserializeObject<ResponseAPI>(strResponse);

            return Json(strResponse, JsonRequestBehavior.AllowGet);
            //try
            //{
            //    strResponse = HttpApi.WebRequestNada(url);
            //    NadaRestModelsResponse data = JsonConvert.DeserializeObject<NadaRestModelsResponse>(strResponse);
            //    var i = 1;
            //    foreach (var item in data.result)
            //    {
            //        var obj = new { Code = item.model, Descr = item.model };
            //        result.Add(obj);
            //        i++;
            //    }
            //    objtoSend.status = true;
            //    objtoSend.msg = "Successfull";
            //    objtoSend.data = result;
            //    return Json(objtoSend, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    objtoSend.status = false;
            //    objtoSend.msg = ex.Message;
            //    return Json(objtoSend, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpGet]
        public JsonResult GetMakes(string period, string modelyear,string token)
        {
            string strResponse;
            string url = "";
            ResponseAPI objtoSend = new ResponseAPI();
            List<object> result = new List<object>();
            token = CurrentUser.accesstoken;
            url = utilityRespository.getBaseUrl() + "Admin/GetMakes?period=" + period + "&modelyear=" + modelyear + "&token="+token;
            strResponse = HttpApi.CreateRequest(url);

            //var myDetails = JsonConvert.DeserializeObject<ResponseAPI>(strResponse);

            return Json(strResponse, JsonRequestBehavior.AllowGet);
            //return Json(strResponse, JsonRequestBehavior.AllowGet);
            //try
            //{
            //    strResponse = HttpApi.WebRequestNada(url);
            //    NadaRestMakesResponse data = JsonConvert.DeserializeObject<NadaRestMakesResponse>(strResponse);
            //    var i = 1;
            //    foreach (var item in data.result)
            //    {
            //        var obj = new { Code = item.make, Descr = item.make };
            //        result.Add(obj);
            //        i++;
            //    }
            //    objtoSend.status = true;
            //    objtoSend.msg = "Successfull";
            //    objtoSend.data = result;
            //    return Json(objtoSend, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    objtoSend.status = false;
            //    objtoSend.msg = ex.Message;
            //    return Json(objtoSend, JsonRequestBehavior.AllowGet);
            //}   
        }


        [HttpGet]
        public JsonResult GetYear(string period)
        {
            string strResponse;
            string url = "";
            ResponseData objtoSend = new ResponseData();
            HeaderTokenForSearch headers = new HeaderTokenForSearch();
            List<object> result = new List<object>();
            headers.period = period;
            headers.token = CurrentUser.accesstoken;


            url = utilityRespository.getBaseUrl() + "Admin/GetYear";
            strResponse = HttpApi.CreateRequest(url, headers);
            //ResponseData data = JsonConvert.DeserializeObject<ResponseData>(strResponse);

            //objtoSend.status = data.status;
            //objtoSend.msg = data.msg;
            //objtoSend.data = data.data;
            //dynamic data = JsonConvert.DeserializeObject<ResponseData>(strResponse);
            //var myDetails = JsonConvert.DeserializeObject<ResponseAPI>(strResponse);
            return Json(strResponse, JsonRequestBehavior.AllowGet);




            //url = utilityRespository.getBaseUrl() + "/years?period=" + period;
            //try
            //{
            //    strResponse = HttpApi.WebRequestNada(url);
            //NadaRestYearResponse data = JsonConvert.DeserializeObject<NadaRestYearResponse>(strResponse);
            //    var i = 1;
            //    foreach (var item in data.result)
            //    {
            //        var obj = new { Code = item.modelyear, Descr = item.modelyear };
            //        result.Add(obj);
            //        i++;
            //    }
            //    objtoSend.status = true;
            //    objtoSend.msg = "Successfull";
            //    objtoSend.data = result;
            //    return Json(objtoSend, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    objtoSend.status = false;
            //    objtoSend.msg = ex.Message;
            //    return Json(objtoSend, JsonRequestBehavior.AllowGet);
            //}
        }






        #endregion

        #endregion

    }
}