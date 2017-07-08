using System;
using System.Runtime.InteropServices;

namespace InputController
{
    internal static class SafeNativeMethods
    {
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);
    }
}
