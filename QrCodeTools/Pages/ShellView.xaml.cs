using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using MahApps.Metro.Controls;
using QrCodeEncoding.DrawQrCode;
using QrCodeEncoding.DrawQrCode.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


using ZXing;
using Rectangle = System.Windows.Shapes.Rectangle;


namespace QrCodeTools.Pages
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellView : MetroWindow
    {

        private QrCodeStyleEnum QrCodeStyleEnum { set; get; } = QrCodeStyleEnum.Square;
        private Color Color { set; get; } =  Color.FromArgb(255, 0, 0, 0);

        private QrCodeSquareSize QrCodeSquareSize { set; get; } = QrCodeSquareSize.Max;
#pragma warning disable CS0108 // '“ShellView.Tag”隐藏继承的成员“FrameworkElement.Tag”。如果是有意隐藏，请使用关键字 new。
        private Tag Tag { set; get; } = (Tag)44;
#pragma warning restore CS0108 // '“ShellView.Tag”隐藏继承的成员“FrameworkElement.Tag”。如果是有意隐藏，请使用关键字 new。
      //  private QrCodeCustom qrCodeCustom { set; get; } = new QrCodeCustom();
        public ShellView()
        {
            
            InitializeComponent();

            
        }

        #region MyRegion

     
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           // if (!(sender is RichTextBox richTextBox)) return;
        }

        private void RichTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog() {
                Filter = "Txt Files (*.txt)|*.txt"
            };
            System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK )
            {
                FileListButton.Command.Execute(openFileDialog.FileName);
               // this.textblock_filename.Text = openFileDialog.FileName;
            }
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog()
            {
                Filter = "图像文件|*.jpg;*.jpeg;*.bmp;*.png"
            };
            System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = openFileDialog.FileName.Trim();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            LogBitmapButton.Command.Execute(textBox.Text);
            //Image < Bgr,Byte > image  = new Image<Bgr,Byte>(textBox.Text);
            //emguimgbox.Image = image;
            // image.Dispose();
        }

        private void EnQrButton_Click(object sender, RoutedEventArgs e)
        {
            //this.BorderThickness.BorderThickness = new Thickness(1);
            EnQrButtonCommand.Command.Execute(null);
        }

        private void ExpButton_Click(object sender, RoutedEventArgs e)
        {


            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };
            System.Windows.Forms.DialogResult dialog =folderBrowserDialog.ShowDialog();

            if(dialog == System.Windows.Forms.DialogResult.OK)
            {
                this.ExpButtonCommand.Command.Execute(folderBrowserDialog.SelectedPath.Trim());
            }
        }

        public Color GetColor(Rectangle rectangle)
        {
            System.Windows.Forms.ColorDialog colorPicker = new System.Windows.Forms.ColorDialog();

            System.Windows.Forms.DialogResult dialogResult = colorPicker.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Color color = colorPicker.Color;
                rectangle.Fill = new System.Windows.Media.SolidColorBrush( System.Windows.Media.Color.FromArgb(color.A, color.R, color.G,color.B)); 

                return color;
            }

            System.Windows.Media.SolidColorBrush SolidBrush = (System.Windows.Media.SolidColorBrush)rectangle.Fill;
            return Color.FromArgb(SolidBrush.Color.A, SolidBrush.Color.R, SolidBrush.Color.G, SolidBrush.Color.B);
        }
        private void TagColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Rectangle rectangle)) return;

            QrCodeStyleEnum StyleEnum = (QrCodeStyleEnum)rectangle.Parent.FindChild<ComboBox>().SelectedItem;
            this.Color = GetColor(rectangle);
            this.Tag = (Tag)(int.Parse(rectangle.Tag.ToString()));
            QrCodeTagOneButton.Command.Execute(new QrCodeCustom() { QrCodeTag=this.Tag, ColorBrush = this.Color,  QrCodeStyle= StyleEnum, QrCodeSize=this.QrCodeSquareSize});
        }
      
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox comboBox)) return;

            this.QrCodeStyleEnum =(QrCodeStyleEnum) comboBox.SelectedItem;

            System.Windows.Media.SolidColorBrush SolidBrush = (System.Windows.Media.SolidColorBrush)comboBox.Parent.FindChild<Rectangle>().Fill;
            Color color = Color.FromArgb(SolidBrush.Color.A, SolidBrush.Color.R, SolidBrush.Color.G, SolidBrush.Color.B);
            this.Tag = (Tag)(int.Parse(comboBox.Tag.ToString()));
            QrCodeTagOneButton.Command.Execute(new QrCodeCustom() { QrCodeTag = this.Tag, ColorBrush = color, QrCodeStyle = this.QrCodeStyleEnum });
        }


        private void ExpJsonButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Filter = "Json File|*.json|所有文件|*.*",
                Title = "保存配置json文件",
                FileName = "QrCodeConfig",//设置默认文件名
                RestoreDirectory = true,//保存对话框是否记忆上次打开的目录
                AddExtension = true
            };
            System.Windows.Forms.DialogResult dialogResult= saveFileDialog.ShowDialog();
            if(dialogResult== System.Windows.Forms.DialogResult.OK)
            {
                ExpJsonCommand.Command.Execute(saveFileDialog.FileName);

            }
        }

        private void ComboBoxSzie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is ComboBox comboBox)) return;

            this.QrCodeSquareSize = (QrCodeSquareSize)comboBox.SelectedItem;

            System.Windows.Media.SolidColorBrush SolidBrush = (System.Windows.Media.SolidColorBrush)comboBox.Parent.FindChild<Rectangle>().Fill;
            Color color = Color.FromArgb(SolidBrush.Color.A, SolidBrush.Color.R, SolidBrush.Color.G, SolidBrush.Color.B);

            this.Tag = (Tag)(int.Parse(comboBox.Tag.ToString()));
            QrCodeTagOneButton.Command.Execute(new QrCodeCustom() { QrCodeTag = this.Tag, ColorBrush = color, QrCodeStyle = this.QrCodeStyleEnum, QrCodeSize = this.QrCodeSquareSize });

        }

        private void FiilSaveAs_Click(object sender, RoutedEventArgs e)
        {
            // if (!(sender is System.Windows.Controls.Image image)) return;

            if (ImgContorl.Source == null) return;
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Filter = "图像 File|*.BMP|格式|*.JPG|格式|*.JPEG|格式|*.PNG|所有文件|*.*",
                Title = "保存预览图像",
                FileName = DateTime.Now.ToLongDateString(),
                RestoreDirectory = true,//保存对话框是否记忆上次打开的目录
                AddExtension = true
            };
            System.Windows.Forms.DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                SaveFileAs.Command.Execute(saveFileDialog.FileName);
                //System.Windows.Media.ImageSource sourceimg = image.Source;
            }
        }
        #endregion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // decode();
           // buttonClick();

                //以锐化效果显示图像
            try
                {
                    Bitmap oldBitmap = new Bitmap(@"D:\Desktop\DIYcode\微孔-识别\RT5911A9OMZ3000973.jpg");
                oldBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    int Height = oldBitmap.Height;
                    int Width = oldBitmap.Width;
                    Bitmap newBitmap = new Bitmap(Width, Height);
             
                Color pixel;
                //拉普拉斯模板
                //int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
                //for (int x = 1; x < Width - 1; x++)
                //    for (int y = 1; y < Height - 1; y++)
                //    {
                //        int r = 0, g = 0, b = 0;
                //        int Index = 0;
                //        for (int col = -1; col <= 1; col++)
                //            for (int row = -1; row <= 1; row++)
                //            {
                //                pixel = oldBitmap.GetPixel(x + row, y + col); r += pixel.R * Laplacian[Index];
                //                g += pixel.G * Laplacian[Index];
                //                b += pixel.B * Laplacian[Index];
                //                Index++;
                //            }
                //        //处理颜色值溢出
                //        r = r > 255 ? 255 : r;
                //        r = r < 0 ? 0 : r;
                //        g = g > 255 ? 255 : g;
                //        g = g < 0 ? 0 : g;
                //        b = b > 255 ? 255 : b;
                //        b = b < 0 ? 0 : b;
                //        newBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                //    }

               // new GrayBitmapData(newBitmap).NewFilter(9);
               // newBitmap.Save(@"D:\Desktop\DIYcode\newBitmap.jpg");


                Image<Gray, Byte> matimg = OtsuThreshold(oldBitmap);

                matimg.Save(@"D:\Desktop\DIYcode\matimg.jpg");
                matimg=matimg.SmoothMedian(11);
                matimg.Save(@"D:\Desktop\DIYcode\SmoothMedian.jpg");
                Image<Gray, Byte> edges = new Image<Gray, byte>(newBitmap.Width, newBitmap.Height);

                Image<Bgr, Byte> contoursimg = new Image<Bgr, byte>(oldBitmap);

                Mat hierarchy = new Mat();
               // Image<Gray, Byte> hierarchy = new Image<Gray, byte>(matimg.Width, matimg.Height);

                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

                VectorOfVectorOfPointF VectorOfPointF = new VectorOfVectorOfPointF();

                CvInvoke.Canny(matimg, edges,0,200);

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
                        if (c >=5)
                        {
                          

                          
                            fater_contours.Add(k);
                        }
                    }
                }
                for(int j=0;j< fater_contours.Count;j++)
                {
                    RotatedRect rect = CvInvoke.MinAreaRect(contours[fater_contours[j]]); //minAreaRect
                    PointF[] pf = CvInvoke.BoxPoints(rect);//BoxPoints();
                   // VectorOfPointF.Push(new VectorOfPointF(pf));
                    contoursimg.Draw(rect,new Bgr(255,0,0),2);
                   // CvInvoke.DrawContours(new Image<Bgr, Byte>(newBitmap), VectorOfPointF, -1, new MCvScalar(0, 255, 0), 1, LineType.AntiAlias);
                }

                var str=decode(matimg.ToBitmap());
                ImgContorl.Source = ToBitmapsource(contoursimg.ToBitmap());
                //CvInvoke.DrawContours(new Image<Bgr, Byte>(newBitmap), VectorOfPointF, -1 , new MCvScalar(0, 255, 0), 1, LineType.AntiAlias);
                //double area = CvInvoke.ContourArea(contours[k]);
                //if (area > 20000 && area < 30000)
                //{
                //    CvInvoke.DrawContours(contoursimg, contours, k, new MCvScalar(0, 255, 0), 1, Emgu.CV.CvEnum.LineType.AntiAlias);
                //    contoursimg.Save(@"D:\Desktop\DIYcode\contoursimg" + k.ToString() + ".jpg");
                //    vector.Push(ofPoint);
                //}

                //if (area > 3300 && area < 3800)
                //{
                //    CvInvoke.DrawContours(contoursimg, contours, k, new MCvScalar(255, 0, 0), 1, Emgu.CV.CvEnum.LineType.AntiAlias);
                //    contoursimg.Save(@"D:\Desktop\DIYcode\werwer" + k.ToString() + ".jpg");
                //    vector.Push(ofPoint);
                //}

                //if (area>500)
                //{
                //    CvInvoke.DrawContours(contoursimg, contours, k, new MCvScalar(255, 0, 0), 1, Emgu.CV.CvEnum.LineType.AntiAlias);
                //}
                //  CvInvoke.DrawContours(disp, contours, k, new MCvScalar(0, 255, 0), 1, Emgu.CV.CvEnum.LineType.AntiAlias, null, 1);
                //    }
                //}

            }
            catch (Exception ex)
            {
                //  return null;
                //  MessageBox.Show(ex.Message, "信息提示");
            }
            
        }

        private System.Windows.Media.ImageSource ToBitmapsource(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png); // 坑点：格式选Bmp时，不带透明度

                stream.Position = 0;
                System.Windows.Media.Imaging.BitmapImage result = new System.Windows.Media.Imaging.BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                stream.Flush();
                stream.Dispose();
                bitmap.Dispose();
                return result;
            }
        }

        private string decode( Bitmap bitmap)
        {
            IBarcodeReader reader = new BarcodeReader();

            // detect and decode the barcode inside the bitmap
            var result = reader.Decode(bitmap);
            // do something with the result
            if (result != null)
            {
                return result.Text;

            }
            else
            {
                return "";
            }
        }


        private string Encode(string text)
        {
            return "";
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
