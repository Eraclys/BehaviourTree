using System;

namespace Demo
{
    public sealed class OpenChrome : StartProcess
    {
        public OpenChrome(string instanceName, Uri url)
            : base(instanceName, "chrome.exe", $"{url} --user-data-dir=\"%temp%/random_name\"")
        {
        }
    }
}