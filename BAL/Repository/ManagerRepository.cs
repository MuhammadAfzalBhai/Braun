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
    public class ManagerRepository : BaseRespoitory
    {
        public ManagerRepository()
            : base()
        { }


        public ManagerRepository(vt_BraunAppEntities ContextDB)
            : base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public List<QuotesListMonths> GetQuoetsoflastsixmonths()
        {
            List<QuotesListMonths> _actuallist = new List<QuotesListMonths>();
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetLastSIxMonthQuotesAsPerMonth");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    QuotesListMonths listitem = new QuotesListMonths();
                    listitem.MonthName = (dt.Rows[i].ItemArray[2]).ToString();
                    listitem.MonthNumber = (dt.Rows[i].ItemArray[0]).ToString();
                    listitem.Year = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                    listitem.NoOfQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[3]);
                    _actuallist.Add(listitem);
                }
            }

            return _actuallist;
        }

        public GetManagerDahboardCounterValues GetManagerDashboardCounterResult(int id)
        {
            GetManagerDahboardCounterValues obj = new GetManagerDahboardCounterValues();

           

            SqlParameter[] param = {
            new SqlParameter("@ID",  id ),   //ID of MarkUp Percent
                               };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetManagerDashboradResult", param);
            if (dt.Rows.Count >= 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    obj.message = "Result is Found";
                    obj.status = true;
                    obj.ApprovedQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                    obj.RejectedQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                    obj.PendingQuotes = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                    obj.TotalUsers = Convert.ToInt32(dt.Rows[i].ItemArray[3]);
                }
            }

            else
            {
                obj.message = "Result is not Found";
                obj.status = false;
                obj.RejectedQuotes = 0;
                obj.PendingQuotes = 0;
                obj.ApprovedQuotes = 0;
                obj.TotalUsers = 0;
            }

            return obj;
        }
        public DataTable GetQuotesByEmpIdForApp(int EmpId)
        {
            SqlParameter[] par =
            {
                new SqlParameter("@EmpId",Convert.ToInt32(EmpId))
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetQuotesByEmpIdForApproval", par);

            return dt;
        }
        public DataTable ApproveDetails(int QuoteID)
        {
            SqlParameter[] par =
            {
                new SqlParameter("@QuoteID",Convert.ToInt32(QuoteID))
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetQuotesByQuoteIDForApproval", par);

            return dt;
        }
        public DataTable ApproveQouteByID(Approvebyid qoute)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  qoute.ID ),
            new SqlParameter("@Comment",  qoute.Comment == null? "":qoute.Comment )
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_ApproveQoutebyID", param);

            return dt;
        }
        public DataTable RejectQouteByID(Rejectbyid qoute)
        {
            SqlParameter[] param = {
            new SqlParameter("@ID",  qoute.ID ),
            new SqlParameter("@Comment",  qoute.Comment == null? "":qoute.Comment  )
                               };

            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_RejectQoutebyID", param);

            return dt;
        }

        public DataTable GetRejectedQuotesByManager(int EmpId)
        {
            SqlParameter[] par =
            {
                new SqlParameter("@EmpId",Convert.ToInt32(EmpId))
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetRejectedQuotesByManager", par);

            return dt;
        }
        public DataTable GetApprovedQuotesByManager(int ManagerId)
        {
            SqlParameter[] par =
            {
                new SqlParameter("@EmpId",Convert.ToInt32(ManagerId))
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetApprovedQuotesByManager", par);

            return dt;
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
        public DataTable GetQoutesforReportManager(int Id)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@MangrID", Id),
            };
            var dt = Entity_Common.get_SP_DataTable(DBContext, "sp_GetQuotesManager", param);

            return dt;
        }
    }
}
