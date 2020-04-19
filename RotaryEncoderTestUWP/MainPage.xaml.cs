using RotaryEncoderTestUWP.Models;
using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;
using static RotaryEncoderTestUWP.Models.EncoderRotaryEventArgs;

namespace RotaryEncoderTestUWP
{
    public sealed partial class MainPage : Page
    {
        private int leftInt = 0;
        private int rightInt = 0;

        public MainPage()
        {
            this.InitializeComponent();

            RotaryEncoder rotaryEncoder = new RotaryEncoder();
            rotaryEncoder.OpenPin(18, 25, 10);
            rotaryEncoder.RotaryValueChanged += RotaryEncoder_ValueChanged;
            rotaryEncoder.ButtonValueChanged += RotaryEncoder_ButtonValueChanged;
        }

        private void RotaryEncoder_ButtonValueChanged(object sender, EncoderRotaryEventArgs e)
        {
            switch (e.Button)
            {
                case ButtonState.Pressed:
                    ButtonPressed();
                    break;
                case ButtonState.Released:
                    ButtonRelased();
                    break;
                default:
                    break;
            }
        }

        private void ButtonRelased()
        {
            throw new NotImplementedException();
        }

        private void ButtonPressed()
        {
            throw new NotImplementedException();
        }

        private void RotaryEncoder_ValueChanged(object sender, EncoderRotaryEventArgs e)
        {
            switch (e.Position)
            {
                case LastPosition.Left:
                    EncoderTurnLeft();
                    break;
                case LastPosition.Right:
                    EncoderTurnRight();
                    break;
                default:
                    break;
            }
        }

        private void EncoderTurnLeft()
        {
            leftInt++;
        }

        private void EncoderTurnRight()
        {
            rightInt++;
        }

        private void exitApp_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}