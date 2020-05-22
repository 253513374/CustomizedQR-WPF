using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QrCodeServer.Service
{
    public class Queryservice
    {
        public ServerUriModel ServerUri { set; get; } = new ServerUriModel();

        public string ServerMsg { set; get; }

        public static string Jsonpath = AppDomain.CurrentDomain.BaseDirectory + "UriModel.json";
        public Queryservice()
        {

            SetUriModel();
        }

        private void SetUriModel()
        {
            ServerUri.TojsonFile();
            FileStream fileStream = new FileStream(Jsonpath, FileMode.Open);
            StreamReader sw = new StreamReader(fileStream);
            var jsonvar= sw.ReadToEnd();
            sw.Close();
            sw.Dispose();
            fileStream.Close();
            fileStream.Dispose();
            ServerUri = JsonConvert.DeserializeObject<ServerUriModel>(jsonvar);
        }

        public string GetHttpUri()
        {
            return ServerUri.GetHttpUri();
        }
        public string GetTcpUrl()
        {
            return ServerUri.GetTcpUrl();
        }


        public bool IsUrl()
        {
            return ServerUri.IsUrl();
        }



    }
}
