using BehaviourTree;
using BehaviourTree.Composites;
using BehaviourTree.InputBehaviours;
using InputSimulator;
using System;
using BehaviourTree.Decorators;

namespace Demo
{
    public sealed class BtGoogleSearch : BtSubTree
    {
        public BtGoogleSearch(string search)
        {
            var behaviour =
                new BtSequence(
                    new BtOpenBrowser("foo", new Uri("https://www.google.com")),
                    new BtCooldown(new BtSequence(
                        new BtSendKeysToProcess("foo", search),
                        new BtSendKey(Key.Return)), 3000)
                );

            SetBehaviour(behaviour);
        }
    }
}
