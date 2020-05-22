using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeEncoding.DrawQrCode.Config
{
   public class QrBitmapMatrix
    {
        public Point Point { set; get; } = new Point();

        public bool BoolVar { set; get; }

        public Rectangle Rectangle { set; get; }

        public Tag Tag { set; get; }

        public int ModuleSize { set; get; }
    }

 
}
