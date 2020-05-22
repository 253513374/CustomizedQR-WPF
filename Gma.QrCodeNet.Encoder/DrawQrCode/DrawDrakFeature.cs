using QrCodeEncoding.DrawQrCode.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace QrCodeEncoding.DrawQrCode
{
    public class DrawDrakFeature
    {


        public Bitmap DrawQrCode(EncoderOptions options)
        {

            Bitmap bitmap = new DrawDarkSquare().DrawQrCode(options);

          //  bitmap.Save(@"D:\bitmap.bmp", ImageFormat.Bmp);
            return  DrawDarkModulePolygon(bitmap, options);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"> 二维码图片</param>
        /// <param name="modulesize">模块大小</param>
        /// <param name="dft">特征密度</param>
        /// <returns></returns>
        public Bitmap DrawDarkModulePolygon(Bitmap bitmap,EncoderOptions options)
        {
            //Bitmap bitmap = (Bitmap)bitmap1.Clone();
            bitmap.SetResolution(options.DpiInch, options.DpiInch);
            // Size size = new Size((int)Math.Round(options.QrRect.ModuleSize * 2.0, 0), options.QrRect.ModuleSize / 4);
            Size size = GetRandomSize(options);

            Scope scope = GetScope(bitmap,options);

            List<PointF[]> pointFs = new QrRect().GetRandomPointF(scope, size, (int)(options.DFT * options.densitydoubleFromat)); 

            Graphics graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < pointFs.Count; i++)
            {
                graphics.FillPolygon(Brushes.White, pointFs[i]);

            }

            graphics.Flush();
            graphics.Dispose();

            int row = options.Matrix.Width-7;
            int mudleSize = options.QrRect.ModuleSize;

            int x = row * mudleSize + mudleSize;
            int y = mudleSize;


           // bitmap.Save(@"D:\DrawDarkModulePolygon.bmp", ImageFormat.Bmp);
            Bitmap map= bitmap.Clone(new Rectangle(x, y, mudleSize * 7, mudleSize * 7), System.Drawing.Imaging.PixelFormat.Format32bppRgb);
           // map.Save(@"D:\Polygon.bmp",ImageFormat.Bmp);
            return map;
            //return bitmap.Clone(new Rectangle(0, y, bitmap.Width, bitmap.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //return bitmap;
        }

        public Scope GetScope( Bitmap bitmap,EncoderOptions options)
        {
            int row = options.Matrix.Width;
            int mudleCount = options.QrRect.ModuleSize;

            return new Scope { Row_Min=(mudleCount * row) - (mudleCount * 8), Row_Max = bitmap.Width, Column_Min = 0, Column_Max = mudleCount * 8 };
            //get { options. }
        }


        public Size GetRandomSize(EncoderOptions options)
        {
            double eee = Math.Round(((1 / 25.4 * options.widthdoubleFromat) * options.DpiInch), 1);
            int width = (int)Math.Round(eee,0); 
            int Height = options.QrRect.ModuleSize*2;
            return new Size(width, Height);
        }

    }
}
