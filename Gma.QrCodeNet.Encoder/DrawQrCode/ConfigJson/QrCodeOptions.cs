
using QrCodeEncoding.DrawQrCode.Config;
using QrCodeEncoding.Masking;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeEncoding.DrawQrCode.ConfigJson
{
    public class QrCodeOptions
    {
        public QrCodeOptions()
        {
        }

        /// <summary>
        /// 掩码
        /// </summary>
        public MaskPatternType PatternType { set; get; } = MaskPatternType.Type5;
        /// <summary>
        /// 自定义样式
        /// </summary>
        public List<QrCodeCustom> qrCodeCustoms { set; get; } = new List<QrCodeCustom>();

        /// <summary>
        /// 二维码生成类型
        /// </summary>
        public QrCodeTypeEnum  QrCodeTypeEnum { set; get; } = QrCodeTypeEnum.Square;
        /// <summary>
        /// log图像物理路径
        /// </summary>
        public string LogoImgPath { set; get; } = "";

        /// <summary>
        /// 是否置顶logo图像
        /// </summary>
        public bool IsTopLogoImg { set; get; } = false;

        /// <summary>
        /// 默认分辨率
        /// </summary>
        public int DpiInch { set; get; } = 600;

        // public IsGrayImage
        /// <summary>
        /// 默认二维码模块大小（像素）
        /// </summary>
        public int QrCodeWidthMM { set; get; } = 100;

        /// <summary>
        /// 留白几个模块边框
        /// </summary>
        public QuietZoneModule QuietZoneModule { set; get; } = QuietZoneModule.One;

        /// <summary>
        /// 纠错等级
        /// </summary>
        public ErrorCorrectionLevel ErrorCorrectionLevel { set; get; } = ErrorCorrectionLevel.Q;

        /// <summary>
        /// 二维码内容
        /// </summary>
        public string QrText { set; get; } = "http://weixin.qq.com/r/gkPcxOjEeLUHrayn9xaQ/t/00742011500000000041/00742011500000000041";


        /// <summary>
        ///像素
        /// </summary>
        public bool IsTypeSizePX { set; get; }


        /// <summary>
        /// 默认自动生成二维码大小
        /// </summary>
        public bool IsTypeSizeAuto { set; get; } = true;


        /// <summary>
        /// 毫米
        /// </summary>
        public bool IsTypeSizeMM { set; get; }


        /// <summary>
        /// 二维码模块大小 像素
        /// </summary>
        public int QrCodePixelWidth { set; get; } = 600;


        public int MudelSize { set; get; }


        public double widthdoubleFromat { set; get; } = 0.05;

        public double densitydoubleFromat { set; get; } = 0.03;

    }
}
