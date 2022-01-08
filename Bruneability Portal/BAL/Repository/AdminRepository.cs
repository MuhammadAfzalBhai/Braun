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
using static Braunability_ViewModal.Model.BraunVM_Response;

namespace BAL.Repository
{
    public class AdminRepository : BaseRespoitory
    {
        public AdminRepository()
            : base()
        { }


        public AdminRepository(vt_BraunAppEntities ContextDB)
            : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public bool UpdatePassword(ChangePassword _changePassword) {
            bool Issuccess = true;
            try
            {
                SqlParameter[] para = {                   
                    new SqlParameter("@newpassword",!string.IsNullOrEmpty(vt_Common.Encrypt(_changePassword.NewPassword))? vt_Common.Encrypt(_changePassword.NewPassword) : string.Empty),
                    new SqlParameter("@ID", _changePassword.EmpID)

              };
                Entity_Common.get_SP_DataTable(DBContext, "sp_UpdatePassword",para);
                Issuccess = true;
                return Issuccess;
            }
            catch (Exception)
            {
                Issuccess = false;
                return Issuccess;
               
            }
        }
        public vt_UserProfile ValidateEmployee(string UserEmail)
        {
            var data = DBContext.ExclueAll().vt_UserProfile.AsNoTracking().Where(x => x.Email == UserEmail ).FirstOrDefault();

            return data;
        }

        public vt_UserProfile ValidateEmployeeforedit(string UserEmail, int empID)
        {
            var data = DBContext.ExclueAll().vt_UserProfile.AsNoTracking().Where(x => x.Email == UserEmail && x.ID != empID).FirstOrDefault();

            return data;
        }


        public DataTable GetAllQoutesforReport()
        {

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllQoutesforReport");

            return dt;
        }

