using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Braunability_ViewModal.Model
{
    public class ResponseData
    {
        public bool status { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }

    public class Datum
    {
        public string Code { get; set; }
        public string Descr { get; set; }
    }
    public class ValidateVinDetails
    {
        public string modelyear { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string body { get; set; }
        public string ucgvehicleid { get; set; }
        public string vid { get; set; }
    }

    public class ValidateVin
    {
        public string requestId { get; set; }
        public List<ValidateVinDetails> result { get; set; }
        public string userinfo { get; set; }
        public string authId { get; set; }
    }
    public class ValidateVinWeb
    {
        public bool status { get; set; }
        public string msg { get; set; }
        public List<ValidateVinDetails> data { get; set; }
    }
    public class ResponseAPI
    {
        public bool status { get; set; }
        public string msg { get; set; }
        public List<Datum> data { get; set; }
    }
    public class NadaRestMakesResponse
    {
        public string requestId { get; set; }
        public List<MakesResult> result { get; set; }
        public string userinfo { get; set; }
        public string authId { get; set; }

    }
    public class MakesResult
    {
        public string make { get; set; }
    }

    public class NadaRestYearResponse
    {
        public string requestId { get; set; }
        public List<YearResult> result { get; set; }
        public string userinfo { get; set; }
        public string authId { get; set; }
        public bool status { get; set; }
        public string msg { get; set; }

    }
    public class YearResult
    {
        public string modelyear { get; set; }
    }

    public class NadaRestModelsResponse
    {
        public string requestId { get; set; }
        public List<ModelsResult> result { get; set; }
        public string userinfo { get; set; }
        public string authId { get; set; }

    }
    public class ModelsResult
    {
        public string model { get; set; }
    }

    public class NadaRestTrimsResponse
    {
        public string requestId { get; set; }
        public List<TrimsResult> result { get; set; }
        public string userinfo { get; set; }
        public string authId { get; set; }

    }
    public class TrimsResult
    {
        public int ucgvehicleid { get; set; }
        public string body { get; set; }
    }

    public class NadaRestRegionByStateCodeResponse
    {
        public string requestId { get; set; }
        public List<RegionByStateCodeResult> result { get; set; }
        public string userinfo { get; set; }
        public string authId { get; set; }

    }
    public class RegionByStateCodeResult
    {
        public int regionid { get; set; }
        public string region { get; set; }
    }

    public class NadaRestVehicleAndValuesByVehicleIdResponse
    {
        public string requestId { get; set; }
        public List<VehicleAndValuesByVehicleIdResult> result { get; set; }
        public string userinfo { get; set; }
        public string authId { get; set; }

    }
    public class VehicleAndValuesByVehicleIdResult
    {
        public int modelyear { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string body { get; set; }
        public int ucgvehicleid { get; set; }
        public string vehicletype { get; set; }
        public string mileageclass { get; set; }
        public string basemsrp { get; set; }
        public string bodytype { get; set; }
        public string doors { get; set; }
        public string trim { get; set; }
        public string trim2 { get; set; }
        public string drivetype { get; set; }
        public string liters { get; set; }
        public string engineconfiguration { get; set; }
        public string cylinders { get; set; }
        public string inductiontype { get; set; }
        public string transmission { get; set; }
        public string fueltype { get; set; }
        public string wheels { get; set; }
        public string curbweight { get; set; }
        public string gvw { get; set; }
        public string gcw { get; set; }
        public string ucgsubsegment { get; set; }
        public string modelnumber { get; set; }
        public string rollupvehicleid { get; set; }
        public string basecleantrade { get; set; }
        public string baseaveragetrade { get; set; }
        public string baseroughtrade { get; set; }
        public string basecleanretail { get; set; }
        public string baseloan { get; set; }
        public string averagemileage { get; set; }
        public string mileageadjustment { get; set; }
        public decimal adjustedcleantrade { get; set; }
        public decimal adjustedaveragetrade { get; set; }
        public decimal adjustedroughtrade { get; set; }
        public decimal adjustedcleanretail { get; set; }
        public string adjustedloan { get; set; }
        public string maxmileageadj { get; set; }
        public string minmileageadj { get; set; }
        public string minadjretail { get; set; }
        public string minadjcleantrade { get; set; }
        public string minadjaveragetrade { get; set; }
        public string minadjroughtrade { get; set; }
        public string minadjloan { get; set; }
        public string minadjretailforloan { get; set; }
        public string userinfo { get; set; }

    }
}
