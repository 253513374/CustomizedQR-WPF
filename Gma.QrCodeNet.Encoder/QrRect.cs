using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QrCodeEncoding
{
    public class QrRect
    {


        /// <summary>
        /// 图像大小
        /// </summary>
        public int QrCodeMinWidth { set; get; }

        /// <summary>
        /// 模块大小
        /// </summary>
        public int ModuleSize { set; get; } = 50;


        /// <summary>
        /// 留边模块个数
        /// </summary>
        public QuietZoneModule ZoneModule { set; get; }


        /// <summary>
        /// 矩形旋转
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="lpfs">旋转后的路径</param>
        public PointF[] Rect2Pointfs(Rectangle rect, float angle)
        {
            using (var graph = new GraphicsPath())
            {
                Point Center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                graph.AddRectangle(rect);
                var a = -angle * (Math.PI / 180);
                var n1 = (float)Math.Cos(a);
                var n2 = (float)Math.Sin(a);
                var n3 = -(float)Math.Sin(a);
                var n4 = (float)Math.Cos(a);
                var n5 = (float)(Center.X * (1 - Math.Cos(a)) + Center.Y * Math.Sin(a));
                var n6 = (float)(Center.Y * (1 - Math.Cos(a)) - Center.X * Math.Sin(a));
                graph.Transform(new Matrix(n1, n2, n3, n4, n5, n6));
                return graph.PathPoints;
            }
        }

        /// <summary>
        /// 随机纹理坐标
        /// </summary>
        /// <param name="pixelWidth">纹理范围</param>
        /// <param name="PolygonWidth">纹理大小</param>
        /// <param name="density">纹理密度</param>
        /// <returns></returns>
        public List<PointF[]> GetRandomPointF(Scope pixelScope, Size PolygonWidth, int density)
        {
            List<PointF[]> points = new List<PointF[]>(100);

            Random random = new Random();

            for (int i = 0; i < density; i++)
            {
                int row = random.Next(pixelScope.Row_Min, pixelScope.Row_Max);
                int column = random.Next(pixelScope.Column_Min, pixelScope.Column_Max);
                int angle = random.Next(0, 360);
                PointF[] pointFs = Rect2Pointfs(new Rectangle(row, column, PolygonWidth.Width, PolygonWidth.Height), angle);
                points.Add(pointFs);
            }
            return points;
        }
    }

    public class Scope
    {
        public Scope() { }
        public int Row_Min { set; get; } = 0;

        public int Row_Max { set; get; }

        public int Column_Min { set; get; } = 0;
        public int Column_Max { set; get; }
    }
}
