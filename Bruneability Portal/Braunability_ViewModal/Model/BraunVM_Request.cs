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
            public string token { get; set; }
        }

        public class GetbyID
        {
            public int ID { get; set; }
            public string token { get; set; }
        }
        



        public class Betweentwodates
        {
            public string start { get; set; }
            public string end { get; set; }
            public string token { get; set; }
        }
        public class GetbyDepreciation
        {
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
         
            [Required(ErrorMessage = "Joining Date is required")]
           
            public string JoiningDate { get; set; }

            [RegularExpression(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$", ErrorMessage = "Phone is not valid.")]
            [Required(ErrorMessage = "Contact is required")]
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

            //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter numeric value.")]
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

        }
    }

}
