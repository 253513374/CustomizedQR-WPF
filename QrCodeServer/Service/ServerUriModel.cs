using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeServer.Service
{
    public class ServerUriModel
    {

        public static string Jsonpath = AppDomain.CurrentDomain.BaseDirectory + "UriModel.json";
        public ServerUriModel() {
          
        }

        public string ServerIP { set; get; } = "127.0.0.1";


        public string ServerProt { set; get; } = "2019";


        public string ServerHttpProt { set; get; } = "2020";

        public string GetHttpUri()
        {
            TojsonFile();         
            return "http://" + ServerIP + ":" + ServerHttpProt + "/";
        }

        public string GetTcpUrl()
        {
            TojsonFile(); 
            return "Tcp://" + ServerIP + ":" + ServerProt + "/";
        }

        public void TojsonFile()
        {
            var jsonvar= JsonConvert.SerializeObject(this);
            FileStream fileStream = new FileStream(Jsonpath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.Write(jsonvar);
            sw.Flush();
            sw.Close();
            sw.Dispose();
            fileStream.Close();
            fileStream.Dispose();
        }
        public bool IsUrl()
        {
            if (ServerIP != "" && ServerProt != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
