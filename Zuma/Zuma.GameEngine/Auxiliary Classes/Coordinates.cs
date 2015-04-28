using System.Text.RegularExpressions;

// Коректний в даному випадку namespace буде Zuma.GameEngine.Auxiliary_Classes.
namespace Zuma.GameEngine
{
    public struct PointF
    {
        public float X { get; set; }
        public float Y { get; set; }

        public PointF(float x, float y)
            : this()
        {
            // Для підвищення читабельності коду, варто використовувати this.
            this.X = x;
            this.Y = y;
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
            // Зайві пропуски.
            MatchCollection matches = Regex.Matches(str, matchFloat);
            // Варто використати var.
            var result = new PointF[matches.Count / 2];
            for (var i = 0; i < result.Length; i++)
            {
                // Варто додати перевірку конвертування, використавши TryParse.
                result[i].X = float.Parse(matches[i * 2].Value);
                result[i].Y = float.Parse(matches[i * 2 + 1].Value);
            }

            return result;
        }
    }

    // Для структур Size та SizeF доцільно використати одну шаблонну структуру.
    // public struct Size<T>
    // {
    //     public T Width { get; set; }
    //     public T Height { get; set; }

    //     public Size(T width, T height)
    //         : this()
    //     {
    //         Width = width;
    //         Height = height;
    //     }
    // }
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
        // Варто задуматись між створенням власної структури Point та використанням System.Drawing.PointF.
        // Використання System.Drawing.PointF вимагатиме наявності в користувача бібліотеки System.Drawing.
        // Якщо вже використовувати System.Drawing.PointF, то варто додати простір імен System.Drawing через using.
        public static System.Drawing.PointF Multiply(this System.Drawing.PointF p, float k)
        {
            return new System.Drawing.PointF(p.X * k, p.Y * k);
        }
        // Можна залишити лише дану реалізацію методу Multiply(this System.Drawing.PointF p, double k).
        // float завжди приведеться до double.
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
