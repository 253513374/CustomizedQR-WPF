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
    public class DrawDarkSquare
    {
        public Bitmap DrawQrCode(EncoderOptions options)
        {
            Bitmap bitmap = new Bitmap(options.BitmapWidth, options.BitmapWidth,PixelFormat.Format24bppRgb);
            //bitmap.SetResolution(options.DpiInch, options.DpiInch);
            Graphics graphics = Graphics.FromImage(bitmap);

           // bitmap.Save(@"D:\DrawQrCode.bmp", ImageFormat.Bmp);
            graphics.Clear(Color.White);
            graphics.Dispose();

            return DrawDarkModuleSquare(bitmap, options);
        }

        private Bitmap DrawDarkModuleSquare(Bitmap bitmap, EncoderOptions options)
        {
            BitMatrix bitMatrix = options.Matrix;
            int ModuleSize = options.QrRect.ModuleSize;
            int QuietZoneModuleSize = (int)options.QrRect.ZoneModule* ModuleSize;
            Graphics graphics = Graphics.FromImage(bitmap);
            //int padding = (size.CodeWidth - size.ModuleSize * matrix.Width) / 2;
            int preX = -1;
            // int moduleSize = size.ModuleSize;
            for (int y = 0; y < bitMatrix.Height; y++)
            {
                for (int x = 0; x < bitMatrix.Width; x++)
                {
                    if (bitMatrix[x, y])
                    {
                        if (preX == -1)
                            preX = x;
                        if (x == bitMatrix.Width - 1)
                        {
                            Rectangle moduleArea =
                                new Rectangle(preX * ModuleSize + QuietZoneModuleSize,
                                    y * ModuleSize + QuietZoneModuleSize,
                                    (x - preX + 1) * ModuleSize,
                                    ModuleSize);
                            graphics.FillRectangle(Brushes.Black, moduleArea);
                            preX = -1;
                        }
                    }
                    else if (preX != -1)
                    {
                        options.DFT++;
                        Rectangle moduleArea =
                            new Rectangle(preX * ModuleSize + QuietZoneModuleSize,
                                y * ModuleSize + QuietZoneModuleSize,
                                (x - preX) * ModuleSize,
                                ModuleSize);
                        graphics.FillRectangle(Brushes.Black, moduleArea);
                        preX = -1;
                    }
                }
            }
           // bitmap.Save(@"D:\DrawDarkModuleSquare.bmp", ImageFormat.Bmp);
            graphics.Flush();
            graphics.Dispose();
            return bitmap;
        }
    }
}
