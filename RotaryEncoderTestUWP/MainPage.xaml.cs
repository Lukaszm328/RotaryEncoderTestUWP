using RotaryEncoderTestUWP.Models;
using System;
using System.Diagnostics;
using Windows.Devices.Gpio;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace RotaryEncoderTestUWP
{
    public sealed partial class MainPage : Page
    {
        //private GpioController GPIO;
        //private GpioPin encoderCLK;
        //private GpioPin encoderDT;
        //private int sequenceCLK = 0;
        //private int sequenceDT = 0;
        private int leftInt = 0;
        private int rightInt = 0;
        //GpioPinValue[] state;
        //GpioPinValue[] lastState;
        //GpioPinValue aLastState;
        //GpioPinValue aState;
        public MainPage()
        {
            this.InitializeComponent();

            RotaryEncoder rotaryEncoder = new RotaryEncoder();
            rotaryEncoder.OpenPin(18, 25, 10);
            rotaryEncoder.ValueChanged += RotaryEncoder_ValueChanged;

            //GPIO = GpioController.GetDefault();

            //if (GPIO == null)
            //{
            //    return;
            //}

            //TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 0);

            //encoderCLK = GPIO.OpenPin(18); // (int.Parse(clkPin.SelectedValue.ToString()));
            //encoderCLK.DebounceTimeout = timeSpan;
            //encoderCLK.ValueChanged += EncoderCLK_ValueChanged;
            //encoderDT = GPIO.OpenPin(25); //(int.Parse(dtPin.SelectedValue.ToString()));
            //encoderDT.DebounceTimeout = TimeSpan.FromTicks(1);
            //encoderDT.ValueChanged += EncoderDT_ValueChanged;

            //aLastState = encoderCLK.Read();

            //state = new GpioPinValue[] { encoderCLK.Read(), encoderDT.Read() };
            //lastState = new GpioPinValue[] { state[0], state[1] };
        }

        private void RotaryEncoder_ValueChanged(object sender, EncoderRotaryEventArgs e)
        {
            switch (e.Position)
            {
                case EncoderRotaryEventArgs.LastPosition.Left:
                    leftInt++;
                    clk.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    break;
                case EncoderRotaryEventArgs.LastPosition.Right:
                    rightInt++;
                    break;
                default:
                    break;
            }
        }

        //private void EncoderDT_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        //{
        //    RotaryEncoder();
        //    //ValueChange();
        //    var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //    {
        //        sequenceDT++;
        //        if (sender.Read() == GpioPinValue.High)
        //            clk.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
        //        else
        //            clk.Fill = new SolidColorBrush(Windows.UI.Colors.Wheat);
        //        textSequenceDT.Text = $"Sequence: {sequenceDT.ToString()}";
        //    });
        //}

        //private void EncoderCLK_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        //{
        //    RotaryEncoder();
        //    //ValueChange();
        //    var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //    {
        //        sequenceCLK++;
        //        if (sender.Read() == GpioPinValue.High)
        //            dt.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
        //        else
        //            dt.Fill = new SolidColorBrush(Windows.UI.Colors.Wheat);
        //        textSequenceCLK.Text = $"Sequence: {sequenceCLK.ToString()}";
        //    });
        //}
      
        //// FIRST OPTION
        //private void ValueChange()
        //{
        //    lastState[0] = state[0];
        //    lastState[1] = state[1];
        //    state[0] = encoderCLK.Read();
        //    state[1] = encoderDT.Read();
        //    CheckTurn();
        //}
        
        //int skipInt = 0;
        //Models.EncoderRotaryEventArgs.LastPosition lastSkipedPosition;
        
        //private void CheckTurn()
        //{
        //    if (lastState[0] == GpioPinValue.Low && lastState[1] == GpioPinValue.Low)
        //        if (state[0] == GpioPinValue.Low && state[1] == GpioPinValue.High)
        //        {
        //            lastSkipedPosition = Models.EncoderRotaryEventArgs.LastPosition.Right;
        //            rightInt++;
        //            TurnRight();
        //        }
        //        else if (state[0] == GpioPinValue.High && state[1] == GpioPinValue.Low)
        //        {
        //            leftInt++;
        //            lastSkipedPosition = Models.EncoderRotaryEventArgs.LastPosition.Left;
        //            TurnLeft();
        //        }
        //        else
        //        {
        //            RotaryFromLastPosition();
        //            //Debug.WriteLine($"Pominal : {skipInt++}");
        //        }
           
        //    if (lastState[0] == 0 && lastState[1] == GpioPinValue.High)
        //        if (state[0] == GpioPinValue.High && state[1] == GpioPinValue.High)
        //        {
        //            lastSkipedPosition = Models.EncoderRotaryEventArgs.LastPosition.Right;
        //            rightInt++;
        //            TurnRight();
        //        }
        //        else if (state[0] == GpioPinValue.Low && state[1] == GpioPinValue.Low)
        //        {
        //            leftInt++;
        //            lastSkipedPosition = Models.EncoderRotaryEventArgs.LastPosition.Left;
        //            TurnLeft();
        //        }
        //        else
        //        {
        //            RotaryFromLastPosition();
        //            //Debug.WriteLine($"Pominal : {skipInt++}");
        //        }

        //    if (lastState[0] == GpioPinValue.High && lastState[1] == GpioPinValue.High)
        //        if (state[0] == GpioPinValue.High && state[1] == GpioPinValue.Low)
        //        {
        //            lastSkipedPosition = Models.EncoderRotaryEventArgs.LastPosition.Right;
        //            rightInt++;
        //            TurnRight();
        //        }
        //        else if (state[0] == GpioPinValue.Low && state[1] == GpioPinValue.High)
        //        {
        //            lastSkipedPosition = Models.EncoderRotaryEventArgs.LastPosition.Left;
        //            leftInt++;
        //            TurnLeft();
        //        }
        //        else
        //        {
        //            RotaryFromLastPosition();
        //            //Debug.WriteLine($"Pominal : {skipInt++}");
        //        }

        //    if (lastState[0] == GpioPinValue.High && lastState[1] == GpioPinValue.Low)
        //        if (state[0] == GpioPinValue.Low && state[1] == GpioPinValue.Low)
        //        {
        //            lastSkipedPosition = Models.EncoderRotaryEventArgs.LastPosition.Right;
        //            rightInt++;
        //            TurnRight();
        //        }
        //        else if (state[0] == GpioPinValue.High && state[1] == GpioPinValue.High)
        //        {
        //            lastSkipedPosition = Models.EncoderRotaryEventArgs.LastPosition.Left;
        //            leftInt++;
        //            TurnLeft();
        //        }
        //        else
        //        {
        //            RotaryFromLastPosition();
        //            //Debug.WriteLine($"Pominal : {skipInt++}");
        //        }
        //}

        //private void RotaryFromLastPosition()
        //{
        //    switch (lastSkipedPosition)
        //    {
        //        case Models.EncoderRotaryEventArgs.LastPosition.Left:
        //            TurnLeft();
        //            skipInt++;
        //            break;
        //        case Models.EncoderRotaryEventArgs.LastPosition.Right:
        //            TurnRight();
        //            skipInt++;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //public void TurnLeft()
        //{
        //    var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //    {
        //        left.Text = "Left : " + leftInt;
        //    });
        //}

        //public void TurnRight()
        //{
        //    var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //   {
        //       right.Text = "Right : " + rightInt;
        //   });
        //}

        //// SECOND OPTION
        //public void RotaryEncoder()
        //{
        //    aState = encoderCLK.Read();

        //    if (aState != aLastState)
        //    {
        //        if (encoderDT.Read() != aState)
        //        {
        //            leftInt++;
        //            TurnLeft();
        //        }
        //        else
        //        {
        //            rightInt++;
        //            TurnRight();
        //        }
        //    }
        //    aLastState = aState;

        //}
    }

}