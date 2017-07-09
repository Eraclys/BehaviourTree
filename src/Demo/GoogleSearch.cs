using System;
using BehaviourTree;

namespace Demo
{
    public sealed class GoogleSearch : BtTree
    {
        public GoogleSearch(string search)
        {
            var behaviour = new OpenBrowser("foo", new Uri("https://www.google.com"));

            SetBehaviour(behaviour);
        }
    }
}
