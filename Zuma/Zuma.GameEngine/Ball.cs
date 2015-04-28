using System;

namespace Zuma.GameEngine
{
    public class Ball
    {
        #region                        - Fields

        private int _pathIndex = -1;

        #endregion


        #region                        - Properties

        public PointF Centre { get; set; }
        public float Radius { get; set; }

        public BallType Type { get; set; }
        public BallBonus Bonus { get; set; }
        public BallColor Color { get; set; }

        #endregion


        #region                        - Constructors
        // Координати варто передавати через конструктор з параметрами.
        // В такому випадку властивість Centre можна зробити обмеженою на запис.
        public Ball()
        {
            // Використовувати this для підвищення читабельності коду.
            this.Centre = new PointF(-1, -1);
        }

        #endregion


        #region                        - Public Methods

        public void Move(IPath path, MovingDirection direction)
        {
            if (path == null)
                throw new NullReferenceException("Path not initialized. Use 'SetPath' method.");

            if (direction == MovingDirection.Forward)
            {
                // Використовувати this для підвищення читабельності коду.
                if (this._pathIndex < path.Points.Length - 1)
                {
                    this.Centre = path.Points[++this._pathIndex];
                }
            }
            else
            {
                if (this._pathIndex > 0)
                {
                    this.Centre = path.Points[--this._pathIndex];
                }
            }
        }

        public float Distance(Ball ball)
        {
            return (float)Math.Sqrt(Math.Pow(Centre.X - ball.Centre.X, 2)
                                    + Math.Pow(Centre.Y - ball.Centre.Y, 2));
        }

        public float Distance(PointF point)
        {
            return (float)Math.Sqrt(Math.Pow(Centre.X - point.X, 2)
                                    + Math.Pow(Centre.Y - point.Y, 2));
        }

        public bool Intersects(Ball ball)
        {
            return this.Distance(ball) <= Radius + ball.Radius;
        }

        #endregion
    }


    #region                        - Ball Characters

    public enum BallType
    {
        Normal,
        Shooting,
        Bonus
    }

    public enum BallBonus
    {
        None,
        Bomb,
        MoveSlow,
        MoveBack
    }

    public enum BallColor
    {
        Red,
        Green,
        Blue,
        Yellow,
        Plum,
        Silver
    }

    #endregion
}
