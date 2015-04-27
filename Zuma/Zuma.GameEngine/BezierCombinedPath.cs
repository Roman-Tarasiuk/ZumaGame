using System;

namespace Zuma.GameEngine
{
    public class BezierCombinedPath : IPath
    {
        #region                        - Fields

        private PointF[] _points;
        private PointF[] _pointsSVG;

        #endregion


        #region                        - IPath interface members

        public PointF[] Points
        {
            get
            {
                if (_pointsSVG == null)
                    throw new InvalidOperationException("Object is not initialized");
                return _points;
            }
        }

        #endregion


        #region                        - Members

        public PointF[] PointsSVG
        {
            get
            {
                if (_pointsSVG == null)
                    throw new InvalidOperationException("Object is not initialized");
                return _pointsSVG;
            }
        }

        #endregion


        #region                        - Constructors

        public BezierCombinedPath(params PointF[] points)
        {
            _points = points;
            _pointsSVG = null;
        }

        #endregion


        #region                        - Methods

        public void SetPointsSVG(params PointF[] points)
        {
            _pointsSVG = points;
        }

        #endregion
    }
}