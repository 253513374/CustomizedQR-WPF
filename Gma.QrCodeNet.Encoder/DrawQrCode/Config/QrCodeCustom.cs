using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QrCodeEncoding.DrawQrCode.Config
{

    /// <summary>
    /// 自定义配置类
    /// </summary>
    public class QrCodeCustom
    {

        public QrCodeCustom() { }

        /// <summary>
        /// 表示当前是哪个码眼
        /// </summary>
        public Tag QrCodeTag { set; get; } = Tag.TagOne;

        /// <summary>
        /// 码眼自定义样式枚举
        /// </summary>
        public QrCodeStyleEnum  QrCodeStyle { set; get; } = QrCodeStyleEnum.Square;

        /// <summary>
        /// 码眼自定义颜色
        /// </summary>
        public Color ColorBrush { set; get; }     = Color.FromArgb(255, 0, 0, 0);


        /// <summary>
        /// 点阵大小
        /// </summary>
        public QrCodeSquareSize QrCodeSize { set; get; }
      
    }
}
