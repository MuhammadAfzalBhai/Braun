using BraunApp_ViewModel.Model;
using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Braunability_ViewModal.Model.BraunVM_Request;

namespace BAL.Repository
{
    public class EmployeeRepository : BaseRespoitory
    {

        public EmployeeRepository()
            : base()
        { }


        public EmployeeRepository(vt_BraunAppEntities ContextDB)
            : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public bool UpdatePassword(ChangePassword _changePassword)
        {
            bool Issuccess = true;
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@newpassword",!string.IsNullOrEmpty(vt_Common.Encrypt(_changePassword.NewPassword))? vt_Common.Encrypt(_changePassword.NewPassword) : string.Empty),
                    new SqlParameter("@ID", _changePassword.EmpID)

              };
                Entity_Common.get_SP_DataTable(DBContext, "sp_UpdatePassword", para);
                Issuccess = true;
                return Issuccess;
            }
            catch (Exception)
            {
                Issuccess = false;
                return Issuccess;

            }
        }
        public DataTable getSearchQuotesNew(string Name,string Model,string Make,int Year,int EmpId) {
            SqlParameter[] para = {
                new SqlParameter("@Name", !string.IsNullOrEmpty(Name) ? Name : string.Empty),
                 new SqlParameter("@Model", !string.IsNullOrEmpty(Model) ? Model : string.Empty),
                  new SqlParameter("@Make", !string.IsNullOrEmpty(Make) ? Make : string.Empty),
                   new SqlParameter("@Year", Year ),
                   new SqlParameter("@EmpID", EmpId )
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_getSearchQuotesNew", para);
            return dt;
        }

        public DataTable getSearchQuotesDraft(string Name, string Model, string Make, int Year,int EmpId)
        {
            SqlParameter[] para = {
                new SqlParameter("@Name", !string.IsNullOrEmpty(Name) ? Name : string.Empty),
                 new SqlParameter("@Model", !string.IsNullOrEmpty(Model) ? Model : string.Empty),
                  new SqlParameter("@Make", !string.IsNullOrEmpty(Make) ? Make : string.Empty),
                   new SqlParameter("@Year", Year ),
                    new SqlParameter("@EmpID", EmpId )
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_getSearchQuotesDraft", para);
            return dt;
        }
        public DataTable GetAllConversions()
        {
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllConversions");

            return dt;
        }

        public DataTable GetAllConversionsbymakeId(int makeId)
        {
            SqlParameter[] par = {
            new SqlParameter("@MakeId",  makeId > 0  ? makeId : 0 ) };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllConversionsById",par);

            return dt;
        }

        public List<vt_Conversions> GetAllConversionsbyArray(int[] arr)
        {
            var data = DBContext.ExclueAll().vt_Conversions.AsNoTracking().Where(x => arr.Contains(x.ID)).ToList();

            return data;
        }

        public List<vt_Deductions> GetAllDeductions()
        {
            var data = DBContext.ExclueAll().vt_Deductions.AsNoTracking().Where(x=>x.IsActive==true && x.Deleted==null) .ToList();
            return data;
        }

        public DataTable GetAllDeduction()
        {
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllDeductions");

            return dt;
        }

        public DataTable AddNewQoute(QouteRequest param)
         {
            SqlParameter[] par = {
            new SqlParameter("@CustomerName",  !string.IsNullOrEmpty(param.CustomerName)? param.CustomerName : string.Empty ),
            new SqlParameter("@Email", !string.IsNullOrEmpty(param.Email)? param.Email : string.Empty ),
            new SqlParameter("@Phone", !string.IsNullOrEmpty(param.Phone)? param.Phone : string.Empty ),
            new SqlParameter("@AddressLine1",!string.IsNullOrEmpty(param.AddressLine1)? param.AddressLine1 : string.Empty ),
            new SqlParameter("@AddressLine2",!string.IsNullOrEmpty(param.AddressLine2)? param.AddressLine2 : string.Empty   ),
            new SqlParameter("@SearchType",!string.IsNullOrEmpty(param.SearchType)? param.SearchType : string.Empty ),
            new SqlParameter("@SearchTypeID",!string.IsNullOrEmpty(param.SearchTypeID)? param.SearchTypeID : string.Empty   ),
            new SqlParameter("@Make",!string.IsNullOrEmpty(param.Make)? param.Make : string.Empty   ),
            new SqlParameter("@MakeID", Convert.ToInt32(param.MakeID)),
            new SqlParameter("@Model",!string.IsNullOrEmpty(param.Model)? param.Model : string.Empty ),
            new SqlParameter("@ModelID", Convert.ToInt32(param.ModelID)),
            new SqlParameter("@Year", Convert.ToInt32(param.Year)),
            new SqlParameter("@YearID", Convert.ToInt32(param.YearID)),
            new SqlParameter("@Trim",!string.IsNullOrEmpty(param.Trim)? param.Trim : string.Empty ),
            new SqlParameter("@TrimID",  Convert.ToInt32(param.TrimID) ),
            new SqlParameter("@Milage", Convert.ToDecimal(param.Millage)),
            new SqlParameter("@StateCode",!string.IsNullOrEmpty(param.StateCode)? param.StateCode : string.Empty   ),
            new SqlParameter("@CleanTrade", Convert.ToDecimal(param.CleanTrade)),
            new SqlParameter("@AvgTrade", Convert.ToDecimal(param.AverageTrade)),
            new SqlParameter("@RoughTrade",  Convert.ToDecimal(param.RoughTrade)),
            new SqlParameter("@RetailTrade",  Convert.ToDecimal(param.RetailTrade)),
            new SqlParameter("@MarkUpPercent",  !string.IsNullOrEmpty(param.MarkUpPercent)? param.MarkUpPercent : string.Empty ),
            new SqlParameter("@MarkUpFixed",  !string.IsNullOrEmpty(param.MarkUpFixed)? param.MarkUpFixed : string.Empty ),
            new SqlParameter("@Status",!string.IsNullOrEmpty(param.Status)? param.Status : string.Empty ),
            new SqlParameter("@Created",  DateTime.Now ),
            new SqlParameter("@EmployeeID", Convert.ToInt32(param.EmployeeID)),
            new SqlParameter("@TotalConversionDeduction", Convert.ToDecimal(param.TotalConversionDeduction)),
            new SqlParameter("@VinNumber",!string.IsNullOrEmpty(param.VinNumber)? param.VinNumber : string.Empty),
            new SqlParameter("@RegionVal", !string.IsNullOrEmpty(param.RegionVal)? param.RegionVal : string.Empty),
            new SqlParameter("@RegionText", !string.IsNullOrEmpty(param.RegionText)? param.RegionText : string.Empty),
            new SqlParameter("@IsClean", param.IsClean),
            new SqlParameter("@IsAverage",  param.IsAverage),
            new SqlParameter("@IsRough",  param.IsRough),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_InsertNewQoute", par);

            return dt;
        }
        
        public DataTable UpdateQoute(QouteRequest param)
        {
            SqlParameter[] par = {
            new SqlParameter("@QouteID", Convert.ToInt32(param.QouteID)),
            new SqlParameter("@CustomerName",  !string.IsNullOrEmpty(param.CustomerName)? param.CustomerName : string.Empty ),
            new SqlParameter("@Email", !string.IsNullOrEmpty(param.Email)? param.Email : string.Empty ),
            new SqlParameter("@Phone", !string.IsNullOrEmpty(param.Phone)? param.Phone : string.Empty ),
            new SqlParameter("@AddressLine1",!string.IsNullOrEmpty(param.AddressLine1)? param.AddressLine1 : string.Empty ),
            new SqlParameter("@AddressLine2",!string.IsNullOrEmpty(param.AddressLine2)? param.AddressLine2 : string.Empty   ),
            new SqlParameter("@SearchType",!string.IsNullOrEmpty(param.SearchType)? param.SearchType : string.Empty ),
            new SqlParameter("@SearchTypeID",!string.IsNullOrEmpty(param.SearchTypeID)? param.SearchTypeID : string.Empty   ),
            new SqlParameter("@Make",!string.IsNullOrEmpty(param.Make)? param.Make : string.Empty   ),
            new SqlParameter("@MakeID", Convert.ToInt32(param.MakeID)),
            new SqlParameter("@Model",!string.IsNullOrEmpty(param.Model)? param.Model : string.Empty ),
            new SqlParameter("@ModelID", Convert.ToInt32(param.ModelID)),
            new SqlParameter("@Year", Convert.ToInt32(param.Year)),
            new SqlParameter("@YearID", Convert.ToInt32(param.YearID)),
            new SqlParameter("@Trim",!string.IsNullOrEmpty(param.Trim)? param.Trim : string.Empty ),
            new SqlParameter("@TrimID",  Convert.ToInt32(param.TrimID) ),
            new SqlParameter("@Milage", Convert.ToDecimal(param.Millage)),
            new SqlParameter("@StateCode",!string.IsNullOrEmpty(param.StateCode)? param.StateCode : string.Empty   ),
            new SqlParameter("@CleanTrade", Convert.ToDecimal(param.CleanTrade)),
            new SqlParameter("@AvgTrade", Convert.ToDecimal(param.AverageTrade)),
            new SqlParameter("@RoughTrade",  Convert.ToDecimal(param.RoughTrade)),
            new SqlParameter("@RetailTrade",  Convert.ToDecimal(param.RetailTrade)),
            new SqlParameter("@Status",!string.IsNullOrEmpty(param.Status)? param.Status : string.Empty ),
            new SqlParameter("@Edited",  DateTime.Now ),
            new SqlParameter("@EmployeeID", Convert.ToInt32(param.EmployeeID)),
            new SqlParameter("@TotalConversionDeduction", Convert.ToDecimal(param.TotalConversionDeduction)),
           new SqlParameter("@VinNumber",!string.IsNullOrEmpty(param.VinNumber)? param.VinNumber : string.Empty),
            new SqlParameter("@RegionVal", !string.IsNullOrEmpty(param.RegionVal)? param.RegionVal : string.Empty),
            new SqlParameter("@RegionText", !string.IsNullOrEmpty(param.RegionText)? param.RegionText : string.Empty),
            new SqlParameter("@IsClean", param.IsClean),
            new SqlParameter("@IsAverage",  param.IsAverage),
            new SqlParameter("@IsRough",  param.IsRough),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_UpdateQoute", par);

            return dt;
        }

        public DataTable DeleteQouteConversions(int ID)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  Convert.ToInt32(ID) ),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_DeleteQouteConverions", param);

            return dt;
        }

        public DataTable DeleteQouteDeductions(int ID)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  Convert.ToInt32(ID) ),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_DeleteQouteDeductions", param);

            return dt;
        }

        public string GetLastQouteID()
        {
            SqlParameter[] param = { };
            var QouteID = Entity_Common.get_Scalar(DBContext, "sp_GetLastQouteID", param);
            return QouteID;
        }

        public DataTable AddNewQouteConversion(int QouteID,int ConversionID , string ConversionAmount)
        {
            SqlParameter[] par = {
            new SqlParameter("@QouteID",  Convert.ToInt32(QouteID)),
            new SqlParameter("@ConversionID",Convert.ToInt32(ConversionID)),
            //new SqlParameter("@ConversionID",(ConversionID > 0) ? ConversionID : DBNull.Value),
            new SqlParameter("@ConversionAmount", Convert.ToDecimal(ConversionAmount)),
            new SqlParameter("@Created",  DateTime.Now ),
                               };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_InsertNewQouteConversion", par);

            return dt;
        }

        public DataTable AddNewQouteDeduction(int QouteID, int ConversionID, string ConversionAmount, int DeductionID, string DeductionAmount,string DepericiationAmount)
        {
            decimal ConAmount = Convert.ToDecimal(ConversionAmount);
            decimal DedAmount = Convert.ToDecimal(DeductionAmount);
            decimal DepAmount = Convert.ToDecimal(DepericiationAmount);
            string ActualAmount = (DepAmount - DedAmount).ToString();

            SqlParameter[] par = {
            new SqlParameter("@QouteID",  Convert.ToInt32(QouteID)),
            new SqlParameter("@ConversionID",Convert.ToInt32(ConversionID)),
            new SqlParameter("@ConversionAmount", Convert.ToDecimal(ConversionAmount)),
            new SqlParameter("@DeductionID",Convert.ToInt32(DeductionID)),
            new SqlParameter("@DeductionAmount", Convert.ToDecimal(DeductionAmount)),
            new SqlParameter("@DepreciationAmount", Convert.ToDouble(DepericiationAmount)),
            new SqlParameter("@ActualAmount", Convert.ToDecimal(ActualAmount)),
            new SqlParameter("@Created",  DateTime.Now ),
                               };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_InsertNewQouteDeduction", par);

            return dt;
        }

        public DataTable GetAllQoutes()
        {

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllQoutes");

            return dt;
        }


        public DataTable GetQuotesByEmpId(int EmpId)
        {
            SqlParameter[] par =
            {
                new SqlParameter("@EmpId",Convert.ToInt32(EmpId))
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetQuotesByEmpId",par);

            return dt;
        }
        public DataTable GetDraftQoutes()
        {

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetDraftQoutes");

            return dt;
        }

        public DataTable GetDraftQuotesByEmpId(int EmpId)
        {
            SqlParameter[] par =
            {
                new SqlParameter("@EmpId",Convert.ToInt32(EmpId))
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetDraftQuotesByEmpId", par);

            return dt;
        }
        public MarkupFormPercent GetMarkupPercent()
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  1 ),   //ID of MarkUp Percent
                               };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetMarkup", param);

            MarkupFormPercent markup = new MarkupFormPercent();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                markup.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                markup.MarkUpPercent = dt.Rows[i]["MarkUpPercent"].ToString();
            }

            return markup;
        }

        public MarkupFormPercent GetMarkupFixed()
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  2 ),   //ID of MarkUp Percent
                               };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetMarkup", param);

            MarkupFormPercent markup = new MarkupFormPercent();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                markup.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                markup.MarkUpPercent = dt.Rows[i]["MarkUpPercent"].ToString();
            }

            return markup;
        }

        //public List<MarkupForm> GetMarkup()
        //{
        //    var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetMarkupPercent");

        //    List<MarkupForm> markuplist = new List<MarkupForm>();
        //    MarkupForm markup = new MarkupForm();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {

        //        markup.IDPercent = Convert.ToInt32(dt.Rows[i]["ID"]);
        //        markup.MarkUpPercent = dt.Rows[i]["MarkUpPercent"].ToString();
        //        markup.MarkUpFixed = Convert.ToInt32(dt.Rows[i]["MarkUpFixed"]);
        //        markuplist.Add(markup);
        //    }

        //    return markuplist;
        //}
        public DataTable GetQouteData(int ID)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  Convert.ToInt32(ID) ),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetQoutebyID", param);

            return dt;
        }

        public DataTable GetAllConversionsbyQouteID(int ID)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  Convert.ToInt32(ID) ),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetConversionsbyQouteID", param);

            return dt;
        }

        public DataTable GetAllDeductionsbyQouteID(int ID)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  Convert.ToInt32(ID) ),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetDeductionsbyQouteID", param);

            return dt;
        }

        public DataTable DeleteQouteByID(int ID)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  Convert.ToInt32(ID) ),
            new SqlParameter("@Deleted" , DateTime.Now)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_DeleteQoutebyID", param);

            return dt;
        }

    }
}
