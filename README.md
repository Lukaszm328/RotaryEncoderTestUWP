# RotaryEncoderTestUWP
App for test rotary encoder on UWP platform.

Create new instance
```bash
RotaryEncoder rotaryEncoder = new RotaryEncoder();
```
== Options ==
- OpenPin()
```bash
rotaryEncoder.OpenPin(2,3,4); // 2 - A, 3 - B, 4 - Button
```

Events

RotaryEncoder_ValueChanged
```bash
rotaryEncoder.RotaryValueChanged += RotaryEncoder_ValueChanged;
```

ButtonValueChanged
```bash
rotaryEncoder.ButtonValueChanged += RotaryEncoder_ButtonValueChanged;
```
