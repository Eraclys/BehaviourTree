using System;
using BehaviourTree;
using System.Diagnostics;

namespace Demo
{
    public class BtStartProcess : BaseBtBehaviour
    {
        private readonly string _instanceName;
        private readonly string _filename;
        private readonly string _arguments;
        private Process _process;

        public BtStartProcess(string instanceName, string filename, string arguments = null)
        {
            _instanceName = instanceName;
            _filename = filename;
            _arguments = arguments;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            if (_process == null || _process.HasExited)
            {
                try
                {
                    _process = Process.Start(_filename, _arguments);
                    context.Set(_instanceName, _process);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return BehaviourStatus.Failed;
                }
            }

            if (_process != null && _process.WaitForInputIdle(0))
            {
                return BehaviourStatus.Succeeded;
            }

            return BehaviourStatus.Running;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    _process?.Kill();
                    _process?.Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}