using RotaryEncoderTestUWP.Models;
using Windows.UI.Xaml.Controls;

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
            rotaryEncoder.ValueChanged += RotaryEncoder_ValueChanged;
        }

        private void RotaryEncoder_ValueChanged(object sender, EncoderRotaryEventArgs e)
        {
            switch (e.Position)
            {
                case EncoderRotaryEventArgs.LastPosition.Left:
                    EncoderTurnLeft();
                    break;
                case EncoderRotaryEventArgs.LastPosition.Right:
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
    }
}