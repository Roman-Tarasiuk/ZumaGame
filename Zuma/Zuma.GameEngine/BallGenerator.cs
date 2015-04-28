using System;

namespace Zuma.GameEngine
{
    // Варто явно вказувати модифікатор доступу.
    internal static class BallGenerator
    {
        #region                        - Class members

        // Дані поля можна зробити доступними лише для читання.
        // Відповідно потрібно змінити імена на ColorsCount та Colors
        private static readonly int _colorsCount;
        private static readonly BallColor[] _colors;

        #endregion


        #region                        - Methods

        public static Ball NewBall()
        {
            // Доцільно використати var.
            var rnd = new Random();
            // Занадто довга конструкція.
            var ball = new Ball()
            {
                Type = BallType.Normal,
                Color = _colors[rnd.Next(_colorsCount)]
            };
            return ball;
        }
        
        #endregion


        #region                        - Class (static) constructor

        static BallGenerator()
        {
            _colorsCount = Enum.GetValues(typeof(BallColor)).Length;
            _colors = (BallColor[])Enum.GetValues(typeof(BallColor));
        }
        
        #endregion
    }
}
