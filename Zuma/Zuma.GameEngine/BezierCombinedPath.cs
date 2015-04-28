using System;

namespace Zuma.GameEngine
{
    public class BezierCombinedPath : IPath
    {
        #region                        - Fields

        // Дане поле варто зробити доступним для читання.
        private readonly PointF[] _points;
        // Варто змінити ім'я на _pointsSvg.
        private PointF[] _pointsSVG;

        #endregion


        #region                        - IPath interface members

        public PointF[] Points
        {
            get
            {
                // Пропущено дужки.
                if (_pointsSVG == null)
                {
                    throw new InvalidOperationException("Object is not initialized");
                }
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
                {
                    throw new InvalidOperationException("Object is not initialized");
                }
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

        // Назву методу варто змінити на SetPointsSvg.
        public void SetPointsSVG(params PointF[] points)
        {
            _pointsSVG = points;
        }

        #endregion
    }
}