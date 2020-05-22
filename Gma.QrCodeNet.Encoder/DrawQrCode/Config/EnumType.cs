using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeEncoding.DrawQrCode.Config
{
    /// <summary>
    /// 生成类型
    /// </summary>
    public enum QrCodeTypeEnum
    {

        Square = 1,
        Custom = 2,
        Feature =3,
        CustomPath=4,
        CustomFeature=5,
        CustomSquare=6

    }

    /// <summary>
    /// 样式
    /// </summary>
    public enum QrCodeStyleEnum
    {
        Square = 1,
        Circle = 2,
        Excircle=3,
        InnerCircle,
        ExInnerCircle,
        ExSquareCircle,
       



    }

    /// <summary>
    /// 码眼标识
    /// </summary>
    public enum Tag
    {
        TagOne = 1,

        TagTwo = 2,

        TagThree = 3,

        Content = 4
    }

    public enum QrCodeSquareSize
    {
        Max=1,
        Min=2 
    }
}
