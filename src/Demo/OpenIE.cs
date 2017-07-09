using System;

namespace Demo
{
    // ReSharper disable once InconsistentNaming
    public sealed class OpenIE : StartProcess
    {
        public OpenIE(string instanceName, Uri url) : base(instanceName, "IExplore.exe", url.ToString())
        {
        }
    }
}