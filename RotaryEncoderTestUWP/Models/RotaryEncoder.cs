using System;
using Windows.Devices.Gpio;
using static RotaryEncoderTestUWP.Models.EncoderRotaryEventArgs;

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
            encoderButton = GPIO.OpenPin(btn);
            encoderButton.ValueChanged += EncoderButton_ValueChanged;
            encoderButton.DebounceTimeout = debounceTimeout;

            encoderCLK = GPIO.OpenPin(CLK);
            encoderCLK.ValueChanged += EncoderCLK_ValueChanged;
            encoderCLK.DebounceTimeout = debounceTimeout;

            encoderDT = GPIO.OpenPin(DT);
            encoderDT.ValueChanged += EncoderDT_ValueChanged;
            encoderDT.DebounceTimeout = debounceTimeout;
        }

        private void EncoderButton_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args) => EncoderCheckStatus(sender, Mode.Button);

        private void EncoderDT_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args) => EncoderCheckStatus(sender, Mode.Encoder);

        private void EncoderCLK_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args) => EncoderCheckStatus(sender, Mode.Encoder);

        /// <summary>
        /// Event change encoder value (left,right)
        /// </summary>
        public event EventHandler<EncoderRotaryEventArgs> RotaryValueChanged;

        /// <summary>
        /// Event button 
        /// </summary>
        public event EventHandler<EncoderRotaryEventArgs> ButtonValueChanged;

        private ButtonState GetButtonState()
        {
            switch (encoderButton.Read())
            {
                case GpioPinValue.Low:
                    return ButtonState.Released;
                    break;
                case GpioPinValue.High:
                    return ButtonState.Pressed;
                    break;
                default:
                    return ButtonState.Released;
                    break;
            }
        }

        private void EncoderCheckStatus(GpioPin gpioPin, Mode mode)
        {
            EncoderRotaryEventArgs args = new EncoderRotaryEventArgs();

            args.Button = GetButtonState();
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

            switch (mode)
            {
                case Mode.Encoder:
                    EncoderValueChange(gpioPin, args);
                    break;
                case Mode.Button:
                    ButtonValueChange(gpioPin, args);
                    break;
                default:
                    EncoderValueChange(gpioPin, args);
                    break;
            }
        }

        private void EncoderValueChange(GpioPin gpioPin, EncoderRotaryEventArgs encoderEventArgs)
        {
            if (RotaryValueChanged != null)
                RotaryValueChanged.Invoke(gpioPin, encoderEventArgs);
        }

        private void ButtonValueChange(GpioPin gpioPin, EncoderRotaryEventArgs encoderEventArgs)
        {
            if (ButtonValueChanged != null)
                ButtonValueChanged.Invoke(gpioPin, encoderEventArgs);
        }
    }
}

public enum Mode
{
    Encoder,
    Button
}