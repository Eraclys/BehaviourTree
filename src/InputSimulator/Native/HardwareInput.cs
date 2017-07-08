namespace InputSimulator.Native
{
    internal struct HardwareInput
    {
#pragma warning disable 169
        public uint Msg;
        public ushort ParamL;
        public ushort ParamH;
#pragma warning restore 169
    }
}