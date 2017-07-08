namespace InputSimulator
{
    public static class Input
    {
        public static readonly IMouseSimulator Mouse = new MouseSimulator();
        public static readonly IKeyboardSimulator Keyboard = new KeyboardSimulator();
    }
}
