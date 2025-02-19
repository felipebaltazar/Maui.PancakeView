﻿using CoreGraphics;
using System.Runtime.InteropServices;
using UIKit;

namespace Maui.PancakeView.iOS
{
    public static class DrawingExtensions
    {
        public static UIBezierPath CreateRoundedRectPath(this CGRect rect, CornerRadius cornerRadius)
        {
            var path = new UIBezierPath();

            path.MoveTo(new CGPoint(rect.Width - cornerRadius.TopRight, rect.Y));

            path.AddArc(new CGPoint((float)rect.X + rect.Width - cornerRadius.TopRight, (float)rect.Y + cornerRadius.TopRight), (NFloat)cornerRadius.TopRight, (float)(Math.PI * 1.5), (float)Math.PI * 2, true);
            path.AddLineTo(new CGPoint(rect.Width, rect.Height - cornerRadius.BottomRight));

            path.AddArc(new CGPoint((float)rect.X + rect.Width - cornerRadius.BottomRight, (float)rect.Y + rect.Height - cornerRadius.BottomRight), (NFloat)cornerRadius.BottomRight, 0, (float)(Math.PI * .5), true);
            path.AddLineTo(new CGPoint(cornerRadius.BottomLeft, rect.Height));

            path.AddArc(new CGPoint((float)rect.X + cornerRadius.BottomLeft, (float)rect.Y + rect.Height - cornerRadius.BottomLeft), (NFloat)cornerRadius.BottomLeft, (float)(Math.PI * .5), (float)Math.PI, true);
            path.AddLineTo(new CGPoint(rect.X, cornerRadius.TopLeft));

            path.AddArc(new CGPoint((float)rect.X + cornerRadius.TopLeft, (float)rect.Y + cornerRadius.TopLeft), (NFloat)cornerRadius.TopLeft, (float)Math.PI, (float)(Math.PI * 1.5), true);

            path.ClosePath();

            return path;
        }

        public static UIBezierPath CreatePolygonPath(this CGRect rect, int sides, double cornerRadius = 0.0, double rotationOffset = 0.0)
        {
            var offsetRadians = rotationOffset * Math.PI / 180;
            var path = new UIBezierPath();
            var theta = 2 * Math.PI / sides;

            var width = (-cornerRadius + Math.Min(rect.Size.Width, rect.Size.Height)) / 2;
            var center = new CGPoint(rect.Width / 2, rect.Height / 2);

            var radius = width + cornerRadius - (Math.Cos(theta) * cornerRadius) / 2;

            var angle = offsetRadians;
            var corner = new CGPoint(center.X + (radius - cornerRadius) * Math.Cos(angle), center.Y + (radius - cornerRadius) * Math.Sin(angle));
            path.MoveTo(new CGPoint(corner.X + cornerRadius * Math.Cos(angle + theta), corner.Y + cornerRadius * Math.Sin(angle + theta)));

            for (var i = 0; i < sides; i++)
            {
                angle += theta;
                corner = new CGPoint(center.X + (radius - cornerRadius) * Math.Cos(angle), center.Y + (radius - cornerRadius) * Math.Sin(angle));
                var tip = new CGPoint(center.X + radius * Math.Cos(angle), center.Y + radius * Math.Sin(angle));
                var start = new CGPoint(corner.X + cornerRadius * Math.Cos(angle - theta), corner.Y + cornerRadius * Math.Sin(angle - theta));
                var end = new CGPoint(corner.X + cornerRadius * Math.Cos(angle + theta), corner.Y + cornerRadius * Math.Sin(angle + theta));

                path.AddLineTo(start);
                path.AddQuadCurveToPoint(end, tip);
            }

            path.ClosePath();

            return path;
        }
    }
}
