using QrCodeEncoding.DrawQrCode.Config;
using QrCodeEncoding.DrawQrCode.ConfigJson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeEncoding.DrawQrCode
{
    public class DrawQrCode
    {
        private QrCodeOptions qrCodeOptions;
        public DrawQrCode() { }
        public DrawQrCode(QrCodeOptions codeOptions) {

            this.QrCodeOptions = codeOptions;
        }

        public QrCodeOptions QrCodeOptions { get => qrCodeOptions; set => qrCodeOptions = value; }
      //  private List<QrBitmapMatrix> bitMats { set; get; } = new List<QrBitmapMatrix>(2000);


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="txtcontent"></param>
        /// <returns></returns>
        public Bitmap QRCodeEncoder(string txtcontent="")
        {
            try
            {
                if(txtcontent == "")
                {
                    txtcontent = QrCodeOptions.QrText;
                   
                }
                EncoderOptions options = GetOptions(txtcontent, QrCodeOptions.PatternType);
                switch (QrCodeOptions.QrCodeTypeEnum)
                {
                    case QrCodeTypeEnum.Custom:

                        return new DrawDarkCustom().DrawQrCode(options);
                    case QrCodeTypeEnum.Square:

                        return new DrawDarkSquare().DrawQrCode(options);

                    case QrCodeTypeEnum.CustomFeature:
                       return  new DrawDrakFeature().DrawQrCode(options);


                    case QrCodeTypeEnum.CustomSquare:
                        return new DrawDrakCustomSquare().DrawQrCode(options);
                }

                return null;

            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        private EncoderOptions GetOptions(string txtcontent, Masking.MaskPatternType maskPatternType)
        {
            if (QrCodeOptions == null) return null;
            BitMatrix matrix = new QrEncoder((ErrorCorrectionLevel)QrCodeOptions.ErrorCorrectionLevel).Encode(txtcontent, maskPatternType).Matrix;
            QrRect  qrRect = GetQrCodeSize( matrix);
            List<QrBitmapMatrix> bitMats=  GetQrBitmapMatrixs(matrix, qrRect);

            return new EncoderOptions() {
                Matrix = matrix,
                BitMats = bitMats,
                QrRect = qrRect,
                QrCodeCustoms= QrCodeOptions.qrCodeCustoms,
                LogoImgPath= QrCodeOptions.LogoImgPath,
                IsTopLogoImg = QrCodeOptions.IsTopLogoImg,
                DpiInch = QrCodeOptions.DpiInch,
                widthdoubleFromat= QrCodeOptions.widthdoubleFromat,
                densitydoubleFromat =qrCodeOptions.densitydoubleFromat
            };
        }

        private QrRect GetQrCodeSize(BitMatrix matrix)
        {
            // int ModuleSize = 50;
            int ModuleSize = QrCodeOptions.MudelSize;
            QrRect qrRect = new QrRect();
            
            if (QrCodeOptions.IsTypeSizeAuto)
            {
                
                qrRect.QrCodeMinWidth = matrix.Width * ModuleSize + ModuleSize * ((int)QrCodeOptions.QuietZoneModule) * 2;
                qrRect.ModuleSize = ModuleSize;
            }
          
            if (QrCodeOptions.IsTypeSizePX)
            {
                //以像素设置大小
                qrRect.QrCodeMinWidth = QrCodeOptions.QrCodePixelWidth;
                qrRect.ModuleSize = (int)Math.Round(QrCodeOptions.QrCodePixelWidth / (matrix.Width + (double)QrCodeOptions.QuietZoneModule * 2), 0);
            }
            if (QrCodeOptions.IsTypeSizeMM)
            {
                    //把毫米换算成实际像素单位
                   
                    double doublewidth =  Math.Round(((1 / 25.4 * QrCodeOptions.QrCodeWidthMM) * QrCodeOptions.DpiInch),1);
                    qrRect.QrCodeMinWidth = (int)Math.Round(doublewidth,0);

                    qrRect.ModuleSize = (int)Math.Round(doublewidth / (matrix.Width + (double)QrCodeOptions.QuietZoneModule * 2), 0);
                }
           
            qrRect.ZoneModule = QrCodeOptions.QuietZoneModule;
            return qrRect;
        }

        private List<QrBitmapMatrix> GetQrBitmapMatrixs(BitMatrix bitMatrix, QrRect qrRect)
        { 
            List<QrBitmapMatrix> bitMats = new List<QrBitmapMatrix>(2000);
            const int msize = 0x7;
            int Width = bitMatrix.Width;
            int hx = bitMatrix.Width - msize;
            Tag tag = 0;
            for (int column = 0; column < Width; column++)
            {
                for (int row = 0; row < Width; row++)
                {
                    bool boolvar = bitMatrix[row, column];
                    if (column < msize && row < msize)
                    {
                        // DrawingBitMat(row, column, boolvar, new SolidBrush(QrCodeOptions.ColorTagOne.ColorBrush));
                        tag = Tag.TagOne;
                    }
                    if (row >= hx && column < msize)
                    {
                        //DrawingBitMat(row, column, boolvar, new SolidBrush(QrCodeOptions.ColorTagTwo.ColorBrush));
                        tag = Tag.TagTwo;
                    }
                    if (column >= hx && row < msize)
                    {
                        // DrawingBitMat(row, column, boolvar, new SolidBrush(QrCodeOptions.ColorTagThree.ColorBrush));
                        tag = Tag.TagThree;
                    }

                    if ((row >= msize && row < hx) && column < msize)
                    {
                        tag = Tag.Content;
                    }
                    if (column >= msize && column < hx)
                    {
                        tag = Tag.Content;
                    }
                    if (row >= msize && column >= hx)
                    {
                        tag = Tag.Content;
                    }

                    bitMats.Add(new QrBitmapMatrix()
                    {
                        Point = new Point(row, column),
                        BoolVar = boolvar,
                        Tag = tag,
                        ModuleSize= qrRect.ModuleSize,
                        Rectangle = new Rectangle(row * qrRect.ModuleSize + qrRect.ModuleSize* (int)qrRect.ZoneModule, column * qrRect.ModuleSize + qrRect.ModuleSize * (int)qrRect.ZoneModule, qrRect.ModuleSize, qrRect.ModuleSize)
                    });
                }
            }
            return bitMats;
        }
    }
}
