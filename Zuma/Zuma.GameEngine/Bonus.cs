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
                /*			
                    Review VV:
                        1) слід додати фігурні дужки,
                        а валідацію взяти в регіон Validation
                        2) тут краще підійде ArgumentException
                */
                if (value < 1)
                    throw new InvalidOperationException("Bonus cannot be zero or negative.");
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
