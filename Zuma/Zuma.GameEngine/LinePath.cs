using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuma.GameEngine
{
    public class LinePath : IPath
    {
        #region                        - Fields

        private PointF[] _points;

        #endregion


        #region                        - IPath interface members

        // Варто використати auto-property.
        // public PointF[] Points { get; private set; }
        public PointF[] Points
        {
            get { return _points; }
        }

        #endregion


        #region                        - Constructors

        public LinePath(IField field, PointF p1, PointF p2)
        {
            this.CalculatePath(field, p1, p2);
        }

        #endregion


        #region                        - Helper Methods

        private void CalculatePath(IField field, PointF p1, PointF p2)
        {
            if (Math.Abs(p2.X - p1.X) > Math.Abs(p2.Y - p1.Y))
            {
                // Доцільніше використовувати var.
                var xE = this.EdgeX(field, p1, p2);
                var length = (int)Math.Abs(p1.X - xE);
                _points = new PointF[length];

                if (xE < p1.X)
                {
                    for (var x = p1.X; x > xE; x -= 1F)
                    {
                        _points[(int)(p1.X - x)].X = x;
                        _points[(int)(p1.X - x)].Y = this.LineEquationY(p1, p2, x);
                    }
                }
                else
                {
                    for (var x = p1.X; x < xE; x += 1F)
                    {
                        _points[(int)(x - p1.X)].X = x;
                        _points[(int)(x - p1.X)].Y = this.LineEquationY(p1, p2, x);
                    }
                }
            }
            else
            {
                var yE = this.EdgeY(field, p1, p2);
                var length = (int)Math.Abs(p1.Y - yE);
                this._points = new PointF[length];

                if (yE < p1.Y)
                {
                    for (var y = p1.Y; y > yE; y -= 1F)
                    {
                        this._points[(int)(p1.Y - y)].Y = y;
                        this._points[(int)(p1.Y - y)].X = this.LineEquationX(p1, p2, y);
                    }
                }
                else
                {
                    for (var y = p1.Y; y < yE; y += 1F)
                    {
                        this._points[(int)(y - p1.Y)].Y = y;
                        this._points[(int)(y - p1.Y)].X = this.LineEquationX(p1, p2, y);
                    }
                }
            }
        }

        //private float EdgeY(IField field, PointF p1, PointF p2)
        //{
        //    if (p1.Y < p2.Y)
        //        return field.Size.Height - 1;
        //    else
        //        return 0;
        //}

        //private float EdgeX(IField field, PointF p1, PointF p2)
        //{
        //    if (p1.X < p2.X)
        //        return field.Size.Width - 1;
        //    else
        //        return 0;
        //}

        private float EdgeY(IField field, PointF p1, PointF p2)
        {
            if (p1.Y < p2.Y)
            {
                if (p1.X == p2.X)
                {
                    return field.Size.Height - 1;
                }

                float Y;
                if (p1.X < p2.X)
                {
                    Y = LineEquationY(p1, p2, field.Size.Width - 1);
                }
                else
                {
                    Y = LineEquationY(p1, p2, 0);
                }

                return Math.Min(Y, field.Size.Height - 1);
            }
            else
            {
                if (p1.X == p2.X)
                    return 0;

                float Y;
                if (p1.X < p2.X)
                    Y = LineEquationY(p1, p2, field.Size.Width - 1);
                else
                    Y = LineEquationY(p1, p2, 0);

                return Math.Max(Y, 0);
            }
        }

        private float EdgeX(IField field, PointF p1, PointF p2)
        {
            if (p1.X < p2.X)
            {
                if (p1.Y == p2.Y)
                    return field.Size.Width - 1;

                float X;
                if (p1.Y < p2.Y)
                    X = LineEquationX(p1, p2, field.Size.Height - 1);
                else
                    X = LineEquationX(p1, p2, 0);

                return Math.Min(X, field.Size.Width - 1);
            }
            else
            {
                if (p1.Y == p2.Y)
                    return 0;

                float X;
                if (p1.Y < p2.Y)
                    X = LineEquationX(p1, p2, field.Size.Height - 1);
                else
                    X = LineEquationX(p1, p2, 0);

                return Math.Max(X, 0);
            }
        }

        private float LineEquationY(PointF p1, PointF p2, float x)
        {
            return (p2.Y - p1.Y) * (x - p1.X) / (p2.X - p1.X) + p1.Y;
        }

        private float LineEquationX(PointF p1, PointF p2, float y)
        {
            return (p2.X - p1.X) * (y - p1.Y) / (p2.Y - p1.Y) + p1.X;
        }

        #endregion
    }
}
