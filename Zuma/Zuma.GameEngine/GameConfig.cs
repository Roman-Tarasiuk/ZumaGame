using System;
using System.IO;

namespace Zuma.GameEngine
{
    public class GameConfig
    {
        #region                        - Fields

        private IField _field;
        private PointF _frogLocation;
        private PointF[] _bonusLocations;
        private IPath _path;

        #endregion


        #region                        - Properties

        public IField Field
        {
            get
            {
                this.CheckObjectIsInitialized();
                return this._field;
            }
        }

        public PointF FrogLocation
        {
            get
            {
                this.CheckObjectIsInitialized();
                return this._frogLocation;
            }
        }

        public PointF[] BonusLocations
        {
            get
            {
                this.CheckObjectIsInitialized();
                return this._bonusLocations;
            }
        }

        public IPath Path
        {
            get
            {
                this.CheckObjectIsInitialized();
                return this._path;
            }
        }

        #endregion


        #region                        - Constructors

        public GameConfig()
        {
        }

        public GameConfig(IField field, PointF frogLocatioin, PointF[] bonusLocations, IPath path)
        {
            this._field = field;
            this._frogLocation = frogLocatioin;
            this._bonusLocations = bonusLocations;
            this._path = path;
        }

        public GameConfig(string filePath)
        {
            this.Load(filePath);
        }

        #endregion


        #region                        - Methods

        private void CheckObjectIsInitialized()
        {
            if (_field == null || _path == null || _bonusLocations == null)
            {
                throw new InvalidOperationException("Object is not initialized");
            }
        }

        public void Load(string filePath)
        {
            StreamReader reader = null;
            try
            {
                // Доцільно використати конструкцію using.
                reader = new StreamReader(filePath);
                char[] coordSeparator = { ':' };
                string line;

                // Field
                //
                line = reader.ReadLine();
                // line може бути null. Тут необхідно виконати перевірку.
                if (line.StartsWith("Field: "))
                {
                    line = line.Substring("Field: ".Length);

                    // Використання var.
                    var coords = PointF.Points(line, true);
                    _field = new Field((int)coords[0].X, (int)coords[0].Y);
                }
                else
                {
                    throw new FormatException(filePath);
                }

                // Frog Location
                //
                line = reader.ReadLine();
                // line може бути null. Тут необхідно виконати перевірку.
                if (line.StartsWith("FrogLocation: "))
                {
                    line = line.Substring("FrogLocation: ".Length);
                    var coords = PointF.Points(line, true);
                    _frogLocation = coords[0];
                }
                else
                {
                    throw new FormatException(filePath);
                }

                // Bonus Locations
                //
                // line може бути null. Тут необхідно виконати перевірку.
                line = reader.ReadLine();
                if (line.StartsWith("BonusLocations: "))
                {
                    line = line.Substring("BonusLocations: ".Length);

                    _bonusLocations = PointF.Points(line, true);
                }
                else
                {
                    throw new FormatException(filePath);
                }

                // Path
                //
                // line може бути null. Тут необхідно виконати перевірку.
                line = reader.ReadLine();
                if (line.StartsWith("BezierCombinedPath: "))
                {
                    line = line.Substring("BezierCombinedPath: ".Length);

                    string[] bezCombSeparator = { "==" };
                    var bezCombParts = line.Split(bezCombSeparator, StringSplitOptions.None);

                    _path = new BezierCombinedPath(PointF.Points(bezCombParts[0], true));
                    ((BezierCombinedPath)_path).SetPointsSVG(PointF.Points(bezCombParts[1], true));
                }
                else
                {
                    throw new FormatException(filePath);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("An error while loading config from " + filePath,
                                        exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        #endregion
    }
}
