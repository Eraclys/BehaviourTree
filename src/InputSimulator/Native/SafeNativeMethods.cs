using System;
using System.Runtime.InteropServices;

namespace InputSimulator.Native
{
    internal static class SafeNativeMethods
    {
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 SendInput(UInt32 numberOfInputs, Input[] inputs, Int32 sizeOfInputStructure);
    }
}
