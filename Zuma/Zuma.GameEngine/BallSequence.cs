using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuma.GameEngine
{
    public class BallSequence
    {
        #region                        - Fields

        private bool _hasHoles = false;
        private PointF _firstMovingBall;
        
        #endregion


        #region                        - Properties

        public List<Ball> Balls { get; private set; }

        #endregion


        #region                        - Constructors

        public BallSequence()
        {
            Balls = new List<Ball>();
        }

        #endregion


        #region                        - Public Methods

        public void Move()
        {
            if (Balls.Count == 0)
                throw new InvalidOperationException("No balls in sequence.");


        }

        #endregion
    }
}
