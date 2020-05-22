using QrCodeEncoding;
using QrCodeEncoding.DrawQrCode.ConfigJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeServer.Service
{
    public class RequestModel
    {
        public string DateTimes { set; get; }

        public QrCodeOptions JsonParameter { set; get; }
    }
}
