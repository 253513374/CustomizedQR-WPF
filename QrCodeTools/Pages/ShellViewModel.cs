using Emgu.CV;
using Emgu.CV.Structure;
using MahApps.Metro.Controls.Dialogs;
using QrCodeEncoding.DrawQrCode;
using QrCodeEncoding.DrawQrCode.Config;
using QrCodeEncoding.DrawQrCode.ConfigJson;
using QrCodeTools.Model;
using Stylet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ZXing;

namespace QrCodeTools.Pages
{
    public class ShellViewModel : Screen
    {

        public DrawingOptions DrawingOptions { set; get; } = new DrawingOptions();
        private readonly IDialogCoordinator _dialogCoordinator;


       
        public ShellViewModel() {
            _dialogCoordinator = DialogCoordinator.Instance;
        }

        public BitmapImage BitmapSource { set; get; }

        public BitmapImage LogBitmaps { set; get; }

        public List<QrCodeCustom>   QrCodeCustoms { set; get; } = new List<QrCodeCustom>() {
            new QrCodeCustom(){ QrCodeTag=Tag.TagOne},
            new QrCodeCustom(){ QrCodeTag=Tag.TagTwo},
            new QrCodeCustom(){ QrCodeTag=Tag.TagThree},
            new QrCodeCustom(){ QrCodeTag=Tag.Content}
        };

        public Hashtable Hashtable { set; get; } = new Hashtable();

        public List<string> ImageContentlist { set; get; } = new List<string>();


        /// <summary>
        /// 预览
        /// </summary>
        public void EnQrCode()
        {

            // QrCode m_QrCode = new QrEncoder(drawingOptions.errorCorrectionLevel).Encode(drawingOptions.QrText);
            try
            {
                Task.Run<Bitmap>(() => {
                    QrCodeOptions options = GetQrCodeOptions();

                    if(ImageContentlist.Count>0)
                    {
                        return new DrawQrCode(options).QRCodeEncoder(ImageContentlist[0].Trim());
                    }
                    else
                    {
                        return new DrawQrCode(options).QRCodeEncoder();
                    }
                  
                    //return DrawingQrCodeImg.DrawDarkModule();
                }).ContinueWith(result => {
                    if (result.IsCompleted)
                    {
                        if (result.Result != null)
                        {
                            ShowEmguImg(result.Result);
                        }
                    }

                });
            }
            catch (Exception ex)
            {
                var controller = _dialogCoordinator.ShowMessageAsync(this, "提示", "程序EnQrCode运行出现异常" + ex.Message);
            }

        }

        private BitmapImage ToBitmapsource(Bitmap bitmap)
        {
            Bitmap img = new Bitmap(bitmap);
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Image<Gray, Byte> matimg = OtsuThreshold(img);

            matimg = matimg.SmoothMedian(11);
           // matimg.Save(@"D:\Desktop\DIYcode\matimg.jpg");
            var re = Decode(matimg.ToBitmap());
            if (re != "")
            {
                DrawingOptions.QrText = re;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png); // 坑点：格式选Bmp时，不带透明度

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                stream.Flush();
                stream.Dispose();
                bitmap.Dispose();
                return result;
            }
        }

        public void ShowLogBitmap(string filename)
        {

          
            if (filename == null || filename == "")
            {
                
                LogBitmaps = null;
                return;
            }
           
            LogBitmaps = ToBitmapsource(new Bitmap(filename));
           // LogBitmaps.CacheOption = BitmapCacheOption.OnLoad;
        }
        public void ShowEmguImg(Bitmap bitmap)
        {
            try
            {
               

                BitmapSource = ToBitmapsource(bitmap);
                //if (imagebox != null && imagebox.Image != null)
                //{
                //    imagebox.Image.Dispose();
                //}
                //Image<Bgr, Byte> image = new Image<Bgr, Byte>(bitmap);
                //imagebox.Image = image;

                //image.Dispose();
                //bitmap.Dispose();
            }
            catch (Exception ex)
            {
                var controller = _dialogCoordinator.ShowMessageAsync(this, "提示", "程序ShowEmguImg运行出现异常" + ex.Message);
            }
        }

        private string Decode(Bitmap bitmap)
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

