using QrCodeEncoding;
using QrCodeEncoding.DrawQrCode.Config;
using QrCodeEncoding.Masking;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace QrCodeTools.Model
{
    public class DrawingOptions
    {

        #region MyRegion

     
        public Array QrCodeSquareSizes
        {
            get { return Enum.GetValues(typeof(QrCodeSquareSize)); }
        }

        public Array QrCodeTagEnums
        {
            get { return Enum.GetValues(typeof(QrCodeStyleEnum)); }
        }
        public Array QrCodeTypes {
            get { return Enum.GetValues(typeof(QrCodeTypeEnum)); }
        }

        public Array MaskPatternTypes
        {

            get { return Enum.GetValues(typeof(MaskPatternType)); }
        }


        public List<double> ListdoubleFromats
        {
            get { return new List<double> { 0.05, 0.09, 0.12,0.15 }; }
        }

        public List<double> densitydoubleFromats
        {
            get { return new List<double> { 0.02,  0.03, 0.04, 0.05 }; }
        }

        /// <summary>
        /// 图像格式
        /// </summary>
        public string[] ImageFromats { set; get; } = new string[3] { "png", "bmp", "jpg" };

        /// <summary>
        /// 图像分辨率
        /// </summary>
        public int[] DpiInchs { set; get; } = new int[7] { 72, 96, 144, 300, 600,960,1200 };

        // public int[] DpiInch { set; get; } = new int[3] { };
        public double widthdoubleFromat { set; get; } = 0.05;

        public double densitydoubleFromat { set; get; } = 0.03;

        /// <summary>
        /// 图像纠错级别
        /// </summary>
        public Array ErrorCorrectionLevelComboBox
        {
            get { return Enum.GetValues(typeof(ErrorCorrectionLevel)); }

        }

        public MaskPatternType PatternType { set; get; } = MaskPatternType.Type5;

        /// <summary>
        /// 二维码边缘模块数量
        /// </summary>
        public Array QuietZoneModuleComboBox
        {
            get { return Enum.GetValues(typeof(QuietZoneModule)); }
        }
        #endregion


        public QrCodeTypeEnum QrCodeTypeEnum { set; get; } = QrCodeTypeEnum.CustomFeature;

        public string ImageFromat { set; get; } = "jpg";
        /// <summary>
        /// log图像
        /// </summary>
        public string LogoImgPath { set; get; } = "";

        public int MudelSize { set; get; } = 50;
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTopLogoImg { set; get; } = false;
        /// <summary>
        /// 二维码内容
        /// </summary>
        public string QrText { set; get; } = "https://www.hao123.com";
        /// <summary>
        /// 留白几个模块边框
        /// </summary>
        public QuietZoneModule QuietZoneModule { set; get; } = QuietZoneModule.One;

        /// <summary>
        /// 纠错等级
        /// </summary>
        public ErrorCorrectionLevel errorCorrectionLevel { set; get; } = ErrorCorrectionLevel.L;


        public bool IsTypeSizeAuto { set; get; }

        public bool IsTypeSizePX { set; get; }

        public bool IsTypeSizeMM { set; get; } = true; 
        /// <summary>
        /// 默认分辨率
        /// </summary>
        public int DpiInch { set; get; } = 600;
      
        // public IsGrayImage
        /// <summary>
        /// 默认二维码模块大小 毫米
        /// </summary>
        public int QrCodeWidthMM { set; get; } = 20;

        /// <summary>
        /// 默认二维码模块大小 像素
        /// </summary>
        public int QrCodePixelWidth { set; get; } = 800;

      

        public QrCodeStyleEnum qrCodeTag1 { set; get; } = QrCodeStyleEnum.Square;
        public QrCodeStyleEnum qrCodeTag2 { set; get; } = QrCodeStyleEnum.Square;
        public QrCodeStyleEnum qrCodeTag3 { set; get; } = QrCodeStyleEnum.Square;
        public QrCodeStyleEnum qrCodeTag4 { set; get; } = QrCodeStyleEnum.Square;


        public QrCodeSquareSize qrCodeSquareSize { set; get; } = QrCodeSquareSize.Max;


    }
}
