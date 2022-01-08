using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraunApp_ViewModel.Model
{
    public class BraunSession
    {
        public vt_UserProfile SessionUser { get; set; }
        public List<vt_UserPermissions> pagelist { get; set; }
        public string accesstoken { get; set; }

    }
}
