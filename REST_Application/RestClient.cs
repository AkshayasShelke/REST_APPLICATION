using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace REST_Application
{
    public enum httpverb
    {
        GET,
        POST,
        DELETE,
        PUT
    }
    class RestClient
    {
        public string endPoint { get; set; }
        public httpverb httpMethod { get; set; }
        public string postData { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpverb.POST;
        }

        public void getJsonString(string strJson)
        {
            postData = strJson;
        }

        public string makeRequest()
        {
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(postData);

            request.Method = httpMethod.ToString();
            request.ContentType = "application/json";

            string _cred = string.Format("{0} {1}", "Basic", "bmlraGlscDpOaWtBa3NoQDE=");
            request.Headers[HttpRequestHeader.Authorization] = _cred; ;
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(responseStream);
            strResponseValue = reader.ReadToEnd();
            return strResponseValue;
        }

    }
}
