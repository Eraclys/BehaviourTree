using BehaviourTree;
using BehaviourTree.Composites;
using System;

namespace Demo
{
    public sealed class BtOpenBrowser : BtSubTree
    {
        public BtOpenBrowser(string instanceName,  Uri startPage)
        {
            var behaviour = new BtSelection(
                new BtOpenIE(instanceName, startPage),
                new BtOpenChrome(instanceName, startPage)
            );

            SetBehaviour(behaviour);
        }
    }
}