using System;

namespace Demo
{
    // ReSharper disable once InconsistentNaming
    public sealed class BtOpenIE : BtStartProcess
    {
        public BtOpenIE(string instanceName, Uri url) : base(instanceName, "IExplore.exe", url.ToString())
        {
        }
    }
}