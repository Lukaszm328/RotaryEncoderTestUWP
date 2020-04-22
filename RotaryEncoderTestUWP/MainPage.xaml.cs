using RotaryEncoderTestUWP.Models;
using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using static RotaryEncoderTestUWP.Models.EncoderRotaryEventArgs;

namespace RotaryEncoderTestUWP
{
    public sealed partial class MainPage : Page
    {
        private int leftInt = 0, rightInt = 0, sequence = 0, sequenceDT = 0, sequenceCLK = 0;

        public MainPage()
        {
            this.InitializeComponent();

            RotaryEncoder rotaryEncoder = new RotaryEncoder();
            rotaryEncoder.DebounceTime = TimeSpan.FromTicks(1);
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
            btn.Fill = new SolidColorBrush(Windows.UI.Colors.Wheat);
        }

        private void ButtonPressed()
        {
            btn.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
        }

        private void RotaryEncoder_ValueChanged(object sender, EncoderRotaryEventArgs e)
        {
            AddSequence(e);

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

        private void AddSequence(EncoderRotaryEventArgs e)
        {
            sequence++;
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                sequenceText.Text = $"Sequence: {sequence}";

                if (e.ValueCLK == Windows.Devices.Gpio.GpioPinValue.High)
                    clk.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                else
                   clk.Fill = new SolidColorBrush(Windows.UI.Colors.Wheat);

                if (e.ValueDT == Windows.Devices.Gpio.GpioPinValue.High)
                    dt.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                else
                    dt.Fill = new SolidColorBrush(Windows.UI.Colors.Wheat);
            });
        }

        private void EncoderTurnLeft()
        {
            leftInt++;
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                left.Text = "left : " + leftInt;
            });

        }

        private void EncoderTurnRight()
        {
            rightInt++;

            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                right.Text = "left : " + rightInt;
            });
        }

        private void exitApp_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}