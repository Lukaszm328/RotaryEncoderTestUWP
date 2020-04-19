using System;

namespace RotaryEncoderTestUWP.Models
{
    public class EncoderRotaryEventArgs : EventArgs
    {
        private LastPosition position;
        public LastPosition Position 
        {
            get { return position; }  
            set { position = value; }
        }

        private ButtonState buttonState;
        public ButtonState Button
        {
            get { return buttonState; }
            set { buttonState = value; }
        }

        public enum LastPosition
        {
            Left,
            Right
        }

        public enum ButtonState
        {
            Pressed,
            Released
        }
    }
}