using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeEncoding.DrawQrCode.Config
{
    public class EncoderOptions
    {

        /// <summary>
        /// 二维码大小（毫米）
        /// </summary>
        public int  QrCodeWidthMM;

        public int DFT { set; get; } = 0;

        public BitMatrix Matrix { set; get; }



        /// <summary>
        /// 线条宽度
        /// </summary>
        public double widthdoubleFromat { set; get; } = 0.05;
        /// <summary>
        /// 密度因数
        /// </summary>
        public double densitydoubleFromat { set; get; } = 0.03;
      
      

        /// <summary>
        /// 需要绘制的坐标
        /// </summary>
        public List<QrBitmapMatrix> BitMats { set; get; } = new List<QrBitmapMatrix>(2000);

        /// <summary>
        /// 需要绘制的大小参数
        /// </summary>
        public QrRect QrRect { set; get; }


        /// <summary>
        /// 自定义样式
        /// </summary>
        public List<QrCodeCustom> QrCodeCustoms { set; get; } = new List<QrCodeCustom>();

        /// <summary>
        /// 图像分辨率DPI
        /// </summary>
        public int DpiInch { set; get; }


        public bool IsTopLogoImg { set; get; }
        /// <summary>
        /// logo图像路径
        /// </summary>
        public string LogoImgPath { set; get; }


        /// <summary>
        /// 背景大小（二维码图片大小）
        /// </summary>
        public Size BitmapSize {
            get { return new Size(QrRect.QrCodeMinWidth, QrRect.QrCodeMinWidth); }
        }

        public int BitmapWidth { get { return QrRect.QrCodeMinWidth; } }


    }
}
