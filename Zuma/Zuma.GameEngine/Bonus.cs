using System;

namespace Zuma.GameEngine
{
    public class Bonus
    {
        #region                        - Fields

        private int _value;

        #endregion


        #region                        - Properties

        public int Value
        {
            get { return _value; }
            set
            {
                if (value < 1)
                {
                    throw new InvalidOperationException("Bonus cannot be zero or negative.");
                }
                _value = value;
            }
        }
        
        public BonusState State { get; set; }

        #endregion
    }

    public enum BonusState
    {
        Disabled,
        Enabled
    }
}
