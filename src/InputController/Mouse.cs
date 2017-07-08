namespace InputController
{
    public static class Mouse
    {
        public static Point GetCursorPosition()
        {
            Point position;
            SafeNativeMethods.GetCursorPos(out position);

            return position;
        }

        public static void SetCursorPosition(Point position)
        {
            SafeNativeMethods.SetCursorPos(position.X, position.Y);
        }
    }
}