using System;

namespace Demo
{
    public sealed class BtOpenChrome : BtStartProcess
    {
        public BtOpenChrome(string instanceName, Uri url)
            : base(instanceName, "chrome.exe", $"{url} --user-data-dir=\"%temp%/random_name\"")
        {
        }
    }
}