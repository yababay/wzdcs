using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace yababay.wzdcs
{

    public class WZDResponse
    {
        public string Description {get; set;}
        public string Response {get; set;}
    }

    public class WZDHeader
    {
        public string Name {get; set;}
        public string Value {get; set;}
    }

    public class WZDClient
    {
        public static WZDResponse SentStringRequest(string url, string content, List<WZDHeader> headers, string method = "GET")
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            if(content != null && (method == "PUT" || method == "POST")){
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                request.ContentType = "text/plain";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            WZDResponse resp = new WZDResponse();
            resp.Response = reader.ReadToEnd();
            resp.Description = ((HttpWebResponse)response).StatusDescription;
            response.Close();
            return resp;
        }
    }
}
