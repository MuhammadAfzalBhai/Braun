using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Braunability_ViewModal.Model.BraunVM_Request;

namespace Braunability_ViewModal.Model
{
    public class BraunVM_Response
    {

        public class HeaderResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class AuthenticationResponseModel
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string accesstoken { get; set; }
            public vt_UserProfile User { get; set; }
            public List<vt_UserPermissions> UserPermission { get; set; }

        }

        public class GetRejectedQoutesDataList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable RejectedQoutesList { get; set; }

        }
        public class GetApprovedQoutesDataList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable ApprovedQoutesList { get; set; }

        }

        public class GetDahboardCounterValues
        {
            public bool status { get; set; }
            public string message { get; set; }
            public int TotalUsers { get; set; }
            public int TotalQuotes { get; set; }
            public int TotalNewQuotes { get; set; }
            public int TotalDraftQuotes { get; set; }
            public int TotalCompanies { get; set; }
            public int TotalMakes { get; set; }
        }

        public class GetEmployeeDataList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable EmployeeList { get; set; }

        }
        public class GetMakesDataList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable MakesList { get; set; }

        }

        //public class GetDahboardCounterValues
        //{
        //    public bool status { get; set; }
        //    public string message { get; set; }
        //    public int TotalUsers { get; set; }
        //    public int TotalQuotes { get; set; }
        //    public int TotalNewQuotes { get; set; }
        //    public int TotalDraftQuotes { get; set; }
        //    public int TotalCompanies { get; set; }
        //    public int TotalMakes { get; set; }

        //}
        

        public class GetDashboard
        {
            public bool status { get; set; }
            public string message { get; set; }
            public listofQuotesListMonths QuotesListLastSixMonth  { get;set;}
            public ListTopfiveMake ListofTopfiveMake { get; set; }
            public ListTopfiveModel ListofTopfiveModel { get; set; }
            public ListTopfiveConversions ListofTopfiveConversions { get; set; }

        }
        public class listofQuotesListMonths {
            public bool status { get; set; }
            public string message { get; set; }
            public List<QuotesListMonths> QuotesListLastSixMonth { get; set; }
        }
        public class QuotesListMonths
        {

            public string MonthName { get; set; }
            public string MonthNumber { get; set; }
            public int Year { get; set; }
            public int NoOfQuotes { get; set; }
        }

        public class ListTopfiveMake
        {
            public bool status { get; set; }
            public string message { get; set; }
            public List<TopfiveMake> ListofTopfiveMake { get; set; }
        }
        public class TopfiveMake
        {

            public string MakeName { get; set; }
            public int TotalMakeQuotes { get; set; }

        }


        public class ListTopfiveModel
        {
            public bool status { get; set; }
            public string message { get; set; }
            public List<TopfiveModel> ListofTopfiveModel { get; set; }
        }
        
        public class TopfiveModel
        {
         
            public string MakeName { get; set; }
            public string ModelName { get; set; }
            public int TotalModelQuotes { get; set; }

        }

        public class ListTopfiveConversions
        {
            public bool status { get; set; }
            public string message { get; set; }
            public List<TopfiveConversions> ListofTopfiveConversions { get; set; }
        }
        public class TopfiveConversions
        {
            public string MakeName { get; set; }
            public string ConversionslName { get; set; }
            public int TotalConversionQuotes { get; set; }

        }
        public class GetTopFiveMake
        {
            public bool status { get; set; }
            public string message { get; set; }
            public List<ListTopfiveMake> ListofTopfiveMake { get; set; }

        }
       

        public class StateCode
        {
            public bool status { get; set; }
            public string message { get; set; }

            public List<statecodeprop> Statecodelist { get; set; }
        }

        public class statecodeprop {

            public string Descr { get; set; }
            public string Code { get; set; }

        }
        public class GetConversionDataList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable ConversionsList { get; set; }

        }
       
        public class GetMakeList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable MakesList { get; set; }
            public DataTable TierList { get; set; }

        }

        public class CleanPrintData
        {
           
            public string yearVal { get; set; }
            public string makeVal { get; set; }
            public string modelVal { get; set; }
            public string trimVal { get; set; }
            public string cleanVal { get; set; }
            public string avgVal { get; set; }
            public string roughVal { get; set; }
            public string retailVal { get; set; }
            public string conversionValwithMarkup { get; set; }
            public string conversionVal { get; set; }
            public string totalCleanVal { get; set; }
            public string totalAvgVal { get; set; }
            public string totalRoughVal { get; set; }
            public string totalRetailVal { get; set; }
        }
        public class GetDepriciationresult
        {
            public bool status { get; set; }
            public string message { get; set; }
            public double Depreciationresult { get; set; }

        }
        public class GetQoutesDataList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable QoutesList { get; set; }

        }

        public class GetMarkUpValues
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string MarkUp_Percent { get; set; }


        }

        public class GetMarkUpValueswithId
        {
            public bool status { get; set; }
            public string message { get; set; }           

            public int Id { get; set; }
            public double MarkUpPer { get; set; }
            public double MarkUpFixed { get; set; }


        }

        

        public class GetQouteConversionDetailList
        {
            public int Amount { get; set; }
            public bool status { get; set; }
            public string message { get; set; }
            public List<vt_Conversions> ConversionList { get; set; }
            public List<vt_Deductions> DeductionsList { get; set; }

            //public DeductionCustomeGrid deductionData { get; set; }

        }

        //public class DeductionCustomeGrid
        //{

        //}

        public class GetMarkUpData
        {
            public bool status { get; set; }
            public string message { get; set; }
            public MarkupFormPercent MarkupData { get; set; }

        }

        public class GetMarkUpDataPercent
        {
            public bool status { get; set; }
            public string message { get; set; }
            public MarkupFormPercent MarkupData { get; set; }

        }
        public class GetTiersDetails
        {
            public bool status { get; set; }
            public string message { get; set; }
            public List<TiersDetails> MarkupData = new List<TiersDetails>();

        }

        public class GetMarkUpDataFixed
        {
            public bool status { get; set; }
            public string message { get; set; }
            public MarkupFormFixed MarkupData { get; set; }

        }

        public class GetDepriciationDataList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable DepriciationList { get; set; }

        }
        public class GetDeductionDataList
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DataTable DeductionList { get; set; }

        }

        public class GetEmployeeForm
        {
            public bool status { get; set; }
            public string message { get; set; }
            public EmployeeForm EmpData { get; set; }
        }
        public class GetMakesForm
        {
            public bool status { get; set; }
            public string message { get; set; }
            public AddNewMake MakeData { get; set; }
        }
        public class GetConversionForm
        {
            public bool status { get; set; }
            public string message { get; set; }
            public ConversionDetailForm ConversionData { get; set; }
        }

        public class GetDepreciationForm
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DeprecaitionForm DepreciationData { get; set; }
        }


        public class GetDeductionForm
        {
            public bool status { get; set; }
            public string message { get; set; }
            public DeductionForm DeductionData { get; set; }
        }
    }
}
