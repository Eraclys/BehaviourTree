namespace InputSimulator
{
    public interface IKeyboardSimulator
    {
        IKeyboardSimulator KeyPress(params Key[] keys);
        IKeyboardSimulator KeyUp(params Key[] keys);
        IKeyboardSimulator KeyDown(params Key[] keys);
        IKeyboardSimulator SendKeys(string value);
    }
}
