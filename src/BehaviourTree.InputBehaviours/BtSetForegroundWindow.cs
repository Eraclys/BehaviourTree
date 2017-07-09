using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BehaviourTree.InputBehaviours
{
    public sealed class BtSetForegroundWindow : BaseBtBehaviour
    {
        private readonly string _instanceName;

        public BtSetForegroundWindow(string instanceName)
        {
            _instanceName = instanceName;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            var process = context.Get<Process>(_instanceName);

            if (process == null || process.HasExited)
            {
                return BehaviourStatus.Failed;
            }

            var activeProcessId = GetActiveProcessId();

            if (process.Id != activeProcessId)
            {
                SetForegroundWindow(process.MainWindowHandle);
            }

            return BehaviourStatus.Succeeded;
        }

        private static int GetActiveProcessId()
        {
            int activeProcId;
            var activatedHandle = GetForegroundWindow();
            GetWindowThreadProcessId(activatedHandle, out activeProcId);
            return activeProcId;
        }


        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
    }
}