using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBEntities;
using System.Web.Mvc;
using System.Data;

namespace Braunability_ViewModal.Model
{
    public class BraunVM_Request
    {
        public class HeaderToken
        {
            public int roleID { get; set; }
            public int UserID { get; set; }
            public string token { get; set; }
        }
        public class HeaderTokenForSearch
        {
            public string period { get; set; }
            public string token { get; set; }
        }


        public class GetbyID
        {
            public int ID { get; set; }
            public int TierId { get; set; }
            public string token { get; set; }
        }

        public class Approvebyid
        {
            public int ID { get; set; }
            public string Comment { get; set; }
            public string token { get; set; }
        }

        public class Rejectbyid
        {
            public int ID { get; set; }
            public string Comment { get; set; }
            public string token { get; set; }
        }
        public class GetAttachmentsbyID
        {
            public int ID { get; set; }
            public int QuoteId { get; set; }

            public string Files { get; set; }
        }




        public class Betweentwodates
        {
            public string start { get; set; }
            public string end { get; set; }
            public string token { get; set; }
        }
        public class GetbyDepreciation
        {
            public int Amount { get; set; }
            public int Month { get; set; }
            public double  conversionamount { get; set; }
            public string token { get; set; }
        }

        public class LoginRequest
        {
            //[System.Web.Mvc.Remote("CheckExistingEmail", "Admin", HttpMethod = "POST", ErrorMessage = "Email already exists!")]
            [Display(Name = "Email Address")]
            [Required(ErrorMessage = "Email address is required.")]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid,")]
            public string Email { get; set; }

            //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter numeric value.")]
            [MinLength(5 , ErrorMessage = "Minimun Length 5 digits.")]
            //[StringLength(12, ErrorMessage = "Maximum Length 12 digits.")]
            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }
            public bool Isportal { get; set; }

            public bool Status { get; set; }
            public string Message { get; set; }
        }
        


        public class EmployeeForm
        {
            public int ID { get; set; }
            [Required(ErrorMessage = "User Name is required")]
            public string UserName { get; set; }
            //[Remote("CheckExistingEmail", "Admin", HttpMethod = "POST", ErrorMessage = "Coupon already exists!")]
            [Display(Name = "Email Address")]
            [Required(ErrorMessage = "Email Address is required.")]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
            public string Email { get; set; }
            public int RoleID { get; set; }
            public SelectList RoleList;

            [Required(ErrorMessage = "Joining Date is required")]
           
            public string JoiningDate { get; set; }

            //[RegularExpression(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$", ErrorMessage = "Phone is not valid.")]
            //[Required(ErrorMessage = "Contact is required")]
            public string Contact { get; set; }

            [Required(ErrorMessage = "City is required")]
            public string City { get; set; }

            [Required(ErrorMessage = "State is required")]
            public string State { get; set; }

            [Required(ErrorMessage = "CompanyName is required")]
            public string CompanyName { get; set; }

            //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter numeric value.")]
            [MinLength(5, ErrorMessage = "Minimun Length 5 digits")]
            //[StringLength(12, ErrorMessage = "Maximum Length 12 digits")]
            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }
            public bool IsApproved { get; set; }
            public bool IsActive { get; set; }
            public string token { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }

            public string Managerstatus { get; set; }


