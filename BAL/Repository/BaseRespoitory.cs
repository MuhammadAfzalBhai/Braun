using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBEntities;

namespace BAL.Repository
{
    public class BaseRespoitory
    {
        public vt_BraunAppEntities DBContext;

        public BaseRespoitory()
        {
            DBContext = new vt_BraunAppEntities();
        }

        public BaseRespoitory(vt_BraunAppEntities ContextDB)
        {
            DBContext = ContextDB;
        }

        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DBContext != null)
                {
                    DBContext.Dispose();
                    DBContext = null;
                }
            }
        }
        ~BaseRespoitory()
        {
            Dispose(false);
        }
    }
}
