using  QrCodeEncoding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using QrCodeTools.Model;

namespace QrCodeTools
{
    class BitMatTag
    {
        #region BitMat


        private List<BitMat> Arraytagtopx { set; get; } = new List<BitMat>(7);

        private List<BitMat> Arraytagtopxend { set; get; } = new List<BitMat>(7);

        private List<BitMat> Arraytagtopy { set; get; } = new List<BitMat>(7);

        private List<BitMat> ArrarContent { set; get; } = new List<BitMat>(1000);

        private BitMatrix bitMatrix { set; get; }
        #endregion


        private Graphics QrCodeGraphics { set; get; }
        private Graphics LogImgGraphics { set; get; }
        private Bitmap Bitmap { set; get; }

        /// <summary>
        /// 二维码最终的实际尺寸
        /// </summary>
        private int PixelWidth { set; get; }

        public int ModuleSize { set; get; } = 50;

        private int QuietZoneModuleSize { get { return (ModuleSize * (int)QrCodeOptions.QuietZoneModule); } }

        private QrCodeOptions QrCodeOptions { set; get; } = new QrCodeOptions();
        public BitMatTag() { }

        public BitMatTag(BitMatrix bit, QrCodeOptions drawing)
        {

            bitMatrix = bit;
            QrCodeOptions = drawing;

            SetBitmap();
        }

        private void SetBitmap()
        {
            int bitmapsize = 0;
            if (bitMatrix == null) return;
            if (QrCodeOptions.IsTypeSize)
            {
                PixelWidth = QrCodeOptions.QrCodePixelWidth;
            }
            else
            {
                //换算成实际像素单位
                PixelWidth = (int)((1 / 25.4 * QrCodeOptions.QrCodeWidthMM) * QrCodeOptions.DpiInch);
               
            }
            if (QrCodeOptions.IsQrCodeAutoSize)
            {
                bitmapsize = bitMatrix.Width * ModuleSize + ModuleSize * ((int)QrCodeOptions.QuietZoneModule) * 2;
                PixelWidth = bitmapsize;
            }
            else
            {
                ModuleSize = (int)Math.Round(PixelWidth / (bitMatrix.Width + (double)QrCodeOptions.QuietZoneModule * 2), 0);
                bitmapsize = bitMatrix.Width * ModuleSize + ModuleSize * ((int)QrCodeOptions.QuietZoneModule) * 2;

            }
            //ModuleSize = (int)Math.Round(PixelWidth / (bitMatrix.Width + (double)QrCodeOptions.QuietZoneModule * 2), 0);
            //int bitmapsize = bitMatrix.Width * ModuleSize + ModuleSize * ((int)QrCodeOptions.QuietZoneModule) * 2;

            this.Bitmap = new Bitmap(bitmapsize, bitmapsize);
            this.Bitmap.SetResolution(QrCodeOptions.DpiInch, QrCodeOptions.DpiInch);

            QrCodeGraphics = Graphics.FromImage(this.Bitmap);
            QrCodeGraphics.Clear(System.Drawing.Color.White);

            AddBitmapLog(QrCodeOptions.GetLogBitmap());

            GetBitMattag();

           
            //QrCodeGraphics.Dispose();
          
        }

        private void GetBitMattag()
        {
            if (Arraytagtopx != null) Arraytagtopx.Clear();
            if (Arraytagtopxend != null) Arraytagtopxend.Clear();
            if (Arraytagtopy != null) Arraytagtopy.Clear();

            int Width = bitMatrix.Width;
            int hx = bitMatrix.Width - 7;
            //int wx = 
            for (int column = 0; column < Width; column++)
            {
                for (int row = 0; row < Width; row++)
                {
                    if (column < 7 && row < 7)
                    {
                        Arraytagtopx.Add(new BitMat()
                        {
                            Point = new System.Drawing.Point(row, column),
                            BoolVar = bitMatrix[row, column]
                        });
                    }
                    if (row >= hx && column < 7)
                    {
                        Arraytagtopxend.Add(new BitMat()
                        {
                            Point = new System.Drawing.Point(row, column),
                            BoolVar = bitMatrix[row, column]
                        });
                    }

                    if (column >= hx && row < 7)
                    {
                        Arraytagtopy.Add(new BitMat()
                        {
                            Point = new System.Drawing.Point(row, column),
                            BoolVar = bitMatrix[row, column]
                        });
                    }

                    if ((row >= 7 && row < hx) && column < 7)
                    {
                        ArrarContent.Add(new BitMat()
                        {
                            Point = new System.Drawing.Point(row, column),
                            BoolVar = bitMatrix[row, column]
                        });
                    }
                    if (column >= 7 && column < hx)
                    {
                        ArrarContent.Add(new BitMat()
                        {
                            Point = new System.Drawing.Point(row, column),
                            BoolVar = bitMatrix[row, column]
                        });
                    }
                    if (row >= 7 && column >= hx)
                    {
                        ArrarContent.Add(new BitMat()
                        {
                            Point = new System.Drawing.Point(row, column),
                            BoolVar = bitMatrix[row, column]
                        });
                    }
                }
            }
        }


