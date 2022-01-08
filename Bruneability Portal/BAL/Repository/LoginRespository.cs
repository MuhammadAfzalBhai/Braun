using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBEntities;
using static Braunability_ViewModal.Model.BraunVM_Request;

namespace BAL.Repository
{
    public class LoginRespository:BaseRespoitory
    {
        public LoginRespository()
            :base()
        { }


        public LoginRespository(vt_BraunAppEntities ContextDB)
            :base(ContextDB)
        {
            DBContext = ContextDB;
        }

        public vt_UserProfile Authentication(LoginRequest LoginRequest)
        {
            var data = DBContext.ExclueAll().vt_UserProfile.AsNoTracking().Where(x => x.Email == LoginRequest.Email && x.Password ==LoginRequest.Password && x.IsApproved == true && x.IsActive==true && x.Deleted==null).FirstOrDefault();

            //vt_UserProfile model = (from s1 in DBContext.ExclueAll().vt_UserProfile.AsNoTracking()
            //        where s1.Email == LoginRequest.Email && s1.Password==LoginRequest.Password && s1.IsActive==true && s1.Deleted==null
            //        select s1).SingleOrDefault();
           

            return data;
        }

        public vt_UserProfile ValidateEmployee(string UserEmail)
        {
            var data = DBContext.ExclueAll().vt_UserProfile.AsNoTracking().Where(x => x.Email == UserEmail).FirstOrDefault();

            return data;
        }

        public List<vt_UserPermissions> AuthenticationPermission(int? RoleID)
        {
            var data = DBContext.ExclueAll().vt_UserPermissions.AsNoTracking().Where(x => x.Role== RoleID && x.IsActive == true).ToList();
            return data;
        }
    }
}