        public GetDahboardCounterValues GetDashboardCounterResult()
        {
            GetDahboardCounterValues obj = new GetDahboardCounterValues();
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetDashboradResult");
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    obj.message = "Result is Found";
                    obj.status = true;
                    obj.TotalCompanies =Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                    obj.TotalUsers = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                    obj.TotalMakes = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                    obj.TotalQuotes= Convert.ToInt32(dt.Rows[i].ItemArray[3]);
                    obj.TotalNewQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[4]);
                    obj.TotalDraftQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[5]);

                }
            }

            else
            {
                obj.message = "Result is not Found";
                obj.status = false;
                obj.TotalCompanies =0;
                obj.TotalUsers =0;
                obj.TotalMakes = 0;
                obj.TotalQuotes = 0;
                obj.TotalNewQuotes = 0;
                obj.TotalDraftQuotes = 0;
            }
            
            return obj;
        }
        public List<QuotesListMonths> GetQuoetsoflastsixmonths()
        {
            List<QuotesListMonths> _actuallist = new List<QuotesListMonths>();
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetLastSIxMonthQuotesAsPerMonth");
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i <dt.Rows.Count; i++)
                {
                    QuotesListMonths listitem = new QuotesListMonths();
                    listitem.MonthName = (dt.Rows[i].ItemArray[2]).ToString();
                    listitem.MonthNumber= (dt.Rows[i].ItemArray[0]).ToString();
                    listitem.Year = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                    listitem.NoOfQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[3]);
                    _actuallist.Add(listitem);

                }

            }

            return _actuallist;
        }


        public List<TopfiveMake> GetListTopfiveMake()
        {
            List<TopfiveMake> _actuallist = new List<TopfiveMake>();
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_TopFiveMake");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TopfiveMake listitem = new TopfiveMake();
                    listitem.MakeName = Convert.ToString(dt.Rows[i].ItemArray[0]);                   
                    listitem.TotalMakeQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                    _actuallist.Add(listitem);

                }

            }

            return _actuallist;
        }

        public List<TopfiveModel> GetListTopfiveModel()
        {
            List<TopfiveModel> _actuallist = new List<TopfiveModel>();
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_TopFiveModel");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TopfiveModel listitem = new TopfiveModel();
                    listitem.MakeName =Convert.ToString(dt.Rows[i].ItemArray[1]);
                    listitem.ModelName = Convert.ToString(dt.Rows[i].ItemArray[0]);
                    listitem.TotalModelQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                    _actuallist.Add(listitem);

                }

            }

            return _actuallist;
        }

        public List<TopfiveConversions> GetListTopfiveConversions()
        {
            List<TopfiveConversions> _actuallist = new List<TopfiveConversions>();
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_TopFiveConversions");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TopfiveConversions listitem = new TopfiveConversions();
                    listitem.MakeName = Convert.ToString(dt.Rows[i].ItemArray[0]);
                    listitem.ConversionslName = Convert.ToString(dt.Rows[i].ItemArray[1]);
                    listitem.TotalConversionQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                    _actuallist.Add(listitem);

                }

            }

            return _actuallist;
        }
        public DataTable GetQoutesBetweentwoDates(string start, string end)
        {
           
            DateTime _end = Convert.ToDateTime(end);
            DateTime _start = Convert.ToDateTime(start);

            TimeSpan t = new TimeSpan(0, 23, 59, 0, 0);
            DateTime _toDate= _end.Add(t);

           
            var fromDate = _start.ToString("MM/dd/yyyy HH:mm:ss");
            var toDate = _toDate.ToString("MM/dd/yyyy HH:mm:ss");



            SqlParameter[] param = {
            new SqlParameter("@start",  fromDate ),
            new SqlParameter("@end", toDate)
           
                               };
          
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetQoutesBetweentwoDates",param);

            return dt;
        }
        public DataTable GetAllEmployees()
        {
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllEmployees");

            return dt;
        }
        public DataTable GetAllEmployeesRequest()
        {
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllEmployeesRequest");

            return dt;
        }

        
        public DataTable AcceptEmployeesRequest(int id)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  id ),
            new SqlParameter("@IsApproved", true),
            new SqlParameter("@IsActive", true),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_AcceptEmployeeRequest", param);

            return dt;
        }

        public DataTable RejectEmployeesRequest(int id)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  id ),
            new SqlParameter("@IsApproved", false),
            new SqlParameter("@IsActive", true),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_RejectEmployeeRequest", param);

            return dt;
        }



        public DataTable GetEmployeeData(int ID) //EmployeeID
        {
            SqlParameter[] param = { new SqlParameter("@EmpID", ID)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetEmployeebyID", param);

            return dt;
        }
        

        public DataTable DeleteEmployeeByID(int ID) //EmployeeID
        {
            SqlParameter[] param = {
                new SqlParameter("@ID", ID),
                new SqlParameter("@DeletedAt", DateTime.Now)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_DeleteEmployeebyID", param);

            return dt;
        }


        public DataTable CreateEmployee(EmployeeForm Emp)
        {
            SqlParameter[] param = {
            new SqlParameter("@UserName", !string.IsNullOrEmpty(Emp.UserName)? Emp.UserName : string.Empty),
            new SqlParameter("@RoleID",  2 ),
            new SqlParameter("@Email", !string.IsNullOrEmpty(Emp.Email)? Emp.Email : string.Empty),
            new SqlParameter("@Phone", !string.IsNullOrEmpty(Emp.Contact)? Emp.Contact : string.Empty),
            new SqlParameter("@JoiningDate", !string.IsNullOrEmpty(Emp.JoiningDate)? Emp.JoiningDate : string.Empty),
            new SqlParameter("@City", !string.IsNullOrEmpty(Emp.City)? Emp.City : string.Empty),
            new SqlParameter("@State", !string.IsNullOrEmpty(Emp.State)? Emp.State : string.Empty),
            new SqlParameter("@CompanyName", !string.IsNullOrEmpty(Emp.CompanyName)? Emp.CompanyName : string.Empty),
            new SqlParameter("@Password", !string.IsNullOrEmpty(vt_Common.Encrypt(Emp.Password))? vt_Common.Encrypt(Emp.Password) : string.Empty),
            new SqlParameter("@IsApproved", Emp.IsApproved),
            new SqlParameter("@IsActive", Emp.IsActive),
            new SqlParameter("@CreatedAt", DateTime.Now )
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_CreateEmployee", param);

            return dt;
        }

        public DataTable UpdateEmployee(EmployeeForm Emp)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID", Emp.ID),
            new SqlParameter("@UserName", !string.IsNullOrEmpty(Emp.UserName)? Emp.UserName : string.Empty),
            new SqlParameter("@Email", !string.IsNullOrEmpty(Emp.Email)? Emp.Email : string.Empty),
            new SqlParameter("@Phone", !string.IsNullOrEmpty(Emp.Contact)? Emp.Contact : string.Empty),
            new SqlParameter("@JoiningDate", !string.IsNullOrEmpty(Emp.JoiningDate)? Emp.JoiningDate : string.Empty),
            new SqlParameter("@City", !string.IsNullOrEmpty(Emp.City)? Emp.City : string.Empty),
            new SqlParameter("@State", !string.IsNullOrEmpty(Emp.State)? Emp.State : string.Empty),
            new SqlParameter("@CompanyName", !string.IsNullOrEmpty(Emp.CompanyName)? Emp.CompanyName : string.Empty),
            new SqlParameter("@Password", !string.IsNullOrEmpty(vt_Common.Encrypt(Emp.Password))? vt_Common.Encrypt(Emp.Password) : string.Empty),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_UpdateEmployee", param);

            return dt;
        }


        public DataTable GetAllConversions()
        {
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllConversions");

            return dt;
        }


        public DataTable GetAllDepriciations()
        {
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllDepriciations");

            return dt;
        }

        //public List<MarkupForm> GetMarkup()
        //{
        //    var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetMarkupPercent");

        //    List<MarkupForm> markuplist = new List<MarkupForm>();
        //    MarkupForm markup = new MarkupForm();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {

        //        if (dt.Rows[i]["MarkUpFixed"].ToString() == "")
        //        {
        //            markup.IDPercent = Convert.ToInt32(dt.Rows[i]["ID"]);
        //        }
        //        else
        //        {
        //            markup.IDFixed = Convert.ToInt32(dt.Rows[i]["ID"]);
        //        }
        //        //markup.IDPercent = Convert.ToInt32(dt.Rows[i]["ID"]);
        //        //markup.MarkUpPercent = dt.Rows[i]["MarkUpPercent"].ToString();
        //        markup.MarkUpPercent = !string.IsNullOrEmpty(dt.Rows[i]["MarkUpPercent"].ToString()) ? string.Empty : dt.Rows[i]["MarkUpPercent"].ToString();
        //        //markup.MarkUpFixed = Convert.ToInt32(dt.Rows[i]["MarkUpFixed"]);
        //        markup.MarkUpFixed =dt.Rows[i]["MarkUpFixed"].ToString() == "" ? 0: Convert.ToInt32(dt.Rows[i]["MarkUpFixed"]);
        //        markuplist.Add(markup);
        //    }

        //    return markuplist;
        //}
        public MarkupFormPercent GetMarkupPercent()
        {
            SqlParameter[] param = {
                new SqlParameter("@ID", 1)
                               };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetMarkup", param);

            MarkupFormPercent markup = new MarkupFormPercent();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                markup.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                markup.MarkUpPercent = dt.Rows[i]["MarkUpPercent"].ToString();
                markup.Selected =Convert.ToBoolean(dt.Rows[i]["Selected"]);
            }

            return markup;
        }


        public MarkupFormFixed GetMarkupFixed()
        {
            SqlParameter[] param = {
                new SqlParameter("@ID", 2)
                               };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetMarkup", param);

            MarkupFormFixed markup = new MarkupFormFixed();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                markup.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                markup.MarkUpFixed = dt.Rows[i]["MarkUpFixed"].ToString();
                markup.Selected = Convert.ToBoolean(dt.Rows[i]["Selected"]);
            }

            return markup;
        }

        public DataTable GetConversionData(int ID) //EmployeeID
        {
            SqlParameter[] param = { new SqlParameter("@ConversionID", ID)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetConversionbyID", param);

            return dt;
        }

        public DataTable GetDepreciationData(int ID) //DepreciationID
        {
            SqlParameter[] param = { new SqlParameter("@DepreciationID", ID)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetDepreciationbyID", param);

            return dt;
        }

        public DataTable CreateConversion(ConversionDetailForm Conversion)
        {
            SqlParameter[] param = {
            new SqlParameter("@ConversionName",  !string.IsNullOrEmpty(Conversion.ConversionName)? Conversion.ConversionName : string.Empty),
            new SqlParameter("@Type",  !string.IsNullOrEmpty(Conversion.Type)? Conversion.Type : string.Empty ),
            new SqlParameter("@Year", Conversion.Year),
            new SqlParameter("@Description",!string.IsNullOrEmpty(Conversion.Description)? Conversion.Description : string.Empty),
            new SqlParameter("@Make",!string.IsNullOrEmpty(Conversion.Make)? Conversion.Make : string.Empty),
            new SqlParameter("@MakeID", Convert.ToInt32(Conversion.MakeID)),
            new SqlParameter("@RetailPrice", Convert.ToDecimal(Conversion.RetailPrice)),
            new SqlParameter("@DealerPrice", Convert.ToDecimal(Conversion.DealerPrice)),
            new SqlParameter("@IsActive", true),
            new SqlParameter("@CreatedAt", DateTime.Now)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_CreateConversion", param);

            return dt;
        }

        public DataTable UpdateConversion(ConversionDetailForm Conversion)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID", Conversion.ID),
            new SqlParameter("@ConversionName", !string.IsNullOrEmpty(Conversion.ConversionName)? Conversion.ConversionName : string.Empty),
            new SqlParameter("@Type",  !string.IsNullOrEmpty(Conversion.Type)? Conversion.Type : string.Empty ),
            new SqlParameter("@Year", Conversion.Year),
            new SqlParameter("@Description",!string.IsNullOrEmpty(Conversion.Description)? Conversion.Description : string.Empty),
            new SqlParameter("@Make",!string.IsNullOrEmpty(Conversion.Make)? Conversion.Make : string.Empty),
            new SqlParameter("@MakeID", Convert.ToInt32(Conversion.MakeID)),
            new SqlParameter("@RetailPrice", Convert.ToDecimal(Conversion.RetailPrice)),
            new SqlParameter("@DealerPrice", Convert.ToDecimal(Conversion.DealerPrice)),
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_UpdateConversion", param);

            return dt;
        }

        public DataTable DeleteConversionByID(int ID) //ConversionID
        {
            SqlParameter[] param = {
                new SqlParameter("@ID", ID),
                new SqlParameter("@DeletedAt", DateTime.Now)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_DeleteConversionbyID", param);

            return dt;
        }

        public DataTable DeleteDepreciationByID(int ID) //DepreciationID
        {
            SqlParameter[] param = {
                new SqlParameter("@ID", ID),
                new SqlParameter("@DeletedAt", DateTime.Now)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_DeleteDepreciationbyID", param);

            return dt;
        }

        public DataTable CreateDepreciation(DeprecaitionForm dep)
        {
            SqlParameter[] param = {
            new SqlParameter("@DepreciationPercentage",!string.IsNullOrEmpty(dep.DepreciationPercentage)? dep.DepreciationPercentage : string.Empty),
            new SqlParameter("@AgeInMonths",!string.IsNullOrEmpty(dep.AgeInMonths)? dep.AgeInMonths : string.Empty),
            new SqlParameter("@IsActive", true),
            new SqlParameter("@CreatedAt", DateTime.Now)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_CreateDepreciation", param);

            return dt;
        }

        public DataTable UpdateDepreciation(DeprecaitionForm dep)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID", dep.ID),
            new SqlParameter("@DepreciationPercentage", !string.IsNullOrEmpty(dep.DepreciationPercentage)? dep.DepreciationPercentage : string.Empty),
            new SqlParameter("@AgeInMonths",  !string.IsNullOrEmpty(dep.AgeInMonths)? dep.AgeInMonths : string.Empty )
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_UpdateDepreciation", param);

            return dt;
        }

        public DataTable UpdateMarkUpPercent(MarkUpFormPercent markup)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID", 1),
            new SqlParameter("@Percent", Convert.ToDecimal(markup.MarkUpPercent)),
            new SqlParameter("@UpdatedAt",  DateTime.Now),
            new SqlParameter("@UpdatedBy",  markup.UserID)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_UpdateMarkUpPercent", param);

            return dt;
        }
        public DataTable UpdateMarkUpFixed(MarkUpFormFixed markup)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID", 2),
            new SqlParameter("@Fixed", Convert.ToDecimal(markup.MarkUpFixed)),
            new SqlParameter("@UpdatedAt",  DateTime.Now),
            new SqlParameter("@UpdatedBy",  markup.UserID)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_UpdateMarkUpFixed", param);

            return dt;
        }

        public DataTable GetAllDeduction()
        {
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetAllDeductions");

            return dt;
        }

        public DataTable GetDeductionData(int ID) //DeductionID
        {
            SqlParameter[] param = { new SqlParameter("@DeductionID", ID)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetDeductionbyID", param);

            return dt;
        }

        public DataTable CreateDeduction(DeductionForm deduct)
        {
            SqlParameter[] param = {
            new SqlParameter("@Amount",!string.IsNullOrEmpty(deduct.DeductionAmount) ? deduct.DeductionAmount : string.Empty),
            new SqlParameter("@Description", !string.IsNullOrEmpty(deduct.Description) ? deduct.Description : string.Empty),
            new SqlParameter("@IsActive", true),
            new SqlParameter("@Created", DateTime.Now)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_CreateDeduction", param);

            return dt;
        }

        public DataTable UpdateDeduction(DeductionForm deduct)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID", deduct.ID),
            new SqlParameter("@Amount",!string.IsNullOrEmpty(deduct.DeductionAmount) ? deduct.DeductionAmount : string.Empty),
            new SqlParameter("@Description", !string.IsNullOrEmpty(deduct.Description) ? deduct.Description : string.Empty)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_UpdateDeduction", param);

            return dt;
        }

        public DataTable DeleteDeductionByID(int ID) //DeductionID
        {
            SqlParameter[] param = {
                new SqlParameter("@ID", ID),
                new SqlParameter("@Deleted", DateTime.Now)
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_DeleteDeductionbyID", param);

            return dt;
        }

        public HeaderResponse EmailExist(string email)
        {
            HeaderResponse Response = new HeaderResponse();

            var IsExist = DBContext.vt_UserProfile.Where(x => x.Email.ToLower() == email.ToLower() && x.IsActive == true).ToList();
            if (IsExist.Count > 0)
            {
                Response.status = false;
                Response.message = "Email already exsist.";
            }
            else
            {
                Response.status = true;
                Response.message = "Email not exsist.";
            }

            return Response;
        }
    }
}
