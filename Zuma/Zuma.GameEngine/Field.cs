using System;

namespace Zuma.GameEngine
{
    public class Field : IField
    {
        #region                        - IField interface members

        public Size Size { get; private set; }

        #endregion


        #region                        - Constructors

        public Field(Size size)
        {
            this.Size = size;
        }

        public Field(int width, int height)
        {
            this.Size = new Size(width, height);
        }

        #endregion
    }
}
