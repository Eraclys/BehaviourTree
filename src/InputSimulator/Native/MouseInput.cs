using System;

namespace InputSimulator.Native
{
    internal struct MouseInput
    {
#pragma warning disable 169
        public int X;
        public int Y;
        public uint MouseData;
        public uint Flags;
        public uint Time;
        public IntPtr ExtraInfo;
#pragma warning restore 169
    }
}
