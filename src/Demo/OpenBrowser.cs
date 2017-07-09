using System;
using BehaviourTree;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;

namespace Demo
{
    public sealed class OpenBrowser : BtTree
    {
        public OpenBrowser(string instanceName,  Uri startPage)
        {
            var behaviour = new BtSequence(
                new BtNot(new IsProcessRunning(instanceName)),
                new BtSelection(
                    new OpenChrome(instanceName, startPage),
                    new OpenIE(instanceName, startPage)
                ));

            SetBehaviour(behaviour);
        }
    }
}