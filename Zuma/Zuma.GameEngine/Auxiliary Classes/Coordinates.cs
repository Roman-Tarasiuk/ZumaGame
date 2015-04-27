using System.Text.RegularExpressions;

namespace Zuma.GameEngine
{
    public struct PointF
    {
        public float X { get; set; }
        public float Y { get; set; }

        public PointF(float x, float y)
            : this()
        {
            X = x;
            Y = y;
        }

        public static PointF Add(PointF p1, PointF p2)
        {
            return new PointF(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static PointF Add(PointF p, SizeF size)
        {
            return new PointF(p.X + size.Width, p.Y + size.Height);
        }

        public static PointF operator +(PointF p1, PointF p2)
        {
            return Add(p1, p2);
        }

        public static PointF operator +(PointF p, SizeF size)
        {
            return Add(p, size);
        }

        public static PointF[] Points(string str, bool svgOfiginalCulture)
        {
            if (svgOfiginalCulture)
            {
                str = str.Replace(",", ":");
                str = str.Replace(".", ",");
            }

            string matchFloat = @"-?\d+,?\d*";

            MatchCollection matches = Regex.Matches(str, matchFloat);

            PointF[] result = new PointF[matches.Count / 2];

            for (int i = 0; i < result.Length; i++)
            {
                result[i].X = float.Parse(matches[i * 2].Value);
                result[i].Y = float.Parse(matches[i * 2 + 1].Value);
            }

            return result;
        }

    }

    public struct Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size(int width, int height)
            : this()
        {
            Width = width;
            Height = height;
        }
    }

    public struct SizeF
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public SizeF(float width, float height)
            : this()
        {
            Width = width;
            Height = height;
        }
    }

    public static class PointFEx
    {
        public static System.Drawing.PointF Multiply(this System.Drawing.PointF p, float k)
        {
            return new System.Drawing.PointF(p.X * k, p.Y * k);
        }

        public static System.Drawing.PointF Multiply(this System.Drawing.PointF p, double k)
        {
            return Multiply(p, (float)k);
        }

        public static System.Drawing.PointF Add(this System.Drawing.PointF p, System.Drawing.PointF p2)
        {
            return new System.Drawing.PointF(p.X + p2.X, p.Y + p2.Y);
        }
    }
}
