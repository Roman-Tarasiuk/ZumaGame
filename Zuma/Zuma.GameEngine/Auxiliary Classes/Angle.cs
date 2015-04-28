using System;

// Коректний в даному випадку namespace буде Zuma.GameEngine.Auxiliary_Classes.
namespace Zuma.GameEngine
{
    public struct Angle
    {
        private double _degree;
        private double _radian;
        // Коректна назва для константи повинна бути Pi.
        private const double _PI = Math.PI;

        public double Degree
        {
            get { return _degree; }
            set
            {
                _degree = value;

                if (value > 0)
                {
                    // Відсутні фігурні дужки. Коректна реалізація нижче.
                    while (_degree > 180)
                    {
                        _degree -= 360;
                    }
                }
                else
                {
                    // Це саме зауваження.
                    while (_degree < -180)
                    {
                        _degree += 360;
                    }
                }
                // В нас є еквівалентна константа _PI, тому тут доцільніше використати її. 
                _radian = _degree * Math.PI / 180;
            }
        }

        public double Radian
        {
            get { return _radian; }
            set
            {
                _radian = value;

                if (_radian > 0)
                {
                    // Пропущено фігурні дужки.
                    while (_radian > _PI)
                    {
                        _radian -= 2*_PI;
                    }
                }
                else
                {
                    while (_radian < -_PI)
                    {
                        _radian += 2*_PI;
                    }
                }

                _degree = _radian * 180 / _PI;
            }
        }

        public Angle(int x, int y)
            : this()
        {
            Radian = Math.Atan2(y, x);
        }

        public Angle(PointF p)
            : this()
        {
            Radian = Math.Atan2(p.Y, p.X);
        }
    }
}
