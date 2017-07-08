using System;

namespace InputSimulator.Native
{
    [Flags]
    internal enum KeyboardFlags
    {
        ExtendedKey = 0x0001,
        KeyUp = 0x0002,
        Unicode = 0x0004,
        ScanCode = 0x0008
    }
}