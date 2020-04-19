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
        private TimeSpan debounceTimeout;

        public RotaryEncoder()
        {
            GPIO = GpioController.GetDefault();

            if (GPIO == null)
                return;

            debounceTimeout = TimeSpan.FromTicks(1);
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

            encoderCLK.ValueChanged += EncoderCLK_ValueChanged;
            encoderCLK.DebounceTimeout = debounceTimeout;

            encoderDT.ValueChanged += EncoderDT_ValueChanged;
            encoderDT.DebounceTimeout = debounceTimeout;
        }

        private void EncoderDT_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args) => EncoderCheckRotary(sender);

        private void EncoderCLK_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args) => EncoderCheckRotary(sender);

        /// <summary>
        /// Event change encoder value (left,right)
        /// </summary>
        public event EventHandler<EncoderRotaryEventArgs> ValueChanged;

        private void EncoderCheckRotary(GpioPin gpioPin)
        {
            EncoderRotaryEventArgs args = new EncoderRotaryEventArgs();

            args.Button = EncoderRotaryEventArgs.ButtonState.Released;
            args.ValueCLK = encoderCLK.Read();
            args.ValueDT = encoderDT.Read();

            aState = encoderCLK.Read();

            if (aState != aLastState)
            {
                if (encoderDT.Read() != aState)
                    args.Position = EncoderRotaryEventArgs.LastPosition.Left;
                else
                    args.Position = EncoderRotaryEventArgs.LastPosition.Right;
            }
            aLastState = aState;

            EncoderValueChange(gpioPin, args);
        }

        private void EncoderValueChange(GpioPin gpioPin, EncoderRotaryEventArgs encoderEventArgs)
        {
            if (ValueChanged != null)
                ValueChanged.Invoke(gpioPin, encoderEventArgs);
        }
    }
}