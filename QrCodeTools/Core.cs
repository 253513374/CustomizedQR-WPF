using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QrCodeTools
{
    public class Core
    {
        //void NaiveRemoveNoise(int pNum)
        //{
        //    naive remove noise
        //    int i, j, m, n, nValue, nCount;
        //    int nWidth = getWidth();
        //    int nHeight = getHeight();
        //    set boundry to be white
        //    for (i = 0; i < nWidth; ++i)
        //    {
        //        setPixel(i, 0, WHITE);
        //        setPixel(i, nHeight - 1, WHITE);
        //    }
        //    for (i = 0; i < nHeight; ++i)
        //    {
        //        setPixel(0, i, WHITE);
        //        setPixel(nWidth - 1, i, WHITE);
        //    }
        //    if the neighbor of a point is white but it is black, delete it
        //    for (j = 1; j < nHeight; ++j)
        //        for (i = 1; i < nWidth; ++i)
        //        {
        //            nValue = getPixel(i, j);
        //            if (!nValue)
        //            {
        //                nCount = 0;
        //                for (m = i - 1; m <= i + 1; ++m)
        //                    for (n = j - 1; n <= j + 1; ++n)
        //                    {
        //                        if (!getPixel(m, n))
        //                            nCount++;
        //                    }
        //                if (nCount <= pNum)
        //                    setPixel(i, j, WHITE);
        //            }
        //            else
        //            {
        //                nCount = 0;
        //                for (m = i - 1; m <= i + 1; ++m)
        //                    for (n = j - 1; n <= j + 1; ++n)
        //                    {
        //                        if (!getPixel(m, n))
        //                            nCount++;
        //                    }
        //                if (nCount >= 7)
        //                    setPixel(i, j, BLACK);
        //            }
        //        }
        //}

        public Image<Bgr, byte> SmoothMedian(Bitmap bitmap)
        {
            var image = new Image<Bgr, byte>(bitmap);
          
           return image.SmoothMedian(5);//使用5*5的卷积核
        }

    }
}
