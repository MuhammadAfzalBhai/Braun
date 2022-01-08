using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        public static async Task<string> CreateNadaRequest(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();

            //var values = new Dictionary<string, string>{{"a",""},};
            //var content = new FormUrlEncodedContent(values);
            //ConfigurationManager.AppSettings["NadaAPIKey"].ToString()
            client.DefaultRequestHeaders.Add("api-key", "7139e338-debb-473e-9f72-ca8bf2bec7ce");
            //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appCon.access_token);
            //client.DefaultRequestHeaders.Add("access_token", Session["AccessToken"].ToString());
            client.DefaultRequestHeaders.ExpectContinue = true;
            HttpResponseMessage resposne;
            resposne = await client.GetAsync(new Uri(url));
            string responseBody = "";
            if (resposne.IsSuccessStatusCode)
            {
                responseBody = resposne.Content.ReadAsStringAsync().Result;
                //var deserialized = JsonConvert.DeserializeObject(responseBody);
            }
            return responseBody;
        }



        public static string CreateNadaRequest(string URL, object obj)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string response = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers["api-key"] = ConfigurationManager.AppSettings["NadaRestApiBaseURL"].ToString();
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



            ////  Type t = obj.GetType();
            ////  PropertyInfo[] props = t.GetProperties();
            ////DateTime startT= Convert.ToDateTime(props[4].GetValue(obj));
            //string DATA = JsonConvert.SerializeObject(obj);
            ////JavaScriptSerializer serializer = new JavaScriptSerializer();
            ////string DATA =  serializer.Serialize(obj);
            //string response = "";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            //request.Method = "POST";
            //request.ContentType = "application/json";
            //request.ContentLength = DATA.Length;
            ////request.Headers = SetRequestHeader();
            //using (Stream webStream = request.GetRequestStream())
            //using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            //{
            //    requestWriter.Write(DATA);
            //}

            //try
            //{
            //    WebResponse webResponse = request.GetResponse();
            //    using (Stream webStream = webResponse.GetResponseStream())
            //    {
            //        if (webStream != null)
            //        {
            //            using (StreamReader responseReader = new StreamReader(webStream))
            //            {
            //                response = responseReader.ReadToEnd();
            //            }
            //        }
            //    }
            //    return response;
            //}
            //catch (Exception e)
            //{
            //    return (e.Message);
            //}

        }

        public static string CreateNadaWebRequest(string url)
        {
            string WEBSERVICE_URL = url;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("api-key", ConfigurationManager.AppSettings["NadaAPIKey"].ToString());

                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            return jsonResponse;
                            //Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
                //Console.WriteLine(ex.ToString());
            }
            return "";
        }


        public static string WebRequestNada(string url)
        {
            string WEBSERVICE_URL = url;
            string data = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("api-key", ConfigurationManager.AppSettings["NadaAPIKey"].ToString());

                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            data = jsonResponse;
                            //Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
            return data;
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