        private void DrawingModule()
        {
            for (int i = 0; i < Arraytagtopxend.Count; i++)
            {
                if (Arraytagtopxend[i].BoolVar)
                {
                    Point point = new Point()
                    {
                        X = Arraytagtopx[i].Point.X * ModuleSize + QuietZoneModuleSize,
                        Y = Arraytagtopx[i].Point.Y * ModuleSize + QuietZoneModuleSize
                    };
                    Point pointd = new Point()
                    {
                        X = Arraytagtopxend[i].Point.X * ModuleSize + QuietZoneModuleSize,
                        Y = Arraytagtopxend[i].Point.Y * ModuleSize + QuietZoneModuleSize
                    };
                    Point pointy = new Point()
                    {
                        X = Arraytagtopy[i].Point.X * ModuleSize + QuietZoneModuleSize,
                        Y = Arraytagtopy[i].Point.Y * ModuleSize + QuietZoneModuleSize
                    };

                    QrCodeGraphics.FillRectangle(Brushes.Black, new Rectangle(point.X, point.Y, ModuleSize, ModuleSize));

                    QrCodeGraphics.FillRectangle(Brushes.Black, new Rectangle(pointd.X, pointd.Y, ModuleSize, ModuleSize));

                    QrCodeGraphics.FillRectangle(Brushes.Black, new Rectangle(pointy.X, pointy.Y, ModuleSize, ModuleSize));
                }

            }
            QrCodeGraphics.Flush();


            //   Bitmap.Save(@"D:\Program Files\桌面\12112\DrawingModule.jpg");
            // Bitmap.Save(@"C:\Users\q4528\Desktop\1111111\DrawingModule.jpg");
            // QrCodeGraphics.Dispose();
        }


        public Bitmap DrawingBitmap()
        {
            DrawingModule();
            //  Graphics graphics = Graphics.FromImage(Bitmap);

            int R = ModuleSize / 4;
            for (int i = 0; i < ArrarContent.Count; i++)
            {
                // new Pen(Brushes.Red, 1)
                if (ArrarContent[i].BoolVar)
                {
                    Point point = new Point();
                    point.X = ArrarContent[i].Point.X * ModuleSize + R + QuietZoneModuleSize;
                    point.Y = ArrarContent[i].Point.Y * ModuleSize + R + QuietZoneModuleSize;

                    QrCodeGraphics.FillEllipse(Brushes.Black, new Rectangle(point.X, point.Y, ModuleSize / 2, ModuleSize / 2));

                    //QrCodeGraphics.FillRectangle(Brushes.Black, new Rectangle(point.X, point.Y, ModuleSize / 2, ModuleSize / 2));
                }
                else
                {
                    Point point = new Point()
                    {
                        X = ArrarContent[i].Point.X * ModuleSize + R + QuietZoneModuleSize,
                        Y = ArrarContent[i].Point.Y * ModuleSize + R + QuietZoneModuleSize
                    };
                    int b = Bitmap.GetPixel(point.X, point.Y).B;
                    int g = Bitmap.GetPixel(point.X, point.Y).G;
                    int r = Bitmap.GetPixel(point.X, point.Y).R;
                    int rgb = (b + g + r) / 3;
                    if (rgb < 128)
                    {
                        QrCodeGraphics.FillEllipse(Brushes.White, new Rectangle(point.X, point.Y, ModuleSize / 2, ModuleSize / 2));

                        // QrCodeGraphics.FillRectangle(Brushes.White, new Rectangle(point.X, point.Y, ModuleSize / 2, ModuleSize / 2));
                    }
                }
            }
            QrCodeGraphics.Flush();
            QrCodeGraphics.Dispose();

         
            return GetPixelWidth();
        }

        private Bitmap GetPixelWidth()
        {
            if (QrCodeOptions.IsQrCodeAutoSize)
            {
                return Bitmap;
            }

            Bitmap bitmap = new Bitmap(PixelWidth, PixelWidth);
            bitmap.SetResolution(QrCodeOptions.DpiInch, QrCodeOptions.DpiInch);

            bitmap.MakeTransparent(Color.White);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.DrawImage(Bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), GraphicsUnit.Pixel);
            graphics.Flush();
            graphics.Dispose();
            // bitmap.Save(@"D:\Program Files\桌面\12112\GetPixelWidth.png");
            return bitmap;

        }


        private Size CalculateSuitableWidth(Size logimgsize)
        {
            double gap = bitMatrix.Width * ModuleSize - ModuleSize * 14;
            double nScalemax = Math.Max(logimgsize.Width, logimgsize.Height);
            double nScalemin = Math.Min(logimgsize.Width, logimgsize.Height);
            Size size = new Size();
            if (nScalemax > gap)
            {
                int differmax = (int)(nScalemax - gap);

                if (logimgsize.Width > logimgsize.Height)
                {
                    int nScaleW = (logimgsize.Width - differmax);
                    int nScaleH = (int)Math.Round((gap / nScalemax) * nScalemin, 0);
                    size.Width = nScaleW > 0 ? nScaleW : (int)gap;
                    size.Height = nScaleH > 0 ? nScaleH : (int)gap;
                }
                else if (logimgsize.Width < logimgsize.Height)
                {
                    int nScaleW = (int)Math.Round((gap / nScalemax) * nScalemin, 0);
                    int nScaleH = (int)(logimgsize.Height - differmax);
                    size.Width = nScaleW > 0 ? nScaleW : (int)gap;
                    size.Height = nScaleH > 0 ? nScaleH : (int)gap;
                }
            }
            else
            {
                size.Width = logimgsize.Width;
                size.Height = logimgsize.Height;
            }
            return size;
        }

        public void AddBitmapLog(Bitmap logimg)
        {
            if (logimg == null) return;
            Size logsize = CalculateSuitableWidth(logimg.Size);

            LogImgGraphics = Graphics.FromImage(Bitmap);
            LogImgGraphics.Clear(Color.White);

            int pointx = (int)(Bitmap.Width / 2 - logsize.Width / 2);
            int pointy = (int)(Bitmap.Height / 2 - logsize.Height / 2);
            LogImgGraphics.DrawImage(logimg, pointx, pointy, logsize.Width, logsize.Height);

            LogImgGraphics.Flush();

            LogImgGraphics.Dispose();
        }
    }
}
