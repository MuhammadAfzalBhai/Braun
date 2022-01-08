using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BraunApp_ViewModel.Model
{
    public class HttpApi
    { 
        public static WebHeaderCollection SetRequestHeader()
        {
            WebHeaderCollection header = new WebHeaderCollection();
            header.Add("Braun_Token", "abc");
            return header;
        }

        #region Post
        public static string CreateRequest(string URL)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string response = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            //request.Headers = SetRequestHeader();
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        public static string CreateRequest(string URL, object obj)
        {
            //  Type t = obj.GetType();
            //  PropertyInfo[] props = t.GetProperties();
            //DateTime startT= Convert.ToDateTime(props[4].GetValue(obj));
            string DATA=  JsonConvert.SerializeObject(obj);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string DATA =  serializer.Serialize(obj);
           string response = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = DATA.Length;
            //request.Headers = SetRequestHeader();
            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(DATA);
            }

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                return (e.Message);
            }

        }

        #endregion 

        #region asyncPost
        public async static Task<string> AsyncCreateRequest(string URL)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string response = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            //request.Headers = SetRequestHeader();
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response = await responseReader.ReadToEndAsync();
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }
        public async static Task<string> AsyncCreateRequest(string URL, object obj)
        {
            //  Type t = obj.GetType();
            //  PropertyInfo[] props = t.GetProperties();
            //DateTime startT= Convert.ToDateTime(props[4].GetValue(obj));
            string DATA = JsonConvert.SerializeObject(obj);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string DATA =  serializer.Serialize(obj);
            string response = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = DATA.Length;
            //request.Headers = SetRequestHeader();
            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(DATA);
            }

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response =await responseReader.ReadToEndAsync();
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                return (e.Message);
            }

        }
        #endregion asyncPost
    }
}
