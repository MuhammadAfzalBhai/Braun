using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BraunApp_ViewModel.Model;
using static Braunability_ViewModal.Model.utilityRespository;

namespace Bruneability_Portal.Controllers
{
    public class BaseController : Controller
    {
        BraunSession _Session;
        public BraunSession CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["BraunSession"] != null)
                {
                    _Session = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
                }
                return _Session;
            }
            set
            {
                _Session = value;
            }
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string redirectController = string.Empty;
            string redirectAction = string.Empty;
            string ErrorMessage = string.Empty;
            bool IsvalidToken = true;
            bool authorizedRequest = true;
            if (System.Web.HttpContext.Current.Session["BraunSession"] != null)
            {
                BraunSession sess = (BraunSession)System.Web.HttpContext.Current.Session["BraunSession"];
                var rd = System.Web.HttpContext.Current.Request.RequestContext.RouteData;
                string currentController = rd.GetRequiredString("controller");
                string currentAction = rd.GetRequiredString("action");
                if (sess != null)
                {
                    IsvalidToken = ValidationAccesstoken(sess.accesstoken);
                    if (IsvalidToken)
                    {
                        string Page_URL = currentController + "/" + currentAction;
                        var right = sess.pagelist.Where(x => x.Controller.ToLower() == currentController.ToLower()).FirstOrDefault();
                        //var right = sess.pagelist.Where(x => x.PageURL.ToLower() == Page_URL.ToLower() && x.Controller.ToLower() == currentController.ToLower()).FirstOrDefault();
                        if (right == null)
                        {
                            redirectController = "Error";
                            redirectAction = "Permission";
                            ErrorMessage = "UnAuthorized";
                            authorizedRequest = false;
                        }
                        else
                        {
                            redirectController = currentController;
                            redirectAction = currentAction;
                            ErrorMessage = "Authorized";
                            authorizedRequest = true;
                        }
                    }
                    else
                    {
                        redirectController = "Login";
                        redirectAction = "Index";
                        ErrorMessage = "InValid Token";
                        authorizedRequest = false;
                    }
                    if (!authorizedRequest)
                    {
                        filterContext.Result = new RedirectResult("~/" + redirectController + "/" + redirectAction + "");
                    }
                }
               
                //var Allow = sess.pagelist.Where()
            }
            else
            {

                filterContext.Result = new RedirectResult("~/Login");

            }
            base.OnActionExecuting(filterContext);
        }
    }
}