using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Model;

namespace WindowsFormsApplication5.Helper
{
    /// <summary>
    /// Tách các hàm thường sử dụng sang đây cho gọn và tái sử dụng
    /// </summary>
    public static class DrawHelper
    {

        public static Point getPointDrawLine(float alpha, Point tamHinhTron, int r)
        {
            if (alpha >= 0 && alpha <= 180)
            {
                return new Point()
                {
                    X = tamHinhTron.X + (int)(r * Math.Sin(Math.PI * alpha / 180)),
                    Y = tamHinhTron.Y - (int)(r * Math.Cos(Math.PI * alpha / 180))
                };
            }
            else
            {
                return new Point()
                {
                    X = tamHinhTron.X - (int)(r * -Math.Sin(Math.PI * alpha / 180)),
                    Y = tamHinhTron.Y - (int)(r * Math.Cos(Math.PI * alpha / 180))
                };
            }
        }

        public static int GetAngle(Point centerPoint, Point currentPoint)
        {
            var x = currentPoint.X - centerPoint.X;
            var y = currentPoint.Y - centerPoint.Y;
            var angle = (int)(Math.Atan2(y, x) * 180 / Math.PI);
            if (angle < 0) angle = 360 + angle;
            return angle;

            
        }

        public static int dcGetAngle(Point centerPoint, Point currentPoint)
        {
            var x = currentPoint.X - centerPoint.X;
            var y = currentPoint.Y - centerPoint.Y;
            var angle = (int)(Math.Atan2(y, x) * 180 / Math.PI) + 90;
            if (angle < 0) angle = 360 + angle;
            return angle;


        }

        public static int GetRadius(Point centerPoint, Point currentPoint)
        {
            var x = currentPoint.X - centerPoint.X;
            var y = currentPoint.Y - centerPoint.Y;

            return (int)Math.Sqrt(x * x + y * y);
        }

        public static bool IsOutBox(Point centerPoint, Point currentPoint, int limit)
        {
            return GetRadius(centerPoint, currentPoint) > limit;
        }
        public static float GetSweepAngle(float startAngle, float stopAngle)
        {
            var sweepAngle = startAngle - stopAngle;
            return sweepAngle < 0 ? sweepAngle * -1 : 360 - sweepAngle;
        }

        public static void ArcDrawer(this Graphics graphics, ArcDrawerModel arcDrawler)
        {
            graphics.DrawLine(arcDrawler.Pen, arcDrawler.StartPointLine1, arcDrawler.StopPointLine1);
            graphics.DrawLine(arcDrawler.Pen, arcDrawler.StartPointLine2, arcDrawler.StopPointLine2);

            graphics.DrawArc(arcDrawler.Pen, arcDrawler.Rectangle1, arcDrawler.StartAngle, arcDrawler.SweepAngle);
            graphics.DrawArc(arcDrawler.Pen, arcDrawler.Rectangle2, arcDrawler.StartAngle, arcDrawler.SweepAngle);
        }

    }
}
