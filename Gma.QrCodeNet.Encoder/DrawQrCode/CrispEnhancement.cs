using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace QrCodeEncoding.DrawQrCode
{
    public class CrispEnhancement
    {
        public CrispEnhancement() { }

        public Bitmap Crisp(string file)
        {
            //以锐化效果显示图像
            try
            {
                Bitmap oldBitmap = new Bitmap(@"D:\Desktop\微孔-识别\RT5911A9OMZ3000973.jpg");
                int Height = oldBitmap.Height;
                int Width = oldBitmap.Width;
                Bitmap newBitmap = new Bitmap(Width, Height);
               // Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel;
                //拉普拉斯模板
                int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
                for (int x = 1; x < Width - 1; x++)
                    for (int y = 1; y < Height - 1; y++)
                    {
                        int r = 0, g = 0, b = 0;
                        int Index = 0;
                        for (int col = -1; col <= 1; col++)
                            for (int row = -1; row <= 1; row++)
                            {
                                pixel = oldBitmap.GetPixel(x + row, y + col); r += pixel.R * Laplacian[Index];
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
                        newBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                    }
                return newBitmap;
              //  this.pictureBox1.Image = newBitmap;
            }
            catch (Exception ex)
            {
                return null;
                //  MessageBox.Show(ex.Message, "信息提示");
            }
        }
    }
}