            public int UserId { get; set; }
            //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter numeric value.")]
        }
        public class AddNewMake
        {
            public int makeID { get; set; }
            public string Makes { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }
        }
        public class GetMAKEbyID
        {
            public int Id { get; set; }
        }

        public class ConversionDetailForm
        {
            public int ID { get; set; }
            [Required(ErrorMessage = "The Conversion Name is required")]
            public string ConversionName { get; set; }
            public string Type { get; set; }
            public string Company { get; set; }
            public Nullable <int> Year { get; set; }
            public string Make { get; set; }
            [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
            public int MakeID { get; set; }
            public string Description { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
            public string RetailPrice { get; set; }
            [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
            public string DealerPrice { get; set; }
            public bool IsActive { get; set; }
            public string token { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }
            public int TierID { get; set; }

            public SelectList TierList;
            public SelectList MakeList;
        }

        public class DeprecaitionForm
        {
            public int ID { get; set; }
            public string DepreciationPercentage { get; set; }
            public string AgeInMonths { get; set; }
            public string token { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }

        }
        public class DeductionForm
        {
            public int ID { get; set; }
            public string DeductionAmount { get; set; }
            public string Description { get; set; }
            public string token { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }

        }

        public class MarkUpFormData
        {
            public MarkupFormPercent Percent { get; set; }
            public MarkupFormFixed Fixed { get; set; }
        }

        public class MarkupFormPercent
        {
            public int ID { get; set; }
            public string MarkUpPercent { get; set; }
            public bool Selected { get; set; }

        }
        public class GetManagerDahboardCounterValues
        {
            public bool status { get; set; }
            public string message { get; set; }
            public int RejectedQuotes { get; set; }
            public int PendingQuotes { get; set; }
            public int ApprovedQuotes { get; set; }
            public int TotalUsers { get; set; }

        }

        public class TiersDetails
        {
            public int Id { get; set; }
            public string TierName { get; set; }
            public decimal Amount { get; set; }
            public string PerMonthorYear { get; set; }

        }
        public class TiersAmount
        {
            public decimal Tier1Amount { get; set; }
            public decimal Tier2Amount { get; set; }
            public decimal Tier3Amount { get; set; }

        }
        public class MarkupFormFixed
        {
            public int ID { get; set; }
            public string MarkUpFixed { get; set; }
            public bool Selected { get; set; }

        }


        public class MarkUpForm
        {
            public int ID { get; set; }
            public int UserID { get; set; }
            public string MarkUpPercent { get; set; }
        }

        public class MarkUpFormPercent
        {
            public int ID { get; set; }
            public int UserID { get; set; }
            public string MarkUpPercent { get; set; }
        }

        public class MarkUpFormFixed
        {
            public int ID { get; set; }
            public int UserID { get; set; }
            public string MarkUpFixed { get; set; }
        }
        public class ChangePassword
        {
            public int EmpID { get; set; }
           
            public string NewPassword { get; set; }
        }




        public class EditQouteForm
        {
            public dynamic data { get; set; }
        }

        public class EditQouteData
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public string qoute { get; set; }
            public string makedata { get; set; }
            public string yearsdata { get; set; }
            public string modeldata { get; set; }
            public string trimdata { get; set; }
            public string regiondata { get; set; }
            public string tradeindata { get; set; }
            public string TradeInPartial { get; set; }
            public dynamic conversionslist { get; set; }
            public dynamic deductionslist { get; set; }
        }


        public class QouteDetails
        {
            public int ID { get; set; }
            public string CustomerName { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string SearchType { get; set; }
            public string SearchTypeID { get; set; }
            public string Make { get; set; }
            public int MakeID { get; set; }
            public string Model { get; set; }
            public int ModelID { get; set; }
            public string Trim { get; set; }
            public int TrimID { get; set; }
            public int Year { get; set; }
            public int YearID { get; set; }
            public string Millage { get; set; }
            public string StateCode { get; set; }
            public string CleanTrade { get; set; }
            public string AverageTrade { get; set; }
            public string RoughTrade { get; set; }
            public string RetailTrade { get; set; }
            public string MarkUpFixed { get; set; }
            public string MarkUpPercent { get; set; }
            public string Status { get; set; }
            public int EmployeeID { get; set; }
            public string TotalConversionDeduction { get; set; }
            public string VinNumber { get; set; }
            public string  RegionVal { get; set; }
            public string RegionText { get; set; }
            public DateTime Created { get; set; }
            public DateTime Deleted { get; set; }
            public bool IsClean { get; set; }
            public bool IsAverage { get; set; }
            public bool IsRough { get; set; }
            public string Comment { get; set; }
            public decimal SuggestedPrice { get; set; }
        }

        public class QouteDetailsData
        {
            public bool status { get; set; }
            public string message { get; set; }
            public QouteDetails qouteData { get; set; }
            
        }

        public class QoutesSearchData
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable qouteData { get; set; }
            //public EmployeeForm employeeData { get; set; }
        }
        public class QuoteSearchHeader {
            public string token { get; set; }
            public int EmpId { get; set; }
            public string Name { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public int Year { get; set; }

        }

        public class TradeInValues
        {
            public int MakeId { get; set; }
            public string MakeName { get; set; }
            public int YearId { get; set; }
            public int Year { get; set; }
            public string Model { get; set; }
            public int ModelId { get; set; }
            public int TrimId { get; set; }
            public string Trim { get; set; }
            public decimal CleanTradeIn { get; set; }
            public decimal AverageTradeIn { get; set; }
            public decimal RoughTradeIn { get; set; }
            public decimal RetailTradeIn { get; set; }
            public decimal MarkUpPercent { get; set; }
            public decimal MarkUpFixed { get; set; }
            public bool Status { get; set; }
            public string Message { get; set; }
            public int RegionVal { get; set; }
            public string RegionText { get; set; }
        }
        public class Result
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
            public string vinoptionstrade { get; set; }
            public string vinoptionsretail { get; set; }
            public string vinoptionsloan { get; set; }
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

        }

        public class SearchByVin
        {
            public string requestId { get; set; }
            public List<Result> result { get; set; }
            public string userinfo { get; set; }
            public string authId { get; set; }
        }

        public class Regions
        {
            public string description { get; set; }
            public string regionid { get; set; }
        }

        public class GetRegions
        {
            public string requestId { get; set; }
            public List<Regions> result { get; set; }
            public string userinfo { get; set; }
            public string authId { get; set; }
        }

        //public class HiddenTradeInValues
        //{
        //    public decimal CleanValue { get; set; }
        //    public decimal AverageValue { get; set; }
        //    public decimal RoughValue { get; set; }
        //    public decimal RetailValue { get; set; }
        //}


        public class ConversionArray
        {
            public string token { get; set; }
            public int[] conversionArray { get; set; }
        }

        public class Attachments
        {
            public int QuoteID { get; set; }
            public int UserID { get; set; }
            public string File { get; set; }
        }
        public class QouteRequest
        {
            public string token { get; set; }
            public int QouteID { get; set; }
            public string CustomerName { get; set; }
            public string  Email { get; set; }
            public string Phone { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string SearchType { get; set; }
            public string SearchTypeID { get; set; }
            public string Make { get; set; }
            public int MakeID { get; set; }
            public string Model { get; set; }
            public int ModelID { get; set; }
            public string Trim { get; set; }
            public int TrimID { get; set; }
            public int Year { get; set; }
            public int YearID { get; set; }
            public string Millage { get; set; }
            public string StateCode { get; set; }
            public string CleanTrade { get; set; }
            public string AverageTrade { get; set; }
            public string RoughTrade { get; set; }
            public string RetailTrade { get; set; }
            public string MarkUpFixed { get; set; }
            public string MarkUpPercent { get; set; }
            public string Status { get; set; }
            public int EmployeeID { get; set; }
            public int ManagerID { get; set; }
            public string TotalConversionDeduction { get; set; }
            public string[] ConversionID { get; set; }
            public string[] ConversionAmount { get; set; }
            public string[] DeductionID { get; set; }
            public string[] DeductionAmount { get; set; }
            public string[] DepericiationAmount { get; set; }
            public string QouteAmount { get; set; }
            public string VinNumber { get; set; }
            public string RegionVal { get; set; }
            public string RegionText { get; set; }
            public bool IsClean { get; set; }
            public bool IsAverage { get; set; }
            public bool IsRough { get; set; }
            public decimal SuggestedPrice { get; set; }
            public string Comment { get; set; }
        }
        public class GetQoutesDataListManager
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable QoutesList { get; set; }

        }
    }

}
