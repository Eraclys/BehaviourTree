using InputSimulator.Native;

namespace InputSimulator
{
    internal sealed class MouseSimulator : IMouseSimulator
    {
        public IMouseSimulator LeftDown(Point location) => MouseEvent(MouseEventFlags.LeftDown, location);

        public IMouseSimulator LeftUp(Point location) => MouseEvent(MouseEventFlags.LeftUp, location);

        public IMouseSimulator RightDown(Point location) => MouseEvent(MouseEventFlags.RightDown, location);

        public IMouseSimulator RightUp(Point location) => MouseEvent(MouseEventFlags.RightUp, location);

        public IMouseSimulator MiddleDown(Point location) => MouseEvent(MouseEventFlags.MiddleDown, location);

        public IMouseSimulator MiddleUp(Point location) => MouseEvent(MouseEventFlags.MiddleUp, location);

        public IMouseSimulator LeftClick(Point location) => MouseEvent(MouseEventFlags.LeftUp | MouseEventFlags.LeftDown, location);

        public IMouseSimulator LeftClick() => LeftClick(GetCursorPosition());

        public IMouseSimulator LeftDown() => LeftDown(GetCursorPosition());

        public IMouseSimulator LeftUp() => LeftUp(GetCursorPosition());

        public IMouseSimulator MiddleDown() => MiddleDown(GetCursorPosition());

        public IMouseSimulator MiddleUp() => MiddleUp(GetCursorPosition());

        public IMouseSimulator RightDown() => RightDown(GetCursorPosition());

        public IMouseSimulator RightUp() => RightUp(GetCursorPosition());

        public IMouseSimulator SetCursorPosition(Point position)
        {
            SafeNativeMethods.SetCursorPos(position.X, position.Y);
            return this;
        }

        public Point GetCursorPosition()
        {
            Point position;
            SafeNativeMethods.GetCursorPos(out position);

            return position;
        }

        private IMouseSimulator MouseEvent(MouseEventFlags flags, Point position)
        {
            SetCursorPosition(position);
            SafeNativeMethods.mouse_event(
                (int)flags,
                position.X,
                position.Y,
                0,
                0);

            return this;
        }
    }
}
