using System;

namespace InputSimulator.Native
{
    [Flags]
    internal enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }
}
