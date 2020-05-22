using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeEncoding.DrawQrCode.Config
{
    public class GrayBitmapData
    {
        public byte[,] Data;//保存像素矩阵
        public int Width;//图像的宽度
        public int Height;//图像的高度

        public GrayBitmapData()
        {
            this.Width = 0;
            this.Height = 0;
            this.Data = null;
        }

        public GrayBitmapData(Bitmap bmp)
        {
            BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            this.Width = bmpData.Width;
            this.Height = bmpData.Height;
            Data = new byte[Height, Width];
            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0.ToPointer();
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        //将24位的RGB彩色图转换为灰度图
                        int temp = (int)(0.114 * (*ptr++)) + (int)(0.587 * (*ptr++)) + (int)(0.299 * (*ptr++));
                        Data[i, j] = (byte)temp;
                    }
                    ptr += bmpData.Stride - Width * 3;//指针加上填充的空白空间
                }
            }
            bmp.UnlockBits(bmpData);
        }

        public GrayBitmapData(string path)
            : this(new Bitmap(path))
        {
        }

        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0.ToPointer();
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        *(ptr++) = Data[i, j];
                        *(ptr++) = Data[i, j];
                        *(ptr++) = Data[i, j];
                    }
                    ptr += bmpData.Stride - Width * 3;
                }
            }
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        //public void ShowImage(PictureBox pbx)
        //{
        //    Bitmap b = this.ToBitmap();
        //    pbx.Image = b;
        //    //b.Dispose();
        //}

        public void SaveImage(string path)
        {
            Bitmap b = ToBitmap();
            b.Save(path);
            //b.Dispose();
        }
        //均值滤波
        public void AverageFilter(int windowSize)
        {
            if (windowSize % 2 == 0)
            {
                return;
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int sum = 0;
                    for (int g = -(windowSize - 1) / 2; g <= (windowSize - 1) / 2; g++)
                    {
                        for (int k = -(windowSize - 1) / 2; k <= (windowSize - 1) / 2; k++)
                        {
                            int a = i + g, b = j + k;
                            if (a < 0) a = 0;
                            if (a > Height - 1) a = Height - 1;
                            if (b < 0) b = 0;
                            if (b > Width - 1) b = Width - 1;
                            sum += Data[a, b];
                        }
                    }
                    Data[i, j] = (byte)(sum / (windowSize * windowSize));
                }
            }
        }
        //中值滤波
        public void MidFilter(int windowSize)
        {
            if (windowSize % 2 == 0)
            {
                return;
            }

            int[] temp = new int[windowSize * windowSize];
            byte[,] newdata = new byte[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int n = 0;
                    for (int g = -(windowSize - 1) / 2; g <= (windowSize - 1) / 2; g++)
                    {
                        for (int k = -(windowSize - 1) / 2; k <= (windowSize - 1) / 2; k++)
                        {
                            int a = i + g, b = j + k;
                            if (a < 0) a = 0;
                            if (a > Height - 1) a = Height - 1;
                            if (b < 0) b = 0;
                            if (b > Width - 1) b = Width - 1;
                            temp[n++] = Data[a, b];
                        }
                    }
                    newdata[i, j] = GetMidValue(temp, windowSize * windowSize);
                }
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Data[i, j] = newdata[i, j];
                }
            }
        }
        //获得一个向量的中值
        private byte GetMidValue(int[] t, int length)
        {
            int temp = 0;
            for (int i = 0; i < length - 2; i++)
            {
                for (int j = i + 1; j < length - 1; j++)
                {
                    if (t[i] > t[j])
                    {
                        temp = t[i];
                        t[i] = t[j];
                        t[j] = temp;
                    }
                }
            }

            return (byte)t[(length - 1) / 2];
        }
        //一种新的滤波方法，是亮的更亮、暗的更暗
        public void NewFilter(int windowSize)
        {
            if (windowSize % 2 == 0)
            {
                return;
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int sum = 0;
                    for (int g = -(windowSize - 1) / 2; g <= (windowSize - 1) / 2; g++)
                    {
                        for (int k = -(windowSize - 1) / 2; k <= (windowSize - 1) / 2; k++)
                        {
                            int a = i + g, b = j + k;
                            if (a < 0) a = 0;
                            if (a > Height - 1) a = Height - 1;
                            if (b < 0) b = 0;
                            if (b > Width - 1) b = Width - 1;
                            sum += Data[a, b];
                        }
                    }
                    double avg = (sum + 0.0) / (windowSize * windowSize);
                    if (avg / 255 < 0.5)
                    {
                        Data[i, j] = (byte)(2 * avg / 255 * Data[i, j]);
                    }
                    else
                    {
                        Data[i, j] = (byte)((1 - 2 * (1 - avg / 255.0) * (1 - Data[i, j] / 255.0)) * 255);
                    }
                }
            }
        }
        //直方图均衡
        public void HistEqual()
        {
            double[] num = new double[256];
            for (int i = 0; i < 256; i++) num[i] = 0;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    num[Data[i, j]]++;
                }
            }

            double[] newGray = new double[256];
            double n = 0;
            for (int i = 0; i < 256; i++)
            {
                n += num[i];
                newGray[i] = n * 255 / (Height * Width);
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Data[i, j] = (byte)newGray[Data[i, j]];
                }
            }
        }

    }
}
