using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using static Braunability_ViewModal.Model.BraunVM_Request;
using static Braunability_ViewModal.Model.BraunVM_Response;
using BAL.Repository;
using DAL.DBEntities;
using BraunApp_ViewModel.Model;
using Braunability_ViewModal.Model;
using System.Web.Helpers;

namespace Bruneability_API.Controllers
{
    public class LoginController : ApiController
    {
        LoginRespository Loginrepo;
        public LoginController()
        {
            Loginrepo = new LoginRespository(new vt_BraunAppEntities());
        }
        // GET: Login

        [Route("api/Login/LoginUser")]
        [HttpPost]
        public AuthenticationResponseModel LoginUser(LoginRequest loginRequest)
        {
            AuthenticationResponseModel sess = new AuthenticationResponseModel();
            if (ModelState.IsValid)
            {
                loginRequest.Password = vt_Common.Decrypt(loginRequest.Password);
                vt_UserProfile model = Loginrepo.Authentication(loginRequest);
                if (model != null)
                {
                    List<vt_UserPermissions> permission = Loginrepo.AuthenticationPermission(model.RoleID);
                    sess.accesstoken = utilityRespository.Accesstoken(model.ID, (int)model.RoleID, loginRequest.Isportal);
                    //sess.accesstoken= Braunability_ViewModal.Model.utilityRespository.CheckAccesstoken(model.ID, (int)model.RoleID, loginRequest.Isportal);
                    sess.User = model;
                    sess.message = "Login Successfully";
                    sess.status = true;
                    sess.UserPermission = permission;
                }

            }
            return sess;
        }

        [Route("api/Login/ValidateUserEmail")]
        [HttpPost]
        public HeaderResponse ValidateUserEmail(EmployeeForm Emp)
        {
            HeaderResponse Response = new HeaderResponse();
            //if (ModelState.IsValid)
            //{
            if (Emp.Email != null || Emp.Email != "")
            {
                vt_UserProfile model = Loginrepo.ValidateEmployee(Emp.Email);

                if (model == null)
                {
                    Response.status = true;
                    Response.message = "Email not exsist.";
                }
                else
                {
                    Response.status = false;
                    Response.message = "Email already exsist.";
                }
            }
            else
            {
                Response.status = false;
                Response.message = "Email in Null.";
            }

            //}
            //else
            //{
            //    Response.status = false;
            //    Response.message = "Invalid Parameters.";
            //}
            return Response;
        }


