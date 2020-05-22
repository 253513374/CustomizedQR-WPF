using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using QrCodeEncoding.DrawQrCode.Config;
using ZXing;

namespace QrCodeEncoding.DrawQrCode
{
    public class DrawDrakCustomSquare
    {
        private Graphics GraphicsCustom { set; get; }

        private Bitmap OldBitmap{set;get;}

        private Bitmap Bitmap { set; get; }

        private List<Rectangle> Rectangles { set; get; } = new List<Rectangle>();
        private int MudelSize { set; get; }

        public Bitmap DrawQrCode(EncoderOptions options)
        {
            SetImg(options.LogoImgPath);
            var QrCodeMinWidth = options.Matrix.Width * MudelSize/7;
            Bitmap = new Bitmap(QrCodeMinWidth, QrCodeMinWidth, PixelFormat.Format24bppRgb);
            GraphicsCustom = Graphics.FromImage(Bitmap);
            GraphicsCustom.Clear(Color.White);
            GraphicsCustom.Flush();

            // graphics.Dispose();

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
            GraphicsCustom.Flush();
            GraphicsCustom.Dispose();
            return Bitmap;
        }

        private void DrawingContent(QrCodeCustom qrCodeCustom, EncoderOptions options)
        {
            List<QrBitmapMatrix> matrixs = options.BitMats.FindAll(f => f.Tag == qrCodeCustom.QrCodeTag);

            int offsetmudule = 1;
            int ModuleSize = MudelSize / 7;

            for (int i = 0; i < matrixs.Count; i++)
            {

                int rowX = matrixs[i].Point.X * ModuleSize;
                int columnY = matrixs[i].Point.Y * ModuleSize;

                if (matrixs[i].BoolVar)
                {
                    GraphicsCustom.FillRectangle(Brushes.Black, new Rectangle(rowX, columnY, ModuleSize, ModuleSize));
                   // GraphicsCustom.FillEllipse(new SolidBrush(qrCodeCustom.ColorBrush), new Rectangle(rowX, columnY, offset, offset));
                    GraphicsCustom.Flush();
                }
                //else
                //{
                //    GraphicsCustom.FillEllipse(Brushes.White, new Rectangle(rowX, columnY, offset, offset));
                //    GraphicsCustom.Flush();
                //    int b = Bitmap.GetPixel(rowX, columnY).B;
                //    int g = Bitmap.GetPixel(rowX, columnY).G;
                //    int r = Bitmap.GetPixel(rowX, columnY).R;
                //    int rgb = (b + g + r) / 3;
                //    if (rgb < 128)
                //    {
                //        GraphicsCustom.FillEllipse(Brushes.White, new Rectangle(rowX, columnY, offset, offset));
                //        GraphicsCustom.Flush();
                //    }
                //}
            }
           
        }

        private void DrawTag(QrCodeCustom qrCodeCustom, EncoderOptions options)
        {
            List<QrBitmapMatrix> matrixs = options.BitMats.FindAll(f => f.Tag == qrCodeCustom.QrCodeTag);// qrCodeCustom.QrCodeTag

            int ModuleSize = MudelSize / 7;

            int rowX = matrixs[0].Point.X * ModuleSize;
            int columnY = matrixs[0].Point.Y * ModuleSize; 
            Bitmap bitmap = GetTagImg(options.LogoImgPath, qrCodeCustom.QrCodeTag);
            bitmap = OtsuThreshold(bitmap).ToBitmap();
           //bitmap.Save(@"D:\Desktop\DIYcode\bitmap212313123123.jpg");

            GraphicsCustom.DrawImage(bitmap, rowX, columnY, Rectangles[0].Width, Rectangles[0].Height);
           
            // throw new NotImplementedException();
        }

