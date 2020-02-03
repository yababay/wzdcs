using System.Collections.Generic;
using System.Net;

namespace yababay.wzdcs
{
    internal enum Methods {GET,  POST, PUT, DELETE}
    internal enum Subjects {None, Keys, Info, Count}
    internal enum Scopes {None, Uniq, All, Files, Archive}

    internal class WZDHeader
    {
        internal WZDHeader(string n, string v)
        {
            this.Value = n + ": " + v;
        }
        internal WZDHeader(string n, int v)
        {
            this.Value = n + ": " + v.ToString();
        }
        private string Value {get; set;}
        public override string ToString()
        {
            return Value;
        }
    }
    internal class WZDRequestFactory
    {
        /*
        public static WebRequest GetCountArchive(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Archive, subject: Subjects.Count); 
        }
        public static WebRequest GetCountFiles(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Files, subject: Subjects.Count); 
        }
        public static WebRequest GetCountAll(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.All, subject: Subjects.Count); 
        }
        public static WebRequest GetCountUniq(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Uniq, subject: Subjects.Count); 
        }
        public static WebRequest GetInfoArchive(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Archive, subject: Subjects.Info); 
        }
        public static WebRequest GetInfoFiles(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Files, subject: Subjects.Info); 
        }
        public static WebRequest GetInfoAll(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.All, subject: Subjects.Info); 
        }
        public static WebRequest GetInfoUniq(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Uniq, subject: Subjects.Info); 
        }
        public static WebRequest GetKeysArchive(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Archive, subject: Subjects.Keys); 
        }
        public static WebRequest GetKeysFiles(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Files, subject: Subjects.Keys); 
        }
        public static WebRequest GetKeysAll(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.All, subject: Subjects.Keys); 
        }
        public static WebRequest GetKeysUniq(string url, json = false, offset = OFFSET, limit = LIMIT) 
        { 
            return Create(url, _json: json, offs: offset, lim: limit, scope: Scopes.Uniq, subject: Subjects.Keys); 
        }
        public static WebRequest GetKeys(string url, bool json = false, int offset = OFFSET, int limit = LIMIT) 
        { 
            return Create(url, _json: json, _offset: offset, _limit: limit, scope: Scopes.Uniq, subject: Subjects.Keys); 
        }
        */


        internal static WebRequest Create(
                string url,
                bool archive, 
                bool json, 
                int  limit, 
                int  offset, 
                Methods method, 
                Scopes scope, 
                Subjects subject, 
                List<WZDHeader> headers
        )
        {
            if(headers == null ) headers = new List<WZDHeader>();
            WebRequest request = WebRequest.Create(url);
            if(method == Methods.GET) request.Method = "GET";
            if(method == Methods.PUT) request.Method = "PUT";
            if(method == Methods.POST) request.Method = "POST";
            if(method == Methods.DELETE) request.Method = "DELETE";
            if(archive) headers.Add(new WZDHeader("Archive", 1));
            if(json) headers.Add(new WZDHeader("JSON", 1));
            if(offset > 0) headers.Add(new WZDHeader("Offset", offset));
            if(limit != 1000) headers.Add(new WZDHeader("Limit", limit));
            if(subject == Subjects.Keys && scope == Scopes.Uniq) headers.Add(new WZDHeader("Keys", 1));
            if(subject == Subjects.Keys && scope == Scopes.All) headers.Add(new WZDHeader("KeysAll", 1));
            if(subject == Subjects.Keys && scope == Scopes.Files) headers.Add(new WZDHeader("KeysFiles", 1));
            if(subject == Subjects.Keys && scope == Scopes.Archive) headers.Add(new WZDHeader("KeysArchive", 1));
            if(subject == Subjects.Info && scope == Scopes.Uniq) headers.Add(new WZDHeader("Info", 1));
            if(subject == Subjects.Info && scope == Scopes.All) headers.Add(new WZDHeader("InfoAll", 1));
            if(subject == Subjects.Info && scope == Scopes.Files) headers.Add(new WZDHeader("InfoFiles", 1));
            if(subject == Subjects.Info && scope == Scopes.Archive) headers.Add(new WZDHeader("InfoArchive", 1));
            if(subject == Subjects.Count && scope == Scopes.Uniq) headers.Add(new WZDHeader("Count", 1));
            if(subject == Subjects.Count && scope == Scopes.All) headers.Add(new WZDHeader("CountAll", 1));
            if(subject == Subjects.Count && scope == Scopes.Files) headers.Add(new WZDHeader("CountFiles", 1));
            if(subject == Subjects.Count && scope == Scopes.Archive) headers.Add(new WZDHeader("CountArchive", 1));
            foreach(var header in headers)
            {
                request.Headers.Add(header.ToString());
            }
            return request;
        }
    }
}

