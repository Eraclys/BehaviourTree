namespace InputSimulator
{
    public interface IMouseSimulator
    {
        Point GetCursorPosition();
        IMouseSimulator LeftClick();
        IMouseSimulator LeftClick(Point location);
        IMouseSimulator LeftDown();
        IMouseSimulator LeftDown(Point location);
        IMouseSimulator LeftUp();
        IMouseSimulator LeftUp(Point location);
        IMouseSimulator MiddleDown();
        IMouseSimulator MiddleDown(Point location);
        IMouseSimulator MiddleUp();
        IMouseSimulator MiddleUp(Point location);
        IMouseSimulator RightDown();
        IMouseSimulator RightDown(Point location);
        IMouseSimulator RightUp();
        IMouseSimulator RightUp(Point location);
        IMouseSimulator SetCursorPosition(Point position);
    }
}