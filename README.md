# RotaryEncoderTestUWP
App for test rotary encoder on UWP platform with GPIO.

![Freelancer Preview](https://github.com/Lukaszm328/RotaryEncoderTestUWP/blob/master/RotaryEncoderTestUWP/Assets/EncoderTest.png?raw=true)

## Create new instance
```csharp
RotaryEncoder rotaryEncoder = new RotaryEncoder();
```
Or
```csharp
RotaryEncoder rotaryEncoder = new RotaryEncoder(gpioController);
```
## Properties
```csharp
rotaryEncoder.DebounceTime = TimeSpan.FromTicks(1); // Every 1 tick refresh ststus
```

## Methods
- OpenPin()
```csharp
rotaryEncoder.OpenPin(2,3,4); // 2 - A, 3 - B, 4 - Button
```

## Events
- RotaryValueChanged
```csharp
rotaryEncoder.RotaryValueChanged += RotaryEncoder_ValueChanged;
```

- ButtonValueChanged
```csharp
rotaryEncoder.ButtonValueChanged += RotaryEncoder_ButtonValueChanged;
```
## Enums
```csharp
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
        
        public enum Mode
        {
            Encoder,
            Button
        }
```

## Example
```csharp
       public MainPage()
        {
            this.InitializeComponent();

            RotaryEncoder rotaryEncoder = new RotaryEncoder();
            rotaryEncoder.DebounceTime = TimeSpan.FromTicks(4);
            rotaryEncoder.OpenPin(18, 25, 10); // Encoder pins
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
           // Button relased
        }

        private void ButtonPressed()
        {
           // Button pressed
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
            // Rotary left
        }

        private void EncoderTurnRight()
        {
            // Rotary right
        }
```
