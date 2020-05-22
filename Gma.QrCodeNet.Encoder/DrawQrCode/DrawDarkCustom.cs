using QrCodeEncoding.DrawQrCode.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeEncoding.DrawQrCode
{
    public class DrawDarkCustom
    {
        private Graphics GraphicsCustom { set; get; }
        private Bitmap Bitmap { set; get; }
        public Bitmap DrawQrCode(EncoderOptions options)
        {
            Bitmap logoimg = GetLogoBitmap(options);
            Bitmap = new Bitmap(options.QrRect.QrCodeMinWidth, options.QrRect.QrCodeMinWidth, PixelFormat.Format24bppRgb);
            GraphicsCustom = Graphics.FromImage(Bitmap);
            GraphicsCustom.Clear(Color.White);
          
            if (options.IsTopLogoImg)
            {
               // Bitmap = new Bitmap(logoimg.Width, logoimg.Height, PixelFormat.Format24bppRgb);
                if (logoimg != null)
                {
                    GraphicsCustom.DrawImage(logoimg, 0, 0, logoimg.Width, logoimg.Height);
                }
            }
            else
            {
              
                if (logoimg != null)
                {
                    int pointx = (int)(Bitmap.Width / 2 - logoimg.Width / 2);
                    int pointy = (int)(Bitmap.Height / 2 - logoimg.Height / 2);

                    GraphicsCustom.DrawImage(logoimg, pointx, pointy, logoimg.Width, logoimg.Height);
                }
            }
            GraphicsCustom.Flush();
            for (int i = 0; i < options.QrCodeCustoms.Count; i++)
            {
                QrCodeCustom qrCodeCustom = options.QrCodeCustoms[i];
                if (qrCodeCustom.QrCodeTag != Tag.Content)
                {
                    DrawTag(qrCodeCustom, options);
                }
                else
                {
                    DrawingContent(qrCodeCustom, options);
                }
            }
            return Bitmap;
        }

        private void DrawTag(QrCodeCustom custom, EncoderOptions options)
        {
            List<QrBitmapMatrix> matrices = options.BitMats.FindAll(f=>f.Tag== custom.QrCodeTag);

            //int ModuleSize = options.QrRect.ModuleSize;
            //int QuietZoneModuleSize = (int)options.QrRect.ZoneModule * ModuleSize;
            switch (custom.QrCodeStyle)
            {
                case QrCodeStyleEnum.Square:
                    DrawTagSquare(custom, matrices);
                    break;

                case QrCodeStyleEnum.Circle:
                    DrawTagCircle(custom, matrices);
                    break;
              
                case QrCodeStyleEnum.Excircle:
                    DrawTagExcircle(custom, matrices);

                    break;
                case QrCodeStyleEnum.InnerCircle:
                    DrawTagInnerCircle(custom, matrices);
                    break;
                case QrCodeStyleEnum.ExInnerCircle:
                    DrawTagExInnercircle(custom, matrices);
                    break;

                case QrCodeStyleEnum.ExSquareCircle:
                    DrawTagExSquareCircle(custom, matrices);
                    break;

                //case QrCodeStyleEnum.CustomSquare:
                //    DrawTagCustomSquare(custom, matrices);
                //    break;
                    
                default:
                    break;
            }
        }

        private void DrawTagCustomSquare(QrCodeCustom custom, List<QrBitmapMatrix> matrices)
        {
            throw new NotImplementedException();
        }

        private void DrawTagSquare(QrCodeCustom custom, List<QrBitmapMatrix> qrBitmapMatrices)
        {
            if (qrBitmapMatrices.Count > 0)
            {
                for (int i = 0; i < qrBitmapMatrices.Count; i++)
                {
                    Rectangle rect = qrBitmapMatrices[i].Rectangle;
                    if (qrBitmapMatrices[i].BoolVar)
                    {
                        GraphicsCustom.FillRectangle(new SolidBrush(custom.ColorBrush), rect);
                    }
                    else
                    {
                        GraphicsCustom.FillRectangle(Brushes.White, rect);
                    }
                }
                GraphicsCustom.Flush();
            }
        }

      

        private void DrawTagCircle(QrCodeCustom custom, List<QrBitmapMatrix>  qrBitmaps)
        {

            List<QrBitmapMatrix> matrices = qrBitmaps.FindAll(f => f.Tag == custom.QrCodeTag);
            if (matrices.Count > 0)
            {
                Rectangle rect = matrices[0].Rectangle;

                Rectangle Ellipserect = new Rectangle()
                {
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width * 7,
                    Height = rect.Height * 7
                };
                Rectangle Ellipserect2 = new Rectangle()
                {
                    X = rect.X + rect.Width,
                    Y = rect.Y + rect.Width,
                    Width = rect.Width * 5,
                    Height = rect.Height * 5
                };
                Rectangle Ellipserect3 = new Rectangle()
                {
                    X = rect.X + rect.Width * 2,
                    Y = rect.Y + rect.Width * 2,
                    Width = rect.Width * 3,
                    Height = rect.Height * 3
                };
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        GraphicsCustom.FillEllipse(new SolidBrush(custom.ColorBrush), Ellipserect);
                    }
                    if (i == 1)
                    {
                        GraphicsCustom.FillEllipse(Brushes.White, Ellipserect2);
                    }
                    if (i == 2)
                        GraphicsCustom.FillEllipse(new SolidBrush(custom.ColorBrush), Ellipserect3);
                    }
                }

            GraphicsCustom.Flush();
               // graphics.Dispose();
            
        }

        private void DrawTagExcircle(QrCodeCustom custom, List<QrBitmapMatrix> qrBitmaps)
        {
            List<QrBitmapMatrix> matrices = qrBitmaps.FindAll(f => f.Tag == custom.QrCodeTag);
            if (matrices.Count > 0)
            {
                Rectangle rect = matrices[0].Rectangle;

                Rectangle Ellipserect = new Rectangle()
                {
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width * 7,
                    Height = rect.Height * 7
                };
                Rectangle Ellipserect2 = new Rectangle()
                {
                    X = rect.X + rect.Width,
                    Y = rect.Y + rect.Width,
                    Width = rect.Width * 5,
                    Height = rect.Height * 5
                };
                Rectangle Ellipserect3 = new Rectangle()
                {
                    X = rect.X + rect.Width * 2,
                    Y = rect.Y + rect.Width * 2,
                    Width = rect.Width * 3,
                    Height = rect.Height * 3
                };
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        GraphicsCustom.FillEllipse(new SolidBrush(custom.ColorBrush), Ellipserect);
                    }
                    if (i == 1)
                    {
                        GraphicsCustom.FillEllipse(Brushes.White, Ellipserect2);
                    }
                    if (i == 2)
                        GraphicsCustom.FillRectangle(new SolidBrush(custom.ColorBrush), Ellipserect3);
                }
            }

            GraphicsCustom.Flush();
        }

        private void DrawTagInnerCircle(QrCodeCustom custom, List<QrBitmapMatrix> qrBitmaps)
        {
            List<QrBitmapMatrix> matrices = qrBitmaps.FindAll(f => f.Tag == custom.QrCodeTag);
            if (matrices.Count > 0)
            {
                Rectangle rect = matrices[0].Rectangle;

                Rectangle Ellipserect = new Rectangle()
                {
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width * 7,
                    Height = rect.Height * 7
                };
                Rectangle Ellipserect2 = new Rectangle()
                {
                    X = rect.X + rect.Width,
                    Y = rect.Y + rect.Width,
                    Width = rect.Width * 5,
                    Height = rect.Height * 5
                };
                Rectangle Ellipserect3 = new Rectangle()
                {
                    X = rect.X + rect.Width * 2,
                    Y = rect.Y + rect.Width * 2,
                    Width = rect.Width * 3,
                    Height = rect.Height * 3
                };
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        GraphicsCustom.FillRectangle(new SolidBrush(custom.ColorBrush), Ellipserect);
                    }
                    if (i == 1)
                    {
                        GraphicsCustom.FillRectangle(Brushes.White, Ellipserect2);
                    }
                    if (i == 2)
                        GraphicsCustom.FillEllipse(new SolidBrush(custom.ColorBrush), Ellipserect3);
                }
            }

            GraphicsCustom.Flush();
        }


        private void DrawTagExInnercircle(QrCodeCustom custom, List<QrBitmapMatrix> qrBitmaps)
        {
            List<QrBitmapMatrix> matrices = qrBitmaps.FindAll(f => f.Tag == custom.QrCodeTag);
            if (matrices.Count > 0)
            {
                Rectangle rect = matrices[0].Rectangle;

                Rectangle Ellipserect = new Rectangle()
                {
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width * 7,
                    Height = rect.Height * 7
                };
                Rectangle Ellipserect2 = new Rectangle()
                {
                    X = rect.X + rect.Width,
                    Y = rect.Y + rect.Width,
                    Width = rect.Width * 5,
                    Height = rect.Height * 5
                };
                Rectangle Ellipserect3 = new Rectangle()
                {
                    X = rect.X + rect.Width * 2,
                    Y = rect.Y + rect.Width * 2,
                    Width = rect.Width * 3,
                    Height = rect.Height * 3
                };
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        GraphicsCustom.FillRectangle(new SolidBrush(custom.ColorBrush), Ellipserect);
                    }
                    if (i == 1)
                    {
                        GraphicsCustom.FillEllipse(Brushes.White, Ellipserect2);
                    }
                    if (i == 2)
                        GraphicsCustom.FillEllipse(new SolidBrush(custom.ColorBrush), Ellipserect3);
                }
            }

            GraphicsCustom.Flush();
        }

        private void DrawTagExSquareCircle(QrCodeCustom custom, List<QrBitmapMatrix> qrBitmaps)
        {
            List<QrBitmapMatrix> matrices = qrBitmaps.FindAll(f => f.Tag == custom.QrCodeTag);
            if (matrices.Count > 0)
            {
                Rectangle rect = matrices[0].Rectangle;

                Rectangle Ellipserect = new Rectangle()
                {
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width * 7,
                    Height = rect.Height * 7
                };
                Rectangle Ellipserect2 = new Rectangle()
                {
                    X = rect.X + rect.Width,
                    Y = rect.Y + rect.Width,
                    Width = rect.Width * 5,
                    Height = rect.Height * 5
                };
                Rectangle Ellipserect3 = new Rectangle()
                {
                    X = rect.X + rect.Width * 2,
                    Y = rect.Y + rect.Width * 2,
                    Width = rect.Width * 3,
                    Height = rect.Height * 3
                };
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        GraphicsCustom.FillEllipse(new SolidBrush(custom.ColorBrush), Ellipserect);
                    }
                    if (i == 1)
                    {
                        GraphicsCustom.FillRectangle(Brushes.White, Ellipserect2);
                    }
                    if (i == 2)
                        GraphicsCustom.FillRectangle(new SolidBrush(custom.ColorBrush), Ellipserect3);
                }
            }

            GraphicsCustom.Flush();
        }
        private void Fill(Rectangle rectangle)
        {
           
        }

        private void DrawingContent(QrCodeCustom custom, EncoderOptions options)
        {

            List<QrBitmapMatrix> matrixs = options.BitMats.FindAll(f => f.Tag == custom.QrCodeTag);

            int offset = 1;
            int offsetmudule = 1;
            int ModuleSize=matrixs[0].Rectangle.Width;
            if (custom.QrCodeSize == QrCodeSquareSize.Max)
            {
                offset = ModuleSize / 2;
                offsetmudule = offset / 2;
            }
            if (custom.QrCodeSize == QrCodeSquareSize.Min)
            {
                offset = ModuleSize / 3;
                offsetmudule = offset;
            }
           // Graphics graphics = Graphics.FromImage(bitmap);
            for (int i = 0; i < matrixs.Count; i++)
            {

                int rowX = matrixs[i].Rectangle.X + offsetmudule;
                int columnY = matrixs[i].Rectangle.Y + offsetmudule;

                if (matrixs[i].BoolVar)
                {

                    GraphicsCustom.FillEllipse(new SolidBrush(custom.ColorBrush), new Rectangle(rowX, columnY, offset, offset));
                    GraphicsCustom.Flush();
                }
                else
                {
                    GraphicsCustom.FillEllipse(Brushes.White, new Rectangle(rowX, columnY, offset, offset));
                    GraphicsCustom.Flush();
                    //int b = Bitmap.GetPixel(rowX, columnY).B;
                    //int g = Bitmap.GetPixel(rowX, columnY).G;
                    //int r = Bitmap.GetPixel(rowX, columnY).R;
                    //int rgb = (b + g + r) / 3;
                    //if (rgb < 128)
                    //{
                    //    GraphicsCustom.FillEllipse(Brushes.White, new Rectangle(rowX, columnY, offset, offset));
                    //    GraphicsCustom.Flush();

                    //}
                }
            }
            GraphicsCustom.Flush();
            GraphicsCustom.Dispose();
        }


        private Bitmap  GetLogoBitmap(EncoderOptions options)
        {
            if (options.LogoImgPath == null || options.LogoImgPath == "") return null;
            //获得二维码可以放置的最大logo尺寸
          
             Bitmap Logobitmap = new Bitmap(options.LogoImgPath);
            // Logobitmap.SetResolution(options.DpiInch, options.DpiInch);// = PixelFormat.Format24bppRgb;
            // if (Logobitmap == null) return;
          
            if (!options.IsTopLogoImg)
            {
                double gap = options.Matrix.Width * options.QrRect.ModuleSize - options.QrRect.ModuleSize * 16;
                Size dsclogsize = CalculateSuitableWidth(Logobitmap.Size, gap);
                Bitmap newbitmap = new Bitmap(Logobitmap, dsclogsize);
                Logobitmap.Dispose();
                return newbitmap;
            }
            else
            {
                Size dsclogsize = CalculateSuitableWidth(Logobitmap.Size, options.QrRect.QrCodeMinWidth);
                Bitmap newbitmap = new Bitmap(Logobitmap, dsclogsize);
                Logobitmap.Dispose();
                return newbitmap;

            }
         
           
        }

        private Size CalculateSuitableWidth(Size logimgsize, double gap)
        {
        
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
                else if(logimgsize.Width == logimgsize.Height)
                {
                    size.Width =  (int)gap;
                    size.Height =  (int)gap;
                }
            }
            else
            {
                size.Width = logimgsize.Width;
                size.Height = logimgsize.Height;
            }
            return size;
        }
    }
}
