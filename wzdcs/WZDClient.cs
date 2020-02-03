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

    public class WZDClient
    {
        // CRUD

        public static WZDResponse GetFile(string url) 
        { 
            return GetResponse(Create(url)); 
        }

        public static WZDResponse DeleteFile(string url) 
        { 
            return GetResponse(Create(url, method: Methods.DELETE)); 
        }

        public static WZDResponse PostFile(string url, string content) 
        { 
            return GetResponse(Create(url, method: Methods.POST), content); 
        }

        public static WZDResponse PutFile(string url, string content) 
        { 
            return GetResponse(Create(url, method: Methods.PUT), content); 
        }

        // Statistics
        
        public static WZDResponse GetCountArchive(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Archive, subject: Subjects.Count)); 
        }

        public static WZDResponse GetCountFiles(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Files, subject: Subjects.Count)); 
        }

        public static WZDResponse GetCountAll(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.All, subject: Subjects.Count)); 
        }

        public static WZDResponse GetCount(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Uniq, subject: Subjects.Count)); 
        }

        public static WZDResponse GetInfoArchive(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Archive, subject: Subjects.Info)); 
        }

        public static WZDResponse GetInfoFiles(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Files, subject: Subjects.Info)); 
        }

        public static WZDResponse GetInfoAll(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.All, subject: Subjects.Info)); 
        }

        public static WZDResponse GetInfo(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Uniq, subject: Subjects.Info)); 
        }

        public static WZDResponse GetKeysArchive(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Archive, subject: Subjects.Keys)); 
        }

        public static WZDResponse GetKeysFiles(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Files, subject: Subjects.Keys)); 
        }

        public static WZDResponse GetKeysAll(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.All, subject: Subjects.Keys)); 
        }

        public static WZDResponse GetKeys(string url, bool json = true, int offset = OFFSET, int limit = LIMIT) 
        { 
            return GetResponse(Create(url, json: json, offset: offset, limit: limit, scope: Scopes.Uniq, subject: Subjects.Keys)); 
        }

        private static WZDResponse GetResponse(WebRequest request, string content = null)
        {
            if(content != null){
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                request.ContentType = "text/plain";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            WZDResponse resp = new WZDResponse();
            resp.Response = reader.ReadToEnd();
            resp.Description = ((HttpWebResponse)response).StatusDescription;
            reader.Close();
            responseStream.Close();
            response.Close();
            return resp;
        }

        private const int LIMIT = 1000;
        private const int OFFSET = 0;

        private static WebRequest Create(
                string url,
                bool archive = false, 
                bool json = false, 
                int  limit = LIMIT, 
                int  offset = OFFSET, 
                Methods method = Methods.GET, 
                Scopes scope = Scopes.None, 
                Subjects subject = Subjects.None, 
                List<WZDHeader> headers = null
        ) 
        { 
            return WZDRequestFactory.Create(url, archive, json, limit, offset, method, scope, subject, headers); 
        }
    }
}
