using System;

namespace Zuma.GameEngine
{
    public struct Angle
    {
        private double _degree;
        private double _radian;

        private const double _PI = Math.PI;

        public double Degree
        {
            get { return _degree; }
            set
            {
                _degree = value;

                if (value > 0)
                {
                    while (_degree > 180)
                        _degree -= 360;
                }
                else
                {
                    while (_degree < -180)
                        _degree += 360;
                }

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
                    while (_radian > _PI)
                        _radian -= 2 * _PI;
                }
                else
                {
                    while (_radian < -_PI)
                        _radian += 2 * _PI;
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