        public void GetFileList(string filename)
        {
            if(ImageContentlist!=null&& ImageContentlist.Count>0)
            {
                ImageContentlist.Clear();
            }
            Task task = Task.Run(() => {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        ImageContentlist.Add(line.Trim());
                    }
                    sr.Close();

                    if(ImageContentlist.Count>0)
                    {
                        DrawingOptions.QrText = ImageContentlist[0].Trim();
                    }
                    
                };
            });
        }

        private QrCodeOptions GetQrCodeOptions() {
            return new QrCodeOptions()
            {
                PatternType = DrawingOptions.PatternType,
                qrCodeCustoms = this.QrCodeCustoms,
                QrCodeTypeEnum = DrawingOptions.QrCodeTypeEnum,
                DpiInch = DrawingOptions.DpiInch,
                ErrorCorrectionLevel = DrawingOptions.errorCorrectionLevel,
                IsTypeSizeMM = DrawingOptions.IsTypeSizeMM,
                IsTypeSizePX = DrawingOptions.IsTypeSizePX,
                IsTypeSizeAuto = DrawingOptions.IsTypeSizeAuto,
                QrCodeWidthMM = DrawingOptions.QrCodeWidthMM,
                LogoImgPath = DrawingOptions.LogoImgPath,
                IsTopLogoImg = DrawingOptions.IsTopLogoImg,
                QrText = DrawingOptions.QrText,
                QuietZoneModule = DrawingOptions.QuietZoneModule,
                QrCodePixelWidth = DrawingOptions.QrCodePixelWidth,
                widthdoubleFromat = DrawingOptions.widthdoubleFromat,
                densitydoubleFromat = DrawingOptions.densitydoubleFromat,
                MudelSize = DrawingOptions.MudelSize
            };
        }

        private ImageFormat StringConvertToEnum(string str)
        {
            ImageFormat  format = ImageFormat.Bmp;
            try
            {
                if (str.ToLower().Contains("bmp")) return ImageFormat.Bmp;

                if (str.ToLower().Contains("png")) return ImageFormat.Png;

                if (str.ToLower().Contains("jpg")) return ImageFormat.Jpeg;

               // format = (ImageFormat)Enum.Parse(typeof(ImageFormat), str);
            }
            catch (Exception ex)
            {
                var controller = _dialogCoordinator.ShowMessageAsync(this, "提示", "存图格式出现异常" + ex.Message);
                throw;
            }
            return format;
        }
        public async void Exportfiles( string folderpath)
        {
            if (ImageContentlist.Count > 0)
            {
                ImageFormat imageFormat = StringConvertToEnum(DrawingOptions.ImageFromat);

                ProgressDialogController dialogcontroller = await OpenDialog();
                for (int i = 0; i < ImageContentlist.Count; i++)
                {

                    string content, fullsavefilename;

                    var index_i = i + 1;
                    PpkReNameMethod(folderpath, index_i, out content, out fullsavefilename);
                    //WtdlReNameMethod(folderpath, i, out content, out fullsavefilename);

                    await Task.Run(() =>
                    {
                        Bitmap bitmap = GetQRCodeEncoder(content);

                        //bitmap.Save(fullsavefilename, imageFormat);
                      
                        var index_k = (i+1) * 5;
                        for (int k = index_k; k > Math.Abs(index_k - 5); k--)
                        {
                            PpkReNameMethod(folderpath, k, out content, out fullsavefilename);
                            bitmap.Save(fullsavefilename, imageFormat);
                        }
                        bitmap.Dispose();
                        ExpStringToText(content, folderpath);
                    });
   
                    dialogcontroller.SetProgress(i);
                    dialogcontroller.SetMessage(string.Format("正在存图第（{0}）张...", i));
                    if (dialogcontroller.IsCanceled)
                        break;
                }
                await dialogcontroller.CloseAsync();
            }
        }

        private async Task<ProgressDialogController> OpenDialog()
        {
            MetroDialogSettings mySettings = new MetroDialogSettings()
            {
                NegativeButtonText = "关闭导出任务",
                AnimateShow = false,
                AnimateHide = false
            };
            var controller = await this._dialogCoordinator.ShowProgressAsync(this, "提示", "正在努力存图中，请稍候。。。。。。", settings: mySettings);
            controller.SetIndeterminate();
            controller.Minimum = 0;
            controller.Maximum = ImageContentlist.Count;

            controller.SetCancelable(true);
            return controller;
        }

        private void PpkReNameMethod(string folderpath , int i, out string contentcvs, out string fullsavefilename)
        {
            string revisename = ReviseMethod(i);

            contentcvs = string.Format(@"{0}\{1},{1}", folderpath, revisename);

            fullsavefilename = folderpath + @"\" + revisename;
        }

        private  string ReviseMethod(int i)
        {
            var replacename = i.ToString();
            if (replacename.Length != 6)
            {
                var switch_onlength = 6 - replacename.Length;
                switch (switch_onlength)
                {
                    case 1:
                        replacename = "0" + replacename;
                        break;
                    case 2:
                        replacename = "00" + replacename;
                        break;
                    case 3:
                        replacename = "000" + replacename;
                        break;
                    case 4:
                        replacename = "0000" + replacename;
                        break;
                    case 5:
                        replacename = "00000" + replacename;
                        break;
                    default:
                        break;
                }
            }

            return replacename + "." + DrawingOptions.ImageFromat.ToString();
        }

        private void WtdlReNameMethod(string savepath, int i, out string content, out string filename)
        {
            var qrcontent = ImageContentlist[i].Trim().Split(',');

            content = qrcontent[0].ToString();
            filename = savepath + @"\" + qrcontent[1].ToString() + "." + DrawingOptions.ImageFromat;
        }

        private Bitmap GetQRCodeEncoder(string Content)
        {
            QrCodeOptions QrCodeOptions = GetQrCodeOptions();
            return new DrawQrCode(QrCodeOptions).QRCodeEncoder(Content);
        }
        /// <summary>
        /// 获取12位序号正则
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public  string ReplaceScanWord(string v)
        {
            string pattern = @"^[\w\W]*?(\w{12,30})$";
            //string pattern = @"^[\w\W]*?([[:alnum:]])$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);//正则表达式
            Match match = regex.Match(v);
            if (string.IsNullOrEmpty(match.Groups[1].Value)) return "";
            return match.Groups[1].Value;
        }

        public string ExpStringToText(string QRtext, string pathFile)
        {
            try
            {
                string path = string.Format(@"{0}\{1}.CSV", pathFile, DateTime.Now.ToLongDateString());
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sr = new StreamWriter(fs))
                    {
                        sr.WriteLine(QRtext);
                        sr.Flush();
                        sr.Dispose();
                    };
                    fs.Flush();
                    fs.Dispose();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }

        public void QrCodeTagOneCommand (QrCodeCustom qrCodeCustom)
        {
            int index = QrCodeCustoms.FindIndex(f => f.QrCodeTag == qrCodeCustom.QrCodeTag);
            if (index >= 0)
            {
                QrCodeCustoms[index] = qrCodeCustom;
               // EnQrCode();
            }
            
        }

        public void ExpJsonCommand(string file)
        {
            using (StreamWriter streamWriter = new StreamWriter(file))
            {
                QrCodeOptions qrCodeOptions = new QrCodeOptions()
                {
                    qrCodeCustoms = QrCodeCustoms,
                    QrCodeTypeEnum = DrawingOptions.QrCodeTypeEnum,
                    DpiInch = DrawingOptions.DpiInch,
                    ErrorCorrectionLevel = DrawingOptions.errorCorrectionLevel,
                    IsTypeSizeMM = DrawingOptions.IsTypeSizeMM,
                    IsTypeSizePX = DrawingOptions.IsTypeSizePX,
                    IsTypeSizeAuto = DrawingOptions.IsTypeSizeAuto,
                    QrCodeWidthMM = DrawingOptions.QrCodeWidthMM,
                    LogoImgPath = DrawingOptions.LogoImgPath,
                    IsTopLogoImg = DrawingOptions.IsTopLogoImg,
                    QrText = DrawingOptions.QrText,
                    QuietZoneModule = DrawingOptions.QuietZoneModule,
                    QrCodePixelWidth = DrawingOptions.QrCodePixelWidth,
                };
                var jsonvar = Newtonsoft.Json.JsonConvert.SerializeObject(qrCodeOptions);
                streamWriter.WriteLineAsync(jsonvar);
            }
        }

        public void FiilSaveAsCommand(string imgfile)
        {
            BitmapEncoder encoder = GetBitmapEncoder(imgfile);
            encoder.Frames.Add(BitmapFrame.Create(BitmapSource));

            using (var stream = new FileStream(imgfile, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }
        private BitmapEncoder GetBitmapEncoder(string filePath)
        {
            var extName = Path.GetExtension(filePath).ToLower();
            if (extName.Equals(".png"))
            {
                return new PngBitmapEncoder();
            }
            else if (extName.Equals(".jpg")|| extName.Equals(".jpeg"))
            {
                return new JpegBitmapEncoder();
            }
            else 
            {
                return new BmpBitmapEncoder();
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
 