using System;
using Windows.Devices.Gpio;

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

        private GpioPinValue valueDT;
        public GpioPinValue ValueDT
        {
            get { return valueDT; }
            set { valueDT = value; }
        }

        private GpioPinValue valueCLK;

        public GpioPinValue ValueCLK
        {
            get { return valueCLK; }
            set { valueCLK = value; }
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