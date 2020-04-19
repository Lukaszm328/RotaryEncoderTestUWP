# RotaryEncoderTestUWP
App for test rotary encoder on UWP platform.

![Freelancer Preview](https://github.com/Lukaszm328/RotaryEncoderTestUWP/blob/master/RotaryEncoderTestUWP/Assets/EncoderTest.png?raw=true)

Create new instance
```csharp
RotaryEncoder rotaryEncoder = new RotaryEncoder();
```
== Options ==
- OpenPin()
```csharp
rotaryEncoder.OpenPin(2,3,4); // 2 - A, 3 - B, 4 - Button
```

Events

RotaryEncoder_ValueChanged
```csharp
rotaryEncoder.RotaryValueChanged += RotaryEncoder_ValueChanged;
```

ButtonValueChanged
```csharp
rotaryEncoder.ButtonValueChanged += RotaryEncoder_ButtonValueChanged;
```