        private Bitmap GetTagImg(string logopath, Tag tag)
        {
            var rectangle = Rectangles[0]; 
            if (tag == Tag.TagOne)
            {
                rectangle = Rectangles[2];
            }
            if (tag == Tag.TagTwo)
            {
                rectangle = Rectangles[1];
            }
            if (tag == Tag.TagThree)
            {
                rectangle = Rectangles[0];
            }

            int ModuleSize = MudelSize / 7;
            int offset = 2;
            Bitmap newbitmap = new Bitmap(rectangle.Width,rectangle.Height);

            Bitmap Laplacianbitmap = new Bitmap(rectangle.Width, rectangle.Height);

            Graphics graphics = Graphics.FromImage(newbitmap);
            graphics.Clear(Color.Black);
           // graphics.Flush();
            Bitmap oldBitmap = new Bitmap(logopath);
            oldBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            //  graphics = Graphics.FromImage(oldBitmap);
            graphics.DrawImage(oldBitmap,new Rectangle() { X=0,Y=0,Width= rectangle.Width,Height=rectangle .Height}, rectangle,GraphicsUnit.Pixel);

            newbitmap.Save(@"D:\Desktop\DIYcode\newbitmap.jpg");

            //new GrayBitmapData(newbitmap).NewFilter(5);
            //newbitmap.Save(@"D:\Desktop\DIYcode\NewFilter.jpg");

            graphics.FillRectangle(Brushes.White, new Rectangle() { X = ModuleSize+2, Y = ModuleSize+2, Width = ModuleSize*5, Height = ModuleSize + offset });

            graphics.FillRectangle(Brushes.White, new Rectangle() { X = ModuleSize*5 + 2, Y = ModuleSize + 2, Width = ModuleSize, Height = ModuleSize*5 });

            graphics.FillRectangle(Brushes.White, new Rectangle() { X = ModuleSize + 2, Y = ModuleSize + 2, Width = ModuleSize, Height = ModuleSize * 5 });

            graphics.FillRectangle(Brushes.White, new Rectangle() { X = ModuleSize + 2, Y = ModuleSize*5 + 2, Width = ModuleSize*5, Height = ModuleSize });


            //Bitmap centreblack = new Bitmap(ModuleSize * 3, ModuleSize * 3);
            //graphics = Graphics.FromImage(centreblack);
            //graphics.DrawImage(newbitmap,
            //                   new Rectangle() { X = 0, Y = 0, Width = centreblack.Width, Height = centreblack.Height },
            //                   new Rectangle() { X = ModuleSize*2- offset, Y = ModuleSize*2- offset, Width = centreblack.Width, Height = centreblack.Height },
            //                   GraphicsUnit.Pixel);
            //graphics.Flush();

            //centreblack.Save(@"D:\Desktop\DIYcode\centreblack.jpg");


            //Bitmap newbitmapcentrewhite = new Bitmap(ModuleSize * 5, ModuleSize * 5);
            //graphics = Graphics.FromImage(newbitmapcentrewhite);
            //graphics.Clear(Color.White);
            //graphics.Flush();
            //graphics = Graphics.FromImage(newbitmap);
            //graphics.DrawImage(newbitmapcentrewhite, ModuleSize + 2, ModuleSize + 2, (ModuleSize * 5) - 2, (ModuleSize * 5) - 2);


            //graphics = Graphics.FromImage(newbitmap);

            //graphics.DrawImage(centreblack, ModuleSize*2, ModuleSize*2, centreblack.Width, centreblack.Height);
            //Bitmap newbitmapcentreblack = new Bitmap(ModuleSize*3, ModuleSize*3);

            Color pixel;
            //拉普拉斯模板
            int Height = newbitmap.Height;
            int Width = newbitmap.Width;
            int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            for (int x = 1; x < Width - 1; x++)
                for (int y = 1; y < Height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int Index = 0;
                    for (int col = -1; col <= 1; col++)
                        for (int row = -1; row <= 1; row++)
                        {
                            pixel = newbitmap.GetPixel(x + row, y + col); r += pixel.R * Laplacian[Index];
                            g += pixel.G * Laplacian[Index];
                            b += pixel.B * Laplacian[Index];
                            Index++;
                        }
                    //处理颜色值溢出
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    Laplacianbitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                }
            Laplacianbitmap.Save(@"D:\Desktop\DIYcode\Laplacianbitmap.jpg");
            return Laplacianbitmap;

        }
        private void SetImg( string path)
        {
            try
            {
                Bitmap oldBitmap = new Bitmap(path);
                oldBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);

                Image<Gray, Byte> matimg = OtsuThreshold(oldBitmap);
                matimg = matimg.SmoothMedian(11);
                Image<Gray, Byte> edges = new Image<Gray, byte>(oldBitmap.Width, oldBitmap.Height);

                Image<Bgr, Byte> contoursimg = new Image<Bgr, byte>(oldBitmap);

                Mat hierarchy = new Mat();

                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                //VectorOfVectorOfPointF VectorOfPointF = new VectorOfVectorOfPointF();

                CvInvoke.Canny(matimg, edges, 0, 200);

                CvInvoke.FindContours(edges,
                                      contours,
                                      hierarchy,
                                      RetrType.Tree,
                                      ChainApproxMethod.ChainApproxSimple);
                List<int> fater_contours = new List<int>();

                int[,,] arraylist = ((int[,,])(hierarchy.GetData()));

                for (int k = 0; k < contours.Size; k++)
                {
                    int i = k;
                    int c = 0;
                    while (arraylist[0, i, 2] != -1)
                    {
                        i = arraylist[0, i, 2];
                        c = c + 1;
                        if (c > 4)
                        {
                            fater_contours.Add(k);
                        }
                    }
                }
                for (int j = 0; j < fater_contours.Count; j++)
                {
                    RotatedRect rect = CvInvoke.MinAreaRect(contours[fater_contours[j]]); //minAreaRect
                    MudelSize=rect.Size.Width > rect.Size.Height ?  (int)rect.Size.Height :  (int)rect.Size.Width;

                    PointF[] pf = CvInvoke.BoxPoints(rect);//BoxPoints();

                    Point[] point = new Point[pf.Length];

                    int[] pointx = new int[4];
                    int[] pointy = new int[4];
                    for (int i=0;i<pf.Length;i++){
                        pointx[i]=(int)pf[i].X;
                        pointy[i] = (int)pf[i].Y;
                    }
                    Rectangles.Add(new Rectangle() {X= pointx.Min(),Y=pointy.Min(),Width =  MudelSize,Height = MudelSize });
                  
                    contoursimg.Draw(rect, new Bgr(255, 0, 0), 2);
                    // CvInvoke.DrawContours(new Image<Bgr, Byte>(newBitmap), VectorOfPointF, -1, new MCvScalar(0, 255, 0), 1, LineType.AntiAlias);
                }


            }
            catch (Exception ex)
            {
                //  return null;
                //  MessageBox.Show(ex.Message, "信息提示");
            }

        }
     
