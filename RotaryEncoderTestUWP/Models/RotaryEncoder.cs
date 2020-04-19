using System;
using Windows.Devices.Gpio;

namespace RotaryEncoderTestUWP.Models
{
    class RotaryEncoder
    {
        private GpioController GPIO;
        private GpioPin encoderCLK, encoderDT, encoderButton;
        private GpioPinValue aState;
        private GpioPinValue aLastState;

        public RotaryEncoder()
        {
            GPIO = GpioController.GetDefault();

            encoderCLK.ValueChanged += EncoderCLK_ValueChanged;
            encoderCLK.DebounceTimeout = TimeSpan.FromTicks(1);
            
            encoderDT.ValueChanged += EncoderDT_ValueChanged;
            encoderDT.DebounceTimeout = TimeSpan.FromTicks(1);
        }

        /// <summary>
        /// Open pin for encoder DT, CLK and button.
        /// </summary>
        /// <param name="DT"></param>
        /// <param name="CLK"></param>
        /// <param name="btn"></param>
        public void OpenPin(int DT, int CLK, int btn)
        {
            encoderCLK = GPIO.OpenPin(CLK);
            encoderDT = GPIO.OpenPin(DT);
            encoderButton = GPIO.OpenPin(btn); 
        }

        private void EncoderDT_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args) => EncoderCheckRotary(sender);

        private void EncoderCLK_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args) => EncoderCheckRotary(sender);

        /// <summary>
        /// Event change encoder value (left,right)
        /// </summary>
        public event EventHandler<EncoderRotaryEventArgs> ValueChanged;

        private void EncoderCheckRotary(GpioPin gpioPin)
        {
            aState = encoderCLK.Read();

            if (aState != aLastState)
            {
                if (encoderDT.Read() != aState)
                {
                    EncoderValueChange(gpioPin, EncoderRotaryEventArgs.LastPosition.Left);
                }
                else
                {
                    EncoderValueChange(gpioPin, EncoderRotaryEventArgs.LastPosition.Right);
                }
            }
            aLastState = aState;
        }

        private void EncoderValueChange(GpioPin gpioPin, EncoderRotaryEventArgs.LastPosition encoderValue)
        {
            EncoderRotaryEventArgs args = new EncoderRotaryEventArgs();

            args.Position = encoderValue;
            args.Button = EncoderRotaryEventArgs.ButtonState.Released;

            ValueChanged.Invoke(gpioPin, args);
        }
    }
}