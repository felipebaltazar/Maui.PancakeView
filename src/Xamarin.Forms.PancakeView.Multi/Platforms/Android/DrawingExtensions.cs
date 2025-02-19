﻿using Path = Android.Graphics.Path;
using Point = Android.Graphics.Point;
using RectF = Android.Graphics.RectF;

namespace Maui.PancakeView.Droid
{
    public static class DrawingExtensions
    {
        public static Path CreateRoundedRectPath(float rectWidth, float rectHeight, float topLeft, float topRight, float bottomRight, float bottomLeft)
        {
            var path = new Path();
            var radii = new[] { topLeft, topLeft,
                                topRight, topRight,
                                bottomRight, bottomRight,
                                bottomLeft, bottomLeft };


            path.AddRoundRect(new RectF(0, 0, rectWidth, rectHeight), radii, Path.Direction.Ccw);
            path.Close();

            return path;
        }

        public static Path CreatePolygonPath(int rectWidth, int rectHeight, int sides, double cornerRadius = 0.0, double rotationOffset = 0.0)
        {
            var offsetRadians = rotationOffset * Math.PI / 180;

            var path = new Path();
            var theta = 2 * Math.PI / sides;

            // depends on the rotation
            var width = (-cornerRadius + Math.Min(rectWidth, rectHeight)) / 2;
            var center = new Point(rectWidth / 2, rectHeight / 2);

            var radius = width + cornerRadius - (Math.Cos(theta) * cornerRadius) / 2;

            var angle = offsetRadians;
            var corner = new Point((int)(center.X + (radius - cornerRadius) * Math.Cos(angle)), (int)(center.Y + (radius - cornerRadius) * Math.Sin(angle)));
            path.MoveTo((float)(corner.X + cornerRadius * Math.Cos(angle + theta)), (float)(corner.Y + cornerRadius * Math.Sin(angle + theta)));

            for (var i = 0; i < sides; i++)
            {
                angle += theta;
                corner = new Point((int)(center.X + (radius - cornerRadius) * Math.Cos(angle)), (int)(center.Y + (radius - cornerRadius) * Math.Sin(angle)));
                var tip = new Point((int)(center.X + radius * Math.Cos(angle)), (int)(center.Y + radius * Math.Sin(angle)));
                var start = new Point((int)(corner.X + cornerRadius * Math.Cos(angle - theta)), (int)(corner.Y + cornerRadius * Math.Sin(angle - theta)));
                var end = new Point((int)(corner.X + cornerRadius * Math.Cos(angle + theta)), (int)(corner.Y + cornerRadius * Math.Sin(angle + theta)));

                path.LineTo((float)start.X, (float)start.Y);
                path.QuadTo((float)tip.X, (float)tip.Y, (float)end.X, (float)end.Y);
            }

            path.Close();

            return path;
        }
    }
}