        /// <summary>
        /// Otsu 全局计算最优阈值
        /// </summary>
        /// <param name="b">位图流</param>
        /// <returns></returns>
        public Image<Gray, byte> OtsuThreshold(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            byte threshold = 0;
            int[] hist = new int[256];
            int AllPixelNumber = 0, PixelNumberSmall = 0, PixelNumberBig = 0;
            double MaxValue, AllSum = 0, SumSmall = 0, SumBig, ProbabilitySmall, ProbabilityBig, Probability;
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* p = (byte*)data.Scan0;
                int offset = data.Stride - width * 4;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        hist[p[0]]++;
                        p += 4;
                    }
                    p += offset;
                }
                bitmap.UnlockBits(data);
            }

            //计算灰度为I的像素出现的概率

            for (int i = 0; i < 256; i++)
            {
                AllSum += i * hist[i];     //   质量矩
                AllPixelNumber += hist[i];  //  质量
            }
            MaxValue = -1.0;
            for (int i = 0; i < 256; i++)
            {
                PixelNumberSmall += hist[i];
                PixelNumberBig = AllPixelNumber - PixelNumberSmall;
                if (PixelNumberBig == 0)
                {
                    break;
                }
                SumSmall += i * hist[i];
                SumBig = AllSum - SumSmall;
                ProbabilitySmall = SumSmall / PixelNumberSmall;
                ProbabilityBig = SumBig / PixelNumberBig;

                Probability = PixelNumberSmall * ProbabilitySmall * ProbabilitySmall + PixelNumberBig * ProbabilityBig * ProbabilityBig;

                if (Probability > MaxValue)
                {
                    MaxValue = Probability;
                    threshold = (byte)i;
                }
            }
            return this.BitmapThreshoding(bitmap, threshold);
            // return threshold;
        }

        /// <summary>
        /// 根据阈值二值化图像
        /// </summary>
        /// <param name="b"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public Image<Gray, byte> BitmapThreshoding(Bitmap bitmap, int threshold)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* p = (byte*)data.Scan0;
                int offset = data.Stride - width * 4;
                byte R, G, B, gray;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        R = p[2];
                        G = p[1];
                        B = p[0];
                        gray = (byte)((R * 19595 + G * 38469 + B * 7472) >> 16);

                        if (gray >= threshold)
                        {
                            p[0] = p[1] = p[2] = 255;
                        }
                        else
                        {
                            p[0] = p[1] = p[2] = 0;
                        }
                        p += 4;
                    }
                    p += offset;
                }
                bitmap.UnlockBits(data);
                return new Image<Gray, byte>(bitmap);
            }
        }

    }
}
