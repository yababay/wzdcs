using System.Collections.Generic;
using System.Net;
using System.IO;
using System;

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
            if(subject != Subjects.None) headers.Add(new WZDHeader("Sea", "1"));
            WebRequest request = WebRequest.Create(url);
            if(method == Methods.GET) request.Method = "GET";
            if(method == Methods.PUT) request.Method = "PUT";
            if(method == Methods.POST) request.Method = "POST";
            if(method == Methods.DELETE) request.Method = "DELETE";
            if(archive) headers.Add(new WZDHeader("Archive", "1"));
            if(json) headers.Add(new WZDHeader("JSON", "1"));
            if(offset > 0) headers.Add(new WZDHeader("Offset", offset));
            if(limit != 1000) headers.Add(new WZDHeader("Limit", limit));
            if(subject == Subjects.Keys && scope == Scopes.Uniq) headers.Add(new WZDHeader("Keys", "1"));
            if(subject == Subjects.Keys && scope == Scopes.All) headers.Add(new WZDHeader("KeysAll", "1"));
            if(subject == Subjects.Keys && scope == Scopes.Files) headers.Add(new WZDHeader("KeysFiles", "1"));
            if(subject == Subjects.Keys && scope == Scopes.Archive) headers.Add(new WZDHeader("KeysArchive", "1"));
            if(subject == Subjects.Info && scope == Scopes.Uniq) headers.Add(new WZDHeader("KeysInfo", "1"));
            if(subject == Subjects.Info && scope == Scopes.All) headers.Add(new WZDHeader("KeysInfoAll", "1"));
            if(subject == Subjects.Info && scope == Scopes.Files) headers.Add(new WZDHeader("KeysInfoFiles", "1"));
            if(subject == Subjects.Info && scope == Scopes.Archive) headers.Add(new WZDHeader("KeysInfoArchive", "1"));
            if(subject == Subjects.Count && scope == Scopes.Uniq) headers.Add(new WZDHeader("KeysCount", "1"));
            if(subject == Subjects.Count && scope == Scopes.All) headers.Add(new WZDHeader("KeysCountAll", "1"));
            if(subject == Subjects.Count && scope == Scopes.Files) headers.Add(new WZDHeader("KeysCountFiles", "1"));
            if(subject == Subjects.Count && scope == Scopes.Archive) headers.Add(new WZDHeader("KeysCountArchive", "1"));
            Console.WriteLine(request.Method);
            Console.WriteLine(headers.Count);
            Console.WriteLine(archive);
            Console.WriteLine("=== Headers: ===");
            foreach(var header in headers)
            {
                Console.WriteLine(header.ToString());
                request.Headers.Add(header.ToString());
            }
            Console.WriteLine(url);
            return request;
        }
    }
}

