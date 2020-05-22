using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using  QrCodeEncoding;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using QrCodeEncoding.DrawQrCode.ConfigJson;
using QrCodeEncoding.DrawQrCode;

namespace QrCodeServer.Service
{
    public class EnQrCodeService
    {
        public EnQrCodeService() { }

        public List<RequestModel> requestModels { set; get; } = new List<RequestModel>(100);
        public static string Jsonpath = AppDomain.CurrentDomain.BaseDirectory + "RequestModel.json";
        QrCodeOptions CodeOptions { set; get; }
        public string Expjson()
        {
           return JsonConvert.SerializeObject(new QrCodeOptions());
        }


        public byte[] Encoder(string jsonvar)
        {
           CodeOptions = JsonConvert.DeserializeObject<QrCodeOptions>(jsonvar);
           Task.Run(()=> AddJson(CodeOptions)).Wait();
           Bitmap bitmap = new DrawQrCode(CodeOptions).QRCodeEncoder();
            // int byteCount = bitmap.GetBy
            return Bitmap2Byte(bitmap);
        }

        private  byte[] Bitmap2Byte(Bitmap bitmap)
        {
            using (Stream stream1 = new MemoryStream())
            {
                bitmap.Save(stream1,ImageFormat.Bmp);
                byte[] byteCount = new byte[stream1.Length];
                stream1.Position = 0;
                stream1.Read(byteCount, 0, (int)stream1.Length);
                stream1.Close();
                return byteCount;
            }
        }



        private  void AddJson(QrCodeOptions qrCodeOptions)
        {
            requestModels.Insert(0,new RequestModel()
            {
                DateTimes = DateTime.Now.ToString(),
                JsonParameter = qrCodeOptions
            });
            var jsonvar = JsonConvert.SerializeObject(requestModels);
            if (!File.Exists(Jsonpath))
            {
                File.Create(Jsonpath);

            }
            FileStream fileStream = new FileStream(Jsonpath, FileMode.Truncate);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.Write(jsonvar);
            sw.Flush();
            sw.Close();
            sw.Dispose();
            fileStream.Close();
            fileStream.Dispose();
        }
    }
}
