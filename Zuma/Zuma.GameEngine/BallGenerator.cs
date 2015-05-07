using System;

namespace Zuma.GameEngine
{
    /*			
		Review VV:
		    рекомендую генератор реалізувати як нестатичний клас з нестатичними функціями	
	*/
    static class BallGenerator
    {
        #region                        - Class members

        private static int _colorsCount;
        private static BallColor[] _colors;

        #endregion


        #region                        - Methods

        public static Ball NewBall()
        {
            Random rnd = new Random();
            return new Ball() { Type = BallType.Normal, Color = _colors[rnd.Next(_colorsCount)] };
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