        [Route("api/Login/IsEmailExist")]
        [HttpGet]
        public HeaderResponse IsEmailExist(string email)
        {
            HeaderResponse Response = new HeaderResponse();
            //if (ModelState.IsValid)
            //{
            if (email != null || email != "")
            {
                vt_BraunAppEntities db = new vt_BraunAppEntities();
                vt_UserProfile model = Loginrepo.ValidateEmployee(email);

                if (model != null)
                {
                    var decryptesPassword = db.vt_UserProfile.Where(x => x.Email.Equals(email)).Select(x => x.Password).FirstOrDefault();
                    var encryptPassword = vt_Common.Decrypt(decryptesPassword);
                    string EMailBody = @"<!doctype html>
<html>
  <head>
    <meta name='viewport' content='width = device-width'>
        <meta http-equiv = 'Content-Type' content = 'text/html; charset=UTF-8'>
         
             <title> Simple Transactional Email</title>
            
                <style>
            

                .wrapper{
                        background-image: url(http://braunability.clientdemos.me/assets/images/nav.png);
                        background-size: contain;
                        background-repeat: no-repeat;
                    }
    .footer{
                        background-image: linear-gradient(to right, #3badcb , #233b7d);
    color: white;
                    }
    .btn-primary table td:hover {
                        background-color: #ffffff !important;
}
                    @media only screen and (max-width: 620px) {
                        table[class=body] h1 {
        font-size: 28px !important;
        margin-bottom: 10px !important;
      }
    table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
        font-size: 16px !important;
      }
table[class=body] .wrapper,
            table[class=body] .article {
        padding: 10px !important;
      }
      table[class=body] .content {
        padding: 0 !important;
      }
      table[class=body] .container {
        padding: 0 !important;
        width: 100% !important;
      }
      table[class=body] .main {
        border-left-width: 0 !important;
        border-radius: 0 !important;
        border-right-width: 0 !important;
      }
      table[class=body] .btn table
{
    width: 100% !important;
}
table[class=body] .btn a
{
    width: 100% !important;
}
table[class=body] .img-responsive {
        height: auto !important;
        max-width: 100% !important;
        width: auto !important;
      }
    }

    
    @media all
{
      .ExternalClass
    {
    width: 100 %;
    }
      .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div
    {
        line-height: 100 %;
    }
      .apple-link a
    {
    color: inherit !important;
        font-family: inherit !important;
        font-size: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
        text-decoration: none !important;
    }
      .btn-primary table td:hover
    {
        background-color: #34495e !important;
      }
      .btn-primary a:hover
    {
        background-color: #34495e !important;
        border-color: #34495e !important;
      }
}
    </style>
  </head>
  <body class='' style='background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;'>
    <table border = '0' cellpadding='0' cellspacing='0' class='body' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background-color: #f6f6f6;'>
     
      <tr>
        <td style = 'font-family: sans-serif; font-size: 14px; vertical-align: top;'> &nbsp;</td>
        <td class='container' style='font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; Margin: 0 auto; max-width: 580px; padding: 10px; max-width: 580px;'>
          <div class='content' style='box-sizing: border-box; display: block; Margin: 0 auto; max-width: 580px; padding: 10px;'>
           

            <!-- START CENTERED WHITE CONTAINER -->
            <span class='preheader' style='color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;'></span>
            <table class='main' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background: #ffffff; border-radius: 3px;'>

              <!-- START MAIN CONTENT AREA -->
              <tr>
                <td class='wrapper' style='font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;background-image: url(http://braunability.clientdemos.me/assets/images/nav.png);background-size: contain;background-repeat: no-repeat;'>
                  <table border = '0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;'>
                    <tr>
                      <td>
                        <img src = 'http://braunability.clientdemos.me/assets/images/login-logo.png' alt='' style='max-width:200px;margin-left: 30px;margin-top: 15px;'>
                      </td>
                    </tr>
                    <tr style = 'height:60px;'></ tr>
                    <tr>
                      <td style='font-family: sans-serif; font-size: 14px; vertical-align: top;'>
                        <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;font-weight: bold;font-size: 18px;'> Hi there,</p>
                        <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>Thank you for using Braunability. Your Email and Password are:</p>
                        <p style = 'font-family: sans-serif; text-align: justify; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'> Email: <strong>" + email + @"</strong></p>
                        <p style = 'font-family: sans-serif; text-align: justify; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'> Password: <strong>" + encryptPassword + @"</strong></p>
                        <table border = '0' cellpadding= '0' cellspacing= '0' class='btn btn-primary' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; box-sizing: border-box;'>
                          <tbody>
                            <tr>
                              <td align = 'left' style='font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;'>
                                <table border = '0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;'>
                                  <tbody>
                                    <tr>
                                      <td style = 'font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;background-color:#ffff!important'> <a href='http://braunability.clientdemos.me' target='_blank' style='display: inline-block; color: #ffffff;  background-image: linear-gradient(to right, #3badcb , #233b7d); border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #3498db;border-radius: 30px;'>Login</a> </td>
                                    </tr>
                                  </tbody>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'> In case of any difficulty kindly contact site admin.</p>
    <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'> Best Regards,</p>                    
    <p style = 'font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;font-weight: bold;font-size: 18px;'>Braunability Support Team</p>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </table>

            <!-- START FOOTER -->
            <div class='footer' style='clear: both;  text-align: center; width: 100%;background-image: linear-gradient(to right, #3badcb , #233b7d);color: white;'>
              <table border = '0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;'>
                <tr>
                  <td class='content-block' style='font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px;  text-align: center;'>
                    <span class='apple-link' style=' font-size: 12px; text-align: center;'>Braunability Inc, 631 West 11th Street-Winamac, IN 46996 </span>
                    <br>Don't reply to this email.
                  </td>
                </tr>
                <tr>
                  <td class='content-block powered-by' style='font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px;  text-align: center;'>
                    Powered by<a href='https://www.nada.com/' style=' font-size: 12px; text-align: center; text-decoration: none;color: white;'> NADA</a>.
                  </td>
                </tr>
              </table>
            </div>
            <!-- END FOOTER -->

          <!-- END CENTERED WHITE CONTAINER -->
          </div>
        </td>
        <td style = 'font-family: sans-serif; font-size: 14px; vertical-align: top;'>&nbsp;</td>
      </tr>
    </table>
  </body>
</html>
";
                    // string EMailBody = "<html></html> UserName: "+email+ "\n"+ "Password: "+ encryptPassword;
                    WebMail.SmtpServer = "smtp.gmail.com";
                    //gmail port to send emails  
                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = true;
                    //sending emails with secure protocol  
                    WebMail.EnableSsl = true;
                    //EmailId used to send emails from application  
                    WebMail.UserName = "braunabilitypvt@gmail.com";
                    WebMail.Password = "@a_12345";

                    //Sender email address.  
                    WebMail.From = "braunabilitypvt@gmail.com";

                    //Send email  
                    WebMail.Send(to: email, subject: "Forgot Password Request", body: EMailBody, cc: null, bcc: null, isBodyHtml: true);


                    Response.status = true;
                    Response.message = "Email send successfully";


                }
                else
                {
                    Response.status = false;
                    Response.message = "Email not exsist.";
                }
            }
            else
            {
                Response.status = false;
                Response.message = "Email in Null.";
            }

            //}
            //else
            //{
            //    Response.status = false;
            //    Response.message = "Invalid Parameters.";
            //}
            return Response;
        }
    }
}
    

